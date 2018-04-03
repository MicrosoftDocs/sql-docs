using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;

using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;

namespace OCSv2_CS_Peer_NTier
{
    class OCSv2_CS_Peer_NTier
    {
        static void Main(string[] args)
        {

            //<snippetOCSv2_CS_Basic_NTier_SyncOrchestrator>
            KnowledgeSyncProvider localProvider;
            KnowledgeSyncProvider remoteProvider;

            string localConnection = @"Data Source = localhost; Initial Catalog = SyncSamplesDb_Peer1; " +
                                      "Integrated Security = True";
            string remoteConnection = @"http://localhost:8000/Sync/SyncService";
            string scopeName = "Sales";
            
            SampleSyncProvider sampleSyncProvider = new SampleSyncProvider();
            localProvider = sampleSyncProvider.SetupSyncProvider(scopeName, localConnection);
            remoteProvider = new SyncProxy(scopeName, remoteConnection);     
            
            SyncOrchestrator syncOrchestrator = new SyncOrchestrator();

            syncOrchestrator.LocalProvider = localProvider;
            syncOrchestrator.RemoteProvider = remoteProvider;

            syncOrchestrator.Direction = SyncDirectionOrder.Download;
            syncOrchestrator.Synchronize();
            //</snippetOCSv2_CS_Basic_NTier_SyncOrchestrator>

        }
    }

    //<snippetOCSv2_CS_Basic_NTier_SyncProxy>
    public class SyncProxy : KnowledgeSyncProvider
    {
        public SyncProxy(string scopeName, string url)
        {
            this.scopeName = scopeName;

            WSHttpBinding binding = new WSHttpBinding();
            binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
            binding.OpenTimeout = new TimeSpan(0, 1, 0);
            ChannelFactory<ISyncContract> factory = new ChannelFactory<ISyncContract>(binding, 
                new EndpointAddress(url));
            proxy = factory.CreateChannel();
        }

        //The IdFormat settings for the peer provider.
        public override SyncIdFormatGroup IdFormats
        {
            get
            {
                if (idFormatGroup == null)
                {
                    idFormatGroup = new SyncIdFormatGroup();

                    //
                    // 1 byte change unit ID
                    //
                    idFormatGroup.ChangeUnitIdFormat.IsVariableLength = false;
                    idFormatGroup.ChangeUnitIdFormat.Length = 1;

                    //
                    // GUID replica ID
                    //
                    idFormatGroup.ReplicaIdFormat.IsVariableLength = false;
                    idFormatGroup.ReplicaIdFormat.Length = 16;


                    //
                    // Global ID for item IDs
                    //
                    idFormatGroup.ItemIdFormat.IsVariableLength = true;
                    idFormatGroup.ItemIdFormat.Length = 10 * 1024;
                }

                return idFormatGroup;
            }
        }

        public override void BeginSession(SyncProviderPosition position, SyncSessionContext syncSessionContext)
        {
            proxy.BeginSession(scopeName);
        }

        public override void EndSession(SyncSessionContext syncSessionContext)
        {
            proxy.EndSession();
        }

        public override void GetSyncBatchParameters(out uint batchSize, out SyncKnowledge knowledge)
        {
            proxy.GetKnowledge(out batchSize, out knowledge);
        }

        public override ChangeBatch GetChangeBatch(uint batchSize, SyncKnowledge destinationKnowledge, 
            out object changeDataRetriever)
        {
            return proxy.GetChanges(batchSize, destinationKnowledge, out changeDataRetriever);
        }

        public override void ProcessChangeBatch(ConflictResolutionPolicy resolutionPolicy, ChangeBatch sourceChanges, 
            object changeDataRetriever, SyncCallbacks syncCallback, SyncSessionStatistics sessionStatistics)
        {
            SyncSessionStatistics remoteSessionStatistics = new SyncSessionStatistics();
            proxy.ApplyChanges(resolutionPolicy, sourceChanges, changeDataRetriever, ref remoteSessionStatistics);
            sessionStatistics.ChangesApplied = remoteSessionStatistics.ChangesApplied;
            sessionStatistics.ChangesFailed = remoteSessionStatistics.ChangesFailed;
        }

        public override FullEnumerationChangeBatch GetFullEnumerationChangeBatch(uint batchSize, SyncId lowerEnumerationBound, 
            SyncKnowledge knowledgeForDataRetrieval, out object changeDataRetriever)
        {
            //Do nothing.
            //
            throw new NotImplementedException();
        }

        public override void ProcessFullEnumerationChangeBatch(ConflictResolutionPolicy resolutionPolicy, 
            FullEnumerationChangeBatch sourceChanges, object changeDataRetriever, SyncCallbacks syncCallback, 
            SyncSessionStatistics sessionStatistics)
        {
            //Do nothing.
            //
            throw new NotImplementedException();
        }

        private string scopeName;
        private ISyncContract proxy;
        private SyncIdFormatGroup idFormatGroup = null;
    }
    //</snippetOCSv2_CS_Basic_NTier_SyncProxy>


    //<snippetOCSv2_CS_Basic_NTier_SyncService>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class SyncService : ISyncContract
    {
        public void BeginSession(string scopeName)
        {
            
            string localConnection = @"Data Source = localhost; Initial Catalog = SyncSamplesDb_Peer2; " +
                                      "Integrated Security = True";
            SampleSyncProvider sampleSyncProvider = new SampleSyncProvider();
            peerProvider = sampleSyncProvider.SetupSyncProvider(scopeName, localConnection);

        }

        public void GetKnowledge(
            out uint batchSize, 
            out SyncKnowledge knowledge)
        {
            peerProvider.GetSyncBatchParameters(out batchSize, out knowledge);
        }

        public ChangeBatch GetChanges(
            uint batchSize, 
            SyncKnowledge destinationKnowledge, 
            out object changeData)
        {
            return peerProvider.GetChangeBatch(batchSize, destinationKnowledge, out changeData);
        }

        public void ApplyChanges(
            ConflictResolutionPolicy resolutionPolicy,
            ChangeBatch sourceChanges,
            object changeData,
            ref SyncSessionStatistics sessionStatistics)
        {
            SyncCallbacks syncCallback = new SyncCallbacks();
            peerProvider.ProcessChangeBatch(resolutionPolicy, sourceChanges, changeData, syncCallback, 
                sessionStatistics);
        }

        public void EndSession()
        {
            peerProvider = null;
        }

        private DbSyncProvider peerProvider = null;
    }
    //</snippetOCSv2_CS_Basic_NTier_SyncService>

    
    //<snippetOCSv2_CS_Basic_NTier_ISyncContract>
    [ServiceContract(SessionMode = SessionMode.Required)]
    [ServiceKnownType(typeof(SyncIdFormatGroup))]
    [ServiceKnownType(typeof(DbSyncContext))]
    public interface ISyncContract
    {
        [OperationContract(IsInitiating = true, IsTerminating = false)]
        void BeginSession(string scopeName);

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        void GetKnowledge(out uint batchSize, out SyncKnowledge knowledge);

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        ChangeBatch GetChanges(uint batchSize, SyncKnowledge destinationKnowledge, out object changeData);

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        void ApplyChanges(ConflictResolutionPolicy resolutionPolicy, ChangeBatch sourceChanges, object changeData, 
            ref SyncSessionStatistics sessionStatistics);

        [OperationContract(IsInitiating = false, IsTerminating = true)]
        void EndSession();
    }
    //</snippetOCSv2_CS_Basic_NTier_ISyncContract>


    //<snippetOCSv2_CS_Basic_NTier_SampleSyncProvider>
    public class SampleSyncProvider
    {
        public DbSyncProvider SetupSyncProvider(string scopeName, string peerConnString)
        {

            const int MetadataAgingInHours = 100;

            DbSyncProvider peerProvider = new DbSyncProvider();

            SqlConnection peerConnection = new SqlConnection(peerConnString);
            peerProvider.Connection = peerConnection;
            peerProvider.ScopeName = scopeName;


            DbSyncAdapter adapterCustomer = new DbSyncAdapter("Customer");
            adapterCustomer.RowIdColumns.Add("CustomerId");


            SqlCommand chgsCustomerCmd = new SqlCommand();
            chgsCustomerCmd.CommandType = CommandType.StoredProcedure;
            chgsCustomerCmd.CommandText = "Sales.sp_Customer_SelectChanges";
            chgsCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncMetadataOnly, SqlDbType.Int);
            chgsCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt);
            chgsCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncInitialize, SqlDbType.Int);            

            adapterCustomer.SelectIncrementalChangesCommand = chgsCustomerCmd;


            SqlCommand insCustomerCmd = new SqlCommand();
            insCustomerCmd.CommandType = CommandType.StoredProcedure;
            insCustomerCmd.CommandText = "Sales.sp_Customer_ApplyInsert";
            insCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            insCustomerCmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar);
            insCustomerCmd.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
            insCustomerCmd.Parameters.Add("@CustomerType", SqlDbType.NVarChar);
            insCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;

            adapterCustomer.InsertCommand = insCustomerCmd;


            SqlCommand updCustomerCmd = new SqlCommand();
            updCustomerCmd.CommandType = CommandType.StoredProcedure;
            updCustomerCmd.CommandText = "Sales.sp_Customer_ApplyUpdate";
            updCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            updCustomerCmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar);
            updCustomerCmd.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
            updCustomerCmd.Parameters.Add("@CustomerType", SqlDbType.NVarChar);
            updCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt);
            updCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;
            updCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncForceWrite, SqlDbType.Int);
            
            adapterCustomer.UpdateCommand = updCustomerCmd;

 
            SqlCommand delCustomerCmd = new SqlCommand();
            delCustomerCmd.CommandType = CommandType.StoredProcedure;
            delCustomerCmd.CommandText = "Sales.sp_Customer_ApplyDelete";
            delCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            delCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncMinTimestamp, SqlDbType.BigInt);
            delCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;

            adapterCustomer.DeleteCommand = delCustomerCmd;


            SqlCommand selRowCustomerCmd = new SqlCommand();
            selRowCustomerCmd.CommandType = CommandType.StoredProcedure;
            selRowCustomerCmd.CommandText = "Sales.sp_Customer_SelectRow";
            selRowCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);

            adapterCustomer.SelectRowCommand = selRowCustomerCmd;


            SqlCommand insMetadataCustomerCmd = new SqlCommand();
            insMetadataCustomerCmd.CommandType = CommandType.StoredProcedure;
            insMetadataCustomerCmd.CommandText = "Sales.sp_Customer_InsertMetadata";
            insMetadataCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCreatePeerKey, SqlDbType.Int);
            insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCreatePeerTimestamp, SqlDbType.BigInt);
            insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncUpdatePeerKey, SqlDbType.Int);
            insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncUpdatePeerTimestamp, SqlDbType.BigInt);
            insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowIsTombstone, SqlDbType.Int);
            insMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;

            adapterCustomer.InsertMetadataCommand = insMetadataCustomerCmd;


            SqlCommand updMetadataCustomerCmd = new SqlCommand();
            updMetadataCustomerCmd.CommandType = CommandType.StoredProcedure;
            updMetadataCustomerCmd.CommandText = "Sales.sp_Customer_UpdateMetadata";
            updMetadataCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCreatePeerKey, SqlDbType.Int);
            updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCreatePeerTimestamp, SqlDbType.BigInt);
            updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncUpdatePeerKey, SqlDbType.Int);
            updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncUpdatePeerTimestamp, SqlDbType.BigInt);
            updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCheckConcurrency, SqlDbType.Int);
            updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowTimestamp, SqlDbType.BigInt);
            updMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;

            adapterCustomer.UpdateMetadataCommand = updMetadataCustomerCmd;


            SqlCommand delMetadataCustomerCmd = new SqlCommand();
            delMetadataCustomerCmd.CommandType = CommandType.StoredProcedure;
            delMetadataCustomerCmd.CommandText = "Sales.sp_Customer_DeleteMetadata";
            delMetadataCustomerCmd.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            delMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncCheckConcurrency, SqlDbType.Int);
            delMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowTimestamp, SqlDbType.BigInt);
            delMetadataCustomerCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;

            adapterCustomer.DeleteMetadataCommand = delMetadataCustomerCmd;


            SqlCommand selMetadataCustomerCmd = new SqlCommand();
            selMetadataCustomerCmd.CommandType = CommandType.StoredProcedure;
            selMetadataCustomerCmd.CommandText = "Sales.sp_Customer_SelectMetadata";
            selMetadataCustomerCmd.Parameters.Add("@metadata_aging_in_hours", SqlDbType.Int).Value = MetadataAgingInHours;

            adapterCustomer.SelectMetadataForCleanupCommand = selMetadataCustomerCmd;

            peerProvider.SyncAdapters.Add(adapterCustomer);

            SqlCommand selectNewTimestampCommand = new SqlCommand();
            string newTimestampVariable = "@" + DbSyncSession.SyncNewTimestamp;
            selectNewTimestampCommand.CommandText = "SELECT " + newTimestampVariable + " = min_active_rowversion() - 1";
            selectNewTimestampCommand.Parameters.Add(newTimestampVariable, SqlDbType.Timestamp);
            selectNewTimestampCommand.Parameters[newTimestampVariable].Direction = ParameterDirection.Output;
            
            peerProvider.SelectNewTimestampCommand = selectNewTimestampCommand;


            SqlCommand selReplicaInfoCmd = new SqlCommand();
            selReplicaInfoCmd.CommandType = CommandType.Text;
            selReplicaInfoCmd.CommandText = "SELECT " +
                                            "@" + DbSyncSession.SyncScopeId + " = scope_id, " +
                                            "@" + DbSyncSession.SyncScopeKnowledge + " = scope_sync_knowledge, " +
                                            "@" + DbSyncSession.SyncScopeCleanupKnowledge + " = scope_tombstone_cleanup_knowledge, " +
                                            "@" + DbSyncSession.SyncScopeTimestamp + " = scope_timestamp " +
                                            "FROM Sales.ScopeInfo " +
                                            "WHERE scope_name = @" + DbSyncSession.SyncScopeName;
            selReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeName, SqlDbType.NVarChar, 100);
            selReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeId, SqlDbType.UniqueIdentifier).Direction = ParameterDirection.Output;
            selReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeKnowledge, SqlDbType.VarBinary, 10000).Direction = ParameterDirection.Output;
            selReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeCleanupKnowledge, SqlDbType.VarBinary, 10000).Direction = ParameterDirection.Output;
            selReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeTimestamp, SqlDbType.BigInt).Direction = ParameterDirection.Output;
            
            peerProvider.SelectScopeInfoCommand = selReplicaInfoCmd;


            SqlCommand updReplicaInfoCmd = new SqlCommand();
            updReplicaInfoCmd.CommandType = CommandType.Text;
            updReplicaInfoCmd.CommandText = "UPDATE  Sales.ScopeInfo SET " +
                                            "scope_sync_knowledge = @" + DbSyncSession.SyncScopeKnowledge + ", " +
                                            "scope_tombstone_cleanup_knowledge = @" + DbSyncSession.SyncScopeCleanupKnowledge + " " +
                                            "WHERE scope_name = @" + DbSyncSession.SyncScopeName + " AND " +
                                            " ( @" + DbSyncSession.SyncCheckConcurrency + " = 0 or scope_timestamp = @" + DbSyncSession.SyncScopeTimestamp + "); " +
                                            "SET @" + DbSyncSession.SyncRowCount + " = @@rowcount";
            updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeKnowledge, SqlDbType.VarBinary, 10000);
            updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeCleanupKnowledge, SqlDbType.VarBinary, 10000);
            updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeName, SqlDbType.NVarChar, 100);
            updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncCheckConcurrency, SqlDbType.Int);
            updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncScopeTimestamp, SqlDbType.BigInt);
            updReplicaInfoCmd.Parameters.Add("@" + DbSyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;
            
            peerProvider.UpdateScopeInfoCommand = updReplicaInfoCmd;

            return peerProvider;

        }
    }
    //</snippetOCSv2_CS_Basic_NTier_SampleSyncProvider>
}

