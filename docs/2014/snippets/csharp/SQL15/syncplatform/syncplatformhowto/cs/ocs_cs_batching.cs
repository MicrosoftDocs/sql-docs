//<snippetOCS_CS_Batching>
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
            //directly related to synchronization, such as holding connection 
            //string information and making changes to the server and client databases.
            Utility util = new Utility();

            //The SampleStats class handles information from the 
            //SyncStatistics object that the Synchronize method returns and
            //from SyncAgent events.
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
            util.MakeDataChangesOnServer("CustomerAndOrderHeader");

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

            //<snippetOCS_CS_Batching_SyncGroupAndTables>
            //Create a SyncGroup so that changes to Customer
            //and OrderHeader are made in one transaction.
            SyncGroup customerOrderSyncGroup = new SyncGroup("CustomerOrder");

            //Add each table: specify a synchronization direction of
            //DownloadOnly.
            SyncTable customerSyncTable = new SyncTable("Customer");
            customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            customerSyncTable.SyncDirection = SyncDirection.DownloadOnly;
            customerSyncTable.SyncGroup = customerOrderSyncGroup;
            this.Configuration.SyncTables.Add(customerSyncTable);

            SyncTable orderHeaderSyncTable = new SyncTable("OrderHeader");
            orderHeaderSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            orderHeaderSyncTable.SyncDirection = SyncDirection.DownloadOnly;
            orderHeaderSyncTable.SyncGroup = customerOrderSyncGroup;
            this.Configuration.SyncTables.Add(orderHeaderSyncTable);
            //</snippetOCS_CS_Batching_SyncGroupAndTables>
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
            //the server. In this case, we call a stored procedure
            //that returns an anchor that can be used with batches
            //of changes.
            //<snippetOCS_CS_Batching_NewAnchorCommand>
            SqlCommand selectNewAnchorCommand = new SqlCommand();
            selectNewAnchorCommand.Connection = serverConn;
            selectNewAnchorCommand.CommandText = "usp_GetNewBatchAnchor";
            selectNewAnchorCommand.CommandType = CommandType.StoredProcedure;            
            selectNewAnchorCommand.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp, 8);
            selectNewAnchorCommand.Parameters.Add("@" + SyncSession.SyncMaxReceivedAnchor, SqlDbType.Timestamp, 8);
            selectNewAnchorCommand.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp, 8);
            selectNewAnchorCommand.Parameters.Add("@" + SyncSession.SyncBatchSize, SqlDbType.Int, 4);
            selectNewAnchorCommand.Parameters.Add("@" + SyncSession.SyncBatchCount, SqlDbType.Int, 4);            

            selectNewAnchorCommand.Parameters["@" + SyncSession.SyncMaxReceivedAnchor].Direction = ParameterDirection.Output;
            selectNewAnchorCommand.Parameters["@" + SyncSession.SyncNewReceivedAnchor].Direction = ParameterDirection.Output;
            selectNewAnchorCommand.Parameters["@" + SyncSession.SyncBatchCount].Direction = ParameterDirection.InputOutput;
            this.SelectNewAnchorCommand = selectNewAnchorCommand;
            this.BatchSize = 50;
            //</snippetOCS_CS_Batching_NewAnchorCommand>
            
            //Create SyncAdapters for each table by using the SqlSyncAdapterBuilder:
            //  * Specify the base table and tombstone table names.
            //  * Specify the columns that are used to track when
            //    and where changes are made.
            //  * Specify download only synchronization.
            //  * Call ToSyncAdapter to create the SyncAdapter.
            //  * Specify a name for the SyncAdapter that matches the
            //    the name specified for the corresponding SyncTable.
            //    Do not include the schema names (Sales in this case).

            //Customer table
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


            //OrderHeader table.
            SqlSyncAdapterBuilder orderHeaderBuilder = new SqlSyncAdapterBuilder(serverConn);

            orderHeaderBuilder.TableName = "Sales.OrderHeader";
            orderHeaderBuilder.TombstoneTableName = orderHeaderBuilder.TableName + "_Tombstone";
            orderHeaderBuilder.SyncDirection = SyncDirection.DownloadOnly;
            orderHeaderBuilder.CreationTrackingColumn = "InsertTimestamp";
            orderHeaderBuilder.UpdateTrackingColumn = "UpdateTimestamp";
            orderHeaderBuilder.DeletionTrackingColumn = "DeleteTimestamp";

            SyncAdapter orderHeaderSyncAdapter = orderHeaderBuilder.ToSyncAdapter();
            orderHeaderSyncAdapter.TableName = "OrderHeader";
            this.SyncAdapters.Add(orderHeaderSyncAdapter);

            //Handle the ChangesSelected event, and display
            //information to the console.
            this.ChangesSelected += new EventHandler<ChangesSelectedEventArgs>(SampleServerSyncProvider_ChangesSelected);
        }

        public void SampleServerSyncProvider_ChangesSelected(object sender, ChangesSelectedEventArgs e)
        {
            Console.WriteLine("Total number of batches: " + e.Context.BatchCount);
            Console.WriteLine("Changes applied for group " + e.GroupMetadata.GroupName);
            Console.WriteLine("Inserts applied for group: " + e.Context.GroupProgress.TotalInserts.ToString());
            Console.WriteLine("Updates applied for group: " + e.Context.GroupProgress.TotalUpdates.ToString());
            Console.WriteLine("Deletes applied for group: " + e.Context.GroupProgress.TotalDeletes.ToString());
        }
    }

    //Create a class that is derived from 
    //Microsoft.Synchronization.Data.SqlServerCe.SqlCeClientSyncProvider.
    public class SampleClientSyncProvider : SqlCeClientSyncProvider
    {

        public SampleClientSyncProvider()
        {
            //Specify a connection string for the sample client database.
            Utility util = new Utility();
            this.ConnectionString = util.ClientConnString;         
        }
    }

    //Handle the statistics that are returned by the SyncAgent.
    public class SampleStats
    {
        public void DisplayStats(SyncStatistics syncStatistics, string syncType)
        {
            Console.WriteLine(String.Empty);
            if (syncType == "initial")
            {
                Console.WriteLine("****** Initial Synchronization Stats ******");
            }
            else if (syncType == "subsequent")
            {
                Console.WriteLine("***** Subsequent Synchronization Stats ****");
            }

            Console.WriteLine("Start Time: " + syncStatistics.SyncStartTime);
            Console.WriteLine("Total Changes Downloaded: " + syncStatistics.TotalChangesDownloaded);
            Console.WriteLine("Complete Time: " + syncStatistics.SyncCompleteTime);
            Console.WriteLine(String.Empty);
        }
    }         
}
//</snippetOCS_CS_Batching>

/*
    //You can just instantiate the provider directly and associate it
    //with the SyncAgent, but here we use this class to handle client 
    //provider events.
 * 
 *      //Handle the SessionProgress event, and display
        //information to the console.        
        this.SessionProgress += new EventHandler<SessionProgressEventArgs>(SampleServerSyncAgent_SessionProgress);


    public void SampleServerSyncAgent_SessionProgress(object sender, SessionProgressEventArgs e)
    {
        Console.WriteLine(e.PercentCompleted);
            
        int batchNumber = e.BatchProgress.BatchNumber;
        int batchCount = e.BatchProgress.BatchCount;

        if (batchCount > 0)
        {
            Console.WriteLine("Batch " + batchNumber + " of " + batchCount);
        }
    }
 * 
*/