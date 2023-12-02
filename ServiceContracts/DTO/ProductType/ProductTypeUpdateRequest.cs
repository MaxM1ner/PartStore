using Entities.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.Order
{
    public class ProductTypeUpdateRequest
    {
        public ProductTypeUpdateRequest()
        {
        }
        public string Value { get; set; } = null!;
        public string TypeImagepath { get; set; } = null!;
        public bool Visible { get; set; } = true;

        public ProductType ToProduct()
        {
            return new ProductType()
            {
                Value = Value,
                TypeImagepath = TypeImagepath,
                Visible = Visible
            };
        }
    }
}
