'<snippetOCS_VB_DownloadOnly>
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

        'Make changes on the server.
        util.MakeDataChangesOnServer("Customer")

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
        'for the single table we are synchronizing; it is typically
        'used so that changes to multiple related tables are 
        'synchronized at the same time.
        Dim customerSyncGroup As New SyncGroup("Customer")

        'Add the Customer table: specify a synchronization direction of
        'DownloadOnly and that an existing table should be dropped.
        '<snippetOCS_VB_DownloadOnly_CustomerSyncTable>
        Dim customerSyncTable As New SyncTable("Customer")
        customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        customerSyncTable.SyncDirection = SyncDirection.DownloadOnly
        customerSyncTable.SyncGroup = customerSyncGroup
        Me.Configuration.SyncTables.Add(customerSyncTable)
        '</snippetOCS_VB_DownloadOnly_CustomerSyncTable>

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
        '<snippetOCS_VB_DownloadOnly_NewAnchorCommand>
        Dim selectNewAnchorCommand As New SqlCommand()
        Dim newAnchorVariable As String = "@" + SyncSession.SyncNewReceivedAnchor
        With selectNewAnchorCommand
            .CommandText = "SELECT " + newAnchorVariable + " = min_active_rowversion() - 1"
            .Parameters.Add(newAnchorVariable, SqlDbType.Timestamp)
            .Parameters(newAnchorVariable).Direction = ParameterDirection.Output
            .Connection = serverConn
        End With
        Me.SelectNewAnchorCommand = selectNewAnchorCommand
        '</snippetOCS_VB_DownloadOnly_NewAnchorCommand>

        'Create a SyncAdapter for the Customer table, and then define
        'the commands to synchronize changes:
        '* SelectIncrementalInsertsCommand, SelectIncrementalUpdatesCommand,
        '  and SelectIncrementalDeletesCommand are used to select changes
        '  from the server that the client provider then applies to the client.           

        'Create the SyncAdapter.
        Dim customerSyncAdapter As New SyncAdapter("Customer")

        'Select inserts from the server.
        Dim customerIncrInserts As New SqlCommand()
        With customerIncrInserts
            .CommandText = _
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " _
              & "FROM Sales.Customer " _
              & "WHERE (InsertTimestamp > @sync_last_received_anchor " _
              & "AND InsertTimestamp <= @sync_new_received_anchor)"
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectIncrementalInsertsCommand = customerIncrInserts

        'Select updates from the server.
        '<snippetOCS_VB_DownloadOnly_CustomerIncrUpdate>
        Dim customerIncrUpdates As New SqlCommand()
        With customerIncrUpdates
            .CommandText = _
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " _
              & "FROM Sales.Customer " _
              & "WHERE (UpdateTimestamp > @sync_last_received_anchor " _
              & "AND UpdateTimestamp <= @sync_new_received_anchor " _
              & "AND NOT (InsertTimestamp > @sync_last_received_anchor))"
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectIncrementalUpdatesCommand = customerIncrUpdates
        '</snippetOCS_VB_DownloadOnly_CustomerIncrUpdate>

        'Select deletes from the server.
        Dim customerIncrDeletes As New SqlCommand()
        With customerIncrDeletes
            .CommandText = _
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " _
              & "FROM Sales.Customer_Tombstone " _
              & "WHERE (@sync_initialized = 1 " _
              & "AND DeleteTimestamp > @sync_last_received_anchor " _
              & "AND DeleteTimestamp <= @sync_new_received_anchor)"
            .Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Bit)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectIncrementalDeletesCommand = customerIncrDeletes

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

        'Unlike Bidirectional and UploadOnly synchronization, we do
        'not need to make schema changes for the Customer table. In
        'those scenarios, we add defaults because inserts are made
        'directly into the client database. We handle the events here
        'so that we can write to the screen.
        AddHandler Me.CreatingSchema, AddressOf SampleClientSyncProvider_CreatingSchema
        AddHandler Me.SchemaCreated, AddressOf SampleClientSyncProvider_SchemaCreated

    End Sub 'New


    Private Sub SampleClientSyncProvider_CreatingSchema(ByVal sender As Object, ByVal e As CreatingSchemaEventArgs)

        Console.Write("Creating schema for " + e.Table.TableName + " | ")

    End Sub 'SampleClientSyncProvider_CreatingSchema


    Private Sub SampleClientSyncProvider_SchemaCreated(ByVal sender As Object, ByVal e As SchemaCreatedEventArgs)

        Console.WriteLine("Schema created for " + e.Table.TableName)

    End Sub 'SampleClientSyncProvider_SchemaCreated
End Class 'SampleClientSyncProvider

'Handle the statistics returned by the SyncAgent.

Public Class SampleStats

    Public Sub DisplayStats(ByVal syncStatistics As SyncStatistics, ByVal syncType As String)
        Console.WriteLine(String.Empty)
        If syncType = "initial" Then
            Console.WriteLine("****** Initial Synchronization ******")
        ElseIf syncType = "subsequent" Then
            Console.WriteLine("***** Subsequent Synchronization ****")
        End If

        Console.WriteLine("Start Time: " & syncStatistics.SyncStartTime)
        Console.WriteLine("Total Changes Downloaded: " & syncStatistics.TotalChangesDownloaded)
        Console.WriteLine("Complete Time: " & syncStatistics.SyncCompleteTime)
        Console.WriteLine(String.Empty)

    End Sub 'DisplayStats
End Class 'SampleStats
'</snippetOCS_VB_DownloadOnly>