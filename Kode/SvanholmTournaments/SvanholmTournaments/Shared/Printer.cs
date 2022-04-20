using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvanholmTournaments.Shared.AuthenticationModels;

namespace SvanholmTournaments.Shared;

public static class Printer
{

    public static void PrintUser(User user)
    {
        Console.WriteLine($"Id: {user.Id}");
        Console.WriteLine($"Firstname: {user.FirstName}");
        Console.WriteLine($"Lastname: {user.LastName}");
        Console.WriteLine($"Username: {user.Username}");
        Console.WriteLine($"CreatedDate: {user.CreatedDate}");

        Console.WriteLine("Roles:");
        foreach (Role role in user.Roles) {
            Console.WriteLine($"Id: {role.Id}");
            Console.WriteLine($"Rolename: {role.RoleName}");
        }
    }
}
