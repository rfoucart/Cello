using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PineApple
{
    class Location
    {
        private static int _referenceNumber = 0;
        private int _number;
        private string _name;
        private int _posx;
        private int _posy;
        private const int _width = 1095;
        private const int _height=2053;
        private const int _abZero =700;
        private const int _ordZero = 1000;
        private const int _metrePixel = 5;//5m par pixels

        public Location(string name, int posx, int posy)
        {
            _referenceNumber++;
            _number = _referenceNumber;
            _name = name;
            _posx = posx;
            _posy = posy;
        }
        public Location(string name, int posx, int posy, int number )
        {
            _number = number;
            _name = name;
            _posx = posx;
            _posy = posy;
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
        /// <summary>
        /// Return (array) the location in pixels/the origin 
        /// </summary>
        /// <returns></returns>
        public int[] getLocation()
        {
            int[] tab={_posx,_posy};
            return tab;
        }
        public void setLocation(int posx, int posy)
        {
            _posx = posx;
            _posy = posy;
        }
        public void WriteXML(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode location = xmlDoc.CreateElement("Location");
            rootNode.AppendChild(location);

            XmlNode Name = xmlDoc.CreateElement("Name");
            Name.InnerText = _name.ToString();
            location.AppendChild(Name);

            XmlNode POSX = xmlDoc.CreateElement("POSX");
            POSX.InnerText = _posx.ToString();
            location.AppendChild(POSX);

            XmlNode POSY = xmlDoc.CreateElement("POSY");
            POSY.InnerText = _posy.ToString();
            location.AppendChild(POSY);

            XmlNode Number = xmlDoc.CreateElement("Number");
            Number.InnerText = _number.ToString();
            location.AppendChild(Number);
        }
    }
}
