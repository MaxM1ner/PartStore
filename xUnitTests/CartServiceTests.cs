using DataAccess;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System.Text;
using Xunit.Abstractions;
using Moq.EntityFrameworkCore;
using Moq;
using ServicesUnitTests.MockData;

namespace ServicesUnitTests
{
    public class CartServiceTests
    {
        private readonly ICartService _cartService;
        private readonly ITestOutputHelper _outputHelper;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly ApplicationDbContext _context;
        private readonly List<CartProduct> _cartProducts;

        public CartServiceTests(ITestOutputHelper testOutputHelper)
        {
            _cartProducts = new List<CartProduct>();
            _outputHelper = testOutputHelper;
            _mockContext = new Mock<ApplicationDbContext>();
            _mockContext.Setup(x => x.Customers).ReturnsDbSet(DbContextData.GetCustomers());
            _mockContext.Setup(x => x.Products).ReturnsDbSet(DbContextData.GetProducts());
            _mockContext.Setup(x => x.CartProducts).ReturnsDbSet(_cartProducts);
            _context = _mockContext.Object;
            _cartService = new CartService(_context);
        }

        #region AddProduct
        [Fact]
        public async void AddProduct_NullProduct()
        {
            CartAddRequest? request = null;

            await Assert.ThrowsAsync<ArgumentNullException>(async () => { await _cartService.AddProductAsync(request); });
        }

        [Fact]
        public async void AddProduct_EmptyCustomerId()
        {
            CartAddRequest? nullCustomerIdrequest = new CartAddRequest(Guid.Empty, 0);

            await Assert.ThrowsAsync<ArgumentException>(async () => { await _cartService.AddProductAsync(nullCustomerIdrequest); });
        }

        [Fact]
        public async void AddProduct_CorrectRequest()
        {
            Customer customer = await _context.Customers.FirstAsync();
            Product product = await _context.Products.FirstAsync();
            CartAddRequest? correctRequest = new CartAddRequest(Guid.Parse(customer.Id), product.Id);

            CartProductResponse responseFromAdd = await _cartService.AddProductAsync(correctRequest);
            Assert.NotNull(responseFromAdd);
            Assert.True(responseFromAdd.CartProductId >= 0);
            Assert.True((await _context.CartProducts.Where(x => x.CartProductId == responseFromAdd.CartProductId).CountAsync()) > 0);
        }
        #endregion
    }
}