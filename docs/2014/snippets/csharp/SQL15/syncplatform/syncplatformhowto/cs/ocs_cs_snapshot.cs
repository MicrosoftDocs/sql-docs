//<snippetOCS_CS_Snapshot>
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

            //Subsequent synchronization. There was one insert,
            //one update, and one delete made on the server;
            //therefore, the row count is identical, but the
            //data is different.
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

            //Create two SyncGroups so that changes to OrderHeader
            //and OrderDetail are made in one transaction. Depending on
            //application requirements, you might include Customer
            //in the same group.
            SyncGroup customerSyncGroup = new SyncGroup("Customer");
            SyncGroup orderSyncGroup = new SyncGroup("Order");

            //Add each table: specify a synchronization direction of
            //Snapshot, and that any existing tables should be dropped.
            //<snippetOCS_CS_Snapshot_CustomerSyncTable>
            SyncTable customerSyncTable = new SyncTable("Customer");
            customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            customerSyncTable.SyncDirection = SyncDirection.Snapshot;
            customerSyncTable.SyncGroup = customerSyncGroup;
            this.Configuration.SyncTables.Add(customerSyncTable);
            //</snippetOCS_CS_Snapshot_CustomerSyncTable>

            SyncTable orderHeaderSyncTable = new SyncTable("OrderHeader");
            orderHeaderSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            orderHeaderSyncTable.SyncDirection = SyncDirection.Snapshot;
            orderHeaderSyncTable.SyncGroup = orderSyncGroup;
            this.Configuration.SyncTables.Add(orderHeaderSyncTable);

            SyncTable orderDetailSyncTable = new SyncTable("OrderDetail");
            orderDetailSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            orderDetailSyncTable.SyncDirection = SyncDirection.Snapshot;
            orderDetailSyncTable.SyncGroup = orderSyncGroup;
            this.Configuration.SyncTables.Add(orderDetailSyncTable);
        }
    }

    //Create a class that is derived from 
    //Microsoft.Synchronization.Server.DbServerSyncProvider.
    //<snippetOCS_CS_Snapshot_SampleServerSyncProvider>
    public class SampleServerSyncProvider : DbServerSyncProvider
    {
        public SampleServerSyncProvider()
        {
            //Create a connection to the sample server database.
            Utility util = new Utility();
            SqlConnection serverConn = new SqlConnection(util.ServerConnString);
            this.Connection = serverConn;
          
            //Create a SyncAdapter for each table, and then define
            //the command to select rows from the table. With the Snapshot
            //option, you do not download incremental changes. However,
            //you still use the SelectIncrementalInsertsCommand to select
            //the rows to download for each snapshot. The commands include
            //only those columns that you want on the client.

            //Customer table.
            //<snippetOCS_CS_Snapshot_CustomerIncrInsert>
            SyncAdapter customerSyncAdapter = new SyncAdapter("Customer");
            SqlCommand customerIncrInserts = new SqlCommand();
            customerIncrInserts.CommandText = 
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " +
                "FROM Sales.Customer";
            customerIncrInserts.Connection = serverConn;
            customerSyncAdapter.SelectIncrementalInsertsCommand = customerIncrInserts;
            this.SyncAdapters.Add(customerSyncAdapter);
            //</snippetOCS_CS_Snapshot_CustomerIncrInsert>

            //OrderHeader table.
            SyncAdapter orderHeaderSyncAdapter = new SyncAdapter("OrderHeader");
            SqlCommand orderHeaderIncrInserts = new SqlCommand();
            orderHeaderIncrInserts.CommandText = 
                "SELECT OrderId, CustomerId, OrderDate, OrderStatus " +
                "FROM Sales.OrderHeader";
            orderHeaderIncrInserts.Connection = serverConn;
            orderHeaderSyncAdapter.SelectIncrementalInsertsCommand = orderHeaderIncrInserts;
            this.SyncAdapters.Add(orderHeaderSyncAdapter);
            
            //OrderDetail table.
            SyncAdapter orderDetailSyncAdapter = new SyncAdapter("OrderDetail");
            SqlCommand orderDetailIncrInserts = new SqlCommand();            
            orderDetailIncrInserts.CommandText = 
                "SELECT OrderDetailId, OrderId, Product, Quantity " +
                "FROM Sales.OrderDetail";
            orderDetailIncrInserts.Connection = serverConn;
            orderDetailSyncAdapter.SelectIncrementalInsertsCommand = orderDetailIncrInserts;
            this.SyncAdapters.Add(orderDetailSyncAdapter);
        }
    }
    //</snippetOCS_CS_Snapshot_SampleServerSyncProvider>

    //Create a class that is derived from 
    //Microsoft.Synchronization.Data.SqlServerCe.SqlCeClientSyncProvider.
    //You can just instantiate the provider directly and associate it
    //with the SyncAgent, but you could use this class to handle client 
    //provider events and other client-side processing.
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
//</snippetOCS_CS_Snapshot>






