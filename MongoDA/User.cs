using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDA
{
    /// <summary>
    /// User model containing all information for a single user.
    /// </summary>
    /// <remarks>
    /// A user refers to a single person in the Revature Housing system.  Currently, this model
    /// only represents associates in training.
    /// </remarks>
    [DataContract]
    public class User
    {
        /// <value>Unique user id</value>
        [BsonId]
        [DataMember(Name = "userId")]
        [Required (ErrorMessage = "User Id is required.")]
        public Guid UserId { get; set; }

        /// <value>Training location for this user</value>
        [DataMember(Name = "location")]
        [Required (ErrorMessage = "Location is required.")]
        [MaxLength(255, ErrorMessage ="Location must be shorter than 256 characters.")]
        public string Location { get; set; }

        /// <value>Address object of user while in the housing system</value>
        [DataMember(Name = "address")]
        [Required (ErrorMessage = "Address is required.")]
        public Address Address { get; set; }

        /// <value>Email address of user</value>
        [DataMember(Name = "email")]
        [Required (ErrorMessage = "Email is required.")]
        [EmailAddress (ErrorMessage = "Invalid email format.")]
        [MaxLength(
            254,
            ErrorMessage = "Email must be shorter than 255 characters.")]
        public string Email { get; set; }

        /// <value>Name object of user</value>
        [DataMember(Name = "name")]
        [Required (ErrorMessage = "Name is required.")]
        public Name Name { get; set; }

        /// <value>Gender of user</value>
        [DataMember(Name = "gender")]
        [Required (ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        /// <value>Type of user; for now, only Associates are implemented.</value>
        [DataMember(Name = "type")]
        [Required (ErrorMessage = "Type is required.")]
        [StringLength(maximumLength: 255, MinimumLength = 0, ErrorMessage = "Type must be shorter than 256 characters")]
        public string Type { get; set; }
    }

    [DataContract]
    public class Name
    {
        [BsonId]
        [DataMember(Name = "nameId")]
        public Guid NameId { get; set; }
        [DataMember(Name = "first")]
        public string First { get; set; }
        [DataMember(Name = "middle")]
        public string Middle { get; set; }
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
