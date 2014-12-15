using System;
using Common.VmEntities;

namespace ProviderInterface
{
    public interface IVmProvider
    {

        //Get data
        VmDto GetData();

        //Enter coin from User wallet to Vm Wallet
        VmDto EnterCoin(int coinId);
        
        //return rest money from WmWallet to User Wallet
        VmDto ReturnRest();

        //Buy some product
        VmDto BuyProduct(int productId);
    }
}
