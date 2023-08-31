using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECommerceSystem;

namespace ECommerceTests
{
    [TestClass]
    public class ShoppingCartTests
    {
        [TestMethod]
        public void AddItem_ValidProduct_AddsToCart()
        {
            // Arrange
            ShoppingCart cart = new ShoppingCart();
            Product product = new Product { Name = "Sample Product", Price = 10.0M };

            // Act
            cart.AddItem(product, 2);

            // Assert
            Assert.AreEqual(1, cart.GetItemCount());
        }

        [TestMethod]
        public void AddItem_DuplicateProduct_IncrementsQuantity()
        {
            // Arrange
            ShoppingCart cart = new ShoppingCart();
            Product product = new Product { Name = "Sample Product", Price = 10.0M };

            // Act
            cart.AddItem(product, 2);
            cart.AddItem(product, 3);

            // Assert
            Assert.AreEqual(1, cart.GetItemCount());
            Assert.AreEqual(5, cart.GetItems()[0].Quantity);
        }

        [TestMethod]
        public void RemoveItem_ValidProduct_RemovesFromCart()
        {
            // Arrange
            ShoppingCart cart = new ShoppingCart();
            Product product = new Product { Name = "Sample Product", Price = 10.0M };
            cart.AddItem(product, 2);

            // Act
            cart.RemoveItem(product);

            // Assert
            Assert.AreEqual(0, cart.GetItemCount());
        }

        [TestMethod]
        public void CalculateTotal_NoItems_ReturnsZero()
        {
            // Arrange
            ShoppingCart cart = new ShoppingCart();

            // Act
            decimal total = cart.CalculateTotal();

            // Assert
            Assert.AreEqual(0.0M, total);
        }

        [TestMethod]
        public void CalculateTotal_MultipleItems_CalculatesCorrectTotal()
        {
            // Arrange
            ShoppingCart cart = new ShoppingCart();
            Product product1 = new Product { Name = "Product 1", Price = 10.0M };
            Product product2 = new Product { Name = "Product 2", Price = 20.0M };
            cart.AddItem(product1, 2);
            cart.AddItem(product2, 1);

            // Act
            decimal total = cart.CalculateTotal();

            // Assert
            Assert.AreEqual(40.0M, total);
        }

        [TestMethod]
        public void CalculateTotal_DiscountApplied_CalculatesCorrectTotal()
        {
            // Arrange
            ShoppingCart cart = new ShoppingCart();
            Product product = new Product { Name = "Discounted Product", Price = 100.0M };
            cart.AddItem(product, 2);

            // Apply a 20% discount
            decimal expectedTotal = 2 * product.Price * 0.8M;

            // Act
            decimal total = cart.CalculateTotal(true);

            // Assert
            Assert.AreEqual(expectedTotal, total, "Discount not applied correctly.");
        }

        [TestMethod]
        public void GetItemCount_AddedItems_ReturnsCorrectCount()
        {
            // Arrange
            ShoppingCart cart = new ShoppingCart();
            Product product1 = new Product { Name = "Product 1", Price = 10.0M };
            Product product2 = new Product { Name = "Product 2", Price = 20.0M };
            cart.AddItem(product1, 3);
            cart.AddItem(product2, 2);

            // Act
            int itemCount = cart.GetItemCount();

            // Assert
            Assert.IsTrue(itemCount == 5, $"Expected item count: 5. Actual: {itemCount}");
        }

        [TestMethod]
        public void AddItem_InvalidQuantity_ThrowsArgumentException()
        {
            // Arrange
            ShoppingCart cart = new ShoppingCart();
            Product product = new Product { Name = "Sample Product", Price = 10.0M };

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => cart.AddItem(product, -1));
        }
    }
}
