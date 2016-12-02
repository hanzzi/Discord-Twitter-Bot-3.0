using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Discord_Twitter_Bot_ReWrite
{
    class XMLGeneration
    {
        private static bool GenXml = false;

        public static void GenerateXML()
        {
            GenXml = true;
            Console.WriteLine("Config.xml file not found do you wish to generate a new template");
            Console.WriteLine("Y/N");
            string key = Console.ReadLine();

            // Acknowlegdement with generating a new config.xml
            if (key == "y" | key == "Y")
            {

                // If no Config file exists generate a new config.xml
                if (!File.Exists(Path.GetFullPath("Config.xml")))
                {
                    XmlWriterSettings Settings = new XmlWriterSettings();
                    //Settings.Encoding = System.Text.Encoding.UTF8;
                    Settings.Indent = true;

                    // Generates a new Config file.
                    using (XmlWriter Writer = XmlWriter.Create(Path.GetFullPath("Config.xml"), Settings))
                    {

                        Writer.WriteStartDocument();
                        Writer.WriteStartElement("Config");

                        Writer.WriteComment(" Enter DiscordToken Here you can create a Bot here https://discordapp.com/developers/applications/me");
                        Writer.WriteElementString("DiscordToken", "InsertDiscordTokenHere");

                        Writer.WriteComment(" True will enable Manual Login in the CommandLine, False will enable Username and Password fields for automatic login");
                        Writer.WriteElementString("ManualAuth", "True/False");

                        Writer.WriteComment(" Enter Twitter Username Here");
                        Writer.WriteElementString("Username", "Twitter Username or email here");

                        Writer.WriteComment(" Enter Twitter Password Here");
                        Writer.WriteElementString("Password", "Twitter Password Here");

                        Writer.WriteComment(" Enter Twitter Consumerkey here you can get a Twitter App here https://apps.twitter.com/");
                        Writer.WriteElementString("Consumerkey", "Consumer key here");

                        Writer.WriteComment(" Enter Twitter ConsumerSecret here");
                        Writer.WriteElementString("ConsumerSecret", "Consumer Secret here");

                        Writer.WriteComment(" Enter Twitter AccessToken Here");
                        Writer.WriteElementString("AccessToken", "Access Token Here");

                        Writer.WriteComment(" Enter Twitter AccessSecret here \t");
                        Writer.WriteElementString("AccessSecret", "Access Secret here");

                        Writer.WriteEndElement();
                        Writer.WriteEndDocument();
                    }

                    Console.WriteLine("Config File Generated Please Restart Application");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    Environment.Exit(0);

                }
                else
                {
                    Console.WriteLine("ERROR Unknown Error has been detected please restart" + Environment.NewLine + "Press any key to continue");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
            else if (key == "n" | key == "N")
            {
                Console.WriteLine("ERROR program cannot continue without a config.xml, please generate Config.xml");
                Console.WriteLine("Press any key to shutdown");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

    }
}
