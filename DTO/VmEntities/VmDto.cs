using System.Collections.Generic;

namespace Common.VmEntities
{
    public class VmDto
    {
        public List<CoinDto> UserWallet { get; set; }
        public List<CoinDto> VmWallet { get; set; }
        public List<ProductDto> Products { get; set; }
        public int EnteredSum { get; set; }
        public Currencies Currency { get; set; }
    }
}
