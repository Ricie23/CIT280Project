using System.Collections.Generic;
using CIT280App.Models;

namespace CIT280App.ViewModels
{
    public class EmployerJobIndex
    {

        public IEnumerable<UserModel> Users { get; set; }
        
        public IEnumerable<JobsModel> Jobs { get; set; }

    }
}