'<snippetOCS_VB_View>
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
        util.MakeDataChangesOnServer("CustomerContact")

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

        'Add the Customer table: specify a synchronization direction of
        'DownloadOnly, and that an existing table should be dropped.
        '<snippetOCS_VB_View_CustomerInfoSyncTable>
        Dim customerInfoSyncTable As New SyncTable("CustomerInfo")
        customerInfoSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        customerInfoSyncTable.SyncDirection = SyncDirection.DownloadOnly
        Me.Configuration.SyncTables.Add(customerInfoSyncTable)
        '</snippetOCS_VB_View_CustomerInfoSyncTable>

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
        '<snippetOCS_VB_View_NewAnchorCommand>
        Dim selectNewAnchorCommand As New SqlCommand()
        Dim newAnchorVariable As String = "@" + SyncSession.SyncNewReceivedAnchor
        With selectNewAnchorCommand
            .CommandText = "SELECT " + newAnchorVariable + " = min_active_rowversion() - 1"
            .Parameters.Add(newAnchorVariable, SqlDbType.Timestamp)
            .Parameters(newAnchorVariable).Direction = ParameterDirection.Output
            .Connection = serverConn
        End With
        Me.SelectNewAnchorCommand = selectNewAnchorCommand
        '</snippetOCS_VB_View_NewAnchorCommand>

        'Create a SyncAdapter for the CustomerInfo table. The CustomerInfo 
        'table on the client is a combination of the Customer and CustomerContact
        'tables on the server. This table is download-only, as specified in 
        'SampleSyncAgent.
        '<snippetOCS_VB_View_CustomerInfoSyncAdapter>
        Dim customerInfoSyncAdapter As New SyncAdapter("CustomerInfo")
        '</snippetOCS_VB_View_CustomerInfoSyncAdapter>

        'Specify synchronization commands. The CustomerInfo table 
        'is download-only, so we do not define commands to apply changes to 
        'the server. Each command joins the base tables or tombstone tables
        'to select the appropriate incremental changes. For this application,
        'the logic is as follows:
        '* Select all inserts for customers that have contact information.
        '  This results in more than one row for a customer if that customer 
        '  has more than one phone number.
        '* Select all updates for customer and contact information that has 
        '  already been downloaded.
        '* Select all deletes for customer and contact information that has 
        '  already been downloaded. If a customer has been deleted, delete
        '  all of the rows for that customer. If a phone number has been
        '  deleted, delete only that row.
        'Select inserts.
        '<snippetOCS_VB_View_CustomerInfoIncrInsert>
        Dim customerInfoIncrementalInsertsCommand As New SqlCommand()
        With customerInfoIncrementalInsertsCommand
            .CommandType = CommandType.Text
            .CommandText = _
                "SELECT c.CustomerId, c.CustomerName, c.SalesPerson, cc.PhoneNumber, cc.PhoneType " _
              & "FROM Sales.Customer c JOIN Sales.CustomerContact cc ON " _
              & "c.CustomerId = cc.CustomerId " _
              & "WHERE ((c.InsertTimestamp > @sync_last_received_anchor " _
              & "AND c.InsertTimestamp <= @sync_new_received_anchor) OR " _
              & "(cc.InsertTimestamp > @sync_last_received_anchor " _
              & "AND cc.InsertTimestamp <= @sync_new_received_anchor))"
            .Parameters.Add("@sync_last_received_anchor", SqlDbType.Timestamp)
            .Parameters.Add("@sync_new_received_anchor", SqlDbType.Timestamp)
            .Connection = serverConn
        End With
        customerInfoSyncAdapter.SelectIncrementalInsertsCommand = customerInfoIncrementalInsertsCommand
        '</snippetOCS_VB_View_CustomerInfoIncrInsert>

        'Select updates.
        Dim customerInfoIncrementalUpdatesCommand As New SqlCommand()
        With customerInfoIncrementalUpdatesCommand
            .CommandType = CommandType.Text
            .CommandText = _
                "SELECT c.CustomerId, c.CustomerName, c.SalesPerson, cc.PhoneNumber, cc.PhoneType " _
              & "FROM Sales.Customer c JOIN Sales.CustomerContact cc ON " _
              & "c.CustomerId = cc.CustomerId " _
              & "WHERE ((c.UpdateTimestamp > @sync_last_received_anchor " _
              & "AND c.UpdateTimestamp <= @sync_new_received_anchor " _
              & "AND c.InsertTimestamp <= @sync_last_received_anchor) " _
              & "OR (cc.UpdateTimestamp > @sync_last_received_anchor " _
              & "AND cc.UpdateTimestamp <= @sync_new_received_anchor " _
              & "AND cc.InsertTimestamp <= @sync_last_received_anchor))"
            .Parameters.Add("@sync_last_received_anchor", SqlDbType.Timestamp)
            .Parameters.Add("@sync_new_received_anchor", SqlDbType.Timestamp)
            .Connection = serverConn
        End With
        customerInfoSyncAdapter.SelectIncrementalUpdatesCommand = customerInfoIncrementalUpdatesCommand

        'Select deletes.
        '<snippetOCS_VB_View_CustomerInfoIncrDelete>
        Dim customerInfoIncrementalDeletesCommand As New SqlCommand()
        With customerInfoIncrementalDeletesCommand
            .CommandType = CommandType.Text
            .CommandText = _
                "SELECT c.CustomerId, cc.PhoneType " _
              & "FROM Sales.Customer_Tombstone c JOIN Sales.CustomerContact cc ON " _
              & "c.CustomerId = cc.CustomerId " _
              & "WHERE (@sync_initialized = 1 " _
              & "AND (DeleteTimestamp > @sync_last_received_anchor " _
              & "AND DeleteTimestamp <= @sync_new_received_anchor)) " _
              & "UNION " _
              & "SELECT CustomerId, PhoneType " _
              & "FROM Sales.CustomerContact_Tombstone " _
              & "WHERE (@sync_initialized = 1 " _
              & "AND (DeleteTimestamp > @sync_last_received_anchor " _
              & "AND DeleteTimestamp <= @sync_new_received_anchor))"
            .Parameters.Add("@sync_initialized", SqlDbType.Bit)
            .Parameters.Add("@sync_last_received_anchor", SqlDbType.Timestamp)
            .Parameters.Add("@sync_new_received_anchor", SqlDbType.Timestamp)
            .Connection = serverConn
        End With
        customerInfoSyncAdapter.SelectIncrementalDeletesCommand = customerInfoIncrementalDeletesCommand
        '</snippetOCS_VB_View_CustomerInfoIncrDelete>

        'Add the SyncAdapter to the provider.
        Me.SyncAdapters.Add(customerInfoSyncAdapter)

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

        'Handle the two schema-related events.
        AddHandler Me.CreatingSchema, AddressOf SampleClientSyncProvider_CreatingSchema
        AddHandler Me.SchemaCreated, AddressOf SampleClientSyncProvider_SchemaCreated

    End Sub 'New


    Private Sub SampleClientSyncProvider_CreatingSchema(ByVal sender As Object, ByVal e As CreatingSchemaEventArgs)

        Console.Write("Creating schema for " + e.Table.TableName + " | ")

        'Create a compostite primary key for the CustomerInfo table.
        '<snippetOCS_VB_View_CustomerInfoPrimaryKey>
        Dim customerInfoPrimaryKey(1) As String
        customerInfoPrimaryKey(0) = "CustomerId"
        customerInfoPrimaryKey(1) = "PhoneType"
        e.Schema.Tables("CustomerInfo").PrimaryKey = customerInfoPrimaryKey
        '</snippetOCS_VB_View_CustomerInfoPrimaryKey>

    End Sub 'SampleClientSyncProvider_CreatingSchema


    Private Sub SampleClientSyncProvider_SchemaCreated(ByVal sender As Object, ByVal e As SchemaCreatedEventArgs)

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
        Console.WriteLine("Total Changes Downloaded: " & syncStatistics.TotalChangesDownloaded)
        Console.WriteLine("Complete Time: " & syncStatistics.SyncCompleteTime)
        Console.WriteLine(String.Empty)

    End Sub 'DisplayStats
End Class 'SampleStats
'</snippetOCS_VB_View>