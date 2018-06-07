using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDA
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoClient mongoClient, string databaseName, string usersTableName)
        {
            IMongoDatabase db = mongoClient.GetDatabase(databaseName);
            _users = db.GetCollection<User>(usersTableName);
        }

        public void Insert(User user)
        {
            _users.InsertOne(user);
        }

        public IQueryable<User> Get()
        {
            return _users.AsQueryable();
        }

        public User GetById(Guid id)
        {
            return _users.AsQueryable().FirstOrDefault(x => x.UserId == id);
        }

        public void Update(User user)
        {
            var filter = Builders<User>.Filter.Eq(x => x.UserId, user.UserId);
            var updateAddress = Builders<User>.Update.Set(x => x.Address, user.Address);
            var updateLocation = Builders<User>.Update.Set(x => x.Location, user.Location);
            _users.UpdateOneAsync(filter, updateAddress);
            _users.UpdateOneAsync(filter, updateLocation);
        }

        public void Delete(Guid id)
        {
            var filter = Builders<User>.Filter.Eq(x => x.UserId, id);
            _users.DeleteOne(filter);
        }

        public void DeleteAll()
        {
            var filter = Builders<User>.Filter.Where(x => true);
            _users.DeleteMany(filter);
        }
    }
}
