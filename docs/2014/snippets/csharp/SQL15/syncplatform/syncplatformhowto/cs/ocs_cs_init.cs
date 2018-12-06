//<snippetOCS_CS_Init>
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

            //Create the Customer table on the client by using SQL. We add
            //a SalesNotes column that will not be synchronized.
            //When we create the Customer SyncTable, we specify that
            //Synchronization Services should use an existing table.          
            util.CreateTableOnClient();

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

            //Create two SyncGroups so that changes to OrderHeader
            //and OrderDetail are made in one transaction. Depending on
            //application requirements, you might include Customer
            //and CustomerContact in the same group.
            SyncGroup customerSyncGroup = new SyncGroup("Customer");
            SyncGroup orderSyncGroup = new SyncGroup("Order");

            //Add each table: specify a synchronization direction of
            //Bidirectional. We create the Customer table before sync:
            //we set CreationOption to UseExistingTableOrFail so
            //we are sure that the table exists.
            SyncTable customerSyncTable = new SyncTable("Customer");
            customerSyncTable.CreationOption = TableCreationOption.UseExistingTableOrFail;
            customerSyncTable.SyncDirection = SyncDirection.Bidirectional;
            customerSyncTable.SyncGroup = customerSyncGroup;
            this.Configuration.SyncTables.Add(customerSyncTable);

            SyncTable customerContactSyncTable = new SyncTable("CustomerContact");
            customerContactSyncTable.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            customerContactSyncTable.SyncDirection = SyncDirection.Bidirectional;
            customerContactSyncTable.SyncGroup = customerSyncGroup;
            this.Configuration.SyncTables.Add(customerContactSyncTable);

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
            //  * Specify bidirectional synchronization, so that all
            //    commands are generated.
            //  * Call ToSyncAdapter to create the SyncAdapter.
            //  * Specify a name for the SyncAdapter that matches the
            //    the name specified for the corresponding SyncTable.
            //    Do not include the schema names (Sales in this case).
                        
            //Customer table
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


            //CustomerContact table.
            SqlSyncAdapterBuilder customerContactBuilder = new SqlSyncAdapterBuilder(serverConn);

            customerContactBuilder.TableName = "Sales.CustomerContact";
            customerContactBuilder.TombstoneTableName = customerContactBuilder.TableName + "_Tombstone";
            customerContactBuilder.SyncDirection = SyncDirection.Bidirectional;
            customerContactBuilder.CreationTrackingColumn = "InsertTimestamp";
            customerContactBuilder.UpdateTrackingColumn = "UpdateTimestamp";
            customerContactBuilder.DeletionTrackingColumn = "DeleteTimestamp";
            customerContactBuilder.CreationOriginatorIdColumn = "InsertId";
            customerContactBuilder.UpdateOriginatorIdColumn = "UpdateId";
            customerContactBuilder.DeletionOriginatorIdColumn = "DeleteId";
            
            SyncAdapter customerContactSyncAdapter = customerContactBuilder.ToSyncAdapter();
            customerContactSyncAdapter.TableName = "CustomerContact";
            this.SyncAdapters.Add(customerContactSyncAdapter);


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

            //Create the schema for the OrderHeader and OrderDetail tables.
            //We first create a schema based on a DataSet that contains only
            //the OrderHeader table. As with the SyncAdapter, the table name
            //must match the SyncTable name. We then add the schema for the 
            //OrderDetail table; this is the place to map data types if
            //your application requires it.
            //<snippetOCS_CS_Init_SyncSchema>
            DataSet orderHeaderDataSet = util.CreateDataSetFromServer();
            orderHeaderDataSet.Tables[0].TableName = "OrderHeader";
            this.Schema = new SyncSchema(orderHeaderDataSet);
            
            this.Schema.Tables.Add("OrderDetail");

            this.Schema.Tables["OrderDetail"].Columns.Add("OrderDetailId");
            this.Schema.Tables["OrderDetail"].Columns["OrderDetailId"].ProviderDataType = "int";
            this.Schema.Tables["OrderDetail"].Columns["OrderDetailId"].AllowNull = false;

            this.Schema.Tables["OrderDetail"].Columns.Add("OrderId");
            this.Schema.Tables["OrderDetail"].Columns["OrderId"].ProviderDataType = "uniqueidentifier";
            this.Schema.Tables["OrderDetail"].Columns["OrderId"].RowGuid = true;
            this.Schema.Tables["OrderDetail"].Columns["OrderId"].AllowNull = false;

            this.Schema.Tables["OrderDetail"].Columns.Add("Product");
            this.Schema.Tables["OrderDetail"].Columns["Product"].ProviderDataType = "nvarchar";
            this.Schema.Tables["OrderDetail"].Columns["Product"].MaxLength = 100;
            this.Schema.Tables["OrderDetail"].Columns["Product"].AllowNull = false;

            this.Schema.Tables["OrderDetail"].Columns.Add("Quantity");
            this.Schema.Tables["OrderDetail"].Columns["Quantity"].ProviderDataType = "int";
            this.Schema.Tables["OrderDetail"].Columns["Quantity"].AllowNull = false;
           
            //The primary key columns are passed as a string array.
            string[] orderDetailPrimaryKey = new string[2];
            orderDetailPrimaryKey[0] = "OrderDetailId";
            orderDetailPrimaryKey[1] = "OrderId";
            this.Schema.Tables["OrderDetail"].PrimaryKey = orderDetailPrimaryKey;
            //</snippetOCS_CS_Init_SyncSchema>
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
            //by using the API. We use the SchemaCreated event 
            //to change the schema by using SQL.
            //Note that both schema events fire for the Customer table,
            //even though we already created the table. This allows us
            //to work with the table at this point if we need to.
            this.CreatingSchema += new EventHandler<CreatingSchemaEventArgs>(SampleClientSyncProvider_CreatingSchema);
            this.SchemaCreated += new EventHandler<SchemaCreatedEventArgs>(SampleClientSyncProvider_SchemaCreated);
        }

        private void SampleClientSyncProvider_CreatingSchema(object sender, CreatingSchemaEventArgs e)
        {
            
            string tableName = e.Table.TableName;
            
            Console.Write("Creating schema for " + tableName + " | ");
            
            if (tableName == "OrderHeader")
            {
                //Set the RowGuid property because it is not copied
                //to the client by default. This is also a good time
                //to specify literal defaults with .Columns[ColName].DefaultValue,
                //but we will specify defaults like NEWID() by calling
                //ALTER TABLE after the table is created.
                e.Schema.Tables["OrderHeader"].Columns["OrderId"].RowGuid = true;
                
            }

            if (tableName == "OrderDetail")
            {
                //Add a foreign key between the OrderDetail and OrderHeader tables.
                e.Schema.Tables["OrderDetail"].ForeignKeys.Add("FK_OrderDetail_OrderHeader", "OrderHeader", "OrderId", "OrderDetail", "OrderId");
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
            if (tableName == "OrderHeader")
            {
                util.MakeSchemaChangesOnClient(e.Connection, e.Transaction, "OrderHeader");                
            }

            if (tableName == "OrderDetail")
            {
                util.MakeSchemaChangesOnClient(e.Connection, e.Transaction, "OrderDetail");                
            }

            Console.WriteLine("Schema created for " + tableName);
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
//</snippetOCS_CS_Init>

//Create foreign keys between each table.
//this.Schema.Tables["OrderHeader"].ForeignKeys.Add("FK_OrderHeader_Customer", "Customer", "CustomerId", "OrderHeader", "CustomerId");
//this.Schema.Tables["OrderDetail"].ForeignKeys.Add("FK_OrderDetail_OrderHeader", "OrderHeader", "OrderId", "OrderDetail", "OrderId");

//this.Schema.Tables.Add("Customer");
//this.Schema.Tables.Add("CustomerContact");
//e.Schema.Tables["OrderHeader"].ForeignKeys.Add("FK_OrderHeader_Customer", "Customer", "CustomerId", "OrderHeader", "CustomerId");
