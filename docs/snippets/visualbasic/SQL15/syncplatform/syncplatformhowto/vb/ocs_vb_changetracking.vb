'<snippetOCS_VB_ChangeTracking>
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

        'Specify which server and database to connect to.
        util.SetServerAndDb("localhost", "SyncSamplesDb_ChangeTracking")

        'Initial synchronization. Instantiate the SyncAgent
        'and call Synchronize.
        Dim sampleSyncAgent As New SampleSyncAgent()
        Dim syncStatistics As SyncStatistics = sampleSyncAgent.Synchronize()
        sampleStats.DisplayStats(syncStatistics, "initial")

        'Make changes on the server and client.
        util.MakeDataChangesOnServer("Customer")
        util.MakeDataChangesOnClient("Customer")

        'Subsequent synchronization.
        syncStatistics = sampleSyncAgent.Synchronize()
        sampleStats.DisplayStats(syncStatistics, "subsequent")

        'Make conflicting changes on the server and client.
        util.MakeConflictingChangesOnClientAndServer()

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
'<snippetOCS_VB_ChangeTracking_SampleSyncAgent>
Public Class SampleSyncAgent
    Inherits SyncAgent

    Public Sub New()
        'Instantiate a client synchronization provider and specify it
        'as the local provider for this synchronization agent.
        Me.LocalProvider = New SampleClientSyncProvider()

        'Instantiate a server synchronization provider and specify it
        'as the remote provider for this synchronization agent.
        Me.RemoteProvider = New SampleServerSyncProvider()

        'Add the Customer table: specify a synchronization direction of
        'Bidirectional, and that an existing table should be dropped.
        '<snippetOCS_VB_ChangeTracking_CustomerSyncTable>
        Dim customerSyncTable As New SyncTable("Customer")
        customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        customerSyncTable.SyncDirection = SyncDirection.Bidirectional
        Me.Configuration.SyncTables.Add(customerSyncTable)
        '</snippetOCS_VB_ChangeTracking_CustomerSyncTable>

    End Sub 'New
End Class 'SampleSyncAgent
'</snippetOCS_VB_ChangeTracking_SampleSyncAgent>

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
        'the server. In this case, we use a BigInt value
        'from the change tracking table.
        'During each synchronization, the new anchor value and
        'the last anchor value from the previous synchronization
        'are used: the set of changes between these upper and
        'lower bounds is synchronized.
        '
        'SyncSession.SyncNewReceivedAnchor is a string constant; 
        'you could also use @sync_new_received_anchor directly in 
        'your queries.
        '<snippetOCS_VB_ChangeTracking_NewAnchorCommand>
        Dim selectNewAnchorCommand As New SqlCommand()
        Dim newAnchorVariable As String = "@" + SyncSession.SyncNewReceivedAnchor
        With selectNewAnchorCommand
            .CommandText = _
                "SELECT " + newAnchorVariable + " = change_tracking_current_version()"
            .Parameters.Add(newAnchorVariable, SqlDbType.BigInt)
            .Parameters(newAnchorVariable).Direction = ParameterDirection.Output
            .Connection = serverConn
        End With
        Me.SelectNewAnchorCommand = selectNewAnchorCommand
        '</snippetOCS_VB_ChangeTracking_NewAnchorCommand>

        'Create a SyncAdapter for the Customer table, and then define
        'the commands to synchronize changes:
        '* SelectIncrementalInsertsCommand, SelectIncrementalUpdatesCommand,
        '  and SelectIncrementalDeletesCommand are used to select changes
        '  from the server that the client provider then applies to the client.
        '* InsertCommand, UpdateCommand, and DeleteCommand are used to apply
        '  to the server the changes that the client provider has selected
        '  from the client.
        '* SelectConflictUpdatedRowsCommand SelectConflictDeletedRowsCommand
        '  are used to detect if there are conflicts on the server during
        '  synchronization.
        'The commands reference the change tracking table that is configured
        'for the Customer table.
        'Create the SyncAdapter.
        Dim customerSyncAdapter As New SyncAdapter("Customer")

        'Select inserts from the server.
        '<snippetOCS_VB_ChangeTracking_CustomerIncrInsert>
        Dim customerIncrInserts As New SqlCommand()
        With customerIncrInserts
            .CommandText = _
                "IF @sync_initialized = 0 " _
                    & "SELECT Sales.Customer.[CustomerId], [CustomerName], [SalesPerson], [CustomerType] " _
                    & "FROM Sales.Customer LEFT OUTER JOIN " _
                    & "CHANGETABLE(CHANGES Sales.Customer, @sync_last_received_anchor) CT " _
                    & "ON CT.[CustomerId] = Sales.Customer.[CustomerId] " _
                    & "WHERE (CT.SYS_CHANGE_CONTEXT IS NULL OR CT.SYS_CHANGE_CONTEXT <> @sync_client_id_binary) " _
                & "ELSE  " _
                & "BEGIN " _
                    & "SELECT Sales.Customer.[CustomerId], [CustomerName], [SalesPerson], [CustomerType] " _
                    & "FROM Sales.Customer JOIN CHANGETABLE(CHANGES Sales.Customer, @sync_last_received_anchor) CT " _
                    & "ON CT.[CustomerId] = Sales.Customer.[CustomerId] " _
                    & "WHERE (CT.SYS_CHANGE_OPERATION = 'I' AND CT.SYS_CHANGE_CREATION_VERSION " _
                    & "<= @sync_new_received_anchor " _
                    & "AND (CT.SYS_CHANGE_CONTEXT IS NULL OR CT.SYS_CHANGE_CONTEXT <> @sync_client_id_binary)); " _
                    & "IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(@sync_table_name)) " _
                    & "> @sync_last_received_anchor " _
                    & "RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. " _
                    & "To recover from this error, the client must reinitialize its local database and try again' " _
                    & ",16,3,@sync_table_name)  " _
                & "END"
            .Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Int)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.BigInt)
            .Parameters.Add("@" + SyncSession.SyncClientIdBinary, SqlDbType.Binary)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.BigInt)
            .Parameters.Add("@" + SyncSession.SyncTableName, SqlDbType.NVarChar)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectIncrementalInsertsCommand = customerIncrInserts
        '</snippetOCS_VB_ChangeTracking_CustomerIncrInsert>
        'Apply inserts to the server.
        Dim customerInserts As New SqlCommand()
        With customerInserts
            .CommandText = _
                  ";WITH CHANGE_TRACKING_CONTEXT (@sync_client_id_binary) " _
                & "INSERT INTO Sales.Customer ([CustomerId], [CustomerName], [SalesPerson], [CustomerType]) " _
                & "VALUES (@CustomerId, @CustomerName, @SalesPerson, @CustomerType) " _
                & "SET @sync_row_count = @@rowcount; " _
                & "IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(@sync_table_name)) > @sync_last_received_anchor " _
                    & "RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. " _
                    & "To recover from this error, the client must reinitialize its local database and try again'" _
                    & ",16,3,@sync_table_name)"
            .Parameters.Add("@" + SyncSession.SyncClientIdBinary, SqlDbType.Binary)
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@CustomerName", SqlDbType.NVarChar)
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@CustomerType", SqlDbType.NVarChar)
            .Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int)
            .Parameters("@" + SyncSession.SyncRowCount).Direction = ParameterDirection.Output
            .Parameters.Add("@" + SyncSession.SyncTableName, SqlDbType.NVarChar)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.BigInt)
            .Connection = serverConn
        End With
        customerSyncAdapter.InsertCommand = customerInserts

        'Select updates from the server.
        '<snippetOCS_VB_ChangeTracking_CustomerIncrUpdate>
        Dim customerIncrUpdates As New SqlCommand()
        With customerIncrUpdates
            .CommandText = _
                  "IF @sync_initialized > 0  " _
                & "BEGIN " _
                    & "SELECT Sales.Customer.[CustomerId], [CustomerName], [SalesPerson], [CustomerType] " _
                    & "FROM Sales.Customer JOIN " _
                    & "CHANGETABLE(CHANGES Sales.Customer, @sync_last_received_anchor) CT " _
                    & "ON CT.[CustomerId] = Sales.Customer.[CustomerId] " _
                    & "WHERE (CT.SYS_CHANGE_OPERATION = 'U' AND CT.SYS_CHANGE_VERSION " _
                    & "<= @sync_new_received_anchor " _
                    & "AND (CT.SYS_CHANGE_CONTEXT IS NULL OR CT.SYS_CHANGE_CONTEXT <> @sync_client_id_binary)); " _
                    & "IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(@sync_table_name)) " _
                    & "> @sync_last_received_anchor " _
                    & "RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. " _
                    & "To recover from this error, the client must reinitialize its local database and try again'" _
                    & ",16,3,@sync_table_name)  " _
                & "END"
            .Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Int)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.BigInt)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.BigInt)
            .Parameters.Add("@" + SyncSession.SyncClientIdBinary, SqlDbType.Binary)
            .Parameters.Add("@" + SyncSession.SyncTableName, SqlDbType.NVarChar)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectIncrementalUpdatesCommand = customerIncrUpdates
        '</snippetOCS_VB_ChangeTracking_CustomerIncrUpdate>
        'Apply updates to the server.
        '<snippetOCS_VB_ChangeTracking_CustomerUpdate>
        Dim customerUpdates As New SqlCommand()
        With customerUpdates
            .CommandText = _
                  ";WITH CHANGE_TRACKING_CONTEXT (@sync_client_id_binary) " _
                & "UPDATE Sales.Customer " _
                & "SET [CustomerName] = @CustomerName, [SalesPerson] = @SalesPerson, [CustomerType] = @CustomerType " _
                & "FROM Sales.Customer  " _
                & "JOIN CHANGETABLE(VERSION Sales.Customer, ([CustomerId]), (@CustomerId)) CT  " _
                & "ON CT.[CustomerId] = Sales.Customer.[CustomerId] " _
                & "WHERE (@sync_force_write = 1 " _
                & "OR CT.SYS_CHANGE_VERSION IS NULL OR CT.SYS_CHANGE_VERSION <= @sync_last_received_anchor " _
                & "OR (CT.SYS_CHANGE_CONTEXT IS NOT NULL AND CT.SYS_CHANGE_CONTEXT = @sync_client_id_binary)) " _
                & "SET @sync_row_count = @@rowcount; " _
                & "IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(@sync_table_name)) > @sync_last_received_anchor " _
                    & "RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. " _
                    & "To recover from this error, the client must reinitialize its local database and try again'" _
                    & ",16,3,@sync_table_name)"
            .Parameters.Add("@" + SyncSession.SyncClientIdBinary, SqlDbType.Binary)
            .Parameters.Add("@CustomerName", SqlDbType.NVarChar)
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@CustomerType", SqlDbType.NVarChar)
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" + SyncSession.SyncForceWrite, SqlDbType.Bit)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.BigInt)
            .Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int)
            .Parameters("@" + SyncSession.SyncRowCount).Direction = ParameterDirection.Output
            .Parameters.Add("@" + SyncSession.SyncTableName, SqlDbType.NVarChar)
            .Connection = serverConn
        End With
        customerSyncAdapter.UpdateCommand = customerUpdates
        '</snippetOCS_VB_ChangeTracking_CustomerUpdate>
        'Select deletes from the server.
        Dim customerIncrDeletes As New SqlCommand()
        With customerIncrDeletes
            .CommandText = _
                  "IF @sync_initialized > 0  " _
                & "BEGIN " _
                    & "SELECT CT.[CustomerId] FROM CHANGETABLE(CHANGES Sales.Customer, @sync_last_received_anchor) CT " _
                    & "WHERE (CT.SYS_CHANGE_OPERATION = 'D' AND CT.SYS_CHANGE_VERSION " _
                    & "<= @sync_new_received_anchor " _
                    & "AND (CT.SYS_CHANGE_CONTEXT IS NULL OR CT.SYS_CHANGE_CONTEXT <> @sync_client_id_binary)); " _
                    & "IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(@sync_table_name)) " _
                    & "> @sync_last_received_anchor " _
                    & "RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. " _
                    & "To recover from this error, the client must reinitialize its local database and try again'" _
                    & ",16,3,@sync_table_name)  " _
                & "END"
            .Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Int)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.BigInt)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.BigInt)
            .Parameters.Add("@" + SyncSession.SyncClientIdBinary, SqlDbType.Binary)
            .Parameters.Add("@" + SyncSession.SyncTableName, SqlDbType.NVarChar)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectIncrementalDeletesCommand = customerIncrDeletes

        'Apply deletes to the server.            
        Dim customerDeletes As New SqlCommand()
        With customerDeletes
            .CommandText = _
                  ";WITH CHANGE_TRACKING_CONTEXT (@sync_client_id_binary) " _
                & "DELETE Sales.Customer FROM Sales.Customer " _
                & "JOIN CHANGETABLE(VERSION Sales.Customer, ([CustomerId]), (@CustomerId)) CT  " _
                & "ON CT.[CustomerId] = Sales.Customer.[CustomerId] " _
                & "WHERE (@sync_force_write = 1 " _
                & "OR CT.SYS_CHANGE_VERSION IS NULL OR CT.SYS_CHANGE_VERSION <= @sync_last_received_anchor " _
                & "OR (CT.SYS_CHANGE_CONTEXT IS NOT NULL AND CT.SYS_CHANGE_CONTEXT = @sync_client_id_binary)) " _
                & "SET @sync_row_count = @@rowcount; " _
                & "IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(@sync_table_name)) > @sync_last_received_anchor " _
                    & "RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. " _
                    & "To recover from this error, the client must reinitialize its local database and try again'" _
                    & ",16,3,@sync_table_name)"
            .Parameters.Add("@" + SyncSession.SyncClientIdBinary, SqlDbType.Binary)
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" + SyncSession.SyncForceWrite, SqlDbType.Bit)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.BigInt)
            .Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int)
            .Parameters("@" + SyncSession.SyncRowCount).Direction = ParameterDirection.Output
            .Parameters.Add("@" + SyncSession.SyncTableName, SqlDbType.NVarChar)
            .Connection = serverConn
        End With
        customerSyncAdapter.DeleteCommand = customerDeletes

        'This command is used if @sync_row_count returns
        '0 when changes are applied to the server.
        '<snippetOCS_VB_ChangeTracking_SelectConflictUpdatedRowsCommand>
        Dim customerUpdateConflicts As New SqlCommand()
        With customerUpdateConflicts
            .CommandText = _
                  "SELECT Sales.Customer.[CustomerId], [CustomerName], [SalesPerson], [CustomerType], " _
                & "CT.SYS_CHANGE_CONTEXT, CT.SYS_CHANGE_VERSION " _
                & "FROM Sales.Customer JOIN CHANGETABLE(VERSION Sales.Customer, ([CustomerId]), (@CustomerId)) CT  " _
                & "ON CT.[CustomerId] = Sales.Customer.[CustomerId]"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectConflictUpdatedRowsCommand = customerUpdateConflicts
        '</snippetOCS_VB_ChangeTracking_SelectConflictUpdatedRowsCommand>

        'This command is used if the server provider cannot find
        'a row in the base table.
        '<snippetOCS_VB_ChangeTracking_SelectConflictDeletedRowsCommand>
        Dim customerDeleteConflicts As New SqlCommand()
        With customerDeleteConflicts
            .CommandText = _
                  "SELECT CT.[CustomerId], " _
                & "CT.SYS_CHANGE_CONTEXT, CT.SYS_CHANGE_VERSION " _
                & "FROM CHANGETABLE(CHANGES Sales.Customer, @sync_last_received_anchor) CT " _
                & "WHERE (CT.[CustomerId] = @CustomerId AND CT.SYS_CHANGE_OPERATION = 'D')"
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.BigInt)
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectConflictDeletedRowsCommand = customerDeleteConflicts
        '</snippetOCS_VB_ChangeTracking_SelectConflictDeletedRowsCommand>

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
        'to specify literal defaults with .Columns[ColName].DefaultValue;
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
        Console.WriteLine("Complete Time: " + syncStatistics.SyncCompleteTime)
        Console.WriteLine(String.Empty)

    End Sub 'DisplayStats
End Class 'SampleStats
'</snippetOCS_VB_ChangeTracking>