using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Data.Models
{
    public class ContactInformation:BaseEntity
    {

        public string Street  { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }


    }
}
