using System.Linq.Expressions;
using Auth.Service.Common.Entities;
using Auth.Service.Common.Repository;
using MongoDB.Driver;

namespace Auth.Service.Common.MongoDB
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = Builders<T>.Filter.Eq(e => e.BaseId, id);
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await _collection.Find(expression).FirstOrDefaultAsync();
        }

        public async Task<T> FindByEmailAsync(string email)
        {
            var filter = Builders<T>.Filter.Eq("Email", email);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> FindByUserNameAsync(string userName)
        {
            var filter = Builders<T>.Filter.Eq("UserName", userName);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var filter = Builders<T>.Filter.Eq(e => e.BaseId, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<string>> GetRoleAsync(Guid id)
        {
            // var filter = Builders<T>.Filter.Eq(e => e.BaseId, id);
            // var user = await _collection.Find(filter).FirstOrDefaultAsync();
            // return user?.Roles ?? new List<string>();
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(e => e.BaseId, entity.BaseId);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task<bool> ValidateCredentialsAsync(string userName, string password)
        {
            // var filter = Builders<T>.Filter.Eq("UserName", userName);
            // var user = await _collection.Find(filter).FirstOrDefaultAsync();
            // if (user == null)
            // {
            //     return false;
            // }

            // // Assuming you have a method to verify the password
            // return VerifyPassword(password, user.PasswordHash);
            throw new NotImplementedException();

        }

        private bool VerifyPassword(string password, string storedHash)
        {
            // Implement your password verification logic here
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}
