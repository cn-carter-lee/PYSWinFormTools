using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.Sql;
using System.Data.SqlClient;

/// <summary>
///PsyDataBase 的摘要说明
/// </summary>
public class PsyDataBase
{
        protected SqlConnection _connection;
        protected static string _connectionString = @"Data Source=PRDMSSQLCLSDB01\DW;Initial Catalog=RegistryOps;Integrated Security=SSPI;";

        public PsyDataBase(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
        }

        //public PsyDataBase(string sectionName)
        //{
            
            
        //}

        private static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            return conn;
        }

        private static SqlCommand BuilderQueryCommand(string commandString, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = GetConnection();
            command.CommandText = commandString.Trim();            ;
            if (parameters != null)
            {
                foreach (SqlParameter p in parameters)
                {
                    command.Parameters.Add(p);
                }
            }
            return command;
        }

        public static SqlDataReader GetDataReader(string commandString, SqlParameter[] parameters)
        {
            SqlDataReader reader;
            SqlConnection conn = GetConnection();
            conn.Open();
            SqlCommand command = BuilderQueryCommand(commandString, parameters);
            command.Connection = conn;
            reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public static SqlDataReader GetDataReader(string commandString)
        {
            SqlDataReader reader;
            SqlConnection conn = GetConnection();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = commandString;
            reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public static SqlDataReader GetReaderWithcommandString(string commandString, SqlParameter[] parameters)
        {
            SqlDataReader reader;
            SqlConnection conn = GetConnection();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = commandString;            
            if (parameters != null)
            {
                foreach (SqlParameter param in parameters)
                {
                    command.Parameters.Add(param);
                    
                }
            }
            reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public static int Execute(string commandString, SqlParameter[] parameters)
        {
            int result = 0;
            SqlConnection conn = GetConnection();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = commandString;
            if (parameters != null)
            {
                foreach (SqlParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
            }
            result = command.ExecuteNonQuery();
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return result;
        }

        public static DataTable GetDataTable(string commandString, SqlParameter[] parameters)
        {
            SqlConnection conn = GetConnection();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = commandString;            
            if (parameters != null)
            {
                foreach (SqlParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
            }
            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(command);
            adp.Fill(dt);
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return dt;
        }

        public static DataTable GetDataTable(string commandString)
        {
            return GetDataTable(commandString, null);
        }

        public static object GetSingleObject(string commandString, SqlParameter[] parameters)
        {
            object objResult = 0;
            SqlConnection conn = GetConnection();
            conn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = commandString;
            if (parameters != null)
            {
                foreach (SqlParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
            }
            objResult = command.ExecuteScalar();
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return objResult;
        }
}
