public class UserRepo
{
    public UserRepo(){}

    public List<User> GetAll() => [.. DbHandler.GetAll<User>()];

    public User? GetById(int id) => DbHandler.GetById<User>(id);

    public void Add(User newUser) => DbHandler.Post(newUser);

    public void Update(User updatedUser) => DbHandler.Put(updatedUser);

    public void Delete(int id) => DbHandler.Delete<User>(id);
}