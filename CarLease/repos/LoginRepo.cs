public class LoginRepo
{
    private readonly string _conStr;

    public LoginRepo()
    {
        _conStr = DbConfig.GetConnectionString();
    }

    
}