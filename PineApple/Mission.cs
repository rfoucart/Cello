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
        private List<Type> _genericTypes;
        private MDate _currentDay;
        private DateTime _firstDayEarth;
        private DateTime _currentDayEarth;

        
        /// <summary>
        /// 
        /// </summary>
        public Mission(string name, int numberOfDays)
        {
            _currentDayEarth = DateTime.Now;
            _numberOfDays = numberOfDays;
            _activities = new List<Activity>(0);
            _astronautes = new List<Astronaute>(0);
            _locations = new List<Location>(0);
            _days = new List<Day>(_numberOfDays);
            _genericTypes = new List<Type>(0);
            _name = name;
            string[] Living = {"Eating","Sleeping","Entertainment","Private","Health control","Medical Act"};
            string[] Science = {"Exploration","Briefing","Debriefing","Inside Experiment","Outside Experiment"};
            string[] Maintenance = {"Cleaning","LSS air system","LSS water system","LSS food system","Power systems","Space suit","Other"};
            string[] Communication = {"Sending message","Receiving message"};
            string[] Repair = { "LSS", "Power systems", "Communication systems","Propulsion systems","Habitat","Space suit","Vehicle"};
            string[] Emergency = {"None"};
            _genericTypes.Add(new Type("Living",Living));
            _genericTypes.Add(new Type("Science", Science));
            _genericTypes.Add(new Type("Maintenance", Maintenance));
            _genericTypes.Add(new Type("Communication", Communication));
            _genericTypes.Add(new Type("Repair", Repair));
            _genericTypes.Add(new Type("Emergency", Emergency));

            _currentDay = new MDate(350,0,0);
        }
        public List<Type> getActivityTypes()
        {
            return _genericTypes;
        }
        public MDate getCurrentDay()
        {
            return _currentDay;
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
        public void newActivity(string description,int genericType, int type, int location, List<int> astronautes, bool externMission, bool spaceVehicle, MDate startDate, MDate endDate)
        {
            _activities.Add(new Activity(description, genericType, type, location, astronautes, externMission, spaceVehicle, startDate, endDate));
        }
        public void defaultDay(int jour)
        {
            List<int> astro=_astronautes.Select(a=>a.getNumber()).ToList();

            _activities.Add(new Activity(" ", 0, 0, 1, astro, false, false, new MDate(jour, 0, 0), new MDate(jour, 7, 0)));
            _activities.Add(new Activity(" ", 0, 1, 1, astro, false, false, new MDate(jour, 7, 0), new MDate(jour, 8, 0)));
            _activities.Add(new Activity(" ", 0, 2, 1, astro, false, false, new MDate(jour, 8, 0), new MDate(jour, 12, 0)));
            _activities.Add(new Activity(" ", 0, 1, 1, astro, false, false, new MDate(jour, 12, 0), new MDate(jour, 14, 0)));
            _activities.Add(new Activity(" ", 0, 3, 1, astro, false, false, new MDate(jour, 19, 0), new MDate(jour, 21, 0)));
            _activities.Add(new Activity(" ", 0, 3, 1, astro, false, false, new MDate(jour, 21, 0), new MDate(jour, 23, 0)));
            _activities.Add(new Activity(" ", 0, 1, 1, astro, false, false, new MDate(jour, 23, 0), new MDate(jour, 23, 40)));

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
        public List<Astronaute> getAstronautes()
        {
            return _astronautes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="genericType"></param>
        /// <returns></returns>
        public List<searchResult> searchByType(int GT, int T, int periodStart, int periodEnd)
        {
            List<Activity> result = _activities.Where(x=> x.getType()==T && x.getGenericType()==GT && periodStart <= x.getDay() && periodEnd >= x.getDay()).ToList();
            List<searchResult> Result = new List<searchResult>(0);
            foreach(Activity a in result)
            {
                searchResult r=new searchResult();
                r.types = string.Format("{0} - {1}", _genericTypes[GT].getName(), _genericTypes[GT].getTypes()[T]);
                r.startDate = a.getStartDate().printMDate();
                r.endDate = a.getEndDate().printMDate();
                r.a=a;
                Result.Add(r);
            }
            return Result ;
        }
        public List<searchResult> searchByKeyword(string keyword, int periodStart, int periodEnd)
        {
            List<Activity> result = _activities.Where(x => x.getDescription().IndexOf(keyword)!=-1 && periodStart <= x.getDay() && periodEnd >= x.getDay()).ToList();
            List<searchResult> Result = new List<searchResult>(0);
            foreach (Activity a in result)
            {
                searchResult r = new searchResult();
                r.types = string.Format("{0} - {1}", _genericTypes[a.getGenericType()].getName(), _genericTypes[a.getGenericType()].getTypes()[a.getType()]);
                r.startDate = a.getStartDate().printMDate();
                r.endDate = a.getEndDate().printMDate();
                r.a = a;
                Result.Add(r);
            }
            return Result;

        }
        public List<Activity> selectActivitiesByDay(int day)
        {
            return _activities.Where(x => x.getDay() == day).ToList();
        }
        // retourne true si il y a une activité en exté ce jour là
        public bool outThisDay(int day)
        {
            bool ext = false;
            List<Activity> activities = selectActivitiesByDay(day);
            if(activities.Count!=0)
            {
                foreach(Activity a in activities)
                {
                    if(a.getExternBool()==true)
                    { ext=true; }
                }   
            }
            return ext;
        }
        public void setCurrentDate()
        {
            _currentDayEarth = DateTime.Now;
            _currentDay = earthToMarsDate(_currentDayEarth);

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
            XmlNode rootNode = xmlDoc.CreateElement("Activities");
            xmlDoc.AppendChild(rootNode);

            foreach (Activity activity in _activities)
            {
                activity.WriteXML(xmlDoc, rootNode);
            }

            xmlDoc.Save("./activities.xml");
        }
        public void ReadActivityXML()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("activities.xml");
            var refNum = doc.SelectSingleNode("/Activities/ReferenceNumber");
            var activities = doc.SelectNodes("/Activities/Activity");

            //Add the astronautes
            foreach (XmlNode activity in activities)
            {
                MDate endDate = new MDate();
                MDate startDate = new MDate();
                endDate.ParseMDate(activity["EndDate"].InnerText);
                startDate.ParseMDate(activity["StartDate"].InnerText);
                string[] astro = activity["Astronautes"].InnerText.Split(' ');
                List<int> _astro = new List<int>(0);
                foreach(string a in astro)
                {
                    _astro.Add(int.Parse(a));
                }
                newActivity(activity["Description"].InnerText, int.Parse(activity["GenerycType"].InnerText), int.Parse(activity["Type"].InnerText), int.Parse(activity["Location"].InnerText), _astro, bool.Parse(activity["ExternMission"].InnerText), bool.Parse(activity["SpaceVehicle"].InnerText), startDate, endDate);
            }

            //Add the ref number to the class
            Activity.setRefNumber(int.Parse(refNum.InnerText));
        }
        //Date martienne en fonction de la date terrestre 
        public MDate earthToMarsDate(DateTime earthDate)
        {
            TimeSpan lengthFromBeginning = earthDate - _firstDayEarth;
            double days=(lengthFromBeginning.Hours+lengthFromBeginning.Minutes/60.0)/24.4;
            double hours=24.4*days-(int)(days);
            double minutes =(hours - (int)(hours))*60;
            
            return new MDate((int)(days),(int)(hours),(int)(minutes));
        }
        //Terrestre en fonction de la date martienne
        public string marsToEarthDate()
        {
            return ""; 
        }

        public struct searchResult
        {
            public string types;
            public string startDate;
            public string endDate;
            public Activity a;
        }
    }
}
