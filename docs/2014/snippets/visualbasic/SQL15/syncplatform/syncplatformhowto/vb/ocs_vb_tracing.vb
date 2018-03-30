'<snippetOCS_VB_Tracing>
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
        'string information and making changes to the server database.
        Dim util As New Utility()

        'The SampleStats class handles information from the SyncStatistics
        'object that the Synchronize method returns.
        Dim sampleStats As New SampleStats()

        'Delete and re-create the database. The client synchronization
        'provider also enables you to create the client database 
        'if it does not exist.
        util.SetClientPassword()
        util.RecreateClientDatabase()

        'Write to the console which tracing levels are enabled. The app.config
        'file specifies a value of 3 for the SyncTracer switch, which corresponds
        'to Info. Therefore, Error, Warning, and Info return True, and Verbose
        'returns False.
        Console.WriteLine("")
        '<snippetOCS_VB_Tracing_LevelsEnabled>
        Console.WriteLine("** Tracing Levels Enabled for this Application **")
        Console.WriteLine("Error: " + SyncTracer.IsErrorEnabled().ToString())
        Console.WriteLine("Warning: " + SyncTracer.IsWarningEnabled().ToString())
        Console.WriteLine("Info: " + SyncTracer.IsInfoEnabled().ToString())
        Console.WriteLine("Verbose: " + SyncTracer.IsVerboseEnabled().ToString())
        '</snippetOCS_VB_Tracing_LevelsEnabled>

        'Initial synchronization. Instantiate the SyncAgent
        'and call Synchronize.
        '<snippetOCS_VB_Tracing_Synchronize>
        Dim sampleSyncAgent As New SampleSyncAgent()
        Dim syncStatistics As SyncStatistics = sampleSyncAgent.Synchronize()
        '</snippetOCS_VB_Tracing_Synchronize>
        sampleStats.DisplayStats(syncStatistics, "initial")

        'Make a change at the client that fails when it is
        'applied at the server. The constraint violation
        'is automatically written to the trace file as a warning.
        util.MakeFailingChangesOnClient()

        'Make changes at the client and server that conflict
        'when they are synchronized. The conflicts are written
        'to the trace file in the SampleServerSyncProvider_ApplyChangeFailed
        'event handler.
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
        'DownloadOnly.
        '<snippetOCS_VB_Tracing_CustomerSyncTable>
        Dim customerSyncTable As New SyncTable("Customer")
        customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable
        customerSyncTable.SyncDirection = SyncDirection.Bidirectional
        Me.Configuration.SyncTables.Add(customerSyncTable)
        '</snippetOCS_VB_Tracing_CustomerSyncTable>

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
        '<snippetOCS_VB_Tracing_NewAnchorCommand>
        Dim selectNewAnchorCommand As New SqlCommand()
        Dim newAnchorVariable As String = "@" + SyncSession.SyncNewReceivedAnchor
        selectNewAnchorCommand.CommandText = "SELECT " + newAnchorVariable + " = min_active_rowversion() - 1"
        selectNewAnchorCommand.Parameters.Add(newAnchorVariable, SqlDbType.Timestamp)
        selectNewAnchorCommand.Parameters(newAnchorVariable).Direction = ParameterDirection.Output
        selectNewAnchorCommand.Connection = serverConn
        Me.SelectNewAnchorCommand = selectNewAnchorCommand
        '</snippetOCS_VB_Tracing_NewAnchorCommand>

        'Create a SyncAdapter for the Customer table by using 
        'the SqlSyncAdapterBuilder:
        '  * Specify the base table and tombstone table names.
        '  * Specify the columns that are used to track when
        '    and where changes are made.
        '  * Specify bidirectional synchronization.
        '  * Call ToSyncAdapter to create the SyncAdapter.
        '  * Specify a name for the SyncAdapter that matches the
        '    the name specified for the corresponding SyncTable.
        '    Do not include the schema names (Sales in this case).
        '<snippetOCS_VB_Tracing_CustomerAdapterBuilder>
        Dim customerBuilder As New SqlSyncAdapterBuilder(serverConn)

        customerBuilder.TableName = "Sales.Customer"
        customerBuilder.TombstoneTableName = customerBuilder.TableName + "_Tombstone"
        customerBuilder.SyncDirection = SyncDirection.Bidirectional
        customerBuilder.CreationTrackingColumn = "InsertTimestamp"
        customerBuilder.UpdateTrackingColumn = "UpdateTimestamp"
        customerBuilder.DeletionTrackingColumn = "DeleteTimestamp"
        customerBuilder.CreationOriginatorIdColumn = "InsertId"
        customerBuilder.UpdateOriginatorIdColumn = "UpdateId"
        customerBuilder.DeletionOriginatorIdColumn = "DeleteId"

        Dim customerSyncAdapter As SyncAdapter = customerBuilder.ToSyncAdapter()
        customerSyncAdapter.TableName = "Customer"
        Me.SyncAdapters.Add(customerSyncAdapter)
        '</snippetOCS_VB_Tracing_CustomerAdapterBuilder>

        'Handle the ApplyChangeFailed event. This allows us to write  
        'information to the trace file about any conflicts that occur.
        AddHandler Me.ApplyChangeFailed, AddressOf SampleServerSyncProvider_ApplyChangeFailed

     End Sub 'New


    Private Sub SampleServerSyncProvider_ApplyChangeFailed(ByVal sender As Object, ByVal e As ApplyChangeFailedEventArgs)

        'Verbose tracing includes information about conflicts. In this application,
        'Verbose tracing is disabled, and we have decided to flag conflicts with a 
        'warning.
        'Check if Verbose tracing is enabled and if the conflict is an error.
        'If the conflict is not an error, we write a warning to the trace file
        'with information about the conflict.
        '<snippetOCS_VB_Tracing_ApplyChangeFailed>
        If SyncTracer.IsVerboseEnabled() = False AndAlso e.Conflict.ConflictType <> ConflictType.ErrorsOccurred Then
            Dim conflictingServerChange As DataTable = e.Conflict.ServerChange
            Dim conflictingClientChange As DataTable = e.Conflict.ClientChange
            Dim serverColumnCount As Integer = conflictingServerChange.Columns.Count
            Dim clientColumnCount As Integer = conflictingClientChange.Columns.Count
            Dim clientRowAsString As New StringBuilder()
            Dim serverRowAsString As New StringBuilder()

            Dim i As Integer
            For i = 1 To clientColumnCount - 1
                clientRowAsString.Append(conflictingClientChange.Rows(0)(i).ToString() & " | ")
            Next i

            For i = 1 To serverColumnCount - 1
                serverRowAsString.Append(conflictingServerChange.Rows(0)(i).ToString() & " | ")
            Next i

            SyncTracer.Warning(1, "CONFLICT DETECTED FOR CLIENT {0}", e.Session.ClientId)
            SyncTracer.Warning(2, "** Client change **")
            SyncTracer.Warning(2, clientRowAsString.ToString())
            SyncTracer.Warning(2, "** Server change **")
            SyncTracer.Warning(2, serverRowAsString.ToString())
        End If
        '</snippetOCS_VB_Tracing_ApplyChangeFailed>

    End Sub 'SampleServerSyncProvider_ApplyChangeFailed 
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

        '<snippetOCS_VB_Tracing_Statistics>
        Console.WriteLine("Start Time: " & syncStatistics.SyncStartTime)
        Console.WriteLine("Total Changes Downloaded: " & syncStatistics.TotalChangesDownloaded)
        Console.WriteLine("Total Changes Uploaded: " & syncStatistics.TotalChangesUploaded)
        Console.WriteLine("Complete Time: " & syncStatistics.SyncCompleteTime)
        Console.WriteLine(String.Empty)
        '</snippetOCS_VB_Tracing_Statistics>

    End Sub 'DisplayStats 
End Class 'SampleStats
'</snippetOCS_VB_Tracing>
