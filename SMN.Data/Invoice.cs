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
    public class Invoice
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string UserSnapID { get; set; }
        public string ProductID { get; set; }
        public Address ShippingAddress { get; set; }
        public Address BillingAddress { get; set; }
    }
}
