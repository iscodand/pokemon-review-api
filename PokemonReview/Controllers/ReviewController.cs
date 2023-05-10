using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;

namespace PokemonReview.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewController(IReviewRepository reviewRepository,
            IPokemonRepository pokemonRepository,
            IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<GetReviewDTO>))]
        public IActionResult GetReviews()
        {
            ICollection<GetReviewDTO> reviews = _reviewRepository.GetReviews();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(GetReviewDTO))]
        [ProducesResponseType(404)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            GetReviewDTO review = _reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("Pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<GetReviewDTO>))]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();

            ICollection<GetReviewDTO> review = _reviewRepository.GetReviewsByPokemon(pokeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("Reviewer/{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<GetReviewDTO>))]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            ICollection<GetReviewDTO> reviews = _reviewRepository.GetReviewsByReviewer(reviewerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CreateReviewDTO))]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromBody] CreateReviewDTO reviewDTO)
        {
            if (!_pokemonRepository.PokemonExists(reviewDTO.PokemonID))
            {
                ModelState.AddModelError("pokemonID", "Pokemon not found. Verify and try again.");
                return BadRequest(ModelState);
            };

            if (!_reviewerRepository.ReviewerExists(reviewDTO.ReviewerID))
            {
                ModelState.AddModelError("reviewerID", "Reviewer not found. Verify and try again.");
                return BadRequest(ModelState);
            };

            if (reviewDTO == null)
                return BadRequest(ModelState);

            bool reviewDuplicate = _reviewRepository.DuplicatedReview(reviewDTO.ReviewerID, reviewDTO.PokemonID);

            if (reviewDuplicate)
            {
                ModelState.AddModelError("review", "This Reviewer already made review for this Pokemon.");
                return BadRequest(ModelState);
            }

            if (!_reviewRepository.CreateReview(reviewDTO))
            {
                ModelState.AddModelError("", "Something gets wrong ... Try again later.");
                return StatusCode(500, ModelState);
            }

            return Ok("Review successfuly created.");
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] UpdateReviewDTO reviewDTO)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            if (!_reviewRepository.UpdateReview(reviewId, reviewDTO))
            {
                ModelState.AddModelError("", "Something gets wrong... Try again later.");
                return StatusCode(500, ModelState);
            }

            return Ok("Review successfuly updated.");
        }

        [HttpPatch("{reviewId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult PartialUpdateReview(int reviewId, [FromBody] JsonPatchDocument<UpdateReviewDTO> patchDocument)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.PartialUpdateReview(reviewId, patchDocument))
            {
                ModelState.AddModelError("", "Something gets wrong... Try again later.");
                return StatusCode(500, ModelState);
            }

            return Ok("Review successfuly updated.");
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewID)
        {
            if (!_reviewRepository.ReviewExists(reviewID))
                return NotFound();

            if (!_reviewRepository.DeleteReview(reviewID))
            {
                ModelState.AddModelError("", "Something gets wrong ... Try again later.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
