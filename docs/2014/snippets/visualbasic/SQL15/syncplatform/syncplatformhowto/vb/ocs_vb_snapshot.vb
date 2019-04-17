'<snippetOCS_VB_Snapshot>
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

        'Subsequent synchronization. There was one insert,
        'one update, and one delete made on the server;
        'therefore, the row count is identical, but the
        'data is different.
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

        'Create two SyncGroups so that changes to OrderHeader
        'and OrderDetail are made in one transaction. Depending on
        'application requirements, you might include Customer
        'in the same group.
        Dim customerSyncGroup As New SyncGroup("Customer")
        Dim orderSyncGroup As New SyncGroup("Order")

        'Add each table: specify a synchronization direction of
        'Snapshot, and that any existing tables should be dropped.
        '<snippetOCS_VB_Snapshot_CustomerSyncTable>
        Dim customerSyncTable As New SyncTable("Customer")
        customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        customerSyncTable.SyncDirection = SyncDirection.Snapshot
        customerSyncTable.SyncGroup = customerSyncGroup
        Me.Configuration.SyncTables.Add(customerSyncTable)
        '</snippetOCS_VB_Snapshot_CustomerSyncTable>

        Dim orderHeaderSyncTable As New SyncTable("OrderHeader")
        orderHeaderSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        orderHeaderSyncTable.SyncDirection = SyncDirection.Snapshot
        orderHeaderSyncTable.SyncGroup = orderSyncGroup
        Me.Configuration.SyncTables.Add(orderHeaderSyncTable)

        Dim orderDetailSyncTable As New SyncTable("OrderDetail")
        orderDetailSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        orderDetailSyncTable.SyncDirection = SyncDirection.Snapshot
        orderDetailSyncTable.SyncGroup = orderSyncGroup
        Me.Configuration.SyncTables.Add(orderDetailSyncTable)

    End Sub 'New
End Class 'SampleSyncAgent

'Create a class that is derived from 
'Microsoft.Synchronization.Server.DbServerSyncProvider.
'<snippetOCS_VB_Snapshot_SampleServerSyncProvider>
Public Class SampleServerSyncProvider
    Inherits DbServerSyncProvider

    Public Sub New()
        'Create a connection to the sample server database.
        Dim util As New Utility()
        Dim serverConn As New SqlConnection(util.ServerConnString)
        Me.Connection = serverConn

        'Create a SyncAdapter for each table, and then define
        'the command to select rows from the table. With the Snapshot
        'option, you do not download incremental changes. However,
        'you still use the SelectIncrementalInsertsCommand to select
        'the rows to download for each snapshot. The commands include
        'only those columns that you want on the client.
        'Customer table.
        '<snippetOCS_VB_Snapshot_CustomerIncrInsert>
        Dim customerSyncAdapter As New SyncAdapter("Customer")
        Dim customerIncrInserts As New SqlCommand()
        customerIncrInserts.CommandText = _
            "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " _
          & "FROM Sales.Customer"
        customerIncrInserts.Connection = serverConn
        customerSyncAdapter.SelectIncrementalInsertsCommand = customerIncrInserts
        Me.SyncAdapters.Add(customerSyncAdapter)
        '</snippetOCS_VB_Snapshot_CustomerIncrInsert>

        'OrderHeader table.
        Dim orderHeaderSyncAdapter As New SyncAdapter("OrderHeader")
        Dim orderHeaderIncrInserts As New SqlCommand()
        orderHeaderIncrInserts.CommandText = _
            "SELECT OrderId, CustomerId, OrderDate, OrderStatus " _
          & "FROM Sales.OrderHeader"
        orderHeaderIncrInserts.Connection = serverConn
        orderHeaderSyncAdapter.SelectIncrementalInsertsCommand = orderHeaderIncrInserts
        Me.SyncAdapters.Add(orderHeaderSyncAdapter)

        'OrderDetail table.
        Dim orderDetailSyncAdapter As New SyncAdapter("OrderDetail")
        Dim orderDetailIncrInserts As New SqlCommand()
        orderDetailIncrInserts.CommandText = _
            "SELECT OrderDetailId, OrderId, Product, Quantity " _
          & "FROM Sales.OrderDetail"
        orderDetailIncrInserts.Connection = serverConn
        orderDetailSyncAdapter.SelectIncrementalInsertsCommand = orderDetailIncrInserts
        Me.SyncAdapters.Add(orderDetailSyncAdapter)

    End Sub 'New
End Class 'SampleServerSyncProvider
'</snippetOCS_VB_Snapshot_SampleServerSyncProvider>

'Create a class that is derived from 
'Microsoft.Synchronization.Data.SqlServerCe.SqlCeClientSyncProvider.
'You can just instantiate the provider directly and associate it
'with the SyncAgent, but you could use this class to handle client 
'provider events and other client-side processing.
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
'</snippetOCS_VB_Snapshot>