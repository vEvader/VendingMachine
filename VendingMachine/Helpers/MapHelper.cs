using AutoMapper;
using Common.VmEntities;
using VendingMachine.Models;

namespace VendingMachine.Helpers
{
    public static class MapHelper
    {
        public static VmModel Map(VmDto source)
        {
            VmModel model = Mapper.Map<VmModel>(source);
            
            FillProducNames(model, source);
            FillCurrencyText(model, source);

            return model;
        }

        private static void FillCurrencyText(VmModel model, VmDto source)
        {
            model.CurrencyText = GetCurrencyText(source.Currency);
        }


        private static void FillProducNames(VmModel model, VmDto source)
        {
            foreach (ProductInfo product in model.Products)
            {
                ProductTypes productType = source.Products.Find(p => p.ProductId == product.ProductId).ProductType;
                product.ProductName = GetProductName(productType);
            }
        }

        private static string GetCurrencyText(Currencies currency)
        {
            switch (currency)
            {
                case Currencies.Usd:
                    return Resources.CurrencyResource.UsdCurrencyText;
                case Currencies.Rub:
                    return Resources.CurrencyResource.RubCurrencyText;
                default:
                    return Resources.CurrencyResource.Noname;
            }
        }


        private static string GetProductName(ProductTypes productType)
        {
            switch (productType)
            {
                case ProductTypes.Tea:
                    return Resources.ProductsResource.Tea;
                case ProductTypes.Coffe:
                    return Resources.ProductsResource.Cofee;
                case ProductTypes.CoffeWithMilk:
                    return Resources.ProductsResource.CofeeWithMilk;
                case ProductTypes.Juce:
                    return Resources.ProductsResource.Juce;
                default:
                    return Resources.ProductsResource.Noname;
            }
        }
    }
}