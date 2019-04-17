//<snippetOCS_CS_Filter_Manual>
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
            //string information, and making changes to the server and client databases.
            Utility util = new Utility();

            //The SampleStats class handles information from the SyncStatistics
            //object that the Synchronize method returns.
            SampleStats sampleStats = new SampleStats();

            //Request a password for the client database, and delete
            //and recreate the database. The client synchronization
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
    //Microsoft.Synchronization.SyncAgent
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

            //Create two SyncGroups, so that changes to OrderHeader
            //and OrderDetail are made in one transaction. Depending on
            //application requirements, you might include Customer
            //in the same group.
            SyncGroup customerSyncGroup = new SyncGroup("Customer");
            SyncGroup orderSyncGroup = new SyncGroup("Order");

            //Add each table: specify a synchronization direction of
            //DownloadOnly.
            SyncTable customerSyncTable = new SyncTable("Customer");
            customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            customerSyncTable.SyncDirection = SyncDirection.DownloadOnly;
            customerSyncTable.SyncGroup = customerSyncGroup;
            this.Configuration.SyncTables.Add(customerSyncTable);
            
            SyncTable orderHeaderSyncTable = new SyncTable("OrderHeader");
            orderHeaderSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            orderHeaderSyncTable.SyncDirection = SyncDirection.DownloadOnly;
            orderHeaderSyncTable.SyncGroup = orderSyncGroup;
            this.Configuration.SyncTables.Add(orderHeaderSyncTable);
            
            SyncTable orderDetailSyncTable = new SyncTable("OrderDetail");
            orderDetailSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            orderDetailSyncTable.SyncDirection = SyncDirection.DownloadOnly;
            orderDetailSyncTable.SyncGroup = orderSyncGroup;
            this.Configuration.SyncTables.Add(orderDetailSyncTable);

            //Specify a value for the @SalesPerson parameter that is added
            //in the server synchronization provider. This value would
            //typically be provided by a user in the application, but we
            //have hardcoded it here for convenience.
            //<snippetOCS_CS_Filter_Manual_AgentSyncParam>
            this.Configuration.SyncParameters.Add(
                new SyncParameter("@SalesPerson", "Brenda Diaz"));
            //</snippetOCS_CS_Filter_Manual_AgentSyncParam>
        }
    }

    //Create a class that is derived from 
    //Microsoft.Synchronization.Server.DbServerSyncProvider
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
            SqlCommand selectNewAnchorCommand = new SqlCommand();
            string newAnchorVariable = "@" + SyncSession.SyncNewReceivedAnchor;
            selectNewAnchorCommand.CommandText = "SELECT " + newAnchorVariable + " = min_active_rowversion() - 1";
            selectNewAnchorCommand.Parameters.Add(newAnchorVariable, SqlDbType.Timestamp);
            selectNewAnchorCommand.Parameters[newAnchorVariable].Direction = ParameterDirection.Output;
            selectNewAnchorCommand.Connection = serverConn;
            this.SelectNewAnchorCommand = selectNewAnchorCommand;

            //Create a SyncAdapter for each table, and then define
            //the commands to synchronize changes:
            //* SelectIncrementalInsertsCommand, SelectIncrementalUpdatesCommand,
            //  and SelectIncrementalDeletesCommand are used to select changes
            //  from the server that the client provider then applies to the client.
            //* Specify if you want only certain columns at the client by 
            //  using the SELECT statement in the command.
            //* Filter rows by using the WHERE clause in the command. 
            //  In this case, we filter out SalesPerson.

            //
            //Customer table
            //

            //Create the SyncAdapter
            SyncAdapter customerSyncAdapter = new SyncAdapter("Customer");

            //Select inserts from the server
            //<snippetOCS_CS_Filter_Manual_CustomerColumnRowFilter>
            SqlCommand customerIncrInserts = new SqlCommand();
            customerIncrInserts.CommandText =
                "SELECT CustomerId, CustomerName, CustomerType " +
                "FROM Sales.Customer " +
                "WHERE SalesPerson = @SalesPerson " +
                "AND (InsertTimestamp > @sync_last_received_anchor " +
                "AND InsertTimestamp <= @sync_new_received_anchor " +
                "AND InsertId <> @sync_client_id)";
            customerIncrInserts.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
            customerIncrInserts.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            customerIncrInserts.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            customerIncrInserts.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            customerIncrInserts.Connection = serverConn;
            customerSyncAdapter.SelectIncrementalInsertsCommand = customerIncrInserts;
            //</snippetOCS_CS_Filter_Manual_CustomerColumnRowFilter>

            //Select updates from the server
            SqlCommand customerIncrUpdates = new SqlCommand();
            customerIncrUpdates.CommandText =
                "SELECT CustomerId, CustomerName, CustomerType " +
                "FROM Sales.Customer " +
                "WHERE SalesPerson = @SalesPerson " +
                "AND (UpdateTimestamp > @sync_last_received_anchor " +
                "AND UpdateTimestamp <= @sync_new_received_anchor " +
                "AND UpdateId <> @sync_client_id " +
                "AND NOT (InsertTimestamp > @sync_last_received_anchor " +
                "AND InsertId <> @sync_client_id))";
            customerIncrUpdates.Parameters.Add("@SalesPerson", SqlDbType.NVarChar); 
            customerIncrUpdates.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            customerIncrUpdates.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            customerIncrUpdates.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            customerIncrUpdates.Connection = serverConn;
            customerSyncAdapter.SelectIncrementalUpdatesCommand = customerIncrUpdates;

            //Select deletes from the server
            SqlCommand customerIncrDeletes = new SqlCommand();
            customerIncrDeletes.CommandText =
                "SELECT CustomerId, CustomerName, CustomerType " +
                "FROM Sales.Customer_Tombstone " +
                "WHERE SalesPerson = @SalesPerson " +
                "AND (@sync_initialized = 1 " +
                "AND DeleteTimestamp > @sync_last_received_anchor " +
                "AND DeleteTimestamp <= @sync_new_received_anchor " +
                "AND DeleteId <> @sync_client_id)";
            customerIncrDeletes.Parameters.Add("@SalesPerson", SqlDbType.NVarChar); 
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Bit);
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            customerIncrDeletes.Connection = serverConn;
            customerSyncAdapter.SelectIncrementalDeletesCommand = customerIncrDeletes;

            //Add the SyncAdapter to the server synchronization provider
            this.SyncAdapters.Add(customerSyncAdapter);

            
            //
            //OrderHeader table
            //

            //Create the SyncAdapter
            SyncAdapter orderHeaderSyncAdapter = new SyncAdapter("OrderHeader");

            //Select inserts from the server
            //<snippetOCS_CS_Filter_Manual_OrderHeaderColumnRowFilter>
            SqlCommand orderHeaderIncrInserts = new SqlCommand();
            orderHeaderIncrInserts.CommandText =
                "SELECT oh.OrderId, oh.CustomerId, oh.OrderDate, oh.OrderStatus " +
                "FROM Sales.OrderHeader oh " +
                "JOIN Sales.Customer c ON oh.CustomerId = c.CustomerId " +
                "WHERE c.SalesPerson = @SalesPerson " +
                "AND (oh.InsertTimestamp > @sync_last_received_anchor " +
                "AND oh.InsertTimestamp <= @sync_new_received_anchor " +
                "AND oh.InsertId <> @sync_client_id)";
            orderHeaderIncrInserts.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
            orderHeaderIncrInserts.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            orderHeaderIncrInserts.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            orderHeaderIncrInserts.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            orderHeaderIncrInserts.Connection = serverConn;
            orderHeaderSyncAdapter.SelectIncrementalInsertsCommand = orderHeaderIncrInserts;
            //</snippetOCS_CS_Filter_Manual_OrderHeaderColumnRowFilter>

            //Select updates from the server
            SqlCommand orderHeaderIncrUpdates = new SqlCommand();
            orderHeaderIncrUpdates.CommandText =
                "SELECT oh.OrderId, oh.CustomerId, oh.OrderDate, oh.OrderStatus " +
                "FROM Sales.OrderHeader oh " +
                "JOIN Sales.Customer c ON oh.CustomerId = c.CustomerId " +
                "WHERE c.SalesPerson = @SalesPerson " +
                "AND (oh.UpdateTimestamp > @sync_last_received_anchor " +
                "AND oh.UpdateTimestamp <= @sync_new_received_anchor " +
                "AND oh.UpdateId <> @sync_client_id " +
                "AND NOT (oh.InsertTimestamp > @sync_last_received_anchor " +
                "AND oh.InsertId <> @sync_client_id))";
            orderHeaderIncrUpdates.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
            orderHeaderIncrUpdates.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            orderHeaderIncrUpdates.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            orderHeaderIncrUpdates.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            orderHeaderIncrUpdates.Connection = serverConn;
            orderHeaderSyncAdapter.SelectIncrementalUpdatesCommand = orderHeaderIncrUpdates;

            //Select deletes from the server
            SqlCommand orderHeaderIncrDeletes = new SqlCommand();
            orderHeaderIncrDeletes.CommandText =
                "SELECT oht.OrderId, oht.CustomerId, oht.OrderDate, oht.OrderStatus " +
                "FROM Sales.OrderHeader_Tombstone oht " +
                "JOIN Sales.Customer c ON oht.CustomerId = c.CustomerId " +
                "WHERE c.SalesPerson = @SalesPerson " +
                "AND (@sync_initialized = 1 " +
                "AND oht.DeleteTimestamp > @sync_last_received_anchor " +
                "AND oht.DeleteTimestamp <= @sync_new_received_anchor " +
                "AND oht.DeleteId <> @sync_client_id)";
            orderHeaderIncrDeletes.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
            orderHeaderIncrDeletes.Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Bit);
            orderHeaderIncrDeletes.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            orderHeaderIncrDeletes.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            orderHeaderIncrDeletes.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            orderHeaderIncrDeletes.Connection = serverConn;
            orderHeaderSyncAdapter.SelectIncrementalDeletesCommand = orderHeaderIncrDeletes;

            //Add the SyncAdapter to the server synchronization provider
            this.SyncAdapters.Add(orderHeaderSyncAdapter);

            
            //
            //OrderDetail table
            //

            //Create the SyncAdapter
            SyncAdapter orderDetailSyncAdapter = new SyncAdapter("OrderDetail");

            //Select inserts from the server
            SqlCommand orderDetailIncrInserts = new SqlCommand();
            orderDetailIncrInserts.CommandText =
                "SELECT od.OrderDetailId, od.OrderId, od.Product, od.Quantity " +
                "FROM Sales.OrderDetail od " +
                "JOIN Sales.OrderHeader oh ON od.OrderId = oh.OrderId " +
                "JOIN Sales.Customer c ON oh.CustomerId = c.CustomerId " +
                "WHERE SalesPerson = @SalesPerson " +
                "AND (od.InsertTimestamp > @sync_last_received_anchor " +
                "AND od.InsertTimestamp <= @sync_new_received_anchor " +
                "AND od.InsertId <> @sync_client_id)";
            orderDetailIncrInserts.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
            orderDetailIncrInserts.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            orderDetailIncrInserts.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            orderDetailIncrInserts.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            orderDetailIncrInserts.Connection = serverConn;
            orderDetailSyncAdapter.SelectIncrementalInsertsCommand = orderDetailIncrInserts;

            //Select updates from the server
            SqlCommand orderDetailIncrUpdates = new SqlCommand();
            orderDetailIncrUpdates.CommandText =
                "SELECT od.OrderDetailId, od.OrderId, od.Product, od.Quantity " +
                "FROM Sales.OrderDetail od " +
                "JOIN Sales.OrderHeader oh ON od.OrderId = oh.OrderId " +
                "JOIN Sales.Customer c ON oh.CustomerId = c.CustomerId " +
                "WHERE SalesPerson = @SalesPerson " +
                "AND (od.UpdateTimestamp > @sync_last_received_anchor " +
                "AND od.UpdateTimestamp <= @sync_new_received_anchor " +
                "AND od.UpdateId <> @sync_client_id " +
                "AND NOT (od.InsertTimestamp > @sync_last_received_anchor " +
                "AND od.InsertId <> @sync_client_id))";
            orderDetailIncrUpdates.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
            orderDetailIncrUpdates.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            orderDetailIncrUpdates.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            orderDetailIncrUpdates.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            orderDetailIncrUpdates.Connection = serverConn;
            orderDetailSyncAdapter.SelectIncrementalUpdatesCommand = orderDetailIncrUpdates;

            //Select deletes from the server
            SqlCommand orderDetailIncrDeletes = new SqlCommand();
            orderDetailIncrDeletes.CommandText =
                "SELECT odt.OrderDetailId, odt.OrderId, odt.Product, odt.Quantity " +
                "FROM Sales.OrderDetail_Tombstone odt " +
                "JOIN Sales.OrderHeader oh ON odt.OrderId = oh.OrderId " +
                "JOIN Sales.Customer c ON oh.CustomerId = c.CustomerId " +
                "WHERE SalesPerson = @SalesPerson " +
                "AND (@sync_initialized = 1 " +
                "AND odt.DeleteTimestamp > @sync_last_received_anchor " +
                "AND odt.DeleteTimestamp <= @sync_new_received_anchor " +
                "AND odt.DeleteId <> @sync_client_id)";
            orderDetailIncrDeletes.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
            orderDetailIncrDeletes.Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Bit);
            orderDetailIncrDeletes.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            orderDetailIncrDeletes.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            orderDetailIncrDeletes.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            orderDetailIncrDeletes.Connection = serverConn;
            orderDetailSyncAdapter.SelectIncrementalDeletesCommand = orderDetailIncrDeletes;

            //Add the SyncAdapter to the server synchronization provider
            this.SyncAdapters.Add(orderDetailSyncAdapter);
           
        }
    }

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
//</snippetOCS_CS_Filter_Manual>
