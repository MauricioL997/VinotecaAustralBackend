﻿using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        User Authenticate(string username, string password);
        int AddUser(User user);
    }
}
