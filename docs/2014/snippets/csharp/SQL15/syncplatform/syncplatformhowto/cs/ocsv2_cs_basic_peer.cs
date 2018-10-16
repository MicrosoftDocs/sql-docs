//<snippetOCSv2_CS_Basic_Peer>
using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;

namespace Microsoft.Samples.Synchronization
{
    class Program
    {
        static void Main(string[] args)
        {

            //The Utility class handles all functionality that is not
            //directly related to synchronization, such as holding peerConnection 
            //string information and making changes to the server database.
            Utility util = new Utility();

            //<snippetOCSv2_CS_Basic_Peer_Synchronize>
            //The SampleStats class handles information from the SyncStatistics
            //object that the Synchronize method returns.
            SampleStats sampleStats = new SampleStats();

            try
            {
                //Initial synchronization. Instantiate the SyncOrchestrator
                //and call Synchronize. Note that data is not synchronized during the
                //session between peer 1 and peer 3, because all rows have already
                //been delivered to peer 3 during its synchronization session with peer 2.
                SyncOrchestrator sampleSyncAgent;
                SyncOperationStatistics syncStatistics;

                sampleSyncAgent = new SampleSyncAgent(util.Peer1ConnString, util.Peer2ConnString);
                syncStatistics = sampleSyncAgent.Synchronize();
                sampleStats.DisplayStats(syncStatistics, "initial");

                sampleSyncAgent = new SampleSyncAgent(util.Peer2ConnString, util.Peer3ConnString);
                syncStatistics = sampleSyncAgent.Synchronize();
                sampleStats.DisplayStats(syncStatistics, "initial");

                sampleSyncAgent = new SampleSyncAgent(util.Peer1ConnString, util.Peer3ConnString);
                syncStatistics = sampleSyncAgent.Synchronize();
                sampleStats.DisplayStats(syncStatistics, "initial");
            }


            catch (DbOutdatedSyncException ex)
            {
                Console.WriteLine("Outdated Knowledge: " + ex.OutdatedPeerSyncKnowledge.ToString() +
                                  " Clean up knowledge: " + ex.MissingCleanupKnowledge.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //</snippetOCSv2_CS_Basic_Peer_Synchronize>

            //Make changes in each peer database.
            util.MakeDataChangesOnPeer(util.Peer1ConnString, "Customer");
            util.MakeDataChangesOnPeer(util.Peer2ConnString, "Customer");
            util.MakeDataChangesOnPeer(util.Peer3ConnString, "Customer");

            try
            {
                //Subsequent synchronization. Changes are now synchronized between all
                //peers.
                SyncOrchestrator sampleSyncAgent;
                SyncOperationStatistics syncStatistics;

                sampleSyncAgent = new SampleSyncAgent(util.Peer1ConnString, util.Peer2ConnString);
                syncStatistics = sampleSyncAgent.Synchronize();
                sampleStats.DisplayStats(syncStatistics, "subsequent");

                sampleSyncAgent = new SampleSyncAgent(util.Peer2ConnString, util.Peer3ConnString);
                syncStatistics = sampleSyncAgent.Synchronize();
                sampleStats.DisplayStats(syncStatistics, "subsequent");

                sampleSyncAgent = new SampleSyncAgent(util.Peer1ConnString, util.Peer3ConnString);
                syncStatistics = sampleSyncAgent.Synchronize();
                sampleStats.DisplayStats(syncStatistics, "subsequent");

                //Sessions in which no new changes have been made.
                //In this case, the call to SelectTableMaxTimestampsCommand indicates 
                //that no data changes are available to synchronize, so
                //SelectIncrementalChangesCommand is not called.
                sampleSyncAgent = new SampleSyncAgent(util.Peer1ConnString, util.Peer2ConnString);
                syncStatistics = sampleSyncAgent.Synchronize();
                sampleStats.DisplayStats(syncStatistics, "subsequent");

                sampleSyncAgent = new SampleSyncAgent(util.Peer1ConnString, util.Peer2ConnString);
                syncStatistics = sampleSyncAgent.Synchronize();
                sampleStats.DisplayStats(syncStatistics, "subsequent");
            }


            catch (DbOutdatedSyncException ex)
            {
                Console.WriteLine("Outdated Knowledge: " + ex.OutdatedPeerSyncKnowledge.ToString() +
                                  " Clean up knowledge: " + ex.MissingCleanupKnowledge.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Return peer data back to its original state.
            util.CleanUpPeer(util.Peer1ConnString);
            util.CleanUpPeer(util.Peer2ConnString);
            util.CleanUpPeer(util.Peer3ConnString);

            //Exit.
            Console.Write("\nPress Enter to close the window.");
            Console.ReadLine();
        }

        //Create a class that is derived from 
        //Microsoft.Synchronization.SyncOrchestrator.
        //<snippetOCSv2_CS_Basic_Peer_SampleSyncAgent>
        public class SampleSyncAgent : SyncOrchestrator
        {
            public SampleSyncAgent(string localProviderConnString, string remoteProviderConnString)
            {

                //Instantiate the sample provider that allows us to create a provider
                //for both of the peers that are being synchronized.
                SampleSyncProvider sampleSyncProvider = new SampleSyncProvider();

                //Instantiate a DbSyncProvider for the local peer and the remote peer.
                //For example, if this code is running at peer1 and is
                //synchronizing with peer2, peer1 would be the local provider
                //and peer2 the remote provider.
                //<snippetOCSv2_CS_Basic_Peer_DbSyncProvider>
                DbSyncProvider localProvider = new DbSyncProvider();
                DbSyncProvider remoteProvider = new DbSyncProvider();

                //Create a provider by using the SetupSyncProvider on the sample class.             
                sampleSyncProvider.SetupSyncProvider(localProviderConnString, localProvider);
                localProvider.SyncProviderPosition = SyncProviderPosition.Local;
                
                sampleSyncProvider.SetupSyncProvider(remoteProviderConnString, remoteProvider);
                remoteProvider.SyncProviderPosition = SyncProviderPosition.Remote;
                //</snippetOCSv2_CS_Basic_Peer_DbSyncProvider>

                //Specify the local and remote providers that should be synchronized,
                //and the direction and order of changes. In this case, changes are first
                //uploaded from remote to local and then downloaded in the other direction.
                this.LocalProvider = localProvider;
                this.RemoteProvider = remoteProvider;
                this.Direction = SyncDirectionOrder.UploadAndDownload;
            }
        }
            //</snippetOCSv2_CS_Basic_Peer_SampleSyncAgent>


        public class SampleSyncProvider
        {
            //<snippetOCSv2_CS_Basic_Peer_SetupSyncProvider>
            public DbSyncProvider SetupSyncProvider(string peerConnString, DbSyncProvider sampleProvider)
            {

                //<snippetOCSv2_CS_Basic_Peer_Scope>
                SqlConnection peerConnection = new SqlConnection(peerConnString);
                sampleProvider.Connection = peerConnection;
                sampleProvider.ScopeName = "Sales";
                //</snippetOCSv2_CS_Basic_Peer_Scope>

                //Create a DbSyncAdapter object for the Customer table and associate it 
                //with the DbSyncProvider. Following the DataAdapter style in ADO.NET, 
                //DbSyncAdapter is the equivalent for synchronization. The commands that 
                //are specified for the DbSyncAdapter object call stored procedures
                //that are created in each peer database.
                //<snippetOCSv2_CS_Basic_Peer_AdapterCustomer>
                DbSyncAdapter adapterCustomer = new DbSyncAdapter("Customer");


                //Specify the primary key, which Sync Services uses
                //to identify each row during synchronization.
                adapterCustomer.RowIdColumns.Add("CustomerId");
                //</snippetOCSv2_CS_Basic_Peer_AdapterCustomer>


                //Specify the command to select incremental changes.
                //In this command and other commands, session variables are
                //used to pass information at runtime. DbSyncSession.SyncMetadataOnly 
                //and SyncMinTimestamp are two of the string constants that
                //the DbSyncSession class exposes. You could also include 
                //@sync_metadata_only and @sync_min_timestamp directly in your 
                //queries:
                //*  sync_metadata_only is used by Sync Services as an optimization
                //   in some queries.
                //* The value of the sync_min_timestamp session variable is compared to
                //   values in the sync_row_timestamp column in the tracking table to 
                //   determine which rows to select.
                //<snippetOCSv2_CS_Basic_Peer_SelectIncrementalChangesCommand>
                SqlCommand chgsCustomerCmd = new SqlCommand();
                chgsCustomerCmd.CommandType = CommandType.StoredProcedure;
                chgsCustomerCmd.CommandText = "Sync.sp_Customer_SelectChanges";
                chgsCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncMetadataOnly, SqlDbType.Int);
                chgsCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt);
                chgsCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncScopeLocalId, SqlDbType.Int);
                chgsCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncInitialize, SqlDbType.Int);

                adapterCustomer.SelectIncrementalChangesCommand = chgsCustomerCmd;
                //</snippetOCSv2_CS_Basic_Peer_SelectIncrementalChangesCommand>

                //Specify the command to insert rows.
                //The sync_row_count session variable is used in this command 
                //and other commands to return a count of the rows affected by an operation. 
                //A count of 0 indicates that an operation failed.
                //<snippetOCSv2_CS_Basic_Peer_InsertCommand>
                SqlCommand insCustomerCmd = new SqlCommand();
                insCustomerCmd.CommandType = CommandType.StoredProcedure;
                insCustomerCmd.CommandText = "Sync.sp_Customer_ApplyInsert";
                insCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
                insCustomerCmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar);
                insCustomerCmd.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
                insCustomerCmd.Parameters.Add("@CustomerType", SqlDbType.NVarChar);
                insCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;

                adapterCustomer.InsertCommand = insCustomerCmd;
                //</snippetOCSv2_CS_Basic_Peer_InsertCommand>


                //Specify the command to update rows.
                //The value of the sync_min_timestamp session variable is compared to
                //values in the sync_row_timestamp column in the tracking table to 
                //determine which rows to update.
                //<snippetOCSv2_CS_Basic_Peer_UpdateCommand>
                SqlCommand updCustomerCmd = new SqlCommand();
                updCustomerCmd.CommandType = CommandType.StoredProcedure;
                updCustomerCmd.CommandText = "Sync.sp_Customer_ApplyUpdate";
                updCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
                updCustomerCmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar);
                updCustomerCmd.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
                updCustomerCmd.Parameters.Add("@CustomerType", SqlDbType.NVarChar);
                updCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt);
                updCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;
                updCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncForceWrite, SqlDbType.Int);
                
                adapterCustomer.UpdateCommand = updCustomerCmd;
                //</snippetOCSv2_CS_Basic_Peer_UpdateCommand>


                //Specify the command to delete rows.
                //The value of the sync_min_timestamp session variable is compared to
                //values in the sync_row_timestamp column in the tracking table to 
                //determine which rows to delete.
                //<snippetOCSv2_CS_Basic_Peer_DeleteCommand>
                SqlCommand delCustomerCmd = new SqlCommand();
                delCustomerCmd.CommandType = CommandType.StoredProcedure;
                delCustomerCmd.CommandText = "Sync.sp_Customer_ApplyDelete";
                delCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
                delCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt);
                delCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;
                delCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncForceWrite, SqlDbType.Int);
                
                adapterCustomer.DeleteCommand = delCustomerCmd;
                //</snippetOCSv2_CS_Basic_Peer_DeleteCommand>

                //Specify the command to select any conflicting rows.
                //<snippetOCSv2_CS_Basic_Peer_SelectRowCommand>
                SqlCommand selRowCustomerCmd = new SqlCommand();
                selRowCustomerCmd.CommandType = CommandType.StoredProcedure;
                selRowCustomerCmd.CommandText = "Sync.sp_Customer_SelectRow";
                selRowCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
                selRowCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncScopeLocalId, SqlDbType.Int);

                adapterCustomer.SelectRowCommand = selRowCustomerCmd;
                //</snippetOCSv2_CS_Basic_Peer_SelectRowCommand>


                //Specify the command to insert metadata rows.
                //The session variables in this command relate to columns in
                //the tracking table.
                //<snippetOCSv2_CS_Basic_Peer_InsertMetadataCommand>
                SqlCommand insMetadataCustomerCmd = new SqlCommand();
                insMetadataCustomerCmd.CommandType = CommandType.StoredProcedure;
                insMetadataCustomerCmd.CommandText = "Sync.sp_Customer_InsertMetadata";
                insMetadataCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);          
                insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncScopeLocalId, SqlDbType.Int);
                insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowTimestamp, SqlDbType.BigInt);
                insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCreatePeerKey, SqlDbType.Int);
                insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCreatePeerTimestamp, SqlDbType.BigInt);
                insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncUpdatePeerKey, SqlDbType.Int);
                insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncUpdatePeerTimestamp, SqlDbType.BigInt);
                insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowIsTombstone, SqlDbType.Int);
                insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCheckConcurrency, SqlDbType.Int);
                insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;

                adapterCustomer.InsertMetadataCommand = insMetadataCustomerCmd;
                //</snippetOCSv2_CS_Basic_Peer_InsertMetadataCommand>


                //Specify the command to update metadata rows.
                //<snippetOCSv2_CS_Basic_Peer_UpdateMetadataCommand>
                SqlCommand updMetadataCustomerCmd = new SqlCommand();
                updMetadataCustomerCmd.CommandType = CommandType.StoredProcedure;
                updMetadataCustomerCmd.CommandText = "Sync.sp_Customer_UpdateMetadata";
                updMetadataCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
                updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncScopeLocalId, SqlDbType.Int);
                updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowTimestamp, SqlDbType.BigInt);
                updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCreatePeerKey, SqlDbType.Int);
                updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCreatePeerTimestamp, SqlDbType.BigInt);
                updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncUpdatePeerKey, SqlDbType.Int);
                updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncUpdatePeerTimestamp, SqlDbType.BigInt);
                updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowIsTombstone, SqlDbType.Int);
                updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCheckConcurrency, SqlDbType.Int);
                updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;

                adapterCustomer.UpdateMetadataCommand = updMetadataCustomerCmd;
                //</snippetOCSv2_CS_Basic_Peer_UpdateMetadataCommand>

                //Specify the command to delete metadata rows.
                //<snippetOCSv2_CS_Basic_Peer_DeleteMetadataCommand>
                SqlCommand delMetadataCustomerCmd = new SqlCommand();
                delMetadataCustomerCmd.CommandType = CommandType.StoredProcedure;
                delMetadataCustomerCmd.CommandText = "Sync.sp_Customer_DeleteMetadata";
                delMetadataCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
                delMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCheckConcurrency, SqlDbType.Int);
                delMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowTimestamp, SqlDbType.BigInt);
                delMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;

                adapterCustomer.DeleteMetadataCommand = delMetadataCustomerCmd;
                //</snippetOCSv2_CS_Basic_Peer_DeleteMetadataCommand>


                //Add the adapter to the provider.
                sampleProvider.SyncAdapters.Add(adapterCustomer);


                // Configure commands that relate to the provider itself rather 
                // than the DbSyncAdapter object for each table:
                // * SelectNewTimestampCommand: Returns the new high watermark for 
                //   the current synchronization session.
                // * SelectScopeInfoCommand: Returns sync knowledge, cleanup knowledge, 
                //   and a scope version (timestamp).
                // * UpdateScopeInfoCommand: Sets new values for sync knowledge and cleanup knowledge.            
                // * SelectTableMaxTimestampsCommand (optional): Returns the maximum timestamp from each base table 
                //   or tracking table, to determine whether for each table the destination already 
                //   has all of the changes from the source. If a destination table has all the changes,
                //   SelectIncrementalChangesCommand is not called for that table.
                // There are additional commands related to metadata cleanup that are not 
                // included in this application.


                //Select a new timestamp.
                //During each synchronization, the new value and
                //the last value from the previous synchronization
                //are used: the set of changes between these upper and
                //lower bounds is synchronized.
                //<snippetOCSv2_CS_Basic_Peer_NewAnchorCommand>
                SqlCommand selectNewTimestampCommand = new SqlCommand();
                string newTimestampVariable = "@" + DbSyncSession.SyncNewTimestamp;
                selectNewTimestampCommand.CommandText = "SELECT " + newTimestampVariable + " = min_active_rowversion() - 1";
                selectNewTimestampCommand.Parameters.Add(newTimestampVariable, SqlDbType.Timestamp);
                selectNewTimestampCommand.Parameters[newTimestampVariable].Direction = ParameterDirection.Output;
                
                sampleProvider.SelectNewTimestampCommand = selectNewTimestampCommand;
                //</snippetOCSv2_CS_Basic_Peer_NewAnchorCommand>

                //Specify the command to select local replica metadata.
                //<snippetOCSv2_CS_Basic_Peer_SelectScopeInfoCommand>
                SqlCommand selReplicaInfoCmd = new SqlCommand();
                selReplicaInfoCmd.CommandType = CommandType.Text;
                selReplicaInfoCmd.CommandText = "SELECT " +
                                                "scope_id, " +
                                                "scope_local_id, " +
                                                "scope_sync_knowledge, " +
                                                "scope_tombstone_cleanup_knowledge, " +
                                                "scope_timestamp " +
                                                "FROM Sync.ScopeInfo " +
                                                "WHERE scope_name = @" + DbSyncSession.SyncScopeName;
                selReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeName, SqlDbType.NVarChar, 100);
                
                sampleProvider.SelectScopeInfoCommand = selReplicaInfoCmd;
                //</snippetOCSv2_CS_Basic_Peer_SelectScopeInfoCommand>


                //Specify the command to update local replica metadata. 
                //<snippetOCSv2_CS_Basic_Peer_UpdateScopeInfoCommand>
                SqlCommand updReplicaInfoCmd = new SqlCommand();
                updReplicaInfoCmd.CommandType = CommandType.Text;
                updReplicaInfoCmd.CommandText = "UPDATE  Sync.ScopeInfo SET " +
                                                "scope_sync_knowledge = @" + DbSyncSession.SyncScopeKnowledge + ", " +
                                                "scope_id = @" + DbSyncSession.SyncScopeId + ", " +
                                                "scope_tombstone_cleanup_knowledge = @" + DbSyncSession.SyncScopeCleanupKnowledge + " " +
                                                "WHERE scope_name = @" + DbSyncSession.SyncScopeName + " AND " +
                                                " ( @" + DbSyncSession.SyncCheckConcurrency + " = 0 OR scope_timestamp = @" + DbSyncSession.SyncScopeTimestamp + "); " +
                                                "set @" + DbSyncSession.SyncRowCount + " = @@rowcount";
                updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeKnowledge, SqlDbType.VarBinary, 10000);
                updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeCleanupKnowledge, SqlDbType.VarBinary, 10000);
                updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeName, SqlDbType.NVarChar, 100);
                updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncCheckConcurrency, SqlDbType.Int);
                updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeId, SqlDbType.UniqueIdentifier);
                updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeTimestamp, SqlDbType.BigInt);
                updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;
                
                sampleProvider.UpdateScopeInfoCommand = updReplicaInfoCmd;
                //</snippetOCSv2_CS_Basic_Peer_UpdateScopeInfoCommand>


                //Return the maximum timestamp from the Customer_Tracking table.
                //If more tables are synchronized, the query should UNION
                //all of the results. The table name is not schema-qualified
                //in this case because the name was not schema qualified in the
                //DbSyncAdapter constructor.
                //<snippetOCSv2_CS_Basic_Peer_SelectTableMaxTimestampsCommand>
                SqlCommand selTableMaxTsCmd = new SqlCommand();
                selTableMaxTsCmd.CommandType = CommandType.Text;
                selTableMaxTsCmd.CommandText = "SELECT 'Customer' AS table_name, " +
                                               "MAX(local_update_peer_timestamp) AS max_timestamp " +
                                               "FROM Sync.Customer_Tracking";
                sampleProvider.SelectTableMaxTimestampsCommand = selTableMaxTsCmd;
                //</snippetOCSv2_CS_Basic_Peer_SelectTableMaxTimestampsCommand>

                return sampleProvider;
            }
            //</snippetOCSv2_CS_Basic_Peer_SetupSyncProvider>
        }

        //Handle the statistics that are returned by the SyncAgent.
        public class SampleStats
        {
            public void DisplayStats(SyncOperationStatistics syncStatistics, string syncType)
            {
                Console.WriteLine(String.Empty);
                if (syncType == "initial")
                {
                    Console.WriteLine("****** Initial Synchronization ******");
                }
                else if (syncType == "subsequent")
                {
                    Console.WriteLine("***** Subsequent Synchronization ****");
                }

                Console.WriteLine("Start Time: " + syncStatistics.SyncStartTime);
                Console.WriteLine("Total Changes Uploaded: " + syncStatistics.UploadChangesTotal);
                Console.WriteLine("Total Changes Downloaded: " + syncStatistics.DownloadChangesTotal);
                Console.WriteLine("Complete Time: " + syncStatistics.SyncEndTime);
                Console.WriteLine(String.Empty);
            }
        }
    }
}
//</snippetOCSv2_CS_Basic_Peer>


/*
 * 
//Specify the command to select metadata rows for cleanup.
//<snippetOCSv2_CS_Basic_Peer_SelectMetadataForCleanupCommand>
SqlCommand selMetadataCustomerCmd = new SqlCommand();
selMetadataCustomerCmd.CommandType = CommandType.StoredProcedure;
selMetadataCustomerCmd.CommandText = "Sync.sp_Customer_SelectMetadata";
selMetadataCustomerCmd.Parameters.Add("@metadata_aging_in_hours", SqlDbType.Int).Value = MetadataAgingInHours;
selMetadataCustomerCmd.Parameters.Add("@sync_scope_local_id", SqlDbType.Int);

adapterCustomer.SelectMetadataForCleanupCommand = selMetadataCustomerCmd;
//</snippetOCSv2_CS_Basic_Peer_SelectMetadataForCleanupCommand>
                
// SelectOverlappingScopesCommand
//<snippetOCSv2_CS_Basic_Peer_SelectOverlappingScopesCommand>
SqlCommand selOverlappingScopesCmd = new SqlCommand();
selOverlappingScopesCmd.CommandType = CommandType.StoredProcedure;
selOverlappingScopesCmd.CommandText = "sp_SelectSharedScopes";
selOverlappingScopesCmd.Parameters.Add("@" + DbSyncSession.SyncScopeName, SqlDbType.NVarChar, 100);
sampleProvider.SelectOverlappingScopesCommand = selOverlappingScopesCmd;
//</snippetOCSv2_CS_Basic_Peer_SelectOverlappingScopesCommand>
        
                
// UpdateScopeCleanupTimestampCommand:
//<snippetOCSv2_CS_Basic_Peer_UpdateScopeCleanupTimestampCommand>
SqlCommand selUpdateScopeCleanupTsCmd = new SqlCommand();
//</snippetOCSv2_CS_Basic_Peer_UpdateScopeCleanupTimestampCommand>*/