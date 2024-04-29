using ExcelToolsApi.Domain.Model;
using MongoDB.Driver;

namespace ExcelToolsApi.PersistenceExcel.Excel;
public class ApiExcelDbContext
{
    private readonly IMongoCollection<MovieModel> _moviesCollection;

    public ApiExcelDbContext()
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECT") ?? "";
        var databaseName = Environment.GetEnvironmentVariable("MONGO_DATABASE") ?? "";

        var mongoClient = new MongoClient(connectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseName);
        _moviesCollection = mongoDatabase.GetCollection<MovieModel>("movies");
    }

    public async Task<List<MovieModel>> GetMoviesAsync() =>
        await _moviesCollection.Find(_ => true).ToListAsync();

    public async Task<MovieModel?> GetMoviesAsync(string idMovie) =>
        await _moviesCollection.Find(x => x.Id.Equals(idMovie)).FirstOrDefaultAsync();

    public async Task CreateAsync(MovieModel newMovie) =>
        await _moviesCollection.InsertOneAsync(newMovie);

    public async Task UpdateAsync(string idMovie, MovieModel updatedMovie) =>
        await _moviesCollection.ReplaceOneAsync(x => x.Id.Equals(idMovie), updatedMovie);

    public async Task RemoveAsync(string idMovie) =>
        await _moviesCollection.DeleteOneAsync(x => x.Id.Equals(idMovie));
}