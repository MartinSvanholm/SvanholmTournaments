using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvanholmTournaments.Shared.Models.Server;

public interface ICsgoServer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PlayersOnline { get; set; }
    public string Ip { get; set; }
    public string GotvIp { get; set; }
    public bool IsOn { get; set; }
    public void StartServer();
    public void StopServer();
}
