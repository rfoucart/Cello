using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace PineApple
{
    class Mission
    {
        private List<Activity> _activities;
        private List<Day> _days;
        private List<Location> _locations;
        private List<Astronaute> _astronautes;
        private int _numberOfDays;
        private string _name;
        private List<GenericType> _genericTypes;
        private MDate _currentDay;
        private DateTime _firstDayEarth;
        private DateTime _CurrentDayEarth;

        
        /// <summary>
        /// 
        /// </summary>
        public Mission(string name, int numberOfDays)
        {
            _CurrentDayEarth = DateTime.Now;
            _numberOfDays = numberOfDays;
            _activities = new List<Activity>(0);
            _astronautes = new List<Astronaute>(0);
            _locations = new List<Location>(0);
            _days = new List<Day>(_numberOfDays);
            _genericTypes = new List<GenericType>(0);
            _name = name;
            string[] Living = {"Eating","Sleeping","Entertainment","Private","Health control","Medical Act"};
            string[] Science = {"Exploration","Briefing","Debriefing","Inside Experiment","Outside Experiment"};
            string[] Maintenance = {"Cleaning","LSS air system","LSS water system","LSS food system","Power systems","Space suit","Other"};
            string[] Communication = {"Sending message","Receiving message"};
            string[] Repair = { "LSS", "Power systems", "Communication systems","Propulsion systems","Habitat","Space suit","Vehicle"};
            string[] Emergency = {"None"};
            _genericTypes.Add(new GenericType("Living",Living));
            _genericTypes.Add(new GenericType("Science", Science));
            _genericTypes.Add(new GenericType("Maintenance", Maintenance));
            _genericTypes.Add(new GenericType("Communication", Communication));
            _genericTypes.Add(new GenericType("Repair", Repair));
            _genericTypes.Add(new GenericType("Emergency", Emergency));

        }


        /// <summary>
        /// Add an activity
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="genericType"></param>
        /// <param name="type"></param>
        /// <param name="location"></param>
        /// <param name="astronautes"></param>
        /// <param name="externMission"></param>
        /// <param name="spaceVehicle"></param>
        public void newActivity(string name, string description,int genericType, int type, int location, List<int> astronautes, bool externMission, bool spaceVehicle, MDate startDate, MDate endDate)
        {
            _activities.Add(new Activity(name, description, genericType, type, location, astronautes, externMission, spaceVehicle, startDate, endDate));
        }

        /// <summary>
        /// Delete an activity
        /// </summary>
        /// <param name="number"></param>
        public void deleteActivity(int number)
        {
            _activities.RemoveAll(x => x.getNumber() == number);
        }
        public void newLocation(string name, int posx, int posy)
        {
            _locations.Add(new Location(name, posx, posy));
        }
        public void newLocation(string name, int posx, int posy, int number)
        {
            _locations.Add(new Location(name, posx, posy, number));
        }
        public void newAstronaute(string name)
        {
            _astronautes.Add(new Astronaute(name));
        }
        public void newAstronaute(string name, int number)
        {
            _astronautes.Add(new Astronaute(name,number));
        }
        /// <summary>
        /// fill the days and time unites from the activities list
        /// </summary>
        public void fillTimeUnites()
        {

        }
        /// <summary>
        /// fill activities from the XML file
        /// </summary>
        public void fillActivities()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchDates"></param>
        /// <returns></returns>
        public List<Activity> searchByPeriod(MDate[] searchDates)
        {
            return new List<Activity>(0);
        }

        /// <summary>
        /// search method by day
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        public List<Activity> searchByDate(MDate searchDate)
        {
            return new List<Activity>(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Activity> searchByType(string type)
        {
            return new List<Activity>(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="genericType"></param>
        /// <returns></returns>
        public List<Activity> searchByGenericType(string genericType)
        {
            return new List<Activity>(0);
        }


        //     \\  //   | \  / |   ||
        //      \\//    ||\\//||   ||
        //      //\\    ||    ||   ||
        //     //  \\   ||    ||   |_ _

        public void WriteXML(XmlDocument xmlDoc, XmlNode rootNode)
        {

            XmlNode Mission = xmlDoc.CreateElement("Mission");
            rootNode.AppendChild(Mission);

            XmlNode Name = xmlDoc.CreateElement("Name");
            Name.InnerText = _name.ToString();
            Mission.AppendChild(Name);

            XmlNode Astronautes = xmlDoc.CreateElement("Astronautes");
            Mission.AppendChild(Astronautes);

            XmlNode refNumberAstronautes = xmlDoc.CreateElement("refNumberAstronaute");
            refNumberAstronautes.InnerText = Astronaute.getRefNumber().ToString();
            Mission.AppendChild(refNumberAstronautes);

            foreach(Astronaute a in _astronautes )
            {
                a.WriteXML(xmlDoc,Astronautes);
            }

            XmlNode Locations = xmlDoc.CreateElement("Locations");
            Mission.AppendChild(Locations);

            XmlNode refNumberLoc = xmlDoc.CreateElement("refNumberLoc");
            refNumberLoc.InnerText = Location.getRefNumber().ToString();
            Mission.AppendChild(refNumberLoc);

            foreach(Location l in _locations)
            {
                l.WriteXML(xmlDoc, Locations);
            }

        }
        public void WriteActivityXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("Activity");
            xmlDoc.AppendChild(rootNode);

            foreach (Activity activity in _activities)
            {
                activity.WriteXML(xmlDoc, rootNode);
            }

            xmlDoc.Save(".activities.xml");
        }
        /*private void ReadActivityXML()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("mission.xml");
            var name = doc.SelectSingleNode("/Missions/Mission/Name");
            var astronautes = doc.SelectNodes("/Missions/Mission/Astronautes/Astronaute");
            var refastro = doc.SelectSingleNode("/Missions/Mission/refNumberAstronaute");
            var locations = doc.SelectNodes("/Missions/Mission/Locations/Location");
            var reflocation = doc.SelectSingleNode("/Missions/Mission/refNumberLoc");

            //Create the mission
            mission = new Mission(name.InnerText, numberOfDays);

            //Add the astronautes
            foreach (XmlNode astronaute in astronautes)
            {
                mission.newAstronaute(astronaute["Name"].InnerText, int.Parse(astronaute["Number"].InnerText));
            }

            //Add the ref number to the class
            Astronaute.setRefNumber(int.Parse(refastro.InnerText));

            foreach (XmlNode location in locations)
            {
                mission.newLocation(location["Name"].InnerText, int.Parse(location["POSX"].InnerText), int.Parse(location["POSY"].InnerText), int.Parse(location["Number"].InnerText));
            }

            //Add the ref number to the class
            PineApple.Location.setRefNumber(int.Parse(reflocation.InnerText));
        }*/
        public MDate earthToMarsDate(DateTime earthDate)
        {
            TimeSpan lengthFromBeginning = earthDate - _firstDayEarth;
            double days=(lengthFromBeginning.Hours+lengthFromBeginning.Minutes/60.0)/24.4;
            double hours=24.4*days-(int)(days);
            double minutes =(hours - (int)(hours))*60;
             
            return new MDate((int)(days),(int)(hours),(int)(minutes));
        }
    }
}
