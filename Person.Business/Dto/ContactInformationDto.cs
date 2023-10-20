using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Business.Dto
{
    public class ContactInformationDto
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
