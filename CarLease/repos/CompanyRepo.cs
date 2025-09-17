public class CompanyRepo
{
    public CompanyRepo() { }

    public List<Company> GetAll() => [.. DbHandler.GetAll<Company>()];

    public Company? GetById(int id) => DbHandler.GetById<Company>(id);

    public void Add(Company newCompany) => DbHandler.Post(newCompany);

    public void Update(Company updatedCompany) => DbHandler.Put(updatedCompany);

    public void Delete(int id) => DbHandler.Delete<Company>(id);
}