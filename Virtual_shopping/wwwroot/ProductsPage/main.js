// Sample product data
const products = [
    { id: 1, name: "Elegant Watch", price: 199.99, image: "https://images.unsplash.com/photo-1523275335684-37898b6baf30" },
    { id: 2, name: "Stylish Sunglasses", price: 89.99, image: "https://images.unsplash.com/photo-1572635196237-14b3f281503f" },
    { id: 3, name: "Leather Wallet", price: 59.99, image: "https://images.unsplash.com/photo-1627123424574-724758594e93" },
    { id: 4, name: "Wireless Earbuds", price: 129.99, image: "https://images.unsplash.com/photo-1590658268037-6bf12165a8df" },
    { id: 5, name: "Smart Fitness Tracker", price: 79.99, image: "https://images.unsplash.com/photo-1575311373937-040b8e1fd5b6" },
    { id: 6, name: "Designer Backpack", price: 129.99, image: "https://images.unsplash.com/photo-1553062407-98eeb64c6a62" },
    { id: 7, name: "Wireless Headphones", price: 159.99, image: "https://images.unsplash.com/photo-1505740420928-5e560c06d30e" },
    { id: 8, name: "Smart Home Speaker", price: 89.99, image: "https://images.unsplash.com/photo-1543512214-318c7553f230" }
];

let cart = [];

const cartItemsContainer = document.querySelector(".cart-items");
const cartTotal = document.querySelector(".cart-total");

function toggleCart() {
    document.getElementById("cart").classList.toggle("open");
}

function addToCart(productId) {
    const product = products.find(p => p.id === productId);
    cart.push(product);
    updateCart();
}

function updateCart() {
    cartItemsContainer.innerHTML = "";
    let total = 0;
    cart.forEach((item, index) => {
        const cartItem = document.createElement("li");
        cartItem.innerHTML = `
                            <span>${item.name}</span>
                            <span>$${item.price.toFixed(2)}</span>
                            <button class="cart-remove" onclick="removeFromCart(${index})">Remove</button>
                        `;
        cartItemsContainer.appendChild(cartItem);
        total += item.price;
    });
    cartTotal.innerText = Total: $${ total.toFixed(2) };
}

function removeFromCart(index) {
    cart.splice(index, 1);
    updateCart();
}

function displayProducts() {
    const productsContainer = document.getElementById("products");
    products.forEach(product => {
        const productCard = document.createElement("div");
        productCard.classList.add("product-card");
        productCard.innerHTML = `
                            <div class="product-image">
                                <img src="${product.image}" alt="${product.name}">
                            </div>
                            <div class="product-info">
                                <h3 class="product-title">${product.name}</h3>
                                <p class="product-price">$${product.price.toFixed(2)}</p>
                                <button class="add-to-cart" onclick="addToCart(${product.id})">Add to Cart</button>
                            </div>
                        `;
        productsContainer.appendChild(productCard);
    });
}

// Display products when page loads
displayProducts();
    </script >
