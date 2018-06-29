'<snippetOCS_VB_Events>
Imports System
Imports System.Collections
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
        'string information, and making changes to the server and client databases.
        Dim util As New Utility()

        'The SampleStatsAndProgress class handles information from the 
        'SyncStatistics object that the Synchronize method returns and
        'from SyncAgent events.
        Dim sampleStats As New SampleStatsAndProgress()

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

        'Make changes on the server and client.
        util.MakeDataChangesOnServer("Customer")
        util.MakeDataChangesOnClient("Customer")

        'Subsequent synchronization.
        syncStatistics = sampleSyncAgent.Synchronize()
        sampleStats.DisplayStats(syncStatistics, "subsequent")

        'Make a change at the client that fails when it is
        'applied at the server.
        util.MakeFailingChangesOnClient()

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
        'Bidirectional.
        Dim customerSyncTable As New SyncTable("Customer")
        customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        customerSyncTable.SyncDirection = SyncDirection.Bidirectional
        customerSyncTable.SyncGroup = customerSyncGroup
        Me.Configuration.SyncTables.Add(customerSyncTable)

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

        'Handle the StateChanged and SessionProgress events, and
        'display information to the console.
        Dim sampleStats As New SampleStatsAndProgress()
        AddHandler Me.StateChanged, AddressOf sampleStats.DisplaySessionProgress
        AddHandler Me.SessionProgress, AddressOf sampleStats.DisplaySessionProgress

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
        '  * Specify bidirectional synchronization.
        '  * Call ToSyncAdapter to create the SyncAdapter.
        '  * Specify a name for the SyncAdapter that matches the
        '    the name specified for the corresponding SyncTable.
        '    Do not include the schema names (Sales in this case).

        'Customer table
        '<snippetOCS_VB_Events_CustomerAdapterBuilder>
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
        '</snippetOCS_VB_Events_CustomerAdapterBuilder>

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

        'Log information for the following events.
        AddHandler Me.ChangesSelected, AddressOf EventLogger.LogEvents
        AddHandler Me.ChangesApplied, AddressOf EventLogger.LogEvents
        '<snippetOCS_VB_Events_ApplyChangeFailedEventHandler>
        AddHandler Me.ApplyChangeFailed, AddressOf EventLogger.LogEvents
        '</snippetOCS_VB_Events_ApplyChangeFailedEventHandler>
 
        'Handle the ApplyingChanges event so that we can
        'make changes to the dataset.
        AddHandler Me.ApplyingChanges, AddressOf SampleServerSyncProvider_ApplyingChanges

    End Sub 'New

    'Look for inserted rows in the dataset that have a CustomerType
    'of Wholesale and update these rows. With access to the dataset,
    'you can write any business logic that your application requires.
    '<snippetOCS_VB_Events_BusinessLogic>
    Private Sub SampleServerSyncProvider_ApplyingChanges(ByVal sender As Object, ByVal e As ApplyingChangesEventArgs)
        Dim customerDataTable As DataTable = e.Changes.Tables("Customer")

        Dim i As Integer
        For i = 1 To customerDataTable.Rows.Count - 1
            If customerDataTable.Rows(i).RowState = DataRowState.Added _
                AndAlso customerDataTable.Rows(i)("CustomerType") = "Wholesale" Then
                customerDataTable.Rows(i)("CustomerType") = "Wholesale (from mobile sales)"
            End If
        Next i

    End Sub
    '</snippetOCS_VB_Events_BusinessLogic>'SampleServerSyncProvider_ApplyingChanges
End Class 'SampleServerSyncProvider 

'Create a class that is derived from 
'Microsoft.Synchronization.Data.SqlServerCe.SqlCeClientSyncProvider.
'You can just instantiate the provider directly and associate it
'with the SyncAgent, but here we use this class to handle client 
'provider events.
'<snippetOCS_VB_Events_SampleClientSyncProvider>
Public Class SampleClientSyncProvider
    Inherits SqlCeClientSyncProvider


    Public Sub New()
        'Specify a connection string for the sample client database.
        Dim util As New Utility()
        Me.ConnectionString = util.ClientConnString

        'Log information for the following events.
        AddHandler Me.SchemaCreated, AddressOf EventLogger.LogEvents
        AddHandler Me.ChangesSelected, AddressOf EventLogger.LogEvents
        AddHandler Me.ChangesApplied, AddressOf EventLogger.LogEvents
        AddHandler Me.ApplyChangeFailed, AddressOf EventLogger.LogEvents

        'Use the following events to fix up schema on the client.
        'We use the CreatingSchema event to change the schema
        'by using the API. We use the SchemaCreated event 
        'to change the schema by using SQL.
        'Note that both schema events fire for the Customer table,
        'even though we already created the table. This allows us
        'to work with the table at this point if we have to.
        AddHandler Me.CreatingSchema, AddressOf SampleClientSyncProvider_CreatingSchema
        AddHandler Me.SchemaCreated, AddressOf SampleClientSyncProvider_SchemaCreated

    End Sub 'New


    Private Sub SampleClientSyncProvider_CreatingSchema(ByVal sender As Object, ByVal e As CreatingSchemaEventArgs)

        Dim tableName As String = e.Table.TableName

        If tableName = "Customer" Then
            'Set the RowGuid property because it is not copied
            'to the client by default. This is also a good time
            'to specify literal defaults with .Columns[ColName].DefaultValue,
            'but we will specify defaults like NEWID() by calling
            'ALTER TABLE after the table is created.
            e.Schema.Tables("Customer").Columns("CustomerId").RowGuid = True
        End If

        If tableName = "OrderHeader" Then
            e.Schema.Tables("OrderHeader").Columns("OrderId").RowGuid = True
        End If

    End Sub 'SampleClientSyncProvider_CreatingSchema


    Private Sub SampleClientSyncProvider_SchemaCreated(ByVal sender As Object, ByVal e As SchemaCreatedEventArgs)
        Dim tableName As String = e.Table.TableName
        Dim util As New Utility()

        'Call ALTER TABLE on the client. This must be done
        'over the same connection and within the same
        'transaction that Synchronization Services uses
        'to create the schema on the client.
        If tableName = "Customer" Then
            util.MakeSchemaChangesOnClient(e.Connection, e.Transaction, "Customer")
        End If

        If tableName = "OrderHeader" Then
            util.MakeSchemaChangesOnClient(e.Connection, e.Transaction, "OrderHeader")
        End If

        If tableName = "OrderDetail" Then
            util.MakeSchemaChangesOnClient(e.Connection, e.Transaction, "OrderDetail")
        End If

    End Sub 'SampleClientSyncProvider_SchemaCreated
End Class 'SampleClientSyncProvider
'</snippetOCS_VB_Events_SampleClientSyncProvider>

'Handle SyncAgent events and the statistics that are returned by the SyncAgent.
Public Class SampleStatsAndProgress

    Public Sub DisplayStats(ByVal syncStatistics As SyncStatistics, ByVal syncType As String)
        Console.WriteLine(String.Empty)
        If syncType = "initial" Then
            Console.WriteLine("****** Initial Synchronization Stats ******")
        ElseIf syncType = "subsequent" Then
            Console.WriteLine("***** Subsequent Synchronization Stats ****")
        End If

        Console.WriteLine("Start Time: " & syncStatistics.SyncStartTime)
        Console.WriteLine("Upload Changes Applied: " & syncStatistics.UploadChangesApplied)
        Console.WriteLine("Upload Changes Failed: " & syncStatistics.UploadChangesFailed)
        Console.WriteLine("Total Changes Uploaded: " & syncStatistics.TotalChangesUploaded)
        Console.WriteLine("Download Changes Applied: " & syncStatistics.DownloadChangesApplied)
        Console.WriteLine("Download Changes Failed: " & syncStatistics.DownloadChangesFailed)
        Console.WriteLine("Total Changes Downloaded: " & syncStatistics.TotalChangesDownloaded)
        Console.WriteLine("Complete Time: " & syncStatistics.SyncCompleteTime)
        Console.WriteLine(String.Empty)

    End Sub 'DisplayStats


    Public Sub DisplaySessionProgress(ByVal sender As Object, ByVal e As EventArgs)

        Dim outputText As New StringBuilder()

        If TypeOf e Is SessionStateChangedEventArgs Then

            Dim args As SessionStateChangedEventArgs = CType(e, SessionStateChangedEventArgs)

            If args.SessionState = SyncSessionState.Synchronizing Then
                outputText.AppendLine(String.Empty)
                outputText.Append("** SyncAgent is synchronizing")

            Else
                outputText.Append("** SyncAgent is ready to synchronize")
            End If

        ElseIf TypeOf e Is SessionProgressEventArgs Then
            Dim args As SessionProgressEventArgs = CType(e, SessionProgressEventArgs)
            outputText.Append("Percent complete: " & args.PercentCompleted & " (" & args.SyncStage.ToString() & ")")

        Else
            outputText.AppendLine("Unknown event occurred")
        End If

        Console.WriteLine(outputText.ToString())

    End Sub 'DisplaySessionProgress
End Class 'SampleStatsAndProgress


Public Class EventLogger
    'Create client and server log files, and write to them
    'based on data from several EventArgs classes.
    Public Shared Sub LogEvents(ByVal sender As Object, ByVal e As EventArgs)
        Dim logFile As String = String.Empty
        Dim site As String = String.Empty

        If TypeOf sender Is SampleServerSyncProvider Then
            logFile = "ServerLogFile.txt"
            site = "server"
        ElseIf TypeOf sender Is SampleClientSyncProvider Then
            logFile = "ClientLogFile.txt"
            site = "client"
        End If

        Dim streamWriter As StreamWriter = File.AppendText(logFile)
        Dim outputText As New StringBuilder()

        If TypeOf e Is ChangesSelectedEventArgs Then

            Dim args As ChangesSelectedEventArgs = CType(e, ChangesSelectedEventArgs)
            outputText.AppendLine("Client ID: " & args.Session.ClientId.ToString())
            outputText.AppendLine("Changes selected from " & site & " for group " & args.GroupMetadata.GroupName)
            outputText.AppendLine("Inserts selected from " & site & " for group: " & args.Context.GroupProgress.TotalInserts.ToString())
            outputText.AppendLine("Updates selected from " & site & " for group: " & args.Context.GroupProgress.TotalUpdates.ToString())
            outputText.AppendLine("Deletes selected from " & site & " for group: " & args.Context.GroupProgress.TotalDeletes.ToString())            


        ElseIf TypeOf e Is ChangesAppliedEventArgs Then

            Dim args As ChangesAppliedEventArgs = CType(e, ChangesAppliedEventArgs)
            outputText.AppendLine("Client ID: " & args.Session.ClientId.ToString())
            outputText.AppendLine("Changes applied to " & site & " for group " & args.GroupMetadata.GroupName)
            outputText.AppendLine("Inserts applied to " & site & " for group: " & args.Context.GroupProgress.TotalInserts.ToString())
            outputText.AppendLine("Updates applied to " & site & " for group: " & args.Context.GroupProgress.TotalUpdates.ToString())
            outputText.AppendLine("Deletes applied to " & site & " for group: " & args.Context.GroupProgress.TotalDeletes.ToString())


        ElseIf TypeOf e Is SchemaCreatedEventArgs Then

            Dim args As SchemaCreatedEventArgs = CType(e, SchemaCreatedEventArgs)
            outputText.AppendLine("Schema creation for group: " & args.Table.SyncGroup.GroupName)
            outputText.AppendLine("Table: " & args.Table.TableName)
            outputText.AppendLine("Direction : " & args.Table.SyncDirection)
            outputText.AppendLine("Creation Option: " & args.Table.CreationOption)

            '<snippetOCS_VB_Events_ApplyChangeFailedEventArgs>
        ElseIf TypeOf e Is ApplyChangeFailedEventArgs Then

            Dim args As ApplyChangeFailedEventArgs = CType(e, ApplyChangeFailedEventArgs)
            outputText.AppendLine("** APPLY CHANGE FAILURE AT " & site.ToUpper() & " **")
            outputText.AppendLine("Table for which failure occurred: " & args.TableMetadata.TableName)
            outputText.AppendLine("Error message: " & args.Error.Message)
            '</snippetOCS_VB_Events_ApplyChangeFailedEventArgs>

        Else
            outputText.AppendLine("Unknown event occurred")
        End If

        streamWriter.WriteLine(DateTime.Now.ToShortTimeString() + " | " + outputText.ToString())
        streamWriter.Flush()
        streamWriter.Dispose()

    End Sub 'LogEvents
End Class 'EventLogger
'</snippetOCS_VB_Events>