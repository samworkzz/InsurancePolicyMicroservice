using Microsoft.AspNetCore.Mvc.Testing;
using PolicyNotesService; // Reference the main project
using PolicyNotesService.Model;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace PolicyNotesService.Tests
{
    public class PolicyNotesApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PolicyNotesApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostNotes_ShouldReturn201Created()
        {
            // Arrange
            var newNote = new PolicyNote { Id = 10, PolicyNumber = "POL-TEST", Note = "Integration Test Note" };

            // Act
            var response = await _client.PostAsJsonAsync("/notes", newNote);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
       public async Task GetNotes_ShouldReturn200OK()
        {
            // Act
            var response = await _client.GetAsync("/notes");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetNoteById_ShouldReturn200_WhenFound()
        {
            // Arrange: First create a note so we can find it
            var newNote = new PolicyNote { Id = 99, PolicyNumber = "POL-99", Note = "To Be Found" };
            await _client.PostAsJsonAsync("/notes", newNote);

            // Act
            var response = await _client.GetAsync("/notes/99");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetNoteById_ShouldReturn404_WhenMissing()
        {
            // Act
            var response = await _client.GetAsync("/notes/9999"); // ID that doesn't exist

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}