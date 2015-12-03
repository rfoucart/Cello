using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineApple
{
    class TimeUnit
    {
        List<Activity> _activities;
        int _number;
        public TimeUnit(int number)
        {
            _activities = new List<Activity>(0);
            _number = number;
        }
        public void AddActivity(Activity activity)
        {
            _activities.Add(activity);
        }
    }
}
