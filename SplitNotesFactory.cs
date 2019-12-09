using LiveSplit.Model;
using LiveSplit.SplitNotes;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(SplitNotesFactory))]

namespace LiveSplit.SplitNotes
{
    class SplitNotesFactory : IComponentFactory
    {
        public string ComponentName => "Split Notes";

        public string Description => "Notes for current split";

        public ComponentCategory Category => ComponentCategory.Information;

        public string UpdateName => ComponentName;

        public string XMLURL => "";

        public string UpdateURL => "";

        public Version Version => Version.Parse("1.0.0");

        public IComponent Create(LiveSplitState state) => new SplitNotesComponent(state);
    }
}
