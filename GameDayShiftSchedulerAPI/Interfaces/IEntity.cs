using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameDayShiftSchedulerAPI.Interfaces
{
    public interface IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
    }
}
