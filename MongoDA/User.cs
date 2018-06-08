using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
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
        public string Gender { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
    /// <summary>
    /// Name model used by the User model
    /// Contains all information for a user's name.
    /// </summary>
    /// <remarks>
    /// Refers to the name of the User model of Revature associates. Model includes a First, Last, and Middle name.
    /// </remarks>
    [DataContract]
    public class Name
    {
        /// <value> Unique name ID. </value>
        [BsonId]
        [BsonRequired]
        [Required]
        [DataMember(Name = "nameId")]
        public Guid NameId { get; set; }

        /// <value> First name of the User associate. </value>
        [BsonRequired]
        [Required]
        [DataMember(Name = "first")]
        [MaxLength(255)]
        public string First { get; set; }

        /// <value> Middle name of the User associate. </value>
        [BsonRequired]
        [DataMember(Name = "middle")]
        [MaxLength(255)]
        public string Middle { get; set; }

        /// <value> Last name of the User associate. </value>
        [BsonRequired]
        [Required]
        [MaxLength(255)]
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
