using RankinRetro.Data;
using RankinRetro.Interfaces;
using RankinRetro.Models;
using System.Linq;

namespace RankinRetro.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ApplicationDbContext _context;


        public SearchRepository(ApplicationDbContext context)
        {
            _context = context;

        }


        public List<Product> SearchProduct(List<string> types, List<string> brands, List<string> categories)
        {
            //Make queryable to allow filtering of products
            IQueryable<Product> productsQuery =  _context.Products;
            List<Product> products = new List<Product>();

            if (types != null && types.Any())
            {
                //Get the type Ids from the types List passed in the parameter
                IEnumerable<string> selectedTypeIds = types.Select(t => t);
                List<string> selectedTypeIdsList = selectedTypeIds.ToList();

                //Find the products which contains the types
                productsQuery = productsQuery.Where(p => selectedTypeIdsList.Contains(p.TypeId.ToString()));

                //Iterate over the products queried and add it to a list of products
                foreach (var product in productsQuery)
                {
                    products.Add(product);
                }
            }

            if (brands != null && brands.Any())
            {

                IEnumerable<string> selectedBrandIds = brands.Select(b => b);
                List<string> selectedBrandIdsList = selectedBrandIds.ToList();

                productsQuery = productsQuery.Where(p => selectedBrandIdsList.Contains(p.BrandId.ToString()));
                foreach (var product in productsQuery)
                {
                    products.Add(product);
                }
            }

            if (categories != null && categories.Any())
            {

                IEnumerable<string> selectedCategoryIds = categories.Select(b => b);
                List<string> selectedCategoryIdsList = selectedCategoryIds.ToList();

                productsQuery = productsQuery.Where(p => selectedCategoryIdsList.Contains(p.CategoryId.ToString()));
                foreach(var product in productsQuery)
                {
                    products.Add(product);
                }
                
            }

            return products;

        }
    }
}
