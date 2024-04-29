using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExcelToolsApi.Domain.Model;

public class MovieModel
{

    public ObjectId Id { get; set; }

    [BsonElement("Title")]
    public string TitleMovie { get; set; } = null!;

    public string Rated { get; set; } = null!;

    public string Plot { get; set; } = null!;
}
