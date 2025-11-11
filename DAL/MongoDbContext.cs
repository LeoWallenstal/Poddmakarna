using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// This goes into all of the repositories, handling the connection to the MongoDB.
    /// </summary>
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoClient _client;

        public MongoDbContext()
        {
            string connectionString = File.ReadAllText("connectionString.txt");
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("OruMongoDB");
        }

        /// <summary>
        /// To let the repositories fetch the relevant collection through .GetCollection<T>(string collectionName).
        /// </summary>
        public IMongoDatabase Database
        {
            get { return _database; }
        }

        /// <summary>
        ///  Exposes the client here, to let the repositories .GetSession() to do transactions in CRUD-operations.
        /// </summary>
        public MongoClient Client{
            get { return _client; }
        }
    }
}
