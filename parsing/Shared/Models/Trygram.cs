using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Trygram
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public List<TrygramValues> Values { get; set; }
    }

}
