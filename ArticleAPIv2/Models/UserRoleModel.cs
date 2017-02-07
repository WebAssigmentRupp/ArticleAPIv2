using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArticleAPIv2.Models
{
    public class UserRoleModel
    {
        [Key]
        public short role_id { get; set; }
        public string role_name { get; set; }
        public string description { get; set; }

  
    }
}