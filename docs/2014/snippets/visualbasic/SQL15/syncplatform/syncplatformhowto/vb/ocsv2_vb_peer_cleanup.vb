'<snippetOCSv2_VB_Peer_Cleanup> 
Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlServerCe
Imports Microsoft.Synchronization
Imports Microsoft.Synchronization.Data
Imports Microsoft.Synchronization.Data.SqlServerCe

Class Program
    Shared Sub Main(ByVal args As String())

        'The Utility class handles all functionality that is not 
        'directly related to synchronization, such as holding peerConnection 
        'string information and making changes to the server database. 
        Dim util As New Utility()
        util.RecreateCePeerDatabase(util.Ce1ConnString)

        '<snippetOCSv2_VB_Peer_Cleanup_Synchronize> 
        'The SampleStats class handles information from the SyncStatistics 
        'object that the Synchronize method returns. 
        Dim sampleStats As New SampleStats()
        Dim sampleSyncProvider As New SampleSyncProvider()

        Try
            'Initial synchronization. Instantiate the SyncOrchestrator 
            'and call Synchronize. 
            sampleSyncProvider = New SampleSyncProvider()
            Dim sampleSyncAgent As SyncOrchestrator
            Dim syncStatistics As SyncOperationStatistics

            'The integer passed to ConfigureDbSyncProvider is how old that metadata 
            'can be (in days) before it is deleted when CleanupMetadata() is called. 
            'The integer value is only relevant if CleanupMetadata() is called, as 
            'demonstrated later in this application. 
            sampleSyncAgent = New SampleSyncAgent(sampleSyncProvider.ConfigureDbSyncProvider( _
                                                  util.Peer1ConnString, 7), _
                                                  sampleSyncProvider.ConfigureDbSyncProvider( _
                                                  util.Peer3ConnString, 7))
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "initial")

            sampleSyncAgent = New SampleSyncAgent(sampleSyncProvider.ConfigureDbSyncProvider( _
                                                  util.Peer3ConnString, 7), _
                                                  sampleSyncProvider.ConfigureCeSyncProvider( _
                                                  util.Ce1ConnString))
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "initial")
        Catch ex As DbOutdatedSyncException


            Console.WriteLine(("Outdated Knowledge: " & ex.OutdatedPeerSyncKnowledge.ToString() _
                               & " Clean up knowledge: ") + ex.MissingCleanupKnowledge.ToString())
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        '</snippetOCSv2_VB_Peer_Cleanup_Synchronize> 


        'Update a row on peer 1. 
        util.MakeDataChangesOnPeer(util.Peer1ConnString, "Customer")


        'Instantiate a provider, connect to peer 1, and delete tombstone metadata that 
        'is older than 7 days. 
        '<snippetOCSv2_VB_Peer_Cleanup_CleanupMetadata> 
        sampleSyncProvider = New SampleSyncProvider()
        Dim provider1 As DbSyncProvider = sampleSyncProvider.ConfigureDbSyncProvider(util.Peer1ConnString, 7)

        If provider1.CleanupMetadata() = True Then
            Console.WriteLine([String].Empty)
            Console.WriteLine("Metadata cleanup ran in the SyncSamplesDb_Peer1 database.")
            Console.WriteLine("Metadata more than 7 days old was deleted.")
        Else
            Console.WriteLine("Metadata cleanup failed, most likely due to concurrency issues.")
        End If
        '</snippetOCSv2_VB_Peer_Cleanup_CleanupMetadata> 

        'Synchronize. 
        Try
            Dim sampleSyncAgent As SyncOrchestrator
            Dim syncStatistics As SyncOperationStatistics

            sampleSyncAgent = New SampleSyncAgent(sampleSyncProvider.ConfigureDbSyncProvider( _
                                                  util.Peer1ConnString, 7), _
                                                  sampleSyncProvider.ConfigureDbSyncProvider( _
                                                  util.Peer3ConnString, 7))
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "subsequent")
        Catch ex As DbOutdatedSyncException



            Console.WriteLine(("Outdated Knowledge: " & ex.OutdatedPeerSyncKnowledge.ToString() _
                               & " Clean up knowledge: ") + ex.MissingCleanupKnowledge.ToString())

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try


        'Delete a row on peer 3. 
        util.MakeDataChangesOnPeer(util.Peer3ConnString, "Customer")


        'Instantiate a provider, connect to peer 3, and delete all tombstone metadata. 
        sampleSyncProvider = New SampleSyncProvider()
        Dim provider3 As DbSyncProvider = sampleSyncProvider.ConfigureDbSyncProvider(util.Peer3ConnString, -1)


        If provider3.CleanupMetadata() = True Then
            Console.WriteLine([String].Empty)
            Console.WriteLine("Metadata cleanup ran in the SyncSamplesDb_Peer3 database.")
            Console.WriteLine("All metadata was deleted.")
        Else
            Console.WriteLine("Metadata cleanup failed, most likely due to concurrency issues.")
        End If


        'Synchronize. 
        Try
            Dim sampleSyncAgent As SyncOrchestrator
            Dim syncStatistics As SyncOperationStatistics

            sampleSyncAgent = New SampleSyncAgent(sampleSyncProvider.ConfigureDbSyncProvider( _
                                                  util.Peer1ConnString, 7), _
                                                  sampleSyncProvider.ConfigureDbSyncProvider( _
                                                  util.Peer3ConnString, 7))
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "subsequent")
        Catch ex As DbOutdatedSyncException



            Console.WriteLine([String].Empty)
            Console.WriteLine("Synchronization failed due to outdated synchronization knowledge,")
            Console.WriteLine("which is expected in this sample application.")
            Console.WriteLine("Drop and recreate the sample databases.")
            Console.WriteLine([String].Empty)
            Console.WriteLine(("Outdated Knowledge: " & ex.OutdatedPeerSyncKnowledge.ToString() _
                               & " Clean up knowledge: ") + ex.MissingCleanupKnowledge.ToString())
            Console.WriteLine([String].Empty)

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        'Return peer data back to its original state. 
        util.CleanUpPeer(util.Peer1ConnString)
        util.CleanUpPeer(util.Peer3ConnString)

        'Exit. 
        Console.Write(vbLf & "Press Enter to close the window.")
        Console.ReadLine()
    End Sub
End Class

'Create a class that is derived from 
'Microsoft.Synchronization.SyncOrchestrator. 
'<snippetOCSv2_VB_Peer_Cleanup_SampleSyncAgent> 
Public Class SampleSyncAgent
    Inherits SyncOrchestrator
    Public Sub New(ByVal localProvider As RelationalSyncProvider, ByVal _
                   remoteProvider As RelationalSyncProvider)

        Me.LocalProvider = localProvider
        Me.RemoteProvider = remoteProvider
        Me.Direction = SyncDirectionOrder.UploadAndDownload

        'Check to see if any provider is a SqlCe provider and if it needs to 
        'be initialized. 
        CheckIfProviderNeedsSchema(TryCast(localProvider, SqlCeSyncProvider), _
                                   TryCast(remoteProvider, DbSyncProvider))
        CheckIfProviderNeedsSchema(TryCast(remoteProvider, SqlCeSyncProvider), _
                                   TryCast(localProvider, DbSyncProvider))
    End Sub

    'For Compact databases that are not initialized with a snapshot, 
    'get the schema and initial data from a server database. 
    '<snippetOCSv2_VB_Basic_PeerWithCe_CheckIfProviderNeedsSchema> 
    Private Sub CheckIfProviderNeedsSchema(ByVal providerToCheck As SqlCeSyncProvider, _
                                           ByVal providerWithSchema As DbSyncProvider)

        'If one of the providers is a SqlCeSyncProvider and it needs 
        'to be initialized, retrieve the schema from the other provider 
        'if that provider is a DbSyncProvider; otherwise configure a 
        'DbSyncProvider, connect to the server, and retrieve the schema. 
        If providerToCheck IsNot Nothing Then
            Dim ceConfig As New SqlCeSyncScopeProvisioning()
            Dim ceConn As SqlCeConnection = DirectCast(providerToCheck.Connection, SqlCeConnection)
            Dim scopeName As String = providerToCheck.ScopeName
            If Not ceConfig.ScopeExists(scopeName, ceConn) Then
                Dim scopeDesc As DbSyncScopeDescription = providerWithSchema.GetScopeDescription()
                ceConfig.ScopeDescription = scopeDesc
                ceConfig.Apply(ceConn)
            End If

        End If

    End Sub
End Class
'</snippetOCSv2_VB_Basic_PeerWithCe_CheckIfProviderNeedsSchema> 

'</snippetOCSv2_VB_Peer_Cleanup_SampleSyncAgent> 

Public Class SampleSyncProvider

    '<snippetOCSv2_VB_Peer_Cleanup_ConfigureCeSyncProvider> 
    Public Function ConfigureCeSyncProvider(ByVal sqlCeConnString As String) As SqlCeSyncProvider

        Dim sampleCeProvider As New SqlCeSyncProvider()

        'Set the scope name 
        sampleCeProvider.ScopeName = "Sales"

        'Set the connection 
        sampleCeProvider.Connection = New SqlCeConnection(sqlCeConnString)

        Return sampleCeProvider
    End Function
    '</snippetOCSv2_VB_Peer_Cleanup_ConfigureCeSyncProvider> 


    '<snippetOCSv2_VB_Peer_Cleanup_ConfigureDbSyncProvider> 
    Public Function ConfigureDbSyncProvider(ByVal peerConnString As String, ByVal metadataAgingInDays As Integer) As DbSyncProvider

        Dim sampleDbProvider As New DbSyncProvider()

        '<snippetOCSv2_VB_Basic_PeerWithCe_Scope> 
        Dim peerConnection As New SqlConnection(peerConnString)
        sampleDbProvider.Connection = peerConnection
        sampleDbProvider.ScopeName = "Sales"
        '</snippetOCSv2_VB_Basic_PeerWithCe_Scope> 

        'Create a DbSyncAdapter object for the Customer table and associate it 
        'with the DbSyncProvider. Following the DataAdapter style in ADO.NET, 
        'DbSyncAdapter is the equivalent for synchronization. The commands that 
        'are specified for the DbSyncAdapter object call stored procedures 
        'that are created in each peer database. 
        '<snippetOCSv2_VB_Basic_PeerWithCe_AdapterCustomer> 
        Dim adapterCustomer As New DbSyncAdapter("Customer")


        'Specify the primary key, which Sync Services uses 
        'to identify each row during synchronization. 
        adapterCustomer.RowIdColumns.Add("CustomerId")
        '</snippetOCSv2_VB_Basic_PeerWithCe_AdapterCustomer> 


        'Specify the command to select incremental changes. 
        'In this command and other commands, session variables are 
        'used to pass information at runtime. DbSyncSession.SyncMetadataOnly 
        'and SyncMinTimestamp are two of the string constants that 
        'the DbSyncSession class exposes. You could also include 
        '@sync_metadata_only and @sync_min_timestamp directly in your 
        'queries: 
        '* sync_metadata_only is used by Sync Services as an optimization 
        ' in some queries. 
        '* The value of the sync_min_timestamp session variable is compared to 
        ' values in the sync_row_timestamp column in the tracking table to 
        ' determine which rows to select. 
        '<snippetOCSv2_VB_Basic_PeerWithCe_SelectIncrementalChangesCommand> 
        Dim chgsCustomerCmd As New SqlCommand()
        With chgsCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_SelectChanges"
            .Parameters.Add("@" & DbSyncSession.SyncMetadataOnly, SqlDbType.Int)
            .Parameters.Add("@" & DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" & DbSyncSession.SyncScopeLocalId, SqlDbType.Int)
            .Parameters.Add("@" & DbSyncSession.SyncInitialize, SqlDbType.Int)
        End With

        adapterCustomer.SelectIncrementalChangesCommand = chgsCustomerCmd
        '</snippetOCSv2_VB_Basic_PeerWithCe_SelectIncrementalChangesCommand> 

        'Specify the command to insert rows. 
        'The sync_row_count session variable is used in this command 
        'and other commands to return a count of the rows affected by an operation. 
        'A count of 0 indicates that an operation failed. 
        '<snippetOCSv2_VB_Basic_PeerWithCe_InsertCommand> 
        Dim insCustomerCmd As New SqlCommand()
        With insCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_ApplyInsert"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@CustomerName", SqlDbType.NVarChar)
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@CustomerType", SqlDbType.NVarChar)
            .Parameters.Add("@" & DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        End With

        adapterCustomer.InsertCommand = insCustomerCmd
        '</snippetOCSv2_VB_Basic_PeerWithCe_InsertCommand> 


        'Specify the command to update rows. 
        'The value of the sync_min_timestamp session variable is compared to 
        'values in the sync_row_timestamp column in the tracking table to 
        'determine which rows to update. 
        '<snippetOCSv2_VB_Basic_PeerWithCe_UpdateCommand> 
        Dim updCustomerCmd As New SqlCommand()
        With updCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_ApplyUpdate"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@CustomerName", SqlDbType.NVarChar)
            .Parameters.Add("@SalesPerson", SqlDbType.NVarChar)
            .Parameters.Add("@CustomerType", SqlDbType.NVarChar)
            .Parameters.Add("@" & DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" & DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
            .Parameters.Add("@" & DbSyncSession.SyncForceWrite, SqlDbType.Int)
        End With

        adapterCustomer.UpdateCommand = updCustomerCmd
        '</snippetOCSv2_VB_Basic_PeerWithCe_UpdateCommand> 


        'Specify the command to delete rows. 
        'The value of the sync_min_timestamp session variable is compared to 
        'values in the sync_row_timestamp column in the tracking table to 
        'determine which rows to delete. 
        '<snippetOCSv2_VB_Basic_PeerWithCe_DeleteCommand> 
        Dim delCustomerCmd As New SqlCommand()
        With delCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_ApplyDelete"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" & DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" & DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
            .Parameters.Add("@" & DbSyncSession.SyncForceWrite, SqlDbType.Int)
        End With

        adapterCustomer.DeleteCommand = delCustomerCmd
        '</snippetOCSv2_VB_Basic_PeerWithCe_DeleteCommand> 

        'Specify the command to select any conflicting rows. 
        '<snippetOCSv2_VB_Basic_PeerWithCe_SelectRowCommand> 
        Dim selRowCustomerCmd As New SqlCommand()
        With selRowCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_SelectRow"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" & DbSyncSession.SyncScopeLocalId, SqlDbType.Int)
        End With

        adapterCustomer.SelectRowCommand = selRowCustomerCmd
        '</snippetOCSv2_VB_Basic_PeerWithCe_SelectRowCommand> 


        'Specify the command to insert metadata rows. 
        'The session variables in this command relate to columns in 
        'the tracking table. 
        '<snippetOCSv2_VB_Basic_PeerWithCe_InsertMetadataCommand> 
        Dim insMetadataCustomerCmd As New SqlCommand()
        With insMetadataCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_InsertMetadata"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" & DbSyncSession.SyncScopeLocalId, SqlDbType.Int)
            .Parameters.Add("@" & DbSyncSession.SyncRowTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" & DbSyncSession.SyncCreatePeerKey, SqlDbType.Int)
            .Parameters.Add("@" & DbSyncSession.SyncCreatePeerTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" & DbSyncSession.SyncUpdatePeerKey, SqlDbType.Int)
            .Parameters.Add("@" & DbSyncSession.SyncUpdatePeerTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" & DbSyncSession.SyncRowIsTombstone, SqlDbType.Int)
            .Parameters.Add("@" & DbSyncSession.SyncCheckConcurrency, SqlDbType.Int)
            .Parameters.Add("@" & DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        End With

        adapterCustomer.InsertMetadataCommand = insMetadataCustomerCmd
        '</snippetOCSv2_VB_Basic_PeerWithCe_InsertMetadataCommand> 


        'Specify the command to update metadata rows. 
        '<snippetOCSv2_VB_Basic_PeerWithCe_UpdateMetadataCommand> 
        Dim updMetadataCustomerCmd As New SqlCommand()
        With updMetadataCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_UpdateMetadata"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" & DbSyncSession.SyncScopeLocalId, SqlDbType.Int)
            .Parameters.Add("@" & DbSyncSession.SyncRowTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" & DbSyncSession.SyncCreatePeerKey, SqlDbType.Int)
            .Parameters.Add("@" & DbSyncSession.SyncCreatePeerTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" & DbSyncSession.SyncUpdatePeerKey, SqlDbType.Int)
            .Parameters.Add("@" & DbSyncSession.SyncUpdatePeerTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" & DbSyncSession.SyncRowIsTombstone, SqlDbType.Int)
            .Parameters.Add("@" & DbSyncSession.SyncCheckConcurrency, SqlDbType.Int)
            .Parameters.Add("@" & DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        End With

        adapterCustomer.UpdateMetadataCommand = updMetadataCustomerCmd
        '</snippetOCSv2_VB_Basic_PeerWithCe_UpdateMetadataCommand> 

        'Specify the command to delete metadata rows. 
        '<snippetOCSv2_VB_Basic_PeerWithCe_DeleteMetadataCommand> 
        Dim delMetadataCustomerCmd As New SqlCommand()
        With delMetadataCustomerCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_Customer_DeleteMetadata"
            .Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" & DbSyncSession.SyncCheckConcurrency, SqlDbType.Int)
            .Parameters.Add("@" & DbSyncSession.SyncRowTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" & DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        End With

        adapterCustomer.DeleteMetadataCommand = delMetadataCustomerCmd
        '</snippetOCSv2_VB_Basic_PeerWithCe_DeleteMetadataCommand> 


        'Specify the command to select metadata rows for cleanup. 
        '<snippetOCSv2_VB_Peer_Cleanup_SelectMetadataForCleanupCommand> 
        Dim selMetadataCustomerCmd As New SqlCommand()
        selMetadataCustomerCmd.CommandType = CommandType.StoredProcedure
        selMetadataCustomerCmd.CommandText = "Sync.sp_Customer_SelectMetadata"
        selMetadataCustomerCmd.Parameters.Add("@metadata_aging_in_days", SqlDbType.Int).Value = metadataAgingInDays
        selMetadataCustomerCmd.Parameters.Add("@" & DbSyncSession.SyncScopeLocalId, SqlDbType.Int)

        adapterCustomer.SelectMetadataForCleanupCommand = selMetadataCustomerCmd
        '</snippetOCSv2_VB_Peer_Cleanup_SelectMetadataForCleanupCommand>


        'Add the adapter to the provider. 
        sampleDbProvider.SyncAdapters.Add(adapterCustomer)


        ' Configure commands that relate to the provider itself rather 
        ' than the DbSyncAdapter object for each table: 
        ' * SelectNewTimestampCommand: Returns the new high watermark for 
        ' the current synchronization session. 
        ' * SelectScopeInfoCommand: Returns sync knowledge, cleanup knowledge, 
        ' and a scope version (timestamp). 
        ' * UpdateScopeInfoCommand: Sets new values for sync knowledge and cleanup knowledge. 
        ' * SelectTableMaxTimestampsCommand (optional): Returns the maximum timestamp from each base table 
        ' or tracking table, to determine whether for each table the destination already 
        ' has all of the changes from the source. If a destination table has all the changes, 
        ' SelectIncrementalChangesCommand is not called for that table. 
        ' * SelectOverlappingScopesCommand: returns the scope name and table name for all tables 
        '   in the specified scope that are also included in other scopes.
        ' * UpdateScopeCleanupTimestampCommand: updates the scope_cleanup_timestamp column for a 
        '   particular scope in the scope_info table, to mark the point up to which cleanup 
        '   has been performed for the scope.


        'Select a new timestamp. 
        'During each synchronization, the new value and 
        'the last value from the previous synchronization 
        'are used: the set of changes between these upper and 
        'lower bounds is synchronized. 
        '<snippetOCSv2_VB_Basic_PeerWithCe_NewAnchorCommand> 
        Dim selectNewTimestampCommand As New SqlCommand()
        Dim newTimestampVariable As String = "@" & DbSyncSession.SyncNewTimestamp
        With selectNewTimestampCommand
            .CommandText = "SELECT " & newTimestampVariable & " = min_active_rowversion() - 1"
            .Parameters.Add(newTimestampVariable, SqlDbType.Timestamp)
            .Parameters(newTimestampVariable).Direction = ParameterDirection.Output
        End With

        sampleDbProvider.SelectNewTimestampCommand = selectNewTimestampCommand
        '</snippetOCSv2_VB_Basic_PeerWithCe_NewAnchorCommand> 

        'Specify the command to select local replica metadata. 
        '<snippetOCSv2_VB_Basic_PeerWithCe_SelectScopeInfoCommand> 
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
            .Parameters.Add("@" & DbSyncSession.SyncScopeName, SqlDbType.NVarChar, 100)
        End With

        sampleDbProvider.SelectScopeInfoCommand = selReplicaInfoCmd
        '</snippetOCSv2_VB_Basic_PeerWithCe_SelectScopeInfoCommand> 


        'Specify the command to update local replica metadata. 
        '<snippetOCSv2_VB_Basic_PeerWithCe_UpdateScopeInfoCommand> 
        Dim updReplicaInfoCmd As New SqlCommand()
        With updReplicaInfoCmd
            .CommandType = CommandType.Text
            .CommandText = "UPDATE  Sync.ScopeInfo SET " _
                         & "scope_sync_knowledge = @" + DbSyncSession.SyncScopeKnowledge + ", " _
                         & "scope_id = @" + DbSyncSession.SyncScopeId + ", " _
                         & "scope_tombstone_cleanup_knowledge = @" + DbSyncSession.SyncScopeCleanupKnowledge + " " _
                         & "WHERE scope_name = @" + DbSyncSession.SyncScopeName + " AND " _
                         & " ( @" + DbSyncSession.SyncCheckConcurrency + " = 0 OR scope_timestamp = @" + DbSyncSession.SyncScopeTimestamp + "); " _
                         & "SET @" + DbSyncSession.SyncRowCount + " = @@rowcount"
            .Parameters.Add("@" & DbSyncSession.SyncScopeKnowledge, SqlDbType.VarBinary, 10000)
            .Parameters.Add("@" & DbSyncSession.SyncScopeCleanupKnowledge, SqlDbType.VarBinary, 10000)
            .Parameters.Add("@" & DbSyncSession.SyncScopeName, SqlDbType.NVarChar, 100)
            .Parameters.Add("@" & DbSyncSession.SyncCheckConcurrency, SqlDbType.Int)
            .Parameters.Add("@" & DbSyncSession.SyncScopeId, SqlDbType.UniqueIdentifier)
            .Parameters.Add("@" & DbSyncSession.SyncScopeTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" & DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        End With

        sampleDbProvider.UpdateScopeInfoCommand = updReplicaInfoCmd
        '</snippetOCSv2_VB_Basic_PeerWithCe_UpdateScopeInfoCommand> 


        'Return the maximum timestamp from the Customer_Tracking table. 
        'If more tables are synchronized, the query should UNION 
        'all of the results. The table name is not schema-qualified 
        'in this case because the name was not schema qualified in the 
        'DbSyncAdapter constructor. 
        '<snippetOCSv2_VB_Basic_PeerWithCe_SelectTableMaxTimestampsCommand> 
        Dim selTableMaxTsCmd As New SqlCommand()
        selTableMaxTsCmd.CommandType = CommandType.Text
        selTableMaxTsCmd.CommandText = "SELECT 'Customer' AS table_name, " _
                                     & "MAX(local_update_peer_timestamp) AS max_timestamp " _
                                     & "FROM Sync.Customer_Tracking"
        sampleDbProvider.SelectTableMaxTimestampsCommand = selTableMaxTsCmd
        '</snippetOCSv2_VB_Basic_PeerWithCe_SelectTableMaxTimestampsCommand> 


        'Specify the command to select table and scope names for
        'any tables that are in more than one scope.
        '<snippetOCSv2_VB_Peer_Cleanup_SelectOverlappingScopesCommand>
        Dim overlappingScopesCmd As New SqlCommand()
        With overlappingScopesCmd
            .CommandType = CommandType.StoredProcedure
            .CommandText = "Sync.sp_SelectSharedScopes"
            .Parameters.Add("@" + DbSyncSession.SyncScopeName, SqlDbType.NVarChar, 100)
        End With

        sampleDbProvider.SelectOverlappingScopesCommand = overlappingScopesCmd
        '</snippetOCSv2_VB_Peer_Cleanup_SelectOverlappingScopesCommand>

        'Specify the command that updates the scope information table
        'to indicate to which point metadata has been cleaned up for a scope.
        '<snippetOCSv2_VB_Peer_Cleanup_UpdateScopeCleanupTimestampCommand>
        Dim updScopeCleanupInfoCmd As New SqlCommand()
        With updScopeCleanupInfoCmd
            .CommandType = CommandType.Text
            .CommandText = "UPDATE  scope_info set " _
                         & " scope_cleanup_timestamp = @" + DbSyncSession.SyncScopeCleanupTimestamp _
                         & " WHERE scope_name = @" + DbSyncSession.SyncScopeName _
                         & " AND(scope_cleanup_timestamp is null or scope_cleanup_timestamp <  @" + DbSyncSession.SyncScopeCleanupTimestamp + ");" _
                         & " SET @" + DbSyncSession.SyncRowCount + " = @@rowcount"
            .Parameters.Add("@" + DbSyncSession.SyncScopeCleanupTimestamp, SqlDbType.BigInt)
            .Parameters.Add("@" + DbSyncSession.SyncScopeName, SqlDbType.NVarChar, 100)
            .Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output
        End With

        sampleDbProvider.UpdateScopeCleanupTimestampCommand = updScopeCleanupInfoCmd
        '</snippetOCSv2_VB_Peer_Cleanup_UpdateScopeCleanupTimestampCommand> 

        Return sampleDbProvider

    End Function
End Class
'</snippetOCSv2_VB_Peer_Cleanup_ConfigureDbSyncProvider> 

'Handle the statistics that are returned by the SyncAgent. 
Public Class SampleStats
    Public Sub DisplayStats(ByVal syncStatistics As SyncOperationStatistics, ByVal syncType As String)
        Console.WriteLine([String].Empty)
        If syncType = "initial" Then
            Console.WriteLine("****** Initial Synchronization ******")
        ElseIf syncType = "subsequent" Then
            Console.WriteLine("***** Subsequent Synchronization ****")
        End If

        Console.WriteLine("Start Time: " & syncStatistics.SyncStartTime)
        Console.WriteLine("Total Changes Uploaded: " & syncStatistics.UploadChangesTotal)
        Console.WriteLine("Total Changes Downloaded: " & syncStatistics.DownloadChangesTotal)
        Console.WriteLine("Complete Time: " & syncStatistics.SyncEndTime)
        Console.WriteLine([String].Empty)
    End Sub
End Class
'</snippetOCSv2_VB_Peer_Cleanup>