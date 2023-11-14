using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CartService : ICartService
    {
        public CartProductResponse AddProduct(CartAddRequest? cartProduct)
        {
            throw new NotImplementedException();
        }

        public bool ClearCart(Guid? CustomerId)
        {
            throw new NotImplementedException();
        }

        public List<CartProductResponse> GetAllProducts(Guid? CustomerId)
        {
            throw new NotImplementedException();
        }

        public CartProductResponse? GetCartProduct(int CartProductId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveProduct(CartProductResponse? cartProduct)
        {
            throw new NotImplementedException();
        }

        public CartProductResponse UpdateQuantity(CartProductResponse? cartProduct, int newQuantity)
        {
            throw new NotImplementedException();
        }
    }
}
