using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core依赖注入详解.Repository
{
    public interface ICharacterRepository
    {
        IEnumerable<Character> ListAll();
        int Add(Character character);
    }
}
