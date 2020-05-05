using GitInfo;
using LiveSplit.Model;
using LiveSplit.SplitNotes;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(SplitNotesFactory))]

namespace LiveSplit.SplitNotes
{
    class SplitNotesFactory : IComponentFactory
    {
        public string ComponentName => "LiveSplit.SplitNotes";

        public string Description => "Notes for current split";

        public ComponentCategory Category => ComponentCategory.Information;

        public string UpdateName => ComponentName;

        public string XMLURL => "";

        public string UpdateURL => "";

        public Version Version => GitVersion.Short.ToVersion();

        public IComponent Create(LiveSplitState state) => new SplitNotesComponent(this, state);
    }
}
