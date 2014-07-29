using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TechTuesdayFeedbackTool.Domain
{
    public class UserRolesMaster : Entity
    {
        public string RoleName { get; set; }
    }
}