using System;
using System.Collections.Generic;
using System.Text;

namespace easyHospital
{
    public enum Status
    {
        admin,
        creator
    }
    class Admin
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string middle_name { get; set; }
        public string phone { get; set; }
        public Status status { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}
