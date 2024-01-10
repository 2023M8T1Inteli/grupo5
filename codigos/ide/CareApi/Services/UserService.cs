using CareApi.Dtos;
using CareApi.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.ComponentModel;
using System.Security.Cryptography;

namespace CareApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IOptions<CareApiDBSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(dbSettings.Value.UserCollectionName);
        }

        public async Task<List<dynamic>> GetManyAsync()
        {
            var projection = Builders<User>.Projection.Exclude(u => u.Password);
            var bsonUsers = await _userCollection.Find(_ => true)
                                                 .Project(projection)
                                                 .ToListAsync();

            var users = bsonUsers.Select(bson => BsonSerializer.Deserialize<dynamic>(bson)).ToList();

            return users;
        }



        public async Task<User> GetByNameAsync(string name) =>
            await _userCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task<User> GetByEmailAsync(string email) =>
            await _userCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

        public async Task<User> CreateOneAsync(CreateUserDto createUserDto)
        {
            var user = new User
            {
                Id = createUserDto.Id,
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                Role = createUserDto.Role
                // Não definir a senha aqui pois ela será configurada pelo usuário
            };

            await _userCollection.InsertOneAsync(user);

            // Gerar e definir o token de redefinição de senha
            var token = GeneratePasswordResetToken();
            var expiration = DateTime.UtcNow.AddHours(1); // Token expira em 1 hora
            var update = Builders<User>.Update
                .Set(u => u.PasswordResetToken, token)
                .Set(u => u.PasswordResetTokenExpiration, expiration);
            await _userCollection.UpdateOneAsync(u => u.Email == user.Email, update);

            // Enviar o e-mail com o link de redefinição de senha
            var emailService = new MailgunEmailService();
            await emailService.SendPasswordSetupEmailAsync(user.Email, token);

            return user;
        }



        public async Task CreateManyAsync(List<User> users) =>
            await _userCollection.InsertManyAsync(users);

        public async Task UpdateByNameAsync(User user, string name) =>
            await _userCollection.ReplaceOneAsync(x => x.Name == name, user);

        public async Task<User> GetByIdAsync(string id) =>
            await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task RemoveByIdAsync(string id) =>
            await _userCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<bool> CheckUserExistsByEmailAsync(string email)
        {
            var user = await _userCollection.Find(u => u.Email == email).FirstOrDefaultAsync();
            return user != null;
        }

        public string GeneratePasswordResetToken()
        {
            var tokenData = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenData);
            }
            string token = Convert.ToBase64String(tokenData)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
            return token;
        }


        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userCollection.Find(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null || user.PasswordResetToken != token || user.PasswordResetTokenExpiration < DateTime.UtcNow)
            {
                return false;
            }

            // Hash the new password
            var hashedNewPassword = HashPassword(newPassword);

            var update = Builders<User>.Update
                .Set(u => u.Password, hashedNewPassword)
                .Unset(u => u.PasswordResetToken) // Remove the token after it's been used
                .Unset(u => u.PasswordResetTokenExpiration);

            var result = await _userCollection.UpdateOneAsync(u => u.Email == email, update);

            return result.ModifiedCount == 1;
        }

        private string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[] { 0x1a, 0x2b, 0x3c, 0x4d, 0x5e, 0x6f, 0x70, 0x81, 0x92, 0xa3, 0xb4, 0xc5, 0xd6, 0xe7, 0xf8, 0x09 };

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }

        public async Task<string> GetHashedPasswordByEmailAsync(string email)
        {
            var user = await _userCollection.Find(u => u.Email == email).FirstOrDefaultAsync();
            return user?.Password; // This will return null if the user is not found
        }
    }
}
