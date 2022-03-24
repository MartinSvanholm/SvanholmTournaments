using System;
namespace SvanholmTournaments.Shared.Models.Match
{
	public class Bo1Veto : Veto
	{
        public string Ban1 { get; set; } = string.Empty;

        public string Ban2 { get; set; } = string.Empty;

        public string Ban3 { get; set; } = string.Empty;

        public string Ban4 { get; set; } = string.Empty;

        public string Ban5 { get; set; } = string.Empty;

        public string Ban6 { get; set; } = string.Empty;

        public string MapToPlay { get; set; } = string.Empty;
    }
}

