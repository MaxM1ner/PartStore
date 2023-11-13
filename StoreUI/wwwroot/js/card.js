function addToOrder(productId) {
  var product = document.querySelector('.card-page-content .product-item[data-product-id="' + productId + '"]');
  
  if (!product.classList.contains('added-to-order')) {
    var clonedProduct = product.cloneNode(true);
    
    var orderBlock = document.querySelector('.card-orders-block');
    
    orderBlock.appendChild(clonedProduct);
    
    clonedProduct.querySelector('.card-btn.add').remove();
    
    var deleteButton = clonedProduct.querySelector('.card-btn.remove');
    deleteButton.addEventListener('click', function() {
      orderBlock.removeChild(clonedProduct);
      product.classList.remove('added-to-order'); 
      updateTotalPrice(); 
    });

    product.classList.add('added-to-order'); 
    updateTotalPrice(); 
  }
}
function updateTotalPrice() {
  var totalPrice = 0;
  var orderProducts = document.querySelectorAll('.card-orders-block .product-item');

  orderProducts.forEach(function(product) {
    var priceString = product.querySelector('p').textContent; 
    var price = parseInt(priceString.match(/\d+/)[0]); 
    totalPrice += price;
  });

  document.getElementById('total-price').textContent = totalPrice + '$';
}
document.addEventListener('DOMContentLoaded', function() {
  
  var deleteButtons = document.querySelectorAll('.card-page-content .card-btn.remove');

  
  deleteButtons.forEach(function(deleteButton) {
    deleteButton.addEventListener('click', function() {
      
      var productItem = deleteButton.closest('.product-item');
      if (productItem) {
        productItem.remove();
      }
    });
  });
});

