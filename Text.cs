using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ExperimentalEntropyEstimation
{
    class Text
    {
        public string txt;
        public int space_presence;


        public Text(string path, int type)
        {
            space_presence = type;
            txt = File.ReadAllText(path, Encoding.UTF8);
        }
    }
}