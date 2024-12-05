using StackExchange.Redis;
using System;

namespace RedisLogin.Services
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisService(IConfiguration configuration)
        {
            var redisConnection = configuration["Redis:ConnectionString"] ?? "localhost:6379";
            _redis = ConnectionMultiplexer.Connect(redisConnection);
            _db = _redis.GetDatabase();
        }

        public bool SaveUserSession(string username, string sessionId)
        {
            return _db.StringSet($"session:{sessionId}", username, TimeSpan.FromHours(1));
        }

        public string GetUserFromSession(string sessionId)
        {
            return _db.StringGet($"session:{sessionId}");
        }

        public bool ValidateUser(string username, string password)
        {
            var storedUser = _db.StringGet($"user:{username}");
            return storedUser == password;
        }
        public bool RegisterUser(string username, string password)
        {
            // Verificar si el usuario ya existe
            var existingUser = _db.StringGet($"user:{username}");
            if (!existingUser.IsNull)
            {
                return false;
            }

            // Guardar el nuevo usuario
            return _db.StringSet($"user:{username}", password);
        }
            }
}