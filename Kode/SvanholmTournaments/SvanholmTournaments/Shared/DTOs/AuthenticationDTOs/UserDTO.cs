using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvanholmTournaments.Shared.AuthenticationModels;

namespace SvanholmTournaments.Shared.DTOs.AuthenticationDTOs;

public class UserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public List<string> Roles { get; set;}

    public UserDTO(string firstName, string lastName, string username, string password, List<string> roles)
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Password = password;
        Roles = roles;
    }
}
