using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace IntermediateAssessment.Samples
{
    public enum UserState
    {
        Base,
        Contact
    }

    public class User
    {
        [XmlAttribute()]
        public long ID;

        [XmlElement(ElementName = "Name")]
        public string UserName;

        [XmlElement(ElementName = "FirstName")]
        public string First;

        [XmlElement(ElementName = "LastName")]
        public string Last;

        [XmlElement(ElementName = "Phone")]
        public string Phone;

        [XmlAttribute()]
        public UserState State;
    }

    public class BotState
    {
        [XmlElement(ElementName = "User")]
        public User[] Users;

        public static BotState Load(string name)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(BotState));

                using (XmlReader rdr = XmlReader.Create(name))
                {
                    return (BotState)ser.Deserialize(rdr);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                return new BotState();
            }
        }

        public void Save(string name)
        {
            XmlSerializer ser = new XmlSerializer(typeof(BotState));
            XmlWriterSettings s = new XmlWriterSettings()
            {
                Indent = true 
            };
            using (XmlWriter wrt = XmlWriter.Create(name, s))
            {
                ser.Serialize(wrt, this);
            }
        }
    }
}