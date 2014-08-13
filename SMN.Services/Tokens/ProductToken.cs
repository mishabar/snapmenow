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
        public float MSRP { get; set; }
        [Required]
        [Range(0, 10000)]
        [Display(Name = "Minimum Price")]
        public float MinPrice { get; set; }
        [Required]
        [Range(0, 10000)]
        [Display(Name = "Items Available")]
        public int ItemsAvailable { get; set; }
        [Required]
        [Range(2, 1000)]
        [Display(Name = "Snap Price")]
        public int SnapPrice { get; set; }
        public SaleToken CurrentSale { get; set; }
        public SnapToken MySnap { get; set; }
    }

    public static class ProductTokenExtensions 
    {
        public static ProductToken AsToken(this Product product, string email)
        {
            return new ProductToken
            {
                ID = product.ID,
                Email = product.Email,
                SKU = product.SKU,
                Name = product.Name,
                Description = product.Description.Replace(Environment.NewLine, "<br/>"),
                Images = product.Images,
                MSRP = (float)product.MSRP / (float)100,
                MinPrice = (float)product.MinPrice / (float)100,
                ItemsAvailable = product.ItemsAvailable,
                SnapPrice = product.SnapPrice,
                CurrentSale = product.CurrentSale == null ? null : new SaleToken 
                {
                    CurrentPrice = (float)product.CurrentSale.CurrentPrice / (float)100,
                    Discount = (1 - ((float)product.CurrentSale.CurrentPrice / (float)product.MSRP)) * 100,
                    EndsAt = product.CurrentSale.StartedAt.AddHours(24),
                    Snaps = product.CurrentSale.Snaps.Count
                },
                MySnap = (email == null || product.CurrentSale == null || !product.CurrentSale.Snaps.Any(s => s.UserID == email) ? null : product.CurrentSale.Snaps.First(s => s.UserID == email).AsToken())
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
                MSRP = (int)(token.MSRP * 100),
                MinPrice = (int)(token.MinPrice * 100),
                ItemsAvailable = token.ItemsAvailable,
                SnapPrice = token.SnapPrice
            };
        }
    }
}
