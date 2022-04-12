using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvanholmTournaments.Shared.AuthenticationModels;

namespace SvanholmTournaments.Shared.DTOs.AuthenticationDTOs;

public class UserDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string? Password { get; set; }
    public DateTime CreatedDate { get; set; }

    public List<string> Roles { get; set; } = new();

    public UserDTO MapUserDTO(User user)
    {
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
        Id = user.Id;
        CreatedDate = user.CreatedDate;

        foreach (Role role in user.Roles) {
            Roles.Add(role.RoleName);
        }

        return this;
    }
}
