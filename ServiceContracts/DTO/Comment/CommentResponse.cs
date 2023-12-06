using Entities.Enums;
using Entities.Models;
using ServiceContracts.DTO.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Comment
{
    /// <summary>
    /// DTO class used as return type of OrdersService methods
    /// </summary>
    public class CommentResponse
    {
        public CommentResponse() 
        {
            
        }
        public int CommentId { get; set; }
        public string Value { get; set; } = null!;
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string CustomerId { get; set; } = null!;
        public string? CustomerName { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(CommentResponse)) return false;

            CommentResponse? toCompare = obj as CommentResponse;
            if (toCompare == null) return false;

            return toCompare.Value == Value &&
                toCompare.ProductId == ProductId &&
                toCompare.CustomerId == CustomerId;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string? ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public static class CustomerCommentExtensions
    {
        public static CommentResponse ToCommentResponse(this ProductComment comment)
        {
            return new CommentResponse
            {
                CommentId = comment.Id,
                Value = comment.Value,
                ProductId = comment.ProductId,
                CustomerId = comment.CustomerId,
                ProductName = comment.Product?.Name,
                CustomerName = comment.Customer?.UserName
            };
        }
    }
}
