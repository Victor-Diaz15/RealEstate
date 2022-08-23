using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Application.ViewModels.User
{
    public class UpdateAgentViewModel
    {
        public string Id { get; set; }
       
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [DataType(DataType.Text)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Profile picture is required")]
        [DataType(DataType.Upload)]
        public IFormFile ProfilePictureFile { get; set; }
        public string ProfilePicture { get; set; }
        public int TypeUser { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
        public bool IsVerified { get; set; } = false;
    }
}
