using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentCreator.Domain.Entities
{
    public class Incident
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string IncidentName { get; set; }

        public string Description { get; set; }
        
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}