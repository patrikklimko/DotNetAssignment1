using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;

    public ListPostsView(IPostRepository postRepository, ICommentRepository commentRepository, IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
    }

    public Task ShowAsync()
    {
        Console.WriteLine();
        return ViewPostsAsync();
    }

    private async Task ViewPostsAsync()
    {
        // Get all posts and order by ID
        List<Post> posts = postRepository.GetMany().OrderBy(p => p.Id).ToList();
        Console.WriteLine("Showing posts:");
        Console.WriteLine("[");
    
        // Display each post with its ID and title
        foreach (Post post in posts)
        {
            Console.WriteLine($"\t({post.Id}): {post.Title}");
        }

        Console.WriteLine("]");
    
        // Menu options for viewing a single post or going back
        const string options = """
                               [post id]) View post by id
                               <) Back
                               """;
        Console.WriteLine(options);
    
        // Read user selection for viewing a post by ID or returning back
        while (true)
        {
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please select a valid option.");
                continue;
            }

            if ("<".Equals(input))
            {
                return;
            }

            int postId;
            if (int.TryParse(input, out postId))
            {
                // Show details for a single post
                SinglePostView singlePostView = new(postRepository, commentRepository, userRepository, postId);
                await singlePostView.ShowAsync();
                Console.WriteLine(options);
            }
            else
            {
                Console.WriteLine("Invalid option, please try again.");
            }
        }
    }
}
