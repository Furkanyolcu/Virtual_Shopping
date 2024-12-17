document.addEventListener('DOMContentLoaded', function () {
    const cartButton = document.getElementById('cartButton');
    const cartPanel = document.getElementById('cartPanel');
    const closeCart = document.getElementById('closeCart');
    const cartItems = document.getElementById('cartItems');
    const cartTotal = document.getElementById('cartTotal');
    const cartItemCount = document.getElementById('cartItemCount');
    const addToCartButtons = document.querySelectorAll('.add-to-cart');

    let cart = []; 

    
    document.addEventListener('click', (e) => {
        if (!cartPanel.contains(e.target) && !cartButton.contains(e.target)) {
            cartPanel.classList.remove('open');
        }
    });

   
    cartPanel.addEventListener('click', (e) => {
        e.stopPropagation(); 
    });

   
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

    function removeFromCart(name) {
        const existingItem = cart.find(item => item.name === name);
        if (existingItem) {
            existingItem.quantity--;
            if (existingItem.quantity <= 0) {
                cart = cart.filter(item => item.name !== name); 
            }
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
                <button class="remove-cart-btn">
                    <i class="fa fa-trash"></i>
                </button>
            `;
            cartItems.appendChild(itemElement);
            total += item.price * item.quantity;

            
            const removeButton = itemElement.querySelector('.remove-cart-btn');
            removeButton.addEventListener('click', (e) => {
                e.stopPropagation(); 
                removeFromCart(item.name);
                updateCartDisplay();
            });
        });

        cartTotal.textContent = `Toplam: ₺${total.toFixed(2)}`;
        cartItemCount.textContent = cart.reduce((sum, item) => sum + item.quantity, 0);
    }
});

document.addEventListener('DOMContentLoaded', function () {
    const favButton = document.getElementById('favButton');
    const favPanel = document.getElementById('favPanel');
    const closeFav = document.getElementById('closeFav');
    const favItems = document.getElementById('favItems');
    const addToFavButtons = document.querySelectorAll('.btn');

    let fav = [];

    
    document.addEventListener('click', (e) => {
        if (!favPanel.contains(e.target) && !favButton.contains(e.target)) {
            favPanel.classList.remove('open');
        }
    });
    favPanel.addEventListener('click', (e) => {
        e.stopPropagation(); 
    });
    favButton.addEventListener('click', () => {
        favPanel.classList.add('open');
    });
    closeFav.addEventListener('click', () => {
        favPanel.classList.remove('open');
    });
    addToFavButtons.forEach(button => {
        button.addEventListener('click', (e) => {
            e.preventDefault();
            const product = button.closest('.product');
            const productName = product.querySelector('h3').textContent;
            const productPrice = parseFloat(product.querySelector('.price').textContent.replace('₺', ''));

            addToFav(productName, productPrice);
            updateFavDisplay();
        });
    });
    function addToFav(name, price) {
        const existingItem = fav.find(item => item.name === name);
        if (!existingItem) {
            fav.push({ name, price }); 
        }
    }
    function updateFavDisplay() {
        favItems.innerHTML = ''; 

        fav.forEach((item, index) => {
            const itemElement = document.createElement('div');
            itemElement.classList.add('fav-item');
            itemElement.innerHTML = `
                <span>${item.name}</span>
                <button class="remove-fav-btn" data-index="${index}">🗑️</button>
            `;
            favItems.appendChild(itemElement);
        });
        const removeButtons = document.querySelectorAll('.remove-fav-btn');
        removeButtons.forEach(button => {
            button.addEventListener('click', (e) => {
                e.stopPropagation(); 
                const itemIndex = parseInt(button.getAttribute('data-index'), 10);
                removeFromFav(itemIndex); 
            });
        });
    }
    function removeFromFav(index) {
        fav.splice(index, 1); 
        updateFavDisplay();  
    }
});
