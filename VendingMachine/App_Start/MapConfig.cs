using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Common.VmEntities;
using VendingMachine.Models;

namespace VendingMachine
{
    public class MapConfig
    {
        public static void RegisterMapping()
        {
            Mapper.CreateMap<VmDto, VmModel>();
            Mapper.CreateMap<CoinDto, CoinInfo>();
            Mapper.CreateMap<ProductDto, ProductInfo>();
        }
    }
}