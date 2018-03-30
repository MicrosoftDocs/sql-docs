'<snippetOCS_VB_Init>
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

        'Create the Customer table on the client by using SQL. We add
        'a SalesNotes column that will not be synchronized.
        'When we create the Customer SyncTable, we specify that
        'Synchronization Services should use an existing table.          
        util.CreateTableOnClient()

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

        'Return the server data back to its original state.
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
        'and CustomerContact in the same group.
        Dim customerSyncGroup As New SyncGroup("Customer")
        Dim orderSyncGroup As New SyncGroup("Order")

        'Add each table: specify a synchronization direction of
        'Bidirectional. We create the Customer table before sync:
        'we set CreationOption to UseExistingTableOrFail so
        'we are sure that the table exists.
        Dim customerSyncTable As New SyncTable("Customer")
        customerSyncTable.CreationOption = TableCreationOption.UseExistingTableOrFail
        customerSyncTable.SyncDirection = SyncDirection.Bidirectional
        customerSyncTable.SyncGroup = customerSyncGroup
        Me.Configuration.SyncTables.Add(customerSyncTable)

        Dim customerContactSyncTable As New SyncTable("CustomerContact")
        customerContactSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        customerContactSyncTable.SyncDirection = SyncDirection.Bidirectional
        customerContactSyncTable.SyncGroup = customerSyncGroup
        Me.Configuration.SyncTables.Add(customerContactSyncTable)

        Dim orderHeaderSyncTable As New SyncTable("OrderHeader")
        orderHeaderSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        orderHeaderSyncTable.SyncDirection = SyncDirection.Bidirectional
        orderHeaderSyncTable.SyncGroup = orderSyncGroup
        Me.Configuration.SyncTables.Add(orderHeaderSyncTable)

        Dim orderDetailSyncTable As New SyncTable("OrderDetail")
        orderDetailSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        orderDetailSyncTable.SyncDirection = SyncDirection.Bidirectional
        orderDetailSyncTable.SyncGroup = orderSyncGroup
        Me.Configuration.SyncTables.Add(orderDetailSyncTable)

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

        'Create SyncAdapters for each table by using the SqlSyncAdapterBuilder:
        '  * Specify the base table and tombstone table names.
        '  * Specify the columns that are used to track when
        '    and where changes are made.
        '  * Specify bidirectional synchronization, so that all
        '    commands are generated.
        '  * Call ToSyncAdapter to create the SyncAdapter.
        '  * Specify a name for the SyncAdapter that matches the
        '    the name specified for the corresponding SyncTable.
        '    Do not include the schema names (Sales in this case).
        'Customer table
        Dim customerBuilder As New SqlSyncAdapterBuilder(serverConn)

        With customerBuilder
            .TableName = "Sales.Customer"
            .TombstoneTableName = customerBuilder.TableName + "_Tombstone"
            .SyncDirection = SyncDirection.Bidirectional
            .CreationTrackingColumn = "InsertTimestamp"
            .UpdateTrackingColumn = "UpdateTimestamp"
            .DeletionTrackingColumn = "DeleteTimestamp"
            .CreationOriginatorIdColumn = "InsertId"
            .UpdateOriginatorIdColumn = "UpdateId"
            .DeletionOriginatorIdColumn = "DeleteId"
        End With

        Dim customerSyncAdapter As SyncAdapter = customerBuilder.ToSyncAdapter()
        customerSyncAdapter.TableName = "Customer"
        Me.SyncAdapters.Add(customerSyncAdapter)


        'CustomerContact table.
        Dim customerContactBuilder As New SqlSyncAdapterBuilder(serverConn)

        With customerContactBuilder
            .TableName = "Sales.CustomerContact"
            .TombstoneTableName = customerContactBuilder.TableName + "_Tombstone"
            .SyncDirection = SyncDirection.Bidirectional
            .CreationTrackingColumn = "InsertTimestamp"
            .UpdateTrackingColumn = "UpdateTimestamp"
            .DeletionTrackingColumn = "DeleteTimestamp"
            .CreationOriginatorIdColumn = "InsertId"
            .UpdateOriginatorIdColumn = "UpdateId"
            .DeletionOriginatorIdColumn = "DeleteId"
        End With

        Dim customerContactSyncAdapter As SyncAdapter = customerContactBuilder.ToSyncAdapter()
        customerContactSyncAdapter.TableName = "CustomerContact"
        Me.SyncAdapters.Add(customerContactSyncAdapter)


        'OrderHeader table.
        Dim orderHeaderBuilder As New SqlSyncAdapterBuilder(serverConn)

        With orderHeaderBuilder
            .TableName = "Sales.OrderHeader"
            .TombstoneTableName = orderHeaderBuilder.TableName + "_Tombstone"
            .SyncDirection = SyncDirection.Bidirectional
            .CreationTrackingColumn = "InsertTimestamp"
            .UpdateTrackingColumn = "UpdateTimestamp"
            .DeletionTrackingColumn = "DeleteTimestamp"
            .CreationOriginatorIdColumn = "InsertId"
            .UpdateOriginatorIdColumn = "UpdateId"
            .DeletionOriginatorIdColumn = "DeleteId"
        End With

        Dim orderHeaderSyncAdapter As SyncAdapter = orderHeaderBuilder.ToSyncAdapter()
        orderHeaderSyncAdapter.TableName = "OrderHeader"
        Me.SyncAdapters.Add(orderHeaderSyncAdapter)


        'OrderDetail table.
        Dim orderDetailBuilder As New SqlSyncAdapterBuilder(serverConn)

        With orderDetailBuilder
            .TableName = "Sales.OrderDetail"
            .TombstoneTableName = orderDetailBuilder.TableName + "_Tombstone"
            .SyncDirection = SyncDirection.Bidirectional
            .CreationTrackingColumn = "InsertTimestamp"
            .UpdateTrackingColumn = "UpdateTimestamp"
            .DeletionTrackingColumn = "DeleteTimestamp"
            .CreationOriginatorIdColumn = "InsertId"
            .UpdateOriginatorIdColumn = "UpdateId"
            .DeletionOriginatorIdColumn = "DeleteId"
        End With

        Dim orderDetailSyncAdapter As SyncAdapter = orderDetailBuilder.ToSyncAdapter()
        orderDetailSyncAdapter.TableName = "OrderDetail"
        Me.SyncAdapters.Add(orderDetailSyncAdapter)

        'Create the schema for the OrderHeader and OrderDetail tables.
        'We first create a schema based on a DataSet that contains only
        'the OrderHeader table. As with the SyncAdapter, the table name
        'must match the SyncTable name. We then add the schema for the 
        'OrderDetail table; this is the place to map data types if
        'your application requires it.
        '<snippetOCS_VB_Init_SyncSchema>
        Dim orderHeaderDataSet As DataSet = util.CreateDataSetFromServer()
        orderHeaderDataSet.Tables(0).TableName = "OrderHeader"
        Me.Schema = New SyncSchema(orderHeaderDataSet)

        With Me.Schema
            .Tables.Add("OrderDetail")

            .Tables("OrderDetail").Columns.Add("OrderDetailId")
            .Tables("OrderDetail").Columns("OrderDetailId").ProviderDataType = "int"
            .Tables("OrderDetail").Columns("OrderDetailId").AllowNull = False

            .Tables("OrderDetail").Columns.Add("OrderId")
            .Tables("OrderDetail").Columns("OrderId").ProviderDataType = "uniqueidentifier"
            .Tables("OrderDetail").Columns("OrderId").RowGuid = True
            .Tables("OrderDetail").Columns("OrderId").AllowNull = False

            .Tables("OrderDetail").Columns.Add("Product")
            .Tables("OrderDetail").Columns("Product").ProviderDataType = "nvarchar"
            .Tables("OrderDetail").Columns("Product").MaxLength = 100
            .Tables("OrderDetail").Columns("Product").AllowNull = False

            .Tables("OrderDetail").Columns.Add("Quantity")
            .Tables("OrderDetail").Columns("Quantity").ProviderDataType = "int"
            .Tables("OrderDetail").Columns("Quantity").AllowNull = False
        End With        

        'The primary key columns are passed as a string array.
        Dim orderDetailPrimaryKey(1) As String
        orderDetailPrimaryKey(0) = "OrderDetailId"
        orderDetailPrimaryKey(1) = "OrderId"
        Me.Schema.Tables("OrderDetail").PrimaryKey = orderDetailPrimaryKey
        '</snippetOCS_VB_Init_SyncSchema>

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
        'by using the API. We use the SchemaCreated event 
        'to change the schema by using SQL.
        'Note that both schema events fire for the Customer table,
        'even though we already created the table. This allows us
        'to work with the table at this point if we need to.
        AddHandler Me.CreatingSchema, AddressOf SampleClientSyncProvider_CreatingSchema
        AddHandler Me.SchemaCreated, AddressOf SampleClientSyncProvider_SchemaCreated

    End Sub 'New


    Private Sub SampleClientSyncProvider_CreatingSchema(ByVal sender As Object, ByVal e As CreatingSchemaEventArgs)

        Dim tableName As String = e.Table.TableName

        Console.Write("Creating schema for " + tableName + " | ")

        If tableName = "OrderHeader" Then
            'Set the RowGuid property because it is not copied
            'to the client by default. This is also a good time
            'to specify literal defaults with .Columns[ColName].DefaultValue,
            'but we will specify defaults like NEWID() by calling
            'ALTER TABLE after the table is created.
            e.Schema.Tables("OrderHeader").Columns("OrderId").RowGuid = True
        End If

        If tableName = "OrderDetail" Then
            'Add a foreign key  between the OrderHeader and OrderDetail tables.
            e.Schema.Tables("OrderDetail").ForeignKeys.Add("FK_OrderDetail_OrderHeader", "OrderHeader", "OrderId", "OrderDetail", "OrderId")
        End If


    End Sub 'SampleClientSyncProvider_CreatingSchema


    Private Sub SampleClientSyncProvider_SchemaCreated(ByVal sender As Object, ByVal e As SchemaCreatedEventArgs)
        Dim tableName As String = e.Table.TableName
        Dim util As New Utility()

        'Call ALTER TABLE on the client. This must be done
        'over the same connection and within the same
        'transaction that Synchronization Services uses
        'to create the schema on the client.
        If tableName = "OrderHeader" Then
            util.MakeSchemaChangesOnClient(e.Connection, e.Transaction, "OrderHeader")
        End If

        If tableName = "OrderDetail" Then
            util.MakeSchemaChangesOnClient(e.Connection, e.Transaction, "OrderDetail")            
        End If

        Console.WriteLine("Schema created for " + tableName)

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
'</snippetOCS_VB_Init>

'Create foreign keys between each table.
'this.Schema.Tables["OrderHeader"].ForeignKeys.Add("FK_OrderHeader_Customer", "Customer", "CustomerId", "OrderHeader", "CustomerId");
'this.Schema.Tables["OrderDetail"].ForeignKeys.Add("FK_OrderDetail_OrderHeader", "OrderHeader", "OrderId", "OrderDetail", "OrderId");

'this.Schema.Tables.Add("Customer");
'this.Schema.Tables.Add("CustomerContact");