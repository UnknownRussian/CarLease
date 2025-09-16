public class CompanyRepo
{
    private readonly string _conStr;

    public CompanyRepo()
    {
        _conStr = DbConfig.GetConnectionString();
    }

    
}