public class VehicleRepo
{
    private readonly string _conStr;

    public VehicleRepo()
    {
        _conStr = DbConfig.GetConnectionString();
    }

    
}