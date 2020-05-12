using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Harmony.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // User general info
        [Required]
        [StringLength(50)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        // Choose a role
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }

        // For Venue Owners
        [Display(Name = "Venue Type")]
        public string VenueType { get; set; }

        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }

        [Display(Name = "AddressLine 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "AddressLine 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "City")]
        public string VenueCity { get; set; }

        [Display(Name = "State")]
        public string VenueState { get; set; }

        [StringLength(10)]
        [Display(Name = "Zipcode")]
        public string ZipCode { get; set; }

        // For Musicians
        [StringLength(50)]
        [Display(Name = "Genre")]
        public string GenreName { get; set; }

        public SelectList stateList { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // User general info
        [Required]
        [StringLength(50)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        // Choose a role
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }

        // For Venue Owners
        [Display(Name = "Venue Type")]
        public string VenueType { get; set; }

        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }
        
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }
        
        [Display(Name = "City")]
        public string VenueCity { get; set; }
        
        [Display(Name = "State")]
        public string VenueState { get; set; }

        [StringLength(10)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        // For Musicians
        [StringLength(50)]
        [Display(Name = "Genre")]
        public string GenreName { get; set; }

        public SelectList stateList { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
