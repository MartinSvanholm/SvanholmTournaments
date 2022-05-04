using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvanholmTournaments.Shared.DTOs.AuthenticationDTOs;
using SvanholmTournaments.Shared.Models;
using SvanholmTournaments.Shared.Models.Server;

namespace SvanholmTournaments.Shared.AuthenticationModels;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public bool IsArchived { get; set; }
    public List<Role> Roles { get; set; } = new();
    public List<Tournament> Tournaments { get; set; } = new();
    public List<ICsgoServer> Servers { get; set; } = new();
    public DatHostAccount? DatHostAccount { get; set; } = new();


    public User(string firstName, string lastName, string username, DateTime greatedDate, bool isArchived = false)
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        CreatedDate = greatedDate;
        IsArchived = isArchived;
    }

    public User()
    {

    }

    public User MapUser(UserDTO userDTO)
    {
        FirstName = userDTO.FirstName;
        LastName = userDTO.LastName;
        Username = userDTO.Username;
        Id = userDTO.Id;
        CreatedDate = userDTO.CreatedDate;
        DatHostAccount = userDTO.DatHostAccount;

        foreach (var item in userDTO.Roles) {
            Roles.Add(new Role(item));
        }

        return this;
    }
}