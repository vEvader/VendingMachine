using System.Collections.Generic;
using System.Linq;
using Common.Exceptions;
using Common.VmEntities;
using ProviderInterface;
using RepositoryInterface;

namespace Provider
{
    public class VmProvider : IVmProvider
    {
        private readonly IVmRepository VmRepository;

        public VmProvider(IVmRepository vmRepository)
        {
            VmRepository = vmRepository;
            //Set initial data to data storage
            InitData();
        }
        
        public VmDto GetData()
        {
            return VmRepository.GetData();
        }

        public VmDto EnterCoin(int coinId)
        {
            VmDto data = VmRepository.GetData();
            CoinDto userCoin = data.UserWallet.FirstOrDefault(w => w.CoinId == coinId && w.Count > 0);
            if (userCoin == null)
                throw new CoinNotFoundForEnterException(string.Format("User hasn't such coin (coin id: {0})", coinId));

            CoinDto vmCoin = data.VmWallet.FirstOrDefault(w => w.CoinId == coinId);
            if (vmCoin == null)
                throw new CoinNotFoundForEnterException(string.Format("VM hasn't such coin (coin id: {0})", coinId));

            userCoin.Count--;
            vmCoin.Count++;
            data.EnteredSum += userCoin.Denomination;
            CommitChanges();
            return data;
        }

        public VmDto ReturnRest()
        {
            VmDto data = VmRepository.GetData();
            while (data.EnteredSum > 0)
            {
                //Get possible larjest coin to reduce entered sum
                CoinDto larjestCoin = data.VmWallet.FindAll(c => c.Denomination <= data.EnteredSum && c.Count > 0)
                    .OrderByDescending(c => c.Denomination)
                    .FirstOrDefault();

                if (larjestCoin == null)
                    throw new CoinNotFoundForReturnException(string.Format("VM hasn't necessary coins to return rest money"));

                larjestCoin.Count --;
                data.UserWallet.First(c => c.CoinId == larjestCoin.CoinId).Count++;
                data.EnteredSum -= larjestCoin.Denomination;
            }
            CommitChanges();
            return data;
        }

        public VmDto BuyProduct(int productId)
        {
            VmDto data = VmRepository.GetData();

            ProductDto product = data.Products.FirstOrDefault(w => w.ProductId == productId && w.Count > 0);
            if (product == null)
                throw new ProductNotFoundException(string.Format("Product not found (Product id: {0})", productId));

            if (product.Price > data.EnteredSum)
                throw new NotEnoughMoneyException(string.Format("Not enough money for {0}", product.ProductType));

            data.EnteredSum -= product.Price;
            product.Count--;
            CommitChanges();
            return data;
        }


        #region Private Mehthods

        private void InitData()
        {
            VmDto initData = GetInitData();
            VmRepository.SetData(initData);
        }

        private void CommitChanges()
        {
            //Do nothing because we user class but not ORM or some thing like this
        }

        private static VmDto GetInitData()
        {
            VmDto model = new VmDto
            {
                Currency = Currencies.Rub,
                EnteredSum = 0,
                UserWallet = new List<CoinDto>
                {
                    new CoinDto {CoinId = 1, Denomination = 1, Count = 10},
                    new CoinDto {CoinId = 2, Denomination = 2, Count = 30},
                    new CoinDto {CoinId = 3, Denomination = 5, Count = 20},
                    new CoinDto {CoinId = 4, Denomination = 10, Count = 15},
                },
                VmWallet = new List<CoinDto>
                {
                    new CoinDto {CoinId = 1, Denomination = 1, Count = 100},
                    new CoinDto {CoinId = 2, Denomination = 2, Count = 100},
                    new CoinDto {CoinId = 3, Denomination = 5, Count = 100},
                    new CoinDto {CoinId = 4, Denomination = 10, Count = 100},
                },
                Products = new List<ProductDto>
                {
                    new ProductDto {ProductId = 1, ProductType = ProductTypes.Tea, Price = 13, Count = 10},
                    new ProductDto {ProductId = 2, ProductType = ProductTypes.Coffe, Price = 18, Count = 20},
                    new ProductDto {ProductId = 3, ProductType = ProductTypes.CoffeWithMilk, Price = 21, Count = 20},
                    new ProductDto {ProductId = 4, ProductType = ProductTypes.Juce, Price = 35, Count = 15},
                }
            };

            return model;
        }

        #endregion Private Methods

    }
}
