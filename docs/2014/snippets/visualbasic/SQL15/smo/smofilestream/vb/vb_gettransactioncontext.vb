'<snippetVB_GetTransactionContext> 
Imports System
Imports System.Data.SqlClient
Imports System.Data

Namespace ConsoleApplication
    ''' <summary> 
    ''' This class is a wrapper that contains methods for: 
    ''' 
    ''' GetTransactionContect() - Returns the current transaction context. 
    ''' BeginTransaction() - Begins a transaction. 
    ''' CommmitTransaction() - Commits the current transaction. 
    ''' 
    ''' </summary> 

    Class SqlAccessWrapper
        ''' <summary> 
        ''' Returns a byte array that contains the current transaction 
        ''' context. 
        ''' </summary> 
        ''' <param name="sqlConnection"> 
        ''' SqlConnection object that represents the instance of SQL Server 
        ''' from which to obtain the transaction context. 
        ''' </param> 
        ''' <returns> 
        ''' If there is a current transaction context, the return 
        ''' value is an Object that represents the context. 
        ''' If there is not a current transaction context, the 
        ''' value returned is DBNull.Value. 
        ''' </returns> 

        Public Function GetTransactionContext(ByVal sqlConnection As SqlConnection) As Object
            Dim cmd As New SqlCommand()
            cmd.CommandText = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()"
            cmd.CommandType = CommandType.Text
            cmd.Connection = sqlConnection

            Return cmd.ExecuteScalar()

        End Function

        ''' <summary> 
        ''' Begins the transaction. 
        ''' </summary> 
        ''' <param name="sqlConnection"> 
        ''' SqlConnection object that represents the server 
        ''' on which to run the BEGIN TRANSACTION statement. 
        ''' </param> 

        Public Sub BeginTransaction(ByVal sqlConnection As SqlConnection)
            Dim cmd As New SqlCommand()

            cmd.CommandText = "BEGIN TRANSACTION"
            cmd.CommandType = CommandType.Text
            cmd.Connection = sqlConnection

            cmd.ExecuteNonQuery()
        End Sub

        ''' <summary> 
        ''' Commits the transaction. 
        ''' </summary> 
        ''' <param name="sqlConnection"> 
        ''' SqlConnection object that represents the instance of SQL Server 
        ''' on which to run the COMMIT statement. 
        ''' </param> 

        Public Sub CommitTransaction(ByVal sqlConnection As SqlConnection)
            Dim cmd As New SqlCommand()

            cmd.CommandText = "COMMIT TRANSACTION"
            cmd.CommandType = CommandType.Text
            cmd.Connection = sqlConnection

            cmd.ExecuteNonQuery()
        End Sub
    End Class

    Class Program
        Shared Sub Main()
            '''Open a connection to the local instance of SQL Server.

            Dim sqlConnection As New SqlConnection("Integrated Security=true;server=(local)")
            sqlConnection.Open()

            Dim sql As New SqlAccessWrapper()

            '''Create a transaction so that sql.GetTransactionContext() will succeed. 
            sql.BeginTransaction(sqlConnection)

            '''The transaction context will be stored in this array. 
            Dim transactionToken As Byte()

            Dim txObj As Object = sql.GetTransactionContext(sqlConnection)

            '''If the returned object is not NULL, there is a valid transaction
            '''token, and it must be converted into a format that is usable within
            '''the application.

            If Not txObj.Equals(DBNull.Value) Then
                transactionToken = DirectCast(txObj, Byte())
                Console.WriteLine("Transaction context obtained." & Chr(10) & "")
            End If

            sql.CommitTransaction(sqlConnection)
        End Sub
    End Class
End Namespace
'</snippetVB_GetTransactionContext> 
