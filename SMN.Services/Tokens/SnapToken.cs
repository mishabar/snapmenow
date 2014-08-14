using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMN.Data;

namespace SMN.Services.Tokens
{
    public class SnapToken
    {
        public string ID { get; set; }
        public string User { get; set; }
        public float Price { get; set; }
        public DateTime SnappedAt { get; set; }
        public string ProductName { get; set; }
        public float FinalPrice { get; set; }
        public string SaleID { get; set; }
        public string Status { get; set; }
        public string ProductID { get; set; }
        public AddressToken Address { get; set; }
    }

    public static class SnapTokenExtensions
    {
        public static SnapToken AsToken(this Snap snap)
        {
            return new SnapToken 
            {
                User = snap.UserID,
                Price = (float)snap.Price / (float)100,
                SnappedAt = snap.SnappedAt,
                FinalPrice = (float)snap.FinalPrice / (float)100
            };
        }

        public static SnapToken AsToken(this UserSnap snap)
        {
            return new SnapToken
            {
                ID = snap.ID,
                User = snap.UserID,
                Price = (float)snap.Price / (float)100,
                SnappedAt = snap.SnappedAt,
                FinalPrice = (float)snap.FinalPrice / (float)100,
                ProductName = snap.ProductName,
                Status = snap.Status,
                ProductID = snap.ProductID,
                Address = snap.Address == null ? new AddressToken() : snap.Address.AsToken()
            };
        }
    }
}
