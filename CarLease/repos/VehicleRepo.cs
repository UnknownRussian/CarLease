public class VehicleRepo
{
    public VehicleRepo(){}

    public List<Vehicle> GetAll() => [.. DbHandler.GetAll<Vehicle>()];

    public Vehicle? GetById(int id) => DbHandler.GetById<Vehicle>(id);

    public void Add(Vehicle newVehicle) => DbHandler.Post(newVehicle);

    public void Update(Vehicle updatedVehicle) => DbHandler.Put(updatedVehicle);

    public void Delete(int id) => DbHandler.Delete<Vehicle>(id);
}