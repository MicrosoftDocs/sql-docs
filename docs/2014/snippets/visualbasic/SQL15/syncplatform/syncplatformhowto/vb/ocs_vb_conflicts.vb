'<snippetOCS_VB_Conflicts>
Imports System
Imports System.Collections
Imports System.Collections.Generic
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

        'Make a change at the client that fails when it is
        'applied at the server.
        util.MakeFailingChangesOnClient()

        'Make changes at the client and server that conflict
        'when they are synchronized.
        util.MakeConflictingChangesOnClientAndServer()

        'Subsequent synchronization.
        syncStatistics = sampleSyncAgent.Synchronize()
        sampleStats.DisplayStats(syncStatistics, "subsequent")

        'Return server data back to its original state.
        'Comment out this line if you want to view the
        'state of the data after all conflicts are resolved.
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

        'Add the Customer table: specify a synchronization direction 
        'of Bidirectional.
        Dim customerSyncTable As New SyncTable("Customer")
        customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        customerSyncTable.SyncDirection = SyncDirection.Bidirectional
        Me.Configuration.SyncTables.Add(customerSyncTable)

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


        'Create a SyncAdapter for the Customer table, and then define
        'the commands to synchronize changes:
        '* SelectConflictUpdatedRowsCommand SelectConflictDeletedRowsCommand
        '  are used to detect if there are conflicts on the server during
        '  synchronization.
        '* SelectIncrementalInsertsCommand, SelectIncrementalUpdatesCommand,
        '  and SelectIncrementalDeletesCommand are used to select changes
        '  from the server that the client provider then applies to the client.
        '* InsertCommand, UpdateCommand, and DeleteCommand are used to apply
        '  to the server the changes that the client provider has selected
        '  from the client.
        'Create the SyncAdapter.
        '<snippetOCS_VB_Conflicts_CustomerSyncAdapter>
        Dim customerSyncAdapter As New SyncAdapter("Customer")

        'This command is used if @sync_row_count returns
        '0 when changes are applied to the server.
        '<snippetOCS_VB_Conflicts_SelectConflictUpdatedRowsCommand>
        Dim customerUpdateConflicts As New SqlCommand()
        With customerUpdateConflicts
            .CommandText = _
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " _
              & "FROM Sales.Customer " + "WHERE CustomerId = @CustomerId"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectConflictUpdatedRowsCommand = customerUpdateConflicts
        '</snippetOCS_VB_Conflicts_SelectConflictUpdatedRowsCommand>

        'This command is used if the server provider cannot find
        'a row in the base table.
        '<snippetOCS_VB_Conflicts_SelectConflictDeletedRowsCommand>
        Dim customerDeleteConflicts As New SqlCommand()
        With customerDeleteConflicts
            .CommandText = _
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " _
              & "FROM Sales.Customer_Tombstone " + "WHERE CustomerId = @CustomerId"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectConflictDeletedRowsCommand = customerDeleteConflicts
        '</snippetOCS_VB_Conflicts_SelectConflictDeletedRowsCommand>

        'Select inserts from the server.
        '<snippetOCS_VB_Conflicts_CustomerIncrInsert>
        Dim customerIncrInserts As New SqlCommand()
        With customerIncrInserts
            .CommandText = _
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " _
              & "FROM Sales.Customer " _
              & "WHERE (InsertTimestamp > @sync_last_received_anchor " _
              & "AND InsertTimestamp <= @sync_new_received_anchor " _
              & "AND InsertId <> @sync_client_id)"
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectIncrementalInsertsCommand = customerIncrInserts
        '</snippetOCS_VB_Conflicts_CustomerIncrInsert>

        'Apply inserts to the server.
        '<snippetOCS_VB_Conflicts_CustomerInsert>
        Dim customerInserts As New SqlCommand()
        customerInserts.CommandType = CommandType.StoredProcedure
        customerInserts.CommandText = "usp_CustomerApplyInsert"
        customerInserts.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
        customerInserts.Parameters.Add("@" + SyncSession.SyncForceWrite, SqlDbType.Bit)
        customerInserts.Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        customerInserts.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
        customerInserts.Parameters.Add("@CustomerName", SqlDbType.NVarChar)
        customerInserts.Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
        customerInserts.Parameters.Add("@CustomerType", SqlDbType.NVarChar)
        customerInserts.Connection = serverConn
        customerSyncAdapter.InsertCommand = customerInserts
        '</snippetOCS_VB_Conflicts_CustomerInsert>


        'Select updates from the server.
        '<snippetOCS_VB_Conflicts_CustomerIncrUpdate>
        Dim customerIncrUpdates As New SqlCommand()
        With customerIncrUpdates
            .CommandText = _
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " _
              & "FROM Sales.Customer " _
              & "WHERE (UpdateTimestamp > @sync_last_received_anchor " _
              & "AND UpdateTimestamp <= @sync_new_received_anchor " _
              & "AND UpdateId <> @sync_client_id " _
              & "AND NOT (InsertTimestamp > @sync_last_received_anchor " _
              & "AND InsertId <> @sync_client_id))"
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectIncrementalUpdatesCommand = customerIncrUpdates
        '</snippetOCS_VB_Conflicts_CustomerIncrUpdate>

        'Apply updates to the server.
        '<snippetOCS_VB_Conflicts_CustomerUpdate>
        Dim customerUpdates As New SqlCommand()
        customerUpdates.CommandType = CommandType.StoredProcedure
        customerUpdates.CommandText = "usp_CustomerApplyUpdate"
        customerUpdates.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
        customerUpdates.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
        customerUpdates.Parameters.Add("@" + SyncSession.SyncForceWrite, SqlDbType.Bit)
        customerUpdates.Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        customerUpdates.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
        customerUpdates.Parameters.Add("@CustomerName", SqlDbType.NVarChar)
        customerUpdates.Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
        customerUpdates.Parameters.Add("@CustomerType", SqlDbType.NVarChar)
        customerUpdates.Connection = serverConn
        customerSyncAdapter.UpdateCommand = customerUpdates
        '</snippetOCS_VB_Conflicts_CustomerUpdate>


        'Select deletes from the server.
        '<snippetOCS_VB_Conflicts_CustomerIncrDelete>
        Dim customerIncrDeletes As New SqlCommand()
        With customerIncrDeletes
            .CommandText = _
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " _
              & "FROM Sales.Customer_Tombstone " _
              & "WHERE (@sync_initialized = 1 " _
              & "AND DeleteTimestamp > @sync_last_received_anchor " _
              & "AND DeleteTimestamp <= @sync_new_received_anchor " _
              & "AND DeleteId <> @sync_client_id)"
            .Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Bit)
            .Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp)
            .Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
            .Connection = serverConn
        End With
        customerSyncAdapter.SelectIncrementalDeletesCommand = customerIncrDeletes
        '</snippetOCS_VB_Conflicts_CustomerIncrDelete>

        'Apply deletes to the server.
        '<snippetOCS_VB_Conflicts_CustomerDelete>
        Dim customerDeletes As New SqlCommand()
        customerDeletes.CommandType = CommandType.StoredProcedure
        customerDeletes.CommandText = "usp_CustomerApplyDelete"
        customerDeletes.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp)
        customerDeletes.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier)
        customerDeletes.Parameters.Add("@" + SyncSession.SyncForceWrite, SqlDbType.Bit)
        customerDeletes.Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        customerDeletes.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
        customerDeletes.Connection = serverConn
        customerSyncAdapter.DeleteCommand = customerDeletes
        '</snippetOCS_VB_Conflicts_CustomerDelete>


        'Add the SyncAdapter to the server synchronization provider.
        Me.SyncAdapters.Add(customerSyncAdapter)
        '</snippetOCS_VB_Conflicts_CustomerSyncAdapter>


        'Handle the ApplyChangeFailed and ChangesApplied events. 
        'This allows us to respond to any conflicts that occur, and to 
        'make changes that are downloaded to the client during the same
        'session.
        AddHandler Me.ApplyChangeFailed, AddressOf SampleServerSyncProvider_ApplyChangeFailed
        AddHandler Me.ChangesApplied, AddressOf SampleServerSyncProvider_ChangesApplied

    End Sub 'New

    'Create a list to hold primary keys from the Customer
    'table. This list is used when we handle the ApplyChangeFailed 
    'and ChangesApplied events.
    Private _updateConflictGuids As ArrayList = New ArrayList

    Private Sub SampleServerSyncProvider_ApplyChangeFailed(ByVal sender As Object, ByVal e As ApplyChangeFailedEventArgs)

        'Log information for the ApplyChangeFailed event.
        EventLogger.LogEvents(sender, e)

        'Respond to four different types of conflicts:
        ' * ClientDeleteServerUpdate
        ' * ClientUpdateServerDelete
        ' * ClientInsertServerInsert
        ' * ClientUpdateServerUpdate
        '
        If e.Conflict.ConflictType = ConflictType.ClientDeleteServerUpdate Then
            'With the commands we are using, the default is for the server 
            'change to win and be applied to the client. Here, we accept the 
            'default on the server side. We also set ConflictResolver.ServerWins 
            'for this conflict in the client provider. This ensures that the server
            'change is applied to the client during the download phase.
            Console.WriteLine(String.Empty)
            Console.WriteLine("***********************************")
            Console.WriteLine("A client delete / server update conflict was detected.")

            e.Action = ApplyAction.Continue

            Console.WriteLine("The server change will be applied at the client.")
            Console.WriteLine("***********************************")
            Console.WriteLine(String.Empty)
        End If

        '<snippetOCS_VB_Conflicts_ServerApplyChangeFailed>
        If e.Conflict.ConflictType = ConflictType.ClientUpdateServerDelete Then

            'For client-update/server-delete conflicts, we force the client 
            'change to be applied at the server. The stored procedure specified for 
            'customerSyncAdapter.UpdateCommand accepts the @sync_force_write parameter
            'and includes logic to handle this case.
            Console.WriteLine(String.Empty)
            Console.WriteLine("***********************************")
            Console.WriteLine("A client update / server delete conflict was detected.")

            e.Action = ApplyAction.RetryWithForceWrite

            Console.WriteLine("The client change was retried at the server with RetryWithForceWrite.")
            Console.WriteLine("***********************************")
            Console.WriteLine(String.Empty)
        End If
        '</snippetOCS_VB_Conflicts_ServerApplyChangeFailed>

        If e.Conflict.ConflictType = ConflictType.ClientInsertServerInsert Then
            'Similar to how we handled the client-delete/server-update conflict.
            'In this case, we set ConflictResolver.FireEvent and use RetryWithForceWrite
            'for this conflict in the client provider. This is equivalent to 
            'ConflictResolver.ServerWins, and ensures that the server
            'change is applied to the client during the download phase.
            Console.WriteLine(String.Empty)
            Console.WriteLine("***********************************")
            Console.WriteLine("A client insert / server insert conflict was detected.")

            e.Action = ApplyAction.Continue

            Console.WriteLine("The server change will be applied at the client.")
            Console.WriteLine("***********************************")
            Console.WriteLine(String.Empty)
        End If

        If e.Conflict.ConflictType = ConflictType.ClientUpdateServerUpdate Then

            'For client-update/server-update conflicts, we want to
            'allow the user to specify the conflict resolution option.
            '
            'It is possible for the Conflict object from the
            'server to have more than one row. Because our custom
            'resolution code only works with one row at a time,
            'we only allow the user to select a resolution
            'option if the object contains a single row.
            If e.Conflict.ServerChange.Rows.Count > 1 Then
                Console.WriteLine(String.Empty)
                Console.WriteLine("***********************************")
                Console.WriteLine("A client update / server update conflict was detected.")

                e.Action = ApplyAction.Continue

                Console.WriteLine("The server change will be applied at the client.")
                Console.WriteLine("***********************************")
                Console.WriteLine(String.Empty)
            Else
                Console.WriteLine(String.Empty)
                Console.WriteLine("***********************************")
                Console.WriteLine("A client update / server update conflict was detected.")
                Console.WriteLine("Conflicting rows are displayed below.")
                Console.WriteLine("***********************************")

                'Get the conflicting changes from the Conflict object
                'and display them. The Conflict object holds a copy
                'of the changes; updates to this object will not be 
                'applied. To make changes, use the Context object,
                'which is demonstrated in the next section of code
                'under ' case "CU" '.
                Dim conflictingServerChange As DataTable = e.Conflict.ServerChange
                Dim conflictingClientChange As DataTable = e.Conflict.ClientChange
                Dim serverColumnCount As Integer = conflictingServerChange.Columns.Count
                Dim clientColumnCount As Integer = conflictingClientChange.Columns.Count

                Console.WriteLine(String.Empty)
                Console.WriteLine("Server row: ")
                Console.Write(" | ")

                'Display the server row.
                Dim i As Integer
                For i = 0 To serverColumnCount - 1
                    Console.Write(conflictingServerChange.Rows(0)(i).ToString() & " | ")
                Next i

                Console.WriteLine(String.Empty)
                Console.WriteLine(String.Empty)
                Console.WriteLine("Client row: ")
                Console.Write(" | ")

                'Display the client row.
                For i = 0 To clientColumnCount - 1
                    Console.Write(conflictingClientChange.Rows(0)(i).ToString() & " | ")
                Next i

                Console.WriteLine(String.Empty)
                Console.WriteLine(String.Empty)

                'Ask for a conflict resolution option.
                Console.WriteLine("Enter a resolution option for this conflict:")
                Console.WriteLine("SE = server change wins")
                Console.WriteLine("CL = client change wins")
                Console.WriteLine("CU = custom resolution (combine rows)")

                Dim conflictResolution As String = Console.ReadLine()
                conflictResolution.ToUpper()

                Select Case conflictResolution
                    Case "SE"

                        'Again, this is the default for the commands we are using:
                        'the server change is persisted and then downloaded to the client.
                        e.Action = ApplyAction.Continue
                        Console.WriteLine(String.Empty)
                        Console.WriteLine("The server change will be applied at the client.")


                    Case "CL"

                        'Override the default by specifying that the client row
                        'should be applied at the server. The stored procedure specified  
                        'for customerSyncAdapter.UpdateCommand accepts the @sync_force_write 
                        'parameter and includes logic to handle this case.
                        e.Action = ApplyAction.RetryWithForceWrite
                        Console.WriteLine(String.Empty)
                        Console.WriteLine("The client change was retried at the server with RetryWithForceWrite.")


                    Case "CU"

                        'Provide a custom resolution scheme that takes each conflicting
                        'column and applies the combined contents of the column to the 
                        'client and server. This is not necessarily a resolution scheme 
                        'that you would use in production. Instead, it is used to 
                        'demonstrate the various ways you can interact with conflicting 
                        'data during synchronization.
                        '
                        'Get the ID for the conflicting row from the client data table,
                        'and add it to a list of GUIDs. We update rows at the server
                        'based on this list.
                        Dim customerId As Guid = CType(conflictingClientChange.Rows(0)("CustomerId"), Guid)
                        _updateConflictGuids.Add(customerId)

                        'Create a hashtable to hold the column ordinal and value for
                        'any columns that are in confict.
                        Dim conflictingColumns As Hashtable = New Hashtable()
                        Dim combinedColumnValue As String

                        'Determine which columns are different at the client and server.
                        'We already looped through these columns once, but we wanted to
                        'keep this code separate from the display code above.
                        For i = 0 To clientColumnCount - 1
                            If conflictingClientChange.Rows(0)(i).ToString() <> conflictingServerChange.Rows(0)(i).ToString() Then
                                'If we find a column that is different, combine the values from
                                'the client and server, and write "| conflict |" between them.
                                combinedColumnValue = conflictingClientChange.Rows(0)(i).ToString() _
                                & "  | conflict |  " & conflictingServerChange.Rows(0)(i).ToString()
                                conflictingColumns.Add(i, combinedColumnValue)
                            End If
                        Next i

                        'Loop through the rows in the Context object, which exposes
                        'the set of changes that are uploaded from the client.
                        'Note: In the ApplyChangeFailed event for the client provider,  
                        'you have access to the set of changes that was downloaded from
                        'the server.
                        Dim allClientChanges As DataTable = e.Context.DataSet.Tables("Customer")
                        Dim allClientRowCount As Integer = allClientChanges.Rows.Count
                        Dim allClientColumnCount As Integer = allClientChanges.Columns.Count

                        For i = 0 To allClientRowCount - 1
                            'Find the changed row with the GUID from the Conflict object.
                            If allClientChanges.Rows(i).RowState = DataRowState.Modified AndAlso CType(allClientChanges.Rows(i)("CustomerId"), Guid) = customerId Then
                                'Loop through the columns and check whether the column
                                'is in the conflictingColumns hashtable. If it is,
                                'update the value in the allClientChanges Context object.
                                Dim j As Integer
                                For j = 0 To allClientColumnCount - 1
                                    If conflictingColumns.ContainsKey(j) Then
                                        allClientChanges.Rows(i)(j) = conflictingColumns(j)
                                    End If
                                Next j
                            End If
                        Next i

                        'Apply the changed row with its combined values to the server.
                        'This change will persist at the server, but it will not be 
                        'downloaded with the SelectIncrementalUpdate command that we use.
                        'It will not download the change because it checks for the UpdateId,
                        'which is still set to the client that made the upload.
                        'We use the ChangesApplied event to set the UpdateId for the
                        'change to a value that represents the server. This ensures
                        'that the change is applied at the client during the download
                        'phase of synchronization (see SampleServerSyncProvider_ChangesApplied).
                        e.Action = ApplyAction.RetryWithForceWrite

                        Console.WriteLine(String.Empty)
                        Console.WriteLine("The custom change was retried at the server with RetryWithForceWrite.")


                    Case Else
                        Console.WriteLine(String.Empty)
                        Console.WriteLine("Not a valid resolution option.")
                End Select
            End If


            Console.WriteLine(String.Empty)
        End If

    End Sub 'SampleServerSyncProvider_ApplyChangeFailed


    Private Sub SampleServerSyncProvider_ChangesApplied(ByVal sender As Object, ByVal e As ChangesAppliedEventArgs)
        'If _updateConflictGuids contains at least one GUID, update the UpdateId
        'column so that each change is downloaded to the client. For more
        'information, see SampleServerSyncProvider_ApplyChangeFailed.
        If _updateConflictGuids.Count > 0 Then
            Dim updateTable As New SqlCommand()
            updateTable.Connection = CType(e.Connection, SqlConnection)
            updateTable.Transaction = CType(e.Transaction, SqlTransaction)
            updateTable.CommandText = String.Empty

            Dim i As Integer
            For i = 0 To _updateConflictGuids.Count - 1
                updateTable.CommandText += _
                    " UPDATE Sales.Customer SET UpdateId = '00000000-0000-0000-0000-000000000000' " _
                    + " WHERE CustomerId='" + _updateConflictGuids(i).ToString() + "'"
            Next i

            updateTable.ExecuteNonQuery()
        End If

    End Sub 'SampleServerSyncProvider_ChangesApplied
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
        'By default, the client database is created if it does not
        'exist.
        Dim util As New Utility()
        Me.ConnectionString = util.ClientConnString

        'Specify conflict resolution options for each type of
        'conflict or error that can occur. The client and server are
        'independent; therefore, these settings have no effect when changes 
        'are applied at the server. However, settings should agree with each
        'other. For example:
        ' * We specify a value of ServerWins for client delete /
        '   server update. On the server side, by default our commands will 
        '   ignore the conflicting delete and download the update to the 
        '   client. ServerWins is equivalent to setting RetryWithForceWrite
        '   on the client.
        ' * Conversely, we specify a value of ClientWins for client update /
        '   server delete. On the server side, we specify that our commands 
        '   should force write the update by turning it into an insert.
        '<snippetOCS_VB_Conflicts_ConflictResolver>
        Me.ConflictResolver.ClientDeleteServerUpdateAction = ResolveAction.ServerWins
        Me.ConflictResolver.ClientUpdateServerDeleteAction = ResolveAction.ClientWins
        'If any of the following conflicts or errors occur, the ApplyChangeFailed
        'event is raised.
        Me.ConflictResolver.ClientInsertServerInsertAction = ResolveAction.FireEvent
        Me.ConflictResolver.ClientUpdateServerUpdateAction = ResolveAction.FireEvent
        Me.ConflictResolver.StoreErrorAction = ResolveAction.FireEvent

        'Log information for the ApplyChangeFailed event and handle any
        'ResolveAction.FireEvent cases.
        AddHandler Me.ApplyChangeFailed, AddressOf SampleClientSyncProvider_ApplyChangeFailed
        '</snippetOCS_VB_Conflicts_ConflictResolver>

        'Use the following events to fix up schema on the client.
        'We use the CreatingSchema event to change the schema
        'by using the API. We use the SchemaCreated event 
        'to change the schema by using SQL.
        AddHandler Me.CreatingSchema, AddressOf SampleClientSyncProvider_CreatingSchema
        AddHandler Me.SchemaCreated, AddressOf SampleClientSyncProvider_SchemaCreated

    End Sub 'New


    '<snippetOCS_VB_Conflicts_ClientApplyChangeFailed>
    Private Sub SampleClientSyncProvider_ApplyChangeFailed(ByVal sender As Object, ByVal e As ApplyChangeFailedEventArgs)

        'Log event data from the client side.
        EventLogger.LogEvents(sender, e)

        'Force write any inserted server rows that are in conflict 
        'when they are downloaded.
        If e.Conflict.ConflictType = ConflictType.ClientInsertServerInsert Then
            e.Action = ApplyAction.RetryWithForceWrite
        End If

        If e.Conflict.ConflictType = ConflictType.ClientUpdateServerUpdate Then
            'Logic goes here.
        End If

        If e.Conflict.ConflictType = ConflictType.ErrorsOccurred Then
            'Logic goes here.
        End If

    End Sub 'SampleClientSyncProvider_ApplyChangeFailed
    '</snippetOCS_VB_Conflicts_ClientApplyChangeFailed>

    Private Sub SampleClientSyncProvider_CreatingSchema(ByVal sender As Object, ByVal e As CreatingSchemaEventArgs)

        'Set the RowGuid property because it is not copied
        'to the client by default. This is also a good time
        'to specify literal defaults with .Columns[ColName].DefaultValue,
        'but we will specify defaults like NEWID() by calling
        'ALTER TABLE after the table is created.
        e.Schema.Tables("Customer").Columns("CustomerId").RowGuid = True

    End Sub 'SampleClientSyncProvider_CreatingSchema


    Private Sub SampleClientSyncProvider_SchemaCreated(ByVal sender As Object, ByVal e As SchemaCreatedEventArgs)
        Dim tableName As String = e.Table.TableName
        Dim util As New Utility()

        'Call ALTER TABLE on the client. This must be done
        'over the same connection and within the same
        'transaction that Synchronization Services uses
        'to create the schema on the client.
        util.MakeSchemaChangesOnClient(e.Connection, e.Transaction, "Customer")

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
        Console.WriteLine("Upload Changes Applied: " & syncStatistics.UploadChangesApplied)
        Console.WriteLine("Upload Changes Failed: " & syncStatistics.UploadChangesFailed)
        Console.WriteLine("Total Changes Uploaded: " & syncStatistics.TotalChangesUploaded)
        Console.WriteLine("Download Changes Applied: " & syncStatistics.DownloadChangesApplied)
        Console.WriteLine("Download Changes Failed: " & syncStatistics.DownloadChangesFailed)
        Console.WriteLine("Total Changes Downloaded: " & syncStatistics.TotalChangesDownloaded)
        Console.WriteLine("Complete Time: " & syncStatistics.SyncCompleteTime)
        Console.WriteLine(String.Empty)

    End Sub 'DisplayStats
End Class 'SampleStats


Public Class EventLogger

    'Create client and server log files, and write to them
    'based on data from the ApplyChangeFailedEventArgs.
    Public Shared Sub LogEvents(ByVal sender As Object, ByVal e As ApplyChangeFailedEventArgs)
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

        outputText.AppendLine("** CONFLICTING CHANGE OR ERROR AT " & site.ToUpper() & " **")
        outputText.AppendLine("Table for which error or conflict occurred: " & e.TableMetadata.TableName)
        outputText.AppendLine("Sync stage: " & e.Conflict.SyncStage.ToString())
        outputText.AppendLine("Conflict type: " & e.Conflict.ConflictType.ToString())

        'If it is a data conflict instead of an error, print out
        'the values of the rows at the client and server.
        If e.Conflict.ConflictType <> ConflictType.ErrorsOccurred AndAlso e.Conflict.ConflictType <> ConflictType.Unknown Then

            Dim serverChange As DataTable = e.Conflict.ServerChange
            Dim clientChange As DataTable = e.Conflict.ClientChange
            Dim serverRows As Integer = serverChange.Rows.Count
            Dim clientRows As Integer = clientChange.Rows.Count
            Dim serverColumns As Integer = serverChange.Columns.Count
            Dim clientColumns As Integer = clientChange.Columns.Count

            Dim i As Integer
            For i = 0 To serverRows - 1
                outputText.Append("Server row: ")

                Dim j As Integer
                For j = 0 To serverColumns - 1
                    outputText.Append(serverChange.Rows(i)(j).ToString() & " | ")
                Next j

                outputText.AppendLine(String.Empty)
            Next i

            For i = 0 To clientRows - 1
                outputText.Append("Client row: ")

                Dim j As Integer
                For j = 0 To clientColumns - 1
                    outputText.Append(clientChange.Rows(i)(j).ToString() & " | ")
                Next j

                outputText.AppendLine(String.Empty)
            Next i
        End If

        If e.Conflict.ConflictType = ConflictType.ErrorsOccurred Then
            outputText.AppendLine("Error message: " + e.Error.Message)
        End If

        streamWriter.WriteLine(DateTime.Now.ToShortTimeString() & " | " + outputText.ToString())
        streamWriter.Flush()
        streamWriter.Dispose()

    End Sub 'LogEvents 
End Class 'EventLogger
'</snippetOCS_VB_Conflicts>