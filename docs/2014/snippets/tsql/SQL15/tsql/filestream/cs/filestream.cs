//<snippetFS_CS_Enable>
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FILESTREAM
{
    class Program
    {
        static void Main(string[] args)
        {
                string sqlDBQuery =
                    "EXEC sp_filestream_configure"
                  + " @enable_level = 3;";

                SqlConnection sqlConnection = new SqlConnection(
                    "Integrated Security=true;server=(local)");

                SqlCommand sqlCommand = new SqlCommand(
                    sqlDBQuery,
                    sqlConnection);

                try
                {
                    sqlConnection.Open();

                    sqlCommand.ExecuteNonQuery();

                    Console.WriteLine("Enable FILESTREAM succeeded.");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(
                        "FILESTREAM Enable failed. Error = "
                        + ex.ToString());
                }
                finally
                {
                    sqlConnection.Close();
                }
                return;
        }
    }
}
//</snippetFS_CS_Enable>

//<snippetFS_CS_CreateDB>
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FILESTREAM
{
    class Program
    {
        static void Main(string[] args)
        {
                string sqlDBQuery = 
                      " CREATE DATABASE Archive "
                    + " ON"
                    + " PRIMARY ( NAME = Arch1,"
                    + " FILENAME = 'c:\\data\\archdat1.mdf'),"
                    + " FILEGROUP FileStreamGroup1"
                    + " CONTAINS FILESTREAM"
                    + " ( NAME = Arch3,"
                    + " FILENAME = 'c:\\data\\filestream1')"
                    + " LOG ON "
                    + " ( NAME = Archlog1,"
                    + " FILENAME = 'c:\\data\\archlog1.ldf')";

                SqlConnection sqlConnection = new SqlConnection(
                    "Integrated Security=true;server=(local)");

                SqlCommand sqlCommand = new SqlCommand(
                    sqlDBQuery,
                    sqlConnection);

                try
                {
                    sqlConnection.Open();

                    sqlCommand.ExecuteNonQuery();

                    Console.WriteLine("Create database succeeded.");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(
                        "Create database failed. Error = "
                        + ex.ToString());
                }
                finally
                {
                    sqlConnection.Close();
                }
                return;
        }
    }
}
//</snippetFS_CS_CreateDB>

//<snippetFS_CS_CreateTable>
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FILESTREAM
{
    class Program
    {
        static void Main(string[] args)
        {
                string sqlDBQuery =
                      "CREATE TABLE Archive.dbo.Records ("
                    + " [Id] [uniqueidentifier]"
                    + " ROWGUIDCOL NOT NULL UNIQUE,"
                    + " [SerialNumber] INTEGER UNIQUE,"
                    + " [Chart] VARBINARY(MAX) FILESTREAM NULL)";

                SqlConnection sqlConnection = new SqlConnection(
                    "Integrated Security=true;server=(local)");

                SqlCommand sqlCommand = new SqlCommand(
                    sqlDBQuery,
                    sqlConnection);

                try
                {
                    sqlConnection.Open();

                    sqlCommand.ExecuteNonQuery();

                    Console.WriteLine("Create table succeeded.");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(
                        "Create table failed. Error = "
                        + ex.ToString());
                }
                finally
                {
                    sqlConnection.Close();
                }
                return;
        }
    }
}
//</snippetFS_CS_CreateTable>

//<snippetFS_CS_InsertNULL>
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FILESTREAM
{
    class Program
    {
        static void Main(string[] args)
        {
                string sqlDBQuery =
                     "INSERT INTO Archive.dbo.Records"
                   + " VALUES (newid (), 1, NULL)";

                SqlConnection sqlConnection = new SqlConnection(
                    "Integrated Security=true;server=(local)");

                SqlCommand sqlCommand = new SqlCommand(
                    sqlDBQuery,
                    sqlConnection);

                try
                {
                    sqlConnection.Open();

                    sqlCommand.ExecuteNonQuery();

                    Console.WriteLine("Insert NULL succeeded.");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(
                        "Insert NULL failed. Error = "
                        + ex.ToString());
                }
                finally
                {
                    sqlConnection.Close();
                }
                return;
        }
    }
}
//</snippetFS_CS_InsertNULL>

//<snippetFS_CS_InsertZero>
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FILESTREAM
{
    class Program
    {
        static void Main(string[] args)
        {
                string sqlDBQuery =
                     "INSERT INTO Archive.dbo.Records"
                   + " VALUES (newid (), 2, CAST ('' as varbinary(max)))";

                SqlConnection sqlConnection = new SqlConnection(
                    "Integrated Security=true;server=(local)");

                SqlCommand sqlCommand = new SqlCommand(
                    sqlDBQuery,
                    sqlConnection);

                try
                {
                    sqlConnection.Open();

                    sqlCommand.ExecuteNonQuery();

                    Console.WriteLine("Insert zero length record succeeded.");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(
                        "Insert zero length record failed. Error = "
                        + ex.ToString());
                }
                finally
                {
                    sqlConnection.Close();
                }
                return;
        }
    }
}
//</snippetFS_CS_InsertZero>

//<snippetFS_CS_InsertData>
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FILESTREAM
{
    class Program
    {
        static void Main(string[] args)
        {
                string sqlDBQuery =
                     "INSERT INTO Archive.dbo.Records"
                   + " VALUES (newid (), 3, "
                   + " CAST('Seismic Data' as varbinary(max)))";

                SqlConnection sqlConnection = new SqlConnection(
                    "Integrated Security=true;server=(local)");

                SqlCommand sqlCommand = new SqlCommand(
                    sqlDBQuery,
                    sqlConnection);

                try
                {
                    sqlConnection.Open();

                    sqlCommand.ExecuteNonQuery();

                    Console.WriteLine("Insert data succeeded.");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(
                        "Insert data failed. Error = "
                        + ex.ToString());
                }
                finally
                {
                    sqlConnection.Close();
                }
                return;
        }
    }
}
//</snippetFS_CS_InsertData>

//<snippetFS_CS_PathName>
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FILESTREAM
{
    class Program
    {
        static void Main(string[] args)
        {
                string filePath = null;

                string sqlDBQuery =
                      "SELECT Chart.PathName()"
                    + " FROM Archive.dbo.Records"
                    + " WHERE SerialNumber = 3";

                SqlConnection sqlConnection = new SqlConnection(
                    "Integrated Security=true;server=(local)");

                SqlCommand sqlCommand = new SqlCommand(
                    sqlDBQuery,
                    sqlConnection);

                try
                {
                    sqlConnection.Open();

                    Object pathObj = sqlCommand.ExecuteScalar();
                    if (DBNull.Value != pathObj)
                        filePath = (string)pathObj;

                    Console.WriteLine("File Path = {0}", filePath);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(
                        "PathName failed. Error = "
                        + ex.ToString());
                }
                finally
                {
                    sqlConnection.Close();
                }
                return;
        }
    }
}
//</snippetFS_CS_PathName>

//<snippetFS_CS_GET_TRANSACTION_CONTEXT>
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

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

                Console.Write("Transaction Context = ");

                for (int i = 0; i < txContext.Length; i++)
                    Console.Write("{0,3:X}", txContext[i]);

                sqlCommand.Transaction.Commit();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(
                    "Failed to retrieve the Transaction Context"
                  + "Error = " + ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
            return;
        }
    }
}
//</snippetFS_CS_GET_TRANSACTION_CONTEXT>

//<snippetFS_CS_ReadAndWriteBLOB>
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
//</snippetFS_CS_ReadAndWriteBLOB>
