using Microsoft.EntityFrameworkCore;
using PolicyNotesService.Data;
using PolicyNotesService.Model;

namespace PolicyNotesService.Repository
{
    public class PolicyNoteRepository : IPolicyNoteRepository
    {
        private readonly PolicyNotesDbContext _context;

        public PolicyNoteRepository(PolicyNotesDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PolicyNote note)
        {
            _context.PolicyNotes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PolicyNote>> GetAllAsync()
        {
            return await _context.PolicyNotes.ToListAsync();
        }

        public async Task<PolicyNote?> GetByIdAsync(int id)
        {
            return await _context.PolicyNotes.FindAsync(id);
        }
    }
}
