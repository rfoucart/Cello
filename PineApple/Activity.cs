﻿using System;
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
        private string _name;
        private string _description;
        private int _genericType;
        private int _type;
        private List<int> _astronautes;
        private int _location;
        private bool _externMission;
        private bool _spaceVehicle;
        private MDate _startDate;
        private MDate _endDate;

        /// <summary>
        /// 
        /// </summary>
        public Activity(string name, string description, int genericType ,int type, int location, List<int> astronautes, bool externMission, bool spaceVehicle, MDate startDate, MDate endDate)
        {
            _number = _referenceNumber;
            _referenceNumber++;
            _name = name;
            _description = description;
            _genericType = genericType;
            _type = type;
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
        public string getName()
        {
            return _name;
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
        public MDate getStartDate()
        {
            return _startDate;
        }
        public MDate getEndDate()
        {
            return _endDate;
        }
        public int getGenericType()
        {
            return _genericType;
        }
        public int getType()
        {
            return _type;
        }
        public void WriteXML(XmlDocument xmlDoc, XmlNode rootNode)
        {
            XmlNode activity = xmlDoc.CreateElement("activity");
            rootNode.AppendChild(activity);

            XmlNode ReferenceNumber = xmlDoc.CreateElement("ReferenceNumber");
            ReferenceNumber.InnerText = _referenceNumber.ToString();
            activity.AppendChild(ReferenceNumber);
          
            XmlNode Number = xmlDoc.CreateElement("Number");
            Number.InnerText = this._number.ToString();
            activity.AppendChild(Number);

            XmlNode Name = xmlDoc.CreateElement("Name");
            Name.InnerText = this._name.ToString();
            activity.AppendChild(Name);

            XmlNode Description = xmlDoc.CreateElement("Description");
            Description.InnerText = this._description.ToString();
            activity.AppendChild(Description);

            XmlNode GenericType = xmlDoc.CreateElement("GenerycType");
            GenericType.InnerText = this._genericType.ToString();
            activity.AppendChild(GenericType);

            XmlNode Type = xmlDoc.CreateElement("Type");
            Type.InnerText = this._type.ToString();
            activity.AppendChild(Type);

            string astro = "";
            foreach (int i in this._astronautes)
            {
                astro += i.ToString() + ",";
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