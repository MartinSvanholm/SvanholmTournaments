using System;
using SvanholmTournaments.Shared.Models.Team;
namespace SvanholmTournaments.Shared.Models.Match
{
    public class Match
    {
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public string State { get; set; } = string.Empty;

        public Team HomeTeam { get; set; }

        public Veto? Veto { get; set; }

        public Match StartMatch()
        {
            throw new NotImplementedException();
        }

        public Match StopMatch()
        {
            throw new NotImplementedException();
        }

        public Match Restartmatch()
        {
            throw new NotImplementedException();
        }

        public void DownloadMatchDemo()
        {
            throw new NotImplementedException();
        }
    }
}

