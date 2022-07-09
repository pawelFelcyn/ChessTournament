using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public record CreateTournamentDto
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Street { get; set; }
        public string? BuildingNumber { get; set; }
        public string? LocalNumber { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? MaxPlayers { get; set; }
        public decimal? CostPerPlayer { get; set; }
        public string? Description { get; set; }
        public int NumberOfRounds { get; set; }
    }
}
