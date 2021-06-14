using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models.Settings;
using Test.ViewModels.Base;

namespace Test.Models
{
    public class ModelCollection
    {
        public SettingsModel SettingsModel { get; set; }
        public Theme.Theme ThemeModel { get; set; }
        public Version VersionModel { get; set; }

        public ModelCollection()
        {
            var settings = new SettingsModel();
            SettingsModel = settings.Load<SettingsModel>("data.xml");

            ThemeModel = new Theme.Theme();
            VersionModel = new Version();
        }
    }
}
