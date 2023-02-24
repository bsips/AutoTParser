using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AutoTParser
{
    //From Cacaroons AutoTranslate Voodoo
    public static class AutoTranslate
    {
        private const byte lang = 0x02; //Engrish onry
        private const string configFolder = "XML/"; 
        public static Dictionary<uint, string> list = new Dictionary<uint, string>();

        public static void Build()
        {
            int errorcount = 0;

            string atStrings = configFolder + "auto_translates.xml";
            string atItems = configFolder + "items.xml";
            string atKeyItems = configFolder + "key_items.xml";

            try
            {
                XDocument xStrings = XDocument.Load(atStrings);

                foreach (XElement element in xStrings.Descendants().Where(p => p.HasElements == false))
                {
                    uint id = GetFullStringID(Convert.ToUInt32(element.Attribute("id").Value));
                    string en = element.Attribute("en").Value;

                    list.Add(id, en);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errorcount++;
            }

            try
            {
                XDocument xItems = XDocument.Load(atItems);

                foreach (XElement element in xItems.Descendants().Where(p => p.HasElements == false))
                {
                    uint id = GetFullItemID(Convert.ToUInt32(element.Attribute("id").Value));
                    string en = element.Attribute("en").Value;

                    list.Add(id, en);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errorcount++;
            }

            try
            {
                XDocument xKeyItems = XDocument.Load(atKeyItems);

                foreach (XElement element in xKeyItems.Descendants().Where(p => p.HasElements == false))
                {
                    uint id = GetFullKeyItemID(Convert.ToUInt32(element.Attribute("id").Value));
                    string en = element.Attribute("en").Value;

                    list.Add(id, en);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errorcount++;
            }
            if (errorcount > 0)
            {
                Console.WriteLine($"Finished with {errorcount} error(s). Some AutoTranslate tables were not completed");
            }
            else
            {
                Console.WriteLine("AutoTranslate tables completed from XMLs - Exporting to file");
            }
        }

        private static uint GetFullStringID(uint value)
        {
            byte[] b =
            {
                02,
                lang,
                (byte)Math.Floor((double)value / 256),
                (byte)(value % 256)
            };
            uint fullvalue = BitConverter.ToUInt32(b, 0);
            return fullvalue;
        }

        private static uint GetFullItemID(uint value)
        {
            byte type = 0x07;
            byte firstbyte = (byte)Math.Floor((double)value / 256);
            byte secondbyte = (byte)(value % 256);

            if (firstbyte == 0x00)
            {
                type = 0x09;
                firstbyte = 0xFF;
            }
            else if (secondbyte == 0x00)
            {
                type = 0x0A;
                secondbyte = 0xFF;
            }
            byte[] b =
            {
                type,
                lang,
                firstbyte,
                secondbyte
            };
            uint fullvalue = BitConverter.ToUInt32(b, 0);
            return fullvalue;
        }

        private static uint GetFullKeyItemID(uint value)
        {
            byte type = 0x13;
            byte firstbyte = (byte)Math.Floor((double)value / 256);
            byte secondbyte = (byte)(value % 256);

            if (firstbyte == 0x00)
            {
                type = 0x15;
                firstbyte = 0xFF;
            }
            else if (secondbyte == 0x00)
            {
                type = 0x16;
                secondbyte = 0xFF;
            }
            byte[] b =
            {
                type,
                lang,
                firstbyte,
                secondbyte
            };
            uint fullvalue = BitConverter.ToUInt32(b, 0);
            return fullvalue;
        }
    }
}
