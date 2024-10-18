document.addEventListener('DOMContentLoaded', () => {
    const productForm = document.getElementById('productForm');
    const productList = document.getElementById('productList');

    productForm.addEventListener('submit', (e) => {
        e.preventDefault();
        addProduct();
    });

    function addProduct() {
        const productName = document.getElementById('productName').value;
        const productDescription = document.getElementById('productDescription').value;
        const productPrice = document.getElementById('productPrice').value;
        const productImageURL = document.getElementById('productImageURL').value;

        const productCard = createProductCard(productName, productDescription, productPrice, productImageURL);
        productList.appendChild(productCard);

        // Form'u temizle
        productForm.reset();
    }

    function createProductCard(name, description, price, imageURL) {
        const card = document.createElement('div');
        card.className = 'product-card';
        card.innerHTML = `
            <img src="${imageURL}" alt="${name}" class="product-image">
            <div class="product-info">
                <h3 class="product-name">${name}</h3>
                <p class="product-description">${description}</p>
                <p class="product-price">${price} TL</p>
                <div class="product-actions">
                    <button class="btn btn-small btn-edit">Düzenle</button>
                    <button class="btn btn-small btn-delete">Sil</button>
                </div>
            </div>
        `;

        // Düzenleme ve silme butonlarına olay dinleyicileri ekle
        card.querySelector('.btn-edit').addEventListener('click', () => editProduct(card));
        card.querySelector('.btn-delete').addEventListener('click', () => deleteProduct(card));

        return card;
    }

    function editProduct(card) {
        // Bu fonksiyon, düzenleme işlemi için controller'a istek gönderecek
        console.log('Ürün düzenleme işlemi başlatıldı');
    }

    function deleteProduct(card) {
        // Bu fonksiyon, silme işlemi için controller'a istek gönderecek
        console.log('Ürün silme işlemi başlatıldı');
    }
});