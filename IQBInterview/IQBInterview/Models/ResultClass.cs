using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IQBInterview.Models
{
    public class ResultClass
    {
        public ResultClass()
        {
            ResultMessages = new List<string>();
        }

        public bool Result { get; set; }
        public List<string> ResultMessages { get; set; }

        public object ResultObject { get; set; }
    }
}