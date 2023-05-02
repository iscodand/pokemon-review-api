using PokemonReview.Data.DTOs;

namespace PokemonReview.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<GetCategoryDTO> GetCategories();
        GetCategoryDTO GetCategory(int categoryId);
        ICollection<GetPokemonDTO> GetPokemonsByCategory(int categoryId);
        bool CategoriesExists(int categoryId);
    }
}
