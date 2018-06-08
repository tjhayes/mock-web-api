using MongoDA;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Security.Authentication;

namespace DeserializeJSON
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString =
              @"mongodb://cameron-wags:rp7KMfeoIp0KgM7dMMpnZDF9Cmtde0PIlQAQ9pdrpZZaZdO9Pqt9mk8VXl3upDpp2pyrzajfNvOm2JZtqfOzkQ==@cameron-wags.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            IMongoClient mongoClient = new MongoClient(settings);
            IMongoDatabase db = mongoClient.GetDatabase("userdb");
            IMongoCollection<User> userCollection = db.GetCollection<User>("users");

            string jsonStr = System.IO.File.ReadAllText(@"C:\Users\tjhay\MockUsers.json");
            List<User> users = Deserialize<List<User>>(jsonStr);

            //UserRepository repo = new UserRepository("mongodb://localhost", "userdb", "users");

            foreach (var user in users)
            {
                userCollection.InsertOne(user);
                //repo.Insert(user);
            }
        }

        // Deserialize JSON string and return object.
        public static T Deserialize<T>(string jsonStr)
        {
            T obj = default(T);
            MemoryStream ms = new MemoryStream();
            try
            {
                DataContractJsonSerializer ser =
                    new DataContractJsonSerializer(typeof(T));
                StreamWriter writer = new StreamWriter(ms);
                writer.Write(jsonStr);
                writer.Flush();
                ms.Position = 0;
                obj = (T)ser.ReadObject(ms);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ms.Close();
            }
            return obj;
        }
    }
}
