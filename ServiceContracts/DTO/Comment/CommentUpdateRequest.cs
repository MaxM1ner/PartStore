using Entities.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Comment
{
    public class CommentUpdateRequest
    {
        public CommentUpdateRequest(int commentId, string value)
        {
            CommentId = commentId;
            CustomerId = null;
            if (value == string.Empty || value is null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }
        public CommentUpdateRequest(int commentId, string value, string customerId)
        {
            CommentId = commentId;
            if (customerId == string.Empty || customerId is null) throw new ArgumentNullException(nameof(customerId));
            CustomerId = customerId;
            if (value == string.Empty || value is null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public int CommentId { get; private set; }
        public string? CustomerId { get; private set; }
        public string Value { get; private set; } = string.Empty;

    }
}
