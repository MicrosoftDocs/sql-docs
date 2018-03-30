//<snippetOCS_CS_View>
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
            util.MakeDataChangesOnServer("CustomerContact");

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

            //Add the Customer table: specify a synchronization direction of
            //DownloadOnly, and that an existing table should be dropped.
            //<snippetOCS_CS_View_CustomerInfoSyncTable>
            SyncTable customerInfoSyncTable = new SyncTable("CustomerInfo");
            customerInfoSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            customerInfoSyncTable.SyncDirection = SyncDirection.DownloadOnly;
            this.Configuration.SyncTables.Add(customerInfoSyncTable);
            //</snippetOCS_CS_View_CustomerInfoSyncTable>
            
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
            //<snippetOCS_CS_View_NewAnchorCommand>
            SqlCommand selectNewAnchorCommand = new SqlCommand();
            string newAnchorVariable = "@" + SyncSession.SyncNewReceivedAnchor;
            selectNewAnchorCommand.CommandText =
                "SELECT " + newAnchorVariable + " = min_active_rowversion() - 1";
            selectNewAnchorCommand.Parameters.Add(newAnchorVariable, SqlDbType.Timestamp);
            selectNewAnchorCommand.Parameters[newAnchorVariable].Direction = ParameterDirection.Output;
            selectNewAnchorCommand.Connection = serverConn;
            this.SelectNewAnchorCommand = selectNewAnchorCommand;
            //</snippetOCS_CS_View_NewAnchorCommand>

            //Create a SyncAdapter for the CustomerInfo table. The CustomerInfo 
            //table on the client is a combination of the Customer and CustomerContact
            //tables on the server. This table is download-only, as specified in 
            //SampleSyncAgent.
            //<snippetOCS_CS_View_CustomerInfoSyncAdapter>
            SyncAdapter customerInfoSyncAdapter = new SyncAdapter("CustomerInfo");
            //</snippetOCS_CS_View_CustomerInfoSyncAdapter>

            //Specify synchronization commands. The CustomerInfo table 
            //is download-only, so we do not define commands to apply changes to 
            //the server. Each command joins the base tables or tombstone tables
            //to select the appropriate incremental changes. For this application,
            //the logic is as follows:
            //* Select all inserts for customers that have contact information.
            //  This results in more than one row for a customer if that customer 
            //  has more than one phone number.
            //* Select all updates for customer and contact information that has 
            //  already been downloaded.
            //* Select all deletes for customer and contact information that has 
            //  already been downloaded. If a customer has been deleted, delete
            //  all of the rows for that customer. If a phone number has been
            //  deleted, delete only that row.

            //Select inserts.
            //<snippetOCS_CS_View_CustomerInfoIncrInsert>
            SqlCommand customerInfoIncrementalInsertsCommand = new SqlCommand();
            customerInfoIncrementalInsertsCommand.CommandType = CommandType.Text;
            customerInfoIncrementalInsertsCommand.CommandText =
                "SELECT c.CustomerId, c.CustomerName, c.SalesPerson, cc.PhoneNumber, cc.PhoneType " +
                "FROM Sales.Customer c JOIN Sales.CustomerContact cc ON " +
                "c.CustomerId = cc.CustomerId " +
                "WHERE ((c.InsertTimestamp > @sync_last_received_anchor " +
                "AND c.InsertTimestamp <= @sync_new_received_anchor) OR " +
                "(cc.InsertTimestamp > @sync_last_received_anchor " +
                "AND cc.InsertTimestamp <= @sync_new_received_anchor))";
            customerInfoIncrementalInsertsCommand.Parameters.Add("@sync_last_received_anchor", SqlDbType.Timestamp);
            customerInfoIncrementalInsertsCommand.Parameters.Add("@sync_new_received_anchor", SqlDbType.Timestamp);
            customerInfoIncrementalInsertsCommand.Connection = serverConn;
            customerInfoSyncAdapter.SelectIncrementalInsertsCommand = customerInfoIncrementalInsertsCommand;
            //</snippetOCS_CS_View_CustomerInfoIncrInsert>

            //Select updates.
            SqlCommand customerInfoIncrementalUpdatesCommand = new SqlCommand();
            customerInfoIncrementalUpdatesCommand.CommandType = CommandType.Text;
            customerInfoIncrementalUpdatesCommand.CommandText =
                "SELECT c.CustomerId, c.CustomerName, c.SalesPerson, cc.PhoneNumber, cc.PhoneType " +
                "FROM Sales.Customer c JOIN Sales.CustomerContact cc ON " +
                "c.CustomerId = cc.CustomerId " +
                "WHERE ((c.UpdateTimestamp > @sync_last_received_anchor " +
                "AND c.UpdateTimestamp <= @sync_new_received_anchor " +
                "AND c.InsertTimestamp <= @sync_last_received_anchor) " +
                "OR (cc.UpdateTimestamp > @sync_last_received_anchor " +
                "AND cc.UpdateTimestamp <= @sync_new_received_anchor " +
                "AND cc.InsertTimestamp <= @sync_last_received_anchor))";
            customerInfoIncrementalUpdatesCommand.Parameters.Add("@sync_last_received_anchor", SqlDbType.Timestamp);
            customerInfoIncrementalUpdatesCommand.Parameters.Add("@sync_new_received_anchor", SqlDbType.Timestamp);
            customerInfoIncrementalUpdatesCommand.Connection = serverConn;
            customerInfoSyncAdapter.SelectIncrementalUpdatesCommand = customerInfoIncrementalUpdatesCommand;

            //Select deletes.
            //<snippetOCS_CS_View_CustomerInfoIncrDelete>
            SqlCommand customerInfoIncrementalDeletesCommand = new SqlCommand();
            customerInfoIncrementalDeletesCommand.CommandType = CommandType.Text;
            customerInfoIncrementalDeletesCommand.CommandText =
                "SELECT c.CustomerId, cc.PhoneType " +
                "FROM Sales.Customer_Tombstone c JOIN Sales.CustomerContact cc ON " +
                "c.CustomerId = cc.CustomerId " +
                "WHERE (@sync_initialized = 1 " +
                "AND (DeleteTimestamp > @sync_last_received_anchor " +
                "AND DeleteTimestamp <= @sync_new_received_anchor)) " +
                "UNION " +
                "SELECT CustomerId, PhoneType " +
                "FROM Sales.CustomerContact_Tombstone " +
                "WHERE (@sync_initialized = 1 " +
                "AND (DeleteTimestamp > @sync_last_received_anchor " +
                "AND DeleteTimestamp <= @sync_new_received_anchor))";
            customerInfoIncrementalDeletesCommand.Parameters.Add("@sync_initialized", SqlDbType.Bit);
            customerInfoIncrementalDeletesCommand.Parameters.Add("@sync_last_received_anchor", SqlDbType.Timestamp);
            customerInfoIncrementalDeletesCommand.Parameters.Add("@sync_new_received_anchor", SqlDbType.Timestamp);
            customerInfoIncrementalDeletesCommand.Connection = serverConn;
            customerInfoSyncAdapter.SelectIncrementalDeletesCommand = customerInfoIncrementalDeletesCommand;
            //</snippetOCS_CS_View_CustomerInfoIncrDelete>

            //Add the SyncAdapter to the provider.
            this.SyncAdapters.Add(customerInfoSyncAdapter);

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

            //Handle the two schema-related events.
            this.CreatingSchema += new EventHandler<CreatingSchemaEventArgs>(SampleClientSyncProvider_CreatingSchema);
            this.SchemaCreated += new EventHandler<SchemaCreatedEventArgs>(SampleClientSyncProvider_SchemaCreated);
        }

        private void SampleClientSyncProvider_CreatingSchema(object sender, CreatingSchemaEventArgs e)
        {
            Console.Write("Creating schema for " + e.Table.TableName + " | ");

            //Create a compostite primary key for the CustomerInfo table.
            //<snippetOCS_CS_View_CustomerInfoPrimaryKey>
            string[] customerInfoPrimaryKey = new string[2];
            customerInfoPrimaryKey[0] = "CustomerId";
            customerInfoPrimaryKey[1] = "PhoneType";
            e.Schema.Tables["CustomerInfo"].PrimaryKey = customerInfoPrimaryKey;
            //</snippetOCS_CS_View_CustomerInfoPrimaryKey>
        }

        private void SampleClientSyncProvider_SchemaCreated(object sender, SchemaCreatedEventArgs e)
        {
            Console.WriteLine("Schema created for " + e.Table.TableName);
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
//</snippetOCS_CS_View>