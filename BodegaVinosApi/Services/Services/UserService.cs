using Common.DTOs;
using Data.Entities;
using Data.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        // Método para autenticar un usuario y generar el token JWT
        public string Authenticate(AuthRequestDto credentials)
        {
            // Validar el usuario con el repositorio
            var user = _userRepository.Authenticate(credentials.Username, credentials.Password);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Credenciales inválidas."); // Usuario no autenticado
            }

            return GenerateJwtToken(user); // Generar el token JWT
        }

        // Método para generar el token JWT
        public string GenerateJwtToken(User user)
        {
            // Crear la clave de seguridad a partir de la configuración
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear los claims del token (datos del usuario)
            var claims = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()), // Identificador único del usuario
                new Claim("username", user.Username) // Nombre de usuario
                // Agrega más claims si es necesario
            };

            // Crear el token JWT
            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1), // Token válido por 1 hora
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken); // Convertir el token a string
        }

        // Método para crear un nuevo usuario
        public int CreateUser(UserDto userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                Password = userDto.Password
            };

            return _userRepository.AddUser(user);
        }
    }
}
