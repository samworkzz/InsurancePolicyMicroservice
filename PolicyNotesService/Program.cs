using Microsoft.EntityFrameworkCore;
using PolicyNotesService;
using PolicyNotesService.Data;
using PolicyNotesService.Model;
using PolicyNotesService.Repository;
using PolicyNotesService.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Register EF Core InMemory Database
builder.Services.AddDbContext<PolicyNotesDbContext>(options =>
    options.UseInMemoryDatabase("InsuranceDb"));

// 2. Register Repository and Service
builder.Services.AddScoped<IPolicyNoteRepository, PolicyNoteRepository>();
builder.Services.AddScoped<IPolicyNoteService, PolicyNoteService>();

// 3. Add Swagger for testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --- API Endpoints ---

// GET /notes
app.MapGet("/notes", async (IPolicyNoteService service) =>
{
    var notes = await service.GetNotesAsync();
    return Results.Ok(notes);
});

// GET /notes/{id}
app.MapGet("/notes/{id}", async (int id, IPolicyNoteService service) =>
{
    var note = await service.GetNoteByIdAsync(id);
    return note is not null ? Results.Ok(note) : Results.NotFound();
});

// POST /notes
app.MapPost("/notes", async (PolicyNote note, IPolicyNoteService service) =>
{
    await service.CreateNoteAsync(note);
    return Results.Created($"/notes/{note.Id}", note);
});

app.Run();

// Make Program public so Integration Tests can access it
public partial class Program { }