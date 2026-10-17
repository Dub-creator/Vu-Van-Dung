using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLySinhVien.Data.DAL
{
    public class DataHelper
    {
        private readonly string connectionString =
            @"Server=LAPTOP-6SN65NCS\HUECTMDB;Database=QLSV;Integrated Security=True;TrustServerCertificate=True";

        public int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);
            if (parameters != null && parameters.Length > 0)
                command.Parameters.AddRange(parameters);

            connection.Open();
            return command.ExecuteNonQuery();
        }

        public DataTable GetDataTable(string sql, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);
            if (parameters != null && parameters.Length > 0)
                command.Parameters.AddRange(parameters);

            using var adapter = new SqlDataAdapter(command);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);
            if (parameters != null && parameters.Length > 0)
                command.Parameters.AddRange(parameters);

            connection.Open();
            return command.ExecuteScalar();
        }
    }
}
