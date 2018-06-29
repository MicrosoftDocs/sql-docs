//<snippetOCS_CS_DownloadOnly>
using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.Server;
using Microsoft.Synchronization.Data.SqlServerCe;

namespace Microsoft.Samples.Synchronization
{
    class Program
    {
        static void Main(string[] args)
        {
            //The Utility class handles all functionality that is not
            //directly related to synchronization, such as holding 
            //connection string information and making changes to the 
            //server and client databases.
            Utility util = new Utility();

            //The SampleStats class handles information from the SyncStatistics
            //object that the Synchronize method returns.
            SampleStats sampleStats = new SampleStats();

            //Request a password for the client database, and delete
            //and re-create the database. The client synchronization
            //provider also enables you to create the client database 
            //if it does not exist.
            util.SetClientPassword();
            util.RecreateClientDatabase();

            //Initial synchronization. Instantiate the SyncAgent
            //and call Synchronize.
            SampleSyncAgent sampleSyncAgent = new SampleSyncAgent();
            SyncStatistics syncStatistics = sampleSyncAgent.Synchronize();
            sampleStats.DisplayStats(syncStatistics, "initial");

            //Make changes on the server.
            util.MakeDataChangesOnServer("Customer");

            //Subsequent synchronization.
            syncStatistics = sampleSyncAgent.Synchronize();
            sampleStats.DisplayStats(syncStatistics, "subsequent");

            //Return server data back to its original state.
            util.CleanUpServer();

            //Exit.
            Console.Write("\nPress Enter to close the window.");
            Console.ReadLine();
        }
    }

    //Create a class that is derived from 
    //Microsoft.Synchronization.SyncAgent.
    public class SampleSyncAgent : SyncAgent
    {
        public SampleSyncAgent()
        {
            //Instantiate a client synchronization provider and specify it
            //as the local provider for this synchronization agent.
            this.LocalProvider = new SampleClientSyncProvider();

            //Instantiate a server synchronization provider and specify it
            //as the remote provider for this synchronization agent.
            this.RemoteProvider = new SampleServerSyncProvider();

            //Create a Customer SyncGroup. This is not required
            //for the single table we are synchronizing; it is typically
            //used so that changes to multiple related tables are 
            //synchronized at the same time.
            SyncGroup customerSyncGroup = new SyncGroup("Customer");

            //Add the Customer table: specify a synchronization direction of
            //DownloadOnly and that an existing table should be dropped.
            //<snippetOCS_CS_DownloadOnly_CustomerSyncTable>
            SyncTable customerSyncTable = new SyncTable("Customer");
            customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            customerSyncTable.SyncDirection = SyncDirection.DownloadOnly;
            customerSyncTable.SyncGroup = customerSyncGroup;
            this.Configuration.SyncTables.Add(customerSyncTable);
            //</snippetOCS_CS_DownloadOnly_CustomerSyncTable>
        }
    }

    //Create a class that is derived from 
    //Microsoft.Synchronization.Server.DbServerSyncProvider.
    public class SampleServerSyncProvider : DbServerSyncProvider
    {
        public SampleServerSyncProvider()
        {
            //Create a connection to the sample server database.
            Utility util = new Utility();
            SqlConnection serverConn = new SqlConnection(util.ServerConnString);
            this.Connection = serverConn;

            //Create a command to retrieve a new anchor value from
            //the server. In this case, we use a timestamp value
            //that is retrieved and stored in the client database.
            //During each synchronization, the new anchor value and
            //the last anchor value from the previous synchronization
            //are used: the set of changes between these upper and
            //lower bounds is synchronized.
            //
            //SyncSession.SyncNewReceivedAnchor is a string constant; 
            //you could also use @sync_new_received_anchor directly in 
            //your queries.
            //<snippetOCS_CS_DownloadOnly_NewAnchorCommand>
            SqlCommand selectNewAnchorCommand = new SqlCommand();
            string newAnchorVariable = "@" + SyncSession.SyncNewReceivedAnchor;
            selectNewAnchorCommand.CommandText =
                "SELECT " + newAnchorVariable + " = min_active_rowversion() - 1";
            selectNewAnchorCommand.Parameters.Add(newAnchorVariable, SqlDbType.Timestamp);
            selectNewAnchorCommand.Parameters[newAnchorVariable].Direction = ParameterDirection.Output;
            selectNewAnchorCommand.Connection = serverConn;
            this.SelectNewAnchorCommand = selectNewAnchorCommand;
            //</snippetOCS_CS_DownloadOnly_NewAnchorCommand>

            //Create a SyncAdapter for the Customer table, and then define
            //the commands to synchronize changes:
            //* SelectIncrementalInsertsCommand, SelectIncrementalUpdatesCommand,
            //  and SelectIncrementalDeletesCommand are used to select changes
            //  from the server that the client provider then applies to the client.           

            //Create the SyncAdapter.
            SyncAdapter customerSyncAdapter = new SyncAdapter("Customer");

            //Select inserts from the server.
            SqlCommand customerIncrInserts = new SqlCommand();
            customerIncrInserts.CommandText =
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " +
                "FROM Sales.Customer " +
                "WHERE (InsertTimestamp > @sync_last_received_anchor " +
                "AND InsertTimestamp <= @sync_new_received_anchor)";
            customerIncrInserts.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            customerIncrInserts.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);            
            customerIncrInserts.Connection = serverConn;
            customerSyncAdapter.SelectIncrementalInsertsCommand = customerIncrInserts;

            //Select updates from the server.
            //<snippetOCS_CS_DownloadOnly_CustomerIncrUpdate>
            SqlCommand customerIncrUpdates = new SqlCommand();
            customerIncrUpdates.CommandText =
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " +
                "FROM Sales.Customer " +
                "WHERE (UpdateTimestamp > @sync_last_received_anchor " +
                "AND UpdateTimestamp <= @sync_new_received_anchor " +
                "AND NOT (InsertTimestamp > @sync_last_received_anchor))";
            customerIncrUpdates.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            customerIncrUpdates.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);            
            customerIncrUpdates.Connection = serverConn;
            customerSyncAdapter.SelectIncrementalUpdatesCommand = customerIncrUpdates;
            //</snippetOCS_CS_DownloadOnly_CustomerIncrUpdate>

            //Select deletes from the server.
            SqlCommand customerIncrDeletes = new SqlCommand();
            customerIncrDeletes.CommandText =
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " +
                "FROM Sales.Customer_Tombstone " +
                "WHERE (@sync_initialized = 1 " +
                "AND DeleteTimestamp > @sync_last_received_anchor " +
                "AND DeleteTimestamp <= @sync_new_received_anchor)";
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Bit);
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);            
            customerIncrDeletes.Connection = serverConn;
            customerSyncAdapter.SelectIncrementalDeletesCommand = customerIncrDeletes;

            //Add the SyncAdapter to the server synchronization provider.
            this.SyncAdapters.Add(customerSyncAdapter);

        }
    }

    //Create a class that is derived from 
    //Microsoft.Synchronization.Data.SqlServerCe.SqlCeClientSyncProvider.
    //You can just instantiate the provider directly and associate it
    //with the SyncAgent, but here we use this class to handle client 
    //provider events.
    public class SampleClientSyncProvider : SqlCeClientSyncProvider
    {

        public SampleClientSyncProvider()
        {
            //Specify a connection string for the sample client database.
            Utility util = new Utility();
            this.ConnectionString = util.ClientConnString;

            //Unlike Bidirectional and UploadOnly synchronization, we do
            //not have to make schema changes for the Customer table. In
            //those scenarios, we added defaults because inserts were made
            //directly into the client database. We handle the events here
            //so that we can write to the screen.
            this.CreatingSchema += new EventHandler<CreatingSchemaEventArgs>(SampleClientSyncProvider_CreatingSchema);
            this.SchemaCreated += new EventHandler<SchemaCreatedEventArgs>(SampleClientSyncProvider_SchemaCreated);
        }

        private void SampleClientSyncProvider_CreatingSchema(object sender, CreatingSchemaEventArgs e)
        {
            Console.Write("Creating schema for " + e.Table.TableName + " | ");
        }

        private void SampleClientSyncProvider_SchemaCreated(object sender, SchemaCreatedEventArgs e)
        {
            Console.WriteLine("Schema created for " + e.Table.TableName);
        }
    }

    //Handle the statistics returned by the SyncAgent.
    public class SampleStats
    {
        public void DisplayStats(SyncStatistics syncStatistics, string syncType)
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
            Console.WriteLine("Total Changes Downloaded: " + syncStatistics.TotalChangesDownloaded);
            Console.WriteLine("Complete Time: " + syncStatistics.SyncCompleteTime);
            Console.WriteLine(String.Empty);
        }
    }
}
//</snippetOCS_CS_DownloadOnly>
