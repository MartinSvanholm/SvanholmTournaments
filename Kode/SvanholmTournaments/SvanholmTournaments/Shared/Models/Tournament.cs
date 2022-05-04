using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvanholmTournaments.Shared.Models.Server;

namespace SvanholmTournaments.Shared.Models
{
    public class Tournament
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Team> Teams = new();

        public List<ICsgoServer> Servers = new();

        public void CreateBrackets()
        {
            throw new NotImplementedException();
        }
    }
}
