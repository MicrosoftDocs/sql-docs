'<snippetOCSv2_VB_Basic_PeerWithCe> 
Imports System
Imports System.Collections.Generic
Imports System.IO
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
        util.RecreateCePeerDatabase(util.Ce2ConnString)

        '<snippetOCSv2_VB_Basic_PeerWithCe_Synchronize> 
        'The SampleStats class handles information from the SyncStatistics 
        'object that the Synchronize method returns. 
        Dim sampleStats As New SampleStats()

        Try
            'Initial synchronization. Instantiate the SyncOrchestrator 
            'and call Synchronize. 
            Dim sampleSyncProvider As New SampleSyncProvider()
            Dim sampleSyncAgent As SyncOrchestrator
            Dim syncStatistics As SyncOperationStatistics

            'First session: Synchronize two databases by using two instances 
            'of DbSyncProvider. 
            sampleSyncAgent = New SampleSyncAgent(sampleSyncProvider.ConfigureDbSyncProvider(util.Peer1ConnString), _
                                                  sampleSyncProvider.ConfigureDbSyncProvider(util.Peer2ConnString))
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "initial")

            '<snippetOCSv2_VB_Basic_PeerWithCe_SnapshotInit> 
            'Second session: Synchronize two databases by using one instance of 
            'DbSyncProvider and one instance of SqlCeSyncProvider. After the Compact 
            'database is initialized, it is copied by using GenerateSnapshot and then 
            'used for the third session. 
            Dim sqlCeSyncProvider1 As SqlCeSyncProvider = sampleSyncProvider.ConfigureCeSyncProvider(util.Ce1ConnString)

            sampleSyncAgent = New SampleSyncAgent(sampleSyncProvider.ConfigureDbSyncProvider(util.Peer1ConnString), _
                                                  sqlCeSyncProvider1)
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "initial")

            'Copy the Compact database and save it as SyncSampleCe2.sdf. 
            Dim snapshotInit As New SqlCeSyncStoreSnapshotInitialization()
            snapshotInit.GenerateSnapshot(New SqlCeConnection(util.Ce1ConnString), "SyncSampleCe2.sdf")

            'Make a change that is synchronized during the third session. 
            util.MakeDataChangesOnPeer(util.Peer1ConnString, "Customer")

            'Third session: Synchronize the new Compact database. The five rows 
            'from SyncSampleCe1.sdf are already in SyncSampleCe2.sdf. The new 
            'change is now downloaded to bring SyncSampleCe2.sdf up to date. 
            'SyncSampleCe2.sdf will get this row during the next round of 
            'synchronization sessions. 
            sampleSyncAgent = New SampleSyncAgent(sampleSyncProvider.ConfigureDbSyncProvider(util.Peer1ConnString), _
                                                  sampleSyncProvider.ConfigureCeSyncProvider(util.Ce2ConnString))
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "initial")
            '</snippetOCSv2_VB_Basic_PeerWithCe_SnapshotInit> 
        Catch ex As DbOutdatedSyncException


            Console.WriteLine(("Outdated Knowledge: " & ex.OutdatedPeerSyncKnowledge.ToString() & " Clean up knowledge: ") _
                              & ex.MissingCleanupKnowledge.ToString())
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        '</snippetOCSv2_VB_Basic_PeerWithCe_Synchronize> 

        'Make a change in one of the databases. 
        util.MakeDataChangesOnPeer(util.Peer2ConnString, "Customer")

        Try
            'Subsequent synchronization. Changes are now synchronized between all 
            'nodes. 
            Dim sampleSyncProvider As New SampleSyncProvider()
            Dim sampleSyncAgent As SyncOrchestrator
            Dim syncStatistics As SyncOperationStatistics

            sampleSyncAgent = New SampleSyncAgent(sampleSyncProvider.ConfigureDbSyncProvider(util.Peer1ConnString), _
                                                  sampleSyncProvider.ConfigureDbSyncProvider(util.Peer2ConnString))
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "subsequent")

            sampleSyncAgent = New SampleSyncAgent(sampleSyncProvider.ConfigureDbSyncProvider(util.Peer1ConnString), _
                                                  sampleSyncProvider.ConfigureCeSyncProvider(util.Ce1ConnString))
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "subsequent")

            sampleSyncAgent = New SampleSyncAgent(sampleSyncProvider.ConfigureDbSyncProvider(util.Peer1ConnString), _
                                                  sampleSyncProvider.ConfigureCeSyncProvider(util.Ce2ConnString))
            syncStatistics = sampleSyncAgent.Synchronize()
            sampleStats.DisplayStats(syncStatistics, "subsequent")

        Catch ex As DbOutdatedSyncException


            Console.WriteLine(("Outdated Knowledge: " & ex.OutdatedPeerSyncKnowledge.ToString() & " Clean up knowledge: ") _
                              & ex.MissingCleanupKnowledge.ToString())
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        'Return data back to its original state. 
        util.CleanUpPeer(util.Peer1ConnString)
        util.CleanUpPeer(util.Peer2ConnString)

        'Exit. 
        Console.Write(vbLf & "Press Enter to close the window.")
        Console.ReadLine()
    End Sub

    'Create a class that is derived from 
    'Microsoft.Synchronization.SyncOrchestrator. 
    '<snippetOCSv2_VB_Basic_PeerWithCe_SampleSyncAgent> 
    Public Class SampleSyncAgent
        Inherits SyncOrchestrator
        Public Sub New(ByVal localProvider As RelationalSyncProvider, ByVal remoteProvider As RelationalSyncProvider)

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
    '</snippetOCSv2_VB_Basic_PeerWithCe_SampleSyncAgent> 


    Public Class SampleSyncProvider

        '<snippetOCSv2_VB_Basic_PeerWithCe_ConfigureCeSyncProvider> 
        Public Function ConfigureCeSyncProvider(ByVal sqlCeConnString As String) As SqlCeSyncProvider

            Dim sampleCeProvider As New SqlCeSyncProvider()

            'Set the scope name 
            sampleCeProvider.ScopeName = "Sales"

            'Set the connection 
            sampleCeProvider.Connection = New SqlCeConnection(sqlCeConnString)

            'Register event handlers 

            'Register the BeginSnapshotInitialization event handler. 
            'It is called when snapshot initialization is about to begin 
            'for a particular scope in a Compact database. 
            AddHandler sampleCeProvider.BeginSnapshotInitialization, AddressOf sampleCeProvider_BeginSnapshotInitialization

            'Register the EndSnapshotInitialization event handler. 
            'It is called when snapshot initialization has completed 
            'for a particular scope in a Compact database. 
            AddHandler sampleCeProvider.EndSnapshotInitialization, AddressOf sampleCeProvider_EndSnapshotInitialization

            Return sampleCeProvider
        End Function
        '</snippetOCSv2_VB_Basic_PeerWithCe_ConfigureCeSyncProvider> 

        '<snippetOCSv2_VB_Basic_PeerWithCe_CeSyncProviderEvents> 
        Public Sub sampleCeProvider_CreatingSchema(ByVal sender As Object, ByVal e As CreatingSchemaEventArgs)
            Console.WriteLine("Full initialization process started...")
            Console.WriteLine(String.Format("CreatingSchema event fired for database {0}", e.Connection.Database))
        End Sub

        Public Sub sampleCeProvider_BeginSnapshotInitialization(ByVal sender As Object, ByVal e As DbBeginSnapshotInitializationEventArgs)
            Console.WriteLine("")
            Console.WriteLine("Snapshot initialization process started...")
            Console.WriteLine(String.Format("BeginSnapshotInitialization event fired for scope {0}", e.ScopeName))
        End Sub

        Public Sub sampleCeProvider_EndSnapshotInitialization(ByVal sender As Object, ByVal e As DbEndSnapshotInitializationEventArgs)
            Console.WriteLine("EndSnapshotInitialization event fired")

            Dim tableStats As Dictionary(Of String, DbSnapshotInitializationTableStatistics) = e.TableInitializationStatistics

            For Each tableName As String In tableStats.Keys

                Dim ts As DbSnapshotInitializationTableStatistics = tableStats(tableName)

                Console.WriteLine(vbTab & "Table Name: " & tableName)

                Console.WriteLine(vbTab & "Total Rows: " & ts.TotalRows)

                Console.WriteLine(vbTab & "Rows Intialized: " & ts.RowsInitialized)

                Console.WriteLine(vbTab & "Start Time: " & ts.StartTime)

                Console.WriteLine(vbTab & "End Time: " & ts.EndTime)

            Next

            Console.WriteLine("Snapshot initialization process completed")
            Console.WriteLine("")
        End Sub
        '</snippetOCSv2_VB_Basic_PeerWithCe_CeSyncProviderEvents> 

        '<snippetOCSv2_VB_Basic_PeerWithCe_ConfigureDbSyncProvider> 
        Public Function ConfigureDbSyncProvider(ByVal peerConnString As String) As DbSyncProvider

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
            ' There are additional commands related to metadata cleanup that are not 
            ' included in this application. 


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

            Return sampleDbProvider
        End Function
    End Class
    '</snippetOCSv2_VB_Basic_PeerWithCe_ConfigureDbSyncProvider> 

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
End Class
'</snippetOCSv2_VB_Basic_PeerWithCe> 


' 
' * 
'//Specify the command to select metadata rows for cleanup. 
'//<snippetOCSv2_VB_Basic_PeerWithCe_SelectMetadataForCleanupCommand> 
'SqlCommand selMetadataCustomerCmd = new SqlCommand(); 
'selMetadataCustomerCmd.CommandType = CommandType.StoredProcedure; 
'selMetadataCustomerCmd.CommandText = "Sync.sp_Customer_SelectMetadata"; 
'selMetadataCustomerCmd.Parameters.Add("@metadata_aging_in_hours", SqlDbType.Int).Value = MetadataAgingInHours; 
'selMetadataCustomerCmd.Parameters.Add("@sync_scope_local_id", SqlDbType.Int); 
' 
'adapterCustomer.SelectMetadataForCleanupCommand = selMetadataCustomerCmd; 
'//</snippetOCSv2_VB_Basic_PeerWithCe_SelectMetadataForCleanupCommand> 
' 
'// SelectOverlappingScopesCommand 
'//<snippetOCSv2_VB_Basic_PeerWithCe_SelectOverlappingScopesCommand> 
'SqlCommand selOverlappingScopesCmd = new SqlCommand(); 
'selOverlappingScopesCmd.CommandType = CommandType.StoredProcedure; 
'selOverlappingScopesCmd.CommandText = "sp_SelectSharedScopes"; 
'selOverlappingScopesCmd.Parameters.Add("@" + DbSyncSession.SyncScopeName, SqlDbType.NVarChar, 100); 
'sampleDbProvider.SelectOverlappingScopesCommand = selOverlappingScopesCmd; 
'//</snippetOCSv2_VB_Basic_PeerWithCe_SelectOverlappingScopesCommand> 
' 
' 
'// UpdateScopeCleanupTimestampCommand: 
'//<snippetOCSv2_VB_Basic_PeerWithCe_UpdateScopeCleanupTimestampCommand> 
'SqlCommand selUpdateScopeCleanupTsCmd = new SqlCommand(); 
'//</snippetOCSv2_VB_Basic_PeerWithCe_UpdateScopeCleanupTimestampCommand> 