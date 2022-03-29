using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvanholmTournaments.Shared.AuthenticationModels;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public bool IsArchived { get; set; }
    public List<Role> Roles { get; set; }

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
}