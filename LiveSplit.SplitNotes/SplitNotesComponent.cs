using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using YamlDotNet.Serialization;

namespace LiveSplit.SplitNotes
{
    class SplitNotesComponent : IComponent
    {
        private readonly IComponentFactory Factory;

        private readonly SplitNotesSettings Settings;

        private Dictionary<string, SplitNote> Notes;

        private SplitNote CurrentNote;

        public SplitNotesComponent(IComponentFactory factory, LiveSplitState state)
        {
            Factory = factory;
            Settings = new SplitNotesSettings();

            // Creates notes file if it doesn't exit
            Notes = GetNotes((Run)state.Run);
            CurrentNote = new SplitNote("", ComponentName);

            state.RunManuallyModified += DoStart;
            state.OnStart += DoStart;

            state.OnSplit += DoSplit;
            state.OnSkipSplit += DoSplit;
            state.OnUndoSplit += DoSplit;

            state.OnReset += DoReset;
        }

        private void DoStart(object sender, EventArgs e)
        {
            LiveSplitState state = (LiveSplitState)sender;
            // Re-read notes file on starting run
            Notes = GetNotes((Run)state.Run);
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
            CurrentNote = new SplitNote("", ComponentName);
        }

        private void Do(LiveSplitState state)
        {
            if (state.CurrentSplit != null && Notes != null && Notes.Count > 0)
            {
                var key = state.CurrentSplit.Name;
                CurrentNote = Notes[key];
            }
        }

        private Dictionary<string, SplitNote> GetNotes(Run run)
        {
            Dictionary<string, string[]> yaml = null;

            if (run.GameName is "")
            {
                return null;
            }

            var filename = new Regex(@"(.+)\.\w+$").Replace(run.FilePath, "$1.yml");

            if (File.Exists(filename))
            {
                using (StreamReader reader = File.OpenText(filename))
                {
                    var ds = new DeserializerBuilder().Build();
                    yaml = ds.Deserialize<Dictionary<string, string[]>>(reader);
                }
            }

            if (yaml is null)
            {
                yaml = new Dictionary<string, string[]>();
            }

            foreach (var segment in run)
            {
                var key = segment.Name;
                if (!yaml.ContainsKey(key))
                {
                    yaml.Add(segment.Name, new string[] { "example", "text" });
                }
            }

            using (StreamWriter writer = new StreamWriter(filename))
            {
                var s = new SerializerBuilder().Build();
                s.Serialize(writer, yaml);
            }

            return yaml.ToDictionary(kvp => kvp.Key, kvp => new SplitNote(kvp.Key, kvp.Value)); ;
        }

        public string ComponentName => Factory.ComponentName;

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
            if (CurrentNote != null)
            {
                SizeF s = new SizeF(width, height);
                CurrentNote.Draw(g, s, state.LayoutSettings.TextFont, state.LayoutSettings.TextColor);
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
