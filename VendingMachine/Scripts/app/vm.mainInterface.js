vm.mainInterface = {
    viewModel: {},
    init: function() {
        var self = vm.mainInterface;
        self.viewModel = {
            userWallet: ko.observableArray(),
            enteredSum: ko.observable(),
            vmWallet: ko.observableArray(),
            products: ko.observableArray(),
            currencyText: ko.observable(),    

            enterCoin: function(data) {
                vm.ajaxProxy.enterCoin(ko.toJSON(data), self.viewModel.updateViewModel);
            },
            returnRest: function () {
                vm.ajaxProxy.returnRest(self.viewModel.updateViewModel);
            },
            buyProduct: function (data) {
                vm.ajaxProxy.buyProduct(ko.toJSON(data), self.viewModel.buyingSuccess);
            },

            buyProduct2: function (data) {
                if (data.price() > self.viewModel.enteredSum()) {
                    alert(vm.messages.notEnoughMoney);
                } else {
                    vm.ajaxProxy.buyProduct(ko.toJSON(data), self.viewModel.buyingSuccess);
                }
            },

            //fill knockout view model with source data
            fillViewModel: function (source) {
                var viewModel = self.viewModel;
                viewModel.userWallet.removeAll();
                source.UserWallet.forEach(function outputItem(item, i, arr) {
                    var walletItem = viewModel.createWalletItem(item);
                    viewModel.userWallet.push(walletItem);
                });
                
                viewModel.vmWallet.removeAll();
                source.VmWallet.forEach(function outputItem(item, i, arr) {
                    var walletItem = viewModel.createWalletItem(item);
                    viewModel.vmWallet.push(walletItem);
                });

                viewModel.products.removeAll();
                source.Products.forEach(function outputItem(item, i, arr) {
                    var productItem = viewModel.createProductItem(item);
                    viewModel.products.push(productItem);
                });
                viewModel.enteredSum(source.EnteredSum);
                viewModel.currencyText(source.CurrencyText);
            },

            //return knockout wallet item
            createWalletItem: function (source) {
                var item = {
                    coinId: ko.observable(source.CoinId),
                    denomination: ko.observable(source.Denomination),
                    count: ko.observable(source.Count)
                };
                return item;
            },
            
            //return knockout product item
            createProductItem: function (source) {
                var item = {
                    productId: ko.observable(source.ProductId),
                    productName: ko.observable(source.ProductName),
                    price: ko.observable(source.Price),
                    count: ko.observable(source.Count)
                };
                return item;
            },
        };

        self.viewModel.updateViewModel = function (data) {
            self.viewModel.fillViewModel(data.Content);
        };

        self.viewModel.buyingSuccess = function (data) {
            alert(vm.messages.buyingSuccess);
            self.viewModel.updateViewModel(data);
        };

        vm.ajaxProxy.initModel(self.viewModel.updateViewModel);
        ko.applyBindings(self.viewModel, document.getElementById('VmInterface'));
    },
};
$(vm.mainInterface.init);
