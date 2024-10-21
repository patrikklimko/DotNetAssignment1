using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;

    public ManagePostsView(IPostRepository postRepository, ICommentRepository commentRepository,
        IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine();
        while (true)
        {
            PrintOptions();
            string input = Console.ReadLine() ?? "";
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please select an option.\n\n");
                continue;
            }

            if ("<".Equals(input))
            {
                return;
            }

            switch (input)
            {
                case "1":
                    await new CreatePostView(postRepository,userRepository).ShowAsync();
                    break;
                case "2":
                    await new UpdatePostView(postRepository).ShowAsync();
                    break;
                
                case "3":
                    // Call DeletePostView to delete a post
                    await new DeletePostView(postRepository).ShowAsync();
                    break;
                    
                case "4":
                    // Prompt the user for a post ID
                    Console.WriteLine("Please enter the ID of the post you want to view:");
                    if (int.TryParse(Console.ReadLine(), out int postId))
                    {
                        // Call SinglePostView to display the specified post
                        await new SinglePostView(postRepository, commentRepository, userRepository, postId).ShowAsync();
                    }
                    else
                    {
                        Console.WriteLine("Invalid post ID. Please try again.");
                    }
                    break;
            }
        }
    }

    private static void PrintOptions()
    {
        Console.WriteLine();
        const string menuOptions = """
                                   Please select:
                                   1) Create new post
                                   2) Update post
                                   3) Delete post
                                   4) View posts
                                   <) Back
                                   """;
        Console.WriteLine(menuOptions);
    }
}
