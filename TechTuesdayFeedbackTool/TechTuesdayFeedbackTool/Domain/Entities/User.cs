using System.ComponentModel.DataAnnotations.Schema;

namespace TechTuesdayFeedbackTool.Domain
{
    public class User : Entity
    {
        //Users
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public int RoleID { get; set; }
        //User roles
        [ForeignKey("RoleID")]
        public UserRolesMaster Role { get; set; }        
    }
}