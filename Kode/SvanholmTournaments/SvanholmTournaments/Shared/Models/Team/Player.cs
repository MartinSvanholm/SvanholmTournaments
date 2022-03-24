using System;
namespace SvanholmTournaments.Shared.Models.Team
{
	public class Player
	{
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string SteamID { get; set; } = string.Empty;

        public string FaceitURL { get; set; } = string.Empty;

        public int Elo { get; set; }
    }
}

