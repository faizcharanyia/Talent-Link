using sp23Team09FinalProject.DAL;
using sp23Team09FinalProject.Models;
using System.Text;

namespace sp23Team09FinalProject.Seeding
{
    public static class SeedCompanys
    {
        public static void SeedAllCompanys(AppDbContext db)
        {
            // check to see if all the genres have already been added
            Int32 intcompanysAdded = 0;
            try
            {
                //Create a list of languages
                List<Company> Companys = new List<Company>();
                

                Company c1 = new Company() { CompanyName = "Shell", CompanyEmail = "shell@example.com", CompanyDescription= "Shell Oil Company, including its consolidated companies and its share in equity companies, is one of America's leading oild and natural gas producers, natural gas marketers, gasoline marketers and petrochemical manufacturers.",
                  
                    
                };
                Companys.Add(c1);

                Company c2 = new Company()
                {
                    CompanyName = "Deloitte",
                    CompanyEmail = "deloitte@example.com",
                    CompanyDescription = "Deloitte is one of the leading professional services organizations in the United States specializing in audit, tax, consulting, and financial advisory services with clients in more than 20 industries.",
                    
                };
                Companys.Add(c2);

                Company c3 = new Company()
                {
                    CompanyName = "Capital One",
                    CompanyEmail = "capitalone@example.com",
                    CompanyDescription = "Capital One offers a broad spectrum of financial products and services to consumers, small businesses and commercial clients.",
                    
                };
                Companys.Add(c3);

                Company c4 = new Company() { CompanyName = "Texas Instruments", CompanyEmail = "texasinstruments@example.com", CompanyDescription = "TI is one of the world’s largest global leaders in analog and digital semiconductor design and manufacturing",
                    
                };
                Companys.Add(c4);

                Company c5 = new Company() { CompanyName = "Hilton Worldwide", CompanyEmail = "hiltonworldwide@example.com", CompanyDescription = "Hilton Worldwide offers business and leisure travelers the finest in accommodations, service, amenities and value."
                     };  
                Companys.Add(c5);


                foreach (Company companiesToAdd in Companys)
                {
                    Company dbCompany = db.Companys.FirstOrDefault(g => g.CompanyName == companiesToAdd.CompanyName);
                    if (dbCompany == null)
                    {
                        db.Companys.Add(companiesToAdd);
                        db.SaveChanges();
                        intcompanysAdded += 1;
                    }
                }
            }
            catch
            {
                String msg = "Companies Added: " + intcompanysAdded.ToString();
                throw new InvalidOperationException(msg);
            }
        }
    }
}
