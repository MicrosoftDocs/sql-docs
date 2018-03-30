//<snippetOCS_CS_SessionVars>
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

            //Make changes on the server and client.
            util.MakeDataChangesOnServer("Vendor");
            util.MakeDataChangesOnClient("Vendor");

            //Subsequent synchronization.
            syncStatistics = sampleSyncAgent.Synchronize();
            sampleStats.DisplayStats(syncStatistics, "subsequent");

            //Return the server data back to its original state.
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

            //Add the Vendor table: specify a synchronization direction of
            //Bidirectional, and that an existing table should be dropped.
            SyncTable vendorSyncTable = new SyncTable("Vendor");
            vendorSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            vendorSyncTable.SyncDirection = SyncDirection.Bidirectional;
            this.Configuration.SyncTables.Add(vendorSyncTable);
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
            //<snippetOCS_CS_SessionVars_NewAnchorCommand>
            SqlCommand selectNewAnchorCommand = new SqlCommand();
            string newAnchorVariable = "@" + SyncSession.SyncNewReceivedAnchor;
            selectNewAnchorCommand.CommandText = 
                "SELECT " + newAnchorVariable + " = min_active_rowversion() - 1";
            selectNewAnchorCommand.Parameters.Add(newAnchorVariable, SqlDbType.Timestamp);
            selectNewAnchorCommand.Parameters[newAnchorVariable].Direction = ParameterDirection.Output;
            selectNewAnchorCommand.Connection = serverConn;
            this.SelectNewAnchorCommand = selectNewAnchorCommand;
            //</snippetOCS_CS_SessionVars_NewAnchorCommand>
            
            //Create a command that enables you to pass in a 
            //client ID (a GUID) and get back the orginator ID (an integer) 
            //that is defined in a mapping table on the server.
            //<snippetOCS_CS_SessionVars_ClientIdCommand>
            SqlCommand selectClientIdCommand = new SqlCommand();
            selectClientIdCommand.CommandType = CommandType.StoredProcedure;
            selectClientIdCommand.CommandText = "usp_GetOriginatorId";
            selectClientIdCommand.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            selectClientIdCommand.Parameters.Add("@" + SyncSession.SyncOriginatorId, SqlDbType.Int).Direction = ParameterDirection.Output;
            selectClientIdCommand.Connection = serverConn;
            this.SelectClientIdCommand = selectClientIdCommand;
            //</snippetOCS_CS_SessionVars_ClientIdCommand>

            //Create a SyncAdapter for the Vendor table, and then define
            //the commands to synchronize changes:
            //* SelectIncrementalInsertsCommand, SelectIncrementalUpdatesCommand,
            //  and SelectIncrementalDeletesCommand are used to select changes
            //  from the server that the client provider then applies to the client.
            //* InsertCommand, UpdateCommand, and DeleteCommand are used to apply
            //  to the server the changes that the client provider has selected
            //  from the client.

            //Create the SyncAdapter
            SyncAdapter vendorSyncAdapter = new SyncAdapter("Vendor");            
            
            //Select inserts from the server.
            //This command includes three session variables:
            //@sync_last_received_anchor, @sync_new_received_anchor,
            //and @sync_originator_id. The anchor variables are used with
            //SelectNewAnchorCommand to determine the set of changes to 
            //synchronize. In other example code, the commands use 
            //@sync_client_id instead of @sync_originator_id. In this case, 
            //@sync_originator_id is used because the SelectClientIdCommand 
            //is specified.
            SqlCommand vendorIncrInserts = new SqlCommand();
            vendorIncrInserts.CommandText =  
                "SELECT VendorId, VendorName, CreditRating, PreferredVendor " +
                "FROM Sales.Vendor " +
                "WHERE (InsertTimestamp > @sync_last_received_anchor " +
                "AND InsertTimestamp <= @sync_new_received_anchor " +
                "AND InsertId <> @sync_originator_id)";
            vendorIncrInserts.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            vendorIncrInserts.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            vendorIncrInserts.Parameters.Add("@" + SyncSession.SyncOriginatorId, SqlDbType.Int);
            vendorIncrInserts.Connection = serverConn;
            vendorSyncAdapter.SelectIncrementalInsertsCommand = vendorIncrInserts;

            //Apply inserts to the server.
            //This command includes @sync_row_count, which returns
            //a count of how many rows were affected by the
            //last database operation. In SQL Server, the variable
            //is assigned the value of @@rowcount. The count is used
            //to determine whether an operation was successful or
            //was unsuccessful due to a conflict or an error.
            SqlCommand vendorInserts = new SqlCommand();
            vendorInserts.CommandText =
                "INSERT INTO Sales.Vendor (VendorId, VendorName, CreditRating, PreferredVendor, InsertId, UpdateId) " +
                "VALUES (@VendorId, @VendorName, @CreditRating, @PreferredVendor, @sync_originator_id, @sync_originator_id) " +
                "SET @sync_row_count = @@rowcount";
            vendorInserts.Parameters.Add("@VendorId", SqlDbType.UniqueIdentifier);
            vendorInserts.Parameters.Add("@VendorName", SqlDbType.NVarChar);
            vendorInserts.Parameters.Add("@CreditRating", SqlDbType.NVarChar);
            vendorInserts.Parameters.Add("@PreferredVendor", SqlDbType.NVarChar);
            vendorInserts.Parameters.Add("@" + SyncSession.SyncOriginatorId, SqlDbType.Int);
            vendorInserts.Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int);
            vendorInserts.Connection = serverConn;
            vendorSyncAdapter.InsertCommand = vendorInserts;
                                    
            
            //Select updates from the server
            //<snippetOCS_CS_SessionVars_VendorIncrUpdate>
            SqlCommand vendorIncrUpdates = new SqlCommand();
            vendorIncrUpdates.CommandText =
                "SELECT VendorId, VendorName, CreditRating, PreferredVendor " +
                "FROM Sales.Vendor " +
                "WHERE (UpdateTimestamp > @sync_last_received_anchor " +
                "AND UpdateTimestamp <= @sync_new_received_anchor " +
                "AND UpdateId <> @sync_originator_id " +
                "AND NOT (InsertTimestamp > @sync_last_received_anchor " +
                "AND InsertId <> @sync_originator_id))";
            vendorIncrUpdates.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            vendorIncrUpdates.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            vendorIncrUpdates.Parameters.Add("@" + SyncSession.SyncOriginatorId, SqlDbType.Int);
            vendorIncrUpdates.Connection = serverConn;
            vendorSyncAdapter.SelectIncrementalUpdatesCommand = vendorIncrUpdates;
            //</snippetOCS_CS_SessionVars_VendorIncrUpdate>
            
            //Apply updates to the server.
            //This command includes @sync_force_write, which can
            //be used to apply changes in case of a conflict.
            //<snippetOCS_CS_SessionVars_VendorUpdate>
            SqlCommand vendorUpdates = new SqlCommand();
            vendorUpdates.CommandText =
                "UPDATE Sales.Vendor SET " + 
                "VendorName = @VendorName, CreditRating = @CreditRating, " + 
                "PreferredVendor = @PreferredVendor, " + 
                "UpdateId = @sync_originator_id " +           
                "WHERE (VendorId = @VendorId) " + 
                "AND (@sync_force_write = 1 " + 
                "OR (UpdateTimestamp <= @sync_last_received_anchor " +
                "OR UpdateId = @sync_originator_id)) " + 
                "SET @sync_row_count = @@rowcount";
            vendorUpdates.Parameters.Add("@VendorName", SqlDbType.NVarChar);
            vendorUpdates.Parameters.Add("@CreditRating", SqlDbType.NVarChar);
            vendorUpdates.Parameters.Add("@PreferredVendor", SqlDbType.NVarChar);
            vendorUpdates.Parameters.Add("@" + SyncSession.SyncOriginatorId, SqlDbType.Int);
            vendorUpdates.Parameters.Add("@VendorId", SqlDbType.UniqueIdentifier);
            vendorUpdates.Parameters.Add("@" + SyncSession.SyncForceWrite, SqlDbType.Bit);
            vendorUpdates.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            vendorUpdates.Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int);
            vendorUpdates.Connection = serverConn;
            vendorSyncAdapter.UpdateCommand = vendorUpdates;
            //</snippetOCS_CS_SessionVars_VendorUpdate>


            //Select deletes from the server.
            //This command includes @sync_initialized, which is
            //used to determine whether a client has been 
            //initialized already. If this variable returns 0,
            //this is the first synchronization for this client ID
            //or originator ID.
            //<snippetOCS_CS_SessionVars_VendorIncrDelete>
            SqlCommand vendorIncrDeletes = new SqlCommand();
            vendorIncrDeletes.CommandText =
                "SELECT VendorId, VendorName, CreditRating, PreferredVendor " +
                "FROM Sales.Vendor_Tombstone " +
                "WHERE (@sync_initialized = 1 " +
                "AND DeleteTimestamp > @sync_last_received_anchor " +
                "AND DeleteTimestamp <= @sync_new_received_anchor " +
                "AND DeleteId <> @sync_originator_id)";
            vendorIncrDeletes.Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Bit);
            vendorIncrDeletes.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            vendorIncrDeletes.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            vendorIncrDeletes.Parameters.Add("@" + SyncSession.SyncOriginatorId, SqlDbType.Int);
            vendorIncrDeletes.Connection = serverConn;
            vendorSyncAdapter.SelectIncrementalDeletesCommand = vendorIncrDeletes;
            //</snippetOCS_CS_SessionVars_VendorIncrDelete>

            //Apply deletes to the server.            
            SqlCommand vendorDeletes = new SqlCommand();
            vendorDeletes.CommandText =
                "DELETE FROM Sales.Vendor " +
                "WHERE (VendorId = @VendorId) " + 
                "AND (@sync_force_write = 1 " + 
                "OR (UpdateTimestamp <= @sync_last_received_anchor " + 
                "OR UpdateId = @sync_originator_id)) " + 
                "SET @sync_row_count = @@rowcount " + 
                "IF (@sync_row_count > 0)  BEGIN " + 
                "UPDATE Sales.Vendor_Tombstone " + 
                "SET DeleteId = @sync_originator_id " +
                "WHERE (VendorId = @VendorId) " + 
                "END";
            vendorDeletes.Parameters.Add("@VendorId", SqlDbType.UniqueIdentifier);       
            vendorDeletes.Parameters.Add("@" + SyncSession.SyncForceWrite, SqlDbType.Bit);
            vendorDeletes.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            vendorDeletes.Parameters.Add("@" + SyncSession.SyncOriginatorId, SqlDbType.Int);
            vendorDeletes.Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int);           
            vendorDeletes.Connection = serverConn;
            vendorSyncAdapter.DeleteCommand = vendorDeletes;     


            //Add the SyncAdapter to the server synchronization provider.
            this.SyncAdapters.Add(vendorSyncAdapter);

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
            //to specify literal defaults with .Columns[ColName].DefaultValue,
            //but we will specify defaults like NEWID() by calling
            //ALTER TABLE after the table is created.
            Console.Write("Creating schema for " + e.Table.TableName + " | ");                        
            e.Schema.Tables["Vendor"].Columns["VendorId"].RowGuid = true;
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
//</snippetOCS_CS_SessionVars>





