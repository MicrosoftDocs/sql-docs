//<snippetOCS_CS_Events>
using System;
using System.Collections;
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

            //The SampleStatsAndProgress class handles information from the 
            //SyncStatistics object that the Synchronize method returns and
            //from SyncAgent events.
            SampleStatsAndProgress sampleStats = new SampleStatsAndProgress();

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
            util.MakeDataChangesOnServer("Customer");
            util.MakeDataChangesOnClient("Customer");

            //Subsequent synchronization.
            syncStatistics = sampleSyncAgent.Synchronize();
            sampleStats.DisplayStats(syncStatistics, "subsequent");

            //Make a change at the client that fails when it is
            //applied at the server.
            util.MakeFailingChangesOnClient();

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

            //Create two SyncGroups so that changes to OrderHeader
            //and OrderDetail are made in one transaction. Depending on
            //application requirements, you might include Customer
            //in the same group.
            SyncGroup customerSyncGroup = new SyncGroup("Customer");
            SyncGroup orderSyncGroup = new SyncGroup("Order");

            //Add each table: specify a synchronization direction of
            //Bidirectional.
            SyncTable customerSyncTable = new SyncTable("Customer");
            customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            customerSyncTable.SyncDirection = SyncDirection.Bidirectional;
            customerSyncTable.SyncGroup = customerSyncGroup;
            this.Configuration.SyncTables.Add(customerSyncTable);

            SyncTable orderHeaderSyncTable = new SyncTable("OrderHeader");
            orderHeaderSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            orderHeaderSyncTable.SyncDirection = SyncDirection.Bidirectional;
            orderHeaderSyncTable.SyncGroup = orderSyncGroup;
            this.Configuration.SyncTables.Add(orderHeaderSyncTable);

            SyncTable orderDetailSyncTable = new SyncTable("OrderDetail");
            orderDetailSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            orderDetailSyncTable.SyncDirection = SyncDirection.Bidirectional;
            orderDetailSyncTable.SyncGroup = orderSyncGroup;
            this.Configuration.SyncTables.Add(orderDetailSyncTable);

            //Handle the StateChanged and SessionProgress events, and
            //display information to the console.
            SampleStatsAndProgress sampleStats = new SampleStatsAndProgress();
            this.StateChanged += new EventHandler<SessionStateChangedEventArgs>(sampleStats.DisplaySessionProgress);
            this.SessionProgress += new EventHandler<SessionProgressEventArgs>(sampleStats.DisplaySessionProgress);

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


            //Create SyncAdapters for each table by using the SqlSyncAdapterBuilder:
            //  * Specify the base table and tombstone table names.
            //  * Specify the columns that are used to track when
            //    and where changes are made.
            //  * Specify bidirectional synchronization.
            //  * Call ToSyncAdapter to create the SyncAdapter.
            //  * Specify a name for the SyncAdapter that matches the
            //    the name specified for the corresponding SyncTable.
            //    Do not include the schema names (Sales in this case).

            //Customer table
            //<snippetOCS_CS_Events_CustomerAdapterBuilder>
            SqlSyncAdapterBuilder customerBuilder = new SqlSyncAdapterBuilder(serverConn);

            customerBuilder.TableName = "Sales.Customer";
            customerBuilder.TombstoneTableName = customerBuilder.TableName + "_Tombstone";
            customerBuilder.SyncDirection = SyncDirection.Bidirectional;
            customerBuilder.CreationTrackingColumn = "InsertTimestamp";
            customerBuilder.UpdateTrackingColumn = "UpdateTimestamp";
            customerBuilder.DeletionTrackingColumn = "DeleteTimestamp";
            customerBuilder.CreationOriginatorIdColumn = "InsertId";
            customerBuilder.UpdateOriginatorIdColumn = "UpdateId";
            customerBuilder.DeletionOriginatorIdColumn = "DeleteId";

            SyncAdapter customerSyncAdapter = customerBuilder.ToSyncAdapter();
            customerSyncAdapter.TableName = "Customer";
            this.SyncAdapters.Add(customerSyncAdapter);
            //</snippetOCS_CS_Events_CustomerAdapterBuilder>

            //OrderHeader table.
            SqlSyncAdapterBuilder orderHeaderBuilder = new SqlSyncAdapterBuilder(serverConn);

            orderHeaderBuilder.TableName = "Sales.OrderHeader";
            orderHeaderBuilder.TombstoneTableName = orderHeaderBuilder.TableName + "_Tombstone";
            orderHeaderBuilder.SyncDirection = SyncDirection.Bidirectional;
            orderHeaderBuilder.CreationTrackingColumn = "InsertTimestamp";
            orderHeaderBuilder.UpdateTrackingColumn = "UpdateTimestamp";
            orderHeaderBuilder.DeletionTrackingColumn = "DeleteTimestamp";
            orderHeaderBuilder.CreationOriginatorIdColumn = "InsertId";
            orderHeaderBuilder.UpdateOriginatorIdColumn = "UpdateId";
            orderHeaderBuilder.DeletionOriginatorIdColumn = "DeleteId";

            SyncAdapter orderHeaderSyncAdapter = orderHeaderBuilder.ToSyncAdapter();
            orderHeaderSyncAdapter.TableName = "OrderHeader";
            this.SyncAdapters.Add(orderHeaderSyncAdapter);


            //OrderDetail table.
            SqlSyncAdapterBuilder orderDetailBuilder = new SqlSyncAdapterBuilder(serverConn);

            orderDetailBuilder.TableName = "Sales.OrderDetail";
            orderDetailBuilder.TombstoneTableName = orderDetailBuilder.TableName + "_Tombstone";
            orderDetailBuilder.SyncDirection = SyncDirection.Bidirectional;
            orderDetailBuilder.CreationTrackingColumn = "InsertTimestamp";
            orderDetailBuilder.UpdateTrackingColumn = "UpdateTimestamp";
            orderDetailBuilder.DeletionTrackingColumn = "DeleteTimestamp";
            orderDetailBuilder.CreationOriginatorIdColumn = "InsertId";
            orderDetailBuilder.UpdateOriginatorIdColumn = "UpdateId";
            orderDetailBuilder.DeletionOriginatorIdColumn = "DeleteId";

            SyncAdapter orderDetailSyncAdapter = orderDetailBuilder.ToSyncAdapter();
            orderDetailSyncAdapter.TableName = "OrderDetail";
            this.SyncAdapters.Add(orderDetailSyncAdapter);

            //Log information for the following events.
            this.ChangesSelected += new EventHandler<ChangesSelectedEventArgs>(EventLogger.LogEvents);
            this.ChangesApplied += new EventHandler<ChangesAppliedEventArgs>(EventLogger.LogEvents);
            //<snippetOCS_CS_Events_ApplyChangeFailedEventHandler>
            this.ApplyChangeFailed += new EventHandler<ApplyChangeFailedEventArgs>(EventLogger.LogEvents);
            //</snippetOCS_CS_Events_ApplyChangeFailedEventHandler>
            //Handle the ApplyingChanges event so that we can
            //make changes to the dataset.
            this.ApplyingChanges += new EventHandler<ApplyingChangesEventArgs>(SampleServerSyncProvider_ApplyingChanges);
  
        }
        //Look for inserted rows in the dataset that have a CustomerType
        //of Wholesale and update these rows. With access to the dataset,
        //you can write any business logic that your application requires.
        //<snippetOCS_CS_Events_BusinessLogic>
        private void SampleServerSyncProvider_ApplyingChanges(object sender, ApplyingChangesEventArgs e)
        {
            DataTable customerDataTable = e.Changes.Tables["Customer"];
            
            for (int i = 0; i < customerDataTable.Rows.Count; i++)
            {
                if (customerDataTable.Rows[i].RowState == DataRowState.Added
                    && customerDataTable.Rows[i]["CustomerType"] == "Wholesale")
                {
                    customerDataTable.Rows[i]["CustomerType"] = "Wholesale (from mobile sales)";
                }
            }
        }
        //</snippetOCS_CS_Events_BusinessLogic>
    }

    //Create a class that is derived from 
    //Microsoft.Synchronization.Data.SqlServerCe.SqlCeClientSyncProvider.
    //You can just instantiate the provider directly and associate it
    //with the SyncAgent, but here we use this class to handle client 
    //provider events.
    //<snippetOCS_CS_Events_SampleClientSyncProvider>
    public class SampleClientSyncProvider : SqlCeClientSyncProvider
    {

        public SampleClientSyncProvider()
        {
            //Specify a connection string for the sample client database.
            Utility util = new Utility();
            this.ConnectionString = util.ClientConnString;

            //Log information for the following events.
            this.SchemaCreated += new EventHandler<SchemaCreatedEventArgs>(EventLogger.LogEvents);
            this.ChangesSelected += new EventHandler<ChangesSelectedEventArgs>(EventLogger.LogEvents);
            this.ChangesApplied += new EventHandler<ChangesAppliedEventArgs>(EventLogger.LogEvents);
            this.ApplyChangeFailed += new EventHandler<ApplyChangeFailedEventArgs>(EventLogger.LogEvents);
            
            //Use the following events to fix up schema on the client.
            //We use the CreatingSchema event to change the schema
            //by using the API. We use the SchemaCreated event 
            //to change the schema by using SQL.
            //Note that both schema events fire for the Customer table,
            //even though we already created the table. This allows us
            //to work with the table at this point if we have to.
            this.CreatingSchema += new EventHandler<CreatingSchemaEventArgs>(SampleClientSyncProvider_CreatingSchema);
            this.SchemaCreated += new EventHandler<SchemaCreatedEventArgs>(SampleClientSyncProvider_SchemaCreated);

         }

        private void SampleClientSyncProvider_CreatingSchema(object sender, CreatingSchemaEventArgs e)
        {

            string tableName = e.Table.TableName;

            if (tableName == "Customer")
            {
                //Set the RowGuid property because it is not copied
                //to the client by default. This is also a good time
                //to specify literal defaults with .Columns[ColName].DefaultValue,
                //but we will specify defaults like NEWID() by calling
                //ALTER TABLE after the table is created.
                e.Schema.Tables["Customer"].Columns["CustomerId"].RowGuid = true;
            }

            if (tableName == "OrderHeader")
            {
                e.Schema.Tables["OrderHeader"].Columns["OrderId"].RowGuid = true;
            }
        }

        private void SampleClientSyncProvider_SchemaCreated(object sender, SchemaCreatedEventArgs e)
        {
            string tableName = e.Table.TableName;
            Utility util = new Utility();

            //Call ALTER TABLE on the client. This must be done
            //over the same connection and within the same
            //transaction that Synchronization Services uses
            //to create the schema on the client.
            if (tableName == "Customer")
            {
                util.MakeSchemaChangesOnClient(e.Connection, e.Transaction, "Customer");                
            }
           
            if (tableName == "OrderHeader")
            {
                util.MakeSchemaChangesOnClient(e.Connection, e.Transaction, "OrderHeader");                
            }

            if (tableName == "OrderDetail")
            {
                util.MakeSchemaChangesOnClient(e.Connection, e.Transaction, "OrderDetail");                
            }
        }
    }
    //</snippetOCS_CS_Events_SampleClientSyncProvider>

    //Handle SyncAgent events and the statistics that are returned by the SyncAgent.
    public class SampleStatsAndProgress
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
            Console.WriteLine("Upload Changes Applied: " + syncStatistics.UploadChangesApplied);
            Console.WriteLine("Upload Changes Failed: " + syncStatistics.UploadChangesFailed);
            Console.WriteLine("Total Changes Uploaded: " + syncStatistics.TotalChangesUploaded);
            Console.WriteLine("Download Changes Applied: " + syncStatistics.DownloadChangesApplied);
            Console.WriteLine("Download Changes Failed: " + syncStatistics.DownloadChangesFailed);
            Console.WriteLine("Total Changes Downloaded: " + syncStatistics.TotalChangesDownloaded);
            Console.WriteLine("Complete Time: " + syncStatistics.SyncCompleteTime);
            Console.WriteLine(String.Empty);
        }

        public void DisplaySessionProgress(object sender, EventArgs e)
        {

            StringBuilder outputText = new StringBuilder();

            if (e is SessionStateChangedEventArgs)
            {

                SessionStateChangedEventArgs args = (SessionStateChangedEventArgs)e;

                if (args.SessionState == SyncSessionState.Synchronizing)
                {
                    outputText.AppendLine(String.Empty);
                    outputText.Append("** SyncAgent is synchronizing");
                }

                else
                {
                    outputText.Append("** SyncAgent is ready to synchronize");
                }
            }

            else if (e is SessionProgressEventArgs)
            {
                SessionProgressEventArgs args = (SessionProgressEventArgs)e;
                outputText.Append("Percent complete: " + args.PercentCompleted + " (" + args.SyncStage + ")");
            }

            else
            {
                outputText.AppendLine("Unknown event occurred");
            }

            Console.WriteLine(outputText.ToString());
        }
    }

    public class EventLogger
    {
        //Create client and server log files, and write to them
        //based on data from several EventArgs classes.
        public static void LogEvents(object sender, EventArgs e)
        {
            string logFile = String.Empty;
            string site = String.Empty;

            if (sender is SampleServerSyncProvider)
            {
                logFile = "ServerLogFile.txt";
                site = "server";
            }
            else if (sender is SampleClientSyncProvider)
            {
                logFile = "ClientLogFile.txt";
                site = "client";
            }

            StreamWriter streamWriter = File.AppendText(logFile);
            StringBuilder outputText = new StringBuilder();

            if (e is ChangesSelectedEventArgs)
            {
                
                ChangesSelectedEventArgs args = (ChangesSelectedEventArgs)e;
                outputText.AppendLine("Client ID: " + args.Session.ClientId);                
                outputText.AppendLine("Changes selected from " + site + " for group " + args.GroupMetadata.GroupName);
                outputText.AppendLine("Inserts selected from " + site + " for group: " + args.Context.GroupProgress.TotalInserts.ToString());
                outputText.AppendLine("Updates selected from " + site + " for group: " + args.Context.GroupProgress.TotalUpdates.ToString());
                outputText.AppendLine("Deletes selected from " + site + " for group: " + args.Context.GroupProgress.TotalDeletes.ToString());
            }

            else if (e is ChangesAppliedEventArgs)
            {
                
                ChangesAppliedEventArgs args = (ChangesAppliedEventArgs)e;
                outputText.AppendLine("Client ID: " + args.Session.ClientId);                
                outputText.AppendLine("Changes applied to " + site + " for group " + args.GroupMetadata.GroupName);
                outputText.AppendLine("Inserts applied to " + site + " for group: " + args.Context.GroupProgress.TotalInserts.ToString());
                outputText.AppendLine("Updates applied to " + site + " for group: " + args.Context.GroupProgress.TotalUpdates.ToString());
                outputText.AppendLine("Deletes applied to " + site + " for group: " + args.Context.GroupProgress.TotalDeletes.ToString());
 
            }   

            else if (e is SchemaCreatedEventArgs)
            {
                
                SchemaCreatedEventArgs args = (SchemaCreatedEventArgs)e;
                outputText.AppendLine("Schema creation for group: " + args.Table.SyncGroup.GroupName);
                outputText.AppendLine("Table: " + args.Table.TableName);
                outputText.AppendLine("Direction : " + args.Table.SyncDirection);
                outputText.AppendLine("Creation Option: " + args.Table.CreationOption);
            }

            //<snippetOCS_CS_Events_ApplyChangeFailedEventArgs>
            else if (e is ApplyChangeFailedEventArgs)
            {

                ApplyChangeFailedEventArgs args = (ApplyChangeFailedEventArgs)e;
                outputText.AppendLine("** APPLY CHANGE FAILURE AT " + site.ToUpper() + " **");
                outputText.AppendLine("Table for which failure occurred: " + args.TableMetadata.TableName);
                outputText.AppendLine("Error message: " + args.Error.Message);
            
            }
            //</snippetOCS_CS_Events_ApplyChangeFailedEventArgs>

            else
            {
                outputText.AppendLine("Unknown event occurred");
            }
 
            streamWriter.WriteLine(DateTime.Now.ToShortTimeString() + " | " + outputText.ToString());
            streamWriter.Flush();
            streamWriter.Dispose();
        }
    }
}
//</snippetOCS_CS_Events>


                /*
                SqlCeConnection conn = new SqlCeConnection("Data Source=SyncSampleClient.sdf;");
                conn.Open();
                SqlCeCommand cmd;
                cmd = new SqlCeCommand("UPDATE Customer SET SalesPerson = 'Brandy Diaz' WHERE SalesPerson = 'Brenda Diaz'", conn);
                cmd.ExecuteNonQuery();
               
                conn.Close();
                 */


                /*

                SqlCeConnection conn = new SqlCeConnection("Data Source=SyncSampleClient.sdf;");
                conn.Open();
                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM Customer", conn);
                SqlCeDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr.GetValue(0));
                    Console.WriteLine(rdr.GetValue(1));
                    Console.WriteLine(rdr.GetValue(2));
                    Console.WriteLine(rdr.GetValue(3));
                    Console.WriteLine(rdr.GetValue(4));
                    Console.WriteLine(rdr.GetValue(5));
                }
                conn.Close();
                Console.ReadLine();
                 */