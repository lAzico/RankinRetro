using RankinRetro.Data;
using RankinRetro.Interfaces;
using RankinRetro.Models;

namespace RankinRetro.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ApplicationDbContext _context;


        public SearchRepository(ApplicationDbContext context)
        {
            _context = context;

        }


        public List<Product> SearchProduct(List<int> types, List<int> brands, List<int> categories)
        {
            //Make queryable to allow filtering of products
            IQueryable<Product> productsQuery =  _context.Products;
            List<Product> products = new List<Product>();

            if (types != null && types.Any())
            {
                //Get the type Ids from the types List passed in the parameter
                IEnumerable<int> selectedTypeIds = types.Select(t => t);

                //Find the products which contains the types
                productsQuery = productsQuery.Where(p => selectedTypeIds.Contains(p.TypeId));

                //Iterate over the products queried and add it to a list of products
                foreach (var product in productsQuery)
                {
                    products.Add(product);
                }
            }

            if (brands != null && brands.Any())
            {

                IEnumerable<int> selectedBrandIds = brands.Select(b => b);

                productsQuery = productsQuery.Where(p => selectedBrandIds.Contains(p.BrandId));
                foreach (var product in productsQuery)
                {
                    products.Add(product);
                }
            }

            if (categories != null && categories.Any())
            {

                IEnumerable<int> selectedCategoryIds = categories.Select(b => b);

                productsQuery = productsQuery.Where(p => selectedCategoryIds.Contains(p.CategoryId));
                foreach(var product in productsQuery)
                {
                    products.Add(product);
                }
                
            }

            return products;

        }
    }
}
