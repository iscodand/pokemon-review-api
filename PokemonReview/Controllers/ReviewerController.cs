using Microsoft.AspNetCore.Mvc;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;

namespace PokemonReview.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        public readonly IReviewerRepository _reviewerRepository;

        public ReviewerController(IReviewerRepository reviewerRepository)
        {
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<GetReviewerDTO>))]
        public IActionResult GetReviewers()
        {
            ICollection<GetReviewerDTO> reviewers = _reviewerRepository.GetReviewers();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewers);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(GetReviewerDTO))]
        [ProducesResponseType(404)]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            GetReviewerDTO reviewer = _reviewerRepository.GetReviewer(reviewerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);
        }

        [HttpGet("{reviewerId}/Reviews")]
        [ProducesResponseType(200, Type = typeof(ICollection<GetReviewDTO>))]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            ICollection<GetReviewDTO> reviews = _reviewerRepository.GetReviewsByReviewer(reviewerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CreateReviewerDTO))]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer(CreateReviewerDTO createReviewerDTO)
        {
            if (createReviewerDTO == null)
                return BadRequest(ModelState);
            
            if (_reviewerRepository.CreateReviewer(createReviewerDTO))
            {
                ModelState.AddModelError("", "Something gets wrong while creating... Try again later.");
                return StatusCode(500, ModelState);
            }

            return Ok("Reviewer successfuly created.");
        }
    }
}
