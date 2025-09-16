public class VehRegRepo
{
    private readonly string _conStr;

    public VehRegRepo()
    {
        _conStr = DbConfig.GetConnectionString();
    }

    
}