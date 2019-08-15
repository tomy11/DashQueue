using System;
using System.Collections.Generic;
using System.Text;

namespace DashReport.Data.Model
{
    public class Staff
    {
        public int uid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public string status { get; set; }
        public DateTime cwhen { get; set; }

    }
}
