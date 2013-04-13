using System.Linq;
using Vakio.Data;
public interface IUserRepository
{
    User GetById(int id);
    IQueryable<User> GetUsers();
    User GetByUserName(string name);
}

public class UserRepository : IUserRepository
{
    public UserRepository() : this(new DataClasses1DataContext()) { }
    
    public UserRepository(DataClasses1DataContext dataContext)        
    {
        _dataContext = dataContext;
    }
    
    private DataClasses1DataContext _dataContext;

    public User GetById(int id)
    {
        return _dataContext.Users.Where(user => user.Id == id).SingleOrDefault();
    }

    public User GetByUserName(string name)
    {
        return _dataContext.Users.Where(user => user.Username == name).SingleOrDefault();
    }

    public IQueryable<User> GetUsers()
    {
        return _dataContext.Users;
    }
}