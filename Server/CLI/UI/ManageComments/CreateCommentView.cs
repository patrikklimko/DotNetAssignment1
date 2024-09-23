using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public Task ShowAsync()
    {
        Console.WriteLine();
        return CreateCommentAsync(); // pass the task to the caller to await if needed
    }

    private Task CreateCommentAsync()
    {
        while (true)
        {
            Console.WriteLine("You are creating a comment.");
            Console.WriteLine("Please insert comment content:");
            string? content = null;
            
            // keep asking for input until it's not empty, or exit if '<' is entered
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
                    Console.WriteLine("Comment creation cancelled.");
                    return Task.CompletedTask;
                }
            }

            Console.WriteLine("Please insert the ID of the user who created the comment:");

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
                    Console.WriteLine("Comment creation cancelled.");
                    return Task.CompletedTask;
                }

                if (int.TryParse(input, out userId))
                {
                    // TODO: Check if the user exists
                    break;
                }
                else
                {
                    Console.WriteLine("Could not parse the ID, please try again.");
                }
            }

            Console.WriteLine("Please insert the ID of the post the comment is related to:");

            int postId;
            while (true)
            {
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Post ID cannot be empty.");
                    continue;
                }

                if ("<".Equals(input))
                {
                    Console.WriteLine("Comment creation cancelled.");
                    return Task.CompletedTask;
                }

                if (int.TryParse(input, out postId))
                {
                    // TODO: Check if the post exists
                    break;
                }
                else
                {
                    Console.WriteLine("Could not parse the post ID, please try again.");
                }
            }

            Console.WriteLine("You are about to create a comment.");
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
                case "y": return AddCommentAsync(content, userId, postId); // return the task to the caller
                case "n":
                {
                    Console.WriteLine("Comment creation cancelled.");
                    return Task.CompletedTask;
                }
                default:
                    Console.WriteLine("Invalid option, please try again.\n\n");
                    break;
            }
        }
    }

    // add comment to the repository
    private async Task AddCommentAsync(string content, int userId, int postId)
    {
        Comment comment = new(1,"Boha tento c#",1,1);
        Comment added = await commentRepository.AddAsync(comment);
        Console.WriteLine("Comment created successfully, with Id: " + added.CommentId);
    }
}
