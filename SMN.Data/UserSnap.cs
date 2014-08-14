using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace SMN.Data
{
    public class UserSnap : Snap
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string ProductName { get; set; }
        public string SaleID { get; set; }
        public string Status { get; set; }
        public Address Address { get; set; }

        public UserSnap(Snap snap, string productName, string saleID, string status = "Snapped")
        {
            ProductID = snap.ProductID;
            UserID = snap.UserID;
            Price = snap.Price;
            SnappedAt = snap.SnappedAt;
            FinalPrice = snap.FinalPrice;
            ProductName = productName;
            SaleID = saleID;
            Status = status;
        }
    }
}
