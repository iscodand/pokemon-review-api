namespace PokemonReview.Data.DTOs
{
    public class GetOwnerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gym { get; set; }
        public GetCountryDTO Country { get; set; }
    }
}
