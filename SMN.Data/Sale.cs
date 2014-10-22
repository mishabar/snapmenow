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
    public class Sale
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public DateTime StartedAt { get; set; }
        public int Items { get; set; }
        public DateTime? EndedAt { get; set; }
        public string ProductID { get; set; }
        public IList<Snap> Snaps { get; set; }
        public int CurrentPrice { get; set; }

        public Sale()
        {
            Snaps = new List<Snap>();
        }
    }
}
