using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Data.Models
{
    public class Persons : BaseEntity
    {
        public string TCNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
    }
}
