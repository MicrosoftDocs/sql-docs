//<snippetOCS_CS_Conflicts>
using System;
using System.Collections;
using System.Collections.Generic;
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

            //Make a change at the client that fails when it is
            //applied at the server.
            util.MakeFailingChangesOnClient();

            //Make changes at the client and server that conflict
            //when they are synchronized.
            util.MakeConflictingChangesOnClientAndServer();

            //Subsequent synchronization.
            syncStatistics = sampleSyncAgent.Synchronize();
            sampleStats.DisplayStats(syncStatistics, "subsequent");

            //Return server data back to its original state.
            //Comment out this line if you want to view the
            //state of the data after all conflicts are resolved.
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

            //Add the Customer table: specify a synchronization direction 
            //of Bidirectional.
            SyncTable customerSyncTable = new SyncTable("Customer");
            customerSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            customerSyncTable.SyncDirection = SyncDirection.Bidirectional;
            this.Configuration.SyncTables.Add(customerSyncTable);
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


            //Create a SyncAdapter for the Customer table, and then define
            //the commands to synchronize changes:
            //* SelectConflictUpdatedRowsCommand SelectConflictDeletedRowsCommand
            //  are used to detect if there are conflicts on the server during
            //  synchronization.
            //* SelectIncrementalInsertsCommand, SelectIncrementalUpdatesCommand,
            //  and SelectIncrementalDeletesCommand are used to select changes
            //  from the server that the client provider then applies to the client.
            //* InsertCommand, UpdateCommand, and DeleteCommand are used to apply
            //  to the server the changes that the client provider has selected
            //  from the client.

            //Create the SyncAdapter.
            //<snippetOCS_CS_Conflicts_CustomerSyncAdapter>
            SyncAdapter customerSyncAdapter = new SyncAdapter("Customer");

            //This command is used if @sync_row_count returns
            //0 when changes are applied to the server.
            //<snippetOCS_CS_Conflicts_SelectConflictUpdatedRowsCommand>
            SqlCommand customerUpdateConflicts = new SqlCommand();
            customerUpdateConflicts.CommandText =
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " +
                "FROM Sales.Customer " +
                "WHERE CustomerId = @CustomerId";
            customerUpdateConflicts.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            customerUpdateConflicts.Connection = serverConn;
            customerSyncAdapter.SelectConflictUpdatedRowsCommand = customerUpdateConflicts;
            //</snippetOCS_CS_Conflicts_SelectConflictUpdatedRowsCommand>

            //This command is used if the server provider cannot find
            //a row in the base table.
            //<snippetOCS_CS_Conflicts_SelectConflictDeletedRowsCommand>
            SqlCommand customerDeleteConflicts = new SqlCommand();
            customerDeleteConflicts.CommandText =
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " +
                "FROM Sales.Customer_Tombstone " +
                "WHERE CustomerId = @CustomerId";
            customerDeleteConflicts.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            customerDeleteConflicts.Connection = serverConn;
            customerSyncAdapter.SelectConflictDeletedRowsCommand = customerDeleteConflicts;
            //</snippetOCS_CS_Conflicts_SelectConflictDeletedRowsCommand>

            //Select inserts from the server.
            //<snippetOCS_CS_Conflicts_CustomerIncrInsert>
            SqlCommand customerIncrInserts = new SqlCommand();
            customerIncrInserts.CommandText =
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " +
                "FROM Sales.Customer " +
                "WHERE (InsertTimestamp > @sync_last_received_anchor " +
                "AND InsertTimestamp <= @sync_new_received_anchor " +
                "AND InsertId <> @sync_client_id)";
            customerIncrInserts.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            customerIncrInserts.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            customerIncrInserts.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            customerIncrInserts.Connection = serverConn;
            customerSyncAdapter.SelectIncrementalInsertsCommand = customerIncrInserts;
            //</snippetOCS_CS_Conflicts_CustomerIncrInsert>

            //Apply inserts to the server.
            //<snippetOCS_CS_Conflicts_CustomerInsert>
            SqlCommand customerInserts = new SqlCommand();
            customerInserts.CommandType = CommandType.StoredProcedure;
            customerInserts.CommandText = "usp_CustomerApplyInsert";
            customerInserts.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            customerInserts.Parameters.Add("@" + SyncSession.SyncForceWrite, SqlDbType.Bit); 
            customerInserts.Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;
            customerInserts.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            customerInserts.Parameters.Add("@CustomerName", SqlDbType.NVarChar);
            customerInserts.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
            customerInserts.Parameters.Add("@CustomerType", SqlDbType.NVarChar);
            customerInserts.Connection = serverConn;
            customerSyncAdapter.InsertCommand = customerInserts;
            //</snippetOCS_CS_Conflicts_CustomerInsert>


            //Select updates from the server.
            //<snippetOCS_CS_Conflicts_CustomerIncrUpdate>
            SqlCommand customerIncrUpdates = new SqlCommand();
            customerIncrUpdates.CommandText =
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " +
                "FROM Sales.Customer " +
                "WHERE (UpdateTimestamp > @sync_last_received_anchor " +
                "AND UpdateTimestamp <= @sync_new_received_anchor " +
                "AND UpdateId <> @sync_client_id " +
                "AND NOT (InsertTimestamp > @sync_last_received_anchor " +
                "AND InsertId <> @sync_client_id))";
            customerIncrUpdates.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            customerIncrUpdates.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            customerIncrUpdates.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            customerIncrUpdates.Connection = serverConn;
            customerSyncAdapter.SelectIncrementalUpdatesCommand = customerIncrUpdates;
            //</snippetOCS_CS_Conflicts_CustomerIncrUpdate>

            //Apply updates to the server.
            //<snippetOCS_CS_Conflicts_CustomerUpdate>
            SqlCommand customerUpdates = new SqlCommand();
            customerUpdates.CommandType = CommandType.StoredProcedure;
            customerUpdates.CommandText = "usp_CustomerApplyUpdate";
            customerUpdates.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            customerUpdates.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            customerUpdates.Parameters.Add("@" + SyncSession.SyncForceWrite, SqlDbType.Bit);            
            customerUpdates.Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;
            customerUpdates.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            customerUpdates.Parameters.Add("@CustomerName", SqlDbType.NVarChar);
            customerUpdates.Parameters.Add("@SalesPerson", SqlDbType.NVarChar);
            customerUpdates.Parameters.Add("@CustomerType", SqlDbType.NVarChar);
            customerUpdates.Connection = serverConn;
            customerSyncAdapter.UpdateCommand = customerUpdates;
            //</snippetOCS_CS_Conflicts_CustomerUpdate>


            //Select deletes from the server.
            //<snippetOCS_CS_Conflicts_CustomerIncrDelete>
            SqlCommand customerIncrDeletes = new SqlCommand();
            customerIncrDeletes.CommandText =
                "SELECT CustomerId, CustomerName, SalesPerson, CustomerType " +
                "FROM Sales.Customer_Tombstone " +
                "WHERE (@sync_initialized = 1 " +
                "AND DeleteTimestamp > @sync_last_received_anchor " +
                "AND DeleteTimestamp <= @sync_new_received_anchor " +
                "AND DeleteId <> @sync_client_id)";
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncInitialized, SqlDbType.Bit);
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncNewReceivedAnchor, SqlDbType.Timestamp);
            customerIncrDeletes.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            customerIncrDeletes.Connection = serverConn;
            customerSyncAdapter.SelectIncrementalDeletesCommand = customerIncrDeletes;
            //</snippetOCS_CS_Conflicts_CustomerIncrDelete>

            //Apply deletes to the server.
            //<snippetOCS_CS_Conflicts_CustomerDelete>
            SqlCommand customerDeletes = new SqlCommand();
            customerDeletes.CommandType = CommandType.StoredProcedure;
            customerDeletes.CommandText = "usp_CustomerApplyDelete";
            customerDeletes.Parameters.Add("@" + SyncSession.SyncLastReceivedAnchor, SqlDbType.Timestamp);
            customerDeletes.Parameters.Add("@" + SyncSession.SyncClientId, SqlDbType.UniqueIdentifier);
            customerDeletes.Parameters.Add("@" + SyncSession.SyncForceWrite, SqlDbType.Bit);           
            customerDeletes.Parameters.Add("@" + SyncSession.SyncRowCount, SqlDbType.Int).Direction = ParameterDirection.Output;
            customerDeletes.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier);
            customerDeletes.Connection = serverConn;
            customerSyncAdapter.DeleteCommand = customerDeletes;
            //</snippetOCS_CS_Conflicts_CustomerDelete>


            //Add the SyncAdapter to the server synchronization provider.
            this.SyncAdapters.Add(customerSyncAdapter);
            //</snippetOCS_CS_Conflicts_CustomerSyncAdapter>


            //Handle the ApplyChangeFailed and ChangesApplied events. 
            //This allows us to respond to any conflicts that occur, and to 
            //make changes that are downloaded to the client during the same
            //session.
            this.ApplyChangeFailed +=new EventHandler<ApplyChangeFailedEventArgs>(SampleServerSyncProvider_ApplyChangeFailed);
            this.ChangesApplied +=new EventHandler<ChangesAppliedEventArgs>(SampleServerSyncProvider_ChangesApplied);
        }

        //Create a list to hold primary keys from the Customer
        //table. This list is used when we handle the ApplyChangeFailed 
        //and ChangesApplied events.
        private List<Guid> _updateConflictGuids = new List<Guid>();
        
        private void SampleServerSyncProvider_ApplyChangeFailed(object sender, ApplyChangeFailedEventArgs e)
        {

            //Log information for the ApplyChangeFailed event.
            EventLogger.LogEvents(sender, e);

            //Respond to four different types of conflicts:
            // * ClientDeleteServerUpdate
            // * ClientUpdateServerDelete
            // * ClientInsertServerInsert
            // * ClientUpdateServerUpdate
            //
            if (e.Conflict.ConflictType == ConflictType.ClientDeleteServerUpdate)
            {
                //With the commands we are using, the default is for the server 
                //change to win and be applied to the client. Here, we accept the 
                //default on the server side. We also set ConflictResolver.ServerWins 
                //for this conflict in the client provider. This ensures that the server
                //change is applied to the client during the download phase.
                Console.WriteLine(String.Empty);
                Console.WriteLine("***********************************");
                Console.WriteLine("A client delete / server update conflict was detected.");

                e.Action = ApplyAction.Continue;

                Console.WriteLine("The server change will be applied at the client.");
                Console.WriteLine("***********************************");
                Console.WriteLine(String.Empty);
            }

            //<snippetOCS_CS_Conflicts_ServerApplyChangeFailed>
            if (e.Conflict.ConflictType == ConflictType.ClientUpdateServerDelete)
            {

                //For client-update/server-delete conflicts, we force the client 
                //change to be applied at the server. The stored procedure specified for 
                //customerSyncAdapter.UpdateCommand accepts the @sync_force_write parameter
                //and includes logic to handle this case.
                Console.WriteLine(String.Empty);
                Console.WriteLine("***********************************");
                Console.WriteLine("A client update / server delete conflict was detected.");

                e.Action = ApplyAction.RetryWithForceWrite;
                
                Console.WriteLine("The client change was retried at the server with RetryWithForceWrite.");
                Console.WriteLine("***********************************"); 
                Console.WriteLine(String.Empty);
             
            }
            //</snippetOCS_CS_Conflicts_ServerApplyChangeFailed>

            if (e.Conflict.ConflictType == ConflictType.ClientInsertServerInsert)
            {
                //Similar to how we handled the client-delete/server-update conflict.
                //In this case, we set ConflictResolver.FireEvent and use RetryWithForceWrite
                //for this conflict in the client provider. This is equivalent to 
                //ConflictResolver.ServerWins, and ensures that the server
                //change is applied to the client during the download phase.
                Console.WriteLine(String.Empty);
                Console.WriteLine("***********************************");
                Console.WriteLine("A client insert / server insert conflict was detected.");

                e.Action = ApplyAction.Continue;

                Console.WriteLine("The server change will be applied at the client.");
                Console.WriteLine("***********************************");
                Console.WriteLine(String.Empty);
            }

            if (e.Conflict.ConflictType == ConflictType.ClientUpdateServerUpdate)
            {

                //For client-update/server-update conflicts, we want to
                //allow the user to specify the conflict resolution option.
                //
                //It is possible for the Conflict object from the
                //server to have more than one row. Because our custom
                //resolution code only works with one row at a time,
                //we only allow the user to select a resolution
                //option if the object contains a single row.
                if (e.Conflict.ServerChange.Rows.Count > 1)
                {
                    Console.WriteLine(String.Empty);
                    Console.WriteLine("***********************************");
                    Console.WriteLine("A client update / server update conflict was detected.");

                    e.Action = ApplyAction.Continue;

                    Console.WriteLine("The server change will be applied at the client.");
                    Console.WriteLine("***********************************");
                    Console.WriteLine(String.Empty);
                }
                else
                {
                    Console.WriteLine(String.Empty);
                    Console.WriteLine("***********************************");
                    Console.WriteLine("A client update / server update conflict was detected.");
                    Console.WriteLine("Conflicting rows are displayed below.");
                    Console.WriteLine("***********************************");

                    //Get the conflicting changes from the Conflict object
                    //and display them. The Conflict object holds a copy
                    //of the changes; updates to this object will not be 
                    //applied. To make changes, use the Context object,
                    //which is demonstrated in the next section of code
                    //under ' case "CU" '.
                    DataTable conflictingServerChange = e.Conflict.ServerChange;
                    DataTable conflictingClientChange = e.Conflict.ClientChange;
                    int serverColumnCount = conflictingServerChange.Columns.Count;
                    int clientColumnCount = conflictingClientChange.Columns.Count;
                    
                    Console.WriteLine(String.Empty);
                    Console.WriteLine("Server row: ");
                    Console.Write(" | ");

                    //Display the server row.
                    for (int i = 0; i < serverColumnCount; i++)
                    {
                        Console.Write(conflictingServerChange.Rows[0][i] + " | ");
                    }

                    Console.WriteLine(String.Empty);
                    Console.WriteLine(String.Empty);
                    Console.WriteLine("Client row: ");
                    Console.Write(" | ");

                    //Display the client row.
                    for (int i = 0; i < clientColumnCount; i++)
                    {
                        Console.Write(conflictingClientChange.Rows[0][i] + " | ");
                    }

                    Console.WriteLine(String.Empty);
                    Console.WriteLine(String.Empty);

                    //Ask for a conflict resolution option.
                    Console.WriteLine("Enter a resolution option for this conflict:");
                    Console.WriteLine("SE = server change wins");
                    Console.WriteLine("CL = client change wins");
                    Console.WriteLine("CU = custom resolution (combine rows)");

                    string conflictResolution = Console.ReadLine();
                    conflictResolution.ToUpper();

                    switch (conflictResolution)
                    {
                        case "SE":

                            //Again, this is the default for the commands we are using:
                            //the server change is persisted and then downloaded to the client.
                            e.Action = ApplyAction.Continue;
                            Console.WriteLine(String.Empty);
                            Console.WriteLine("The server change will be applied at the client.");

                            break;

                        case "CL":

                            //Override the default by specifying that the client row
                            //should be applied at the server. The stored procedure specified  
                            //for customerSyncAdapter.UpdateCommand accepts the @sync_force_write 
                            //parameter and includes logic to handle this case.
                            e.Action = ApplyAction.RetryWithForceWrite;
                            Console.WriteLine(String.Empty);
                            Console.WriteLine("The client change was retried at the server with RetryWithForceWrite.");

                            break;

                        case "CU":

                            //Provide a custom resolution scheme that takes each conflicting
                            //column and applies the combined contents of the column to the 
                            //client and server. This is not necessarily a resolution scheme 
                            //that you would use in production. Instead, it is used to 
                            //demonstrate the various ways you can interact with conflicting 
                            //data during synchronization.
                            //
                            //Get the ID for the conflicting row from the client data table,
                            //and add it to a list of GUIDs. We update rows at the server
                            //based on this list.
                            Guid customerId = (Guid)conflictingClientChange.Rows[0]["CustomerId"];
                            _updateConflictGuids.Add(customerId);
                            
                            //Create a dictionary to hold the column ordinal and value for
                            //any columns that are in confict.
                            Dictionary<int, string> conflictingColumns = new Dictionary<int, string>();
                            string combinedColumnValue;

                            //Determine which columns are different at the client and server.
                            //We already looped through these columns once, but we wanted to
                            //keep this code separate from the display code above.
                            for (int i = 0; i < clientColumnCount; i++)
                            {
                                if (conflictingClientChange.Rows[0][i].ToString() != conflictingServerChange.Rows[0][i].ToString())
                                {
                                    //If we find a column that is different, combine the values from
                                    //the client and server, and write "| conflict |" between them.
                                    combinedColumnValue = conflictingClientChange.Rows[0][i] + "  | conflict |  " + 
                                        conflictingServerChange.Rows[0][i];
                                    conflictingColumns.Add(i, combinedColumnValue);
                                }
                            }

                            //Loop through the rows in the Context object, which exposes
                            //the set of changes that are uploaded from the client.
                            //Note: In the ApplyChangeFailed event for the client provider,  
                            //you have access to the set of changes that was downloaded from
                            //the server.
                            DataTable allClientChanges = e.Context.DataSet.Tables["Customer"];
                            int allClientRowCount = allClientChanges.Rows.Count;
                            int allClientColumnCount = allClientChanges.Columns.Count;

                            for (int i = 0; i < allClientRowCount; i++)
                            {
                                //Find the changed row with the GUID from the Conflict object.
                                if (allClientChanges.Rows[i].RowState == DataRowState.Modified &&
                                    (Guid)allClientChanges.Rows[i]["CustomerId"] == customerId)
                                {
                                    //Loop through the columns and check whether the column
                                    //is in the conflictingColumns dictionary. If it is,
                                    //update the value in the allClientChanges Context object.
                                    for (int j = 0; j < allClientColumnCount; j++)
                                    {
                                        if (conflictingColumns.ContainsKey(j))
                                        {
                                            allClientChanges.Rows[i][j] = conflictingColumns[j];
                                        }
                                    }
                                }
                            }

                            //Apply the changed row with its combined values to the server.
                            //This change will persist at the server, but it will not be 
                            //downloaded with the SelectIncrementalUpdate command that we use.
                            //It will not download the change because it checks for the UpdateId,
                            //which is still set to the client that made the upload.
                            //We use the ChangesApplied event to set the UpdateId for the
                            //change to a value that represents the server. This ensures
                            //that the change is applied at the client during the download
                            //phase of synchronization (see SampleServerSyncProvider_ChangesApplied).
                            e.Action = ApplyAction.RetryWithForceWrite;

                            Console.WriteLine(String.Empty);
                            Console.WriteLine("The custom change was retried at the server with RetryWithForceWrite.");

                            break;

                        default:
                            Console.WriteLine(String.Empty);
                            Console.WriteLine("Not a valid resolution option.");
                            
                            break;
                    }
                
                }

                Console.WriteLine(String.Empty);
            }
        }

        private void SampleServerSyncProvider_ChangesApplied(object sender, ChangesAppliedEventArgs e)
        {
            //If _updateConflictGuids contains at least one GUID, update the UpdateId
            //column so that each change is downloaded to the client. For more
            //information, see SampleServerSyncProvider_ApplyChangeFailed.
            if (_updateConflictGuids.Count > 0)
            {
                SqlCommand updateTable = new SqlCommand();
                updateTable.Connection = (SqlConnection)e.Connection;
                updateTable.Transaction = (SqlTransaction)e.Transaction;
                updateTable.CommandText = String.Empty;

                for (int i = 0; i < _updateConflictGuids.Count; i++)
                {
                    updateTable.CommandText +=
                        " UPDATE Sales.Customer SET UpdateId = '00000000-0000-0000-0000-000000000000' " +
                        " WHERE CustomerId='" + _updateConflictGuids[i].ToString() + "'";
                }

                updateTable.ExecuteNonQuery();
            }
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
            //By default, the client database is created if it does not
            //exist.
            Utility util = new Utility();
            this.ConnectionString = util.ClientConnString;

            //Specify conflict resolution options for each type of
            //conflict or error that can occur. The client and server are
            //independent; therefore, these settings have no effect when changes 
            //are applied at the server. However, settings should agree with each
            //other. For example:
            // * We specify a value of ServerWins for client delete /
            //   server update. On the server side, by default our commands will 
            //   ignore the conflicting delete and download the update to the 
            //   client. ServerWins is equivalent to setting RetryWithForceWrite
            //   on the client.
            // * Conversely, we specify a value of ClientWins for client update /
            //   server delete. On the server side, we specify that our commands 
            //   should force write the update by turning it into an insert.
            //<snippetOCS_CS_Conflicts_ConflictResolver>
            this.ConflictResolver.ClientDeleteServerUpdateAction = ResolveAction.ServerWins;            
            this.ConflictResolver.ClientUpdateServerDeleteAction = ResolveAction.ClientWins;
            //If any of the following conflicts or errors occur, the ApplyChangeFailed
            //event is raised.
            this.ConflictResolver.ClientInsertServerInsertAction = ResolveAction.FireEvent;
            this.ConflictResolver.ClientUpdateServerUpdateAction = ResolveAction.FireEvent;
            this.ConflictResolver.StoreErrorAction = ResolveAction.FireEvent;

            //Log information for the ApplyChangeFailed event and handle any
            //ResolveAction.FireEvent cases.
            this.ApplyChangeFailed +=new EventHandler<ApplyChangeFailedEventArgs>(SampleClientSyncProvider_ApplyChangeFailed);
            //</snippetOCS_CS_Conflicts_ConflictResolver>

            //Use the following events to fix up schema on the client.
            //We use the CreatingSchema event to change the schema
            //by using the API. We use the SchemaCreated event 
            //to change the schema by using SQL.
            this.CreatingSchema += new EventHandler<CreatingSchemaEventArgs>(SampleClientSyncProvider_CreatingSchema);
            this.SchemaCreated += new EventHandler<SchemaCreatedEventArgs>(SampleClientSyncProvider_SchemaCreated);

        }

        //<snippetOCS_CS_Conflicts_ClientApplyChangeFailed>
        private void SampleClientSyncProvider_ApplyChangeFailed(object sender, ApplyChangeFailedEventArgs e)
        {

            //Log event data from the client side.
            EventLogger.LogEvents(sender, e);

            //Force write any inserted server rows that are in conflict 
            //when they are downloaded.
            if (e.Conflict.ConflictType == ConflictType.ClientInsertServerInsert)
            {
                e.Action = ApplyAction.RetryWithForceWrite;
            }

            if (e.Conflict.ConflictType == ConflictType.ClientUpdateServerUpdate)
            {
                //Logic goes here.
            }

            if (e.Conflict.ConflictType == ConflictType.ErrorsOccurred)
            {
                //Logic goes here.
            }

        }
        //</snippetOCS_CS_Conflicts_ClientApplyChangeFailed>

        private void SampleClientSyncProvider_CreatingSchema(object sender, CreatingSchemaEventArgs e)
        {
            
            //Set the RowGuid property because it is not copied
            //to the client by default. This is also a good time
            //to specify literal defaults with .Columns[ColName].DefaultValue,
            //but we will specify defaults like NEWID() by calling
            //ALTER TABLE after the table is created.
            e.Schema.Tables["Customer"].Columns["CustomerId"].RowGuid = true;
          
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
    }

    public class EventLogger
    {
        //Create client and server log files, and write to them
        //based on data from the ApplyChangeFailedEventArgs.
        public static void LogEvents(object sender, ApplyChangeFailedEventArgs e)
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

            outputText.AppendLine("** CONFLICTING CHANGE OR ERROR AT " + site.ToUpper() + " **");
            outputText.AppendLine("Table for which error or conflict occurred: " + e.TableMetadata.TableName);
            outputText.AppendLine("Sync stage: " + e.Conflict.SyncStage);
            outputText.AppendLine("Conflict type: " + e.Conflict.ConflictType);

            //If it is a data conflict instead of an error, print out
            //the values of the rows at the client and server.
            if (e.Conflict.ConflictType != ConflictType.ErrorsOccurred && 
                e.Conflict.ConflictType != ConflictType.Unknown)
            {

                DataTable serverChange = e.Conflict.ServerChange;
                DataTable clientChange = e.Conflict.ClientChange;
                int serverRows = serverChange.Rows.Count;
                int clientRows = clientChange.Rows.Count;
                int serverColumns = serverChange.Columns.Count;
                int clientColumns = clientChange.Columns.Count;

                for (int i = 0; i < serverRows; i++)
                {
                    outputText.Append("Server row: ");
                    
                    for (int j = 0; j < serverColumns; j++)
                    {
                        outputText.Append(serverChange.Rows[i][j] + " | ");

                    }

                    outputText.AppendLine(String.Empty);
                }

                for (int i = 0; i < clientRows; i++)
                {
                    outputText.Append("Client row: ");
                    
                    for (int j = 0; j < clientColumns; j++)
                    {
                        outputText.Append(clientChange.Rows[i][j] + " | ");
                    }

                    outputText.AppendLine(String.Empty);
                }
            }

            if (e.Conflict.ConflictType == ConflictType.ErrorsOccurred)
            {
                outputText.AppendLine("Error message: " + e.Error.Message);
            }

            streamWriter.WriteLine(DateTime.Now.ToShortTimeString() + " | " + outputText.ToString());
            streamWriter.Flush();
            streamWriter.Dispose();
            
        }
    }
}
//</snippetOCS_CS_Conflicts>

/*

 * 
 * 
                




                /*
                 * 
                 * 
                 * DataTable customerDataTable = e.Changes.Tables["Customer"];
            
            for (int i = 0; i < customerDataTable.Rows.Count; i++)
            {
                if (customerDataTable.Rows[i].RowState == DataRowState.Added
                    && customerDataTable.Rows[i]["CustomerType"] == "Wholesale")
                {
                    e.Changes.Tables["Customer"].Rows[i]["CustomerType"] = "Wholesale (from mobile sales)";
                }
                 * 
                 * 
                 * 
                 * 
                 * Console.WriteLine(String.Empty);


                DataTable customerDataTable = e.Context.DataSet.Tables["Customer"];

                for (int i = 0; i < customerDataTable.Rows.Count; i++)
                {
                    Console.Write(customerDataTable.Rows[i].RowState);

                    if (customerDataTable.Rows[i].RowState != DataRowState.Deleted)
                    {
                        Console.WriteLine(" | " + customerDataTable.Rows[i]["CustomerId"]
                        + " | " + customerDataTable.Rows[i]["CustomerName"]
                        + " | " + customerDataTable.Rows[i]["SalesPerson"]
                        + " | " + customerDataTable.Rows[i]["CustomerType"]);
                    }
                }  */

