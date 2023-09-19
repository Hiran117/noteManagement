using api.Models;
using MongoDB.Driver;

public class NoteContext
{
    private readonly IMongoDatabase _database;

    public NoteContext(IMongoClient client, IConfiguration configuration)
    {
        _database = client.GetDatabase(configuration.GetSection("MongoDB:Database").Value);
    }

    public IMongoCollection<Note> Notes => _database.GetCollection<Note>("notes");
    public IMongoCollection<User> Users => _database.GetCollection<User>("users");
}
