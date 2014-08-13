﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SMN.Services
{
    public interface IProductsService
    {
        IEnumerable<Tokens.ProductToken> GetAllForRetailer(string retailerEmail);

        void Create(Tokens.ProductToken token, HttpFileCollectionBase files);

        Tokens.ProductToken Get(string id);

        void Update(Tokens.ProductToken token);
    }
}
