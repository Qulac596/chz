using chz.WindowsServices.Setting;
using System;
using System.Xml;

namespace commoni.Setting
{
    public class SettingProvider : ISettingProvider
    {
        private const string SettingFile = "Setting.xml";
        private readonly XmlElement rootXmlElement;
        private const string NotFindElement = "Элемент не найден. Имя элмента: ";

        public SettingProvider()
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(SettingFile);
                rootXmlElement = xmlDocument.DocumentElement;
            }
            catch (Exception e)
            {
                throw new SettingException(e.Message, e);
            }
        }

        public string GetValue(string key)
        {
            foreach (XmlNode xnode in rootXmlElement)
            {
                if (xnode.Name == "add")
                {
                    var keyAttribute = xnode.Attributes.GetNamedItem("key");

                    if (keyAttribute != null)
                    {
                        if (keyAttribute.Value == key)
                        {
                            var valueAttribyte = xnode.Attributes.GetNamedItem("value");

                            if (valueAttribyte != null)
                            {
                                return valueAttribyte.Value;
                            }
                            else
                            {
                                throw new SettingException(NotFindElement + "value");
                            }
                        }
                    }
                    else
                    {
                        throw new SettingException(NotFindElement + "key");
                    }
                }
            }
            throw new SettingException(NotFindElement + key);
        }
    }
}
