using DataAccess;
using Entities.Enums;
using Entities.Models;
using ServiceContracts.DTO.Order;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO.Feature;
using ServiceContracts.DTO.Comment;

namespace Services
{
    public sealed class ProductCommentManager : IProductCommentService
    {
        private readonly ApplicationDbContext _context;

        public ProductCommentManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommentResponse> AddCommentAsync(CommentAddRequest productComment)
        {
            if (productComment == null) throw new ArgumentNullException(nameof(productComment));
            if (productComment.CustomerId == string.Empty || productComment.CustomerId == null) throw new ArgumentException(nameof(productComment.CustomerId));

            var newComment = productComment.ToProductComment();

            await _context.ProductComments.AddAsync(newComment);
            await _context.SaveChangesAsync();

            return newComment.ToCommentResponse();
        }

        public async Task<List<CommentResponse>> GetAllCommentsAsync(int productId)
        {

            return await _context.ProductComments.Include(x => x.Product).Include(x => x.Customer).Where(x => x.ProductId == productId).Select(x => x.ToCommentResponse()).ToListAsync();
        }

        public async Task<CommentResponse> GetCommentAsync(int commentId)
        {

            var comment = await _context.ProductComments.Include(x => x.Product).Include(x => x.Customer).Where(x => x.Id == commentId).FirstAsync();
            if (comment == null) throw new ArgumentException(nameof(commentId));

            return comment.ToCommentResponse();
        }

        public async Task<bool> RemoveCommentAsync(CommentResponse productComment)
        {
            if (productComment == null) throw new ArgumentNullException(nameof(productComment));

            var dbProductComment = await _context.CustomerOrders.Where(x => x.Id == productComment.CommentId).FirstOrDefaultAsync();
            if (dbProductComment == null) return false;

            _context.CustomerOrders.Remove(dbProductComment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveCommentByIdAsync(int productCommentId)
        {
            if (productCommentId < 0) throw new ArgumentException(nameof(productCommentId));

            var dbProductComment = await _context.CustomerOrders.Where(x => x.Id == productCommentId).FirstOrDefaultAsync();
            if (dbProductComment == null) return false;

            _context.CustomerOrders.Remove(dbProductComment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CommentResponse> UpdateCommentAsync(CommentUpdateRequest commentUpdateRequest)
        {
            if (commentUpdateRequest == null) throw new ArgumentNullException(nameof(commentUpdateRequest));

            CommentResponse comment = await GetCommentAsync(commentUpdateRequest.CommentId);
            if (comment == null) throw new ArgumentException(nameof(commentUpdateRequest));

            var commentToUpdate = (await GetCommentAsync(commentUpdateRequest.CommentId));
            if (commentToUpdate == null) throw new ArgumentException(nameof(commentUpdateRequest));
            var dbProductComment = await _context.ProductComments.Where(x => x.Id == commentToUpdate.CommentId).FirstOrDefaultAsync();

            dbProductComment.Value = commentUpdateRequest.Value;
            if (commentUpdateRequest.CustomerId != string.Empty && commentUpdateRequest.CustomerId != null)
                dbProductComment.CustomerId = commentUpdateRequest.CustomerId;

            var updatedComment = _context.ProductComments.Update(dbProductComment);
            await _context.SaveChangesAsync();
            return dbProductComment.ToCommentResponse();
        }
    }
}
