using PolicyNotesService.Model;

namespace PolicyNotesService.Repository
{
    public interface IPolicyNoteRepository
    {
        Task<IEnumerable<PolicyNote>> GetAllAsync();
        Task<PolicyNote?> GetByIdAsync(int id);
        Task AddAsync(PolicyNote note);
    }
}
