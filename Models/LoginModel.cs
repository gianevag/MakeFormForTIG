using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MakeFormForTIG.Models
{
    public class LoginModel
    {
        [BsonId]
        public virtual ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
