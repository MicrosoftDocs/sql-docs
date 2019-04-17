//<snippetOCS_CS_ChangeTracking>
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

            //Specify which server and database to connect to.
            util.SetServerAndDb("localhost", "SyncSamplesDb_ChangeTracking");
            
            //Initial synchronization. Instantiate the SyncAgent
            //and call Synchronize.
            SampleSyncAgent sampleSyncAgent = new SampleSyncAgent();
            SyncStatistics syncStatistics = sampleSyncAgent.Synchronize();
            sampleStats.DisplayStats(syncStatistics, "initial");

            //Make changes on the server and client.
            util.MakeDataChangesOnServer("Customer");
            util.MakeDataChangesOnClient("Customer");          

            //Subsequent synchronization.
            syncStatistics = sampleSyncAgent.Synchronize();
            sampleStats.DisplayStats(syncStatistics, "subsequent");

            //Make conflicting changes on the server and client.
            util.MakeConflictingChangesOnClientAndServer();

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
    //<snippetOCS_CS_ChangeTracking_SampleSyncAgent>
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
            //Bidirectional, and that an existing table should be dropped.
            //<snippetOCS_CS_ChangeTracking_CustomerSyncTable>
            SyncTable customerSyncTable = new SyncTable("Customer");
            customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            customerSyncTable.SyncDirection = SyncDirection.Bidirectional;
            this.Configuration.SyncTables.Add(customerSyncTable);
            //</snippetOCS_CS_ChangeTracking_CustomerSyncTable>
        }
    }
    //</snippetOCS_CS_ChangeTracking_SampleSyncAgent>

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
            //the server. In this case, we use a BigInt value
            //from the change tracking table.
            //During each synchronization, the new anchor value and
            //the last anchor value from the previous synchronization
            //are used: the set of changes between these upper and
            //lower bounds is synchronized.
            //
            //SyncSession.SyncNewReceivedAnchor is a string constant; 
            //you could also use @sync_new_received_anchor directly in 
            //your queries.
            //<snippetOCS_CS_ChangeTracking_NewAnchorCommand>
            SqlCommand selectNewAnchorCommand = new SqlCommand();
            string newAnchorVariable = "@" + SyncSession.SyncNewReceivedAnchor;
            selectNewAnchorCommand.CommandText =
                "SELECT " + newAnchorVariable + " = change_tracking_current_version()";
            selectNewAnchorCommand.Parameters.Add(newAnchorVariable, SqlDbType.BigInt);
            selectNewAnchorCommand.Parameters[newAnchorVariable].Direction = ParameterDirection.Output;
            selectNewAnchorCommand.Connection = serverConn;
            this.SelectNewAnchorCommand = selectNewAnchorCommand;
            //</snippetOCS_CS_ChangeTracking_NewAnchorCommand>
            
            //Create a SyncAdapter for the Customer table, and then define
            //the commands to synchronize changes:
            //* SelectIncrementalInsertsCommand, SelectIncrementalUpdatesCommand,
            //  and SelectIncrementalDeletesCommand are used to select changes
            //  from the server that the client provider then applies to the client.
            //* InsertCommand, UpdateCommand, and DeleteCommand are used to apply
            //  to the server the changes that the client provider has selected
            //  from the client.
            //* SelectConflictUpdatedRowsCommand SelectConflictDeletedRowsCommand
            //  are used to detect if there are conflicts on the server during
            //  synchronization.
            //The commands reference the change tracking table that is configured
            //for the Customer table.

            //Create the SyncAdapter.
            SyncAdapter customerSyncAdapter = new SyncAdapter("Customer");            
            
            //Select inserts from the server.
            //<snippetOCS_CS_ChangeTracking_CustomerIncrInsert>
            SqlCommand customerIncrInserts = new SqlCommand();
            customerIncrInserts.CommandText =
                "IF @sync_initialized = 0 " +
                    "SELECT Sales.Customer.[CustomerId], [CustomerName], [SalesPerson], [CustomerType] " +
                    "FROM Sales.Customer LEFT OUTER JOIN " +
                    "CHANGETABLE(CHANGES Sales.Customer, @sync_last_received_anchor) CT " +
                    "ON CT.[CustomerId] = Sales.Customer.[CustomerId] " +
                    "WHERE (CT.SYS_CHANGE_CONTEXT IS NULL OR CT.SYS_CHANGE_CONTEXT <> @sync_client_id_binary) " +
                "ELSE  " +
                "BEGIN " +
                    "SELECT Sales.Customer.[CustomerId], [CustomerName], [SalesPerson], [CustomerType] " +
                    "FROM Sales.Customer JOIN CHANGETABLE(CHANGES Sales.Customer, @sync_last_received_anchor) CT " +
                    "ON CT.[CustomerId] = Sales.Customer.[CustomerId] " +
                    "WHERE (CT.SYS_CHANGE_OPERATION = 'I' AND CT.SYS_CHANGE_CREATION_VERSION " +
                    "<= @sync_new_received_anchor " +
                    "AND (CT.SYS_CHANGE_CONTEXT IS NULL OR CT.SYS_CHANGE_CONTEXT <> @sync_client_id_binary)); " +
                    "IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(@sync_table_name)) " +
                    "> @sync_last_received_anchor " +
                    "RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. " +
                    "To recover from this error, the client must reinitialize its local database and try again' " +
                    ",16,3,@sync_table_name)  " +
                "END";
            customerIncrInserts.Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Int);
            customerIncrInserts.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.BigInt);
            customerIncrInserts.Parameters.Add("@" + SyncSession.SyncClientIdBinary, SqlDbType.Binary);
            customerIncrInserts.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.BigInt);
            customerIncrInserts.Parameters.Add("@" + SyncSession.SyncTableName, SqlDbType.NVarChar);
            customerIncrInserts.Connection = serverConn;
            customerSyncAdapter.SelectIncrementalInsertsCommand = customerIncrInserts;
            //</snippetOCS_CS_ChangeTracking_CustomerIncrInsert>

            //Apply inserts to the server.
            SqlCommand customerInserts = new SqlCommand();
            customerInserts.CommandText =
                ";WITH CHANGE_TRACKING_CONTEXT (@sync_client_id_binary) " +
                "INSERT INTO Sales.Customer ([CustomerId], [CustomerName], [SalesPerson], [CustomerType]) " +
                "VALUES (@CustomerId, @CustomerName, @SalesPerson, @CustomerType) " +
                "SET @sync_row_count = @@rowcount; " +
                "IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(@sync_table_name)) > @sync_last_received_anchor " +
                    "RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. " +
                    "To recover from this error, the client must reinitialize its local database and try again'" +
                    ",16,3,@sync_table_name)";
            customerInserts.Parameters.Add("@" + SyncSession.SyncClientIdBinary, SqlDbType.Binary);
            customerInserts.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            customerInserts.Parameters.Add("@CustomerName", SqlDbType.NVarChar);
            customerInserts.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
            customerInserts.Parameters.Add("@CustomerType", SqlDbType.NVarChar);
            customerInserts.Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int);
            customerInserts.Parameters["@" + SyncSession.SyncRowCount].Direction = ParameterDirection.Output;
            customerInserts.Parameters.Add("@" + SyncSession.SyncTableName, SqlDbType.NVarChar);
            customerInserts.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.BigInt);
            customerInserts.Connection = serverConn;
            customerSyncAdapter.InsertCommand = customerInserts;
                                    
            //Select updates from the server.
            //<snippetOCS_CS_ChangeTracking_CustomerIncrUpdate>
            SqlCommand customerIncrUpdates = new SqlCommand();
            customerIncrUpdates.CommandText =
                "IF @sync_initialized > 0  " +
                "BEGIN " +
                    "SELECT Sales.Customer.[CustomerId], [CustomerName], [SalesPerson], [CustomerType] " +
                    "FROM Sales.Customer JOIN " +
                    "CHANGETABLE(CHANGES Sales.Customer, @sync_last_received_anchor) CT " +
                    "ON CT.[CustomerId] = Sales.Customer.[CustomerId] " +
                    "WHERE (CT.SYS_CHANGE_OPERATION = 'U' AND CT.SYS_CHANGE_VERSION " +
                    "<= @sync_new_received_anchor " +
                    "AND (CT.SYS_CHANGE_CONTEXT IS NULL OR CT.SYS_CHANGE_CONTEXT <> @sync_client_id_binary)); " +
                    "IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(@sync_table_name)) " +
                    "> @sync_last_received_anchor " +
                    "RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. " +
                    "To recover from this error, the client must reinitialize its local database and try again'" +
                    ",16,3,@sync_table_name)  " +
                "END";
            customerIncrUpdates.Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Int);
            customerIncrUpdates.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.BigInt);
            customerIncrUpdates.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.BigInt);
            customerIncrUpdates.Parameters.Add("@" + SyncSession.SyncClientIdBinary, SqlDbType.Binary);
            customerIncrUpdates.Parameters.Add("@" + SyncSession.SyncTableName, SqlDbType.NVarChar);
            customerIncrUpdates.Connection = serverConn;
            customerSyncAdapter.SelectIncrementalUpdatesCommand = customerIncrUpdates;
            //</snippetOCS_CS_ChangeTracking_CustomerIncrUpdate>
            
            //Apply updates to the server.
            //<snippetOCS_CS_ChangeTracking_CustomerUpdate>
            SqlCommand customerUpdates = new SqlCommand();
            customerUpdates.CommandText =
                ";WITH CHANGE_TRACKING_CONTEXT (@sync_client_id_binary) " +
                "UPDATE Sales.Customer " +
                "SET [CustomerName] = @CustomerName, [SalesPerson] = @SalesPerson, [CustomerType] = @CustomerType " +
                "FROM Sales.Customer  " +
                "JOIN CHANGETABLE(VERSION Sales.Customer, ([CustomerId]), (@CustomerId)) CT  " +
                "ON CT.[CustomerId] = Sales.Customer.[CustomerId] " +
                "WHERE (@sync_force_write = 1 " +
                "OR CT.SYS_CHANGE_VERSION IS NULL OR CT.SYS_CHANGE_VERSION <= @sync_last_received_anchor " +
                "OR (CT.SYS_CHANGE_CONTEXT IS NOT NULL AND CT.SYS_CHANGE_CONTEXT = @sync_client_id_binary)) " +
                "SET @sync_row_count = @@rowcount; " +
                "IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(@sync_table_name)) > @sync_last_received_anchor " +
                    "RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. " +
                    "To recover from this error, the client must reinitialize its local database and try again'" +
                    ",16,3,@sync_table_name)";
            customerUpdates.Parameters.Add("@" + SyncSession.SyncClientIdBinary, SqlDbType.Binary); 
            customerUpdates.Parameters.Add("@CustomerName", SqlDbType.NVarChar);
            customerUpdates.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
            customerUpdates.Parameters.Add("@CustomerType", SqlDbType.NVarChar);            
            customerUpdates.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            customerUpdates.Parameters.Add("@" + SyncSession.SyncForceWrite, SqlDbType.Bit);
            customerUpdates.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.BigInt);
            customerUpdates.Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int);
            customerUpdates.Parameters["@" + SyncSession.SyncRowCount].Direction = ParameterDirection.Output;
            customerUpdates.Parameters.Add("@" + SyncSession.SyncTableName, SqlDbType.NVarChar);
            customerUpdates.Connection = serverConn;
            customerSyncAdapter.UpdateCommand = customerUpdates;
            //</snippetOCS_CS_ChangeTracking_CustomerUpdate>

            //Select deletes from the server.
            SqlCommand customerIncrDeletes = new SqlCommand();
            customerIncrDeletes.CommandText =
                "IF @sync_initialized > 0  " +
                "BEGIN " +
                    "SELECT CT.[CustomerId] FROM CHANGETABLE(CHANGES Sales.Customer, @sync_last_received_anchor) CT " +
                    "WHERE (CT.SYS_CHANGE_OPERATION = 'D' AND CT.SYS_CHANGE_VERSION " +
                    "<= @sync_new_received_anchor " +
                    "AND (CT.SYS_CHANGE_CONTEXT IS NULL OR CT.SYS_CHANGE_CONTEXT <> @sync_client_id_binary)); " +
                    "IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(@sync_table_name)) " +
                    "> @sync_last_received_anchor " +
                    "RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. " +
                    "To recover from this error, the client must reinitialize its local database and try again'" +
                    ",16,3,@sync_table_name)  " +
                "END";
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Int);
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.BigInt);
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.BigInt);
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncClientIdBinary, SqlDbType.Binary);
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncTableName, SqlDbType.NVarChar);
            customerIncrDeletes.Connection = serverConn;
            customerSyncAdapter.SelectIncrementalDeletesCommand = customerIncrDeletes;

            //Apply deletes to the server.            
            SqlCommand customerDeletes = new SqlCommand();
            customerDeletes.CommandText =
                ";WITH CHANGE_TRACKING_CONTEXT (@sync_client_id_binary) " +
                "DELETE Sales.Customer FROM Sales.Customer " +
                "JOIN CHANGETABLE(VERSION Sales.Customer, ([CustomerId]), (@CustomerId)) CT  " +
                "ON CT.[CustomerId] = Sales.Customer.[CustomerId] " +
                "WHERE (@sync_force_write = 1 " +
                "OR CT.SYS_CHANGE_VERSION IS NULL OR CT.SYS_CHANGE_VERSION <= @sync_last_received_anchor " +
                "OR (CT.SYS_CHANGE_CONTEXT IS NOT NULL AND CT.SYS_CHANGE_CONTEXT = @sync_client_id_binary)) " +
                "SET @sync_row_count = @@rowcount; " +
                "IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(@sync_table_name)) > @sync_last_received_anchor " +
                    "RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. " +
                    "To recover from this error, the client must reinitialize its local database and try again'" +
                    ",16,3,@sync_table_name)";
            customerDeletes.Parameters.Add("@" + SyncSession.SyncClientIdBinary, SqlDbType.Binary);
            customerDeletes.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            customerDeletes.Parameters.Add("@" + SyncSession.SyncForceWrite, SqlDbType.Bit);
            customerDeletes.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.BigInt);           
            customerDeletes.Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int);
            customerDeletes.Parameters["@" + SyncSession.SyncRowCount].Direction = ParameterDirection.Output;
            customerDeletes.Parameters.Add("@" + SyncSession.SyncTableName, SqlDbType.NVarChar);
            customerDeletes.Connection = serverConn;
            customerSyncAdapter.DeleteCommand = customerDeletes;
           
            //This command is used if @sync_row_count returns
            //0 when changes are applied to the server.
            //<snippetOCS_CS_ChangeTracking_SelectConflictUpdatedRowsCommand>
            SqlCommand customerUpdateConflicts = new SqlCommand();
            customerUpdateConflicts.CommandText =
                "SELECT Sales.Customer.[CustomerId], [CustomerName], [SalesPerson], [CustomerType], " +
                "CT.SYS_CHANGE_CONTEXT, CT.SYS_CHANGE_VERSION " +
                "FROM Sales.Customer JOIN CHANGETABLE(VERSION Sales.Customer, ([CustomerId]), (@CustomerId)) CT  " +
                "ON CT.[CustomerId] = Sales.Customer.[CustomerId]";
            customerUpdateConflicts.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            customerUpdateConflicts.Connection = serverConn;
            customerSyncAdapter.SelectConflictUpdatedRowsCommand = customerUpdateConflicts;
            //</snippetOCS_CS_ChangeTracking_SelectConflictUpdatedRowsCommand>

            //This command is used if the server provider cannot find
            //a row in the base table.
            //<snippetOCS_CS_ChangeTracking_SelectConflictDeletedRowsCommand>
            SqlCommand customerDeleteConflicts = new SqlCommand();
            customerDeleteConflicts.CommandText =
                "SELECT CT.[CustomerId], " +
                "CT.SYS_CHANGE_CONTEXT, CT.SYS_CHANGE_VERSION " +
                "FROM CHANGETABLE(CHANGES Sales.Customer, @sync_last_received_anchor) CT " +
                "WHERE (CT.[CustomerId] = @CustomerId AND CT.SYS_CHANGE_OPERATION = 'D')";
            customerDeleteConflicts.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.BigInt);
            customerDeleteConflicts.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            customerDeleteConflicts.Connection = serverConn;
            customerSyncAdapter.SelectConflictDeletedRowsCommand = customerDeleteConflicts;
            //</snippetOCS_CS_ChangeTracking_SelectConflictDeletedRowsCommand>


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

            //We use the CreatingSchema event to change the schema
            //by using the API. We use the SchemaCreated event to 
            //change the schema by using SQL.
            this.CreatingSchema +=new EventHandler<CreatingSchemaEventArgs>(SampleClientSyncProvider_CreatingSchema);
            this.SchemaCreated +=new EventHandler<SchemaCreatedEventArgs>(SampleClientSyncProvider_SchemaCreated);
        }

        private void SampleClientSyncProvider_CreatingSchema(object sender, CreatingSchemaEventArgs e)
        {
            //Set the RowGuid property because it is not copied
            //to the client by default. This is also a good time
            //to specify literal defaults with .Columns[ColName].DefaultValue;
            //but we will specify defaults like NEWID() by calling
            //ALTER TABLE after the table is created.
            Console.Write("Creating schema for " + e.Table.TableName + " | ");                        
            e.Schema.Tables["Customer"].Columns["CustomerId"].RowGuid = true;
        }

        private void SampleClientSyncProvider_SchemaCreated(object sender, SchemaCreatedEventArgs e)
        {        
            //Call ALTER TABLE on the client. This must be done
            //over the same connection and within the same
            //transaction that Synchronization Services uses
            //to create the schema on the client.
            Utility util = new Utility();
            util.MakeSchemaChangesOnClient(e.Connection, e.Transaction, e.Table.TableName);
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
            Console.WriteLine("Total Changes Uploaded: " + syncStatistics.TotalChangesUploaded);
            Console.WriteLine("Total Changes Downloaded: " + syncStatistics.TotalChangesDownloaded);
            Console.WriteLine("Complete Time: " + syncStatistics.SyncCompleteTime);
            Console.WriteLine(String.Empty);
        }
    }
}
//</snippetOCS_CS_ChangeTracking>