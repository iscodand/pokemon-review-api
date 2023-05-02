using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonReview.Data;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;

namespace PokemonReview.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OwnerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<GetOwnerDTO> GetOwners()
        {
            return _mapper.Map<List<GetOwnerDTO>>(
                _context.Owners.OrderBy(o => o.Id).ToList());
        }

        public GetOwnerDTO GetOwner(int ownerId)
        {
            return _mapper.Map<GetOwnerDTO>(
                _context.Owners.Where(o => o.Id == ownerId)
                .Include(o => o.Country)
                .FirstOrDefault());
        }

        public ICollection<GetPokemonDTO> GetPokemonsByOwner(int ownerId)
        {
            return _mapper.Map<List<GetPokemonDTO>>(
                _context.PokemonOwners.Where(po => po.OwnerId == ownerId)
                .Select(po => po.Pokemon).ToList());
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }
    }
}
