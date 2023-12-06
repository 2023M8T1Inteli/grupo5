using CareApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

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

        public async Task<List<User>> GetManyAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();

        public async Task<User> GetByNameAsync(string name) =>
            await _userCollection.Find(x => x.Name == name).FirstOrDefaultAsync();



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
            return Convert.ToBase64String(tokenData);
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
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}
