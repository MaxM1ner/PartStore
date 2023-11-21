using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents cart service methods to use cart products
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Add a product to customer's cart
        /// </summary>
        /// <param name="cartProduct">Cart product to add</param>
        /// <returns>Added cart product response</returns>
        Task<CartProductResponse?> AddProductAsync(CartAddRequest? cartProduct);

        /// <summary>
        /// Find cart product by id
        /// </summary>
        /// <param name="CartProductId">Cart product id</param>
        /// <returns>Cart product response</returns>
        Task<CartProductResponse>? GetCartProductAsync(int CartProductId);

        /// <summary>
        /// Get all cart products in customer's cart
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        Task<List<CartProductResponse>> GetAllProductsAsync(string? CustomerId);

        /// <summary>
        /// Change quantity of cart product
        /// </summary>
        /// <param name="cartProduct">Cart product to change</param>
        /// <param name="newQuantity">New quantity of product</param>
        /// <returns>Updated CartProductResponse</returns>
        Task<CartProductResponse> UpdateQuantityAsync(CartProductResponse? cartProduct, int newQuantity);

        /// <summary>
        /// Remove product from customer's cart
        /// </summary>
        /// <param name="cartProduct">Cart product to remove</param>
        /// <returns>True if success, otherwise false</returns>
        Task<bool> RemoveProductAsync(CartProductResponse? cartProduct);

        /// <summary>
        /// Clear customer's cart
        /// </summary>
        /// <param name="CustomerId">Customer Id</param>
        /// <returns>True if success, otherwise false</returns>
        Task<bool> ClearCartAsync(string? CustomerId);
    }
}