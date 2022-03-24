using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvanholmTournaments.Shared.Models
{
    public class Tournament
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Team> Teams = new();

        public List<Server> Servers = new();

        public void CreateBrackets()
        {
            throw new NotImplementedException();
        }
    }
}
