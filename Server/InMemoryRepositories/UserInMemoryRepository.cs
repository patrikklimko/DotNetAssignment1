using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository 
{
    List<User> users = new List<User>();

    public Task<User> AddAsync(User user)
    {
        user.UserId = users.Any()
            ? users.Max(u => u.UserId) + 1  
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(u => u.UserId == user.UserId);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.UserId}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int userId)
    {
        User? userToRemove = users.SingleOrDefault(u => u.UserId == userId);
        if (userToRemove is null)
        {
            throw new InvalidOperationException($"User with ID '{userId}' not found");
        }

        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int userId)
    {
        User? user = users.SingleOrDefault(u => u.UserId == userId);
        if (user is null)
        {
            throw new InvalidOperationException($"User with ID '{userId}' not found");
        }

        return Task.FromResult(user);
    }
    

    public IQueryable<User> GetManyAsync()
    {
        return users.AsQueryable();
    }
}