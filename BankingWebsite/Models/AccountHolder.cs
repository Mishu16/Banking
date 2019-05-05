//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankingWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AccountHolder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccountHolder()
        {
            this.Cards = new HashSet<Card>();
        }

        public int accountHolderID { get; set; }

        [Required]
        [DisplayName("First name")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])[A-Za-z]{1,20}$", ErrorMessage = "Name contain invalid character")]
        public string firstname { get; set; }

        [Required]
        [DisplayName("Last name")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])[A-Za-z]{1,20}$", ErrorMessage = "Name contain invalid character")]
        public string lastname { get; set; }

        [NotMapped]
        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[a-z])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Must contain at least one lowercase letter, uppercase letter and number")]
        public string password { get; set; }

        [NotMapped]
        [Required]
        [DisplayName("Confirm password")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Password doesn't match !")]
        public string confirmPassword { get; set; }
        public string passwordsalt { get; set; }
        public string passwordhash { get; set; }

        [NotMapped]
        [DisplayName("Pin")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[1000-9999]{4,4}$", ErrorMessage = "4 digit number")]
        public string pin { get; set; }
        public string pinsalt { get; set; }
        public string pinhash { get; set; }

        [Required]
        [DisplayName("Date of birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }


        [DisplayName("Customer reference")]
        public string customerRef { get; set; }

        [Required]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string email { get; set; }

        [Required]
        [RegularExpression(@"^07[0-9]{8,10}$", ErrorMessage = "Invalid mobile number")]
        [DisplayName("Mobile")]
        public string mobile { get; set; }
        public string firstLineaddr { get; set; }
        public string cityOrTown { get; set; }
        public string postcode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Card> Cards { get; set; }
    }
}
