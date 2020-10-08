using CRM.Application.Features.Products.Commands.CreateProduct;
using CRM.Application.Features.Products.Queries.GetAllProducts;
using AutoMapper;
using CRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}
