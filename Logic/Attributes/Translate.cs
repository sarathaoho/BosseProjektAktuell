using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Attributes
{
    // ANVÄNDS EJ ÄN
    class Translate : Attribute
    {
        public string Name { get; set; }
        public string Extra { get; set; }

        public Translate(string name, string extra)
        {
            Name = name;
            Extra = extra;
        }
    }
}
