using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageUsers;

public class UpdateUserView
{
    private readonly IUserRepository userRepository;

    public UpdateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine();
        Console.WriteLine("Enter the ID of the user you want to update:");
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

        Console.WriteLine($"Current username: {user.Name}");
        Console.WriteLine("Enter a new username (leave blank to keep the current username):");
        string newUsername = Console.ReadLine() ?? "";

        // Update the user with new values
        if (!string.IsNullOrEmpty(newUsername))
        {
            user.Name = newUsername;
        }

        // Save the updated user
        await userRepository.UpdateAsync(user);
        Console.WriteLine("User updated successfully!");
    }
}