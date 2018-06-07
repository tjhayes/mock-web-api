using MongoDA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace DeserializeJSON
{
    public class Program
    {
        static void Main(string[] args)
        {
            string jsonStr = System.IO.File.ReadAllText(@"C:\Users\tjhay\MockUsers.json");
            List<User> users = Deserialize<List<User>>(jsonStr);

            UserRepository repo = new UserRepository("mongodb://localhost", "usersdb", "users");

            foreach (var user in users)
            {
                repo.Insert(user);
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
