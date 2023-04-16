using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Bilet1
{
    public class ConnectDB : IDisposable
    {
        bool _isConnected;
        SqlConnection _connection;
        string _connectionString = @"Data Source=LAYBRING\MSSQLSERVER02; Initial Catalog=ExamUsers;Integrated Security=True";
        //string _connectionString = @"Server = db.edu.cchgeu.ru;Database = 193_Rulev; User = '193_Rulev'; Password = 'Qq123123'; Integrated Security=False";

        public ConnectDB()
        {
            ConnectionOpen();
        }

        public void ConnectionOpen()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _isConnected = true;
        }

        public void ConnectionClose()
        {
            _connection.Close();
            _isConnected = false;
        }

        public DataTable ExecuteSql(string sql)
        {
            DataTable dataTable = new DataTable();
            SqlCommand command = new SqlCommand(sql, _connection);
            var reader = command.ExecuteReader();
            dataTable.Load(reader);

            return dataTable;
        }

        public void ExecuteSqlNonQuery(string sql)
        {
            SqlCommand command = new SqlCommand(sql, _connection);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            ConnectionClose();
        }
    }
}
