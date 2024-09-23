using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public void Show()
    {
        Console.WriteLine();
        ViewUsersAsync();
    }
    
    private void ViewUsersAsync()
    {
        IEnumerable<User> manyAsync = userRepository.GetMany();
        List<User> users = manyAsync.OrderBy(u => u.UserId).ToList();
        
        Console.WriteLine("Users:");
        Console.WriteLine("[");
        foreach (User user in users)
        {
            Console.WriteLine($"\tID: {user.UserId}, Name: {user.Name}");
        }

        Console.WriteLine("]");
        Console.WriteLine();
    }
}