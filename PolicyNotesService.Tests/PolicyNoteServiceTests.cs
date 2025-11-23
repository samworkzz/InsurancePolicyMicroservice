using Moq;
using PolicyNotesService;
using PolicyNotesService.Model;
using PolicyNotesService.Repository;
using PolicyNotesService.Services;
using Xunit;

namespace PolicyNotesService.Tests
{
    public class PolicyNoteServiceTests
    {
        private readonly Mock<IPolicyNoteRepository> _mockRepo;
        private readonly PolicyNoteService _service;

        public PolicyNoteServiceTests()
        {
            _mockRepo = new Mock<IPolicyNoteRepository>();
            _service = new PolicyNoteService(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateNoteAsync_ShouldCallRepositoryAdd()
        {
            // Arrange
            var note = new PolicyNote { Id = 1, PolicyNumber = "POL-001", Note = "Initial Note" };

            // Act
            await _service.CreateNoteAsync(note);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(note), Times.Once);
        }

        [Fact]
        public async Task GetNotesAsync_ShouldReturnListOfNotes()
        {
            // Arrange
            var notes = new List<PolicyNote>
            {
                new PolicyNote { Id = 1, PolicyNumber = "P1", Note = "N1" },
                new PolicyNote { Id = 2, PolicyNumber = "P2", Note = "N2" }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(notes);

            // Act
            var result = await _service.GetNotesAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }
    }
}