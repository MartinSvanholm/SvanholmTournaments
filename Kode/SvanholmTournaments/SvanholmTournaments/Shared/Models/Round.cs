using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvanholmTournaments.Shared.Models
{
    public class Round
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Team> Teams { get; set; } = new();

        public List<Match> Matches { get; set; } = new();

        public void CreateMatch()
        {
            throw new NotImplementedException();
        }

        public void GenerateMatches()
        {
            throw new NotImplementedException();
        }
    }
}
