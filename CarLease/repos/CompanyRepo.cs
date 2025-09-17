public class CompanyRepo
{
    public CompanyRepo() { }

    public List<Company> GetAllCompanies()
    {
        return [.. DbHandler.GetAll<Company>()];
    }

    public Company? GetCompanyById(int id) => DbHandler.GetById<Company>(id);

    
}