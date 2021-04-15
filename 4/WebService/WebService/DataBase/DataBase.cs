using System;

namespace WebService.DataBase
{
    public abstract class DataBase
    {
        protected string ConnectionString { get; private set; }

        protected DataBase(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
    }
}