using BirthCal.DTO;
using BirthCal.Models;
using Microsoft.EntityFrameworkCore;

namespace BirthCal.Services
{
    public class PersonService
    {
        private ApplicationDbContext _dbContext;

        public PersonService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            var allPeople = await _dbContext.People.ToListAsync();
            var personDtos = new List<PersonDTO>();
            
            foreach (var person in allPeople) 
            {
                personDtos.Add(modelToDto(person));
            }

            return personDtos;
        }

        public PersonDTO modelToDto(Person person) 
        {

            return new PersonDTO()
            {
                Id = person.Id,
                DateOfBirth = person.DateOfBirth,
                FirstName = person.FirstName,
                LastName = person.LastName,
            };
        
        }

        public async Task CreateAsync(PersonDTO newPerson)
        {
            await _dbContext.People.AddAsync(DtoToModel(newPerson));
            await _dbContext.SaveChangesAsync();
        }

        private Person DtoToModel(PersonDTO person)
        {
            return new Person()
            {
                Id = person.Id,
                DateOfBirth = person.DateOfBirth,
                FirstName = person.FirstName,
                LastName = person.LastName,
            };
        }

        public async Task<PersonDTO> GetByIdAsync(int id)
        {
            var person = await _dbContext.People.FirstOrDefaultAsync(n => n.Id == id);

            return modelToDto(person);
        }

        public async Task<PersonDTO> UpdateAsync(int id, PersonDTO updatedPerson)
        {
            _dbContext.Update(DtoToModel(updatedPerson));
            await _dbContext.SaveChangesAsync();
            return updatedPerson;
        }

        internal async Task DeleteAsync(int id)
        {
            var personToDelete = await _dbContext.People.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.People.Remove(personToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
