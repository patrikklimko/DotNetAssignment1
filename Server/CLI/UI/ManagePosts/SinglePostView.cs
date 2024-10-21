using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts
{
    public class SinglePostView
    {
        private readonly IPostRepository postRepository;
        private readonly ICommentRepository commentRepository;
        private readonly IUserRepository userRepository;
        private readonly int postId;

        public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository, IUserRepository userRepository, int postId)
        {
            this.postRepository = postRepository;
            this.commentRepository = commentRepository;
            this.userRepository = userRepository;
            this.postId = postId;
        }

        public async Task ShowAsync()
        {
            // Retrieve the post by ID
            var post = await postRepository.GetSingleAsync(postId);
            if (post == null)
            {
                Console.WriteLine("Post not found.");
                return;
            }

            // Display post details
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine($"Content: {post.Content}");
            Console.WriteLine("-----------------------------------------------------");

            // Get comments associated with the post
            var comments = commentRepository.GetMany().Where(c => c.PostId == postId);
            Console.WriteLine("Comments:");
            foreach (var comment in comments)
            {
                Console.WriteLine($"Comment: {comment.Text} by User {comment.UserId}");
            }
        }

    }
}