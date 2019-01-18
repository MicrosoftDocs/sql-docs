'<snippetOCS_VB_Filter_Manual>
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

        'Create two SyncGroups so that changes to OrderHeader
        'and OrderDetail are made in one transaction. Depending on
        'application requirements, you might include Customer
        'in the same group.
        Dim customerSyncGroup As New SyncGroup("Customer")
        Dim orderSyncGroup As New SyncGroup("Order")

        'Add each table: specify a synchronization direction of
        'DownloadOnly.
        Dim customerSyncTable As New SyncTable("Customer")
        customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        customerSyncTable.SyncDirection = SyncDirection.DownloadOnly
        customerSyncTable.SyncGroup = customerSyncGroup
        Me.Configuration.SyncTables.Add(customerSyncTable)

        Dim orderHeaderSyncTable As New SyncTable("OrderHeader")
        orderHeaderSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        orderHeaderSyncTable.SyncDirection = SyncDirection.DownloadOnly
        orderHeaderSyncTable.SyncGroup = orderSyncGroup
        Me.Configuration.SyncTables.Add(orderHeaderSyncTable)

        Dim orderDetailSyncTable As New SyncTable("OrderDetail")
        orderDetailSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        orderDetailSyncTable.SyncDirection = SyncDirection.DownloadOnly
        orderDetailSyncTable.SyncGroup = orderSyncGroup
        Me.Configuration.SyncTables.Add(orderDetailSyncTable)

        'Specify a value for the @SalesPerson parameter that is added
        'in the server synchronization provider. This value would
        'typically be provided by a user in the application, but we
        'have hardcoded it here for convenience.
        '<snippetOCS_VB_Filter_Manual_AgentSyncParam>
        Me.Configuration.SyncParameters.Add(New SyncParameter("@SalesPerson", "Brenda Diaz"))
        '</snippetOCS_VB_Filter_Manual_AgentSyncParam>

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
        Dim selectNewAnchorCommand As New SqlCommand()
        Dim newAnchorVariable As String = "@" + SyncSession.SyncNewReceivedAnchor
        With selectNewAnchorCommand
            .CommandText = "SELECT " + newAnchorVariable + " = min_active_rowversion() - 1"
            .Parameters.Add(newAnchorVariable, SqlDbType.Timestamp)
            .Parameters(newAnchorVariable).Direction = ParameterDirection.Output
            .Connection = serverConn
        End With
        Me.SelectNewAnchorCommand = selectNewAnchorCommand

        'Create a SyncAdapter for each table, and then define
        'the commands to synchronize changes:
        '* SelectIncrementalInsertsCommand, SelectIncrementalUpdatesCommand,
        '  and SelectIncrementalDeletesCommand are used to select changes
        '  from the server that the client provider then applies to the client.
        '* Specify if you want only certain columns at the client by 
        '  using the SELECT statement in the command.
        '* Filter rows by using the WHERE clause in the command. 
        '  In this case, we filter out SalesPerson.
        '
        'Customer table.
        '
        'Create the SyncAdapter.
        Dim customerSyncAdapter As New SyncAdapter("Customer")

        'Select inserts from the server.
        '<snippetOCS_VB_Filter_Manual_CustomerColumnRowFilter>
        Dim customerIncrInserts As New SqlCommand()
        With customerIncrInserts
            .CommandText = _
                "SELECT CustomerId, CustomerName, CustomerType " _
              & "FROM Sales.Customer " _
              & "WHERE SalesPerson = @SalesPerson " _
              & "AND (InsertTimestamp > @sync_last_received_anchor " _
              & "AND InsertTimestamp <= @sync_new_received_anchor " _
              & "AND InsertId <> @sync_client_id)"
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectIncrementalInsertsCommand = customerIncrInserts
        '</snippetOCS_VB_Filter_Manual_CustomerColumnRowFilter>

        'Select updates from the server.
        Dim customerIncrUpdates As New SqlCommand()
        With customerIncrUpdates
            .CommandText = _
                "SELECT CustomerId, CustomerName, CustomerType " _
              & "FROM Sales.Customer " _
              & "WHERE SalesPerson = @SalesPerson " _
              & "AND (UpdateTimestamp > @sync_last_received_anchor " _
              & "AND UpdateTimestamp <= @sync_new_received_anchor " _
              & "AND UpdateId <> @sync_client_id " _
              & "AND NOT (InsertTimestamp > @sync_last_received_anchor " _
              & "AND InsertId <> @sync_client_id))"
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectIncrementalUpdatesCommand = customerIncrUpdates

        'Select deletes from the server.
        Dim customerIncrDeletes As New SqlCommand()
        With customerIncrDeletes
            .CommandText = _
                "SELECT CustomerId, CustomerName, CustomerType " _
              & "FROM Sales.Customer_Tombstone " _
              & "WHERE SalesPerson = @SalesPerson " _
              & "AND (@sync_initialized = 1 " _
              & "AND DeleteTimestamp > @sync_last_received_anchor " _
              & "AND DeleteTimestamp <= @sync_new_received_anchor " _
              & "AND DeleteId <> @sync_client_id)"
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Bit)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectIncrementalDeletesCommand = customerIncrDeletes

        'Add the SyncAdapter to the server synchronization provider.
        Me.SyncAdapters.Add(customerSyncAdapter)


        '
        'OrderHeader table.
        '
        'Create the SyncAdapter.
        Dim orderHeaderSyncAdapter As New SyncAdapter("OrderHeader")

        'Select inserts from the server.
        '<snippetOCS_VB_Filter_Manual_OrderHeaderColumnRowFilter>
        Dim orderHeaderIncrInserts As New SqlCommand()
        With orderHeaderIncrInserts
            .CommandText = _
                "SELECT oh.OrderId, oh.CustomerId, oh.OrderDate, oh.OrderStatus " _
              & "FROM Sales.OrderHeader oh " _
              & "JOIN Sales.Customer c ON oh.CustomerId = c.CustomerId " _
              & "WHERE c.SalesPerson = @SalesPerson " _
              & "AND (oh.InsertTimestamp > @sync_last_received_anchor " _
              & "AND oh.InsertTimestamp <= @sync_new_received_anchor " _
              & "AND oh.InsertId <> @sync_client_id)"
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        orderHeaderSyncAdapter.SelectIncrementalInsertsCommand = orderHeaderIncrInserts
        '</snippetOCS_VB_Filter_Manual_OrderHeaderColumnRowFilter>

        'Select updates from the server.
        Dim orderHeaderIncrUpdates As New SqlCommand()
        With orderHeaderIncrUpdates
            .CommandText = _
                "SELECT oh.OrderId, oh.CustomerId, oh.OrderDate, oh.OrderStatus " _
              & "FROM Sales.OrderHeader oh " _
              & "JOIN Sales.Customer c ON oh.CustomerId = c.CustomerId " _
              & "WHERE c.SalesPerson = @SalesPerson " _
              & "AND (oh.UpdateTimestamp > @sync_last_received_anchor " _
              & "AND oh.UpdateTimestamp <= @sync_new_received_anchor " _
              & "AND oh.UpdateId <> @sync_client_id " _
              & "AND NOT (oh.InsertTimestamp > @sync_last_received_anchor " _
              & "AND oh.InsertId <> @sync_client_id))"
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        orderHeaderSyncAdapter.SelectIncrementalUpdatesCommand = orderHeaderIncrUpdates

        'Select deletes from the server.
        Dim orderHeaderIncrDeletes As New SqlCommand()
        With orderHeaderIncrDeletes
            .CommandText = _
                "SELECT oht.OrderId, oht.CustomerId, oht.OrderDate, oht.OrderStatus " _
              & "FROM Sales.OrderHeader_Tombstone oht " _
              & "JOIN Sales.Customer c ON oht.CustomerId = c.CustomerId " _
              & "WHERE c.SalesPerson = @SalesPerson " _
              & "AND (@sync_initialized = 1 " _
              & "AND oht.DeleteTimestamp > @sync_last_received_anchor " _
              & "AND oht.DeleteTimestamp <= @sync_new_received_anchor " _
              & "AND oht.DeleteId <> @sync_client_id)"
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Bit)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        orderHeaderSyncAdapter.SelectIncrementalDeletesCommand = orderHeaderIncrDeletes

        'Add the SyncAdapter to the server synchronization provider.
        Me.SyncAdapters.Add(orderHeaderSyncAdapter)


        '
        'OrderDetail table.
        '
        'Create the SyncAdapter.
        Dim orderDetailSyncAdapter As New SyncAdapter("OrderDetail")

        'Select inserts from the server.
        Dim orderDetailIncrInserts As New SqlCommand()
        With orderDetailIncrInserts
            .CommandText = _
                "SELECT od.OrderDetailId, od.OrderId, od.Product, od.Quantity " _
              & "FROM Sales.OrderDetail od " _
              & "JOIN Sales.OrderHeader oh ON od.OrderId = oh.OrderId " _
              & "JOIN Sales.Customer c ON oh.CustomerId = c.CustomerId " _
              & "WHERE SalesPerson = @SalesPerson " _
              & "AND (od.InsertTimestamp > @sync_last_received_anchor " _
              & "AND od.InsertTimestamp <= @sync_new_received_anchor " _
              & "AND od.InsertId <> @sync_client_id)"
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        orderDetailSyncAdapter.SelectIncrementalInsertsCommand = orderDetailIncrInserts

        'Select updates from the server.
        Dim orderDetailIncrUpdates As New SqlCommand()
        With orderDetailIncrUpdates
            .CommandText = _
                "SELECT od.OrderDetailId, od.OrderId, od.Product, od.Quantity " _
              & "FROM Sales.OrderDetail od " _
              & "JOIN Sales.OrderHeader oh ON od.OrderId = oh.OrderId " _
              & "JOIN Sales.Customer c ON oh.CustomerId = c.CustomerId " _
              & "WHERE SalesPerson = @SalesPerson " _
              & "AND (od.UpdateTimestamp > @sync_last_received_anchor " _
              & "AND od.UpdateTimestamp <= @sync_new_received_anchor " _
              & "AND od.UpdateId <> @sync_client_id " _
              & "AND NOT (od.InsertTimestamp > @sync_last_received_anchor " _
              & "AND od.InsertId <> @sync_client_id))"
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        orderDetailSyncAdapter.SelectIncrementalUpdatesCommand = orderDetailIncrUpdates

        'Select deletes from the server.
        Dim orderDetailIncrDeletes As New SqlCommand()
        With orderDetailIncrDeletes
            .CommandText = _
                "SELECT odt.OrderDetailId, odt.OrderId, odt.Product, odt.Quantity " _
              & "FROM Sales.OrderDetail_Tombstone odt " _
              & "JOIN Sales.OrderHeader oh ON odt.OrderId = oh.OrderId " _
              & "JOIN Sales.Customer c ON oh.CustomerId = c.CustomerId " _
              & "WHERE SalesPerson = @SalesPerson " _
              & "AND (@sync_initialized = 1 " _
              & "AND odt.DeleteTimestamp > @sync_last_received_anchor " _
              & "AND odt.DeleteTimestamp <= @sync_new_received_anchor " _
              & "AND odt.DeleteId <> @sync_client_id)"
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Bit)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        orderDetailSyncAdapter.SelectIncrementalDeletesCommand = orderDetailIncrDeletes

        'Add the SyncAdapter to the server synchronization provider.
        Me.SyncAdapters.Add(orderDetailSyncAdapter)

    End Sub 'New 
End Class 'SampleServerSyncProvider

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
'</snippetOCS_VB_Filter_Manual>