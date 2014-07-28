using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace SMN.Web.App_Start
{
    public class ApplicationDbContext : IDisposable
    {
        public ApplicationDbContext()
        {
            MongoClient client = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString);
            Database =  client.GetServer().GetDatabase(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString.Split('/').Last());
        }

        public MongoDatabase Database { get; private set; }

        public void Dispose()
        {
        }
    }
}
