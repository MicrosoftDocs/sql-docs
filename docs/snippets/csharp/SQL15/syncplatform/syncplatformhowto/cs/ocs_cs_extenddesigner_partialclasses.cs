//<snippetOCS_CS_ExtendDesigner_PartialClasses>
namespace OCSWalkthroughCS1 {

    //<snippetOCS_CS_ExtendDesigner_PartialClasses_NorthwindCacheSyncAgent>
    public partial class NorthwindCacheSyncAgent 
    {   
        partial void OnInitialized()
        {
            this.Customers.SyncDirection = 
                Microsoft.Synchronization.Data.SyncDirection.Bidirectional;
        }
    }
    //</snippetOCS_CS_ExtendDesigner_PartialClasses_NorthwindCacheSyncAgent>

    //<snippetOCS_CS_ExtendDesigner_PartialClasses_NorthwindCacheServerSyncProvider>
    public partial class NorthwindCacheServerSyncProvider
    {

        partial void OnInitialized()
        {
            this.ApplyChangeFailed +=
                new System.EventHandler<Microsoft.Synchronization.Data.ApplyChangeFailedEventArgs>
                (NorthwindCacheServerSyncProvider_ApplyChangeFailed);
        }
        
        private void NorthwindCacheServerSyncProvider_ApplyChangeFailed(object sender,
            Microsoft.Synchronization.Data.ApplyChangeFailedEventArgs e)
        {

        if (e.Conflict.ConflictType ==
            Microsoft.Synchronization.Data.ConflictType.ClientUpdateServerUpdate)
            {

            //Resolve a client update / server update conflict by force writing
            //the client change to the server database.
            System.Windows.Forms.MessageBox.Show("A client update / server update conflict " +
                                                    "was detected at the server.");
            e.Action = Microsoft.Synchronization.Data.ApplyAction.RetryWithForceWrite;

            }

        }
    }

    public partial class NorthwindCacheClientSyncProvider
    {

        public void AddHandlers()
        {
            this.ApplyChangeFailed +=
                new System.EventHandler<Microsoft.Synchronization.Data.ApplyChangeFailedEventArgs>
                (NorthwindCacheClientSyncProvider_ApplyChangeFailed);
        }

        private void NorthwindCacheClientSyncProvider_ApplyChangeFailed(object sender,
            Microsoft.Synchronization.Data.ApplyChangeFailedEventArgs e)
        {

            if (e.Conflict.ConflictType ==
                Microsoft.Synchronization.Data.ConflictType.ClientUpdateServerUpdate)
            {

                //Resolve a client update / server update conflict by keeping the 
                //client change.
                e.Action = Microsoft.Synchronization.Data.ApplyAction.Continue;

            }

        }
    }
    //</snippetOCS_CS_ExtendDesigner_PartialClasses_NorthwindCacheServerSyncProvider>

    //<snippetOCS_CS_ExtendDesigner_PartialClasses_CustomersSyncAdapter>
    public partial class CustomersSyncAdapter
    {

        partial void OnInitialized()
        {

        //Redefine the insert command so that it does not insert values 
        //into the CreationDate and LastEditDate columns.
        System.Data.SqlClient.SqlCommand insertCommand = new System.Data.SqlClient.SqlCommand();
        
        insertCommand.CommandText = "INSERT INTO dbo.Customers ([CustomerID], [CompanyName], " +
            "[ContactName], [ContactTitle], [Address], [City], [Region], [PostalCode], " +
            "[Country], [Phone], [Fax] )" +
            "VALUES (@CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, " +
            "@Region, @PostalCode, @Country, @Phone, @Fax) SET @sync_row_count = @@rowcount";
        insertCommand.CommandType = System.Data.CommandType.Text;
        insertCommand.Parameters.Add("@CustomerID", System.Data.SqlDbType.NChar);
        insertCommand.Parameters.Add("@CompanyName", System.Data.SqlDbType.NVarChar);
        insertCommand.Parameters.Add("@ContactName", System.Data.SqlDbType.NVarChar);
        insertCommand.Parameters.Add("@ContactTitle", System.Data.SqlDbType.NVarChar);
        insertCommand.Parameters.Add("@Address", System.Data.SqlDbType.NVarChar);
        insertCommand.Parameters.Add("@City", System.Data.SqlDbType.NVarChar);
        insertCommand.Parameters.Add("@Region", System.Data.SqlDbType.NVarChar);
        insertCommand.Parameters.Add("@PostalCode", System.Data.SqlDbType.NVarChar);
        insertCommand.Parameters.Add("@Country", System.Data.SqlDbType.NVarChar);
        insertCommand.Parameters.Add("@Phone", System.Data.SqlDbType.NVarChar);
        insertCommand.Parameters.Add("@Fax", System.Data.SqlDbType.NVarChar);
        insertCommand.Parameters.Add("@sync_row_count", System.Data.SqlDbType.Int);
        insertCommand.Parameters["@sync_row_count"].Direction = 
            System.Data.ParameterDirection.Output;

        this.InsertCommand = insertCommand;


        //Redefine the update command so that it does not update values 
        //in the CreationDate and LastEditDate columns.
        System.Data.SqlClient.SqlCommand updateCommand = new System.Data.SqlClient.SqlCommand();

        updateCommand.CommandText = "UPDATE dbo.Customers SET [CompanyName] = @CompanyName, [ContactName] " +
            "= @ContactName, [ContactTitle] = @ContactTitle, [Address] = @Address, [City] " +
            "= @City, [Region] = @Region, [PostalCode] = @PostalCode, [Country] = @Country, " +
            "[Phone] = @Phone, [Fax] = @Fax " +
            "WHERE ([CustomerID] = @CustomerID) AND (@sync_force_write = 1 " +
            "OR ([LastEditDate] <= @sync_last_received_anchor)) SET @sync_row_count = @@rowcount";
        updateCommand.CommandType = System.Data.CommandType.Text;
        updateCommand.Parameters.Add("@CompanyName", System.Data.SqlDbType.NVarChar);
        updateCommand.Parameters.Add("@ContactName", System.Data.SqlDbType.NVarChar);
        updateCommand.Parameters.Add("@ContactTitle", System.Data.SqlDbType.NVarChar);
        updateCommand.Parameters.Add("@Address", System.Data.SqlDbType.NVarChar);
        updateCommand.Parameters.Add("@City", System.Data.SqlDbType.NVarChar);
        updateCommand.Parameters.Add("@Region", System.Data.SqlDbType.NVarChar);
        updateCommand.Parameters.Add("@PostalCode", System.Data.SqlDbType.NVarChar);
        updateCommand.Parameters.Add("@Country", System.Data.SqlDbType.NVarChar);
        updateCommand.Parameters.Add("@Phone", System.Data.SqlDbType.NVarChar);
        updateCommand.Parameters.Add("@Fax", System.Data.SqlDbType.NVarChar);
        updateCommand.Parameters.Add("@CustomerID", System.Data.SqlDbType.NChar);
        updateCommand.Parameters.Add("@sync_force_write", System.Data.SqlDbType.Bit);
        updateCommand.Parameters.Add("@sync_last_received_anchor", System.Data.SqlDbType.DateTime);
        updateCommand.Parameters.Add("@sync_row_count", System.Data.SqlDbType.Int);
        updateCommand.Parameters["@sync_row_count"].Direction = 
            System.Data.ParameterDirection.Output;

        this.UpdateCommand = updateCommand;

        }
    }
    //</snippetOCS_CS_ExtendDesigner_PartialClasses_CustomersSyncAdapter>
}
//</snippetOCS_CS_ExtendDesigner_PartialClasses>
