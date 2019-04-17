'<snippetOCS_VB_Batching>
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
        'directly related to synchronization, such as holding connection 
        'string information and making changes to the server and client databases.
        Dim util As New Utility()

        'The SampleStats class handles information from the 
        'SyncStatistics object that the Synchronize method returns and
        'from SyncAgent events.
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
        util.MakeDataChangesOnServer("CustomerAndOrderHeader")

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

        '<snippetOCS_VB_Batching_SyncGroupAndTables>
        'Create a SyncGroup so that changes to Customer
        'and OrderHeader are made in one transaction.
        Dim customerOrderSyncGroup As New SyncGroup("CustomerOrder")

        'Add each table: specify a synchronization direction of
        'DownloadOnly.
        Dim customerSyncTable As New SyncTable("Customer")
        customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        customerSyncTable.SyncDirection = SyncDirection.DownloadOnly
        customerSyncTable.SyncGroup = customerOrderSyncGroup
        Me.Configuration.SyncTables.Add(customerSyncTable)

        Dim orderHeaderSyncTable As New SyncTable("OrderHeader")
        orderHeaderSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        orderHeaderSyncTable.SyncDirection = SyncDirection.DownloadOnly
        orderHeaderSyncTable.SyncGroup = customerOrderSyncGroup
        Me.Configuration.SyncTables.Add(orderHeaderSyncTable)

    End Sub 'New '</snippetOCS_VB_Batching_SyncGroupAndTables>
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
        'the server. In this case, we call a stored procedure
        'that returns an anchor that can be used with batches
        'of changes.
        '<snippetOCS_VB_Batching_NewAnchorCommand>
        Dim selectNewAnchorCommand As New SqlCommand()
        selectNewAnchorCommand.Connection = serverConn
        selectNewAnchorCommand.CommandText = "usp_GetNewBatchAnchor"
        selectNewAnchorCommand.CommandType = CommandType.StoredProcedure
        selectNewAnchorCommand.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp, 8)
        selectNewAnchorCommand.Parameters.Add("@" + SyncSession.SyncMaxReceivedAnchor, SqlDbType.Timestamp, 8)
        selectNewAnchorCommand.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp, 8)
        selectNewAnchorCommand.Parameters.Add("@" + SyncSession.SyncBatchSize, SqlDbType.Int, 4)
        selectNewAnchorCommand.Parameters.Add("@" + SyncSession.SyncBatchCount, SqlDbType.Int, 4)

        selectNewAnchorCommand.Parameters("@" + SyncSession.SyncMaxReceivedAnchor).Direction = ParameterDirection.Output
        selectNewAnchorCommand.Parameters("@" + SyncSession.SyncNewReceivedAnchor).Direction = ParameterDirection.Output
        selectNewAnchorCommand.Parameters("@" + SyncSession.SyncBatchCount).Direction = ParameterDirection.InputOutput
        Me.SelectNewAnchorCommand = selectNewAnchorCommand
        Me.BatchSize = 50
        '</snippetOCS_VB_Batching_NewAnchorCommand>
        'Create SyncAdapters for each table by using the SqlSyncAdapterBuilder:
        '  * Specify the base table and tombstone table names.
        '  * Specify the columns that are used to track when
        '    and where changes are made.
        '  * Specify download only synchronization.
        '  * Call ToSyncAdapter to create the SyncAdapter.
        '  * Specify a name for the SyncAdapter that matches the
        '    the name specified for the corresponding SyncTable.
        '    Do not include the schema names (Sales in this case).
        'Customer table
        Dim customerBuilder As New SqlSyncAdapterBuilder(serverConn)

        customerBuilder.TableName = "Sales.Customer"
        customerBuilder.TombstoneTableName = customerBuilder.TableName + "_Tombstone"
        customerBuilder.SyncDirection = SyncDirection.DownloadOnly
        customerBuilder.CreationTrackingColumn = "InsertTimestamp"
        customerBuilder.UpdateTrackingColumn = "UpdateTimestamp"
        customerBuilder.DeletionTrackingColumn = "DeleteTimestamp"

        Dim customerSyncAdapter As SyncAdapter = customerBuilder.ToSyncAdapter()
        customerSyncAdapter.TableName = "Customer"
        Me.SyncAdapters.Add(customerSyncAdapter)


        'OrderHeader table.
        Dim orderHeaderBuilder As New SqlSyncAdapterBuilder(serverConn)

        orderHeaderBuilder.TableName = "Sales.OrderHeader"
        orderHeaderBuilder.TombstoneTableName = orderHeaderBuilder.TableName + "_Tombstone"
        orderHeaderBuilder.SyncDirection = SyncDirection.DownloadOnly
        orderHeaderBuilder.CreationTrackingColumn = "InsertTimestamp"
        orderHeaderBuilder.UpdateTrackingColumn = "UpdateTimestamp"
        orderHeaderBuilder.DeletionTrackingColumn = "DeleteTimestamp"

        Dim orderHeaderSyncAdapter As SyncAdapter = orderHeaderBuilder.ToSyncAdapter()
        orderHeaderSyncAdapter.TableName = "OrderHeader"
        Me.SyncAdapters.Add(orderHeaderSyncAdapter)

        'Handle the ChangesSelected event, and display
        'information to the console.
        AddHandler Me.ChangesSelected, AddressOf SampleServerSyncProvider_ChangesSelected

    End Sub 'New


    Public Sub SampleServerSyncProvider_ChangesSelected(ByVal sender As Object, ByVal e As ChangesSelectedEventArgs)
        Console.WriteLine("Total number of batches: " & e.Context.BatchCount)
        Console.WriteLine("Changes applied for group " & e.GroupMetadata.GroupName)
        Console.WriteLine("Inserts applied for group: " & e.Context.GroupProgress.TotalInserts.ToString())
        Console.WriteLine("Updates applied for group: " & e.Context.GroupProgress.TotalUpdates.ToString())
        Console.WriteLine("Deletes applied for group: " & e.Context.GroupProgress.TotalDeletes.ToString())

    End Sub 'SampleServerSyncProvider_ChangesSelected
End Class 'SampleServerSyncProvider

'Create a class that is derived from 
'Microsoft.Synchronization.Data.SqlServerCe.SqlCeClientSyncProvider.
Public Class SampleClientSyncProvider
    Inherits SqlCeClientSyncProvider


    Public Sub New()
        'Specify a connection string for the sample client database.
        Dim util As New Utility()
        Me.ConnectionString = util.ClientConnString

    End Sub 'New
End Class 'SampleClientSyncProvider

'Handle the statistics that are returned by the SyncAgent.
Public Class SampleStats

    Public Sub DisplayStats(ByVal syncStatistics As SyncStatistics, ByVal syncType As String)
        Console.WriteLine(String.Empty)
        If syncType = "initial" Then
            Console.WriteLine("****** Initial Synchronization Stats ******")
        ElseIf syncType = "subsequent" Then
            Console.WriteLine("***** Subsequent Synchronization Stats ****")
        End If

        Console.WriteLine("Start Time: " & syncStatistics.SyncStartTime)
        Console.WriteLine("Total Changes Downloaded: " & syncStatistics.TotalChangesDownloaded)
        Console.WriteLine("Complete Time: " & syncStatistics.SyncCompleteTime)
        Console.WriteLine(String.Empty)

    End Sub 'DisplayStats
End Class 'SampleStats
'</snippetOCS_VB_Batching>