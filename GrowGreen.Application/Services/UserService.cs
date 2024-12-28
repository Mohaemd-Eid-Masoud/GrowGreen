using GrowGreen.Domain.Entities;
using GrowGreen.Domain.Interfaces;
using GrowGreen.Application.DTOs;
using BCrypt.Net;

namespace GrowGreen.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Get user by id
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("User not found");

            // Map User entity to UserDto
            return new UserDto
            {
                Id = user.Id,
                Username = user.Name,
                Email = user.Email
            };
        }

        // Register a new user
        public async Task RegisterUserAsync(UserDto userDto)
        {
            // Check if the user already exists
            var existingUser = await _userRepository.GetByUsernameAsync(userDto.Username);
            if (existingUser != null)
                throw new Exception("User with this email already exists");

            // Hash password before storing
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var user = new User
            {
                Name = userDto.Username,
                Email = userDto.Email,
                PasswordHash = hashedPassword // Store the hashed password
            };

            await _userRepository.AddAsync(user);

        }

        // Update user profile
        public async Task UpdateUserProfileAsync(int id, UserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("User not found");

            user.Name = userDto.Username;
            user.Email = userDto.Email;

            // Hash password before updating
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            }

            await _userRepository.UpdateAsync(user);
        }

        // Delete user
        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("User not found");

            await _userRepository.DeleteAsync(user);
        }

        // Authenticate user
        public async Task<bool> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
                return false;

            // Compare plain-text password with the hashed password
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
    }
}
