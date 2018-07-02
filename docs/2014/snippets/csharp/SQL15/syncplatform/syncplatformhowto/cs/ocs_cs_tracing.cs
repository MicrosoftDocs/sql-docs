//<snippetOCS_CS_Tracing>
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

            //Write to the console which tracing levels are enabled. The app.config
            //file specifies a value of 3 for the SyncTracer switch, which corresponds
            //to Info. Therefore, Error, Warning, and Info return True, and Verbose
            //returns False.
            Console.WriteLine("");
            //<snippetOCS_CS_Tracing_LevelsEnabled>
            Console.WriteLine("** Tracing Levels Enabled for this Application **");
            Console.WriteLine("Error: " + SyncTracer.IsErrorEnabled().ToString());
            Console.WriteLine("Warning: " + SyncTracer.IsWarningEnabled().ToString());
            Console.WriteLine("Info: " + SyncTracer.IsInfoEnabled().ToString());
            Console.WriteLine("Verbose: " + SyncTracer.IsVerboseEnabled().ToString());
            //</snippetOCS_CS_Tracing_LevelsEnabled>

            //Initial synchronization. Instantiate the SyncAgent
            //and call Synchronize.
            //<snippetOCS_CS_Tracing_Synchronize>
            SampleSyncAgent sampleSyncAgent = new SampleSyncAgent();
            SyncStatistics syncStatistics = sampleSyncAgent.Synchronize();
            //</snippetOCS_CS_Tracing_Synchronize>
            sampleStats.DisplayStats(syncStatistics, "initial");

            //Make a change at the client that fails when it is
            //applied at the server. The constraint violation
            //is automatically written to the trace file as a warning.
            util.MakeFailingChangesOnClient();

            //Make changes at the client and server that conflict
            //when they are synchronized. The conflicts are written
            //to the trace file in the SampleServerSyncProvider_ApplyChangeFailed
            //event handler.
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
            //<snippetOCS_CS_Tracing_CustomerSyncTable>
            SyncTable customerSyncTable = new SyncTable("Customer");
            customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            customerSyncTable.SyncDirection = SyncDirection.Bidirectional;
            this.Configuration.SyncTables.Add(customerSyncTable);
            //</snippetOCS_CS_Tracing_CustomerSyncTable>
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
            //<snippetOCS_CS_Tracing_NewAnchorCommand>
            SqlCommand selectNewAnchorCommand = new SqlCommand();
            string newAnchorVariable = "@" + SyncSession.SyncNewReceivedAnchor;
            selectNewAnchorCommand.CommandText = "SELECT " + newAnchorVariable + " = min_active_rowversion() - 1";
            selectNewAnchorCommand.Parameters.Add(newAnchorVariable, SqlDbType.Timestamp);
            selectNewAnchorCommand.Parameters[newAnchorVariable].Direction = ParameterDirection.Output;
            selectNewAnchorCommand.Connection = serverConn;
            this.SelectNewAnchorCommand = selectNewAnchorCommand;
            //</snippetOCS_CS_Tracing_NewAnchorCommand>


            //Create a SyncAdapter for the Customer table by using 
            //the SqlSyncAdapterBuilder:
            //  * Specify the base table and tombstone table names.
            //  * Specify the columns that are used to track when
            //    and where changes are made.
            //  * Specify bidirectional synchronization.
            //  * Call ToSyncAdapter to create the SyncAdapter.
            //  * Specify a name for the SyncAdapter that matches the
            //    the name specified for the corresponding SyncTable.
            //    Do not include the schema names (Sales in this case).

            //<snippetOCS_CS_Tracing_CustomerAdapterBuilder>
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
            //</snippetOCS_CS_Tracing_CustomerAdapterBuilder>

            //Handle the ApplyChangeFailed event. This allows us to write  
            //information to the trace file about any conflicts that occur.
            this.ApplyChangeFailed += new EventHandler<ApplyChangeFailedEventArgs>(SampleServerSyncProvider_ApplyChangeFailed);
        }

        private void SampleServerSyncProvider_ApplyChangeFailed(object sender, ApplyChangeFailedEventArgs e)
        {
            //Verbose tracing includes information about conflicts. In this application,
            //Verbose tracing is disabled, and we have decided to flag conflicts with a 
            //warning.
            //Check if Verbose tracing is enabled and if the conflict is an error.
            //If the conflict is not an error, we write a warning to the trace file
            //with information about the conflict.
            //<snippetOCS_CS_Tracing_ApplyChangeFailed>
            if (SyncTracer.IsVerboseEnabled() == false && e.Conflict.ConflictType != ConflictType.ErrorsOccurred)
            {
                DataTable conflictingServerChange = e.Conflict.ServerChange;
                DataTable conflictingClientChange = e.Conflict.ClientChange;
                int serverColumnCount = conflictingServerChange.Columns.Count;
                int clientColumnCount = conflictingClientChange.Columns.Count;
                StringBuilder clientRowAsString = new StringBuilder();
                StringBuilder serverRowAsString = new StringBuilder();

                for (int i = 0; i < clientColumnCount; i++)
                {
                    clientRowAsString.Append(conflictingClientChange.Rows[0][i] + " | ");
                }

                for (int i = 0; i < serverColumnCount; i++)
                {
                    serverRowAsString.Append(conflictingServerChange.Rows[0][i] + " | ");
                }

                SyncTracer.Warning(1, "CONFLICT DETECTED FOR CLIENT {0}", e.Session.ClientId);
                SyncTracer.Warning(2, "** Client change **");
                SyncTracer.Warning(2, clientRowAsString.ToString());
                SyncTracer.Warning(2, "** Server change **");
                SyncTracer.Warning(2, serverRowAsString.ToString());
            }
            //</snippetOCS_CS_Tracing_ApplyChangeFailed>
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
            
            this.SchemaCreated += new EventHandler<SchemaCreatedEventArgs>(SampleClientSyncProvider_SchemaCreated);            
        }

        private void SampleClientSyncProvider_SchemaCreated(object sender, SchemaCreatedEventArgs e)
        {
            string tableName = e.Table.TableName;
            Utility util = new Utility();

            //Call ALTER TABLE on the client. This must be done
            //over the same connection and within the same
            //transaction that Synchronization Services uses
            //to create the schema on the client.
            util.MakeSchemaChangesOnClient(e.Connection, e.Transaction, "Customer");

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

            //<snippetOCS_CS_Tracing_Statistics>
            Console.WriteLine("Start Time: " + syncStatistics.SyncStartTime);
            Console.WriteLine("Total Changes Downloaded: " + syncStatistics.TotalChangesDownloaded);
            Console.WriteLine("Total Changes Uploaded: " + syncStatistics.TotalChangesUploaded);
            Console.WriteLine("Complete Time: " + syncStatistics.SyncCompleteTime);
            Console.WriteLine(String.Empty);
            //</snippetOCS_CS_Tracing_Statistics>

        }
    }
}
//</snippetOCS_CS_Tracing>