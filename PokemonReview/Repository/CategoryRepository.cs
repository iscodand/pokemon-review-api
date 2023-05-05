using AutoMapper;
using PokemonReview.Data;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;
using PokemonReview.Models;

namespace PokemonReview.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public ICollection<GetCategoryDTO> GetCategories()
        {
            return _mapper.Map<List<GetCategoryDTO>>(
                _context.Categories.OrderBy(c => c.Id).ToList());
        }

        public GetCategoryDTO GetCategory(int categoryId)
        {
            return _mapper.Map<GetCategoryDTO>(
                _context.Categories.FirstOrDefault(c => c.Id == categoryId));
        }

        public ICollection<GetPokemonDTO> GetPokemonsByCategory(int categoryId)
        {
            return _mapper.Map<List<GetPokemonDTO>>(
                _context.PokemonCategories
                .Where(c => c.CategoryId == categoryId)
                .Select(c => c.Pokemon).ToList());
        }

        public bool CategoriesExists(int categoryId)
        {
            return _context.Categories.Any(c => c.Id == categoryId);
        }

        public bool CreateCategory(CreateCategoryDTO categoryDTO)
        {
            Category category = _mapper.Map<Category>(categoryDTO);
            category.CreatedAt = DateTime.Now;
            _context.Categories.Add(category);
            return Save();
        }

        public bool UpdateCategory(int categoryId, CreateCategoryDTO categoryDTO)
        {
            Category? category = _context.Categories.First(c => c.Id == categoryId);
            _mapper.Map(categoryDTO, category);
            return Save();
        }

        public bool DeleteCategory(int categoryId)
        {
            Category? category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                return Save();
            }
            return false;
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }

    }
}
