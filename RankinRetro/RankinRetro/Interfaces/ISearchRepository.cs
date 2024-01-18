﻿using RankinRetro.Models;

namespace RankinRetro.Interfaces
{
    public interface ISearchRepository
    {
        List<Product> SearchProduct(List<Types> types, List<Brand> brands, List<Category> categories); 

    }
}
