using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrygramParserAPI.Models
{
    public class Trygram
    {
        public string Key { get; set; }
        public List<TrygramValues> Values { get; set; }
    }

}
