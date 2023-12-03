using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IProductCommentService
    {
        /// <summary>
        /// Add a product to customer's cart
        /// </summary>
        /// <param name="productComment">Product comment to add</param>
        /// <returns>Added product comment response</returns>
        public Task<FeatureResponse> AddCommentAsync(FeatureAddRequest productComment);
        /// <summary>
        /// Get all comments by product Id
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Added product comment response</returns>
        public Task<List<FeatureResponse>> GetAllCommentsAsync(int productId);
        /// <summary>
        /// Get comment by Id
        /// </summary>
        /// <param name="commentId">Comment Id</param>
        /// <returns>List of all comments</returns>
        public Task<FeatureResponse> GetCommentAsync(int commentId);
        /// <summary>
        /// Remove comment by CommentResponse
        /// </summary>
        /// <param name="productComment">Comment Responce to Remove</param>
        /// <returns>true, if removed</returns>
        public Task<bool> RemoveCommentAsync(FeatureResponse productComment);
        /// <summary>
        /// Remove comment by Id
        /// </summary>
        /// <param name="productCommentId">Product comment Id to Remove</param>
        /// <returns>true, if removed</returns>
        public Task<bool> RemoveCommentByIdAsync(int productCommentId);
        /// <summary>
        /// Update existing comment
        /// </summary>
        /// <param name="commentUpdateRequest">Comment Update Request</param>
        /// <returns>Updated comment responce</returns>
        public Task<FeatureResponse> UpdateCommentAsync(FeatureUpdateRequest commentUpdateRequest);

    }
}
