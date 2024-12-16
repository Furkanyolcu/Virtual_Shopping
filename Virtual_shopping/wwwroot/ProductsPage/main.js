document.addEventListener('DOMContentLoaded', function () {
    const cartButton = document.getElementById('cartButton');
    const cartPanel = document.getElementById('cartPanel');
    const closeCart = document.getElementById('closeCart');
    const cartItems = document.getElementById('cartItems');
    const cartTotal = document.getElementById('cartTotal');
    const cartItemCount = document.getElementById('cartItemCount');
    const addToCartButtons = document.querySelectorAll('.add-to-cart');

    let cart = []; // Sepet verilerini tutan dizi

    // Sepet paneli dışına tıklanınca kapanmasını sağlar
    document.addEventListener('click', (e) => {
        if (!cartPanel.contains(e.target) && !cartButton.contains(e.target)) {
            cartPanel.classList.remove('open');
        }
    });

    // Sepet paneli dışında bir yere tıklanırsa panelin kapanması engellenir
    cartPanel.addEventListener('click', (e) => {
        e.stopPropagation(); // Sepet paneline tıklanmasını engelle
    });

    // Sepet panelini aç
    cartButton.addEventListener('click', () => {
        cartPanel.classList.add('open');
    });

    // Sepet panelini kapat
    closeCart.addEventListener('click', () => {
        cartPanel.classList.remove('open');
    });

    // Ürün ekleme butonları
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

    // Sepete ürün ekleme
    function addToCart(name, price) {
        const existingItem = cart.find(item => item.name === name);
        if (existingItem) {
            existingItem.quantity++;
        } else {
            cart.push({ name, price, quantity: 1 });
        }
    }

    // Sepetten ürün kaldırma
    function removeFromCart(name) {
        const existingItem = cart.find(item => item.name === name);
        if (existingItem) {
            existingItem.quantity--;
            if (existingItem.quantity <= 0) {
                cart = cart.filter(item => item.name !== name); // Ürünü tamamen kaldır
            }
        }
    }

    // Sepeti güncelleme
    function updateCartDisplay() {
        cartItems.innerHTML = ''; // Sepet içeriğini sıfırla
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

            // Silme butonuna olay ekle
            const removeButton = itemElement.querySelector('.remove-cart-btn');
            removeButton.addEventListener('click', (e) => {
                e.stopPropagation(); // Silme butonunun paneli kapatmasını engelle
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

    // Favoriler paneli dışına tıklanınca kapanmasını sağlar
    document.addEventListener('click', (e) => {
        if (!favPanel.contains(e.target) && !favButton.contains(e.target)) {
            favPanel.classList.remove('open');
        }
    });

    // Favoriler paneli dışında bir yere tıklanırsa panelin kapanması engellenir
    favPanel.addEventListener('click', (e) => {
        e.stopPropagation(); // Favori paneline tıklanmasını engelle
    });

    // Favoriler panelini aç
    favButton.addEventListener('click', () => {
        favPanel.classList.add('open');
    });

    // Favoriler panelini kapat
    closeFav.addEventListener('click', () => {
        favPanel.classList.remove('open');
    });

    // Ürünleri favorilere ekle
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

    // Favorilere ekleme fonksiyonu
    function addToFav(name, price) {
        const existingItem = fav.find(item => item.name === name);
        if (!existingItem) {
            fav.push({ name, price }); // Favorilere yeni ürün ekle
        }
    }

    // Favoriler panelini güncelleme fonksiyonu
    function updateFavDisplay() {
        favItems.innerHTML = ''; // Favoriler panelini temizle

        fav.forEach((item, index) => {
            const itemElement = document.createElement('div');
            itemElement.classList.add('fav-item');
            itemElement.innerHTML = `
                <span>${item.name}</span>
                <button class="remove-fav-btn" data-index="${index}">🗑️</button>
            `;
            favItems.appendChild(itemElement);
        });

        // Çöp tenekesi butonlarına tıklama işlevi ekle
        const removeButtons = document.querySelectorAll('.remove-fav-btn');
        removeButtons.forEach(button => {
            button.addEventListener('click', (e) => {
                e.stopPropagation(); // Favori butonunun tıklanmasını engelle, panelin kapanmasını önler
                const itemIndex = parseInt(button.getAttribute('data-index'), 10);
                removeFromFav(itemIndex); // Ürünü favorilerden kaldır
            });
        });
    }

    // Favorilerden ürün kaldırma fonksiyonu
    function removeFromFav(index) {
        fav.splice(index, 1); // Belirtilen ürünü favoriler listesinden sil
        updateFavDisplay();  // Favoriler panelini yeniden güncelle
    }
});
