using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ServiceContracts.DTO.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.ProductType
{
    /// <summary>
    /// DTO class to add a new product type
    /// </summary>
    public class ProductTypeAddRequest
    {
        public ProductTypeAddRequest()
        {

        }
        public string Value { get; set; } = null!;
        public string TypeImagepath { get; set; } = null!;
        public bool Visible { get; set; } = true;

        public Entities.Models.ProductType ToProductType()
        {
            return new Entities.Models.ProductType()
            {
                Value = this.Value,
                TypeImagepath = this.TypeImagepath,
                Visible = this.Visible
            };
        }
    }
}
