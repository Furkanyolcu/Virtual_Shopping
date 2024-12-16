document.addEventListener('DOMContentLoaded', function () {
    const cardNumber = document.getElementById('cardNumber');
    const cardHolder = document.getElementById('cardHolder');
    const cardExpiry = document.getElementById('cardExpiry');
    const cardCvv = document.getElementById('cardCvv');
    const cardContainer = document.querySelector('.card-container');

    const cardNumberInput = document.getElementById('cardNumberInput');
    const cardHolderInput = document.getElementById('cardHolderInput');
    const cardExpiryInput = document.getElementById('cardExpiryInput');
    const cardCvvInput = document.getElementById('cardCvvInput');

    cardNumberInput.addEventListener('input', function (e) {
        let value = e.target.value.replace(/\s/g, '');
        let formattedValue = '';
        for (let i = 0; i < value.length; i++) {
            if (i > 0 && i % 4 === 0) {
                formattedValue += ' ';
            }
            formattedValue += value[i];
        }
        e.target.value = formattedValue;
        cardNumber.textContent = formattedValue || '**** **** **** ****';
    });

    cardHolderInput.addEventListener('input', function (e) {
        cardHolder.textContent = e.target.value.toUpperCase() || 'AD SOYAD';
    });

    cardExpiryInput.addEventListener('input', function (e) {
        let value = e.target.value.replace(/\D/g, '');
        if (value.length > 2) {
            value = value.slice(0, 2) + '/' + value.slice(2);
        }
        e.target.value = value;
        cardExpiry.textContent = value || 'MM/YY';
    });

    cardCvvInput.addEventListener('focus', function () {
        cardContainer.style.transform = 'rotateY(180deg)';
    });

    cardCvvInput.addEventListener('blur', function () {
        cardContainer.style.transform = 'rotateY(0)';
    });

    cardCvvInput.addEventListener('input', function (e) {
        cardCvv.textContent = e.target.value || '***';
    });

    document.getElementById('paymentForm').addEventListener('submit', function (e) {
        e.preventDefault();
        alert('Ödeme işlemi simüle edildi. Gerçek bir ödeme yapılmadı.');
    });
});