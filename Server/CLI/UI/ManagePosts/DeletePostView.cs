using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class DeletePostView
{
    private readonly IPostRepository postRepository;

    public DeletePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    // This method prompts the user for a post ID and deletes the post if valid
    public async Task ShowAsync()
    {
        Console.WriteLine("Enter the ID of the post to delete:");
        string input = Console.ReadLine() ?? "";
        
        if (int.TryParse(input, out int postId))
        {
            await DeletePostAsync(postId); // Call the method to delete the post by ID
        }
        else
        {
            Console.WriteLine("Invalid ID. Please try again.");
        }
    }

    // This method performs the deletion of a post with the provided ID
    public async Task DeletePostAsync(int postId)
    {
        try
        {
            await postRepository.DeleteAsync(postId); // Call repository to delete the post
            Console.WriteLine($"Post with ID {postId} deleted successfully.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}"); // Handle any exceptions (e.g., post not found)
        }
    }
}