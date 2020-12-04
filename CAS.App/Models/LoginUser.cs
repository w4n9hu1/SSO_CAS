using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAS.App.Models
{
    public class LoginUser
    {
        public int UserID { get; set; }
        public string LoginName { get; set; }
        public string App { get; set; }
    }
}