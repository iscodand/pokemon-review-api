namespace PokemonReview.Models
{
    public class PokemonCategory
    {
        public int Id { get; set; }
        public Pokemon Pokemon { get; set; }
        public int PokemonId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
