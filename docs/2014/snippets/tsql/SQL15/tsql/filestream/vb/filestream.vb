'<snippetFS_VB_Enable>
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Module Module1

    Sub Main()
        Dim filePath As String = Nothing
        Dim sqlDBQuery As String

        sqlDBQuery = "EXEC sp_filestream_configure"
        sqlDBQuery = sqlDBQuery + " @enable_level = 3;"

        Dim sqlConn As String
        sqlConn = "Integrated Security=true;server=(local)"

        Dim sqlConnection As New SqlConnection(sqlConn)
        Dim sqlCommand As New SqlCommand(sqlDBQuery, sqlConnection)

        Try
            sqlConnection.Open()

            sqlCommand.ExecuteNonQuery()
            Console.WriteLine("Enable FILESTREAM succeeded.")
        Catch ex As System.Exception
            Console.WriteLine("FILESTREAM Enable failed. Error = " + ex.ToString())
        Finally
            sqlConnection.Close()
        End Try
    End Sub
End Module
'</snippetFS_VB_Enable>

'<snippetFS_VB_CreateDB> 
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Module Module1

    Sub Main()
        Dim sqlQuery As String

        sqlQuery = " CREATE DATABASE Archive "
        sqlQuery = sqlQuery + " ON"
        sqlQuery = sqlQuery + " PRIMARY ( NAME = Arch1,"
        sqlQuery = sqlQuery + " FILENAME = 'c:\\data\\archdat1.mdf'),"
        sqlQuery = sqlQuery + " FILEGROUP FileStreamGroup1"
        sqlQuery = sqlQuery + " CONTAINS FILESTREAM"
        sqlQuery = sqlQuery + " ( NAME = Arch3,"
        sqlQuery = sqlQuery + " FILENAME = 'c:\\data\\filestream1')"
        sqlQuery = sqlQuery + " LOG ON "
        sqlQuery = sqlQuery + " ( NAME = Archlog1,"
        sqlQuery = sqlQuery + " FILENAME = 'c:\\data\\archlog1.ldf')"

        Dim sqlConn As String
        sqlConn = "Integrated Security=true;server=(local)"

        Dim sqlConnection As New SqlConnection(sqlConn)
        Dim sqlCommand As New SqlCommand(sqlQuery, sqlConnection)

        Try
            sqlConnection.Open()

            sqlCommand.ExecuteNonQuery()
            Console.WriteLine("The database Archive was created successfully.")
        Catch ex As System.Exception
            Console.WriteLine("There was an error creating the Archive database. Error = " + ex.ToString())
        Finally
            sqlConnection.Close()
        End Try
        Return
    End Sub
End Module
'</snippetFS_VB_CreateDB> 

'<snippetFS_VB_CreateTable>
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Module Module1

    Sub Main()
        Dim sqlQuery As String

        sqlQuery = "CREATE TABLE Archive.dbo.Records ("
        sqlQuery = sqlQuery + " [Id] [uniqueidentifier]"
        sqlQuery = sqlQuery + " ROWGUIDCOL NOT NULL UNIQUE,"
        sqlQuery = sqlQuery + " [SerialNumber] INTEGER UNIQUE,"
        sqlQuery = sqlQuery + " [Chart] VARBINARY(MAX) FILESTREAM NULL)";

        Dim sqlConn As String
        sqlConn = "Integrated Security=true;server=(local)"

        Dim sqlConnection As New SqlConnection(sqlConn)
        Dim sqlCommand As New SqlCommand(sqlQuery, sqlConnection)

        Try
            sqlConnection.Open()

            sqlCommand.ExecuteNonQuery()
            Console.WriteLine("The table Records was created successfully.")
        Catch ex As System.Exception
            Console.WriteLine("There was an error creating the Records table. Error = " + ex.ToString())
        Finally
            sqlConnection.Close()
        End Try
        Return
    End Sub
End Module
'</snippetFS_VB_CreateTable>

'<snippetFS_VB_InsertNULL>
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Module Module1

    Sub Main()
        Dim sqlQuery As String

        sqlQuery = "INSERT INTO Archive.dbo.Records VALUES (newid (), 1, NULL)"

        Dim sqlConn As String
        sqlConn = "Integrated Security=true;server=(local)"

        Dim sqlConnection As New SqlConnection(sqlConn)
        Dim sqlCommand As New SqlCommand(sqlQuery, sqlConnection)

        Try
            sqlConnection.Open()

            sqlCommand.ExecuteNonQuery()
            Console.WriteLine("The table Records was created successfully.")
        Catch ex As System.Exception
            Console.WriteLine("There was an error creating the Records table. Error = " + ex.ToString())
        Finally
            sqlConnection.Close()
        End Try
        Return
    End Sub
End Module
'</snippetFS_VB_InsertNULL>

'<snippetFS_VB_InsertZero>
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Module Module1

    Sub Main()
        Dim sqlQuery As String

        sqlQuery = "INSERT INTO Archive.dbo.Records VALUES (newid (), 2, CAST ('' as varbinary(max)))"

        Dim sqlConn As String
        sqlConn = "Integrated Security=true;server=(local)"

        Dim sqlConnection As New SqlConnection(sqlConn)
        Dim sqlCommand As New SqlCommand(sqlQuery, sqlConnection)

        Try
            sqlConnection.Open()

            sqlCommand.ExecuteNonQuery()
            Console.WriteLine("The table Records was created successfully.")
        Catch ex As System.Exception
            Console.WriteLine("There was an error creating the Records table. Error = " + ex.ToString())
        Finally
            sqlConnection.Close()
        End Try
        Return
    End Sub
End Module
'</snippetFS_VB_InsertZero>

'<snippetFS_VB_InsertData>
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Module Module1

    Sub Main()
        Dim sqlQuery As String

        sqlQuery = "INSERT INTO Archive.dbo.Records VALUES (newid (), 3, CAST('Seismic Data' as varbinary(max)))"

        Dim sqlConn As String
        sqlConn = "Integrated Security=true;server=(local)"

        Dim sqlConnection As New SqlConnection(sqlConn)
        Dim sqlCommand As New SqlCommand(sqlQuery, sqlConnection)

        Try
            sqlConnection.Open()

            sqlCommand.ExecuteNonQuery()
            Console.WriteLine("The table Records was created successfully.")
        Catch ex As System.Exception
            Console.WriteLine("There was an error creating the Records table. Error = " + ex.ToString())
        Finally
            sqlConnection.Close()
        End Try
        Return
    End Sub
End Module
'</snippetFS_VB_InsertData>

'<snippetFS_VB_PathName> 
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Module Module1

    Sub Main()
        Dim filePath As String = Nothing
        Dim sqlDBQuery As String

        sqlDBQuery = "SELECT Chart.PathName()"
        sqlDBQuery = sqlDBQuery + " FROM Archive.dbo.Records"
        sqlDBQuery = sqlDBQuery + " WHERE SerialNumber = 3"

        Dim sqlConn As String
        sqlConn = "Integrated Security=true;server=(local)"

        Dim sqlConnection As New SqlConnection(sqlConn)
        Dim sqlCommand As New SqlCommand(sqlDBQuery, sqlConnection)

        Try
            sqlConnection.Open()

            Dim pathObj As Object = sqlCommand.ExecuteScalar()

            If Not pathObj.Equals(DBNull.Value) Then
                filePath = DirectCast(pathObj, String)
            End If
            Console.WriteLine("File Path = {0}", filePath)
        Catch ex As System.Exception
            Console.WriteLine("PathName failed. Error = " + ex.ToString())
        Finally
            sqlConnection.Close()
        End Try
    End Sub
End Module
'</snippetFS_VB_PathName> 

'<snippetFS_VB_GET_TRANSACTION_CONTEXT>
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Module Module1

    Sub Main()
        Dim sqlConnection As New SqlConnection("Integrated Security=true;server=(local)")

        Dim sqlCommand As New SqlCommand()
        sqlCommand.Connection = sqlConnection

        Try
            sqlConnection.Open()

            sqlCommand.CommandText = "BEGIN TRANSACTION"
            sqlCommand.ExecuteNonQuery()

            sqlCommand.CommandText = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()"

            Dim txContext As Byte()
            Dim txObj As Object = sqlCommand.ExecuteScalar()

            If Not txObj.Equals(DBNull.Value) Then
                txContext = DirectCast(txObj, Byte())
            Console.Write("Transaction Context = ")

            Dim I As Long

            For I = 0 To txContext.ToString().Length
                Console.Write("{0,3:X}", txContext(I))
            Next
            End If

            sqlCommand.CommandText = "COMMIT"
            sqlCommand.ExecuteNonQuery()
        Catch ex As System.Exception
            Console.WriteLine("Failed to retrieve the Transaction Context" + "Error = " + ex.ToString())
        Finally
            sqlConnection.Close()
        End Try
    End Sub
End Module
'</snippetFS_VB_GET_TRANSACTION_CONTEXT>

'<snippetFS_VB_ReadAndWriteBLOB>
Imports System.IO
Imports System 
Imports System.Collections.Generic 
Imports System.Text 
Imports System.Data 
Imports System.Data.SqlClient 
Imports System.Data.SqlTypes 

Module Module1
    Public Sub Main(ByVal args As String())
        '        Dim sqlConnection As New SqlConnection("Integrated Security=true;server=(local)")
        Dim sqlConnection As New SqlConnection("Integrated Security=true;server=kellyreyue\MSSQL1")

        Dim sqlCommand As New SqlCommand()
        sqlCommand.Connection = sqlConnection

        Try
            sqlConnection.Open()

            'The first task is to retrieve the file path 
            'of the SQL FILESTREAM BLOB that we want to 
            'access in the application. 

            sqlCommand.CommandText = "SELECT Chart.PathName()" + " FROM Archive.dbo.Records" + " WHERE SerialNumber = 3"

            Dim filePath As String = Nothing

            Dim pathObj As Object = sqlCommand.ExecuteScalar()
            If Not pathObj.Equals(DBNull.Value) Then
                filePath = DirectCast(pathObj, String)
            Else
                Throw New System.Exception("Chart.PathName() failed" + " to read the path name " + " for the Chart column.")
            End If

            'The next task is to obtain a transaction 
            'context. All FILESTREAM BLOB operations 
            'occur within a transaction context to 
            'maintain data consistency. 

            'All SQL FILESTREAM BLOB access must occur in 
            'a transaction. MARS-enabled connections 
            'have specific rules for batch scoped transactions, 
            'which the Transact-SQL BEGIN TRANSACTION statement 
            'violates. To avoid this issue, client applications 
            'should use appropriate API facilities for transaction management, 
            'management, such as the SqlTransaction class. 

            Dim transaction As SqlTransaction = sqlConnection.BeginTransaction("mainTranaction")
            sqlCommand.Transaction = transaction

            sqlCommand.CommandText = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()"

            Dim obj As Object = sqlCommand.ExecuteScalar()
            Dim txContext As Byte() = Nothing

            Dim contextLength As UInteger

            If Not obj.Equals(DBNull.Value) Then
                txContext = DirectCast(obj, Byte())
                contextLength = txContext.Length()
            Else
                Dim message As String = "GET_FILESTREAM_TRANSACTION_CONTEXT() failed"
                Throw New System.Exception(message)
            End If

            'The next step is to obtain a handle that 
            'can be passed to the Win32 FILE APIs. 

            Dim sqlFileStream As New SqlFileStream(filePath, txContext, FileAccess.ReadWrite)

            Dim buffer As Byte() = New Byte(511) {}

            Dim numBytes As Integer = 0

            'Write the string, "EKG data." to the FILESTREAM BLOB. 
            'In your application this string would be replaced with 
            'the binary data that you want to write. 

            Dim someData As String = "EKG data."
            Dim unicode As Encoding = Encoding.GetEncoding(0)

            sqlFileStream.Write(unicode.GetBytes(someData.ToCharArray()), 0, someData.Length)

            'Read the data from the FILESTREAM 
            'BLOB. 

            sqlFileStream.Seek(0, SeekOrigin.Begin)

            numBytes = sqlFileStream.Read(buffer, 0, buffer.Length)

            Dim readData As String = unicode.GetString(buffer)

            If numBytes <> 0 Then
                Console.WriteLine(readData)
            End If

            'Because reading and writing are finished, FILESTREAM 
            'must be closed. This closes the c# FileStream class, 
            'but does not necessarily close the underlying 
            'FILESTREAM handle. 
            sqlFileStream.Close()

            'The final step is to commit or roll back the read and write 
            'operations that were performed on the FILESTREAM BLOB. 

            sqlCommand.Transaction.Commit()
        Catch ex As System.Exception
            Console.WriteLine(ex.ToString())
        Finally
            sqlConnection.Close()
        End Try
        Return
    End Sub
End Module
'</snippetFS_VB_ReadAndWriteBLOB>