namespace IncidentCreator.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
        public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
    }
}