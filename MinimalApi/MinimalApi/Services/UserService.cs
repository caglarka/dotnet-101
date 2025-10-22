using MinimalApi.Models;

namespace MinimalApi.Services;

public class UserService
{
    public User? Get(int id)
    {
        if(id <= 0)
            return null;
        
        return new User() { Id = id, Name = Guid.NewGuid().ToString() };
    }
}