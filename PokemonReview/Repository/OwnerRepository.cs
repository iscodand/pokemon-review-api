using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using PokemonReview.Data;
using PokemonReview.Data.DTOs;
using PokemonReview.Interfaces;
using PokemonReview.Models;

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

        public bool CreateOwner(CreateOwnerDTO ownerDTO)
        {
            Owner owner = _mapper.Map<Owner>(ownerDTO);
            owner.Country = _context.Countries.First(c => c.Id == ownerDTO.CountryID);
            owner.CreatedAt = DateTime.Now;
            _context.Owners.Add(owner);
            return Save();
        }

        public bool UpdateOwner(int ownerId, UpdateOwnerDTO ownerDTO)
        {
            Owner owner = _context.Owners.First(o => o.Id == ownerId);
            _mapper.Map(ownerDTO, owner);
            return Save();
        }

        public bool PartialUpdateOwner(int ownerId, JsonPatchDocument patchDocument)
        {
            Owner owner = _context.Owners.First(o => o.Id == ownerId);
            patchDocument.ApplyTo(owner);
            _context.Owners.Update(owner);
            return Save();
        }

        public bool DeleteOwner(int ownerId)
        {
            Owner owner = _context.Owners.First(o => o.Id == ownerId);
            _context.Owners.Remove(owner);
            return Save();
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
