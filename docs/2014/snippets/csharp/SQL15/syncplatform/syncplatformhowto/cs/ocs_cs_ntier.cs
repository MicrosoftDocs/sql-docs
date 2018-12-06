//<snippetOCS_CS_NTier_ProvidersTwoTier>
this.LocalProvider = new SampleClientSyncProvider();

this.RemoteProvider = new SampleServerSyncProvider();
//</snippetOCS_CS_NTier_ProvidersTwoTier>

//<snippetOCS_CS_NTier_ProvidersNTier>
this.LocalProvider = new SampleClientSyncProvider();

ServiceReference.ServiceForSyncClient serviceProxy = new ServiceReference.ServiceForSyncClient();
this.RemoteProvider = new ServerSyncProviderProxy(serviceProxy);
//</snippetOCS_CS_NTier_ProvidersNTier>

//<snippetOCS_CS_NTier_SampleClientSyncProvider>
public class SampleClientSyncProvider : SqlCeClientSyncProvider
{

    public SampleClientSyncProvider()
    {
        //Specify a connection string for the sample client database.
        Utility util = new Utility();
        this.ConnectionString = util.ClientConnString;
    }
}
//</snippetOCS_CS_NTier_SampleClientSyncProvider>


//<snippetOCS_CS_NTier_IServiceForSync>
[ServiceContract]
public interface IServiceForSync
{
    [OperationContract()]
    SyncContext ApplyChanges(SyncGroupMetadata groupMetadata, DataSet dataSet, SyncSession syncSession);

    [OperationContract()]
    SyncContext GetChanges(SyncGroupMetadata groupMetadata, SyncSession syncSession);

    [OperationContract()]
    SyncSchema GetSchema(Collection<string> tableNames, SyncSession syncSession);

    [OperationContract()]
    SyncServerInfo GetServerInfo(SyncSession syncSession);
}
//</snippetOCS_CS_NTier_IServiceForSync>

//<snippetOCS_CS_NTier_ServiceForSync>
public class ServiceForSync : IServiceForSync
{

    private SampleServerSyncProvider _serverSyncProvider;

    public ServiceForSync()
    {
         this._serverSyncProvider = new SampleServerSyncProvider();
    }
    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public virtual SyncContext ApplyChanges(SyncGroupMetadata groupMetadata, DataSet dataSet, SyncSession syncSession) {
        return this._serverSyncProvider.ApplyChanges(groupMetadata, dataSet, syncSession);
    }
    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public virtual SyncContext GetChanges(SyncGroupMetadata groupMetadata, SyncSession syncSession) {
        return this._serverSyncProvider.GetChanges(groupMetadata, syncSession);
    }
    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public virtual SyncSchema GetSchema(Collection<string> tableNames, SyncSession syncSession) {
        return this._serverSyncProvider.GetSchema(tableNames, syncSession);
    }
    
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public virtual SyncServerInfo GetServerInfo(SyncSession syncSession) {
        return this._serverSyncProvider.GetServerInfo(syncSession);
    }       
}
//</snippetOCS_CS_NTier_ServiceForSync>

//<snippetOCS_CS_NTier_SampleServerSyncProvider>
public class SampleServerSyncProvider : DbServerSyncProvider
{
    public SampleServerSyncProvider()
    {
        //Create a connection to the sample server database.
        Utility util = new Utility();
        SqlConnection serverConn = new SqlConnection(util.ServerConnString);
        this.Connection = serverConn;

        //Create a command to retrieve a new anchor value from
        //the server.
        SqlCommand selectNewAnchorCommand = new SqlCommand();
        string newAnchorVariable = "@" + SyncSession.SyncNewReceivedAnchor;
        selectNewAnchorCommand.CommandText = "SELECT " + newAnchorVariable + " = min_active_rowversion() - 1";                                                         
        selectNewAnchorCommand.Parameters.Add(newAnchorVariable, SqlDbType.Timestamp);
        selectNewAnchorCommand.Parameters[newAnchorVariable].Direction = ParameterDirection.Output;
        selectNewAnchorCommand.Connection = serverConn;
        this.SelectNewAnchorCommand = selectNewAnchorCommand;

        //Create a SyncAdapter for the Customer table manually, or
        //by using the SqlSyncAdapterBuilder as in the following
        //code.
        SqlSyncAdapterBuilder customerBuilder = new SqlSyncAdapterBuilder(serverConn);

        customerBuilder.TableName = "Sales.Customer";
        customerBuilder.TombstoneTableName = customerBuilder.TableName + "_Tombstone";
        customerBuilder.SyncDirection = SyncDirection.DownloadOnly;
        customerBuilder.CreationTrackingColumn = "InsertTimestamp";
        customerBuilder.UpdateTrackingColumn = "UpdateTimestamp";
        customerBuilder.DeletionTrackingColumn = "DeleteTimestamp";
        
        SyncAdapter customerSyncAdapter = customerBuilder.ToSyncAdapter();
        customerSyncAdapter.TableName = "Customer";
        this.SyncAdapters.Add(customerSyncAdapter);
    }
}
//</snippetOCS_CS_NTier_SampleServerSyncProvider>