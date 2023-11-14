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
        CartProductResponse AddProduct(CartAddRequest? cartProduct);

        /// <summary>
        /// Find cart product by id
        /// </summary>
        /// <param name="CartProductId">Cart product id</param>
        /// <returns>Cart product response</returns>
        CartProductResponse? GetCartProduct(int CartProductId);

        /// <summary>
        /// Get all cart products in customer's cart
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        List<CartProductResponse> GetAllProducts(Guid? CustomerId);

        /// <summary>
        /// Change quantity of cart product
        /// </summary>
        /// <param name="cartProduct">Cart product to change</param>
        /// <param name="newQuantity">New quantity of product</param>
        /// <returns>Updated CartProductResponse</returns>
        CartProductResponse UpdateQuantity(CartProductResponse? cartProduct, int newQuantity);

        /// <summary>
        /// Remove product from customer's cart
        /// </summary>
        /// <param name="cartProduct">Cart product to remove</param>
        /// <returns>True if success, otherwise false</returns>
        bool RemoveProduct(CartProductResponse? cartProduct);

        /// <summary>
        /// Clear customer's cart
        /// </summary>
        /// <param name="CustomerId">Customer Id</param>
        /// <returns>True if success, otherwise false</returns>
        bool ClearCart(Guid? CustomerId);
    }
}