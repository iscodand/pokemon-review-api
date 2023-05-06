using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using PokemonReview.Data;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;
using PokemonReview.Models;

namespace PokemonReview.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<GetReviewerDTO> GetReviewers()
        {
            return _mapper.Map<List<GetReviewerDTO>>(
                _context.Reviewers.OrderBy(r => r.Id).ToList());
        }

        public GetReviewerDTO GetReviewer(int reviewerId)
        {
            return _mapper.Map<GetReviewerDTO>(
                _context.Reviewers.FirstOrDefault(r => r.Id == reviewerId));
        }

        public ICollection<GetReviewDTO> GetReviewsByReviewer(int reviewerId)
        {
            return _mapper.Map<List<GetReviewDTO>>(
                _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList());
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(r => r.Id == reviewerId);
        }

        public bool CreateReviewer(CreateReviewerDTO reviewerDTO)
        {
            Reviewer reviewer = _mapper.Map<Reviewer>(reviewerDTO);
            _context.Reviewers.Add(reviewer);
            return Save();
        }

        public bool UpdateReviewer(int reviewerId, UpdateReviewerDTO reviewerDTO)
        {
            Reviewer reviewer = _context.Reviewers.First(r => r.Id == reviewerId);
            _mapper.Map(reviewerDTO, reviewer);
            return Save();
        }

        public bool PartialUpdateReviewer(int reviewerId, JsonPatchDocument<UpdateReviewerDTO> patchDocument)
        {
            Reviewer reviewer = _context.Reviewers.First(r => r.Id == reviewerId);
            UpdateReviewerDTO reviewerDTO = _mapper.Map<UpdateReviewerDTO>(reviewer);
            patchDocument.ApplyTo(reviewerDTO);
            _mapper.Map(reviewerDTO, reviewer);
            return Save();
        }

        public bool DeleteReviewer(int reviewerId)
        {
            Reviewer? reviewer = _context.Reviewers.FirstOrDefault(r => r.Id == reviewerId);

            if (reviewer != null)
            {
                _context.Reviewers.Remove(reviewer);
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
