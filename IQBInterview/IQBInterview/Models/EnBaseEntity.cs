using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IQBInterview.Models
{
    public class EnBaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}