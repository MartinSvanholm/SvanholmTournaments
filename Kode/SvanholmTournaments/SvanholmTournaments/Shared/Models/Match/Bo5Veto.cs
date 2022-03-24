using System;
namespace SvanholmTournaments.Shared.Models
{
	public class Bo5Veto : Veto
	{
        public string Ban1 { get; set; } = string.Empty;

        public string Ban2 { get; set; } = string.Empty;

        public string Pick1 { get; set; } = string.Empty;

        public string Pick2 { get; set; } = string.Empty;

        public string Pick3 { get; set; } = string.Empty;

        public string Pick4 { get; set; } = string.Empty;

        public string Decider { get; set; } = string.Empty;
    }
}

