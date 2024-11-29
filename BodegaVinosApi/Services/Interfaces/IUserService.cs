using Common.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        string Authenticate(AuthRequestDto credentials); 
        string GenerateJwtToken(User user); 
        int CreateUser(UserDto userDto);
    }
}
