using RepositoryContracts;
using Entities;

namespace CLI.UI.ManagePosts;

public class UpdatePostView
{
    private readonly IPostRepository postRepository;

    public UpdatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine();
        Console.WriteLine("Enter the ID of the post you want to update:");
        if (!int.TryParse(Console.ReadLine(), out int postId))
        {
            Console.WriteLine("Invalid input. Please enter a valid post ID.");
            return;
        }

        // Retrieve the post
        var post = await postRepository.GetSingleAsync(postId);
        if (post == null)
        {
            Console.WriteLine($"Post with ID {postId} not found.");
            return;
        }

        Console.WriteLine($"Current title: {post.Title}");
        Console.WriteLine("Enter a new title (leave blank to keep the current title):");
        string newTitle = Console.ReadLine() ?? "";

        Console.WriteLine($"Current content: {post.Content}");
        Console.WriteLine("Enter new content (leave blank to keep the current content):");
        string newContent = Console.ReadLine() ?? "";

        // Update the post with new values
        if (!string.IsNullOrEmpty(newTitle))
        {
            post.Title = newTitle;
        }
        if (!string.IsNullOrEmpty(newContent))
        {
            post.Content = newContent;
        }

        // Save the updated post
        await postRepository.UpdateAsync(post);
        Console.WriteLine("Post updated successfully!");
    }
}