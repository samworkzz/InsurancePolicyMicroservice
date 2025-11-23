using PolicyNotesService.Model;
using PolicyNotesService.Repository;

namespace PolicyNotesService.Services
{
    public class PolicyNoteService : IPolicyNoteService
    {
        private readonly IPolicyNoteRepository _repository;

        public PolicyNoteService(IPolicyNoteRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateNoteAsync(PolicyNote note)
        {
            // Business logic could go here (e.g., validation)
            await _repository.AddAsync(note);
        }

        public async Task<PolicyNote?> GetNoteByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<PolicyNote>> GetNotesAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
