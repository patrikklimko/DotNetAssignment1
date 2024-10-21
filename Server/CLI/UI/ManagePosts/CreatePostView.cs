using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository; // Added user repository

    public CreatePostView(IPostRepository postRepository, IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository; // Initialize the user repository
    }

    public CreatePostView(IPostRepository postRepository1)
    {
        throw new NotImplementedException();
    }

    public Task ShowAsync()
    {
        Console.WriteLine();
        return CreatePostAsync(); 
    }

    private async Task CreatePostAsync()
    {
        while (true)
        {
            Console.WriteLine("You are creating a post.");
            Console.WriteLine("Please insert post title:");
            string? title = null;

            while (string.IsNullOrEmpty(title))
            {
                title = Console.ReadLine();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Title cannot be empty.");
                    continue;
                }

                if ("<".Equals(title))
                {
                    Console.WriteLine("Post creation cancelled.");
                    return; // Cancel post creation
                }
            }

            Console.WriteLine("Please insert post content:");
            string? content = null;

            while (string.IsNullOrEmpty(content))
            {
                content = Console.ReadLine();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("Content cannot be empty.");
                    continue;
                }

                if ("<".Equals(content))
                {
                    Console.WriteLine("Post creation cancelled.");
                    return; // Cancel post creation
                }
            }

            Console.WriteLine("Please insert the ID of the user that created the post:");

            int userId;

            while (true)
            {
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("ID cannot be empty.");
                    continue;
                }

                if ("<".Equals(input))
                {
                    Console.WriteLine("Post creation cancelled.");
                    return; // Cancel post creation
                }

                if (int.TryParse(input, out userId))
                {
                    // Check if user exists
                    var user = await userRepository.GetSingleAsync(userId); // Fetch user by ID
                    if (user == null)
                    {
                        Console.WriteLine("User not found. Please enter a valid User ID.");
                        continue; // Ask for user ID again
                    }
                    break; // Valid user ID
                }
                else
                {
                    Console.WriteLine("Could not parse the ID, please try again.");
                }
            }

            // Print out the information and ask for confirmation.
            Console.WriteLine("You are about to create a post.");
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
                    await AddPostAsync(title, content, userId); // Await AddPostAsync to ensure completion
                    break;
                case "n":
                    Console.WriteLine("Post creation cancelled.");
                    return; // Cancel post creation
                default:
                    Console.WriteLine("Invalid option, please try again.\n\n");
                    break;
            }
        }
    }

    // Add post to the repository.
    private async Task AddPostAsync(string title, string content, int userId)
    {
        Post post = new(title, content, userId);
        Post added = await postRepository.AddAsync(post);
        Console.WriteLine("Post created successfully, with Id: " + added.Id);
    }
}
