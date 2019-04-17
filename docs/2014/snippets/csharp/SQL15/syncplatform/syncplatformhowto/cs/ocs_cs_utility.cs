using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace Microsoft.Samples.Synchronization
{
//<snippetOCS_CS_Utility>
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

        private static string _serverName = "localhost";
        private static string _serverDbName = "SyncSamplesDb";

        //Set the server and database to connect to. 
        public void SetServerAndDb(string serverName, string serverDbName)
        {
            _serverName = serverName;
            _serverDbName = serverDbName;
        }

        //Return the server connection string. 
        public string ServerConnString
        {

            get { return "Data Source=" + _serverName + "; Initial Catalog=" + _serverDbName + "; Integrated Security=True"; }

        }

        //Make server changes that are synchronized on the second 
        //synchronization.
        public void MakeDataChangesOnServer(string tableName)
        {
            int rowCount = 0;

            using (SqlConnection serverConn = new SqlConnection(this.ServerConnString))
            {
                SqlCommand sqlCommand = serverConn.CreateCommand();

                if (tableName == "Customer")
                {
                    sqlCommand.CommandText =
                        "INSERT INTO Sales.Customer (CustomerName, SalesPerson, CustomerType) " +
                        "VALUES ('Cycle Mart', 'James Bailey', 'Retail') " +

                        "UPDATE Sales.Customer " +
                        "SET  SalesPerson = 'James Bailey' " +
                        "WHERE CustomerName = 'Tandem Bicycle Store' " +

                        "DELETE FROM Sales.Customer WHERE CustomerName = 'Sharp Bikes'";
                }

                else if (tableName == "CustomerContact")
                {
                    sqlCommand.CommandText =
                        "DECLARE @CustomerId uniqueidentifier " +
                        "DECLARE @InsertString nvarchar(1024) " +
                        "SELECT @CustomerId = CustomerId FROM Sales.Customer " +
                        "WHERE CustomerName = 'Tandem Bicycle Store' " +
                        "SET @InsertString = " +
                        "'INSERT INTO Sales.CustomerContact (CustomerId, PhoneNumber, PhoneType) " +
                        "VALUES (''' + CAST(@CustomerId AS nvarchar(38)) + ''', ''959-555-2021'', ''Fax'')' " +
                        "EXECUTE sp_executesql @InsertString " +

                        "SELECT @CustomerId = CustomerId FROM Sales.Customer " +
                        "WHERE CustomerName = 'Rural Cycle Emporium' " +
                        "SET @InsertString = " +
                        "'UPDATE Sales.CustomerContact SET PhoneNumber = ''158-555-0142'' " +
                        "WHERE (CustomerId = ''' + CAST(@CustomerId AS nvarchar(38)) + ''' AND PhoneType = ''Business'')' " +
                        "EXECUTE sp_executesql @InsertString " +

                        "DELETE FROM Sales.CustomerContact WHERE PhoneType = 'Mobile'";
                }

                else if (tableName == "CustomerAndOrderHeader")
                {
                    //Specify the number of rows to insert into the Customer
                    //and OrderHeader tables.
                    sqlCommand.CommandText = "usp_InsertCustomerAndOrderHeader ";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@customer_inserts", SqlDbType.Int);
                    sqlCommand.Parameters["@customer_inserts"].Value = 13;
                    sqlCommand.Parameters.Add("@orderheader_inserts", SqlDbType.Int);
                    sqlCommand.Parameters["@orderheader_inserts"].Value = 33;
                    sqlCommand.Parameters.Add("@sets_of_inserts", SqlDbType.Int);
                    sqlCommand.Parameters["@sets_of_inserts"].Value = 2;
                }

                else if (tableName == "Vendor")
                {
                    sqlCommand.CommandText =
                        "INSERT INTO Sales.Vendor (VendorName, CreditRating, PreferredVendor) " +
                        "VALUES ('Victory Bikes', 4, 0) " +

                        "UPDATE Sales.Vendor " +
                        "SET CreditRating = 2 " +
                        "WHERE VendorName = 'Metro Sport Equipment'" +

                        "DELETE FROM Sales.Vendor " +
                        "WHERE VendorName = 'Premier Sport, Inc.'";
                }

                serverConn.Open();
                rowCount = sqlCommand.ExecuteNonQuery();
                serverConn.Close();
            }

            Console.WriteLine("Rows inserted, updated, or deleted at the server: " + rowCount);
        }

        //Get a dataset to use for schema creation.
        public DataSet CreateDataSetFromServer()
        {
            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            using (SqlConnection serverConn = new SqlConnection(this.ServerConnString))
            {
                SqlCommand createDataSet = serverConn.CreateCommand();
                createDataSet.CommandText =
                    "SELECT OrderId, CustomerId, OrderDate " +
                    "FROM Sales.OrderHeader";                

                serverConn.Open();
                dataAdapter.SelectCommand = createDataSet;
                dataAdapter.FillSchema(dataSet, SchemaType.Source);
                serverConn.Close();
            }

            return dataSet;
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

        //Create the Customer table on the client.
        public void CreateTableOnClient()
        {           
            using(SqlCeConnection clientConn = new SqlCeConnection(this.ClientConnString))
            {
                SqlCeCommand createTable = clientConn.CreateCommand();
                createTable.CommandText =
                    "CREATE TABLE Customer( " +
                    "CustomerId uniqueidentifier NOT NULL " +
                        "PRIMARY KEY DEFAULT NEWID(), " +
                    "CustomerName nvarchar(100) NOT NULL, " +
                    "SalesPerson nvarchar(100) NOT NULL, " +
                    "CustomerType nvarchar(100) NOT NULL, " +
                    "SalesNotes nvarchar(1000) NULL)";

                clientConn.Open();
                createTable.ExecuteNonQuery();
                clientConn.Close();
            }
        }

        //Add DEFAULT constraints on the client.
        public void MakeSchemaChangesOnClient(IDbConnection clientConn, IDbTransaction clientTran, string tableName)
        {

            //Execute the command over the same connection and within
            //the same transaction that Synchronization Services uses
            //to create the schema on the client.
            SqlCeCommand alterTable = new SqlCeCommand();
            alterTable.Connection = (SqlCeConnection)clientConn;
            alterTable.Transaction = (SqlCeTransaction)clientTran;
            alterTable.CommandText = String.Empty;

            //Execute the command, then leave the transaction and 
            //connection open. The client provider will commit and close.
            switch (tableName)
            {
                case "Customer":
                    alterTable.CommandText =
                        "ALTER TABLE Customer " +
                        "ADD CONSTRAINT DF_CustomerId " +
                        "DEFAULT NEWID() FOR CustomerId";
                    alterTable.ExecuteNonQuery();
                    break;

                case "OrderHeader":
                    alterTable.CommandText =
                        "ALTER TABLE OrderHeader " +
                        "ADD CONSTRAINT DF_OrderId " +
                        "DEFAULT NEWID() FOR OrderId";
                    alterTable.ExecuteNonQuery();

                    alterTable.CommandText =
                        "ALTER TABLE OrderHeader " +
                        "ADD CONSTRAINT DF_OrderDate " +
                        "DEFAULT GETDATE() FOR OrderDate";
                    alterTable.ExecuteNonQuery();
                    break;

                case "OrderDetail":
                    alterTable.CommandText =
                        "ALTER TABLE OrderDetail " +
                        "ADD CONSTRAINT DF_Quantity " +
                        "DEFAULT 1 FOR Quantity";
                    alterTable.ExecuteNonQuery();
                    break;

                case "Vendor":
                    alterTable.CommandText =
                        "ALTER TABLE Vendor " +
                        "ADD CONSTRAINT DF_VendorId " +
                        "DEFAULT NEWID() FOR VendorId";
                    alterTable.ExecuteNonQuery();
                    break;
            }       

        }

        //Make client changes that are synchronized on the second 
        //synchronization.
        public void MakeDataChangesOnClient(string tableName)
        {
            int rowCount = 0;

            using (SqlCeConnection clientConn = new SqlCeConnection(this.ClientConnString))
            {

                SqlCeCommand sqlCeCommand = clientConn.CreateCommand();

                clientConn.Open();

                if (tableName == "Customer")
                {
                    sqlCeCommand.CommandText =
                        "INSERT INTO Customer (CustomerName, SalesPerson, CustomerType) " +
                        "VALUES ('Cycle Merchants', 'Brenda Diaz', 'Wholesale') ";
                    rowCount = sqlCeCommand.ExecuteNonQuery();

                    sqlCeCommand.CommandText =
                        "UPDATE Customer " +
                        "SET SalesPerson = 'Brenda Diaz' " +
                        "WHERE CustomerName = 'Exemplary Cycles'";
                    rowCount += sqlCeCommand.ExecuteNonQuery();

                    sqlCeCommand.CommandText =
                        "DELETE FROM Customer " +
                        "WHERE CustomerName = 'Aerobic Exercise Company'";
                    rowCount += sqlCeCommand.ExecuteNonQuery();
                }

                else if (tableName == "Vendor")
                {
                    sqlCeCommand.CommandText =
                        "INSERT INTO Vendor (VendorName, CreditRating, PreferredVendor) " +
                        "VALUES ('Cycling Master', 2, 1) ";
                    rowCount = sqlCeCommand.ExecuteNonQuery();

                    sqlCeCommand.CommandText =
                        "UPDATE Vendor " +
                        "SET CreditRating = 2 " +
                        "WHERE VendorName = 'Mountain Works'";
                    rowCount += sqlCeCommand.ExecuteNonQuery();

                    sqlCeCommand.CommandText =
                        "DELETE FROM Vendor " +
                        "WHERE VendorName = 'Compete, Inc.'";
                    rowCount += sqlCeCommand.ExecuteNonQuery();
                }

                clientConn.Close();

            }

            Console.WriteLine("Rows inserted, updated, or deleted at the client: " + rowCount);
        }

        //Make a change at the client that fails when it is
        //applied at the server.
        public void MakeFailingChangesOnClient()
        {
            int rowCount = 0;

            using (SqlCeConnection clientConn = new SqlCeConnection(this.ClientConnString))
            {

                SqlCeCommand sqlCeCommand = clientConn.CreateCommand();

                clientConn.Open();

                sqlCeCommand.CommandText =
                    "DELETE FROM Customer " +
                    "WHERE CustomerName = 'Rural Cycle Emporium'";
                rowCount += sqlCeCommand.ExecuteNonQuery();

                clientConn.Close();

            }

            Console.WriteLine("Rows deleted at the client that will fail at the server: " + rowCount);
        }

        //Make changes at the client and server that conflict
        //when they are synchronized.
        public void MakeConflictingChangesOnClientAndServer()
        {
            int rowCount = 0;

            using (SqlConnection serverConn = new SqlConnection(this.ServerConnString))
            {
                SqlCommand sqlCommand = serverConn.CreateCommand();
                sqlCommand.CommandText =
                    "INSERT INTO Sales.Customer (CustomerId, CustomerName, SalesPerson, CustomerType) " +
                    "VALUES ('009aa4b6-3433-4136-ad9a-a7e1bb2528f7', 'Cycle Merchants', 'Brenda Diaz', 'Wholesale') " +
                
                    "DELETE FROM Sales.Customer WHERE CustomerName = 'Aerobic Exercise Company' " +
                    
                    "UPDATE Sales.Customer " +
                    "SET SalesPerson = 'James Bailey' " +
                    "WHERE CustomerName = 'Sharp Bikes' " +

                    "UPDATE Sales.Customer " +
                    "SET CustomerType = 'Distributor' " +
                    "WHERE CustomerName = 'Exemplary Cycles'"; 

                serverConn.Open();
                rowCount = sqlCommand.ExecuteNonQuery();
                serverConn.Close();
            }

            using (SqlCeConnection clientConn = new SqlCeConnection(this.ClientConnString))
            {

                SqlCeCommand sqlCeCommand = clientConn.CreateCommand();

                clientConn.Open();               

                sqlCeCommand.CommandText =
                   "INSERT INTO Customer (CustomerId, CustomerName, SalesPerson, CustomerType) " +
                   "VALUES ('009aa4b6-3433-4136-ad9a-a7e1bb2528f7', 'Cycle Merchants', 'James Bailey', 'Wholesale')";
                rowCount += sqlCeCommand.ExecuteNonQuery();

                sqlCeCommand.CommandText =
                    "UPDATE Customer " +
                    "SET CustomerType = 'Retail' " +
                    "WHERE CustomerName = 'Aerobic Exercise Company'";
                rowCount += sqlCeCommand.ExecuteNonQuery();

                sqlCeCommand.CommandText =
                   "DELETE FROM Customer WHERE CustomerName = 'Sharp Bikes'";
                rowCount += sqlCeCommand.ExecuteNonQuery();

                sqlCeCommand.CommandText =
                    "UPDATE Customer " +
                    "SET CustomerType = 'Wholesale' " +
                    "WHERE CustomerName = 'Exemplary Cycles'"; 
                rowCount += sqlCeCommand.ExecuteNonQuery();

                clientConn.Close();
            }

            Console.WriteLine("Conflicting rows inserted, updated, or deleted at the client or server: " + rowCount);
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
//</snippetOCS_CS_Utility>