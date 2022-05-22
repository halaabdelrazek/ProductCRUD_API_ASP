using AutoMapper;
using ProductBL.DTOs.Product;
using ProductDataAL.Data.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBL.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductReadDTO>();

            CreateMap<ProductReadDTO, Product>();


            CreateMap<ProductWriteDTO, Product>();
            CreateMap<Product, ProductWriteDTO>();



        }
    }
}
