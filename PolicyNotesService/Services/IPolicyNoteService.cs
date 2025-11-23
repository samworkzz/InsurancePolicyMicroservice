using PolicyNotesService.Model;

namespace PolicyNotesService.Services
{
    public interface IPolicyNoteService
    {
        Task<IEnumerable<PolicyNote>> GetNotesAsync();
        Task<PolicyNote?> GetNoteByIdAsync(int id);
        Task CreateNoteAsync(PolicyNote note);
    }
}
