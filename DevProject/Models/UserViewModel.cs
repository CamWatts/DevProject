using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevProjectDataModels;

namespace DevProject.Models
{
    public class UserViewModel : TransactionStatusModel
    {
        public UserDTO UserDto { get; set; } // for creating / updating users
        public List<UserDTO> UserDtoList { get; set; } // for retrieving all users
    }
}