using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.Models;
using SvanholmTournaments.Shared.Models.Server;

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
    public List<Tournament> Tournaments { get; set; } = new();
    public List<ICsgoServer> Servers { get; set; } = new();
    public DatHostAccount? DatHostAccount { get; set; }

    public UserDTO MapUserDTO(User user)
    {
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
        Id = user.Id;
        CreatedDate = user.CreatedDate;
        DatHostAccount = user.DatHostAccount;

        foreach (Role role in user.Roles) {
            Roles.Add(role.RoleName);
        }

        return this;
    }
}
