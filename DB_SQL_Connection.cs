using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MyWindowsApp
{
    class DB_SQL_Connection
    {
        private string _sqlString;
        private string _connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=MyTestDB;
                    Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;
                    ApplicationIntent=ReadWrite;MultiSubnetFailover=False; MultipleActiveResultSets=true";
        SqlDataAdapter _dataAdapter;

        public string SqlQuery { set { _sqlString = value; } }

        public DataSet GetConnection { get { return MyDataSet(); } }

        private DataSet MyDataSet()
        {   // SQL String with No Parameters - return Dataset
            SqlConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            _dataAdapter = new SqlDataAdapter(_sqlString, dbConnection);
            DataSet dataSet = new DataSet();
            _dataAdapter.Fill(dataSet, "Table_Data_1");
            dbConnection.Close();
            return dataSet;
        }

        public void UpdateDatabase(DataSet dataSet)
        {   // Update Changes in Dataset to DB
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_dataAdapter);
            commandBuilder.DataAdapter.Update(dataSet.Tables[0]);
        }

        public DataSet ReturnQuery(string myStoredProcedure)
        {   // Stored Procedure with No Parameters - return Dataset
            SqlConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            _dataAdapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            SqlCommand myCommand = new SqlCommand(myStoredProcedure, dbConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            _dataAdapter.SelectCommand = myCommand;
            _dataAdapter.Fill(dataSet, "Table_Data_1");
            dbConnection.Close();
            return dataSet;
        }

        public DataSet ReturnQuery(string myStoredProcedure, string param1, string value1)
        {   // Stored Procedure with One Parameter - return Dataset
            DataSet dataSet = new DataSet();
            SqlConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            SqlCommand myCommand = new SqlCommand(myStoredProcedure, dbConnection);
            myCommand.Parameters.AddWithValue("@" + param1, value1);
            myCommand.CommandType = CommandType.StoredProcedure;
            _dataAdapter = new SqlDataAdapter();
            _dataAdapter.SelectCommand = myCommand;
            _dataAdapter.Fill(dataSet, "Table_Data_1");
            dbConnection.Close();
            return dataSet;
        }

        public DataSet ReturnQuery(string myStoredProcedure, string param1, string value1, string param2, string value2)
        {   // Stored Procedure with Two Parameters - return Dataset
            DataSet dataSet = new DataSet();
            SqlConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            SqlCommand myCommand = new SqlCommand(myStoredProcedure, dbConnection);
            myCommand.Parameters.AddWithValue("@" + param1, value1);
            myCommand.Parameters.AddWithValue("@" + param2, value2);
            myCommand.CommandType = CommandType.StoredProcedure;
            _dataAdapter = new SqlDataAdapter();
            _dataAdapter.SelectCommand = myCommand;
            _dataAdapter.Fill(dataSet, "Table_Data_1");
            dbConnection.Close();
            return dataSet;
        }

        public DataSet ReturnQuery(string myStoredProcedure, string param1, string value1, string param2, string value2, 
            string param3, string value3)
        {   // Stored Procedure with Three Parameters - return Dataset
            DataSet dataSet = new DataSet();
            SqlConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            SqlCommand myCommand = new SqlCommand(myStoredProcedure, dbConnection);
            myCommand.Parameters.AddWithValue("@" + param1, value1);
            myCommand.Parameters.AddWithValue("@" + param2, value2);
            myCommand.Parameters.AddWithValue("@" + param3, value3);
            myCommand.CommandType = CommandType.StoredProcedure;
            _dataAdapter = new SqlDataAdapter();
            _dataAdapter.SelectCommand = myCommand;
            _dataAdapter.Fill(dataSet, "Table_Data_1");
            dbConnection.Close();
            return dataSet;
        }

        public DataSet ReturnQuery(string myStoredProcedure, string param1, string value1, string param2, string value2,
            string param3, string value3, string param4, string value4)
        {   // Stored Procedure with Four Parameters - return Dataset
            DataSet dataSet = new DataSet();
            SqlConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            SqlCommand myCommand = new SqlCommand(myStoredProcedure, dbConnection);
            myCommand.Parameters.AddWithValue("@" + param1, value1);
            myCommand.Parameters.AddWithValue("@" + param2, value2);
            myCommand.Parameters.AddWithValue("@" + param3, value3);
            myCommand.Parameters.AddWithValue("@" + param4, value4);
            myCommand.CommandType = CommandType.StoredProcedure;
            _dataAdapter = new SqlDataAdapter();
            _dataAdapter.SelectCommand = myCommand;
            _dataAdapter.Fill(dataSet, "Table_Data_1");
            dbConnection.Close();
            return dataSet;
        }

        public void ExecuteQuery(string myStoredProcedure)
        {   // SQL String or Stored Procedure with zero Parameters - No return
            SqlConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            SqlCommand myCommand = new SqlCommand(myStoredProcedure, dbConnection);
            myCommand.ExecuteNonQuery();
            dbConnection.Close();
        }

        public void ExecuteQuery(string myStoredProcedure, string param1, string value1)
        {   // Stored Procedure with One Parameter - No return
            SqlConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            SqlCommand myCommand = new SqlCommand(myStoredProcedure, dbConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@" + param1, value1);
            myCommand.ExecuteNonQuery();
            dbConnection.Close();
        }

        public void ExecuteQuery(string myStoredProcedure, string param1, string value1, string param2, string value2)
        {   // Stored Procedure with Two Parameters - No return
            SqlConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            SqlCommand myCommand = new SqlCommand(myStoredProcedure, dbConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@" + param1, value1);
            myCommand.Parameters.AddWithValue("@" + param2, value2);
            myCommand.ExecuteNonQuery();
            dbConnection.Close();
        }

        public void ExecuteQuery(string myStoredProcedure, string param1, string value1, string param2, string value2, 
            string param3, string value3)
        {   // Stored Procedure with Three Parameters - No return
            SqlConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            SqlCommand myCommand = new SqlCommand(myStoredProcedure, dbConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@" + param1, value1);
            myCommand.Parameters.AddWithValue("@" + param2, value2);
            myCommand.Parameters.AddWithValue("@" + param3, value3);
            myCommand.ExecuteNonQuery();
            dbConnection.Close();
        }

        public void ExecuteQuery(string myStoredProcedure, string param1, string value1, string param2, string value2,
            string param3, string value3, string param4, string value4)
        {   // Stored Procedure with Four Parameters - No return
            SqlConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            SqlCommand myCommand = new SqlCommand(myStoredProcedure, dbConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@" + param1, value1);
            myCommand.Parameters.AddWithValue("@" + param2, value2);
            myCommand.Parameters.AddWithValue("@" + param3, value3);
            myCommand.Parameters.AddWithValue("@" + param4, value4);
            myCommand.ExecuteNonQuery();
            dbConnection.Close();
        }
    }
}
