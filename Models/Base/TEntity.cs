using MongoDB.Bson;

namespace Online_Survey.Models.Base
{
    public interface TEntity
    {
        ObjectId Id { get; set; }
    }
}
