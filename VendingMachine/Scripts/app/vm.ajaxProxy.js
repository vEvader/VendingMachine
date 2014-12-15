vm.ajaxProxy = function () {

    var sendAjaxRequest = function (url, jsonRequest, onSuccessCallback, onFailureCallback, onComplete) {


        var opts = {lines: 9, length: 0, width: 15, radius: 30, corners: 1, rotate: 30, direction: 1, color: '#000', speed: 0.8, 
            trail: 60, shadow: false, hwaccel: false, className: 'spinner', zIndex: 2e9, top: '50%', left: '50%' 
        };

        var target = document.getElementById('body');
        var spinner = new Spinner(opts);
        spinner.spin(target);

        var onFailure = function (data) {
            if (data.status == 200) {
                window.location.href = vm.urls.startPage;
            } else {
                var response = {
                    Message: vm.messages.technicalError
                };
                if (onFailureCallback) {
                    onFailureCallback(response);
                } else {
                    alert(vm.messages.technicalError);
                }
            }
        };

        var onSuccess = function (response) {
            if (response && response.Status === true && onSuccessCallback) {
                onSuccessCallback(response);
            }
            else {
                alert(response.Message);
            }
        };

        $.ajax({
            url: url,
            type: 'POST',
            data: jsonRequest,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: onSuccess,
            error: onFailure,
            complete: onComplete
        }).always(function () {
            spinner.stop();
        });

    };

    return {
        returnRest: function (onSuccess, onFailure, onComplete) {
            sendAjaxRequest(vm.urls.returnRest, null, onSuccess, onFailure, onComplete);
        },
        enterCoin: function (jsonRequest, onSuccess, onFailure, onComplete) {
            sendAjaxRequest(vm.urls.enterCoin, jsonRequest, onSuccess, onFailure, onComplete);
        },
        buyProduct: function (jsonRequest, onSuccess, onFailure, onComplete) {
            sendAjaxRequest(vm.urls.buyProduct, jsonRequest, onSuccess, onFailure, onComplete);
        },
        
        initModel: function (onSuccess, onFailure, onComplete) {
            sendAjaxRequest(vm.urls.initModel, null, onSuccess, onFailure, onComplete);
        },
    };
}();
