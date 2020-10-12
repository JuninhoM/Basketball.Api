using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace Basketball.Repository.Repositories
{
    public partial class BaseRepository
    {
        protected SqlConnection _conn;

        public BaseRepository()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables().Build();

            _conn = new SqlConnection(builder.GetConnectionString("BasketballBD"));
        }
    }
}
