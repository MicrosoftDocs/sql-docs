'<snippetOCS_VB_UploadOnly>
Imports System
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlServerCe
Imports Microsoft.Synchronization
Imports Microsoft.Synchronization.Data
Imports Microsoft.Synchronization.Data.Server
Imports Microsoft.Synchronization.Data.SqlServerCe

Class Program

    Shared Sub Main(ByVal args() As String)
        'The Utility class handles all functionality that is not
        'directly related to synchronization, such as holding 
        'connection string information and making changes to the 
        'server and client databases.
        Dim util As New Utility()

        'The SampleStats class handles information from the SyncStatistics
        'object that the Synchronize method returns.
        Dim sampleStats As New SampleStats()

        'Request a password for the client database, and delete
        'and re-create the database. The client synchronization
        'provider also enables you to create the client database 
        'if it does not exist.
        util.SetClientPassword()
        util.RecreateClientDatabase()

        'Initial synchronization. Instantiate the SyncAgent
        'and call Synchronize.
        Dim sampleSyncAgent As New SampleSyncAgent()
        Dim syncStatistics As SyncStatistics = sampleSyncAgent.Synchronize()
        sampleStats.DisplayStats(syncStatistics, "initial")

        'Make changes on the client.
        util.MakeDataChangesOnClient("Customer")

        'Subsequent synchronization.
        syncStatistics = sampleSyncAgent.Synchronize()
        sampleStats.DisplayStats(syncStatistics, "subsequent")

        'Return server data back to its original state.
        util.CleanUpServer()

        'Exit.
        Console.Write(vbLf + "Press Enter to close the window.")
        Console.ReadLine()

    End Sub 'Main
End Class 'Program

'Create a class that is derived from 
'Microsoft.Synchronization.SyncAgent.
Public Class SampleSyncAgent
    Inherits SyncAgent

    Public Sub New()
        'Instantiate a client synchronization provider and specify it
        'as the local provider for this synchronization agent.
        Me.LocalProvider = New SampleClientSyncProvider()

        'Instantiate a server synchronization provider and specify it
        'as the remote provider for this synchronization agent.
        Me.RemoteProvider = New SampleServerSyncProvider()

        'Create a Customer SyncGroup. This is not required
        'for the single table that we are synchronizing. It is typically
        'used so that changes to multiple related tables are 
        'synchronized at the same time.
        Dim customerSyncGroup As New SyncGroup("Customer")

        'Add the Customer table: specify a synchronization direction of
        'UploadOnly, and that an existing table should be dropped.
        '<snippetOCS_VB_UploadOnly_CustomerSyncTable>
        Dim customerSyncTable As New SyncTable("Customer")
        customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        customerSyncTable.SyncDirection = SyncDirection.UploadOnly
        customerSyncTable.SyncGroup = customerSyncGroup
        Me.Configuration.SyncTables.Add(customerSyncTable)
        '</snippetOCS_VB_UploadOnly_CustomerSyncTable>

    End Sub 'New
End Class 'SampleSyncAgent

'Create a class that is derived from 
'Microsoft.Synchronization.Server.DbServerSyncProvider.
Public Class SampleServerSyncProvider
    Inherits DbServerSyncProvider

    Public Sub New()
        'Create a connection to the sample server database.
        Dim util As New Utility()
        Dim serverConn As New SqlConnection(util.ServerConnString)
        Me.Connection = serverConn

        'Create a command to retrieve a new anchor value from
        'the server. In this case, we use a timestamp value
        'that is retrieved and stored in the client database.
        'During each synchronization, the new anchor value and
        'the last anchor value from the previous synchronization
        'are used: the set of changes between these upper and
        'lower bounds is synchronized.
        '
        'SyncSession.SyncNewReceivedAnchor is a string constant; 
        'you could also use @sync_new_received_anchor directly in 
        'your queries.
        '<snippetOCS_VB_UploadOnly_NewAnchorCommand>
        Dim selectNewAnchorCommand As New SqlCommand()
        Dim newAnchorVariable As String = "@" + SyncSession.SyncNewReceivedAnchor
        With selectNewAnchorCommand
            .CommandText = "SELECT " + newAnchorVariable + " = min_active_rowversion() - 1"
            .Parameters.Add(newAnchorVariable, SqlDbType.Timestamp)
            .Parameters(newAnchorVariable).Direction = ParameterDirection.Output
            .Connection = serverConn
        End With
        Me.SelectNewAnchorCommand = selectNewAnchorCommand
        '</snippetOCS_VB_UploadOnly_NewAnchorCommand>

        'Create a SyncAdapter for the Customer table, and then define
        'the commands to synchronize changes:
        '* SelectIncrementalInsertsCommand is used to get the schema
        '  from the server.
        '* InsertCommand, UpdateCommand, and DeleteCommand are used to apply
        '  to the server the changes that the client provider has selected
        '  from the client.

        'Create the SyncAdapter.
        Dim customerSyncAdapter As New SyncAdapter("Customer")

        'Get the schema from the server.
        '<snippetOCS_VB_UploadOnly_CustomerIncrInsert>
        Dim customerIncrInserts As New SqlCommand()
        With customerIncrInserts
            .CommandText = _
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " _
              & "FROM Sales.Customer"
            .Connection = serverConn            
        End With
        customerSyncAdapter.SelectIncrementalInsertsCommand = customerIncrInserts
        '</snippetOCS_VB_UploadOnly_CustomerIncrInsert>

        'Apply inserts to the server.
        Dim customerInserts As New SqlCommand()
        With customerInserts
            .CommandText = _
               "INSERT INTO Sales.Customer (CustomerId, CustomerName, SalesPerson, CustomerType, InsertId, UpdateId) " _
             & "VALUES (@CustomerId, @CustomerName, @SalesPerson, @CustomerType, @sync_client_id, @sync_client_id) " _
             & "SET @sync_row_count = @@rowcount"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@CustomerName", SqlDbType.NVarChar)
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@CustomerType", SqlDbType.NVarChar)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int)
            .Connection = serverConn
        End With
        customerSyncAdapter.InsertCommand = customerInserts

        'Apply updates to the server.
        '<snippetOCS_VB_UploadOnly_CustomerUpdate>
        Dim customerUpdates As New SqlCommand()
        With customerUpdates
            .CommandText = _
                "UPDATE Sales.Customer SET " _
              & "CustomerName = @CustomerName, SalesPerson = @SalesPerson, " _
              & "CustomerType = @CustomerType, " _
              & "UpdateId = @sync_client_id " _
              & "WHERE (CustomerId = @CustomerId) " _
              & "AND (@sync_force_write = 1 " _
              & "OR (UpdateTimestamp <= @sync_last_received_anchor " _
              & "OR UpdateId = @sync_client_id)) " _
              & "SET @sync_row_count = @@rowcount"
            .Parameters.Add("@CustomerName", SqlDbType.NVarChar)
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@CustomerType", SqlDbType.NVarChar)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" + SyncSession.SyncForceWrite, SqlDbType.Bit)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int)
            .Connection = serverConn
        End With
        customerSyncAdapter.UpdateCommand = customerUpdates
        '</snippetOCS_VB_UploadOnly_CustomerUpdate>

        'Apply deletes to the server.            
        Dim customerDeletes As New SqlCommand()
        With customerDeletes
            .CommandText = _
                "DELETE FROM Sales.Customer " _
              & "WHERE (CustomerId = @CustomerId) " _
              & "AND (@sync_force_write = 1 " _
              & "OR (UpdateTimestamp <= @sync_last_received_anchor " _
              & "OR UpdateId = @sync_client_id)) " _
              & "SET @sync_row_count = @@rowcount " _
              & "IF (@sync_row_count > 0)  BEGIN " _
              & "UPDATE Sales.Customer_Tombstone " _
              & "SET DeleteId = @sync_client_id " _
              & "WHERE (CustomerId = @CustomerId) " _
              & "END"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" + SyncSession.SyncForceWrite, SqlDbType.Bit)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int)
            .Connection = serverConn
        End With
        customerSyncAdapter.DeleteCommand = customerDeletes

        'Add the SyncAdapter to the server synchronization provider.
        Me.SyncAdapters.Add(customerSyncAdapter)

    End Sub 'New 
End Class 'SampleServerSyncProvider

'Create a class that is derived from 
'Microsoft.Synchronization.Data.SqlServerCe.SqlCeClientSyncProvider.
'You can just instantiate the provider directly and associate it
'with the SyncAgent, but here we use this class to handle client 
'provider events.
Public Class SampleClientSyncProvider
    Inherits SqlCeClientSyncProvider


    Public Sub New()
        'Specify a connection string for the sample client database.
        Dim util As New Utility()
        Me.ConnectionString = util.ClientConnString

        'We use the CreatingSchema event to change the schema
        'by using the API. We use the SchemaCreated event to 
        'change the schema by using SQL.
        AddHandler Me.CreatingSchema, AddressOf SampleClientSyncProvider_CreatingSchema
        AddHandler Me.SchemaCreated, AddressOf SampleClientSyncProvider_SchemaCreated
    End Sub 'New


    Private Sub SampleClientSyncProvider_CreatingSchema(ByVal sender As Object, ByVal e As CreatingSchemaEventArgs)
        'Set the RowGuid property because it is not copied
        'to the client by default. This is also a good time
        'to specify literal defaults with .Columns[ColName].DefaultValue,
        'but we will specify defaults like NEWID() by calling
        'ALTER TABLE after the table is created.
        Console.Write("Creating schema for " + e.Table.TableName + " | ")
        e.Schema.Tables("Customer").Columns("CustomerId").RowGuid = True

    End Sub 'SampleClientSyncProvider_CreatingSchema


    Private Sub SampleClientSyncProvider_SchemaCreated(ByVal sender As Object, ByVal e As SchemaCreatedEventArgs)
        'Call ALTER TABLE on the client. This must be done
        'over the same connection and within the same
        'transaction that Synchronization Services uses
        'to create the schema on the client.
        Dim util As New Utility()
        util.MakeSchemaChangesOnClient(e.Connection, e.Transaction, e.Table.TableName)
        Console.WriteLine("Schema created for " + e.Table.TableName)

    End Sub 'SampleClientSyncProvider_SchemaCreated
End Class 'SampleClientSyncProvider

'Handle the statistics that are returned by the SyncAgent.
Public Class SampleStats

    Public Sub DisplayStats(ByVal syncStatistics As SyncStatistics, ByVal syncType As String)
        Console.WriteLine(String.Empty)
        If syncType = "initial" Then
            Console.WriteLine("****** Initial Synchronization ******")
        ElseIf syncType = "subsequent" Then
            Console.WriteLine("***** Subsequent Synchronization ****")
        End If

        Console.WriteLine("Start Time: " & syncStatistics.SyncStartTime)
        Console.WriteLine("Total Changes Uploaded: " & syncStatistics.TotalChangesUploaded)
        Console.WriteLine("Total Changes Downloaded: " & syncStatistics.TotalChangesDownloaded)
        Console.WriteLine("Complete Time: " & syncStatistics.SyncCompleteTime)
        Console.WriteLine(String.Empty)

    End Sub 'DisplayStats
End Class 'SampleStats
'</snippetOCS_VB_UploadOnly>