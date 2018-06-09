using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDA
{
    [DataContract]
    public class User
    {
        [BsonId]
        [Required]
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string Location { get; set; }
        [DataMember]
        public Address Address { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public Name Name { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string Type { get; set; }
    }

    [DataContract]
    public class Name
    {
        [BsonId]
        [DataMember]
        public Guid NameId { get; set; }
        [DataMember]
        public string First { get; set; }
        [DataMember]
        public string Middle { get; set; }
        [DataMember]
        public string Last { get; set; }
    }

    [DataContract]
    public class Address
    {
        [BsonId]
        [DataMember]
        public Guid AddressId { get; set; }
        [DataMember]
        public string Address1 { get; set; }
        [DataMember]
        public string Address2 { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
        [DataMember]
        public string Country { get; set; }
    }
}
