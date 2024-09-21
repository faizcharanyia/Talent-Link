using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using sp23Team09FinalProject.DAL;
using sp23Team09FinalProject.Models;

namespace sp23Team09FinalProject
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CompanyController(AppDbContext context, UserManager<AppUser> userManger)
        {
            _context = context;
            _userManager = userManger;
        }

        public SelectList GetAllIndustrySelectList()
        {
          
            List<Industry> IndustryList = _context.Industrys.ToList();

            SelectList IndystrySelectList = new SelectList(IndustryList.OrderBy(s => s.IndustryID), "IndustryID", "IndustryName");

            //return the SelectList
            return IndystrySelectList;
        }
        private MultiSelectList GetAllIndustrySelectList(Company company)
        {
            //Create a new list of departments and get the list of the departments
            //from the database
            List<Industry> IndustryList = _context.Industrys.ToList();

            //loop through the list of course departments to find a list of department ids
            //create a list to store the department ids
            List<Int32> selectedIndustryIDs = new List<Int32>();

            //Loop through the list to find the DepartmentIDs
            foreach (Industry associatedIndustry in company.Industrys)
            {
                selectedIndustryIDs.Add(associatedIndustry.IndustryID);
            }

            //use the MultiSelectList constructor method to get a new MultiSelectList
            MultiSelectList mslAllIndustry = new MultiSelectList(IndustryList.OrderBy(d => d.IndustryName), "IndustryID", "IndustryName", selectedIndustryIDs);

            //return the MultiSelectList
            return mslAllIndustry;
        }

        public SelectList GetAllCompanySelectList()
        {
            //Get the list of genres from the database
            List<Company> CompanyList = _context.Companys.ToList();

            SelectList CompanySelectList = new SelectList(CompanyList.OrderBy(b => b.CompanyID), "CompanyID", "CompanyName");

            //return the SelectList
            return CompanySelectList;
        }

        // GET: Company
        public async Task<IActionResult> Index(String SearchString)
        {
            var query = from c in _context.Companys
                        select c;
            List<Company> SelectedCompany = query
                .Include(c => c.CurrDate).Include(c => c.Positions).ToList();

            if (SearchString == null || SearchString == "")
            {
                if (User.IsInRole("Recruiter"))
                {
                    AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    SelectedCompany = query
                                    .Where(r => r == appUser.Company)
                                    .ToList();
                }
                return View(SelectedCompany.OrderBy(s => s.CompanyName));
            }
            else
            {
                var query2 = from c in _context.Companys
                             select c;

                query2 = query2.Where(b => b.CompanyName.Contains(SearchString) ||
                b.CompanyDescription.Contains(SearchString));

                List<Company> SelectedCompanysnon = query2.ToList();

                if (User.IsInRole("Recruiter"))
                {
                    AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    SelectedCompany = query2
                                    .Where(r => r == appUser.Company)
                                    .ToList();
                }

            }

            List<Company> SelectedCompanys = query.ToList();


            //Populate the view bag with a count of all books
            ViewBag.AllCompanys = _context.Companys.Count();
            //Populate the view bag with a count of selected books
            ViewBag.SelectedCompanys = SelectedCompanys.Count;

            return View(SelectedCompanys.OrderBy(s => s.CompanyName));




            //return View(await _context.Companys.ToListAsync());
        }

        // GET: Company/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Companys == null)
            {
                return NotFound();
            }


            Company company = await _context.Companys.Include(c => c.Industrys).Include(p => p.Positions)
               .FirstOrDefaultAsync(m => m.CompanyID == id);

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Company/Create
        [Authorize(Roles = "CSO,Recruiter")]
        public IActionResult Create()
        {
            ViewBag.AllIndustries = GetAllIndustrySelectList();
            return View();
        }

        // POST: Company/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "CSO,Recruiter")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company, int[] SelectedIndustries)
        {

            if (ModelState.IsValid == false)
            {

                ViewBag.AllIndustries = GetAllIndustrySelectList();
                return View(company);
            }
            List<Company> CompanyList = _context.Companys.ToList(); ;
            foreach (Company com in CompanyList)
            {
                if ((com.CompanyName == company.CompanyName) && (com.CompanyID != company.CompanyID))
                {
                    return View("Error", new string[] { "This company is already made" });
                }
            }
            company.CurrDate = _context.CurrDate.Find(1);
            _context.Add(company);
            await _context.SaveChangesAsync();

            //add the associated departments to the course
            //loop through the list of deparment ids selected by the user
            foreach (int industryID in SelectedIndustries)
            {
                //find the department associated with that id
                Industry dbIndustry = _context.Industrys.Find(industryID);

                //add the department to the course's list of departments and save changes
                company.Industrys.Add(dbIndustry);
                _context.SaveChanges();
            }

            //Send the user to the page with all the departments
            return RedirectToAction(nameof(Index));
        }

        // GET: Company/Edit/5
        [Authorize(Roles = "CSO,Recruiter")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Companys == null)
            {
                return NotFound();
            }

            Company company = await _context.Companys.Include(c => c.Industrys)
                .FirstOrDefaultAsync(m => m.CompanyID == id);

            if (company == null)
            {
                return NotFound();
            }
            ViewBag.AllIndustries = GetAllIndustrySelectList(company);
            return View(company);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "CSO,Recruiter")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Company company, int[] SelectedIndustries)
        {
            //this is a security check to see if the user is trying to modify
            //a different record.  Show an error message
            if (id != company.CompanyID)
            {
                return View("Error", new string[] { "Please try again!" });
            }

            if (ModelState.IsValid == false) //there is something wrong
            {
                ViewBag.AllIndustries = GetAllIndustrySelectList(company);
                return View(company);
            }

            //if code gets this far, attempt to edit the course
            try
            {
                //Find the course to edit in the database and include relevant 
                //navigational properties
                Company dbCompany = _context.Companys
                    .Include(c => c.Industrys)
                    .FirstOrDefault(c => c.CompanyID == company.CompanyID);

                //create a list of departments that need to be removed
                List<Industry> IndustriesToRemove = new List<Industry>();

                //find the departments that should no longer be selected because the
                //user removed them
                //remember, SelectedDepartments = the list from the HTTP request (listbox)
                foreach (Industry industry in dbCompany.Industrys)
                {
                    //see if the new list contains the department id from the old list
                    if (SelectedIndustries.Contains(industry.IndustryID) == false)//this department is not on the new list
                    {
                        IndustriesToRemove.Add(industry);
                    }
                }

                //remove the departments you found in the list above
                //this has to be 2 separate steps because you can't iterate (loop)
                //over a list that you are removing things from
                foreach (Industry industry in IndustriesToRemove)
                {
                    //remove this course department from the course's list of departments
                    dbCompany.Industrys.Remove(industry);
                    _context.SaveChanges();
                }

                //add the departments that aren't already there
                foreach (int industryID in SelectedIndustries)
                {
                    if (dbCompany.Industrys.Any(d => d.IndustryID == industryID) == false)//this department is NOT already associated with this course
                    {
                        //Find the associated department in the database
                        Industry dbIndustry = _context.Industrys.Find(industryID);

                        //Add the department to the course's list of departments
                        dbCompany.Industrys.Add(dbIndustry);
                        _context.SaveChanges();
                    }
                }

                //update the course's scalar properties
                if (User.IsInRole("CSO"))
                {
                    dbCompany.CompanyName = company.CompanyName;
                }
                dbCompany.CompanyDescription = company.CompanyDescription;
                dbCompany.CompanyEmail = company.CompanyEmail;
                dbCompany.CurrDate = _context.CurrDate.Find(1);



                _context.Companys.Update(dbCompany);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return View("Error", new string[] { "There was an error editing this company.", ex.Message });
            }

            //if code gets this far, everything is okay
            //send the user back to the page with all the courses
            return RedirectToAction(nameof(Index));
        }

       
        [Authorize(Roles = "CSO,Recruiter")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Companys == null)
            {
                return NotFound();
            }

            var company = await _context.Companys
                .FirstOrDefaultAsync(m => m.CompanyID == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CSO,Recruiter")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Companys == null)
            {
                return Problem("Entity set 'AppDbContext.Companys'  is null.");
            }
            var company = await _context.Companys.FindAsync(id);
            if (company != null)
            {
                _context.Companys.Remove(company);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool CompanyExists(int id)
        {
          return _context.Companys.Any(e => e.CompanyID == id);
        }

        public IActionResult DetailedSearch()
        {
            ViewBag.AllPosition = GetAllCompanySelectList();
            SearchViewModel svm = new SearchViewModel();

            return View(svm);
        }


        public IActionResult DisplaySearchResult(SearchViewModel svm)
        {
            var query = from c in _context.Companys
                        select c;
            
            if (svm.SearchCompany != null)
            {
                query = query.Where(c => c.CompanyName.Contains(svm.SearchCompany));
            }
            
            if (svm.SearchIndustry != null)
            {
                query = query.Where(c => c.Industrys.Any(m => m.IndustryName == svm.SearchIndustry));
            }
            if (svm.SearchType != null)
            {
                
                query = query.Where(c => c.Positions.Any(m => m.PositionType == svm.SearchType));
            }
            if (svm.SearchLocation != null)
            {
                query = query.Where(c => c.Positions.Any(m => m.Location.Contains(svm.SearchLocation)));
            }

            List<Company> SelectedCompany = query.Include(c => c.Positions).ToList();
            return View("Index", SelectedCompany.OrderBy(s => s.CompanyName));
        }
    }
}
