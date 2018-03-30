//<snippetOCS_CS_BasicCT>
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
            //string information and making changes to the server database.
            Utility util = new Utility();

            //The SampleStats class handles information from the SyncStatistics
            //object that the Synchronize method returns.
            SampleStats sampleStats = new SampleStats();

            //Delete and re-create the database. The client synchronization
            //provider also enables you to create the client database 
            //if it does not exist.
            util.SetClientPassword();
            util.RecreateClientDatabase();

            //Initial synchronization. Instantiate the SyncAgent
            //and call Synchronize.
            //<snippetOCS_CS_BasicCT_Synchronize>
            SampleSyncAgent sampleSyncAgent = new SampleSyncAgent();
            SyncStatistics syncStatistics = sampleSyncAgent.Synchronize();
            //</snippetOCS_CS_BasicCT_Synchronize>
            sampleStats.DisplayStats(syncStatistics, "initial");

            //Make changes on the server.
            util.MakeDataChangesOnServer();

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
            //DownloadOnly.
            //<snippetOCS_CS_BasicCT_CustomerSyncTable>
            SyncTable customerSyncTable = new SyncTable("Customer");
            customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            customerSyncTable.SyncDirection = SyncDirection.DownloadOnly;
            this.Configuration.SyncTables.Add(customerSyncTable);
            //</snippetOCS_CS_BasicCT_CustomerSyncTable>
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
            //<snippetOCS_CS_BasicCT_NewAnchorCommand>
            SqlCommand selectNewAnchorCommand = new SqlCommand();
            string newAnchorVariable = "@" + SyncSession.SyncNewReceivedAnchor;
            selectNewAnchorCommand.CommandText =
                "SELECT " + newAnchorVariable + " = change_tracking_current_version()";
            selectNewAnchorCommand.Parameters.Add(newAnchorVariable, SqlDbType.BigInt);
            selectNewAnchorCommand.Parameters[newAnchorVariable].Direction = ParameterDirection.Output;
            selectNewAnchorCommand.Connection = serverConn;
            this.SelectNewAnchorCommand = selectNewAnchorCommand;
            //</snippetOCS_CS_BasicCT_NewAnchorCommand>


            //Create a SyncAdapter for the Customer table by using 
            //the SqlSyncAdapterBuilder:
            //  * Specify the base table name.
            //  * Specify that the server uses SQL Server change tracking.
            //  * Specify download-only synchronization.
            //  * Call ToSyncAdapter to create the SyncAdapter.
            //  * Specify a name for the SyncAdapter that matches the
            //    the name specified for the corresponding SyncTable.
            //    Do not include the schema names (Sales in this case).

            //<snippetOCS_CS_BasicCT_CustomerAdapterBuilder>
            SqlSyncAdapterBuilder customerBuilder = new SqlSyncAdapterBuilder(serverConn);

            customerBuilder.TableName = "Sales.Customer";
            customerBuilder.ChangeTrackingType = ChangeTrackingType.SqlServerChangeTracking;

            SyncAdapter customerSyncAdapter = customerBuilder.ToSyncAdapter();
            customerSyncAdapter.TableName = "Customer";
            this.SyncAdapters.Add(customerSyncAdapter);
            //</snippetOCS_CS_BasicCT_CustomerAdapterBuilder>

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

            //<snippetOCS_CS_BasicCT_Statistics>
            Console.WriteLine("Start Time: " + syncStatistics.SyncStartTime);
            Console.WriteLine("Total Changes Downloaded: " + syncStatistics.TotalChangesDownloaded);
            Console.WriteLine("Complete Time: " + syncStatistics.SyncCompleteTime);
            Console.WriteLine(String.Empty);
            //</snippetOCS_CS_BasicCT_Statistics>

        }
    }

    public class Utility
    {

        private static string _clientPassword;

        //Get and set the client database password.
        public static string Password
        {
            get { return _clientPassword; }
            set { _clientPassword = value; }
        }

        //Have the user enter a password for the client database file.
        public void SetClientPassword()
        {
            Console.WriteLine("Type a strong password for the client");
            Console.WriteLine("database, and then press Enter.");
            Utility.Password = Console.ReadLine();
        }

        //Return the client connection string with the password.
        public string ClientConnString
        {
            get { return @"Data Source='SyncSampleClient.sdf'; Password=" + Utility.Password; }
        }

        //Return the server connection string. 
        public string ServerConnString
        {

            get { return @"Data Source=localhost; Initial Catalog=SyncSamplesDb_ChangeTracking; Integrated Security=True"; }

        }

        //Make server changes that are synchronized on the second 
        //synchronization.
        public void MakeDataChangesOnServer()
        {
            int rowCount = 0;

            using (SqlConnection serverConn = new SqlConnection(this.ServerConnString))
            {
                SqlCommand sqlCommand = serverConn.CreateCommand();
                sqlCommand.CommandText =
                    "INSERT INTO Sales.Customer (CustomerName, SalesPerson, CustomerType) " +
                    "VALUES ('Cycle Mart', 'James Bailey', 'Retail') " +
                    
                    "UPDATE Sales.Customer " +
                    "SET  SalesPerson = 'James Bailey' " +
                    "WHERE CustomerName = 'Tandem Bicycle Store' " +

                    "DELETE FROM Sales.Customer WHERE CustomerName = 'Sharp Bikes'"; 
                
                serverConn.Open();
                rowCount = sqlCommand.ExecuteNonQuery();
                serverConn.Close();
            }

            Console.WriteLine("Rows inserted, updated, or deleted at the server: " + rowCount);
        }

        //Revert changes that were made during synchronization.
        public void CleanUpServer()
        {          
            using(SqlConnection serverConn = new SqlConnection(this.ServerConnString))
            {
                SqlCommand sqlCommand = serverConn.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "usp_InsertSampleData";
                
                serverConn.Open();               
                sqlCommand.ExecuteNonQuery();
                serverConn.Close();
            }
        }

        //Delete the client database.
        public void RecreateClientDatabase()
        {
            using (SqlCeConnection clientConn = new SqlCeConnection(this.ClientConnString))
            {
                if (File.Exists(clientConn.Database))
                {
                    File.Delete(clientConn.Database);
                }
            }
                
            SqlCeEngine sqlCeEngine = new SqlCeEngine(this.ClientConnString);
            sqlCeEngine.CreateDatabase();
        }
    }
}
//</snippetOCS_CS_BasicCT>