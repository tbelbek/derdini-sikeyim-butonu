using System;
using System.Collections.Generic;
using System.Data;
using derdini_sikeyim_api.Controllers;
using Microsoft.Data.Sqlite;

namespace derdini_sikeyim_api
{
    public class SqliteHelper : ISqliteHelper
    {
        public bool ConnectionChecker()
        {
            var connection = CreateConnection();

            try
            {
                connection.Open();
                return connection.State == ConnectionState.Open;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public SqliteConnection CreateConnection()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "data.db" };

            using var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
            return connection;
        }

        public bool ExecuteSql(List<string> sql)
        {
            var connection = CreateConnection();

            try
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();

                    foreach (var query in sql)
                    {
                        insertCmd.CommandText = query;
                        insertCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ExecuteSql(string sql)
        {
            var connection = CreateConnection();

            try
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();

                    insertCmd.CommandText = sql;
                    insertCmd.ExecuteNonQuery();

                    transaction.Commit();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public EntryObject ReadEntryData(long entryId)
        {
            var connection = CreateConnection();

            try
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = $"Select * from EntryLog WHERE EntryId = {entryId}";
                var data = new EntryObject();
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Id = (long)reader[1];
                        data.EntryId = (string)reader[0];
                        data.FuckCount = (long)reader[2];
                    }
                }

                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }


    }

    public interface ISqliteHelper
    {
        bool ConnectionChecker();
        SqliteConnection CreateConnection();
        bool ExecuteSql(List<string> sql);
        bool ExecuteSql(string sql);
        EntryObject ReadEntryData(long entryId);
    }
}
