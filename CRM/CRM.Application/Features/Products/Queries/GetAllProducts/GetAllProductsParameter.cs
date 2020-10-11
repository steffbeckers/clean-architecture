using CRM.Application.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsParameter : RequestParameter
    {
        public string Name { get; set; }
    }
}
