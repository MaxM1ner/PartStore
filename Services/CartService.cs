using DataAccess;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CartProductResponse> AddProductAsync(CartAddRequest? cartProduct)
        {
            if (cartProduct == null) throw new ArgumentNullException(nameof(cartProduct));

            if (cartProduct.CustomerId == Guid.Empty) throw new ArgumentException(nameof(cartProduct.CustomerId));

            var newCartProduct = await _context.AddAsync(new CartProduct()
            {
                CustomerId = cartProduct.CustomerId.ToString(),
                ProductId = cartProduct.ProductId,
                Quantity = cartProduct.Quantity
            });
            await _context.SaveChangesAsync();

            return newCartProduct.Entity.ToCartProductResponse();
        }

        public async Task<bool> ClearCartAsync(string? CustomerId)
        {
            if (CustomerId == null) throw new ArgumentNullException(nameof(CustomerId));

            if ((await _context.Customers.FindAsync(CustomerId.ToString())) == null) throw new ArgumentException(nameof(CustomerId));

            foreach (var cartProduct in _context.CartProducts.Where(x => x.CustomerId == CustomerId.ToString()))
            {
                await RemoveProductAsync(cartProduct.ToCartProductResponse());
            }
            return true;
        }

        public async Task<List<CartProductResponse>> GetAllProductsAsync(string? CustomerId)
        {
            if (CustomerId == null) throw new ArgumentNullException(nameof(CustomerId));

            if ((await _context.Customers.FindAsync(CustomerId.ToString())) == null) throw new ArgumentException(nameof(CustomerId));

            return (await _context.CartProducts.Include(x => x.Product).ThenInclude(x => x.Images).Where(x => x.CustomerId == CustomerId.ToString()).ToListAsync()).Select(x => x.ToCartProductResponse()).ToList();
        }

        public async Task<CartProductResponse>? GetCartProductAsync(int cartProductId)
        {
            if (cartProductId < 0) throw new ArgumentException($"CartProductId {cartProductId} can't be lower than 0");

            var cartProduct = await _context.CartProducts.Include(x => x.Product).ThenInclude(x => x.Images).Where(x => x.CartProductId == cartProductId).FirstAsync();
            if (cartProduct == null) throw new ArgumentException(nameof(cartProductId));

            return cartProduct.ToCartProductResponse();
        }

        public async Task<bool> RemoveProductAsync(CartProductResponse? cartProduct)
        {
            if (cartProduct == null) throw new ArgumentNullException(nameof(cartProduct));

            if (cartProduct.CustomerId == Guid.Empty) throw new ArgumentException(nameof(cartProduct.CustomerId));

            var dbCartProduct = _context.CartProducts.Where(x => x.CartProductId == cartProduct.CartProductId).FirstOrDefault();
            if (dbCartProduct == null) return false;

            _context.CartProducts.Remove(dbCartProduct);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CartProductResponse> UpdateQuantityAsync(CartProductResponse? cartProduct, int newQuantity)
        {
            if (cartProduct == null) throw new ArgumentNullException(nameof(cartProduct));

            if (cartProduct.CustomerId == Guid.Empty) throw new ArgumentException(nameof(cartProduct.CustomerId));

            var dbCartProduct = _context.CartProducts.Where(x => x.CartProductId == cartProduct.CartProductId).FirstOrDefault();
            if (dbCartProduct == null) throw new NullReferenceException(nameof(cartProduct));

            dbCartProduct.Quantity = newQuantity;

            var newCartProduct = _context.CartProducts.Update(dbCartProduct);
            await _context.SaveChangesAsync();

            return newCartProduct.Entity.ToCartProductResponse();
        }
    }
}
