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
            UserContext context = new UserContext(new UserRepository());
            string jsonStr = System.IO.File.ReadAllText(@"C:\Users\tjhay\MockUsers.json");
            List<User> users = Deserialize<List<User>>(jsonStr);

            foreach (var user in users)
            {
                context.Insert(user);
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
