using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PineApple
{
    class Astronaute
    {
        private string _name;
        private static int _referenceNumber = 0;
        private int _number;
        public Astronaute(string name)
        {
            _number = _referenceNumber;
            _referenceNumber++;
            _name = name;
        }
        public Astronaute(string name,int number)
        {
            _number = number;
            _name = name;
        }
        public static int getRefNumber()
        {
            return _referenceNumber;
        }
        public static void setRefNumber(int refNumber)
        {
            if (_referenceNumber == 0)
            {
                _referenceNumber = refNumber;
            }
        }
        public string getName()
        {
            return _name;
        }
        public int getNumber()
        {
            return _number;
        }
        public void WriteXML(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode astronaute = xmlDoc.CreateElement("Astronaute");
            rootNode.AppendChild(astronaute);

            XmlNode Name = xmlDoc.CreateElement("Name");
            Name.InnerText = _name.ToString();
            astronaute.AppendChild(Name);

            XmlNode Number = xmlDoc.CreateElement("Number");
            Number.InnerText = _number.ToString();
            astronaute.AppendChild(Number);
        }
    }
}
