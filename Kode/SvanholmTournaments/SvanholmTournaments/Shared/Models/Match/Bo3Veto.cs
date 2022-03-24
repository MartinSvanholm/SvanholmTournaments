using System;
namespace SvanholmTournaments.Shared.Models
{
	public class Bo3Veto : Veto
	{
        public string Ban1 { get; set; } = string.Empty;

        public string Ban2 { get; set; } = string.Empty;

        public string Pick1 { get; set; } = string.Empty;

        public string Pick2 { get; set; } = string.Empty;

        public string Ban3 { get; set; } = string.Empty;

        public string Ban4 { get; set; } = string.Empty;

        public string Decider { get; set; } = string.Empty;
    }
}

