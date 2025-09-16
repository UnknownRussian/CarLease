public class UserRepo
{
    private readonly string _conStr;

    public UserRepo()
    {
        _conStr = DbConfig.GetConnectionString();
    }

    
}