using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net.Mail;

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

        /// <summary>
        /// Returns true if the User model is valid, and false otherwise.
        /// </summary>
        /// <remarks>
        /// All fields are required except Address, thus if any field besides
        /// Address is null or the Guid is the default value, the model is invalid.
        /// If Address is not null, the Address must be valid for the User to 
        /// be valid (via Address's validate method)
        /// Name must be valid for the User to be valid also (via Name's validate method).
        /// </remarks>
        /// <returns>True if user model is valid and false if invalid.</returns>
        public Boolean Validate()
        {
            if(UserId == Guid.Empty) { return false; }
            if(Location == null || Location == "") { return false; }
            if(Address != null && Address.Validate() == false) { return false; }
            if(ValidateEmail() == false) { return false; }
            if(Name == null || Name.Validate() == false) { return false; }
            if(Gender == null || ValidateGender() == false) { return false; }
            if(Type == null || Type == "") { return false; }

            return true;
        }

        /// <summary>
        /// Check if Email is null, empty string or an invalid email address.
        /// If any of those are true, the email is invalid. Otherwise it is valid.
        /// </summary>
        /// <returns>True if the email is valid and false otherwise.</returns>
        public Boolean ValidateEmail()
        {
            try
            {
                // MailAddress constructor throws an exception if 
                // Email is null, emptry string or an invalid email address.
                MailAddress emailAddress = new MailAddress(Email);
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check that the Gender field is a recognizable way to represent
        /// Male or Female.
        /// </summary>
        /// <remarks>
        /// Valid gender strings are "M", "Male", "F", "Female". (all case-insensitive)
        /// </remarks>
        /// <returns>True if the Gender is valid and false otherwise.</returns>
        public Boolean ValidateGender()
        {
            List<string> validGenders = new List<string>() { "M", "F", "MALE", "FEMALE" };
            if (validGenders.Contains(Gender.ToUpper())) { return true; }
            else { return false; }
        }
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

        /// <summary>
        /// Check that the Name object represents a valid name.
        /// </summary>
        /// <remarks>
        /// First and Last names are required, as well as Name Id. 
        /// Middle name is optional.
        /// No name fields can be an empty string.
        /// </remarks>
        /// <returns>True if the name is valid and false otherwise.</returns>
        public Boolean Validate()
        {
            if(NameId == Guid.Empty) { return false; }
            if(First == null || First == "") { return false; }
            if(Middle == "") { return false; }
            if(Last == null || Last == "") { return false; }
            return true;
        }
    }

    /// <summary>
    /// Stores data for an address model.
    /// </summary>
    /// <remarks>
    /// An Address refers to the street, City, and apartment number of a room.
    /// </remarks>
    [DataContract]
    public class Address
    {
        /// <value> The unique ID of an address. </value>
        [BsonId]
        [DataMember]
        public Guid AddressId { get; set; }
        ///<value> The first street and apartment number. </value>
        [DataMember]
        public string Address1 { get; set; }
        /// <value> The second street and apartment number. </value>
        [DataMember]
        public string Address2 { get; set; } 
        /// <value> The city. </value>
        [DataMember]
        public string City { get; set; }
        /// <value> The state. </value>
        [DataMember]
        public string State { get; set; }
        /// <value> The zip code.. </value>
        [DataMember]
        public string PostalCode { get; set; }
        /// <value> The country. </value>
        [DataMember]
        public string Country { get; set; }

        /// <summary>
        /// Check whether the address is valid.
        /// </summary>
        /// <remarks>
        /// AddressId, Address1, City, State, PostalCode and Country are all required.
        /// Address2 is not required but cannot be an empty string.
        /// If the country is the United States, postal codes must follow the
        /// 5-digit convention (with or without the 4-digit extension code).
        /// Country must follow the ISO Alpha-2 country code format, 
        /// e.g. US for United States and GB for United Kingdom.
        /// </remarks>
        /// <returns>True if the address is valid and false otherwise</returns>
        public Boolean Validate()
        {
            if(AddressId == Guid.Empty) { return false; }
            if(Address1 == null || Address1 == "") { return false; }
            if(Address2 == "") { return false; }
            if(City == null || City == "") { return false; }
            if(State == null || State == "") { return false; }
            if(PostalCode == null || PostalCode == "") { return false; }
            if(Country == null || ValidateCountry() == false) { return false; }
            if(Country.ToUpper() == "US" && ValidateAmericanState() == false) { return false; }
            if(Country.ToUpper() == "US" && ValidateAmericanPostalCode() == false) { return false; }
            return true;
        }

        /// <summary>
        /// Check whether Country is a valid ISO Alpha-2 country code.
        /// </summary>
        /// <remarks>
        /// Try to construct a RegionInfo object with the Country string.
        /// If RegionInfo constructor throws ArgumentException,
        /// then Country is not a valid ISO Alpha-2 country code.
        /// Thus Country is invalid. If no exception is thrown, Country is valid.
        /// </remarks>
        /// <returns>True if Country is valid, and false otherwise</returns>
        public Boolean ValidateCountry()
        {
            bool validCountry = true;
            try { RegionInfo region = new RegionInfo(Country); }
            catch(ArgumentException e) { validCountry = false; }
            return validCountry; 
        }

        /// <summary>
        /// Check whether the string is one of the valid 2-digit American
        /// State codes. If so, it is valid, otherwise invalid.
        /// </summary>
        /// <returns>True if the state code is valid, and false otherwise.</returns>
        public Boolean ValidateAmericanState()
        {
            switch (State)
            {
                case "AL":
                case "AK":
                case "AR":
                case "AZ":
                case "CA":
                case "CO":
                case "CT":
                case "DE":
                case "FL":
                case "GA":
                case "HI":
                case "ID":
                case "IL":
                case "IN":
                case "IA":
                case "KS":
                case "KY":
                case "LA":
                case "ME":
                case "MD":
                case "MA":
                case "MI":
                case "MN":
                case "MS":
                case "MO":
                case "MT":
                case "NE":
                case "NV":
                case "NH":
                case "NJ":
                case "NM":
                case "NY":
                case "NC":
                case "ND":
                case "OH":
                case "OK":
                case "OR":
                case "PA":
                case "RI":
                case "SC":
                case "SD":
                case "TN":
                case "TX":
                case "UT":
                case "VT":
                case "VA":
                case "WA":
                case "WV":
                case "WI":
                case "WY":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Check whether the postal code is in the 5-digit ZIP or the ZIP+4
        /// postal code format for American postal codes. 
        /// If it is, then it is valid. Otherwise it is invalid.
        /// </summary>
        /// <returns>True if postal code is in a valid format and false otherwise.</returns>
        public Boolean ValidateAmericanPostalCode()
        {
            Regex regex = new Regex(@"^\d{5}(?:-\d{4})?$");
            return regex.Match(PostalCode).Success;
        }
    }
}
