using System;
namespace SvanholmTournaments.Shared.Models
{
    public class Match
    {
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public string State { get; set; } = string.Empty;

        public Team HomeTeam { get; set; } = new();

        public Team AwayTeam { get; set; } = new();

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

