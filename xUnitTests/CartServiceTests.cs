using DataAccess;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System.Text;
using Xunit.Abstractions;

namespace xUnitTests
{
    public class CartServiceTests
    {
        private readonly ICartService _cartService;
        private readonly ITestOutputHelper _outputHelper;
        private readonly ApplicationDbContext _context;

        public CartServiceTests(ITestOutputHelper testOutputHelper, ApplicationDbContext context)
        {
            _cartService = new CartService();
            _outputHelper = testOutputHelper;
            _context = context;
        }

        [Fact]
        public void AddProduct_NullProduct()
        {
            CartAddRequest? request = null;

            Assert.Throws<ArgumentNullException>(() => { _cartService.AddProduct(request); });
        }

        [Fact]
        public void AddProduct_EmptyCustomerId()
        {
            CartAddRequest? nullCustomerIdrequest = new CartAddRequest(Guid.Empty, 0);

            Assert.Throws<ArgumentException>(() => { _cartService.AddProduct(nullCustomerIdrequest); });
        }

        [Fact]
        public async void AddProduct_CorrectRequest()
        {
            Customer customer = await _context.Customers.FirstAsync();
            Product product = await _context.Products.FirstAsync();
            CartAddRequest? correctRequest = new CartAddRequest(Guid.Parse(customer.Id), product.Id);

            CartProductResponse responseFromAdd = _cartService.AddProduct(correctRequest);
            Assert.NotNull(responseFromAdd);
            Assert.True(responseFromAdd.CartProductId >= 0);
            Assert.True((await _context.CartProducts.Where(x => x.CartProductId == responseFromAdd.CartProductId).CountAsync()) > 0);

            _cartService.RemoveProduct(responseFromAdd);
        }
    }
}