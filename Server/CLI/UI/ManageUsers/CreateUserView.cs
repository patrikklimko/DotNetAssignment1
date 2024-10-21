using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers
{
    public class CreateUserView
    {
        private readonly IUserRepository userRepository;
        private int lastUserId; // Variable to keep track of the last used User ID

        public CreateUserView(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.lastUserId = 0; // Initialize the last used User ID
        }

        public async Task ShowAsync()
        {
            Console.WriteLine();
            await CreateUserAsync();
        }

        private async Task CreateUserAsync()
        {
            while (true)
            {
                Console.WriteLine("You are creating a user.");

                // Get username
                string? name = null;
                while (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Please insert user name:");
                    name = Console.ReadLine();
                    if (string.IsNullOrEmpty(name))
                    {
                        Console.WriteLine("Name cannot be empty.");
                        continue;
                    }

                    if ("<".Equals(name))
                    {
                        Console.WriteLine("User creation cancelled.");
                        return;
                    }
                }

                // Get password
                string? password = null;
                while (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Please insert password:");
                    password = Console.ReadLine();
                    if (string.IsNullOrEmpty(password))
                    {
                        Console.WriteLine("Password cannot be empty.");
                        continue;
                    }

                    if ("<".Equals(password))
                    {
                        Console.WriteLine("User creation cancelled.");
                        return;
                    }
                }

                // Generate a new User ID
                lastUserId++; // Increment to get the next User ID

                // Confirm the creation
                Console.WriteLine("You are about to create a user with the following information:");
                Console.WriteLine($"User name: {name}");
                Console.WriteLine($"Password: {password}");
                Console.WriteLine($"User ID: {lastUserId}");
                Console.WriteLine("Do you want to proceed? (y/n)");

                string? confirmation = null;
                while (true)
                {
                    confirmation = Console.ReadLine();
                    if (string.IsNullOrEmpty(confirmation))
                    {
                        Console.WriteLine("Please select an option.\n\n");
                        continue;
                    }

                    confirmation = confirmation.ToLower();
                    if (confirmation != "y" && confirmation != "n")
                    {
                        Console.WriteLine("Please select a valid option.\n\n");
                        continue;
                    }

                    break;
                }

                switch (confirmation)
                {
                    case "y":
                        await AddUserAsync(name, lastUserId, password);
                        break;
                    case "n":
                        Console.WriteLine("User creation cancelled.");
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.\n\n");
                        break;
                }
            }
        }

        private async Task AddUserAsync(string name, int userId, string password)
        {
            // Create a new User object
            User user = new User(name, userId, password);

            // Add the user to the repository
            User created = await userRepository.AddAsync(user);

            // Display success message with details
            Console.WriteLine("User created successfully:");
            Console.WriteLine($"ID: {created.UserId}");
            Console.WriteLine($"Username: {created.Name}");
        }
    }
}
