namespace AlfaBank.Model
{
    public class Channel
    {
        [System.Xml.Serialization.XmlElement("title")]
        public string Titel { get; set; }
        [System.Xml.Serialization.XmlElement("link")]
        public string Link { get; set; }
        [System.Xml.Serialization.XmlElement("description")]
        public string Description { get; set; }
        [System.Xml.Serialization.XmlElement("category")]
        public string Category { get; set; }
        [System.Xml.Serialization.XmlElement("pubDate")]
        public string PubDate { get; set; }
    }
}