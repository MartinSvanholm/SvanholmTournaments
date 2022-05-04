using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvanholmTournaments.Shared.Models;

public class CsgoServer
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int PlayersOnline { get; set; }

    public string Ip { get; set; } = string.Empty;

    public bool IsOn { get; set; }

    public void StartServer()
    {
        throw new NotImplementedException();
    }

    public void StopServer()
    {
        throw new NotImplementedException();
    }

    public void DeleteServer()
    {
        throw new NotImplementedException();
    }
}
