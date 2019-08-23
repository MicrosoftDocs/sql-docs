using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace FILESTREAM
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlConnection = new SqlConnection(
                "Integrated Security=true;server=(local)");

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;

            try
            {
                sqlConnection.Open();

                //The first task is to retrieve the file path
                //of the SQL FILESTREAM BLOB that we want to
                //access in the application.

                sqlCommand.CommandText =
                      "SELECT Chart.PathName()"
                    + " FROM Archive.dbo.Records"
                    + " WHERE SerialNumber = 3";

                String filePath = null;

                Object pathObj = sqlCommand.ExecuteScalar();
                if (DBNull.Value != pathObj)
                    filePath = (string)pathObj;
                else
                {
                    throw new System.Exception(
                        "Chart.PathName() failed"
                      + " to read the path name "
                      + " for the Chart column.");
                }

                //The next task is to obtain a transaction
                //context. All FILESTREAM BLOB operations
                //occur within a transaction context to
                //maintain data consistency.

                //All SQL FILESTREAM BLOB access must occur in 
                //a transaction. MARS-enabled connections
                //have specific rules for batch scoped transactions,
                //which the Transact-SQL BEGIN TRANSACTION statement
                //violates. To avoid this issue, client applications 
                //should use appropriate API facilities for transaction management, 
                //management, such as the SqlTransaction class.

                SqlTransaction transaction = sqlConnection.BeginTransaction("mainTranaction");
                sqlCommand.Transaction = transaction;

                sqlCommand.CommandText =
                    "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()";

                Object obj = sqlCommand.ExecuteScalar();
                byte[] txContext = (byte[])obj;

                //The next step is to obtain a handle that
                //can be passed to the Win32 FILE APIs.

                SqlFileStream sqlFileStream = new SqlFileStream(filePath, txContext, FileAccess.ReadWrite);

                byte[] buffer = new byte[512];

                int numBytes = 0;

                //Write the string, "EKG data." to the FILESTREAM BLOB.
                //In your application this string would be replaced with
                //the binary data that you want to write.

                string someData = "EKG data.";
                Encoding unicode = Encoding.GetEncoding(0);

                sqlFileStream.Write(unicode.GetBytes(someData.ToCharArray()),
                    0,
                    someData.Length);

                //Read the data from the FILESTREAM
                //BLOB.

                sqlFileStream.Seek(0L, SeekOrigin.Begin);

                numBytes = sqlFileStream.Read(buffer, 0, buffer.Length);

                string readData = unicode.GetString(buffer);

                if (numBytes != 0)
                    Console.WriteLine(readData);

                //Because reading and writing are finished, FILESTREAM 
                //must be closed. This closes the c# FileStream class, 
                //but does not necessarily close the underlying 
                //FILESTREAM handle. 
                sqlFileStream.Close();

                //The final step is to commit or roll back the read and write
                //operations that were performed on the FILESTREAM BLOB.

                sqlCommand.Transaction.Commit();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
            return;
        }
    }
}