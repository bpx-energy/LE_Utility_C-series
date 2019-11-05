using AFEDIVE.DataAccess.Constants;
using AFEDIVE.DataAccess.Interfaces.Respositories;
using AFEDIVE.DataAccess.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFEDIVE.DataAccess.Repositories
{
    public class UserRepository : DapperDb, IUserRepository
    {
        public UserRepository(string connectionString)
           : base(connectionString)
        {
        }

        public async Task<int> GetUserPermission(string wellId, string groups)
        {
            using (var connection = CreateConnection())
            {
                int permission = 0;

                try
                {
                    // Geting permissions 
                    // Note: BU is take from WELL HEADER baced on well id
                    var result = await connection.QueryAsync<PermissionsDTO>(StoredProcedureNames.GET_PERMISSIONS_FOR_USER, new { API10 = wellId, GROUP = groups }, null, null, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                    permission = result.ToList().Exists(x=>x.Permission == 1) ? 1 : 0;

                }
                catch (Exception ex)
                {

                }

                return permission;

            }
        }

        public async Task<List<PermissionsDTO>> GetUserGroups()
        {
            using (var connection = CreateConnection())
            {
                var permissions = new List<PermissionsDTO>();

                try
                {
                    // Geting permissions 
                    // Note: BU is take from WELL HEADER baced on well id
                    var result = await connection.QueryAsync<PermissionsDTO>(StoredProcedureNames.GET_USER_PERMISSIONS_BY_BU, null, null, null, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                    return result.ToList();

                }
                catch (Exception ex)
                {

                }

                return permissions;

            }
        }
    }
}
