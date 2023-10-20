using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Data
{
    public class PrjSettings
    {
        public string ConnectionString { get; set; }
        public string RunMode { get; set; }
        public string JwtSecret { get; set; }

        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }

        public string ServerMailHostName { get; set; }
        public int ServerMailPort { get; set; }
        public string ServerMailUserName { get; set; }
        public string ServerMailPassword { get; set; }
        public string ServerMailAddress { get; set; }
        public bool ServerMailSsl { get; set; }
        public string AppUrl { get; set; }

        public bool IsMailServiceActive { get; set; }
        public string ActivationUser { get; set; }
        public string ActivationPassword { get; set; }


    }
}
