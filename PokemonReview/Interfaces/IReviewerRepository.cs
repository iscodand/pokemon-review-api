using Microsoft.AspNetCore.JsonPatch;
using PokemonReview.Data.DTOs;

namespace PokemonReview.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<GetReviewerDTO> GetReviewers();
        GetReviewerDTO GetReviewer(int reviewerId);
        ICollection<GetReviewDTO> GetReviewsByReviewer(int reviewerId);
        bool ReviewerExists (int reviewerId);

        bool CreateReviewer(CreateReviewerDTO reviewerDTO);
        bool UpdateReviewer(int reviewerId, UpdateReviewerDTO patchDocument);
        bool PartialUpdateReviewer(int reviewerId, JsonPatchDocument<UpdateReviewerDTO> reviewerDTO);
        bool DeleteReviewer(int reviewerId);
        bool Save();
    }
}
