using System.Xml.Serialization;

namespace AlfaBank.Model
{
    [XmlRootAttribute("channel")]
    public class Channels
    {
        [XmlElement("item")]
        public Channel[] Items { get; set; }
    }
}
