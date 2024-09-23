using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ManageCommentsView
{
    private readonly ICommentRepository commentRepository;

    public ManageCommentsView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
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
                    await new CreateCommentView(commentRepository).ShowAsync();
                    break;
                case "4":
                    new ListCommentsView(commentRepository).Show();
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.\n\n");
                    break;
            }
        }
    }

    private static void PrintOptions()
    {
        Console.WriteLine();
        const string menuOptions = """
                                   Please select:
                                   1) Create new comment
                                   2) Update comment
                                   3) Delete comment
                                   4) View comments
                                   <) Back
                                   """;
        Console.WriteLine(menuOptions);
    }
}