using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachine.Models
{
    public class VmModel
    {
        public List<CoinInfo> UserWallet { get; set; }
        public List<CoinInfo> VmWallet { get; set; }
        public List<ProductInfo> Products { get; set; }
        public int EnteredSum { get; set; }
        public string CurrencyText { get; set; }
    }

    public class CoinInfo
    {
        public int CoinId { get; set; }
        public int Denomination { get; set; }
        public int Count { get; set; }
    }

    public class ProductInfo
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}