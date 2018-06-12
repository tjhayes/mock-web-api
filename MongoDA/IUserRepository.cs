using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDA
{
    /// <summary>
    /// Repository interface for User model with CRUD functionality.
    /// </summary>
    public interface IUserRepository
    {
        void Insert(User user);
        IEnumerable<User> Get();
        User GetById(Guid id);
        void Update(User user);
        void Delete(Guid id);
        void DeleteAll();
    }
}
