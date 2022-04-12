using SvanholmTournaments.Shared.AuthenticationModels;
using SvanholmTournaments.Shared.DTOs.AuthenticationDTOs;

namespace SvanholmTournaments.Client.Services.UserService;

public interface IUserService
{
    Task<IEnumerable<User>?> GetUsers();
    Task<User> UpdateUser(UserDTO userDTO);
    Task DeleteUser(UserDTO userDTO);
    Task<User> RegisterUser(UserDTO userDTO);
}