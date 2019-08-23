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