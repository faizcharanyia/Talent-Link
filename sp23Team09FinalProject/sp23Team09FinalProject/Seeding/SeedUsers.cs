using Microsoft.AspNetCore.Identity;


//TODO: Update these using statements to include your project name
using sp23Team09FinalProject.Models;
using sp23Team09FinalProject.Utilities;
using sp23Team09FinalProject.DAL;

//TODO: Upddate this namespace to match your project name
namespace sp23Team09FinalProject.Seeding
{
    public static class SeedUsers
    {
        public async static Task<IdentityResult> SeedAllUsers(UserManager<AppUser> userManager, AppDbContext context)
        {
            //Create a list of AddUserModels
            List<AddUserModel> AllUsers = new List<AddUserModel>();

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    PhoneNumber = "(512)555-1234",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Admin",
                    LastName = "Last",
                    Id="1"

                },
                Password = "Abc123!",
                RoleName = "CSO"
                
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "bevo@example.com",
                    Email = "bevo@example.com",
                    PhoneNumber = "(512)555-5555",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Bevo",
                    LastName = "Last",
                    Id = "2"

                },
                Password = "Password123!",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "recruiter@example.com",
                    Email = "recruiter@example.com",
                    PhoneNumber = "(512)111-1111",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Recruiter",
                    LastName = "Last",
                    Id="3"

                },
                Password = "Password123!",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "cbaker@example.com",
                    Email = "cbaker@example.com",

                    FirstName = "Christopher",
                    LastName = "Baker",
                    Id = "4",

                },
                Password = "bookworm!",
                RoleName = "Student"

            });
            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "limchou@gogle.com",
                    Email = "limchou@gogle.com",

                    FirstName = "Lim",
                    LastName = "Chou",
                    Id = "5",

                },
                Password = "allyrally",
                RoleName = "Student"
            });
            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "j.b.evans@aheca.org",
                    Email = "j.b.evans@aheca.org",

                    FirstName = "Jim Bob",
                    LastName = "Evans",
                    Id = "6",

                },
                Password = "billyboy",
                RoleName = "Student"
            });
            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "tfreeley@minnetonka.ci.us",
                    Email = "tfreeley@minnetonka.ci.us",

                    FirstName = "Tesa",
                    LastName = "Freeley",
                    Id = "7",

                },
                Password = "dustydusty",
                RoleName = "Student"
            });
            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "ingram@jack.com",
                    Email = "ingram@jack.com",

                    FirstName = "Brad",
                    LastName = "Ingram",
                    Id = "8",

                },
                Password = "joejoejoe",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "cluce@gogle.com",
                    Email = "cluce@gogle.com",

                    FirstName = "Chuck",
                    LastName = "Luce",
                    Id = "9",

                },
                Password = "meganr34",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "erynrice@aoll.com",
                    Email = "erynrice@aoll.com",

                    FirstName = "Eryn",
                    LastName = "Rice",
                    Id = "10",

                },
                Password = "radgirl",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "saunders@pen.com",
                    Email = "saunders@pen.com",

                    FirstName = "Sarah",
                    LastName = "Saunders",
                    Id = "11",

                },
                Password = "slowwind",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "estuart@anchor.net",
                    Email = "estuart@anchor.net",

                    FirstName = "Eric",
                    LastName = "Stuart",
                    Id = "12",

                },
                Password = "stewball",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "rwood@voyager.net",
                    Email = "rwood@voyager.net",

                    FirstName = "Reagan",
                    LastName = "Wood",
                    Id = "13",

                },
                Password = "xcellent",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "elowe@netscrape.net",
                    Email = "elowe@netscrape.net",

                    FirstName = "Ernest",
                    LastName = "Lowe",
                    Id = "14",

                },
                Password = "v3n5AV",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "mclarence@aool.com",
                    Email = "mclarence@aool.com",

                    FirstName = "Clarence",
                    LastName = "Martin",
                    Id = "15",

                },
                Password = "zBLq3S",
                RoleName = "Recruiter"
            });
            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "nelson.Kelly@aool.com",
                    Email = "nelson.Kelly@aool.com",

                    FirstName = "Kelly",
                    LastName = "Nelson",
                    Id = "16",

                },
                Password = "FSb8rA",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "sheff44@ggmail.com",
                    Email = "sheff44@ggmail.com",

                    FirstName = "Martin",
                    LastName = "Sheffield",
                    Id = "17",

                },
                Password = "4XKLsd",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "yhuik9.Taylor@aool.com",
                    Email = "yhuik9.Taylor@aool.com",

                    FirstName = "Rachel",
                    LastName = "Taylor",
                    Id = "18",

                },
                Password = "9yhFS3",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "smartinmartin.Martin@aool.com",
                    Email = "smartinmartin.Martin@aool.com",

                    FirstName = "Gregory",
                    LastName = "Martinez",
                    Id = "19",

                },
                Password = "1rKkMW",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "tanner@ggmail.com",
                    Email = "tanner@ggmail.com",

                    FirstName = "Jeremy",
                    LastName = "Tanner",
                    Id = "20",

                },
                Password = "w9wPff",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "ra@aoo.com",
                    Email = "ra@aoo.com",

                    FirstName = "Allen",
                    LastName = "Rogers",
                    Id = "21",

                },
                Password = "3wCynC",
                RoleName = "CSO"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "captain@enterprise.net",
                    Email = "captain@enterprise.net",

                    FirstName = "Jean Luc",
                    LastName = "Picard",
                    Id = "22",

                },
                Password = "Pbon0r",
                RoleName = "CSO"
            });



            //create flag to help with errors
            String errorFlag = "Start";

            //create an identity result
            IdentityResult result = new IdentityResult();
            //call the method to seed the user
            try
            {
                foreach (AddUserModel aum in AllUsers)
                {
                    errorFlag = aum.User.Email;
                    result = await Utilities.AddUser.AddUserWithRoleAsync(aum, userManager, context);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem adding the user with email: " 
                    + errorFlag, ex);
            }

            return result;
        }
    }
}
