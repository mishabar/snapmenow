using System;
using System.Collections.Generic;
using System.Configuration;
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
        private IImageRepository _imagesRepository;

        public ProductsService(IProductsRepository productsRepository, IImageRepository imagesRepository)
        {
            _productsRepository = productsRepository;
            _imagesRepository = imagesRepository;
        }

        public IEnumerable<Tokens.ProductToken> GetAllForRetailer(string retailerEmail)
        {
            return _productsRepository.FindAllForRetailer(retailerEmail).Select(p => p.AsToken(null));
        }

        public string Create(ProductToken token)
        {
            Product product = _productsRepository.Create(token.AsProduct());
            return product.ID;
        }

        public ProductToken Get(string id)
        {
            return _productsRepository.Get(id).AsToken(null);
        }

        public void Update(ProductToken token, string email)
        {
            _productsRepository.Update(token.AsProduct(), email);
        }


        public void UpdateImage(string id, System.Web.HttpPostedFileBase image, int i)
        {
            string key = string.Format("{0}-{1}", id, i);
            // delete existing image
            //_imagesRepository.DeleteImage(key);
            _imagesRepository.UploadImage(image, key);
            string url = string.Format("https://s3-ap-southeast-2.amazonaws.com/{0}/images/{1}", ConfigurationManager.AppSettings["AWS:ImagesBucketName"], key);
            _productsRepository.UpdateImage(id, i, url);
        }


        public void Delete(string id, string email)
        {
            string[] images = _productsRepository.Delete(id, email);
            foreach (var image in images)
            {
                if (!string.IsNullOrEmpty(image))
                {
                    _imagesRepository.DeleteImage(image.Substring(image.IndexOf("/images/")));
                }
            }
        }


        public void ScheduleSale(string id, DateTime start_on, bool start_now, int duration, int items, string email)
        {
            _productsRepository.ScheduleSale(id, start_on, start_now, duration, items, email);
        }
    }
}
