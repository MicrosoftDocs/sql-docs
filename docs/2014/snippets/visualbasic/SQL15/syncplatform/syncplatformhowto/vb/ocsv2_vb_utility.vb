Imports System
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlServerCe

'<snippetOCSv2_VB_Utility>
Public Class Utility

    Private Shared _clientPassword As String

    'Get and set the client database password.  
    Public Shared Property Password() As String
        Get
            Return _clientPassword
        End Get
        Set(ByVal value As String)
            _clientPassword = value
        End Set
    End Property

    'Have the user enter a password for the client database file.
    Public Sub SetClientPassword()
        Console.WriteLine("Type a strong password for the client")
        Console.WriteLine("database, and then press Enter.")
        Utility.Password = Console.ReadLine()

    End Sub 'SetClientPassword

    'Return the client connection string with the password.  
    Public ReadOnly Property ClientConnString() As String
        Get
            Return "Data Source='SyncSampleClient.sdf'; Password=" & Utility.Password
        End Get
    End Property

    Private Shared _serverName As String = "localhost"
    Private Shared _serverDbName As String = "SyncSamplesDb"

    'Set the server and database to connect to. 
    Public Sub SetServerAndDb(ByVal serverName As String, ByVal serverDbName As String)
        _serverName = serverName
        _serverDbName = serverDbName
    End Sub

    'Return the server connection string. 
    Public ReadOnly Property ServerConnString() As String
        Get
            Return "Data Source=" & _serverName & "; Initial Catalog=" & _serverDbName & "; Integrated Security=True"
        End Get
    End Property

    'Return the peer1 connection string. 
    Public ReadOnly Property Peer1ConnString() As String
        Get
            Return "Data Source=localhost; Initial Catalog=SyncSamplesDb_Peer1; Integrated Security=True"
        End Get
    End Property

    'Return the peer2 connection string. 
    Public ReadOnly Property Peer2ConnString() As String
        Get
            Return "Data Source=localhost; Initial Catalog=SyncSamplesDb_Peer2; Integrated Security=True"
        End Get
    End Property

    'Return the peer3 connection string. 
    Public ReadOnly Property Peer3ConnString() As String
        Get
            Return "Data Source=localhost; Initial Catalog=SyncSamplesDb_Peer3; Integrated Security=True"
        End Get
    End Property


    'Make server changes that are synchronized on the second 
    'synchronization.
    Public Sub MakeDataChangesOnServer(ByVal tableName As String)

        Dim rowCount As Integer = 0

        Dim serverConn As New SqlConnection(Me.ServerConnString)
        Try
            Dim sqlCommand As SqlCommand = serverConn.CreateCommand()

            If tableName = "Customer" Then

                sqlCommand.CommandText = _
                    "INSERT INTO Sales.Customer (CustomerName, SalesPerson, CustomerType) " _
                  & "VALUES ('Cycle Mart', 'James Bailey', 'Retail') " _
                  & "UPDATE Sales.Customer " _
                  & "SET  SalesPerson = 'James Bailey' " _
                  & "WHERE CustomerName = 'Tandem Bicycle Store' " _
                  & "DELETE FROM Sales.Customer WHERE CustomerName = 'Sharp Bikes'"

            ElseIf tableName = "CustomerContact" Then

                sqlCommand.CommandText = _
                    "DECLARE @CustomerId uniqueidentifier " _
                  & "DECLARE @InsertString nvarchar(1024) " _
                  & "SELECT @CustomerId = CustomerId FROM Sales.Customer " _
                  & "WHERE CustomerName = 'Tandem Bicycle Store' " _
                  & "SET @InsertString = " _
                  & "'INSERT INTO Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) " _
                  & "VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''959-555-2021'', ''Fax'')' " _
                  & "EXECUTE sp_executesql @InsertString " _
                  & "SELECT @CustomerId = CustomerId FROM Sales.Customer " _
                  & "WHERE CustomerName = 'Rural Cycle Emporium' " _
                  & "SET @InsertString = " _
                  & "'UPDATE Sales.CustomerContact SET PhoneNumber = ''158-555-0142'' " _
                  & "WHERE (CustomerId = ''' + CAST(@CustomerId AS nvarchar(38)) + ''' AND PhoneType = ''Business'')' " _
                  & "EXECUTE sp_executesql @InsertString " _
                  & "DELETE FROM Sales.CustomerContact WHERE PhoneType = 'Mobile'"

            ElseIf tableName = "CustomerAndOrderHeader" Then

                'Specify the number of rows to insert into the Customer
                'and OrderHeader tables.
                With sqlCommand
                    .CommandText = "usp_InsertCustomerAndOrderHeader "
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add("@customer_inserts", SqlDbType.Int)
                    .Parameters("@customer_inserts").Value = 13
                    .Parameters.Add("@orderheader_inserts", SqlDbType.Int)
                    .Parameters("@orderheader_inserts").Value = 33
                    .Parameters.Add("@sets_of_inserts", SqlDbType.Int)
                    .Parameters("@sets_of_inserts").Value = 2
                End With

            ElseIf tableName = "Vendor" Then

                sqlCommand.CommandText = _
                    "INSERT INTO Sales.Vendor (VendorName, CreditRating, PreferredVendor) " _
                  & "VALUES ('Victory Bikes', 4, 0) " _
                  & "UPDATE Sales.Vendor " _
                  & "SET CreditRating = 2 " _
                  & "WHERE VendorName = 'Metro Sport Equipment'" _
                  & "DELETE FROM Sales.Vendor WHERE VendorName = 'Premier Sport, Inc.'"

            End If


            serverConn.Open()
            rowCount = sqlCommand.ExecuteNonQuery()
            serverConn.Close()
        Finally
            serverConn.Dispose()
        End Try

        Console.WriteLine("Rows inserted, updated, or deleted at the server: " & rowCount)

    End Sub 'MakeDataChangesOnServer


    'Get a dataset to use for schema creation.
    Public Function CreateDataSetFromServer() As DataSet

        Dim dataSet As New DataSet()
        Dim dataAdapter As New SqlDataAdapter()

        Dim serverConn As New SqlConnection(Me.ServerConnString)
        Try
            Dim createDataSet As SqlCommand = serverConn.CreateCommand()
            createDataSet.CommandText = _
                "SELECT OrderId, CustomerId, OrderDate " _
              & "FROM Sales.OrderHeader"

            serverConn.Open()
            dataAdapter.SelectCommand = createDataSet
            dataAdapter.FillSchema(dataSet, SchemaType.Source)
            serverConn.Close()
        Finally
            serverConn.Dispose()
        End Try

        Return dataSet

    End Function 'CreateDataSetFromServer


    'Revert changes that were made during synchronization.
    Public Sub CleanUpServer()

        Dim serverConn As New SqlConnection(Me.ServerConnString)
        Try
            Dim sqlCommand As SqlCommand = serverConn.CreateCommand()
            sqlCommand.CommandType = CommandType.StoredProcedure
            sqlCommand.CommandText = "usp_InsertSampleData"

            serverConn.Open()
            sqlCommand.ExecuteNonQuery()
            serverConn.Close()
        Finally
            serverConn.Dispose()
        End Try

    End Sub 'CleanUpServer

    'Make peer changes that are synchronized on the second 
    'synchronization.
    Public Sub MakeDataChangesOnPeer(ByVal peerConnString As String, ByVal tableName As String)
        Dim rowCount As Integer = 0

        Dim peerConn As New SqlConnection(peerConnString)
        Try
            Dim sqlCommand As SqlCommand = peerConn.CreateCommand()

            If tableName = "Customer" Then

                If peerConnString = Me.Peer1ConnString Then
                    sqlCommand.CommandText = "INSERT INTO Sales.Customer (CustomerName, SalesPerson, CustomerType) " + "VALUES ('Cycle Mart', 'James Bailey', 'Retail')"
                ElseIf peerConnString = Me.Peer2ConnString Then
                    sqlCommand.CommandText = "UPDATE Sales.Customer " + "SET  SalesPerson = 'James Bailey' " + "WHERE CustomerName = 'Tandem Bicycle Store' "
                ElseIf peerConnString = Me.Peer3ConnString Then

                    sqlCommand.CommandText = "DELETE FROM Sales.Customer WHERE CustomerName = 'Sharp Bikes'"
                End If
            End If

            peerConn.Open()
            rowCount = sqlCommand.ExecuteNonQuery()
            peerConn.Close()
        Finally
            peerConn.Dispose()
        End Try

        Console.WriteLine("Total rows inserted, updated, or deleted at all peers: " & rowCount / 2)

    End Sub 'MakeDataChangesOnPeer


    'Make conflicting peer changes that are synchronized on the second 
    'synchronization.
    Public Sub MakeConflictingChangesOnPeer(ByVal peerConnString As String, ByVal tableName As String)

        Dim rowCount As Integer = 0

        Dim peerConn As New SqlConnection(peerConnString)
        Try
            Dim sqlCommand As SqlCommand = peerConn.CreateCommand()

            If tableName = "Customer" Then

                If peerConnString = Me.Peer1ConnString Then
                    sqlCommand.CommandText = _
                        "UPDATE Sales.Customer " _
                      & "SET  SalesPerson = 'ChangeFromPeerOne' " _
                      & "WHERE CustomerName = 'Tandem Bicycle Store'"
                ElseIf peerConnString = Me.Peer2ConnString Then
                    sqlCommand.CommandText = _
                        "UPDATE Sales.Customer " _
                      & "SET  SalesPerson = 'ChangeFromPeerTwo' " _
                      & "WHERE CustomerName = 'Tandem Bicycle Store'"
                ElseIf peerConnString = Me.Peer3ConnString Then
                    sqlCommand.CommandText = _
                        "UPDATE Sales.Customer " _
                      & "SET  SalesPerson = 'ChangeFromPeerThree' " _
                      & "WHERE CustomerName = 'Tandem Bicycle Store'"
                End If
            End If

            peerConn.Open()
            rowCount = sqlCommand.ExecuteNonQuery()
            peerConn.Close()
        Finally
            peerConn.Dispose()
        End Try

        Console.WriteLine("Total rows inserted, updated, or deleted at all peers: " & rowCount / 2)

    End Sub 'MakeConflictingChangesOnPeer


    Public Sub MakeFailingChangeOnPeer(ByVal peerConnString As String)

        Dim rowCount As Integer = 0

        Dim peerConn As New SqlConnection(peerConnString)
        Try
            Dim sqlCommand As SqlCommand = peerConn.CreateCommand()

            If peerConnString = Me.Peer2ConnString Then
                sqlCommand.CommandText = _
                    "DELETE FROM Sales.Customer " _
                  & "WHERE CustomerName = 'Rural Cycle Emporium'"
            End If

            peerConn.Open()
            rowCount = sqlCommand.ExecuteNonQuery()
            peerConn.Close()
        Finally
            peerConn.Dispose()
        End Try

        Console.WriteLine("Total rows inserted, updated, or deleted at all peers: " & rowCount / 2)

    End Sub 'MakeFailingChangeOnPeer


    Public Sub CleanUpPeer(ByVal peerConnString As String)
        Dim peerConn As New SqlConnection(peerConnString)
        Try
            Dim sqlCommand As SqlCommand = peerConn.CreateCommand()
            sqlCommand.CommandType = CommandType.StoredProcedure
            sqlCommand.CommandText = "usp_ResetPeerData"

            peerConn.Open()
            sqlCommand.ExecuteNonQuery()
            peerConn.Close()
        Finally
            peerConn.Dispose()
        End Try

    End Sub 'CleanUpPeer

    'Create the Customer table on the client.
    Public Sub CreateTableOnClient()

        Dim clientConn As New SqlCeConnection(Me.ClientConnString)
        Try
            Dim createTable As SqlCeCommand = clientConn.CreateCommand()
            createTable.CommandText = _
                "CREATE TABLE Customer( " _
              & "CustomerId uniqueidentifier NOT NULL " _
              & "PRIMARY KEY DEFAULT NEWID(), " _
              & "CustomerName nvarchar(100) NOT NULL, " _
              & "SalesPerson nvarchar(100) NOT NULL, " _
              & "CustomerType nvarchar(100) NOT NULL, " _
              & "SalesNotes nvarchar(1000) NULL)"

            clientConn.Open()
            createTable.ExecuteNonQuery()
            clientConn.Close()
        Finally
            clientConn.Dispose()
        End Try

    End Sub 'CreateTableOnClient


    'Add DEFAULT constraints on the client.
    Public Sub MakeSchemaChangesOnClient(ByVal clientConn As IDbConnection, ByVal clientTran As IDbTransaction, ByVal tableName As String)

        'Execute the command over the same connection and within
        'the same transaction that Synchronization Services uses
        'to create the schema on the client.
        Dim alterTable As New SqlCeCommand()
        alterTable.Connection = CType(clientConn, SqlCeConnection)
        alterTable.Transaction = CType(clientTran, SqlCeTransaction)
        alterTable.CommandText = String.Empty

        'Execute the command, then leave the transaction and 
        'connection open. The client provider will commit and close.
        Select Case tableName
            Case "Customer"
                alterTable.CommandText = _
                    "ALTER TABLE Customer " _
                  & "ADD CONSTRAINT DF_CustomerId " _
                  & "DEFAULT NEWID() FOR CustomerId"
                alterTable.ExecuteNonQuery()

            Case "OrderHeader"
                alterTable.CommandText = _
                     "ALTER TABLE OrderHeader " _
                   & "ADD CONSTRAINT DF_OrderId " _
                   & "DEFAULT NEWID() FOR OrderId"
                alterTable.ExecuteNonQuery()

                alterTable.CommandText = _
                    "ALTER TABLE OrderHeader " _
                  & "ADD CONSTRAINT DF_OrderDate " _
                  & "DEFAULT GETDATE() FOR OrderDate"
                alterTable.ExecuteNonQuery()

            Case "OrderDetail"
                alterTable.CommandText = _
                    "ALTER TABLE OrderDetail " _
                  & "ADD CONSTRAINT DF_Quantity " _
                  & "DEFAULT 1 FOR Quantity"
                alterTable.ExecuteNonQuery()

            Case "Vendor"
                alterTable.CommandText = _
                    "ALTER TABLE Vendor " _
                  & "ADD CONSTRAINT DF_VendorId " _
                  & "DEFAULT NEWID() FOR VendorId"
                alterTable.ExecuteNonQuery()

        End Select

    End Sub 'MakeSchemaChangesOnClient


    'Make client changes that are synchronized on the second 
    'synchronization.
    Public Sub MakeDataChangesOnClient(ByVal tableName As String)

        Dim rowCount As Integer = 0

        Dim clientConn As New SqlCeConnection(Me.ClientConnString)
        Try

            Dim sqlCeCommand As SqlCeCommand = clientConn.CreateCommand()

            clientConn.Open()

            If tableName = "Customer" Then

                sqlCeCommand.CommandText = _
                    "INSERT INTO Customer (CustomerName, SalesPerson, CustomerType) " _
                  & "VALUES ('Cycle Merchants', 'Brenda Diaz', 'Wholesale') "
                rowCount = sqlCeCommand.ExecuteNonQuery()

                sqlCeCommand.CommandText = _
                    "UPDATE Customer " _
                  & "SET SalesPerson = 'Brenda Diaz' " _
                  & "WHERE CustomerName = 'Exemplary Cycles'"
                rowCount += sqlCeCommand.ExecuteNonQuery()

                sqlCeCommand.CommandText = _
                    "DELETE FROM Customer " _
                  & "WHERE CustomerName = 'Aerobic Exercise Company'"
                rowCount += sqlCeCommand.ExecuteNonQuery()

            ElseIf tableName = "Vendor" Then

                sqlCeCommand.CommandText = _
                    "INSERT INTO Vendor (VendorName, CreditRating, PreferredVendor) " _
                  & "VALUES ('Cycling Master', 2, 1) "
                rowCount = sqlCeCommand.ExecuteNonQuery()

                sqlCeCommand.CommandText = _
                    "UPDATE Vendor " _
                  & "SET CreditRating = 2 " _
                  & "WHERE VendorName = 'Mountain Works'"
                rowCount += sqlCeCommand.ExecuteNonQuery()

                sqlCeCommand.CommandText = _
                    "DELETE FROM Vendor " _
                  & "WHERE VendorName = 'Compete, Inc.'"
                rowCount += sqlCeCommand.ExecuteNonQuery()

            End If

            clientConn.Close()
        Finally
            clientConn.Dispose()
        End Try

        Console.WriteLine("Rows inserted, updated, or deleted at the client: " & rowCount)

    End Sub 'MakeDataChangesOnClient


    'Make a change at the client that fails when it is
    'applied at the server.
    Public Sub MakeFailingChangesOnClient()

        Dim rowCount As Integer = 0

        Dim clientConn As New SqlCeConnection(Me.ClientConnString)
        Try

            Dim sqlCeCommand As SqlCeCommand = clientConn.CreateCommand()

            clientConn.Open()

            sqlCeCommand.CommandText = _
                "DELETE FROM Customer " _
              & "WHERE CustomerName = 'Rural Cycle Emporium'"
            rowCount += sqlCeCommand.ExecuteNonQuery()

            clientConn.Close()
        Finally
            clientConn.Dispose()
        End Try

        Console.WriteLine("Rows deleted at the client that will fail at the server: " & rowCount)

    End Sub 'MakeFailingChangesOnClient


    'Make changes at the client and server that conflict
    'when they are synchronized.
    Public Sub MakeConflictingChangesOnClientAndServer()

        Dim rowCount As Integer = 0

        Dim serverConn As New SqlConnection(Me.ServerConnString)
        Try
            Dim sqlCommand As SqlCommand = serverConn.CreateCommand()
            sqlCommand.CommandText = _
                "INSERT INTO Sales.Customer (CustomerId, CustomerName, SalesPerson, CustomerType) " _
              & "VALUES ('009aa4b6-3433-4136-ad9a-a7e1bb2528f7', 'Cycle Merchants', 'Brenda Diaz', 'Wholesale') " _
              & "DELETE FROM Sales.Customer " _
              & "WHERE CustomerName = 'Aerobic Exercise Company' " _
              & "UPDATE Sales.Customer " _
              & "SET SalesPerson = 'James Bailey' " _
              & "WHERE CustomerName = 'Sharp Bikes' " _
              & "UPDATE Sales.Customer " _
              & "SET CustomerType = 'Distributor' " _
              & "WHERE CustomerName = 'Exemplary Cycles'"

            serverConn.Open()
            rowCount = sqlCommand.ExecuteNonQuery()
            serverConn.Close()
        Finally
            serverConn.Dispose()
        End Try

        Dim clientConn As New SqlCeConnection(Me.ClientConnString)
        Try

            Dim sqlCeCommand As SqlCeCommand = clientConn.CreateCommand()

            clientConn.Open()

            sqlCeCommand.CommandText = _
                "INSERT INTO Customer (CustomerId, CustomerName, SalesPerson, CustomerType) " _
              & "VALUES ('009aa4b6-3433-4136-ad9a-a7e1bb2528f7', 'Cycle Merchants', 'James Bailey', 'Wholesale') "
            rowCount += sqlCeCommand.ExecuteNonQuery()

            sqlCeCommand.CommandText = _
                "UPDATE Customer " _
              & "SET CustomerType = 'Retail' " _
              & "WHERE CustomerName = 'Aerobic Exercise Company'"
            rowCount += sqlCeCommand.ExecuteNonQuery()

            sqlCeCommand.CommandText = _
                "DELETE FROM Customer WHERE CustomerName = 'Sharp Bikes'"
            rowCount += sqlCeCommand.ExecuteNonQuery()

            sqlCeCommand.CommandText = _
                "UPDATE Customer " _
              & "SET CustomerType = 'Wholesale' " _
              & "WHERE CustomerName = 'Exemplary Cycles'"
            rowCount += sqlCeCommand.ExecuteNonQuery()

            clientConn.Close()
        Finally
            clientConn.Dispose()
        End Try

        Console.WriteLine("Conflicting rows inserted, updated, or deleted at the client or server: " & rowCount)

    End Sub 'MakeConflictingChangesOnClientAndServer


    'Delete the client database.
    Public Sub RecreateClientDatabase()
        Dim clientConn As New SqlCeConnection(Me.ClientConnString)
        Try
            If File.Exists(clientConn.Database) Then
                File.Delete(clientConn.Database)
            End If
        Finally
            clientConn.Dispose()
        End Try

        Dim sqlCeEngine As New SqlCeEngine(Me.ClientConnString)
        sqlCeEngine.CreateDatabase()

    End Sub 'RecreateClientDatabase
End Class 'Utility
'</snippetOCSv2_VB_Utility>
