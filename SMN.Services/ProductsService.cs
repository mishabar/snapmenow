using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMN.Data;
using SMN.Data.Repositories;
using SMN.Services.Tokens;

namespace SMN.Services
{
    public class ProductsService : IProductsService
    {
        private IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public IEnumerable<Tokens.ProductToken> GetAllForRetailer(string retailerEmail)
        {
            return _productsRepository.FindAllForRetailer(retailerEmail).Select(p => p.AsToken(null));
        }

        public void CreateProduct(ProductToken token, System.Web.HttpFileCollectionBase files)
        {
            Product product = _productsRepository.Create(token.AsProduct());
        }

        public ProductToken Get(string id)
        {
            return _productsRepository.Get(id).AsToken(null);
        }
    }
}
