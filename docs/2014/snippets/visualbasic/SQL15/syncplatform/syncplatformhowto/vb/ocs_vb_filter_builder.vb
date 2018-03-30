'<snippetOCS_VB_Filter_Builder>
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

        '<snippetOCS_VB_Filter_Builder_SyncGroupAndTables>
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
        '</snippetOCS_VB_Filter_Builder_SyncGroupAndTables>

        'Specify a value for the @SalesPerson parameter that is added
        'in the server synchronization provider. This value would
        'typically be provided by a user in the application, but we
        'have hardcoded it here for convenience.
        '<snippetOCS_VB_Filter_Builder_AgentSyncParam>
        Me.Configuration.SyncParameters.Add(New SyncParameter("@SalesPerson", "Brenda Diaz"))
        '</snippetOCS_VB_Filter_Builder_AgentSyncParam>

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

        'Create a filter parameter that will be used in the filter clause for
        'all three tables.
        '<snippetOCS_VB_Filter_Builder_ProviderSyncParam>
        Dim filterParameter As New SqlParameter("@SalesPerson", SqlDbType.NVarChar)
        '</snippetOCS_VB_Filter_Builder_ProviderSyncParam>

        'Create SyncAdapters for each table by using the SqlSyncAdapterBuilder:
        '  * Specify the base table and tombstone table names.
        '  * Specify the columns that are used to track when
        '    changes are made.
        '  * Specify download-only synchronization.
        '  * Specify if you want only certain columns at the client.
        '  * Specify filter clauses for the base tables and tombstone
        '    tables.
        '  * Call ToSyncAdapter to create the SyncAdapter.
        '  * Specify a name for the SyncAdapter that matches the
        '    the name that is specified for the corresponding SyncTable.
        '    Do not include the schema names (Sales in this case).

        'Customer table.
        Dim customerBuilder As New SqlSyncAdapterBuilder(serverConn)
        With customerBuilder
            .TableName = "Sales.Customer"
            .TombstoneTableName = customerBuilder.TableName + "_Tombstone"
            .SyncDirection = SyncDirection.DownloadOnly
            .CreationTrackingColumn = "InsertTimestamp"
            .UpdateTrackingColumn = "UpdateTimestamp"
            .DeletionTrackingColumn = "DeleteTimestamp"
        End With

        'Specify the columns that you want at the client. If you
        'want all columns, this code is not required. In this
        'case, we filter out SalesPerson.
        '<snippetOCS_VB_Filter_Builder_CustomerColumnFilter>
        Dim customerDataColumns(2) As String
        customerDataColumns(0) = "CustomerId"
        customerDataColumns(1) = "CustomerName"
        customerDataColumns(2) = "CustomerType"
        customerBuilder.DataColumns.AddRange(customerDataColumns)
        customerBuilder.TombstoneDataColumns.AddRange(customerDataColumns)
        '</snippetOCS_VB_Filter_Builder_CustomerColumnFilter>

        'Specify a filter clause, which is an SQL WHERE clause
        'without the WHERE keyword. Use the parameter that is 
        'created above. The value for the parameter is specified 
        'in the SyncAgent Configuration object.
        '<snippetOCS_VB_Filter_Builder_CustomerRowFilter>
        Dim customerFilterClause As String = "SalesPerson=@SalesPerson"
        With customerBuilder
            .FilterClause = customerFilterClause
            .FilterParameters.Add(filterParameter)
            .TombstoneFilterClause = customerFilterClause
            .TombstoneFilterParameters.Add(filterParameter)
        End With
        '</snippetOCS_VB_Filter_Builder_CustomerRowFilter>

        Dim customerSyncAdapter As SyncAdapter = customerBuilder.ToSyncAdapter()
        customerSyncAdapter.TableName = "Customer"
        Me.SyncAdapters.Add(customerSyncAdapter)


        'OrderHeader table.
        Dim orderHeaderBuilder As New SqlSyncAdapterBuilder(serverConn)
        With orderHeaderBuilder
            .TableName = "Sales.OrderHeader"
            .TombstoneTableName = orderHeaderBuilder.TableName + "_Tombstone"
            .SyncDirection = SyncDirection.DownloadOnly
            .CreationTrackingColumn = "InsertTimestamp"
            .UpdateTrackingColumn = "UpdateTimestamp"
            .DeletionTrackingColumn = "DeleteTimestamp"
        End With

        'Filter properties: extend the filter to the OrderHeader table.
        '<snippetOCS_VB_Filter_Builder_OrderHeaderRowFilter>
        Dim orderHeaderFilterClause As String = _
            "CustomerId IN (SELECT CustomerId FROM Sales.Customer " _
                & "WHERE SalesPerson=@SalesPerson)"
        With orderHeaderBuilder
            .FilterClause = orderHeaderFilterClause
            .FilterParameters.Add(filterParameter)
            .TombstoneFilterClause = orderHeaderFilterClause
            .TombstoneFilterParameters.Add(filterParameter)
        End With
        '</snippetOCS_VB_Filter_Builder_OrderHeaderRowFilter>

        Dim orderHeaderSyncAdapter As SyncAdapter = orderHeaderBuilder.ToSyncAdapter()
        orderHeaderSyncAdapter.TableName = "OrderHeader"
        Me.SyncAdapters.Add(orderHeaderSyncAdapter)


        'OrderDetail table.
        Dim orderDetailBuilder As New SqlSyncAdapterBuilder(serverConn)
        With orderDetailBuilder
            .TableName = "Sales.OrderDetail"
            .TombstoneTableName = orderDetailBuilder.TableName + "_Tombstone"
            .SyncDirection = SyncDirection.DownloadOnly
            .CreationTrackingColumn = "InsertTimestamp"
            .UpdateTrackingColumn = "UpdateTimestamp"
            .DeletionTrackingColumn = "DeleteTimestamp"

            'Filter properties: extend the filter to the OrderDetail table.
            Dim orderDetailFilterClause As String = _
                "OrderId IN (SELECT OrderId FROM Sales.OrderHeader " _
                    & "WHERE CustomerId IN " _
                        & "(SELECT CustomerId FROM Sales.Customer " _
                            & "WHERE SalesPerson=@SalesPerson))"
            .FilterClause = orderDetailFilterClause
            .FilterParameters.Add(filterParameter)
            .TombstoneFilterClause = orderDetailFilterClause
            .TombstoneFilterParameters.Add(filterParameter)
        End With
        Dim orderDetailSyncAdapter As SyncAdapter = orderDetailBuilder.ToSyncAdapter()
        orderDetailSyncAdapter.TableName = "OrderDetail"
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
'</snippetOCS_VB_Filter_Builder>