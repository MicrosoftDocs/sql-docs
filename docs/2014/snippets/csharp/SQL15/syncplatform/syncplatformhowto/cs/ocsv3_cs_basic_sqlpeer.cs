//<snippetOCSv3_CS_Basic_SqlPeer>
using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization.Data.SqlServerCe;

namespace Microsoft.Samples.Synchronization
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create the connections over which provisioning and sycnhronization
            // are performed. The Utility class handles all functionality that is not
            //directly related to synchronization, such as holding connection 
            //string information and making changes to the server database.
            SqlConnection serverConn = new SqlConnection(Utility.ServerConnString);
            SqlConnection clientSqlConn = new SqlConnection(Utility.ClientSqlConnString);
            SqlCeConnection clientSqlCeConn = new SqlCeConnection(Utility.ClientSqlCeConnString);

            // Create a scope named "filtered_customer", and add two tables to the scope.
            // GetDescriptionForTable gets the schema of each table, so that tracking 
            // tables and triggers can be created for that table.
            //<snippetOCSv3_CS_Basic_SqlPeer_ScopeDesc>
            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription("filtered_customer");

            scopeDesc.Tables.Add(
            SqlSyncDescriptionBuilder.GetDescriptionForTable("Customer", serverConn));
            scopeDesc.Tables.Add(
            SqlSyncDescriptionBuilder.GetDescriptionForTable("CustomerContact", serverConn));
            //</snippetOCSv3_CS_Basic_SqlPeer_ScopeDesc>

            // Create a provisioning object for "filtered_customer" and specify that
            // base tables should not be created (They already exist in SyncSamplesDb_SqlPeer1).
            //<snippetOCSv3_CS_Basic_SqlPeer_ServerConfig>
            SqlSyncScopeProvisioning serverConfig = new SqlSyncScopeProvisioning(scopeDesc);
            serverConfig.CreateTableDefault = DbSyncCreationOption.Skip;

            // Specify which column(s) in the Customer table to use for filtering data, 
            // and the filtering clause to use against the tracking table.
            // "[side]" is an alias for the tracking table.
            serverConfig["Customer"].AddFilterColumn("CustomerType");
            serverConfig["Customer"].FilterClause = "[side].[CustomerType] = 'Retail'";

            // Configure the scope and change tracking infrastructure.
            serverConfig.Apply(serverConn);

            // Write the configuration script to a file. You can modify 
            // this script if necessary and run it against the server
            // to customize behavior.
            File.WriteAllText("SampleConfigScript.txt", 
                serverConfig.Script("SyncSamplesDb_SqlPeer1"));
            //</snippetOCSv3_CS_Basic_SqlPeer_ServerConfig>


            // Retrieve scope information from the server and use the schema that is retrieved
            // to provision the SQL Server and SQL Server Compact client databases.           

            //<snippetOCSv3_CS_Basic_SqlPeer_ClientConfig>
            // This database already exists on the server.
            DbSyncScopeDescription clientSqlDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope("filtered_customer", serverConn);
            SqlSyncScopeProvisioning clientSqlConfig = new SqlSyncScopeProvisioning(clientSqlDesc);
            clientSqlConfig.Apply(clientSqlConn);

            // This database does not yet exist.
            Utility.CreateSqlCeDatabase();
            DbSyncScopeDescription clientSqlCeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope("filtered_customer", serverConn);
            SqlCeSyncScopeProvisioning clientSqlCeConfig = new SqlCeSyncScopeProvisioning(clientSqlCeDesc);
            clientSqlCeConfig.Apply(clientSqlCeConn);
            //</snippetOCSv3_CS_Basic_SqlPeer_ClientConfig>


            // Initial synchronization sessions. 7 rows are synchronized:
            // all rows (4) from CustomerContact, and the 3 rows from Customer 
            // that satisfy the filtering criteria.
            //<snippetOCSv3_CS_Basic_SqlPeer_InitialSync>
            SampleSyncOrchestrator syncOrchestrator;
            SyncOperationStatistics syncStats;

            // Data is downloaded from the server to the SQL Server client.
            syncOrchestrator = new SampleSyncOrchestrator(
                new SqlSyncProvider("filtered_customer", clientSqlConn),
                new SqlSyncProvider("filtered_customer", serverConn)
                );
            syncStats = syncOrchestrator.Synchronize();
            syncOrchestrator.DisplayStats(syncStats, "initial");

            // Data is downloaded from the SQL Server client to the 
            // SQL Server Compact client.
            syncOrchestrator = new SampleSyncOrchestrator(
                new SqlCeSyncProvider("filtered_customer", clientSqlCeConn),
                new SqlSyncProvider("filtered_customer", clientSqlConn)
                );
            syncStats = syncOrchestrator.Synchronize();
            syncOrchestrator.DisplayStats(syncStats, "initial");
            //</snippetOCSv3_CS_Basic_SqlPeer_InitialSync>


            // Make changes on the server: 1 insert, 1 update, and 1 delete.
            Utility.MakeDataChangesOnServer();
                      
            
            // Synchronize again. Three changes were made on the server, but
            // only two of them applied to rows that are in the "filtered_customer"
            // scope. The other row is not synchronized.
            // Notice that the order of synchronization is different from the initial
            // sessions, but the two changes are propagated to all nodes.
            syncOrchestrator = new SampleSyncOrchestrator(
                new SqlCeSyncProvider("filtered_customer", clientSqlCeConn),
                new SqlSyncProvider("filtered_customer", serverConn)
                );
            syncStats = syncOrchestrator.Synchronize();
            syncOrchestrator.DisplayStats(syncStats, "subsequent");

            syncOrchestrator = new SampleSyncOrchestrator(
                new SqlSyncProvider("filtered_customer", clientSqlConn),
                new SqlCeSyncProvider("filtered_customer", clientSqlCeConn)
                );
            syncStats = syncOrchestrator.Synchronize();
            syncOrchestrator.DisplayStats(syncStats, "subsequent");

            serverConn.Close();
            serverConn.Dispose();
            clientSqlConn.Close();
            clientSqlConn.Dispose();
            clientSqlCeConn.Close();
            clientSqlCeConn.Dispose();

            Console.Write("\nPress any key to exit.");
            Console.Read();
            
        }

    }

    public class SampleSyncOrchestrator : SyncOrchestrator
    {
        //<snippetOCSv3_CS_Basic_SqlPeer_SampleSyncOrchestrator>
        public SampleSyncOrchestrator(RelationalSyncProvider localProvider, RelationalSyncProvider remoteProvider)
        {

            this.LocalProvider = localProvider;
            this.RemoteProvider = remoteProvider;
            this.Direction = SyncDirectionOrder.UploadAndDownload;
        }
        //</snippetOCSv3_CS_Basic_SqlPeer_SampleSyncOrchestrator>

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



    public class Utility
    {

        //Return connection strings

        public static string ServerConnString
        {

            get { return @"Data Source=localhost; Initial Catalog=SyncSamplesDb_SqlPeer1; Integrated Security=True"; }

        }
        
        public static string ClientSqlConnString
        {

            get { return "Data Source=localhost; Initial Catalog=SyncSamplesDb_SqlPeer2; Integrated Security=True"; }

        }

        public static string ClientSqlCeConnString
        {
            get { return @"Data Source='SyncSampleClient.sdf'"; }
        }


        //Make server changes that are synchronized on the second 
        //synchronization.
        public static void MakeDataChangesOnServer()
        {
            int rowCount = 0;

            using (SqlConnection serverConn = new SqlConnection(Utility.ServerConnString))
            {
                SqlCommand sqlCommand = serverConn.CreateCommand();
                sqlCommand.CommandText =
                    "INSERT INTO Customer (CustomerName, SalesPerson, CustomerType) " +
                    "VALUES ('Cycle Mart', 'James Bailey', 'Retail') " +
                    
                    "UPDATE Customer " +
                    "SET  SalesPerson = 'James Bailey' " +
                    "WHERE CustomerName = 'Tandem Bicycle Store' " +

                    "DELETE FROM Customer WHERE CustomerName = 'Sharp Bikes'"; 
                
                serverConn.Open();
                rowCount = sqlCommand.ExecuteNonQuery();
                serverConn.Close();
            }

            // Row count is divided by 2 because of inserts to tracking tables.
            Console.WriteLine("Rows inserted, updated, or deleted at the server: " + rowCount / 2);
        }

        public static void CreateSqlCeDatabase()
        {
            using (SqlCeConnection clientConn = new SqlCeConnection(Utility.ClientSqlCeConnString))
            {
                if (File.Exists(clientConn.Database))
                {
                    File.Delete(clientConn.Database);
                }
            }

            SqlCeEngine sqlCeEngine = new SqlCeEngine(Utility.ClientSqlCeConnString);
            sqlCeEngine.CreateDatabase();
        }
    }
}
//</snippetOCSv3_CS_Basic_SqlPeer>


//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////


/*
 * 
 *         //Revert changes that were made during synchronization.
public void CleanUpServer()
{
using (SqlConnection serverConn = new SqlConnection(Utility.ServerConnString))
{
    SqlCommand sqlCommand = serverConn.CreateCommand();
    sqlCommand.CommandType = CommandType.StoredProcedure;
    sqlCommand.CommandText = "usp_ResetSqlPeerData";
                
    serverConn.Open();               
    sqlCommand.ExecuteNonQuery();
    serverConn.Close();
}
}
SqlConnection serverConn = new SqlConnection(serverBld.ToString());
            
// Get the schema of each table, so that tracking tables and
// triggers can be created. Set options that specify the following:
//  * Don't create base tables because they already exist.
//  * Don't create procedures b/c they are scope-specific, and they
//    will be created in the next code block.
//  * Populate the tracking table with metadata about the rows that
//    already exist in the base table.

// Customer table
DbSyncTableDescription customerDescription = 
    SqlSyncDescriptionBuilder.GetDescriptionForTable("Customer", serverConn);
SqlSyncTableProvisioning customerProvisioning = 
    new SqlSyncTableProvisioning(customerDescription);                    
customerProvisioning.CreateTable = DbSyncCreationOption.Skip;
customerProvisioning.CreateProcedures = DbSyncCreationOption.Skip;
customerProvisioning.PopulateTrackingTable = DbSyncCreationOption.Create;         
customerProvisioning.Apply(serverConn);

// CustomerContact table
DbSyncTableDescription customerContactDescription = 
    SqlSyncDescriptionBuilder.GetDescriptionForTable("CustomerContact", serverConn);
SqlSyncTableProvisioning customerContactProvisioning = 
    new SqlSyncTableProvisioning(customerContactDescription);                    
customerContactProvisioning.CreateTable = DbSyncCreationOption.Skip;
customerContactProvisioning.CreateProcedures = DbSyncCreationOption.Skip;
customerContactProvisioning.PopulateTrackingTable = DbSyncCreationOption.Create;
customerContactProvisioning.Apply(serverConn);

// Create two scopes:
// * Scope "all_data" includes all data from both tables.
// * Scope "filtered_data" includes only retail data from "Customer".
DbSyncScopeDescription scopeDescription;
SqlSyncScopeProvisioning scopeProvisioning;
            
scopeDescription = new DbSyncScopeDescription("all_data");
scopeProvisioning = new SqlSyncScopeProvisioning(scopeDescription);

            
scopeDescription = new DbSyncScopeDescription("all_data");
            
customerProvisioning.AddFilterColumn("CustomerTypes");

                
    .ScopeName = ""
            
            
            
            
            
/*/
/*/



// Specify tables, and filtering columns for those tables.
Dictionary<string, string> tables = new Dictionary<string, string>();
tables.Add("Customer", "CustomerType");
tables.Add("CustomerContact", null);

foreach (KeyValuePair<string, string> table in tables)
{
 // Get the schema of the table, so that tracking tables and
 // triggers can be created. Set options that specify the following:
 //  * Don't create base tables because they already exist.
 //  * Don't create procedures b/c they are scope-specific, and they
 //    will be created in the next code block.
 //  * Populate the tracking table with metadata about the rows that
 //    already exist in the base table.
 customerDescription = SqlSyncDescriptionBuilder.GetDescriptionForTable(table.Key, serverConn);
 tableProvisioning = new SqlSyncTableProvisioning(customerDescription);                    
 tableProvisioning.CreateTable = DbSyncCreationOption.Skip;
 tableProvisioning.CreateProcedures = DbSyncCreationOption.Skip;
 tableProvisioning.PopulateTrackingTable = DbSyncCreationOption.Create;
                
 // Specify which column(s) will be used for filtering.
 if (table.Key != null)
 {
     tableProvisioning.AddFilterColumn(table.Key);
 }
                
 // Create objects in the server database.
 tableProvisioning.Apply(serverConn);
}

scopeDescription = new DbSyncScopeDescription("ScopeOne");
scopeDescription.Tables.Add(
            
serverConn.Close();
serverConn.Dispose();*/



            /*public class SampleServer
            {
                            public void EnableChangeTracking{}

                            public void CreateScopes{}
            }*/
            
        //= new SqlSyncTableProvisioning(;

        /*Utility util = new Utility();

        SampleSyncProvider sampleSyncProvider = new SampleSyncProvider();
        SyncOrchestrator sampleSyncAgent;
        SyncOperationStatistics syncStatistics;
        SampleStats sampleStats = new SampleStats();

        sampleSyncAgent = new SampleSyncAgent(
                                sampleSyncProvider.ConfigureSqlSyncProvider(util.Peer1ConnString),
                                sampleSyncProvider.ConfigureSqlSyncProvider(util.Peer2ConnString));
        syncStatistics = sampleSyncAgent.Synchronize();
        sampleStats.DisplayStats(syncStatistics, "initial");*/

       



    /*public class SampleSyncAgent : SyncOrchestrator
    {
        public SampleSyncAgent(RelationalSyncProvider localProvider, RelationalSyncProvider remoteProvider)
        {

            this.LocalProvider = localProvider;
            this.RemoteProvider = remoteProvider;
            this.Direction = SyncDirectionOrder.UploadAndDownload;

        }
    }


    public class SampleSyncProvider
    {

        public SqlSyncProvider ConfigureSqlSyncProvider(string sqlConnString)
        {
            return sampleSqlProvider;
        }

        public SqlCeSyncProvider ConfigureSqlCeSyncProvider(string sqlCeConnString)
        {
            return sampleSqlCeProvider;
        }
    }

/*SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
            syncOrchestrator.LocalProvider = new SqlCeSyncProvider("filtered_customer", clientSqlCeConn);
            syncOrchestrator.RemoteProvider = new SqlSyncProvider("filtered_customer", serverConn);
            syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
            syncStats = syncOrchestrator.Synchronize();*/
            
            
            // Subsequent synchronization sessions.
            
            
            
            
            
            //SampleStats sampleStats = new SampleStats();
            
            /*
            syncOrchestrator.LocalProvider = new SqlSyncProvider("filtered_customer", conn);
            syncOrchestrator.RemoteProvider = new SqlSyncProvider("filtered_customer", serverConn);
            syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
            SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
            sampleStats.DisplayStats(syncStats, "initial");

            Console.Read();

            SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
            syncOrchestrator.LocalProvider = new SqlCeSyncProvider("filtered_customer", new SqlCeConnection(util.ClientConnString));
            syncOrchestrator.RemoteProvider = new SqlSyncProvider("filtered_customer", serverConn);
            syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
            syncStats = syncOrchestrator.Synchronize();
            sampleStats.DisplayStats(syncStats, "initial");

            Console.Read();*/

            /*
             * 
             * 
            util.CleanUpServer();

            SampleSyncOrchestrator sampleSyncOrchestrator;
            SyncOperationStatistics syncStatistics;
            SampleStats sampleStats = new SampleStats();
            Utility util = new Utility();

             * sampleSyncOrchestrator = new SampleSyncOrchestrator(
                new SqlSyncProvider("filtered_customer", new SqlConnection(util.Peer2ConnString)),
                new SqlSyncProvider("filtered_customer", new SqlConnection(util.Peer1ConnString)));
            syncStatistics = sampleSyncOrchestrator.Synchronize();
            sampleStats.DisplayStats(syncStatistics, "initial");
             * */

            //serverConn.Close();
            //serverConn.Dispose();
