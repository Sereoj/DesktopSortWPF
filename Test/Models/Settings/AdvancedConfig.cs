using System.Windows.Media;
using System.Xml.Serialization;
using Test.ViewModels;
using static Test.Models.Theme.Theme;

namespace Test.Models.Settings
{

    public class AdvancedConfig
    {
        [XmlAttribute]
        public string Background { get; set; }
        [XmlAttribute]
        public bool IsBackground { get; set; }
        [XmlAttribute]
        public bool Update { get; set; }
        [XmlAttribute]
        public ThemeTypes Theme { get; set; }
        [XmlAttribute]
        public Locale.Translation Language { get; set; }
        [XmlAttribute]
        public bool DeleteDefaultDirectory { get; set; }
        [XmlAttribute]
        public string InputPath { get; set; }
        [XmlAttribute]
        public string OutputPath { get; set; }
        [XmlAttribute]
        public ApplicationNavigationMode Mode { get; set; }
    }
}