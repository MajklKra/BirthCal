using BirthCal.DTO;
using BirthCal.Models;
using Microsoft.EntityFrameworkCore;

namespace BirthCal.Services
{
    public class PresentService
    {
        private ApplicationDbContext _dbContext;

        public PresentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PresentDTO>> GetAllAsync()
        {
            var allPresents = await _dbContext.Presents.ToListAsync();
            var presentDtos = new List<PresentDTO>();

            foreach (var present in allPresents)
            {
                presentDtos.Add(modelToDto(present));
            }

            return presentDtos;
        }

        public PresentDTO modelToDto(Present present)
        {

            return new PresentDTO()
            {
                Id = present.Id,
                Description = present.Description,
                Price = present.Price,
                Note = present.Note,
                Purchase = present.Purchase,
                URL = present.URL,
            };

        }

        public async Task CreateAsync(PresentDTO newPresent)
        {
            await _dbContext.Presents.AddAsync(DtoToModel(newPresent));
            await _dbContext.SaveChangesAsync();
        }

        private Present DtoToModel(PresentDTO present)
        {
            return new Present()
            {
                Id = present.Id,
                Description = present.Description,
                Price = present.Price,
                Note = present.Note,
                Purchase = present.Purchase,
                URL = present.URL
            };
        }

        public async Task<PresentDTO> GetByIdAsync(int id)
        {
            var present = await _dbContext.Presents.FirstOrDefaultAsync(n => n.Id == id);

            return modelToDto(present);
        }

        public async Task<PresentDTO> UpdateAsync(int id, PresentDTO updatedPresent)
        {
            _dbContext.Update(DtoToModel(updatedPresent));
            await _dbContext.SaveChangesAsync();
            return updatedPresent;
        }

        internal async Task DeleteAsync(int id)
        {
            var presentToDelete = await _dbContext.Presents.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Presents.Remove(presentToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
