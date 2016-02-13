﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel;

namespace CommercialRecordSystem.Common
{
    class CrsDictionary
    {
        private CrsDictionary()
        {
            Turkish = new SortedDictionary<string, SortedDictionary<string, string>>();
            English = new SortedDictionary<string, SortedDictionary<string, string>>();

            loadDictionaryFromFile("en-US/en.us.dictionary.xml", English);
            loadDictionaryFromFile("tr-TR/tr.tr.dictionary.xml", Turkish);
        }

        private SortedDictionary<string, SortedDictionary<string, string>> Turkish = null;
        private SortedDictionary<string, SortedDictionary<string, string>> English = null;

        public static readonly string CurrentLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

        public const string
                        TURKISH = "tr",
                        ENGLISH = "en";

        private static CrsDictionary instance = null;

        public static CrsDictionary getInstance()
        {
            if (null == instance)
            {
                instance = new CrsDictionary();
            }

            return instance;
        }

        public IEnumerable<string> getKeys(string dictionary)
        {
            if (English.ContainsKey(dictionary))
                return English[dictionary].Keys;
            else
                return null;
        }

        public IEnumerable<string> getValues(string dictionary)
        {
            if (English.ContainsKey(dictionary))
            {
                if (CurrentLanguage == TURKISH)
                    return Turkish[dictionary].Values;
                else
                    return English[dictionary].Values;
            }
            else
                return null;
        }

        public string lookup(string dictionary, string key, params object[] prms)
        {
            string remark = key;
            switch (CurrentLanguage)
            {
                case TURKISH:
                    if (Turkish.ContainsKey(dictionary) &&
                        Turkish[dictionary].ContainsKey(key))
                        remark = Turkish[dictionary][key];
                    break;
                case ENGLISH:
                    if (English.ContainsKey(dictionary) &&
                        English[dictionary].ContainsKey(key))
                        remark = English[dictionary][key];
                    break;
                default:
                    if (English.ContainsKey(dictionary) &&
                        English[dictionary].ContainsKey(key))
                        remark = English[dictionary][key];
                    break;
            }

            return String.Format(remark, prms);
        }

        private void loadDictionaryFromFile(string path, SortedDictionary<string, SortedDictionary<string, string>> dictionary)
        {
            string dictionaryXMLPath = Path.Combine(Package.Current.InstalledLocation.Path, "Strings/" + path);
            XDocument loadedData = XDocument.Load(dictionaryXMLPath);


            foreach (XElement dicElement in loadedData.Descendants("dictionary"))
            {
                SortedDictionary<string, string> dictionaryBuff = new SortedDictionary<string, string>();
                foreach (XNode node in dicElement.Nodes())
                {
                    XElement elementBuff = (XElement)node;
                    dictionaryBuff.Add(elementBuff.Attribute("key").Value, elementBuff.Value);
                }

                dictionary.Add(dicElement.Attribute("id").Value, dictionaryBuff);
            }
        }
    }
}
