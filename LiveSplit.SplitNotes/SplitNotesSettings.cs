using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.UI;

namespace LiveSplit.SplitNotes
{
    public partial class SplitNotesSettings : UserControl
    {
        public float ComponentSize { get; set; }

        public event EventHandler SettingChanged;

        public SplitNotesSettings()
        {
            InitializeComponent();

            sizeUpDown.DataBindings.Add(nameof(sizeUpDown.Value), this, nameof(ComponentSize), true, DataSourceUpdateMode.OnPropertyChanged).BindingComplete += OnSettingChanged;
        }

        private void OnSettingChanged(object sender, BindingCompleteEventArgs e)
        {
            SettingChanged?.Invoke(this, e);
        }

        internal XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            SettingsHelper.CreateSetting(document, parent, nameof(ComponentSize), ComponentSize);
            return parent;
        }

        internal void SetSettings(XmlNode settings)
        {
            ComponentSize = SettingsHelper.ParseFloat(settings[nameof(ComponentSize)]);
        }
    }
}
