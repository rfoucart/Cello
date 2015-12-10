using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineApple
{
    class Type
    {
        private string _genericName;
        private string[] _typeNames;

        public Type(string genericName, string[] types)
        {
            _genericName = genericName;
            _typeNames = types;
        }
        public string getGenericType()
        {
            return _genericName;
        }
        public string getName()
        {
            return _genericName;
        }
        public string[] getTypes()
        {
            return _typeNames;
        }
    }
}
