using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MongoDA
{
    [DataContract]
    public class User
    {
        [BsonId]
        [DataMember(Name = "userId")]
        public Guid UserId { get; set; }
        [DataMember(Name = "location")]
        public string Location { get; set; }
        [DataMember(Name = "address")]
        public Address Address { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "name")]
        public Name Name { get; set; }
        [DataMember(Name = "gender")]
        public char Gender { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    [DataContract]
    public class Name
    {
        [BsonId]
        [BsonRequired]
        [Required]
        [DataMember(Name = "nameId")]
        public Guid NameId { get; set; }
        [BsonRequired]
        [Required]
        [DataMember(Name = "first")]
        [MaxLength(20)]
        public string First { get; set; }
        [BsonRequired]
        [DataMember(Name = "middle")]
        [MaxLength(20)]
        public string Middle { get; set; }
        [BsonRequired]
        [Required]
        [MaxLength(30)]
        [DataMember(Name = "last")]
        public string Last { get; set; }
    }

    [DataContract]
    public class Address
    {
        [BsonId]
        [DataMember(Name = "addressId")]
        public Guid AddressId { get; set; }
        [DataMember(Name = "address1")]
        public string Address1 { get; set; }
        [DataMember(Name = "address2")]
        public string Address2 { get; set; }
        [DataMember(Name = "city")]
        public string City { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
        [DataMember(Name = "postalCode")]
        public string PostalCode { get; set; }
        [DataMember(Name = "country")]
        public string Country { get; set; }
    }
}
