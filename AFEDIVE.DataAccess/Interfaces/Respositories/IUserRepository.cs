using AFEDIVE.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AFEDIVE.DataAccess.Interfaces.Respositories
{
    public interface IUserRepository
    {
        Task<int> GetUserPermission(string wellId, string bussinessUnit);
        Task<List<PermissionsDTO>> GetUserGroups();
    }
}
