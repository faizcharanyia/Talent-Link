using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sp23Team09FinalProject.DAL;
using sp23Team09FinalProject.Models;

namespace sp23Team09FinalProject.Controllers
{
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PositionController(AppDbContext context, UserManager<AppUser> userManger)
        {
            _context = context;
            _userManager = userManger;
        }

        public SelectList GetAllCompanySelectList()
        {
            //Get the list of genres from the database
            List<Company> CompanyList = _context.Companys.ToList();

            SelectList CompanySelectList = new SelectList(CompanyList.OrderBy(b => b.CompanyID), "CompanyID", "CompanyName");

            //return the SelectList
            return CompanySelectList;
        }

        public SelectList GetAllCompanySelectList2()
        {
            //Get the list of genres from the database
            List<Company> CompanyList = _context.Companys.ToList();

            SelectList CompanySelectList = new SelectList(CompanyList.OrderBy(b => b.CompanyID), "CompanyID", "CompanyName");

            //return the SelectList
            return CompanySelectList;
        }

        public SelectList GetAllMajorSelectList()
        {
            //Get the list of genres from the database
            List<Major> MajorList = _context.Majors.ToList();

            SelectList MajorSelectList = new SelectList(MajorList.OrderBy(s => s.MajorID), "MajorID", "MajorName");

            //return the SelectList
            return MajorSelectList;
        }
        private MultiSelectList GetAllMajorSelectList(Position position)
        {
            //Create a new list of departments and get the list of the departments
            //from the database
            List<Major> MajorList = _context.Majors.ToList();

            //loop through the list of course departments to find a list of department ids
            //create a list to store the department ids
            List<Int32> selectedMajorIDs = new List<Int32>();

            //Loop through the list to find the DepartmentIDs
            foreach (Major associatedMajor in position.Majors)
            {
                selectedMajorIDs.Add(associatedMajor.MajorID);
            }

            //use the MultiSelectList constructor method to get a new MultiSelectList
            MultiSelectList mslAllMajors = new MultiSelectList(MajorList.OrderBy(d => d.MajorName), "MajorID", "MajorName", selectedMajorIDs);

            //return the MultiSelectList
            return mslAllMajors;
        }


        // GET: Position
        public async Task<IActionResult> Index(String SearchString)
        {

            var query = from p in _context.Positions
                        select p;

            List<Position> SelectedPositions = query
                .Include(c => c.CurrDate).Include(c => c.Company).Include(c => c.Majors).Include(c => c.Company.Industrys).ToList();

            
            if (SearchString == null || SearchString == "")
            {
                if (User.IsInRole("Recruiter"))
                {
                    AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    SelectedPositions = query
                                    //.Where(r => r.CurrDate.CurrDate <= r.Deadline)
                                    .Where(r => r.Company == appUser.Company)
                                    .Include(c => c.Company)
                                    .Include(c => c.Majors)
                                    .Include(c => c.Company.Industrys)
                                    .ToList();
                }
                if (User.IsInRole("Student"))
                {
                    AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    SelectedPositions = query
                                    .Where(r => r.CurrDate.CurrDate <= r.Deadline )
                                    .Include(c => c.Company)
                                    .Include(c => c.Majors)
                                    .Include(c => c.Company.Industrys)
                                    .ToList();
                }
                if (User.IsInRole("CSO"))
                {

                    SelectedPositions = query
                                    .Include(c => c.Company)
                                    .Include(c => c.Majors)
                                    .Include(c => c.Company.Industrys)
                                    .ToList();
                }

                ViewBag.AllPositions = _context.Positions.Count();
                //Populate the view bag with a count of selected books
                ViewBag.SelectedPositions = SelectedPositions.Count;

                return View(SelectedPositions.OrderBy(s => s.Title)); 
            }
            else
            {

                var query2 = from p in _context.Positions
                            select p;

                query2 = query2.Where(b => b.Title.Contains(SearchString) ||
                b.Description.Contains(SearchString));

                List<Position> SelectedPositionsnon = query2.Include(c => c.Company).Include(c => c.Majors).Include(c => c.Company.Industrys).ToList();

                if (User.IsInRole("Recruiter"))
                {
                    AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    SelectedPositionsnon = query2
                                    //.Where(r => r.CurrDate.CurrDate <= r.Deadline)
                                    .Where(r => r.Company == appUser.Company)
                                    .Include(c => c.Company)
                                    .Include(c => c.Majors)
                                    .Include(c => c.Company.Industrys)
                                    .ToList();
                }
                if (User.IsInRole("Student"))
                {
                    AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    SelectedPositionsnon = query2
                                    .Where(r => r.CurrDate.CurrDate <= r.Deadline)
                                    .Include(c => c.Company)
                                    .Include(c => c.Majors)
                                    .Include(c => c.Company.Industrys)
                                    .ToList();
                }
                if (User.IsInRole("CSO"))
                {

                    SelectedPositionsnon = query2
                                    .Include(c => c.Company)
                                    .Include(c => c.Majors)
                                    .Include(c => c.Company.Industrys)
                                    .ToList();
                }

                ViewBag.AllPositions = _context.Positions.Count();
                //Populate the view bag with a count of selected books
                ViewBag.SelectedPositionsnon = SelectedPositionsnon.Count;

                return View(SelectedPositionsnon.OrderBy(s => s.Title));
            }
            

           }

        // GET: Position/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            Position position = await _context.Positions.Include(c => c.Company).Include(c => c.Majors).Include(c => c.Company.Industrys)
                .FirstOrDefaultAsync(m => m.PositionID == id);
            

            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // GET: Position/Create
        [Authorize(Roles = "CSO,Recruiter")]
        public IActionResult Create()
        {
            ViewBag.AllCompanys = GetAllCompanySelectList();
            ViewBag.AllMajors = GetAllMajorSelectList();
            return View();
        }

        // POST: Position/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "CSO,Recruiter")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Position position, int SelectedCompanyID, int[] SelectedMajors)
        {

               
                ViewBag.AllCompanys = GetAllCompanySelectList();
                ViewBag.AllMajors = GetAllMajorSelectList();


            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            position.Company = appUser.Company;
            if (User.IsInRole("CSO"))
            {
                position.Company = _context.Companys.Find(SelectedCompanyID);
            }
            _context.Add(position);
            await _context.SaveChangesAsync();

            //add the associated departments to the course
            //loop through the list of deparment ids selected by the user
            foreach (int majorID in SelectedMajors)
            {
                //find the department associated with that id
                Major dbMajor = _context.Majors.Find(majorID);

                //add the department to the course's list of departments and save changes
                position.Majors.Add(dbMajor);
                position.CurrDate = _context.CurrDate.Find(1);
                _context.SaveChanges();
            }
            
            //Send the user to the page with all the departments
            return RedirectToAction(nameof(Index));
        }

        // GET: Position/Edit/5
        [Authorize(Roles = "CSO,Recruiter")]
        public async Task<IActionResult> Edit(int? id)
        {
            //if the user didn't specify a course id, we can't show them 
            //the data, so show an error instead
            if (id == null)
            {
                return View("Error", new string[] { "Please specify a course to edit!" });
            }

            //find the course in the database
            //be sure to change the data type to course instead of 'var'
            Position position = await _context.Positions.Include(c => c.Company).Include(c => c.Majors)
                .FirstOrDefaultAsync(m => m.PositionID == id);

            //if the course does not exist in the database, then show the user
            //an error message
            if (position == null)
            {
                return View("Error", new string[] { "This course was not found!" });
            }

            //populate the viewbag with existing departments
            ViewBag.AllCompanys = GetAllCompanySelectList();
            ViewBag.AllMajors = GetAllMajorSelectList(position);
            return View(position);
        }

        // POST: Position/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "CSO,Recruiter")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Position position, int SelectedCompanyID, int[] SelectedMajors)
        {
            //this is a security check to see if the user is trying to modify
            //a different record.  Show an error message
            if (id != position.PositionID)
            {
                return View("Error", new string[] { "Please try again!" });
            }

            if (ModelState.IsValid == false) //there is something wrong
            {
                ViewBag.AllCompanys = GetAllCompanySelectList();
                ViewBag.AllMajors = GetAllMajorSelectList(position);
                return View(position);
            }

            //if code gets this far, attempt to edit the course
            try
            {
                //Find the course to edit in the database and include relevant 
                //navigational properties
                Position dbPosition = _context.Positions
                    .Include(c => c.Company)
                    .Include(c => c.Majors)
                    .FirstOrDefault(c => c.PositionID == position.PositionID);

                //create a list of departments that need to be removed
                List<Major> MajorsToRemove = new List<Major>();

                //find the departments that should no longer be selected because the
                //user removed them
                //remember, SelectedDepartments = the list from the HTTP request (listbox)
                foreach (Major major in dbPosition.Majors)
                {
                    //see if the new list contains the department id from the old list
                    if (SelectedMajors.Contains(major.MajorID) == false)//this department is not on the new list
                    {
                        MajorsToRemove.Add(major);
                    }
                }

                //remove the departments you found in the list above
                //this has to be 2 separate steps because you can't iterate (loop)
                //over a list that you are removing things from
                foreach (Major major in MajorsToRemove)
                {
                    //remove this course department from the course's list of departments
                    dbPosition.Majors.Remove(major);
                    _context.SaveChanges();
                }

                //add the departments that aren't already there
                foreach (int majorID in SelectedMajors)
                {
                    if (dbPosition.Majors.Any(d => d.MajorID == majorID) == false)//this department is NOT already associated with this course
                    {
                        //Find the associated department in the database
                        Major dbMajor = _context.Majors.Find(majorID);

                        //Add the department to the course's list of departments
                        dbPosition.Majors.Add(dbMajor);
                        _context.SaveChanges();
                    }
                }

                //update the course's scalar properties
                dbPosition.Title = position.Title;
                dbPosition.Description = position.Description;
                dbPosition.PositionType = position.PositionType;
                dbPosition.Location = position.Location;
                dbPosition.Deadline = position.Deadline;
                dbPosition.CurrDate = _context.CurrDate.Find(1);

                
                if (User.IsInRole("Recruiter"))
                {
                    AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    dbPosition.Company = appUser.Company;
                }
                if (User.IsInRole("CSO"))
                {
                    dbPosition.Company = _context.Companys.Find(SelectedCompanyID);
                }
                //save the changes
                _context.Positions.Update(dbPosition);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return View("Error", new string[] { "There was an error editing this course.", ex.Message });
            }

            //if code gets this far, everything is okay
            //send the user back to the page with all the courses
            return RedirectToAction(nameof(Index));
        }

        // GET: Position/Delete/5
        [Authorize(Roles = "CSO,Recruiter")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .FirstOrDefaultAsync(m => m.PositionID == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // POST: Position/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CSO,Recruiter")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Positions == null)
            {
                return Problem("Entity set 'AppDbContext.Positions'  is null.");
            }
            var position = await _context.Positions.FindAsync(id);
            if (position != null)
            {
                _context.Positions.Remove(position);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(int id)
        {
            return _context.Positions.Any(e => e.PositionID == id);
        }

        public SelectList GetAllPositionSelectList()
        {
            //Get the list of genres from the database
            List<Position> PositionList = _context.Positions.ToList();

            SelectList PositionSelectList = new SelectList(PositionList.OrderBy(b => b.PositionID), "CompanyID", "CompanyName");

            //return the SelectList
            return PositionSelectList;
        }

        public IActionResult DetailedSearch()
        {
            ViewBag.AllPosition = GetAllPositionSelectList();
            ViewBag.AllMajors = GetAllPositionSelectList();
            SearchViewModel svm = new SearchViewModel();

            return View(svm);
        }

        public async Task<IActionResult> DisplaySearchResult(SearchViewModel svm)
        {
            var query = from p in _context.Positions
                        select p;

            if (svm.SearchCompany != null)
            {
                query = query.Where(p => p.Company.CompanyName.Contains(svm.SearchCompany));
            }
            if (svm.SearchIndustry != null)
            {
                query = query.Where(p => p.Company.Industrys.Any(m => m.IndustryName == svm.SearchIndustry));
            }
            if (svm.SearchType != null)
            {
                query = query.Where(p => p.PositionType == (svm.SearchType));
            }
            if (svm.SearchMajor != null)
            {
                query = query.Where(p => p.Majors.Any(m=>m.MajorName==svm.SearchMajor));
            }
            if (svm.SearchLocation != null)
            {
                query = query.Where(p => p.Location.Contains(svm.SearchLocation));
            }

            List<Position> SelectedPositionsnon = query.Include(b => b.Company).ToList();
            if (User.IsInRole("Recruiter"))
            {
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                SelectedPositionsnon = query
                                //.Where(r => r.CurrDate.CurrDate <= r.Deadline)
                                .Where(r => r.Company == appUser.Company)
                                .Include(c => c.Company)
                                .Include(c => c.Majors)
                                .Include(c => c.Company.Industrys)
                                .ToList();
            }
            if (User.IsInRole("Student"))
            {
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                SelectedPositionsnon = query
                                .Where(r => r.CurrDate.CurrDate <= r.Deadline)
                                .Include(c => c.Company)
                                .Include(c => c.Majors)
                                .Include(c => c.Company.Industrys)
                                .ToList();
            }
            if (User.IsInRole("CSO"))
            {

                SelectedPositionsnon = query
                                .Include(c => c.Company)
                                .Include(c => c.Majors)
                                .Include(c => c.Company.Industrys)
                                .ToList();
            }

            ViewBag.AllPositions = _context.Positions.Count();
            ViewBag.SelectedPositionsnon = SelectedPositionsnon.Count();
            return View("Index", SelectedPositionsnon.OrderBy(s => s.Title));
        }
    }
}

