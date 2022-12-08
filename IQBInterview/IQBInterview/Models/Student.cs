using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IQBInterview.Models
{
    public class Student : EnBaseEntity
    {
        public string FullName { get; set; }
        public int Number { get; set; }
        public string Email { get; set; }
        public string GsmNumber { get; set; }
    }
}