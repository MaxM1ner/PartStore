using Entities.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.ProductType
{
    public class ProductTypeUpdateRequest
    {
        public ProductTypeUpdateRequest(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
        public string Value { get; set; } = null!;
        public string TypeImagepath { get; set; } = null!;
        public bool Visible { get; set; } = true;

        public Entities.Models.ProductType ToProduct()
        {
            return new Entities.Models.ProductType()
            {
                Value = Value,
                TypeImagepath = TypeImagepath,
                Visible = Visible
            };
        }
    }
}
