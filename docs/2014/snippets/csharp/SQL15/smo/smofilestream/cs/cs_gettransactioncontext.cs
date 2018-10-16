//<snippetCS_GetTransactionContext>
using System;
using System.Data.SqlClient;
using System.Data;

namespace ConsoleApplication
{
    /// <summary>
    /// This class is a wrapper that contains methods for:
    /// 
    ///     GetTransactionContect() - Returns the current transaction context.
    ///     BeginTransaction() - Begins a transaction.
    ///     CommmitTransaction() - Commits the current transaction.
    /// 
    /// </summary>

    class SqlAccessWrapper
    {
        /// <summary>
        /// Returns a byte array that contains the current transaction
        /// context.
        /// </summary>
        /// <param name="sqlConnection">
        /// SqlConnection object that represents the instance of SQL Server
        /// from which to obtain the transaction context. 
        /// </param>
        /// <returns>
        /// If there is a current transaction context, the return
        /// value is an Object that represents the context.
        /// If there is not a current transaction context, the
        /// value returned is DBNull.Value.
        /// </returns>

        public Object GetTransactionContext(SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;

            return cmd.ExecuteScalar();

        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="sqlConnection">
        /// SqlConnection object that represents the server
        /// on which to run the BEGIN TRANSACTION statement.
        /// </param>

        public void BeginTransaction(SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "BEGIN TRANSACTION";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        /// <param name="sqlConnection">
        /// SqlConnection object that represents the instance of SQL Server
        /// on which to run the COMMIT statement.
        /// </param>

        public void CommitTransaction(SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "COMMIT TRANSACTION";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;

            cmd.ExecuteNonQuery();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Open a connection to the local instance of SQL Server.

            SqlConnection sqlConnection = new SqlConnection("Integrated Security=true;server=(local)");
            sqlConnection.Open();

            SqlAccessWrapper sql = new SqlAccessWrapper();

            //Create a transaction so that sql.GetTransactionContext() will succeed.
            sql.BeginTransaction(sqlConnection);

            //The transaction context will be stored in this array.
            Byte[] transactionToken;   

            Object txObj = sql.GetTransactionContext(sqlConnection);
            if (DBNull.Value != txObj)
            {
                transactionToken = (byte[])txObj;
                Console.WriteLine("Transaction context obtained.\n");
            }

            sql.CommitTransaction(sqlConnection);
        }
    }
}
//</snippetCS_GetTransactionContext>
