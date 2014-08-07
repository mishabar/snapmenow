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
        public string User { get; set; }
        public float Price { get; set; }
        public DateTime SnappedAt { get; set; }
    }

    public static class SnapTokenExtensions
    {
        public static SnapToken AsToken(this Snap snap)
        {
            return new SnapToken 
            {
                User = snap.UserID,
                Price = (float)snap.Price / (float)100,
                SnappedAt = snap.SnappedAt
            };
        }
    }
}
