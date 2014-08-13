using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SMN.Data
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string Email { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Images { get; set; }
        public int MSRP { get; set; }
        public int MinPrice { get; set; }
        public int ItemsAvailable { get; set; }
        [BsonIgnoreIfNull]
        public Sale CurrentSale { get; set; }
        public int SnapPrice { get; set; }
        public float Rank { get; set; }

        public Product()
        {
            Images = new string[0];
        }
    }
}
