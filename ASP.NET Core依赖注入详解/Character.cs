using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core依赖注入详解
{
    public class Character
    {
        public int id{ get; set; }
        public string CharList { get; set; }
        public Character(string charList)
        {
            CharList = charList;
        }
    }
}
