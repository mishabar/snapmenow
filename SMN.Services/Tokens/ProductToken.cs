using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMN.Data;

namespace SMN.Services.Tokens
{
    public class ProductToken
    {
        public string ID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string[] Images { get; set; }
        [Required]
        [Range(0, 10000)]
        public int MSRP { get; set; }
        [Required]
        [Range(0, 10000)]
        [Display(Name = "Minimum Price")]
        public int MinPrice { get; set; }
        [Required]
        [Range(0, 10000)]
        [Display(Name = "Items Available")]
        public int ItemsAvailable { get; set; }

        public SaleToken CurrentSale { get; set; }
    }

    public static class ProductTokenExtensions 
    {
        public static ProductToken AsToken(this Product product)
        {
            return new ProductToken
            {
                ID = product.ID,
                Email = product.Email,
                SKU = product.SKU,
                Name = product.Name,
                Description = product.Description,
                Images = product.Images,
                MSRP = product.MSRP,
                MinPrice = product.MinPrice,
                ItemsAvailable = product.ItemsAvailable,
                CurrentSale = product.CurrentSale == null ? null : new SaleToken 
                {
                    CurrentPrice = product.CurrentSale.CurrentPrice,
                    Discount = (1 - (product.MSRP / product.CurrentSale.CurrentPrice)) * 100,
                    EndsAt = product.CurrentSale.EndsAt
                }
            };
        }

        public static Product AsProduct(this ProductToken token)
        {
            return new Product
            {
                ID = token.ID,
                Email = token.Email,
                SKU = token.SKU,
                Name = token.Name,
                Description = token.Description,
                Images = token.Images,
                MSRP = token.MSRP,
                MinPrice = token.MinPrice,
                ItemsAvailable = token.ItemsAvailable
            };
        }
    }
}
