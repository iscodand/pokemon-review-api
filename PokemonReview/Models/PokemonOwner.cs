namespace PokemonReview.Models
{
    public class PokemonOwner
    {
        public int Id { get; set; }
        public Pokemon Pokemon { get; set; }
        public int PokemonId { get; set; }
        public Owner Owner { get; set; }
        public int OwnerId { get; set; }
    }
}
