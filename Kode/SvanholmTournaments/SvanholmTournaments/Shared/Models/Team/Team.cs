using System;

namespace SvanholmTournaments.Shared.Models
{ 
	public class Team
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public List<Player> Players { get; set; } = new();

		public double AverageElo { get; set; }

		public int Points { get; set; }

		public int MatchesWon { get; set; }

		public int MatchesLost { get; set; }

		public int MatchesDraw { get; set; }

		public int MapsWon { get; set; }

		public int MapsLost { get; set; }

		public int MapsDraw { get; set; }

		public int RoundsWon { get; set; }

		public int RoundsLost { get; set; }

		public void CalculateElo()
        {
			throw new NotImplementedException();
        }
	}
}

