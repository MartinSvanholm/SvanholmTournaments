using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvanholmTournaments.Shared.Models
{
    public abstract class Bracket
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Team> Teams { get; set; } = new();

        public virtual void CreateNextRound()
        {
            throw new NotImplementedException();
        }
    }
}
