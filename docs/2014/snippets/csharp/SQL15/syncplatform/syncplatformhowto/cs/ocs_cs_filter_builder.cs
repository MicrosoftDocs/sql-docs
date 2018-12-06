//<snippetOCS_CS_Filter_Builder>
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

            //<snippetOCS_CS_Filter_Builder_SyncGroupAndTables>
            //Create two SyncGroups so that changes to OrderHeader
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
            //</snippetOCS_CS_Filter_Builder_SyncGroupAndTables>

            //Specify a value for the @SalesPerson parameter that is added
            //in the server synchronization provider. This value would
            //typically be provided by a user in the application, but we
            //have hardcoded it here for convenience.
            //<snippetOCS_CS_Filter_Builder_AgentSyncParam>
            this.Configuration.SyncParameters.Add(
                new SyncParameter("@SalesPerson", "Brenda Diaz"));
            //</snippetOCS_CS_Filter_Builder_AgentSyncParam>
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
            SqlCommand selectNewAnchorCommand = new SqlCommand();
            string newAnchorVariable = "@" + SyncSession.SyncNewReceivedAnchor;
            selectNewAnchorCommand.CommandText = "SELECT " + newAnchorVariable + " = min_active_rowversion() - 1";
            selectNewAnchorCommand.Parameters.Add(newAnchorVariable, SqlDbType.Timestamp);
            selectNewAnchorCommand.Parameters[newAnchorVariable].Direction = ParameterDirection.Output;
            selectNewAnchorCommand.Connection = serverConn;
            this.SelectNewAnchorCommand = selectNewAnchorCommand;

            //Create a filter parameter that will be used in the filter clause for
            //all three tables.
            //<snippetOCS_CS_Filter_Builder_ProviderSyncParam>
            SqlParameter filterParameter = new SqlParameter("@SalesPerson", SqlDbType.NVarChar);
            //</snippetOCS_CS_Filter_Builder_ProviderSyncParam>

            //Create SyncAdapters for each table by using the SqlSyncAdapterBuilder:
            //  * Specify the base table and tombstone table names.
            //  * Specify the columns that are used to track when
            //    changes are made.
            //  * Specify download-only synchronization.
            //  * Specify if you want only certain columns at the client.
            //  * Specify filter clauses for the base tables and tombstone
            //    tables.
            //  * Call ToSyncAdapter to create the SyncAdapter.
            //  * Specify a name for the SyncAdapter that matches the
            //    the name that is specified for the corresponding SyncTable.
            //    Do not include the schema names (Sales in this case).

            //Customer table.
            SqlSyncAdapterBuilder customerBuilder = new SqlSyncAdapterBuilder(serverConn);

            customerBuilder.TableName = "Sales.Customer";
            customerBuilder.TombstoneTableName = customerBuilder.TableName + "_Tombstone";
            customerBuilder.SyncDirection = SyncDirection.DownloadOnly;
            customerBuilder.CreationTrackingColumn = "InsertTimestamp";
            customerBuilder.UpdateTrackingColumn = "UpdateTimestamp";
            customerBuilder.DeletionTrackingColumn = "DeleteTimestamp";
            
            //Specify the columns that you want at the client. If you
            //want all columns, this code is not required. In this
            //case, we filter out SalesPerson.
            //<snippetOCS_CS_Filter_Builder_CustomerColumnFilter>
            string[] customerDataColumns = new string[3];
            customerDataColumns[0] = "CustomerId";
            customerDataColumns[1] = "CustomerName";
            customerDataColumns[2] = "CustomerType";
            customerBuilder.DataColumns.AddRange(customerDataColumns);
            customerBuilder.TombstoneDataColumns.AddRange(customerDataColumns);
            //</snippetOCS_CS_Filter_Builder_CustomerColumnFilter>

            //Specify a filter clause, which is an SQL WHERE clause
            //without the WHERE keyword. Use the parameter that is 
            //created above. The value for the parameter is specified 
            //in the SyncAgent Configuration object.
            //<snippetOCS_CS_Filter_Builder_CustomerRowFilter>
            string customerFilterClause = "SalesPerson=@SalesPerson";
            customerBuilder.FilterClause = customerFilterClause;
            customerBuilder.FilterParameters.Add(filterParameter);
            customerBuilder.TombstoneFilterClause = customerFilterClause;
            customerBuilder.TombstoneFilterParameters.Add(filterParameter);
            //</snippetOCS_CS_Filter_Builder_CustomerRowFilter>
 
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
            //Filter properties: extend the filter to the OrderHeader table.
            //<snippetOCS_CS_Filter_Builder_OrderHeaderRowFilter>
            string orderHeaderFilterClause =
                "CustomerId IN (SELECT CustomerId FROM Sales.Customer " +
                                    "WHERE SalesPerson=@SalesPerson)";
            orderHeaderBuilder.FilterClause = orderHeaderFilterClause;   
            orderHeaderBuilder.FilterParameters.Add(filterParameter);
            orderHeaderBuilder.TombstoneFilterClause = orderHeaderFilterClause;
            orderHeaderBuilder.TombstoneFilterParameters.Add(filterParameter);
            //</snippetOCS_CS_Filter_Builder_OrderHeaderRowFilter>

            SyncAdapter orderHeaderSyncAdapter = orderHeaderBuilder.ToSyncAdapter();
            orderHeaderSyncAdapter.TableName = "OrderHeader";
            this.SyncAdapters.Add(orderHeaderSyncAdapter);


            //OrderDetail table.
            SqlSyncAdapterBuilder orderDetailBuilder = new SqlSyncAdapterBuilder(serverConn);

            orderDetailBuilder.TableName = "Sales.OrderDetail";
            orderDetailBuilder.TombstoneTableName = orderDetailBuilder.TableName + "_Tombstone";
            orderDetailBuilder.SyncDirection = SyncDirection.DownloadOnly;
            orderDetailBuilder.CreationTrackingColumn = "InsertTimestamp";
            orderDetailBuilder.UpdateTrackingColumn = "UpdateTimestamp";
            orderDetailBuilder.DeletionTrackingColumn = "DeleteTimestamp";
            //Filter properties: extend the filter to the OrderDetail table.
            string orderDetailFilterClause =
                "OrderId IN (SELECT OrderId FROM Sales.OrderHeader " +
                                "WHERE CustomerId IN " +
                                    "(SELECT CustomerId FROM Sales.Customer " +
                                        "WHERE SalesPerson=@SalesPerson))";
            orderDetailBuilder.FilterClause = orderDetailFilterClause;      
            orderDetailBuilder.FilterParameters.Add(filterParameter);
            orderDetailBuilder.TombstoneFilterClause = orderDetailFilterClause;
            orderDetailBuilder.TombstoneFilterParameters.Add(filterParameter);

            SyncAdapter orderDetailSyncAdapter = orderDetailBuilder.ToSyncAdapter();
            orderDetailSyncAdapter.TableName = "OrderDetail";
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
//</snippetOCS_CS_Filter_Builder>