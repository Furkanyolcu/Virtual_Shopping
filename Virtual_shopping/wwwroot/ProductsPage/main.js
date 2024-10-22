document.addEventListener('DOMContentLoaded', function () {
    const cartButton = document.getElementById('cartButton');
    const cartPanel = document.getElementById('cartPanel');
    const closeCart = document.getElementById('closeCart');
    const cartItems = document.getElementById('cartItems');
    const cartTotal = document.getElementById('cartTotal');
    const cartItemCount = document.getElementById('cartItemCount');
    const addToCartButtons = document.querySelectorAll('.btn');

    let cart = [];

    cartButton.addEventListener('click', () => {
        cartPanel.classList.add('open');
    });

    closeCart.addEventListener('click', () => {
        cartPanel.classList.remove('open');
    });

    addToCartButtons.forEach(button => {
        button.addEventListener('click', (e) => {
            e.preventDefault();
            const product = button.closest('.product');
            const productName = product.querySelector('h3').textContent;
            const productPrice = parseFloat(product.querySelector('.price').textContent.replace('₺', ''));

            addToCart(productName, productPrice);
            updateCartDisplay();
        });
    });

    function addToCart(name, price) {
        const existingItem = cart.find(item => item.name === name);
        if (existingItem) {
            existingItem.quantity++;
        } else {
            cart.push({ name, price, quantity: 1 });
        }
    }

    function updateCartDisplay() {
        cartItems.innerHTML = '';
        let total = 0;

        cart.forEach(item => {
            const itemElement = document.createElement('div');
            itemElement.classList.add('cart-item');
            itemElement.innerHTML = `
                <span>${item.name} x${item.quantity}</span>
                <span>₺${(item.price * item.quantity).toFixed(2)}</span>
            `;
            cartItems.appendChild(itemElement);
            total += item.price * item.quantity;
        });

        cartTotal.textContent = `Toplam: ₺${total.toFixed(2)}`;
        cartItemCount.textContent = cart.reduce((sum, item) => sum + item.quantity, 0);
    }
});