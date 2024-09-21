using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using sp23Team09FinalProject.DAL;
using sp23Team09FinalProject.Models;

namespace sp23Team09FinalProject.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SelectList GetAllPositionSelectList()
        {
            List<Models.Position> PositionList = _context.Positions.ToList();

            SelectList PositionSelectList = new SelectList(PositionList.OrderBy(b => b.PositionID), "PositionID", "Title");

            return PositionSelectList;
        }

        public SelectList GetAllPositionSelectList(Application apl)
        {
            List<Models.Position> PositionList = _context.Positions.Include(r => r.Majors).Include(r => r.CurrDate).ToList();


            if (User.IsInRole("Student"))
            {
                SelectList PositionSelectList2 = new SelectList(PositionList.Where(r => r.PositionType == apl.Student.PositionType)
                    .Where(r => r.Majors.Contains(apl.Student.Major))
                    .Where(r=> r.Deadline>r.CurrDate.CurrDate)
                    .OrderBy(b => b.PositionID), "PositionID", "Title");

                if (PositionSelectList2==null)
                {
                    return null;
                }

                return PositionSelectList2;
            }

            

            SelectList PositionSelectList = new SelectList(PositionList.OrderBy(b => b.PositionID), "PositionID", "Title");

            return PositionSelectList;
        }

        public SelectList GetAllStudentsSelectList()
        {
            //Get the list of genres from the database
            List<Models.AppUser> AppUserList = _context.Users.Include(r => r.Company).ToList();

            SelectList StudentSelectList = new SelectList(AppUserList.OrderBy(b => b.Id), "Id", "FirstName");


            //return the SelectList
            return StudentSelectList;
        }
        public SelectList GetAllStudentsSelectList(Application apl)
        {
            //Get the list of genres from the database
            List<Models.AppUser> AppUserList = _context.Users.Include(r => r.Company).ToList();

            

            SelectList StudentSelectList = new SelectList(AppUserList.OrderBy(b => b.Id), "Id", "FirstName");



            //return the SelectList
            return StudentSelectList;
        }


        public ApplicationsController(AppDbContext context, UserManager<AppUser> userManger)
        {
            _context = context;
            _userManager = userManger;
        }

    
        public async Task<IActionResult> Index()
        {
            //Set up a list of registrations to display
            List<Application> applications;
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            applications = _context.Applications
                                .Include(r => r.Position)
                                .Include(r => r.Position.Company)
                                .Include(r => r.Position.Interviewer)
                                .Include(r => r.Student)
                                .ToList();

            if (User.IsInRole("Recruiter"))
            {
                applications = _context.Applications
                                .Where(r => r.Position.Company == appUser.Company)
                                .Where(r => r.CurrDate.CurrDate > r.Position.Deadline)
                                .Include(r => r.Position)
                                .Include(r => r.Position.Company)
                                .Include(r => r.Position.Interviewer)
                                .Include(r => r.Student)
                                .ToList();
            }

            if (User.IsInRole("Student"))
             //user is a customer, so only display their records
            {
                applications = _context.Applications
                                .Where(r => r.Student.UserName == User.Identity.Name)
                                .Include(r => r.Position)
                                .Include(r => r.Position.Interviewer)
                                .Include(r => r.Position.Company)
                                .Include(r => r.Student)
                                .ToList();
            }

            //
            return View(applications);
        }
        public async Task<IActionResult> Interviews()
        {
            //Set up a list of registrations to display
            List<Application> applications;
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            applications = _context.Applications
                                .Include(r => r.Position)
                                .Include(r => r.Position.Company)
                                .Include(r => r.Position.Interviewer)
                                .Include(r => r.Student)
                                .ToList();
            if (User.IsInRole("Recruiter"))
            {
                applications = _context.Applications
                            .Where(r => r.Position.Company == appUser.Company)
                            .Include(r => r.Position)
                            .Include(r => r.Position.Interviewer)
                            .Include(r => r.Position.Company)
                            .Include(r => r.Student)
                            .ToList();
            }
            if (User.IsInRole("Student"))
            //user is a customer, so only display their records
            {
                applications = _context.Applications
                                .Where(r => r.Student.UserName == User.Identity.Name)
                                .Include(r => r.Position)
                                .Include(r => r.Position.Interviewer)
                                .Include(r => r.Position.Company)
                                .Include(r => r.Student)
                                .ToList();
            }

            //
            return View(applications);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //the user did not specify a registration to view
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a application to view!" });
            }

            //find the registration in the database
            Application application = await _context.Applications
                                              .Include(r => r.Position)
                                              .Include(r => r.Position.Company)
                                              .Include(r => r.Student)
                                              .FirstOrDefaultAsync(m => m.ApplicationID == id);

            //registration was not found in the database
            if (application == null)
            {
                return View("Error", new String[] { "This application was not found!" });
            }

            //make sure this registration belongs to this user
            if (User.IsInRole("Student") && application.Student.UserName != User.Identity.Name)
            {
                return View("Error", new String[] { "This is not your application!  Don't be such a snoop!" });
            }

            //Send the user to the details page
            return View(application);
        }

        // GET: Orders/Create
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            Application apl = new Application();
            apl.Student = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.AllPositions = GetAllPositionSelectList();
            ViewBag.AllStudents = GetAllStudentsSelectList();
            var test= GetAllPositionSelectList(apl);

            if (User.IsInRole("Student"))
            {
                ViewBag.AllPositions = GetAllPositionSelectList(apl);
            }
            if (User.IsInRole("Recruiter"))
            {
                ViewBag.AllStudents = GetAllStudentsSelectList(apl);

                AppUser appuser = await _userManager.FindByNameAsync(User.Identity.Name);

                List<Models.Position> PositionList = _context.Positions.Include(r => r.Company).Include(r => r.CurrDate).ToList();

                SelectList PositionSelectListcompany = new SelectList(PositionList
                        .Where(r => r.Company == appuser.Company)
                        .OrderBy(b => b.PositionID), "PositionID", "Title");

                ViewBag.AllPositions = PositionSelectListcompany;
            }

            if (test.Count() == 0)
            {
                return View("Error", new string[] { "You have no positons to apply for at this time" });
            }
            

            return View(apl);

        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Application application, int SelectedPositionID, string SelectedStudentID)
        {
            List<Application> ApplicationList = _context.Applications.Include(r => r.Student).Include(r => r.Position).ToList();

            

            application.Position = _context.Positions.Find(SelectedPositionID);
            application.Student = await _userManager.FindByNameAsync(User.Identity.Name);

            foreach (Application app in ApplicationList)
            {
                if ((app.Position==application.Position) && (app.Student == application.Student) && (app.ApplicationID!=application.ApplicationID))
                {
                    return View("Error", new string[] { "You already applied to this position" });
                }
            }


                if (User.IsInRole("CSO") || User.IsInRole("Recruiter"))
            {
                application.Student = _context.Users.Find(SelectedStudentID);
            }

            Application applicationdate = await _context.Applications
                                          .Include(r => r.CurrDate)
                                          .Include(r => r.CurrDate)
                                          .Include(r => r.Position)
                                          .Include(r => r.Position.Company)
                                          .Include(r => r.Student)
                                          .FirstOrDefaultAsync(m => m.ApplicationID == 50);

            application.SubmissionDate = applicationdate.CurrDate.CurrDate;

            //Associate the registration with the logged-in customer
            if (User.IsInRole("Student"))
            {
                application.Student = await _userManager.FindByNameAsync(User.Identity.Name);
            }
           
            application.Position = _context.Positions.Find(SelectedPositionID);
            application.CurrDate = _context.CurrDate.Find(1);
            application.Room = Rooms.NoRoom;
            application.Date = applicationdate.CurrDate.CurrDate;
            //if code gets this far, add the registration to the database
            _context.Add(application);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Applications");
        }

        // GET: Orders/Edit/5
        public IActionResult Edit(int? id)
        {

            //user did not specify a registration to edit
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a application to edit" });
            }

            //find the registration in the database, and be sure to include details
            Application application = _context.Applications
                                       .Include(r => r.Position)
                                       .Include(r=>r.CurrDate)
                                       .Include(r => r.Position.Company)
                                       .Include(r => r.Student)
                                       .FirstOrDefault(r => r.ApplicationID == id);

            //registration was nout found in the database
            if (application == null)
            {
                return View("Error", new String[] { "This application was not found in the database!" });
            }

            //registration does not belong to this user
            if (application.Date!=null)
            {

                TimeSpan timeDiffapp = ((TimeSpan)(application.Date - application.CurrDate.CurrDate));
                if (User.IsInRole("Student") && timeDiffapp.TotalHours <= 24 && timeDiffapp.TotalHours > 0)
                {
                    return View("Error", new String[] { "You cannot edit your interview time, 24 hours before it!" });
                }
            }

            if (User.IsInRole("Student") && application.Status != Status.AcceptedforInterview)
            {
                return View("Error", new String[] { "You are not authorized to schedule this application!" });
            }


            ViewBag.AllPositions = GetAllPositionSelectList(application);
            return View(application);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Application application, int SelectedAppID)
        {
            //this is a security check to see if the user is trying to modify
            //a different record.  Show an error message
            
            if (ModelState.IsValid == false) //there is something wrong
            {
                ViewBag.AllPositions = GetAllPositionSelectList(application);
                return View(application);
            }
            Application applicationtest = _context.Applications
                                       .Include(r => r.Position)
                                       .Include(r => r.CurrDate)
                                       .Include(r => r.Position.Company)
                                       .Include(r => r.Student)
                                       .FirstOrDefault(r => r.ApplicationID == id);

            
            if ((User.IsInRole("Recruiter") || User.IsInRole("CSO")) && (applicationtest.Position.Deadline > applicationtest.CurrDate.CurrDate) && application.Status!=Status.Active)
            {
                return View("Error", new String[] { "You cannot accept students for interviews before the position deadline" });
            }
            

            //if code gets this far, attempt to edit the course
            try
            {
                //Find the course to edit in the database and include relevant 
                //navigational properties
                Application dbApplication = _context.Applications
                                       .Include(r => r.Position)
                                       .Include(r => r.CurrDate)
                                       .Include(r => r.Position.Company)
                                       .Include(r => r.Student)
                                       .FirstOrDefault(r => r.ApplicationID == id);

                List<Application> ApplicationList = _context.Applications.ToList();

                dbApplication.Status = application.Status;
                dbApplication.CurrDate = _context.CurrDate.Find(1);


                if (User.IsInRole("Recruiter") && application.Status==Status.AcceptedforInterview)
                {
                    AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    dbApplication.Position.Interviewer = (appUser);
                    
                }

                    if (User.IsInRole("Student"))
                {
                    dbApplication.Status = Status.AcceptedforInterview;

                }

                
                

                if (User.IsInRole("Student"))
                {
                    application.Student = await _userManager.FindByNameAsync(User.Identity.Name);
                    application.Room = Rooms.A;
                    foreach (Application app in ApplicationList.Where(r=>r.Date!=null))
                    {
                        TimeSpan timeDiff = ((TimeSpan)(app.Date - application.Date));
                        if (timeDiff.Duration() < TimeSpan.FromHours(1) && app.Student==application.Student && app.ApplicationID != id)
                        {
                            return View("Error", new string[] { "You already have an interview scheduled for this time" });
                        }

                        if (timeDiff.Duration() < TimeSpan.FromHours(1) && app.Student != application.Student)

                        {
                            if (app.Room == Rooms.A)
                            {
                                application.Room = Rooms.B;
                            }
                            if (app.Room == Rooms.B && application.Room != Rooms.A)
                            {
                                application.Room = Rooms.C;
                            }
                            if (app.Room == Rooms.C && application.Room != Rooms.A && application.Room != Rooms.B)
                            {
                                application.Room = Rooms.D;
                            }
                            if (app.Room == Rooms.D && application.Room != Rooms.A && application.Room != Rooms.B && application.Room != Rooms.C )
                            {
                                return View("Error", new string[] { "No interviews are avalible at that time, please try again." });
                            }


                        }

                    }

                    dbApplication.Room = application.Room;
                }

                
                
                TimeSpan timeDiffdate = ((TimeSpan)(application.Date - applicationtest.Position.Deadline));
                if (timeDiffdate.Duration()<TimeSpan.FromHours(48))
                {
                    return View("Error", new string[] { "You can only book an  interview at least 48 hours after the deadline" });
                }
                dbApplication.Date = application.Date;
                

                if (User.IsInRole("Recruiter"))
                {
                    dbApplication.Room = Rooms.NoRoom;
                }
                if (User.IsInRole("CSO"))
                {
                    dbApplication.Room = application.Room;
                }

                _context.Applications.Update(dbApplication);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return View("Error", new string[] { "There was an error editing this application.", ex.Message });
            }

            Application appemail = _context.Applications
                                       .Include(r => r.Position)
                                       .Include(r => r.CurrDate)
                                       .Include(r => r.Position.Company)
                                       .Include(r => r.Position.Interviewer)
                                       .Include(r => r.Student)
                                       .FirstOrDefault(r => r.ApplicationID == id);

            if (application.Status == Status.AcceptedforInterview)
            {
                try
                {
                    String emailBody = "Hello!\n\nThank you for your application! You have been invited to interview! We will update you regarding interview details soon!";
                    Utilities.EmailMessaging.SendEmail("MIS333K Final Project - Invited to Interview", emailBody);
                }
                catch (Exception ex)
                {
                    return View("Error", new String[] { "There was a problem sending the email", ex.Message });
                }

               
            }


            if (application.Status == Status.AcceptedforInterview)
            {
                try
                {
                    String emailBody = "Hello!\n\nThank you for your application!\n\nDate: " + appemail.Date + "\n\nRoom: " + appemail.Room + "\n\nPosition: " + appemail.Position.Title + "\n\nInterviewer: " + appemail.Position.Interviewer.FirstName;
                    Utilities.EmailMessaging.SendEmail("MIS333K Final Project - Interview Confirmation", emailBody);
                }
                catch (Exception ex)
                {
                    return View("Error", new String[] { "There was a problem sending the email", ex.Message });
                }


                try
                {
                    String emailBody = "Hello " + appemail.Position.Interviewer.FirstName + "! \n\nYou are scheduled to interview a candidate" + "\n\nDate: " + appemail.Date + "\n\nRoom: " + appemail.Room + "\n\nPosition: " + appemail.Position.Title + "\n\nStudent: " + appemail.Student.FirstName;
                    Utilities.EmailMessaging.SendEmail("MIS333K Final Project - Interview Confirmation for Recruiter", emailBody);
                }
                catch (Exception ex)
                {
                    return View("Error", new String[] { "There was a problem sending the email", ex.Message });
                }

            }

            if (application.Status == Status.Declined)
            {
                try
                {
                    String emailBody = "Hello!\n\nThank you for your application! We regret to inform you that we will not be continuing with your application. Thank you.";
                    Utilities.EmailMessaging.SendEmail("MIS333K Final Project - Decline to Interview", emailBody);
                }
                catch (Exception ex)
                {
                    return View("Error", new String[] { "There was a problem sending the email", ex.Message });
                }

            }
            if (application.Status == Status.AcceptedforPosition)
            {
                try
                {
                    String emailBody = "Hello!\n\nCongrats you have been accepted for the " + appemail.Position.Title + " position.";
                    Utilities.EmailMessaging.SendEmail("MIS333K Final Project - Acceptance Email", emailBody);
                }
                catch (Exception ex)
                {
                    return View("Error", new String[] { "There was a problem sending the email", ex.Message });
                }
            }

            if (User.IsInRole("Student"))
            {
                try
                {
                    String emailBody = "Hello!\n\nThank you for your application!\n\nDate: " + appemail.Date + "\n\nRoom: " + appemail.Room + "\n\nPosition: " + appemail.Position.Title + "\n\nInterviewer: " + appemail.Position.Interviewer.FirstName;
                    Utilities.EmailMessaging.SendEmail("MIS333K Final Project - Interview Confirmation", emailBody);
                }
                catch (Exception ex)
                {
                    return View("Error", new String[] { "There was a problem sending the email", ex.Message });
                }


                try
                {
                    String emailBody = "Hello " + appemail.Position.Interviewer.FirstName + "! \n\nYou are scheduled to interview a candidate" + "\n\nDate: " + appemail.Date + "\n\nRoom: " + appemail.Room + "\n\nPosition: " + appemail.Position.Title + "\n\nStudent: " + appemail.Student.FirstName;
                    Utilities.EmailMessaging.SendEmail("MIS333K Final Project - Interview Confirmation for Recruiter", emailBody);
                }
                catch (Exception ex)
                {
                    return View("Error", new String[] { "There was a problem sending the email", ex.Message });
                }
            }

            //if code gets this far, everything is okay
            //send the user back to the page with all the courses
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }
            var application = await _context.Applications
                .Include(r => r.Position)
                .Include(r => r.CurrDate)
                .Include(r => r.Position.Company)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.ApplicationID == id);

            if (application.Position.Deadline<application.CurrDate.CurrDate)
            {
                return View("Error", new String[] { "You cannot withdraw after the deadline" });
            }

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Position/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Applications == null)
            {
                return Problem("Entity set 'AppDbContext.Application'  is null.");
            }
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return (_context.Applications?.Any(e => e.ApplicationID == id)).GetValueOrDefault();
        }
    }
}
