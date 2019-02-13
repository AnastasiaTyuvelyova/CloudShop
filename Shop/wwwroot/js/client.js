$(document).ready(function() {
    var products = document.querySelectorAll('.goods li');
    var payButton = document.getElementById('payButton');
    var totalSum = 0;
    var currenciesAreEqual;

    products.forEach(function(element) {
        element.addEventListener('click', function() {
            var li = $(this);
            const id = $(li).data('id');
            li.toggleClass('selected');
            if ($(li).hasClass('selected')) {
                $.post("/Products/AddItemToOrder", { itemId: id }).done(() => 
                {
                    $('#totalSumPartialView').load('/Products/TotalSum');
                    $.get("/Products/GetTotalSum", null, data => {
                        totalSum = data;                         disableEnablePayButton(data);                     });
                });
            } 
            else { 
                $.post("/Products/RemoveItemFromOrder", { itemId: id }).done(() => 
                {
                    $('#totalSumPartialView').load('/Products/TotalSum'); 
                    $.get("/Products/GetTotalSum", null, data => {
                        totalSum = data;
                        disableEnablePayButton(data);
                    });
                });
            };
        });
    });

    var pay = function () {
        $('#errorMessage').prop('hidden', true)
        $.get("/Products/CheckCurrency", null, data => { currenciesAreEqual = data }).done(() => {
            if (currenciesAreEqual) {
               var widget = new cp.CloudPayments();
                    widget.charge({ // options
                    publicId: 'test_api_00000000000000000000001',  //id из личного кабинета
                    description: 'Пример оплаты (деньги сниматься не будут)', //назначение
                    amount: totalSum, //сумма
                    currency: 'RUB', //валюта
                    invoiceId: '1234567', //номер заказа  (необязательно)
                    accountId: 'user@example.com', //идентификатор плательщика (необязательно)
                    data: {
                        myProp: 'myProp value' //произвольный набор параметров
                    }
                },
            function (options) { // success
                //действие при успешной оплате
            },
            function (reason, options) { // fail
                //действие при неуспешной оплате
            });
        }
        else {
            $('#errorMessage').prop('hidden', false)
        }
       });
    };

    payButton.addEventListener('click', pay); 

    var disableEnablePayButton = function(totalSum) {
        totalSum == 0 ? $('#payButton').prop('disabled', true) : $('#payButton').prop('disabled', false);
    };

});