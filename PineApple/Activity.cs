using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PineApple
{
    class Activity
    {
        private static int _referenceNumber=0;
        private int _number;
        private string _description;
        private int _indexOfGenericType;
        private int _indexOfType;
        private List<int> _astronautes;
        private int _location;
        private bool _externMission;
        private bool _spaceVehicle;
        private MDate _startDate;
        private MDate _endDate;

        /// <summary>
        /// 
        /// </summary>

        public Activity() {}
        public Activity(string description, int genericType ,int type, int location, List<int> astronautes, bool externMission, bool spaceVehicle, MDate startDate, MDate endDate)
        {
            _number = _referenceNumber;
            _referenceNumber++;
            _description = description;
            _indexOfGenericType = genericType;
            _indexOfType = type;
            _location = location;
            _externMission = externMission;
            _spaceVehicle = spaceVehicle;
            _astronautes = astronautes;
            _startDate = startDate;
            _endDate = endDate;
        }
        public int getNumber()
        {
            return _number;
        }
        public bool getExternBool()
        {
            return _externMission;
        }
        public string getDescription()
        {
            return _description;
        }
        public int getLocation()
        {
            return _location;
        }
        public List<int> getAstronautes()
        {
            return _astronautes;
        }
        public int getDay()
        {
            return _startDate.getDay();
        }
        public MDate getStartDate()
        {
            return _startDate;
        }
        public MDate getEndDate()
        {
            return _endDate;
        }
        public int getIndexOfGenericType()
        {
            return _indexOfGenericType;
        }
        public int getIndexOfType()
        {
            return _indexOfType;
        }
        public static void setRefNumber(int refNumber)
        {
            if(_referenceNumber==0)
            {
                _referenceNumber = refNumber;
            }
        }
        public void WriteXML(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode ReferenceNumber = xmlDoc.CreateElement("ReferenceNumber");
            ReferenceNumber.InnerText = _referenceNumber.ToString();
            rootNode.AppendChild(ReferenceNumber);

            XmlNode activity = xmlDoc.CreateElement("Activity");
            rootNode.AppendChild(activity);
          
            XmlNode Number = xmlDoc.CreateElement("Number");
            Number.InnerText = this._number.ToString();
            activity.AppendChild(Number);

            XmlNode Description = xmlDoc.CreateElement("Description");
            Description.InnerText = this._description.ToString();
            activity.AppendChild(Description);

            XmlNode GenericType = xmlDoc.CreateElement("GenerycType");
            GenericType.InnerText = this._indexOfGenericType.ToString();
            activity.AppendChild(GenericType);

            XmlNode Type = xmlDoc.CreateElement("Type");
            Type.InnerText = this._indexOfType.ToString();
            activity.AppendChild(Type);

            string astro = "";
            int c = 0;
            foreach (int i in this._astronautes)
            {
                if (c < this._astronautes.Count() - 1)
                {
                    astro += i.ToString() + " ";
                    c++;
                }
                else if (c == this._astronautes.Count() - 1)
                {
                    astro += i.ToString();
                }
            }
            XmlNode Astronautes = xmlDoc.CreateElement("Astronautes");
            Astronautes.InnerText = astro;
            activity.AppendChild(Astronautes);

            XmlNode Location = xmlDoc.CreateElement("Location");
            Location.InnerText = this._location.ToString();
            activity.AppendChild(Location);

            XmlNode ExternMission = xmlDoc.CreateElement("ExternMission");
            ExternMission.InnerText = this._externMission.ToString();
            activity.AppendChild(ExternMission);

            XmlNode SpaceVehicle = xmlDoc.CreateElement("SpaceVehicle");
            SpaceVehicle.InnerText = this._spaceVehicle.ToString();
            activity.AppendChild(SpaceVehicle);

            XmlNode StartDate = xmlDoc.CreateElement("StartDate");
            StartDate.InnerText = this._startDate.ToString();
            activity.AppendChild(StartDate);

            XmlNode EndDate = xmlDoc.CreateElement("EndDate");
            EndDate.InnerText = this._endDate.ToString();
            activity.AppendChild(EndDate);

            //___________________________________________________________
        }

    }
}
