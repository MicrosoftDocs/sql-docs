'<snippetOCSv2_VB_Peer_Conflicts>
Imports System
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Synchronization
Imports Microsoft.Synchronization.Data


Class Program

    Shared Sub Main(ByVal args() As String)

        'The Utility class handles all functionality that is not
        'directly related to synchronization, such as holding peerConnection 
        'string information and making changes to the server database.
        Dim util As New Utility()

        '<snippetOCSv2_VB_Peer_Conflicts_Synchronize>
        'The SampleStats class handles information from the SyncStatistics
        'object that the Synchronize method returns.
        Dim sampleStats As New SampleStats()

        Try
            'Initial synchronization. Instantiate the SyncOrchestrator
            'and call Synchronize. Note that data is not synchronized during the
            'session between peer 1 and peer 3, because all rows have already
            'been delivered to peer 3 during its synchronization session with peer 2.     
            Dim sampleSyncAgent As SyncOrchestrator
            Dim syncStatistics As SyncOperationStatistics

            sampleSyncAgent = New SampleSyncAgent(util.Peer1ConnString, util.Peer2ConnString)
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "initial")

            sampleSyncAgent = New SampleSyncAgent(util.Peer2ConnString, util.Peer3ConnString)
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "initial")

            sampleSyncAgent = New SampleSyncAgent(util.Peer1ConnString, util.Peer3ConnString)
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "initial")


        Catch ex As DbOutdatedSyncException
            Console.WriteLine("Outdated Knowledge: " & ex.OutdatedPeerSyncKnowledge.ToString() & " Clean up knowledge: " & ex.MissingCleanupKnowledge.ToString())
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        '</snippetOCSv2_VB_Peer_Conflicts_Synchronize>

        'Make conflicting changes in each peer database.
        util.MakeConflictingChangesOnPeer(util.Peer1ConnString, "Customer")
        util.MakeConflictingChangesOnPeer(util.Peer2ConnString, "Customer")
        util.MakeConflictingChangesOnPeer(util.Peer3ConnString, "Customer")

        Try
            'Subsequent synchronization. Changes are now synchronized between all
            'peers.
            Dim sampleSyncAgent As SyncOrchestrator
            Dim syncStatistics As SyncOperationStatistics

            sampleSyncAgent = New SampleSyncAgent(util.Peer1ConnString, util.Peer2ConnString)
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "subsequent")

            sampleSyncAgent = New SampleSyncAgent(util.Peer2ConnString, util.Peer3ConnString)
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "subsequent")

            sampleSyncAgent = New SampleSyncAgent(util.Peer1ConnString, util.Peer3ConnString)
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "subsequent")


        Catch ex As DbOutdatedSyncException
            Console.WriteLine("Outdated Knowledge: " & ex.OutdatedPeerSyncKnowledge.ToString() & " Clean up knowledge: " & ex.MissingCleanupKnowledge.ToString())
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        'Make a change in SyncSamplesDb_Peer2 that will fail when it
        'is synchronized with SyncSamplesDb_Peer1.
        util.MakeFailingChangeOnPeer(util.Peer2ConnString)

        Try
            Dim sampleSyncAgent As SyncOrchestrator
            Dim syncStatistics As SyncOperationStatistics

            sampleSyncAgent = New SampleSyncAgent(util.Peer1ConnString, util.Peer2ConnString)
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "subsequent")


        Catch ex As DbOutdatedSyncException
            Console.WriteLine("Outdated Knowledge: " & ex.OutdatedPeerSyncKnowledge.ToString() & " Clean up knowledge: " & ex.MissingCleanupKnowledge.ToString())
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        'Return peer data back to its original state.
        util.CleanUpPeer(util.Peer1ConnString)
        util.CleanUpPeer(util.Peer2ConnString)
        util.CleanUpPeer(util.Peer3ConnString)

        'Exit.
        Console.Write(vbLf & "Press Enter to close the window.")
        Console.ReadLine()

    End Sub 'Main
End Class 'Program


'Create a class that is derived from 
'Microsoft.Synchronization.SyncOrchestrator.
'<snippetOCSv2_VB_Peer_Conflicts_SampleSyncAgent>
Public Class SampleSyncAgent
    Inherits SyncOrchestrator

    'Create class-level variables so that the ApplyChangeFailedEvent 
    'handler can use them.
    Private _localProviderDatabase As String
    Private _remoteProviderDatabase As String


    Public Sub New(ByVal localProviderConnString As String, ByVal remoteProviderConnString As String)

        'Instantiate the sample provider that allows us to create a provider
        'for both of the peers that are being synchronized.
        Dim sampleSyncProvider As New SampleSyncProvider()

        'Instantiate a DbSyncProvider for the local peer and the remote peer.
        'For example, if this code is running at peer1 and is
        'synchronizing with peer2, peer1 would be the local provider
        'and peer2 the remote provider.                 
        Dim localProvider As New DbSyncProvider()
        Dim remoteProvider As New DbSyncProvider()

        'Create a provider by using the SetupSyncProvider on the sample class.             
        sampleSyncProvider.SetupSyncProvider(localProviderConnString, localProvider)
        localProvider.SyncProviderPosition = SyncProviderPosition.Local

        sampleSyncProvider.SetupSyncProvider(remoteProviderConnString, remoteProvider)
        remoteProvider.SyncProviderPosition = SyncProviderPosition.Remote

        'Specify event handlers for the ApplyChangeFailed event for each provider.
        'The handlers are used to resolve conflicting rows and log error information.
        '<snippetOCSv2_VB_Peer_Conflicts_DbApplyChangeFailedEventHandler>
        AddHandler localProvider.ApplyChangeFailed, AddressOf dbProvider_ApplyChangeFailed
        AddHandler remoteProvider.ApplyChangeFailed, AddressOf dbProvider_ApplyChangeFailed
        '</snippetOCSv2_VB_Peer_Conflicts_DbApplyChangeFailedEventHandler>

        Me.LocalProvider = localProvider
        Me.RemoteProvider = remoteProvider
        Me.Direction = SyncDirectionOrder.UploadAndDownload

        _localProviderDatabase = localProvider.Connection.Database.ToString()
        _remoteProviderDatabase = remoteProvider.Connection.Database.ToString()

    End Sub 'New


    Private Sub dbProvider_ApplyChangeFailed(ByVal sender As Object, ByVal e As DbApplyChangeFailedEventArgs)

        'For conflict detection, the "local" database is the one at which the
        'ApplyChangeFailed event occurs. We determine at which database the event
        'fired and then compare the name of that database to the names of
        'the databases specified as the LocalProvider and RemoteProvider.
        Dim DbConflictDetected As String = e.Connection.Database.ToString()
        Dim DbOther As String

        DbOther = IIf(DbConflictDetected = _localProviderDatabase, _remoteProviderDatabase, _localProviderDatabase)

        Console.WriteLine(String.Empty)
        Console.WriteLine("Conflict of type " & e.Conflict.Type.ToString & " was detected at " & DbConflictDetected & ".")

        '<snippetOCSv2_VB_Peer_Conflicts_LocalUpdateRemoteUpdate>
        If e.Conflict.Type = DbConflictType.LocalUpdateRemoteUpdate Then

            'Get the conflicting changes from the Conflict object
            'and display them. The Conflict object holds a copy
            'of the changes; updates to this object will not be 
            'applied. To make changes, use the Context object.
            Dim conflictingRemoteChange As DataTable = e.Conflict.RemoteChange
            Dim conflictingLocalChange As DataTable = e.Conflict.LocalChange
            Dim remoteColumnCount As Integer = conflictingRemoteChange.Columns.Count
            Dim localColumnCount As Integer = conflictingLocalChange.Columns.Count

            Console.WriteLine(String.Empty)
            Console.WriteLine(String.Empty)
            Console.WriteLine("Row from database " & DbConflictDetected)
            Console.Write(" | ")

            'Display the local row. As mentioned above, this is the row
            'from the database at which the conflict was detected.
            Dim i As Integer
            For i = 0 To localColumnCount - 1
                Console.Write(conflictingLocalChange.Rows(0)(i).ToString & " | ")
            Next i

            Console.WriteLine(String.Empty)
            Console.WriteLine(String.Empty)
            Console.WriteLine(String.Empty)
            Console.WriteLine("Row from database " & DbOther)
            Console.Write(" | ")

            'Display the remote row.
            For i = 0 To remoteColumnCount - 1
                Console.Write(conflictingRemoteChange.Rows(0)(i).ToString & " | ")
            Next i

            'Ask for a conflict resolution option.
            Console.WriteLine(String.Empty)
            Console.WriteLine(String.Empty)
            Console.WriteLine("Enter a resolution option for this conflict:")
            Console.WriteLine("A = change from " & DbConflictDetected & " wins.")
            Console.WriteLine("B = change from " & DbOther & " wins.")

            Dim conflictResolution As String = Console.ReadLine()
            conflictResolution.ToUpper()

            If conflictResolution = "A" Then
                e.Action = ApplyAction.Continue

            ElseIf conflictResolution = "B" Then
                e.Action = ApplyAction.RetryWithForceWrite

            Else
                Console.WriteLine(String.Empty)
                Console.WriteLine("Not a valid resolution option.")
            End If
            '</snippetOCSv2_VB_Peer_Conflicts_LocalUpdateRemoteUpdate>

            'Write any errors to a log file.
            '<snippetOCSv2_VB_Peer_Conflicts_ErrorsOccurred>
        ElseIf e.Conflict.Type = DbConflictType.ErrorsOccurred Then

            Dim logFile As String = "C:\SyncErrorLog.txt"

            Console.WriteLine(String.Empty)
            Console.WriteLine("An error occurred during synchronization.")
            Console.WriteLine("This error has been logged to " & logFile & ".")

            Dim streamWriter As StreamWriter = File.AppendText(logFile)
            Dim outputText As New StringBuilder()

            outputText.AppendLine("** APPLY CHANGE FAILURE AT " & DbConflictDetected.ToUpper() & " **")
            outputText.AppendLine("Error source: " & e.Error.Source)
            outputText.AppendLine("Error message: " & e.Error.Message)

            streamWriter.WriteLine(DateTime.Now.ToShortTimeString() & " | " & outputText.ToString())
            streamWriter.Flush()
            streamWriter.Dispose()
        End If
        '</snippetOCSv2_VB_Peer_Conflicts_ErrorsOccurred>

    End Sub 'dbProvider_ApplyChangeFailed
End Class 'SampleSyncAgent 
'</snippetOCSv2_VB_Peer_Conflicts_SampleSyncAgent>

Public Class SampleSyncProvider

    '<snippetOCSv2_VB_Peer_Conflicts_SetupSyncProvider>
    Public Function SetupSyncProvider(ByVal peerConnString As String, ByVal sampleProvider As DbSyncProvider) As DbSyncProvider

        '<snippetOCSv2_VB_Peer_Conflicts_Scope>
        Dim peerConnection As New SqlConnection(peerConnString)
        sampleProvider.Connection = peerConnection
        sampleProvider.ScopeName = "Sales"
        '</snippetOCSv2_VB_Peer_Conflicts_Scope>

        'Create a DbSyncAdapter object for the Customer table and associate it 
        'with the DbSyncProvider. Following the DataAdapter style in ADO.NET, 
        'DbSyncAdapter is the equivalent for synchronization. The commands that 
        'are specified for the DbSyncAdapter object call stored procedures
        'that are created in each peer database.
        '<snippetOCSv2_VB_Peer_Conflicts_AdapterCustomer>
        Dim adapterCustomer As New DbSyncAdapter("Customer")

        'Specify the primary key, which Sync Services uses
        'to identify each row during synchronization.
        adapterCustomer.RowIdColumns.Add("CustomerId")
        '</snippetOCSv2_VB_Peer_Conflicts_AdapterCustomer>

        'Specify the command to select incremental changes.
        'In this command and other commands, session variables are
        'used to pass information at runtime. DbSyncSession.SyncMetadataOnly 
        'and SyncMinTimestamp are two of the string constants that
        'the DbSyncSession class exposes. You could also include 
        '@sync_metadata_only and @sync_min_timestamp directly in your 
        'queries:
        '*  sync_metadata_only is used by Sync Services as an optimization
        '   in some queries.
        '* The value of the sync_min_timestamp session variable is compared to
        '   values in the sync_row_timestamp column in the tracking table to 
        '   determine which rows to select.
        '<snippetOCSv2_VB_Peer_Conflicts_SelectIncrementalChangesCommand>
        Dim chgsCustomerCmd As New SqlCommand()

        With chgsCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_SelectChanges"
            .Parameters.Add("@" + DbSyncSession.SyncMetadataOnly, SqlDbType.Int)
            .Parameters.Add("@" + DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" + DbSyncSession.SyncScopeLocalId, SqlDbType.Int)
            .Parameters.Add("@" + DbSyncSession.SyncInitialize, SqlDbType.Int)
        End With

        adapterCustomer.SelectIncrementalChangesCommand = chgsCustomerCmd
        '</snippetOCSv2_VB_Peer_Conflicts_SelectIncrementalChangesCommand>

        'Specify the command to insert rows.
        'The sync_row_count session variable is used in this command 
        'and other commands to return a count of the rows affected by an operation. 
        'A count of 0 indicates that an operation failed.
        '<snippetOCSv2_VB_Peer_Conflicts_InsertCommand>
        Dim insCustomerCmd As New SqlCommand()

        With insCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_ApplyInsert"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@CustomerName", SqlDbType.NVarChar)
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@CustomerType", SqlDbType.NVarChar)
            .Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        End With

        adapterCustomer.InsertCommand = insCustomerCmd
        '</snippetOCSv2_VB_Peer_Conflicts_InsertCommand>


        'Specify the command to update rows.
        'The value of the sync_min_timestamp session variable is compared to
        'values in the sync_row_timestamp column in the tracking table to 
        'determine which rows to update.
        '<snippetOCSv2_VB_Peer_Conflicts_UpdateCommand>
        Dim updCustomerCmd As New SqlCommand()

        With updCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_ApplyUpdate"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@CustomerName", SqlDbType.NVarChar)
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@CustomerType", SqlDbType.NVarChar)
            .Parameters.Add("@" + DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
            .Parameters.Add("@" + DbSyncSession.SyncForceWrite, SqlDbType.Int)
        End With

        adapterCustomer.UpdateCommand = updCustomerCmd
        '</snippetOCSv2_VB_Peer_Conflicts_UpdateCommand>


        'Specify the command to delete rows.
        'The value of the sync_min_timestamp session variable is compared to
        'values in the sync_row_timestamp column in the tracking table to 
        'determine which rows to delete.
        '<snippetOCSv2_VB_Peer_Conflicts_DeleteCommand>
        Dim delCustomerCmd As New SqlCommand()

        With delCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_ApplyDelete"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" + DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
            .Parameters.Add("@" + DbSyncSession.SyncForceWrite, SqlDbType.Int)
        End With

        adapterCustomer.DeleteCommand = delCustomerCmd
        '</snippetOCSv2_VB_Peer_Conflicts_DeleteCommand>

        'Specify the command to select any conflicting rows.
        '<snippetOCSv2_VB_Peer_Conflicts_SelectRowCommand>
        Dim selRowCustomerCmd As New SqlCommand()

        With selRowCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_SelectRow"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" + DbSyncSession.SyncScopeLocalId, SqlDbType.Int)
        End With

        adapterCustomer.SelectRowCommand = selRowCustomerCmd
        '</snippetOCSv2_VB_Peer_Conflicts_SelectRowCommand>


        'Specify the command to insert metadata rows.
        'The session variables in this command relate to columns in
        'the tracking table. These are the same columns
        'that were specified as DbSyncAdapter properties at the beginning 
        'of this code example.
        '<snippetOCSv2_VB_Peer_Conflicts_InsertMetadataCommand>
        Dim insMetadataCustomerCmd As New SqlCommand()

        With insMetadataCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_InsertMetadata"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" + DbSyncSession.SyncScopeLocalId, SqlDbType.Int)
            .Parameters.Add("@" + DbSyncSession.SyncRowTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" + DbSyncSession.SyncCreatePeerKey, SqlDbType.Int)
            .Parameters.Add("@" + DbSyncSession.SyncCreatePeerTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" + DbSyncSession.SyncUpdatePeerKey, SqlDbType.Int)
            .Parameters.Add("@" + DbSyncSession.SyncUpdatePeerTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" + DbSyncSession.SyncRowIsTombstone, SqlDbType.Int)
            .Parameters.Add("@" + DbSyncSession.SyncCheckConcurrency, SqlDbType.Int)
            .Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        End With

        adapterCustomer.InsertMetadataCommand = insMetadataCustomerCmd
        '</snippetOCSv2_VB_Peer_Conflicts_InsertMetadataCommand>


        'Specify the command to update metadata rows.
        '<snippetOCSv2_VB_Peer_Conflicts_UpdateMetadataCommand>
        Dim updMetadataCustomerCmd As New SqlCommand()

        With updMetadataCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_UpdateMetadata"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" + DbSyncSession.SyncScopeLocalId, SqlDbType.Int)
            .Parameters.Add("@" + DbSyncSession.SyncRowTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" + DbSyncSession.SyncCreatePeerKey, SqlDbType.Int)
            .Parameters.Add("@" + DbSyncSession.SyncCreatePeerTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" + DbSyncSession.SyncUpdatePeerKey, SqlDbType.Int)
            .Parameters.Add("@" + DbSyncSession.SyncUpdatePeerTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" + DbSyncSession.SyncRowIsTombstone, SqlDbType.Int)
            .Parameters.Add("@" + DbSyncSession.SyncCheckConcurrency, SqlDbType.Int)
            .Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        End With

        adapterCustomer.UpdateMetadataCommand = updMetadataCustomerCmd
        '</snippetOCSv2_VB_Peer_Conflicts_UpdateMetadataCommand>

        'Specify the command to delete metadata rows.
        '<snippetOCSv2_VB_Peer_Conflicts_DeleteMetadataCommand>
        Dim delMetadataCustomerCmd As New SqlCommand()

        With delMetadataCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_DeleteMetadata"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" + DbSyncSession.SyncCheckConcurrency, SqlDbType.Int)
            .Parameters.Add("@" + DbSyncSession.SyncRowTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        End With

        adapterCustomer.DeleteMetadataCommand = delMetadataCustomerCmd
        '</snippetOCSv2_VB_Peer_Conflicts_DeleteMetadataCommand>


        sampleProvider.SyncAdapters.Add(adapterCustomer)


        ' Configure commands that relate to the provider itself rather 
        ' than the DbSyncAdapter object for each table:
        ' * SelectNewTimestampCommand: Returns the new high watermark for 
        '   the current synchronization session.
        ' * SelectScopeInfoCommand: Returns sync knowledge, cleanup knowledge, 
        '   and a scope version (timestamp).
        ' * UpdateScopeInfoCommand: Sets new values for sync knowledge and cleanup knowledge.            
        ' * SelectTableMaxTimestampsCommand (optional): Returns the maximum timestamp from each base table 
        '   or tracking table, to determine whether for each table the destination already 
        '   has all of the changes from the source. If a destination table has all the changes,
        '   SelectIncrementalChangesCommand is not called for that table.
        ' There are additional commands related to metadata cleanup that are not 
        ' included in this application.           


        'Select a new timestamp.
        'During each synchronization, the new value and
        'the last value from the previous synchronization
        'are used: the set of changes between these upper and
        'lower bounds is synchronized.
        '<snippetOCSv2_VB_Peer_Conflicts_NewAnchorCommand>
        Dim newTimestampVariable As String = "@" + DbSyncSession.SyncNewTimestamp

        Dim selectNewTimestampCommand As New SqlCommand()

        With selectNewTimestampCommand
            .CommandText = "SELECT " + newTimestampVariable + " = min_active_rowversion() - 1"
            .Parameters.Add(newTimestampVariable, SqlDbType.Timestamp)
            .Parameters(newTimestampVariable).Direction = ParameterDirection.Output
        End With

        sampleProvider.SelectNewTimestampCommand = selectNewTimestampCommand
        '</snippetOCSv2_VB_Peer_Conflicts_NewAnchorCommand>

        'Specify the command to select local replica metadata. 
        '<snippetOCSv2_VB_Peer_Conflicts_SelectScopeInfoCommand>
        Dim selReplicaInfoCmd As New SqlCommand()

        With selReplicaInfoCmd
            .CommandType = CommandType.Text
            .CommandText = "SELECT " _
                         & "scope_id, " _
                         & "scope_local_id, " _
                         & "scope_sync_knowledge, " _
                         & "scope_tombstone_cleanup_knowledge, " _
                         & "scope_timestamp " _
                         & "FROM Sync.ScopeInfo " _
                         & "WHERE scope_name = @" + DbSyncSession.SyncScopeName
            .Parameters.Add("@" + DbSyncSession.SyncScopeName, SqlDbType.NVarChar, 100)
        End With

        sampleProvider.SelectScopeInfoCommand = selReplicaInfoCmd
        '</snippetOCSv2_VB_Peer_Conflicts_SelectScopeInfoCommand>


        'Specify the command to update local replica metadata. 
        '<snippetOCSv2_VB_Peer_Conflicts_UpdateScopeInfoCommand>
        Dim updReplicaInfoCmd As New SqlCommand()

        With updReplicaInfoCmd
            .CommandType = CommandType.Text
            .CommandText = "UPDATE  Sync.ScopeInfo SET " _
                         & "scope_sync_knowledge = @" + DbSyncSession.SyncScopeKnowledge + ", " _
                         & "scope_id = @" + DbSyncSession.SyncScopeId + ", " _
                         & "scope_tombstone_cleanup_knowledge = @" + DbSyncSession.SyncScopeCleanupKnowledge + " " _
                         & "WHERE scope_name = @" + DbSyncSession.SyncScopeName + " AND " _
                         & " ( @" + DbSyncSession.SyncCheckConcurrency + " = 0 OR scope_timestamp = @" + DbSyncSession.SyncScopeTimestamp + "); " _
                         & "set @" + DbSyncSession.SyncRowCount + " = @@rowcount"
            .Parameters.Add("@" + DbSyncSession.SyncScopeKnowledge, SqlDbType.VarBinary, 10000)
            .Parameters.Add("@" + DbSyncSession.SyncScopeCleanupKnowledge, SqlDbType.VarBinary, 10000)
            .Parameters.Add("@" + DbSyncSession.SyncScopeName, SqlDbType.NVarChar, 100)
            .Parameters.Add("@" + DbSyncSession.SyncCheckConcurrency, SqlDbType.Int)
            .Parameters.Add("@" + DbSyncSession.SyncScopeId, SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" + DbSyncSession.SyncScopeTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        End With

        sampleProvider.UpdateScopeInfoCommand = updReplicaInfoCmd
        '</snippetOCSv2_VB_Peer_Conflicts_UpdateScopeInfoCommand>

        'Return the maximum timestamp from the Customer_Tracking table.
        'If more tables are synchronized, the query should UNION
        'all of the results. The table name is not schema-qualified
        'in this case because the name was not schema qualified in the
        'DbSyncAdapter constructor.
        '<snippetOCSv2_VB_Peer_Conflicts_SelectTableMaxTimestampsCommand>
        Dim selTableMaxTsCmd As New SqlCommand()
        selTableMaxTsCmd.CommandType = CommandType.Text
        selTableMaxTsCmd.CommandText = "SELECT 'Customer' AS table_name, " _
                                     & "MAX(local_update_peer_timestamp) AS max_timestamp " _
                                     & "FROM Sync.Customer_Tracking"
        sampleProvider.SelectTableMaxTimestampsCommand = selTableMaxTsCmd
        '</snippetOCSv2_VB_Peer_Conflicts_SelectTableMaxTimestampsCommand>

        Return sampleProvider

    End Function 'SetupSyncProvider
    '</snippetOCSv2_VB_Peer_Conflicts_SetupSyncProvider>

End Class 'SampleSyncProvider


'Handle the statistics that are returned by the SyncAgent.
Public Class SampleStats

    Public Sub DisplayStats(ByVal syncStatistics As SyncOperationStatistics, ByVal syncType As String)
        Console.WriteLine(String.Empty)
        If syncType = "initial" Then
            Console.WriteLine("****** Initial Synchronization ******")
        ElseIf syncType = "subsequent" Then
            Console.WriteLine("***** Subsequent Synchronization ****")
        End If

        Console.WriteLine("Start Time: " & syncStatistics.SyncStartTime)
        Console.WriteLine("Total Changes Uploaded: " & syncStatistics.UploadChangesTotal)
        Console.WriteLine("Total Changes Downloaded: " & syncStatistics.DownloadChangesTotal)
        Console.WriteLine("Complete Time: " & syncStatistics.SyncEndTime)
        Console.WriteLine(String.Empty)

    End Sub 'DisplayStats
End Class 'SampleStats
'</snippetOCSv2_VB_Peer_Conflicts>