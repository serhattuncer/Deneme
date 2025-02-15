﻿using Entities.ModelDto;
using Entities.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryUsers:IGenericRepository<Users>
    {
        Task<bool> ValidateUser(LoginUserDto loginUserDto);
    }
}
