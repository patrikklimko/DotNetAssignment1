using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class DeleteUserView
{
    private readonly IUserRepository userRepository;

    public DeleteUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine();
        Console.WriteLine("Enter the ID of the user you want to delete:");
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

        // Confirm deletion
        Console.WriteLine($"Are you sure you want to delete user '{user.Name}'? (y/n):");
        string confirmation = Console.ReadLine()?.ToLower();
        if (confirmation == "y")
        {
            await userRepository.DeleteAsync(userId);
            Console.WriteLine("User deleted successfully!");
        }
        else
        {
            Console.WriteLine("User deletion cancelled.");
        }
    }
}