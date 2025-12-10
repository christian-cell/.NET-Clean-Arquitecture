using CleanArquitecture.Application.Commands.Auth.Register;
using CleanArquitecture.Application.Dtos;
using CleanArquitecture.Domain.Entities;

namespace CleanArquitecture.Application.Services.Auth
{
    public interface IUserService
    {
        public Task<bool> CheckUserExists(string email);

        public Task<Guid> CreateUser(CreateUserCommand user);

        public string GetUser();
        Task<UserDto> CheckVerifiedUserAsync(string email, string password);
        Task<User> SearchUserByEmail(string email);
        Task<User> SearchUserById(Guid id);
    }
};

