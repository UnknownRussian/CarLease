public class VehRegRepo
{
    public VehRegRepo(){}

    public List<VehReg> GetAll() => [.. DbHandler.GetAll<VehReg>()];

    public VehReg? GetById(int id) => DbHandler.GetById<VehReg>(id);

    public void Add(VehReg newVehReg) => DbHandler.Post(newVehReg);

    public void Update(VehReg updatedVehReg) => DbHandler.Put(updatedVehReg);

    public void Delete(int id) => DbHandler.Delete<VehReg>(id);
}