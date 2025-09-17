public class LoginRepo
{
    public LoginRepo(){}

    public List<Login> GetAll() => [.. DbHandler.GetAll<Login>()];

    public Login? GetById(int id) => DbHandler.GetById<Login>(id);

    public void Add(Login newLogin) => DbHandler.Post(newLogin);

    public void Update(Login updatedLogin) => DbHandler.Put(updatedLogin);

    public void Delete(int id) => DbHandler.Delete<Login>(id);
}