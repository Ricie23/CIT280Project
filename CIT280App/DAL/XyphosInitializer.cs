using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CIT280App.Models;


namespace CIT280App.DAL
{
    public class XyphosInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<XyphosContext>
    {
        protected override void Seed(XyphosContext context)
        {
            var students = new List<StudentModel>
            {
            new StudentModel{
                ID=1,
                Role= UserRole.Student,
                FirstName="Carson",
                LastName="Alexander",
                City= "Traverse City",
                State= "Michigan",
                Description= "I am new to the college setting but I strive to be the best.",
                Email= "JoJo@mail.com",
                Phone= "231-360-0001",
                ProfilePic= "",
                Reviews= "",
                Major= "Construction",
                School= "NMC",
                YearInSchool= "Senior"
            },
            new StudentModel{
                ID=2,
                Role= UserRole.Student,
                FirstName="Willy",
                LastName="Chilly",
                City= "Grand Rapids",
                State= "Michigan",
                Description= "I live and breath college",
                Email= "chillywilly@mail.com",
                Phone= "231-360-0301",
                ProfilePic= "",
                Reviews= "",
                Major= "Software Development",
                School= "NMC",
                YearInSchool= "Freshman"
            },
            new StudentModel{
                ID=3,
                Role= UserRole.Student,
                FirstName="Henry",
                LastName="Howard",
                City= "Petosky",
                State= "Michigan",
                Description= "I love candy",
                Email= "henryhoward@mail.com",
                Phone= "231-555-0001",
                ProfilePic= "",
                Reviews= "",
                Major= "Software Development",
                School= "NMC",
                YearInSchool= "Junior"
            },
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var employers = new List<EmployerModel>
            {
                new EmployerModel{
                    ID=4,
                    Role= UserRole.Employer,
                    FirstName="Jill",
                    LastName="Benton",
                    City= "Cadilac",
                    State= "Michigan",
                    Description= "I love my Buisness",
                    Email= "jillbenton@mail.com",
                    Phone= "231-999-0101",
                    ProfilePic= "",
                    Reviews= "",
                    BuisnessName= "Puppies Plaza",
                    BuisnessType= "Pet Store"
                },
                new EmployerModel{
                    ID=5,
                    Role= UserRole.Employer,
                    FirstName="Kelro",
                    LastName="Pharmton",
                    City= "Houghton Lake",
                    State= "Michigan",
                    Description= "Buisnessman for Life",
                    Email= "kelropharmton@mail.com",
                    Phone= "231-888-0001",
                    ProfilePic= "",
                    Reviews= "",
                    BuisnessName= "IT Surplus",
                    BuisnessType= "Electronic Store"
                },
                new EmployerModel{
                    ID=6,
                    Role= UserRole.Employer,
                    FirstName="George",
                    LastName="Jameson",
                    City= "Leelanau",
                    State= "Michigan",
                    Description= "The Shop is my Treasure",
                    Email= "georgejameson@mail.com",
                    Phone= "231-111-0001",
                    ProfilePic= "",
                    Reviews= "",
                    BuisnessName= "Build Guild",
                    BuisnessType= "Construction"
                }
            };

            employers.ForEach(e => context.Employers.Add(e));
            context.SaveChanges();

            var jobs = new List<JobsModel>
            {
                new JobsModel{
                    ID=01,
                    UserID=6,
                    Name="Shingling a Roof",
                    Description="Construction workers needed to shingle a roof",
                    City="Manton",
                    State="Michigan",
                    RequiredSkills="5 months of construction experience",
                    Photo="",
                    Pay=100,
                    IsComplete=false
                },
                new JobsModel{
                    ID=02,
                    UserID=5,
                    Name="Building computers",
                    Description="Need extra help to build computers during the weekend.",
                    City="Traverse City",
                    State="Michigan",
                    RequiredSkills="Some experience with building computers",
                    Photo="",
                    Pay=50,
                    IsComplete=false
                },
                new JobsModel{
                    ID=03,
                    UserID=6,
                    Name="Paveing a driveway",
                    Description="Need help paveing a drive way this weekend",
                    City="Manistee",
                    State="Michigan",
                    RequiredSkills="5 months of construction experience",
                    Photo="",
                    Pay=100,
                    IsComplete=false
                },
            };
            jobs.ForEach(at => context.Jobs.Add(at));
            context.SaveChanges();
            var admin = new List<UserModel>
            {
                new UserModel
                {
                    ID=7,
                    Role=UserRole.Admin,
                    FirstName="Tom",
                    LastName="Farrer",
                    Description="Smells Like Coffee",
                    City="Traverse City",
                    State="MI",
                    Email="tfarrer@mail.nmc.edu", 
                },
                  new UserModel{
                      ID=8,
                    Role=UserRole.Admin,
                    FirstName="Rich",
                    LastName="Robertson",
                    Description="the quiet one",
                    City="Traverse City",
                    State="MI",
                    Email="rrobertson@mail.nmc.edu",
                },
                  new UserModel{
                    ID=9,
                    Role=UserRole.Admin,
                    FirstName="Tanner",
                    LastName="Rabb",
                    Description="likes the databases",
                    City="Traverse City",
                    State="MI",
                    Email="trabb@mail.nmc.edu",
                }
            };
            admin.ForEach(a => context.Admins.Add(a));
            context.SaveChanges();

           
        }
    }
}