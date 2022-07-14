using Infrastructure.Constants;
using Infrastructure.DataServices;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("WebCrawlerTests")]
namespace Infrastructure.Abstractions
{

    public abstract class StorageData
    {
        private string _connection;
        private string cmdDatabase = "select count(*) from master.dbo.sysdatabases where name=@database";
        private string procedureCode = SqlCommands.Insert_Procedure;

        public ServerSettingsBase Settings { get; set; }
        public string ConnectionString
        {
            get
            {
                return this._connection;
            }
        }
        public StorageData(ServerSettingsBase serverSettings)
        {

            Settings = serverSettings;
            this._connection = ConnectionStringGenerator();
            InitializeDatabaseAsync();
        }
        private string ConnectionStringGenerator()
        {
            var acc = Settings.Account == null ? "SSPI" : Settings.Account;
            return string.Format($"Server={Settings.ServerName};Database={Settings.Database};Integrated Security={acc};");
        }

        private async Task InitializeDatabaseAsync()
        {
            using (SqlConnection sql = new SqlConnection($"Server={Settings.ServerName};Database=master;Integrated Security=SSPI;"))
            {
                using (SqlCommand cmd = new SqlCommand(cmdDatabase, sql))
                {
                    SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(_connection);
                    string database = sqlConnectionStringBuilder.InitialCatalog;

                    cmd.Parameters.Add("@database", System.Data.SqlDbType.NVarChar).Value = database;
                    sql.Open();

                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    {
                        await CreateDatabase(database);
                        await CreateTablesAsync();
                    }

                }
            }
        }

        private async Task CreateDatabase(string database)
        {

            using (SqlConnection myConn = new SqlConnection($"Server={Settings.ServerName};Database=master;Integrated Security=SSPI;"))
            {
                //string str = $"CREATE DATABASE {database} ON PRIMARY " +
                //             $"(NAME = {database}, " +
                //             $"FILENAME = 'C:\\{database}.mdf', " +
                //             "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
                //             $"LOG ON (NAME = {database}_Log, " +
                //             $"FILENAME = 'C:\\{database}.ldf', " +
                //             "SIZE = 1MB, " +
                //             "MAXSIZE = 5MB, " +
                //             "FILEGROWTH = 10%)";

                string str = $"CREATE DATABASE {database}";

                SqlCommand myCommand = new SqlCommand(str, myConn);
                try
                {
                    myConn.Open();
                    //await myCommand.ExecuteNonQueryAsync();
                    myCommand.ExecuteNonQuery();

                }
                catch (System.Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (myConn.State == ConnectionState.Open)
                    {
                        myConn.Close();
                    }
                }
            }

        }
        private async Task CreateTablesAsync()
        {
            string createTables = SqlCommands.Create_Tables_Brands_Pictures;

            SqlConnection conn = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand(createTables, conn);
            SqlCommand procedureCmd = new SqlCommand(procedureCode, conn);
            try
            {
                conn.Open();
                //await cmd.ExecuteNonQueryAsync();
                //await procedureCmd.ExecuteNonQueryAsync();
                cmd.ExecuteNonQuery();
                procedureCmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }



    }
}
