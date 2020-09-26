using System.Xml.Serialization;

namespace IntermediateAssessment.Samples
{
    [XmlRoot(ElementName ="Dict")]
    public class Dictionary
    {
        [XmlElement()]
        public Word[] Words;

        public Dictionary()
        {
            Word word = new Word()
            {
                Name = "ГОСТ",
                Approved = true,
                Source = "Словарь",
                LongCount = 0,
                Description = "Государственный стандарт"
            };
            Words = new Word[] { word };
        }
    }

    public class Word
    {
        [XmlAttribute(AttributeName = "Word")]
        public string Name;

        [XmlAttribute()]
        public bool Approved;

        [XmlAttribute()]
        public string Source;

        [XmlAttribute()]
        public int LongCount;

        [XmlText()]
        public string Description;
    }
}