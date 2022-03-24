using SvanholmTournaments.Shared.AuthenticationModels;

namespace SvanholmTournaments.Shared.Data;

public interface IUserData
{
    Task DeleteUser(int id);
    Task<User?> GetUserById(int id);
    Task<IEnumerable<User>> GetUsers();
    Task InsertUser(User user);
    Task UpdateUser(User user);
}