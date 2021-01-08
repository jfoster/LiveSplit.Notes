using GitInfo;
using LiveSplit.Model;
using LiveSplit.Notes;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(NotesFactory))]

namespace LiveSplit.Notes
{
    class NotesFactory : IComponentFactory
    {
        public string ComponentName => "Notes";

        public string Description => "Notes for current split";

        public ComponentCategory Category => ComponentCategory.Information;

        public string UpdateName => ComponentName;

        public string XMLURL => "";

        public string UpdateURL => "";

        public Version Version => GitVersion.Short.ToVersion();

        public IComponent Create(LiveSplitState state) => new NotesComponent(this, state);
    }
}
