using System.ComponentModel.DataAnnotations;

namespace TechTuesdayFeedbackTool.Domain
{
    public class Users : Entity
    {
        //Users
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        //User roles
        public int RoleID { get; set; }
    }
}