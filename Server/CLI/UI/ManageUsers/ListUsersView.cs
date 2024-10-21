using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine();
        Console.WriteLine("Enter the ID of the user you want to view:");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid input. Please enter a valid user ID.");
            return;
        }

        // Retrieve the user
        var user = await userRepository.GetSingleAsync(userId);
        if (user == null)
        {
            Console.WriteLine($"User with ID {userId} not found.");
            return;
        }

        // Display user details
        Console.WriteLine("User Details:");
        Console.WriteLine($"ID: {user.UserId}");
        Console.WriteLine($"Username: {user.Name}");
    }
    
}