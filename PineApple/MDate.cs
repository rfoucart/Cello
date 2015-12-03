using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineApple
{
    class MDate
    {
        int _dayNumber;
        int _hours;
        int _minutes;
        public MDate()
        {
            _dayNumber = 0;
            _hours = 0;
            _minutes = 0;
        }
        public MDate(int dayNumber, int hours, int minutes)
        {
            _dayNumber = dayNumber;
            _hours = hours;
            _minutes = minutes;
        }
        public string ToString()
        {
            return string.Format("{0} {1} {2}", _dayNumber, _hours, _minutes);
        }
        public void ParseMDate(string mdateString)
        {
            string[] mdateTab = mdateString.Split(' ');
            _dayNumber=int.Parse(mdateTab[0]);
            _hours = int.Parse(mdateTab[1]);
            _minutes = int.Parse(mdateTab[2]);
        }
    }
}
