using System;
using System.Collections.Generic;
using System.Text;

namespace easyHospital
{
    class User
    {
        public long id { get; set; }
        public string token { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string middle_name { get; set; }

        public string birthday { get; set; }
        public string passport { get; set; }
        public string phone { get; set; }
        public string pin_code { get; set; }

        public DateTime created_at = new DateTime();

        public DateTime updated_at = new DateTime();
    }
}
