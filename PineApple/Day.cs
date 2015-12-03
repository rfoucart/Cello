using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineApple
{
    class Day
    {
        private static int _referenceNumber = 0;
        private int _number;
        private List<TimeUnit> TimeUnits;
        const int _numberOfTUperDay = 148;//24*6+40/10
        private string _dayRecap;

        public Day()
        {
            TimeUnits = new List<TimeUnit>(0);
            for (int i = 0; i < _numberOfTUperDay; i++)
            {
                TimeUnits.Add(new TimeUnit(i+1));
            }
            _referenceNumber++;
            _number = _referenceNumber;
            _dayRecap = "";
        }
        public int getNumber()
        {
            return _number;
        }
        public string getDayRecap()
        {
            return _dayRecap;
        }
        public void setDayRecap(string dayRecap)
        {
            _dayRecap=dayRecap;
        }

    }
}
