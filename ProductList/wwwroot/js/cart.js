
function addToCart(productId) {
    // localStorage'da mevcut sepet verilerini al
    let cart = JSON.parse(localStorage.getItem('cart')) || [];

    // Sepete eklemek istedi�iniz �r�n� bulun
    let product = findProductById(productId);


    // �r�n� sepete ekleyin
    if (product) {
        cart.push(product);
    }
   
    // Sepet verilerini localStorage'a geri kaydedin
    localStorage.setItem('cart', JSON.stringify(cart));
}


function findProductById(productId) {
    var element = $(`#${productId}`);
    console.log(element.data('imageUrl'));
    return {
        Id: productId,
        Name: element.data('name'),
        Description: element.data('description'),
        Price: parseFloat(element.data('price').replace(",",".")),
        ImageURL: element.data('image')
    };
}


//sepetteki �r�n say�s�n� g�sterme
function updateCartCount() {
    var cart = JSON.parse(localStorage.getItem('cart')) || [];
    var itemCount = cart.length;

    // "cart-count" ��esini g�ncelleyin.
    var cartCount = document.getElementById("cart-count");
    if (cartCount) {
        cartCount.innerHTML = itemCount;
    }
}

//function updateCartCount() {
//    $.ajax({
//        url: 'Home/Cart',
//        method: 'GET',
//        success: function (data) {
//            var itemCount = data.cart.length;
//            var cartCount = document.getElementById("cart-count");
//            console.log( data);
//            if (cartCount) {
//                cartCount.innerHTML = itemCount;
//            }
//        },
//        error: function (error) {
//            console.error("Hata olu�tu: " + error);
//        }
//    });
//}


document.addEventListener("DOMContentLoaded", function () {
    // Sayfa y�klendi�inde veya sepet g�ncellendi�inde sepet ikonunu g�ncelle
    updateCartCount();
});
