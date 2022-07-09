using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tournament
    {
        public int Id { get; set; }
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
        public TournamentStatus Status { get; set; }
        public int NumberOfRounds { get; set; }
        public int? CreatedById { get; set; }
        public virtual User? CreatedBy { get; set; }

        public virtual List<User>? Players { get; set; }
    }
}
