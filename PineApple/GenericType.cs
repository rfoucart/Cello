using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineApple
{
    class GenericType
    {
        private string _name;
        private string[] _types;

        public GenericType(string name, string[] types)
        {
            _name = name;
            _types = types;
        }
        public string getName()
        {
            return _name;
        }
        public string[] getTypes()
        {
            return _types;
        }
    }
}
