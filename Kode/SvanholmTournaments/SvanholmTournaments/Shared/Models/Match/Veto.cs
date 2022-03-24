using System;
namespace SvanholmTournaments.Shared.Models.Match
{
	public abstract class Veto
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;
	}
}

