using PokemonReview.Data.DTOs;

namespace PokemonReview.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<GetCategoryDTO> GetCategories();
        GetCategoryDTO GetCategory(int categoryId);
        ICollection<GetPokemonDTO> GetPokemonsByCategory(int categoryId);
        bool CategoriesExists(int categoryId);

        bool CreateCategory(CreateCategoryDTO category);
        bool UpdateCategory(int categoryId, CreateCategoryDTO categoryDTO);
        bool DeleteCategory(int categoryId);
        bool Save();
    }
}
