using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIT280App.Models
{
    public enum UserRole 
    {
        Admin, Student, Employer
    }
    public class UserModel
    {
        
        public int ID { get; set; }
        public UserRole Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }        
        public string ProfilePic { get; set; }
        public string Reviews { get; set; }

        public virtual ICollection<JobsModel> Jobs { get; set; }
    }
}