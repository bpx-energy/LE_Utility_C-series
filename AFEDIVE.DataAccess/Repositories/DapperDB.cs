using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace AFEDIVE.DataAccess.Repositories
{
    public abstract class DapperDb
    {
        private const int DefaultCommandTimeout = 30;
        protected int CommandTimeout { get; set; }

        protected IConfiguration Configuration;
        protected string ConnectionName { get; private set; }

        protected DapperDb(IConfiguration configuration, string connectionName)
        {
            this.ConnectionName = connectionName;
            this.Configuration = configuration;
        }

        protected DapperDb(string connectionString)
        {
            this.ConnectionName = connectionString;

        }

        protected virtual IDbConnection CreateConnection()
        {
            return Connection(this.ConnectionName);
        }

        protected virtual IDbConnection Connection(string connName = null)
        {
            if (string.IsNullOrWhiteSpace(connName))
            {
                connName = ConnectionName;
            }
            var connectionString = this.ConnectionName;

            return new SqlConnection(connectionString);
        }

      
    }
}
