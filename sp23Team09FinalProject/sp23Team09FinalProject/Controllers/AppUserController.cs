using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using sp23Team09FinalProject.DAL;
using sp23Team09FinalProject.Models;

namespace sp23Team09FinalProject
{
    public class AppUserController : Controller
    {
        private readonly AppDbContext _context;

        public AppUserController(AppDbContext context)
        {
            _context = context;
        }

        //GET: AppUser


        public SelectList GetAllCompanySelectList()
        {
            //Get the list of genres from the database
            List<Company> CompanyList = _context.Companys.ToList();

            SelectList CompanySelectList = new SelectList(CompanyList.OrderBy(b => b.CompanyID), "CompanyID", "CompanyName");

            //return the SelectList
            return CompanySelectList;
        }

        public SelectList DetailedMajorSelectList()
        {
            //Get the list of genres from the database
            List<Major> MajorList = _context.Majors.ToList();
            Major SelectNone = new Major() { MajorID = 0, MajorName = "All Majors" };
            MajorList.Add(SelectNone);

            SelectList MajorSelectList = new SelectList(MajorList.OrderBy(b => b.MajorID), "MajorID", "MajorName");

            //return the SelectList
            

            return MajorSelectList;
        }

        public SelectList AllMajorSelectList()
        {
            //Get the list of genres from the database
            List<Major> MajorList = _context.Majors.ToList();

            SelectList MajorSelectList = new SelectList(MajorList.OrderBy(b => b.MajorID), "MajorID", "MajorName");

            //return the SelectList


            return MajorSelectList;
        }

        public async Task<IActionResult> Index()
        {
            List<AppUser> appUsers;
           
            appUsers = _context.Users
                                .Where(r => r.UserName == User.Identity.Name)
                                .Include(m=>m.Major)
                                .Include(m => m.Company)
                                .ToList();
            
            
            return View(appUsers);
        }
        [Authorize(Roles = "CSO, Recruiter")]
        public async Task<IActionResult> IndexStudent()
        {
            List<AppUser> appUsers;
                appUsers = _context.Users
                                .Where(r => r.Company.CompanyName == "N/A")
                                .Where(r => r.Major.MajorName != "N/A")
                                .Include(m => m.Major)
                                .Include(m => m.Company)
                                .ToList();
            if (User.IsInRole("CSO"))
            {
                List<AppUser> appUserscso;
                appUserscso = _context.Users
                                .Include(m => m.Major)
                                .Include(m => m.Company)
                                .ToList();
                return View(appUserscso);
            }
            return View(appUsers);
            

        }



        // GET: AppUser/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: AppUser/Create
        public IActionResult Create()
        {
            ViewBag.AllCompanys = GetAllCompanySelectList();
            ViewBag.DetailedAllMajors = DetailedMajorSelectList();
            ViewBag.AllMajors = AllMajorSelectList();
            return View();
        }


        // POST: AppUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CSO")]
        public async Task<IActionResult> Create(AppUser appuser, int SelectedCompanyID, int SelectedMajorID)
           {
        if (ModelState.IsValid == false)
        {

            ViewBag.AllCompanys = GetAllCompanySelectList();
            ViewBag.AllMajors = DetailedMajorSelectList();
            return View(appuser);
        }

        appuser.Company = _context.Companys.Find(SelectedCompanyID);
        appuser.Major = _context.Majors.Find(SelectedMajorID);
        _context.Add(appuser);

        await _context.SaveChangesAsync();


        return RedirectToAction(nameof(Index));
        }

        // GET: AppUser/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            AppUser appUser = await _context.Users.Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }
            ViewBag.AllMajors = AllMajorSelectList();
            ViewBag.AllCompanys = GetAllCompanySelectList();
            return View(appUser);
        }

        // POST: AppUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AppUser appUser, int SelectedCompanyID, int SelectedMajorID)
        {
            //this is a security check to see if the user is trying to modify
            //a different record.  Show an error message
            

            //if code gets this far, attempt to edit the course
            try
            {
                //Find the course to edit in the database and include relevant 
                //navigational properties
                AppUser dbUser = _context.Users
                    .Include(c => c.Company)
                    .Include(c => c.Major)
                    .FirstOrDefault(c => c.Id == appUser.Id);

                AppUser test = _context.Users
                    .Include(c => c.Company)
                    .Include(c => c.Major)
                    .FirstOrDefault(c => c.Id == appUser.Id);

                //update the course's scalar properties
                dbUser.FirstName = appUser.FirstName;
                dbUser.LastName = appUser.LastName;
                dbUser.MiddleInitial = appUser.MiddleInitial;
                dbUser.GPA = appUser.GPA;
                dbUser.City = appUser.City;
                dbUser.Street = appUser.Street;
                dbUser.State = appUser.State;
                dbUser.Zip = appUser.Zip;
                dbUser.PositionType = appUser.PositionType;
                dbUser.BirthDate = appUser.BirthDate;
                dbUser.GradDate = appUser.GradDate;
                dbUser.PhoneNumber = appUser.PhoneNumber;
                dbUser.Email = appUser.Email;
                dbUser.UserName = appUser.Email;

                
                if (User.IsInRole("Student"))
                {
                    dbUser.Major = _context.Majors.Find(SelectedMajorID);

                }
                if (User.IsInRole("CSO"))
                {
                    dbUser.Company = _context.Companys.Find(SelectedCompanyID);
                    dbUser.Major = _context.Majors.Find(SelectedMajorID);

                }

                if (User.IsInRole("Recruiter"))
                {
                    dbUser.Company = test.Company;

                }

                _context.Users.Update(dbUser);
                _context.SaveChanges();


            }
            catch (Exception ex)
            {
                return View("Error", new string[] { "There was an error editing this course.", ex.Message });
            }

            //if code gets this far, everything is okay
            //send the user back to the page with all the courses
            if (User.IsInRole("Recruiter") || User.IsInRole("Student"))
            {
                return RedirectToAction(nameof(Index));
            }

            else 
            {
                return RedirectToAction("IndexStudent", "AppUser");
            }
                
        }

        // GET: AppUser/Delete/5
        [Authorize(Roles = "CSO")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: AppUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CSO")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AppDbContext.Users'  is null.");
            }
            var appUser = await _context.Users.FindAsync(id);
            if (appUser != null)
            {
                _context.Users.Remove(appUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(string id)
        {
          return _context.Users.Any(e => e.Id == id);
        }

        private SelectList GetAllMajorsSelectList()
        {
            List<Major> majorList = _context.Majors.ToList();
            Major SelectNone = new Major() { MajorID = 0, MajorName = "All Majors" };

            majorList.Add(SelectNone);

            SelectList majorSelectList = new SelectList(majorList.OrderBy(m => m.MajorID), "MajorID", "MajorName");

            return majorSelectList;
        }

        private SelectList GetAllUserSelectList()
        {
            List<AppUser> userList = _context.Users.ToList();
            AppUser SelectNone = new AppUser() { UserName = "All Majors" };

            userList.Add(SelectNone);

            SelectList userSelectList = new SelectList(userList.OrderBy(m => m.Id), "UserID", "UserName");

            return userSelectList;
        }

        public IActionResult DetailedSearch()
        {
            ViewBag.DetailedAllMajors = DetailedMajorSelectList();
            ViewBag.AllUsers = GetAllUserSelectList();
            SearchViewModel svm = new SearchViewModel();

            return View(svm);
        }

        public IActionResult DisplaySearchResult(SearchViewModel svm)
        {
            var query = from s in _context.Users
                        select s;

            if (svm.SearchName != null)
            {
                query = query.Where(s => s.FirstName.Contains(svm.SearchName) || s.LastName.Contains(svm.SearchName));
            }
            if (svm.SearchMajorID != 0)
            {
                query = query.Where(m => m.Major.MajorID == svm.SearchMajorID);
            }
            if (svm.SearchDate != null)
            {
                query = query.Where(s => s.GradDate >= (svm.SearchDate));
            }
            if (svm.SearchGPA != null)
            {
                query = query.Where(s => s.GPA >= (svm.SearchGPA));
            }
            if (svm.SearchType != null)
            {
                query = query.Where(s => s.PositionType == (svm.SearchType));
            }
            List<AppUser> SelectedUser = query.Include(m => m.Major).ToList();
            //ViewBag.AllUsers = _context.Users.Count();
            //ViewBag.SelectedUser = SelectedUser.Count();
            return View("IndexStudent", SelectedUser.OrderBy(s => s.UserName));
        }

    }
}
