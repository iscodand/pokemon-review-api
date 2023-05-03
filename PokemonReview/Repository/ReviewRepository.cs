using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonReview.Data;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;
using PokemonReview.Models;

namespace PokemonReview.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<GetReviewDTO> GetReviews()
        {
            return _mapper.Map<List<GetReviewDTO>>(
                _context.Reviews.OrderBy(r => r.Id).ToList());
        }

        public GetReviewDTO GetReview(int reviewId)
        {
            return _mapper.Map<GetReviewDTO>(
                _context.Reviews.Where(r => r.Id == reviewId)
                .Include(r => r.Pokemon)
                .Include(r => r.Reviewer).FirstOrDefault());
        }

        public ICollection<GetReviewDTO> GetReviewsByPokemon(int pokeId)
        {
            return _mapper.Map<List<GetReviewDTO>>(
                _context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList());
        }

        public ICollection<GetReviewDTO> GetReviewsByReviewer(int reviewerId)
        {
            return _mapper.Map<List<GetReviewDTO>>(
                _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList());
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }
        
        public bool DuplicatedReview(int reviewerId, int pokeId)
        {
            Reviewer reviewer = _context.Reviewers.First(r => r.Id == reviewerId);
            Pokemon pokemon = _context.Pokemons.First(p => p.Id == pokeId);

            bool reviewExists = _context.Reviews.Any(r => r.Reviewer == reviewer && r.Pokemon == pokemon);

            if (reviewExists)
            {
                return true;
            }

            return false;
        }

        public bool CreateReview(CreateReviewDTO reviewDTO)
        {
            Review review = _mapper.Map<Review>(reviewDTO);
            review.Reviewer = _context.Reviewers.First(r => r.Id == reviewDTO.ReviewerID);
            review.Pokemon = _context.Pokemons.First(p => p.Id == reviewDTO.PokemonID);

            _context.Reviews.Add(review);
            return Save();
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
