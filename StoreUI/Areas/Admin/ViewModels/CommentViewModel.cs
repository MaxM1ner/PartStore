using ServiceContracts.DTO.Comment;

namespace StoreUI.Areas.Admin.ViewModels
{
    public sealed class CommentViewModel
    {
        public int CommentId { get; set; }
        public string Value { get; set; } = null!;
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }

        public CommentAddRequest ToCommentAddRequest()
        {
            return new CommentAddRequest(CustomerId, ProductId, Value);
        }
        public CommentUpdateRequest ToCommentUpdateRequest()
        {
            return new CommentUpdateRequest(CommentId, Value);
        }
    }
    public static class CommentResponseExtensions
    {
        public static CommentViewModel ToCommentViewModel(this CommentResponse response)
        {
            return new CommentViewModel() 
            {
                CommentId = response.CommentId,
                Value = response.Value,
                ProductId = response.ProductId,
                CustomerId = response.CustomerId,
                CustomerName = response.CustomerName,
                ProductName = response.ProductName,
            };
        }
    }
}
