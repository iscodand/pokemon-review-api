namespace PokemonReview.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ICollection<Owner>? Owners { get; set; }
    }
}
