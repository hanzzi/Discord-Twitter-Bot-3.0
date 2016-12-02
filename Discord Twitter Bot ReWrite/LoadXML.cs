using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Discord_Twitter_Bot_ReWrite
{
    class LoadXML
    {
        // initializes Load which loads the Config.xml file.
        public static void Main()
        {
            Load();
        }

        private static void Load()
        {
            try
            {
                string XML = Path.GetFullPath("Config.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(XML);
                SetInfo(doc);
            } catch (FileNotFoundException)
            {
                XMLGeneration.GenerateXML();
            }
            
        }
        
        private static void SetInfo(XmlDocument doc)
        {
            ConfigInfo.DiscordToken = doc.ChildNodes.Item(1).ChildNodes.Item(1).InnerText.ToString();

            ConfigInfo.YesNo = doc.ChildNodes.Item(1).ChildNodes.Item(3).InnerText.ToString();

            ConfigInfo.Username = doc.ChildNodes.Item(1).ChildNodes.Item(5).InnerText.ToString();

            ConfigInfo.Password = doc.ChildNodes.Item(1).ChildNodes.Item(7).InnerText.ToString();

            ConfigInfo.ConsumerKey = doc.ChildNodes.Item(1).ChildNodes.Item(9).InnerText.ToString();

            ConfigInfo.ConsumerSecret = doc.ChildNodes.Item(1).ChildNodes.Item(11).InnerText.ToString();

            ConfigInfo.AccessToken = doc.ChildNodes.Item(1).ChildNodes.Item(13).InnerText.ToString();

            ConfigInfo.AccessSecret = doc.ChildNodes.Item(1).ChildNodes.Item(15).InnerText.ToString();

        }
    }
}
