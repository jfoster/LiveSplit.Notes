using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using YamlDotNet.Serialization;

namespace LiveSplit.SplitNotes
{
    class SplitNotesComponent : IComponent
    {
        protected SplitNotesSettings Settings { get; set; }

        protected List<SplitNote> Notes { get; set; }

        protected SplitNote CurrentNote;

        public SplitNotesComponent(LiveSplitState state)
        {
            Settings = new SplitNotesSettings();

            // Creates notes file if it doesn't exit
            Notes = GetNotes(state);

            state.OnStart += DoStart;
            state.OnSplit += DoSplit;
            state.OnUndoSplit += DoSplit;
            state.OnReset += DoReset;
            state.RunManuallyModified += DoStart;
        }

        private void DoStart(object sender, EventArgs e)
        {
            LiveSplitState state = (LiveSplitState)sender;
            // Re-read notes file on starting run
            Notes = GetNotes(state);
            Do(state);
        }

        private void DoSplit(object sender, EventArgs e)
        {
            LiveSplitState state = (LiveSplitState)sender;
            Do(state);
        }

        private void DoReset(object sender, TimerPhase value)
        {
            LiveSplitState state = (LiveSplitState)sender;
            Do(state);
        }

        private void Do(LiveSplitState state)
        {
            if (Notes.Count > 0 && state.CurrentSplit != null)
            {
                CurrentNote = Notes.FirstOrDefault(f => f.Name == state.CurrentSplit.Name);
            }
        }

        private List<SplitNote> GetNotes(LiveSplitState state)
        {
            var filename = new Regex(@"(.+)\.\w+$").Replace(state.Run.FilePath, "$1.yml");

            if (File.Exists(filename))
            {
                using (StreamReader reader = File.OpenText(filename))
                {
                    var ds = new DeserializerBuilder().Build();
                    return ds.Deserialize<List<SplitNote>>(reader);
                }
            }

            var notes = new List<SplitNote>();

            foreach (var segment in state.Run)
            {
                notes.Add(new SplitNote
                {
                    Name = segment.Name,
                    Text = new string[] { "example" }
                });
            }

            using (StreamWriter writer = new StreamWriter(filename))
            {
                var s = new SerializerBuilder().Build();
                s.Serialize(writer, notes);
            }

            return notes;
        }

        public string ComponentName => "Split Notes";

        public float HorizontalWidth => Settings.ComponentSize == 0 ? 128f : Settings.ComponentSize;
        public float VerticalHeight => Settings.ComponentSize == 0 ? 128f : Settings.ComponentSize;

        public float MinimumHeight => 16f;
        public float MinimumWidth => 16f;

        public float PaddingBottom => 8f;
        public float PaddingLeft => 8f;
        public float PaddingRight => 8f;
        public float PaddingTop => 8f;

        public IDictionary<string, Action> ContextMenuControls => null;

        private void Draw(Graphics g, LiveSplitState state, float width, float height, Region clipRegion)
        {
            if (state.CurrentPhase != TimerPhase.NotRunning)
            {
                if (CurrentNote != null)
                {
                    SizeF s = new SizeF(width, height);
                    CurrentNote.Draw(g, s, state.LayoutSettings.TextFont, state.LayoutSettings.TextColor);
                }
            }
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            Draw(g, state, HorizontalWidth, height, clipRegion);
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            Draw(g, state, width, VerticalHeight, clipRegion);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            return Settings.GetSettings(document);
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            return Settings;
        }

        public void SetSettings(XmlNode settings)
        {
            Settings.SetSettings(settings);
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            if (invalidator != null)
            {
                invalidator.Invalidate(0, 0, width, height);
            }
        }

        public void Dispose()
        {

        }
    }
}
