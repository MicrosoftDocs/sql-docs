---
title: "Using Bulk Copy with the JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "07/11/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 21e19635-340d-49bb-b39d-4867102fb5df
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Bulk Copy with the JDBC Driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Microsoft SQL Server includes a popular command-line utility named **bcp** for quickly bulk copying large files into tables or views in SQL Server databases. The SQLServerBulkCopy class allows you to write code solutions in Java that provide similar functionality. There are other ways to load data into a SQL Server table (INSERT statements, for example) but SQLServerBulkCopy offers a significant performance advantage over them.  
  
The SQLServerBulkCopy class can be used to write data only to SQL Server tables. But the data source isn't limited to SQL Server; any data source can be used, as long as the data can be read with a ResultSet, RowSet, or ISQLServerBulkRecord implementation.  
  
Using the SQLServerBulkCopy class, you can perform:  
  
- A single bulk copy operation  
  
- Multiple bulk copy operations  
  
- A bulk copy operation with a transaction  
  
> [!NOTE]  
> When using the Microsoft JDBC Driver 4.1 for SQL Server or earlier (which does not support the SQLServerBulkCopy class), you can execute the SQL Server Transact-SQL BULK INSERT statement instead.  
  
## Bulk copy example setup  

The SQLServerBulkCopy class can be used to write data only to SQL Server tables. The code samples shown in this article use the SQL Server sample database, AdventureWorks. To avoid altering the existing tables code samples write data to tables that you must create first.  
  
The BulkCopyDemoMatchingColumns and BulkCopyDemoDifferentColumns tables are both based on the AdventureWorks Production.Products table. In code samples that use these tables, data is added from the Production.Products table to one of these sample tables. The BulkCopyDemoDifferentColumns table is used when the sample illustrates how to map columns from the source data to the destination table; BulkCopyDemoMatchingColumns is used for most other samples.  
  
A few of the code samples demonstrate how to use one SQLServerBulkCopy class to write to multiple tables. For these samples, the BulkCopyDemoOrderHeader and BulkCopyDemoOrderDetail tables are used as the destination tables. These tables are based on the Sales.SalesOrderHeader and Sales.SalesOrderDetail tables in AdventureWorks.  
  
> [!NOTE]  
> The SQLServerBulkCopy code samples are provided to demonstrate the syntax for using SQLServerBulkCopy only. If the source and destination tables are located in the same SQL Server instance, it is easier and faster to use a Transact-SQL INSERT ... SELECT statement to copy the data.  

### Table setup  

To create the tables necessary for the code samples to run correctly, you must run the following Transact-SQL statements in a SQL Server database.  

```sql
USE AdventureWorks  
  
IF EXISTS (SELECT * FROM dbo.sysobjects
 WHERE id = object_id(N'[dbo].[BulkCopyDemoMatchingColumns]')
 AND OBJECTPROPERTY(id, N'IsUserTable') = 1)  
    DROP TABLE [dbo].[BulkCopyDemoMatchingColumns]  
  
CREATE TABLE [dbo].[BulkCopyDemoMatchingColumns]([ProductID] [int] IDENTITY(1,1) NOT NULL,  
    [Name] [nvarchar](50) NOT NULL,  
    [ProductNumber] [nvarchar](25) NOT NULL,  
 CONSTRAINT [PK_ProductID] PRIMARY KEY CLUSTERED
(  
    [ProductID] ASC  
) ON [PRIMARY]) ON [PRIMARY]  
  
IF EXISTS (SELECT * FROM dbo.sysobjects
 WHERE id = object_id(N'[dbo].[BulkCopyDemoDifferentColumns]')
 AND OBJECTPROPERTY(id, N'IsUserTable') = 1)  
    DROP TABLE [dbo].[BulkCopyDemoDifferentColumns]  
  
CREATE TABLE [dbo].[BulkCopyDemoDifferentColumns]([ProdID] [int] IDENTITY(1,1) NOT NULL,  
    [ProdNum] [nvarchar](25) NOT NULL,  
    [ProdName] [nvarchar](50) NOT NULL,  
 CONSTRAINT [PK_ProdID] PRIMARY KEY CLUSTERED
(  
    [ProdID] ASC  
) ON [PRIMARY]) ON [PRIMARY]  
  
IF EXISTS (SELECT * FROM dbo.sysobject
 WHERE id = object_id(N'[dbo].[BulkCopyDemoOrderHeader]')
 AND OBJECTPROPERTY(id, N'IsUserTable') = 1)  
    DROP TABLE [dbo].[BulkCopyDemoOrderHeader]  
  
CREATE TABLE [dbo].[BulkCopyDemoOrderHeader]([SalesOrderID] [int] IDENTITY(1,1) NOT NULL,  
    [OrderDate] [datetime] NOT NULL,  
    [AccountNumber] [nvarchar](15) NULL,  
 CONSTRAINT [PK_SalesOrderID] PRIMARY KEY CLUSTERED
(  
    [SalesOrderID] ASC  
) ON [PRIMARY]) ON [PRIMARY]  
  
IF EXISTS (SELECT * FROM dbo.sysobjects
 WHERE id = object_id(N'[dbo].[BulkCopyDemoOrderDetail]')
 AND OBJECTPROPERTY(id, N'IsUserTable') = 1)  
    DROP TABLE [dbo].[BulkCopyDemoOrderDetail]  
  
CREATE TABLE [dbo].[BulkCopyDemoOrderDetail]([SalesOrderID] [int] NOT NULL,  
    [SalesOrderDetailID] [int] NOT NULL,  
    [OrderQty] [smallint] NOT NULL,  
    [ProductID] [int] NOT NULL,  
    [UnitPrice] [money] NOT NULL,  
 CONSTRAINT [PK_LineNumber] PRIMARY KEY CLUSTERED
(  
    [SalesOrderID] ASC,  
    [SalesOrderDetailID] ASC  
) ON [PRIMARY]) ON [PRIMARY]  
  
```

## Single bulk copy operations

The simplest approach to performing a SQL Server bulk copy operation is to perform a single operation against a database. By default, a bulk copy operation is performed as an isolated operation: the copy operation occurs in a non-transacted way, with no opportunity for rolling it back.  
  
> [!NOTE]  
> If you need to roll back all or part of the bulk copy when an error occurs, you can either use a SQLServerBulkCopy-managed transaction, or perform the bulk copy operation within an existing transaction.  
> For more information, see [Transaction and bulk copy operations](../../connect/jdbc/using-bulk-copy-with-the-jdbc-driver.md#BKMK_TransactionBulk)  
  
 The general steps to perform a bulk copy operation are:  
  
1. Connect to the source server and obtain the data to be copied. Data can also come from other sources, if it can be retrieved from a ResultSet object or an ISQLServerBulkRecord implementation.  
  
2. Connect to the destination server (unless you want **SQLServerBulkCopy** to establish a connection for you).  
  
3. Create a SQLServerBulkCopy object, setting any necessary properties via **setBulkCopyOptions**.  
  
4. Call the **setDestinationTableName** method to indicate the target table for the bulk insert operation.  
  
5. Call one of the **writeToServer** methods.  
  
6. Optionally, update properties via **setBulkCopyOptions** and call **writeToServer** again as necessary.  
  
7. Call **close**, or wrap the bulk copy operations within a try-with-resources statement.  
  
> [!CAUTION]  
> We recommend that the source and target column data types match. If the data types do not match, SQLServerBulkCopy attempts to convert each source value to the target data type. Conversions can affect performance, and also can result in unexpected errors. For example, a double data type can be converted to a decimal data type most of the time, but not always.  
  
### Example

The following application demonstrates how to load data using the SQLServerBulkCopy class. In this example, a ResultSet is used to copy data from the Production.Product table in the SQL Server AdventureWorks database to a similar table in the same database.  
  
> [!IMPORTANT]  
> This sample will not run unless you have created the work tables as described in [Table setup](../../connect/jdbc/using-bulk-copy-with-the-jdbc-driver.md#BKMK_TableSetup). This code is provided to demonstrate the syntax for using SQLServerBulkCopy only. If the source and destination tables are located in the same SQL Server instance, it is easier and faster to use a Transact-SQL INSERT ... SELECT statement to copy the data.  

```java
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerBulkCopy;

public class BulkCopySingle {
    public static void main(String[] args) {
        String connectionUrl = "jdbc:sqlserver://<server>:<port>;databaseName=AdventureWorks;user=<user>;password=<password>";
        String destinationTable = "dbo.BulkCopyDemoMatchingColumns";
        int countBefore, countAfter;
        ResultSet rsSourceData;

        try (Connection sourceConnection = DriverManager.getConnection(connectionUrl);
                Connection destinationConnection = DriverManager.getConnection(connectionUrl);
                Statement stmt = sourceConnection.createStatement();
                SQLServerBulkCopy bulkCopy = new SQLServerBulkCopy(destinationConnection)) {

            // Empty the destination table.
            stmt.executeUpdate("DELETE FROM " + destinationTable);

            // Perform an initial count on the destination table.
            countBefore = getRowCount(stmt, destinationTable);

            // Get data from the source table as a ResultSet.
            rsSourceData = stmt.executeQuery("SELECT ProductID, Name, ProductNumber FROM Production.Product");

            // In real world applications you would
            // not use SQLServerBulkCopy to move data from one table to the other
            // in the same database. This is for demonstration purposes only.

            // Set up the bulk copy object.
            // Note that the column positions in the source
            // table match the column positions in
            // the destination table so there is no need to
            // map columns.
            bulkCopy.setDestinationTableName(destinationTable);

            // Write from the source to the destination.
            bulkCopy.writeToServer(rsSourceData);

            // Perform a final count on the destination
            // table to see how many rows were added.
            countAfter = getRowCount(stmt, destinationTable);
            System.out.println((countAfter - countBefore) + " rows were added.");
        }
        // Handle any errors that may have occurred.
        catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private static int getRowCount(Statement stmt,
            String tableName) throws SQLException {
        ResultSet rs = stmt.executeQuery("SELECT COUNT(*) FROM " + tableName);
        rs.next();
        int count = rs.getInt(1);
        rs.close();
        return count;
    }
}
```

### Performing a bulk copy operation using Transact-SQL

The following example illustrates how to use the executeUpdate method to execute the BULK INSERT statement.  
  
> [!NOTE]  
> The file path for the data source is relative to the server. The server process must have access to that path in order for the bulk copy operation to succeed.  

```java
try (Connection con = DriverManager.getConnection(connectionUrl);
        Statement stmt = con.createStatement()) {
    // Perform the BULK INSERT
    stmt.executeUpdate(
            "BULK INSERT Northwind.dbo.[Order Details] " + "FROM 'f:\\mydata\\data.tbl' " + "WITH ( FORMATFILE='f:\\mydata\\data.fmt' )");
}
```  

## Multiple bulk copy operations  

You can perform multiple bulk copy operations using a single instance of a SQLServerBulkCopy class. If the operation parameters change between copies (for example, the name of the destination table), you must update them prior to any subsequent calls to any of the writeToServer methods, as demonstrated in the following example. Unless explicitly changed, all property values remain the same as they were on the previous bulk copy operation for a given instance.  

> [!NOTE]  
> Performing multiple bulk copy operations using the same instance of SQLServerBulkCopy is usually more efficient than using a separate instance for each operation.  
  
If you perform several bulk copy operations using the same SQLServerBulkCopy object, there are no restrictions on whether source or target information is equal or different in each operation. However, you must ensure that column association information is properly set each time you write to the server.  
  
> [!IMPORTANT]  
> This sample will not run unless you have created the work tables as described in [Table setup](../../connect/jdbc/using-bulk-copy-with-the-jdbc-driver.md#BKMK_TableSetup). This code is provided to demonstrate the syntax for using SQLServerBulkCopy only. If the source and destination tables are located in the same SQL Server instance, it is easier and faster to use a Transact-SQL INSERT ... SELECT statement to copy the data.  

```java
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerBulkCopy;
import com.microsoft.sqlserver.jdbc.SQLServerBulkCopyOptions;

public class BulkCopyMultiple {
    public static void main(String[] args) {
        String connectionUrl = "jdbc:sqlserver://<server>:<port>;databaseName=AdventureWorks;user=<user>;password=<password>";
        String destinationHeaderTable = "dbo.BulkCopyDemoOrderHeader";
        String destinationDetailTable = "dbo.BulkCopyDemoOrderDetail";
        int countHeaderBefore, countDetailBefore, countHeaderAfter, countDetailAfter;
        ResultSet rsHeader, rsDetail;

        try (Connection sourceConnection1 = DriverManager.getConnection(connectionUrl);
                Connection sourceConnection2 = DriverManager.getConnection(connectionUrl);
                Statement stmt = sourceConnection1.createStatement();
                PreparedStatement preparedStmt1 = sourceConnection1.prepareStatement(
                        "SELECT [SalesOrderID], [OrderDate], [AccountNumber] FROM [Sales].[SalesOrderHeader] WHERE [AccountNumber] = ?;");
                PreparedStatement preparedStmt2 = sourceConnection2.prepareStatement(
                        "SELECT [Sales].[SalesOrderDetail].[SalesOrderID], [SalesOrderDetailID], [OrderQty], [ProductID], [UnitPrice] FROM "
                                + "[Sales].[SalesOrderDetail] INNER JOIN [Sales].[SalesOrderHeader] ON "
                                + "[Sales].[SalesOrderDetail].[SalesOrderID] = [Sales].[SalesOrderHeader].[SalesOrderID] WHERE [AccountNumber] = ?;");
                SQLServerBulkCopy bulkCopy = new SQLServerBulkCopy(connectionUrl);) {

            // Empty the destination tables.
            stmt.executeUpdate("DELETE FROM " + destinationHeaderTable);
            stmt.executeUpdate("DELETE FROM " + destinationDetailTable);

            // Perform an initial count on the destination
            // table with matching columns.
            countHeaderBefore = getRowCount(stmt, destinationHeaderTable);

            // Perform an initial count on the destination
            // table with different column positions.
            countDetailBefore = getRowCount(stmt, destinationDetailTable);

            // Get data from the source table as a ResultSet.
            // The Sales.SalesOrderHeader and Sales.SalesOrderDetail
            // tables are quite large and could easily cause a timeout
            // if all data from the tables is added to the destination.
            // To keep the example simple and quick, a parameter is
            // used to select only orders for a particular account
            // as the source for the bulk insert.
            preparedStmt1.setString(1, "10-4020-000034");
            rsHeader = preparedStmt1.executeQuery();

            // Get the Detail data in a separate connection.
            preparedStmt2.setString(1, "10-4020-000034");
            rsDetail = preparedStmt2.executeQuery();

            // Create the SQLServerBulkCopySQLServerBulkCopy object.
            SQLServerBulkCopyOptions copyOptions = new SQLServerBulkCopyOptions();
            copyOptions.setBulkCopyTimeout(100);
            bulkCopy.setBulkCopyOptions(copyOptions);
            bulkCopy.setDestinationTableName(destinationHeaderTable);

            // Guarantee that columns are mapped correctly by
            // defining the column mappings for the order.
            bulkCopy.addColumnMapping("SalesOrderID", "SalesOrderID");
            bulkCopy.addColumnMapping("OrderDate", "OrderDate");
            bulkCopy.addColumnMapping("AccountNumber", "AccountNumber");

            // Write rsHeader to the destination.
            bulkCopy.writeToServer(rsHeader);

            // Set up the order details destination.
            bulkCopy.setDestinationTableName(destinationDetailTable);

            // Clear the existing column mappings
            bulkCopy.clearColumnMappings();

            // Add order detail column mappings.
            bulkCopy.addColumnMapping("SalesOrderID", "SalesOrderID");
            bulkCopy.addColumnMapping("SalesOrderDetailID", "SalesOrderDetailID");
            bulkCopy.addColumnMapping("OrderQty", "OrderQty");
            bulkCopy.addColumnMapping("ProductID", "ProductID");
            bulkCopy.addColumnMapping("UnitPrice", "UnitPrice");

            // Write rsDetail to the destination.
            bulkCopy.writeToServer(rsDetail);

            // Perform a final count on the destination
            // tables to see how many rows were added.
            countHeaderAfter = getRowCount(stmt, destinationHeaderTable);
            countDetailAfter = getRowCount(stmt, destinationDetailTable);

            System.out.println((countHeaderAfter - countHeaderBefore) + " rows were added to the Header table.");
            System.out.println((countDetailAfter - countDetailBefore) + " rows were added to the Detail table.");
        }
        // Handle any errors that may have occurred.
        catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private static int getRowCount(Statement stmt,
            String tableName) throws SQLException {
        ResultSet rs = stmt.executeQuery("SELECT COUNT(*) FROM " + tableName);
        rs.next();
        int count = rs.getInt(1);
        rs.close();
        return count;
    }
}
```

## Transaction and bulk copy operations

 Bulk copy operations can be performed as isolated operations or as part of a multiple step transaction. This latter option enables you to perform more than one bulk copy operation within the same transaction, as well as perform other database operations (such as inserts, updates, and deletes) while still being able to commit or roll back the entire transaction.  
  
 By default, a bulk copy operation is performed as an isolated operation. The bulk copy operation occurs in a non-transacted way, with no opportunity for rolling it back. If you need to roll back all or part of the bulk copy when an error occurs, you can use a SQLServerBulkCopy-managed transaction or perform the bulk copy operation within an existing transaction.  
  
### Performing a non-transacted bulk copy operation

The following application shows what happens when a non-transacted bulk copy operation encounters an error partway through the operation.  
  
In the example, the source table and destination table each include an Identity column named **ProductID**. The code first prepares the destination table by deleting all rows and then inserting a single row whose **ProductID** is known to exist in the source table. By default, a new value for the Identity column is generated in the destination table for each row added. In this example, an option is set when the connection is opened that forces the bulk load process to use the Identity values from the source table instead.  
  
The bulk copy operation is executed with the **BatchSize** property set to 10. When the operation encounters the invalid row, an exception is thrown. In this first example, the bulk copy operation is non-transacted. All batches copied up to the point of the error are committed; the batch containing the duplicate key is rolled back, and the bulk copy operation is halted before processing any other batches.  
  
> [!NOTE]  
> This sample will not run unless you have created the work tables as described in [Table setup](../../connect/jdbc/using-bulk-copy-with-the-jdbc-driver.md#BKMK_TableSetup). This code is provided to demonstrate the syntax for using SQLServerBulkCopy only. If the source and destination tables are located in the same SQL Server instance, it is easier and faster to use a Transact-SQL INSERT ... SELECT statement to copy the data.  

```java
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerBulkCopy;
import com.microsoft.sqlserver.jdbc.SQLServerBulkCopyOptions;

public class BulkCopyNonTransacted {
    public static void main(String[] args) {
        String connectionUrl = "jdbc:sqlserver://<server>:<port>;databaseName=AdventureWorks;user=<user>;password=<password>";
        String destinationTable = "dbo.BulkCopyDemoMatchingColumns";
        int countBefore, countAfter;
        ResultSet rsSourceData;

        try (Connection sourceConnection = DriverManager.getConnection(connectionUrl);
                Statement stmt = sourceConnection.createStatement();
                SQLServerBulkCopy bulkCopy = new SQLServerBulkCopy(connectionUrl)) {

            // Empty the destination table.
            stmt.executeUpdate("DELETE FROM " + destinationTable);

            // Add a single row that will result in duplicate key
            // when all rows from source are bulk copied.
            // Note that this technique will only be successful in
            // illustrating the point if a row with ProductID = 446
            // exists in the AdventureWorks Production.Products table.
            // If you have made changes to the data in this table, change
            // the SQL statement in the code to add a ProductID that
            // does exist in your version of the Production.Products
            // table. Choose any ProductID in the middle of the table
            // (not first or last row) to best illustrate the result.
            stmt.executeUpdate("SET IDENTITY_INSERT " + destinationTable + " ON;" + "INSERT INTO " + destinationTable
                    + "([ProductID], [Name] ,[ProductNumber]) VALUES(446, 'Lock Nut 23','LN-3416'); SET IDENTITY_INSERT " + destinationTable
                    + " OFF");

            // Perform an initial count on the destination table.
            countBefore = getRowCount(stmt, destinationTable);

            // Get data from the source table as a ResultSet.
            rsSourceData = stmt.executeQuery("SELECT ProductID, Name, ProductNumber FROM Production.Product");

            // Set up the bulk copy object using the KeepIdentity option and BatchSize = 10.
            SQLServerBulkCopyOptions copyOptions = new SQLServerBulkCopyOptions();
            copyOptions.setKeepIdentity(true);
            copyOptions.setBatchSize(10);

            bulkCopy.setBulkCopyOptions(copyOptions);
            bulkCopy.setDestinationTableName(destinationTable);

            // Write from the source to the destination.
            // This should fail with a duplicate key error
            // after some of the batches have been copied.
            try {
                bulkCopy.writeToServer(rsSourceData);
            }
            catch (SQLException e) {
                e.printStackTrace();
            }

            // Perform a final count on the destination
            // table to see how many rows were added.
            countAfter = getRowCount(stmt, destinationTable);
            System.out.println((countAfter - countBefore) + " rows were added.");
        }
        // Handle any errors that may have occurred.
        catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private static int getRowCount(Statement stmt,
            String tableName) throws SQLException {
        ResultSet rs = stmt.executeQuery("SELECT COUNT(*) FROM " + tableName);
        rs.next();
        int count = rs.getInt(1);
        rs.close();
        return count;
    }
}
```

### Performing a dedicated build copy operation in a transaction 

By default, a bulk copy operation is its own transaction. When you want to perform a dedicated bulk copy operation, create a new instance of SQLServerBulkCopy with a connection string. In this scenario, the bulk copy operation creates, and then commits or rolls back the transaction. You can explicitly specify the **UseInternalTransaction** option in **SQLServerBulkCopyOptions** to explicitly cause a bulk copy operation to execute in its own transaction, causing each batch of the bulk copy operation to execute within a separate transaction.  
  
> [!NOTE]  
> Since different batches are executed in different transactions, if an error occurs during the bulk copy operation, all the rows in the current batch will be rolled back, but rows from previous batches will remain in the database.  
  
When you specify the **UseInternalTransaction** option in **BulkCopyNonTransacted**, the bulk copy operation is included in a larger, external transaction. When the primary key violation error occurs, the entire transaction is rolled back and no rows are added to the destination table.

```java
SQLServerBulkCopyOptions copyOptions = new SQLServerBulkCopyOptions();
copyOptions.setKeepIdentity(true);
copyOptions.setBatchSize(10);
copyOptions.setUseInternalTransaction(true);
```

### Using existing transactions

You can pass a Connection object that has transactions enabled as a parameter in a SQLServerBulkCopy constructor. In this situation, the bulk copy operation is performed in an existing transaction, and no change is made to the transaction state (that is, it's neither committed nor aborted). This allows an application to include the bulk copy operation in a transaction with other database operations. If you need to roll back the entire bulk copy operation because an error occurs, or if the bulk copy should execute as part of a larger process that can be rolled back, you can perform the rollback on the Connection object at any point after the bulk copy operation.  
  
The following application is similar to **BulkCopyNonTransacted**, with one exception: in this example, the bulk copy operation is included in a larger, external transaction. When the primary key violation error occurs, the entire transaction is rolled back and no rows are added to the destination table.

> [!NOTE]  
> This sample will not run unless you have created the work tables as described in [Table setup](../../connect/jdbc/using-bulk-copy-with-the-jdbc-driver.md#BKMK_TableSetup). This code is provided to demonstrate the syntax for using SQLServerBulkCopy only. If the source and destination tables are located in the same SQL Server instance, it is easier and faster to use a Transact-SQL INSERT ... SELECT statement to copy the data.  

```java
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerBulkCopy;
import com.microsoft.sqlserver.jdbc.SQLServerBulkCopyOptions;

public class BulkCopyExistingTransactions {
    public static void main(String[] args) {
        String connectionUrl = "jdbc:sqlserver://<server>:<port>;databaseName=AdventureWorks;user=<user>;password=<password>";
        String destinationTable = "dbo.BulkCopyDemoMatchingColumns";
        int countBefore, countAfter;
        ResultSet rsSourceData;
        SQLServerBulkCopyOptions copyOptions;

        try (Connection sourceConnection = DriverManager.getConnection(connectionUrl);
                Connection destinationConnection = DriverManager.getConnection(connectionUrl);
                Statement stmt = sourceConnection.createStatement();
                SQLServerBulkCopy bulkCopy = new SQLServerBulkCopy(destinationConnection);) {

            // Empty the destination table.
            stmt.executeUpdate("DELETE FROM " + destinationTable);

            // Add a single row that will result in duplicate key
            // when all rows from source are bulk copied.
            // Note that this technique will only be successful in
            // illustrating the point if a row with ProductID = 446
            // exists in the AdventureWorks Production.Products table.
            // If you have made changes to the data in this table, change
            // the SQL statement in the code to add a ProductID that
            // does exist in your version of the Production.Products
            // table. Choose any ProductID in the middle of the table
            // (not first or last row) to best illustrate the result.
            stmt.executeUpdate("SET IDENTITY_INSERT " + destinationTable + " ON;" + "INSERT INTO " + destinationTable
                    + "([ProductID], [Name] ,[ProductNumber]) VALUES(446, 'Lock Nut 23','LN-3416'); SET IDENTITY_INSERT " + destinationTable
                    + " OFF");

            // Perform an initial count on the destination table.
            countBefore = getRowCount(stmt, destinationTable);

            // Get data from the source table as a ResultSet.
            rsSourceData = stmt.executeQuery("SELECT ProductID, Name, ProductNumber FROM Production.Product");

            // Set up the bulk copy object inside the transaction.
            destinationConnection.setAutoCommit(false);

            copyOptions = new SQLServerBulkCopyOptions();
            copyOptions.setKeepIdentity(true);
            copyOptions.setBatchSize(10);

            bulkCopy.setBulkCopyOptions(copyOptions);
            bulkCopy.setDestinationTableName(destinationTable);

            // Write from the source to the destination.
            // This should fail with a duplicate key error.
            try {
                bulkCopy.writeToServer(rsSourceData);
                destinationConnection.commit();
            }
            catch (SQLException e) {
                e.printStackTrace();
            }

            // Perform a final count on the destination
            // table to see how many rows were added.
            countAfter = getRowCount(stmt, destinationTable);
            System.out.println((countAfter - countBefore) + " rows were added.");
        }
        catch (Exception e) {
            // Handle any errors that may have occurred.
            e.printStackTrace();
        }
    }

    private static int getRowCount(Statement stmt,
            String tableName) throws SQLException {
        ResultSet rs = stmt.executeQuery("SELECT COUNT(*) FROM " + tableName);
        rs.next();
        int count = rs.getInt(1);
        rs.close();
        return count;
    }
}
```

### Bulk Copy from a CSV File  

 The following application demonstrates how to load data using the SQLServerBulkCopy class. In this example, a CSV file is used to copy data exported from the Production.Product table in the SQL Server AdventureWorks database to a similar table in the database.  
  
> [!IMPORTANT]  
> This sample will not run unless you have created the work tables as described in [Table setup](../../ssms/download-sql-server-management-studio-ssms.md) to get it.  
  
1. Open **SQL Server Management Studio** and connect to the SQL Server with the AdventureWorks database.  
  
2. Expand the databases, right-click the AdventureWorks database, select **Tasks** and **Export Data**...  
  
3. For the Data Source, select the **Data source** that allows you to connect to your SQL Server (e.g. SQL Server Native Client 11.0), check the configuration and then **Next**  
  
4. For the Destination, Select the **Flat File Destination** and enter a **File Name** with a destination such as C:\Test\TestBulkCSVExample.csv. Check that the **Format** is Delimited, the **Text qualifier** is none, and enable **Column names in the first data row**, and then select **Next**  
  
5. Select **Write a query to specify the data to transfer** and **Next**.  Enter your **SQL Statement** SELECT ProductID, Name, ProductNumber FROM Production.Product, and **Next**  
  
6. Check the configuration: You can leave the Row delimiter as {CR}{LF} and Column Delimiter as Comma {,}.  Select **Edit Mappings**... and check that the data **Type** is correct for each column (e.g. integer for ProductID and Unicode string for the others).  
  
7. Skip ahead to **Finish** and run the export.  

```java
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import com.microsoft.sqlserver.jdbc.SQLServerBulkCSVFileRecord;
import com.microsoft.sqlserver.jdbc.SQLServerBulkCopy;

public class BulkCopyCSV {
    public static void main(String[] args) {
        String connectionUrl = "jdbc:sqlserver://<server>:<port>;databaseName=AdventureWorks;user=<user>;password=<password>";
        String destinationTable = "dbo.BulkCopyDemoMatchingColumns";
        int countBefore, countAfter;

        // Get data from the source file by loading it into a class that implements ISQLServerBulkRecord.
        // Here we are using the SQLServerBulkCSVFileRecord implementation to import the example CSV file.
        try (Connection destinationConnection = DriverManager.getConnection(connectionUrl);
                Statement stmt = destinationConnection.createStatement();
                SQLServerBulkCopy bulkCopy = new SQLServerBulkCopy(destinationConnection);
                SQLServerBulkCSVFileRecord fileRecord = new SQLServerBulkCSVFileRecord("C:\\Test\\TestBulkCSVExample.csv", true);) {

            // Set the metadata for each column to be copied.
            fileRecord.addColumnMetadata(1, null, java.sql.Types.INTEGER, 0, 0);
            fileRecord.addColumnMetadata(2, null, java.sql.Types.NVARCHAR, 50, 0);
            fileRecord.addColumnMetadata(3, null, java.sql.Types.NVARCHAR, 25, 0);

            // Empty the destination table.
            stmt.executeUpdate("DELETE FROM " + destinationTable);

            // Perform an initial count on the destination table.
            countBefore = getRowCount(stmt, destinationTable);

            // Set up the bulk copy object.
            // Note that the column positions in the source
            // data reader match the column positions in
            // the destination table so there is no need to
            // map columns.
            bulkCopy.setDestinationTableName(destinationTable);

            // Write from the source to the destination.
            bulkCopy.writeToServer(fileRecord);

            // Perform a final count on the destination
            // table to see how many rows were added.
            countAfter = getRowCount(stmt, destinationTable);
            System.out.println((countAfter - countBefore) + " rows were added.");
        }
        // Handle any errors that may have occurred.
        catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private static int getRowCount(Statement stmt,
            String tableName) throws SQLException {
        ResultSet rs = stmt.executeQuery("SELECT COUNT(*) FROM " + tableName);
        rs.next();
        int count = rs.getInt(1);
        rs.close();
        return count;
    }
}
```  

### Bulk copy with Always Encrypted columns  

Beginning with Microsoft JDBC Driver 6.0 for SQL Server, bulk copy is supported with Always Encrypted columns.  
  
Depending on the bulk copy options, and the encryption type of the source and destination tables the JDBC driver may transparently decrypt and then encrypt the data or it may send the encrypted data as is. For example, when bulk copying data from an encrypted column to an unencrypted column, the driver transparently decrypts data before sending to SQL Server. Similarly when bulk copying data from an unencrypted column (or from a CSV file) to an encrypted column, the driver transparently encrypts data before sending to SQL Server. If both source and destination is encrypted, then depending on the **allowEncryptedValueModifications** bulk copy option, the driver would send data as is or would decrypt the data and encrypt it again before sending to SQL Server.  
  
For more information, see the **allowEncryptedValueModifications** bulk copy option below, and [Using Always Encrypted with the JDBC Driver](../../connect/jdbc/using-always-encrypted-with-the-jdbc-driver.md).  
  
> [!IMPORTANT]  
> Limitation of the Microsoft JDBC Driver 6.0 for SQL Server, when bulk copying data from a CSV file to encrypted columns:  
>
> Only the Transact-SQL default string literal format is supported for the date and time types  
>
> DATETIME and SMALLDATETIME datatypes are not supported  
  
## Bulk copy API for JDBC driver  
  
### SQLServerBulkCopy 

Lets you efficiently bulk load a SQL Server table with data from another source.  
  
Microsoft SQL Server includes a popular command-prompt utility named bcp for moving data from one table to another, whether on a single server or between servers. The SQLServerBulkCopy class lets you write code solutions in Java that provide similar functionality. There are other ways to load data into a SQL Server table (INSERT statements, for example), but SQLServerBulkCopy offers a significant performance advantage over them.  
  
The SQLServerBulkCopy class can be used to write data only to SQL Server tables. However, the data source isn't limited to SQL Server; any data source can be used, as long as the data can be read with a ResultSet instance or ISQLServerBulkRecord implementation.  
  
| Constructor                             | Description                                                                                                                                                                                                                    |
| --------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| SQLServerBulkCopy(Connection)           | Initializes a new instance of the SQLServerBulkCopy class using the specified open instance of SQLServerConnection. If the Connection has transactions enabled, the copy operations will be performed within that transaction. |
| SQLServerBulkCopy(String connectionURL) | Initializes and opens a new instance of SQLServerConnection based on the supplied connectionURL. The constructor uses the SQLServerConnection to initialize a new instance of the SQLServerBulkCopy class.                     |
  
| Property                    | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| --------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| String DestinationTableName | Name of the destination table on the server.<br /><br /> If DestinationTableName hasn't been set when writeToServer is called, a SQLServerException is thrown.<br /><br /> DestinationTableName is a three-part name (\<database>.\<owningschema>.\<name>). You can qualify the table name with its database and owning schema if you choose. However, if the table name uses an underscore ("_") or any other special characters, you must escape the name using surrounding brackets. For more information, see "Identifiers" in SQL Server Books Online. |
| ColumnMappings              | Column mappings define the relationships between columns in the data source and columns in the destination.<br /><br /> If mappings aren't defined the columns are mapped implicitly based on ordinal position. For this to work, source and target schemas must match. If they don't, an Exception will be thrown.<br /><br /> If the mappings isn't empty, not every column present in the data source has to be specified. Those not mapped are ignored.<br /><br /> You can refer to source and target columns by either name or ordinal.               |
  
| Method                                                                | Description                                                                                                                                                                |
| --------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Void addColumnMapping((int sourceColumn, int destinationColumn)       | Adds a new column-mapping, using ordinals to specify both source and destination columns.                                                                                  |
| Void addColumnMapping ((int sourceColumn, String destinationColumn)   | Adds a new column-mapping, using an ordinal for the source column and a column name for the destination column.                                                            |
| Void addColumnMapping ((String sourceColumn, int destinationColumn)   | Adds a new column-mapping, using a column name to describe the source column and an ordinal to specify the destination column.                                             |
| Void addColumnMapping (String sourceColumn, String destinationColumn) | Adds a new column-mapping, using column names to specify both source and destination columns.                                                                              |
| Void clearColumnMappings()                                            | Clears the contents of the column mappings.                                                                                                                                |
| Void close()                                                          | Closes the SQLServerBulkCopy instance.                                                                                                                                     |
| SQLServerBulkCopyOptions getBulkCopyOptions()                         | Retrieves the current set of SQLServerBulkCopyOptions.                                                                                                                     |
| String getDestinationTableName()                                      | Retrieve the current destination table name.                                                                                                                               |
| Void setBulkCopyOptions(SQLServerBulkCopyOptions copyOptions)         | Updates the behavior of the SQLServerBulkCopy instance according to the options supplied.                                                                                  |
| Void setDestinationTableName(String tableName)                        | Sets the name of the destination table.                                                                                                                                    |
| Void writeToServer(ResultSet sourceData)                              | Copies all rows in the supplied ResultSet to a destination table specified by the DestinationTableName property of the SQLServerBulkCopy object.                           |
| Void writeToServer(RowSet sourceData)                                 | Copies all rows in the supplied RowSet to a destination table specified by the DestinationTableName property of the SQLServerBulkCopy object.                              |
| Void writeToServer(ISQLServerBulkRecord sourceData)                   | Copies all rows in the supplied ISQLServerBulkRecord implementation to a destination table specified by the DestinationTableName property of the SQLServerBulkCopy object. |
  
### SQLServerBulkCopyOptions  

 A collection of settings that control how the writeToServer methods behave in an instance of SQLServerBulkCopy.  
  
| Constructor                | Description                                                                                              |
| -------------------------- | -------------------------------------------------------------------------------------------------------- |
| SQLServerBulkCopyOptions() | Initializes a new instance of the SQLServerBulkCopyOptions class using defaults for all of the settings. |
  
 Getters and setters exist for the following options:  
  
| Option                                   | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             | Default                                                              |
| ---------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------- |
| Boolean CheckConstraints                 | Check constraints while data is being inserted.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         | False - constraints aren't checked                                   |
| Boolean FireTriggers                     | When specified, cause the server to fire the insert triggers for the rows being inserted into the database.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             | False - no triggers are fired                                        |
| Boolean KeepIdentity                     | Preserve source identity values.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        | False - identity values are assigned by the destination              |
| Boolean KeepNulls                        | Preserve null values in the destination table regardless of the settings for default values.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            | False - null values are replaced by default values where applicable. |
| Boolean TableLock                        | Obtain a bulk update lock for the duration of the bulk copy operation.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  | False - row locks are used.                                          |
| Boolean UseInternalTransaction           | When specified, each batch of the bulk-copy operation will occur within a transaction. If SQLServerBulkCopy is using an existing connection (as specified by the constructor), a SQLServerException will occur.  If SQLServerBulkCopy created a dedicated connection, a transaction will be enabled.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    | False - no transaction                                               |
| Int BatchSize                            | Number of rows in each batch. At the end of each batch, the rows in the batch are sent to the server.<br /><br /> A batch is complete when BatchSize rows have been processed or there are no more rows to send to the destination data source.  If the SQLServerBulkCopy instance has been declared without the UseInternalTransaction option in effect, rows are sent to the server BatchSize rows at a time, but no transaction-related action is taken. If UseInternalTransaction is in effect, each batch of rows is inserted as a separate transaction.                                                                                                                                                                                                                                                                                                                                                                                                                                           | 0 - indicates that each writeToServer operation is a single batch    |
| Int BulkCopyTimeout                      | Number of seconds for the operation to complete before it times out. A value of 0 indicates no limit; the bulk copy will wait indefinitely.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             | 60 seconds.                                                          |
| Boolean allowEncryptedValueModifications | This option is available with Microsoft JDBC Driver 6.0 (or higher) for SQL Server.<br /><br /> When specified, **allowEncryptedValueModifications** enables bulk copying of encrypted data between tables or databases, without decrypting the data. Typically, an application would select data from encrypted columns from one table without decrypting the data (the app would connect to the database with the column encryption setting keyword set to disabled) and then would use this option to bulk insert the data, which is still encrypted. For more information, see [Using Always Encrypted with the JDBC Driver](../../connect/jdbc/using-always-encrypted-with-the-jdbc-driver.md).<br /><br /> Use caution when specifying **allowEncryptedValueModifications** as this may lead to corrupting the database because the driver doesn't check if the data is indeed encrypted, or if it is correctly encrypted using the same encryption type, algorithm and key as the target column. |
  
 Getters and setters:  
  
| Methods                                                                            | Description                                                                                                                                                                               |
| ---------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Boolean isCheckConstraints()                                                       | Indicates whether constraints are to be checked while data is being inserted or not.                                                                                                      |
| Void setCheckConstraints(Boolean checkConstraints)                                 | Sets whether constraints are to be checked while data is being inserted or not.                                                                                                           |
| Boolean isFireTriggers()                                                           | Indicates if the server should fire the insert triggers for the rows being inserted into the database.                                                                                    |
| Void setFireTriggers(Boolean fireTriggers)                                         | Sets whether the server should be set to fire triggers for the rows being inserted into the database.                                                                                     |
| Boolean isKeepIdentity()                                                           | Indicates whether or not to preserve any source identity values.                                                                                                                          |
| Void setKeepIdentity(Boolean keepIdentity)                                         | Sets whether or not to preserve identity values.                                                                                                                                          |
| Boolean isKeepNulls()                                                              | Indicates whether to preserve null values in the destination table regardless of the settings for default values, or if they should be replaced by the default values (where applicable). |
| Void setKeepNulls(Boolean keepNulls)                                               | Sets whether to preserve null values in the destination table regardless of the settings for default values, or if they should be replaced by the default values (where applicable).      |
| Boolean isTableLock()                                                              | Indicates whether SQLServerBulkCopy should obtain a bulk update lock for the duration of the bulk copy operation.                                                                         |
| Void setTableLock(Boolean tableLock)                                               | Sets whether SQLServerBulkCopy should obtain a bulk update lock for the duration of the bulk copy operation.                                                                              |
| Boolean isUseInternalTransaction()                                                 | Indicates whether each batch of the bulk-copy operation will occur within a transaction.                                                                                                  |
| Void setUseInternalTranscation(Boolean useInternalTransaction)                     | Sets whether each batch of the bulk-copy operations will occur within a transaction or not.                                                                                               |
| Int getBatchSize()                                                                 | Gets the number of rows in each batch. At the end of each batch, the rows in the batch are sent to the server                                                                             |
| Void setBatchSize(int batchSize)                                                   | Sets the number of rows in each batch. At the end of each batch, the rows in the batch are sent to the server.                                                                            |
| Int getBulkCopyTimeout()                                                           | Gets the number of seconds for the operation to complete before it times out.                                                                                                             |
| Void  setBulkCopyTimeout(int timeout)                                              | Sets the number of seconds for the operation to complete before it times out.                                                                                                             |
| boolean isAllowEncryptedValueModifications()                                       | Indicates whether allowEncryptedValueModifications setting is enabled or disabled.                                                                                                        |
| void setAllowEncryptedValueModifications(boolean allowEncryptedValueModifications) | Configures the allowEncryptedValueModifications setting that is used for bulk copy with Always Encrypted columns.                                                                         |
  
### ISQLServerBulkRecord  

 The ISQLServerBulkRecord interface can be used to create classes that read in data from any source (such as a file) and allow a SQLServerBulkCopy instance to bulk load a SQL Server table with that data.  
  
| Interface Methods                   | Description                                                                                                                                                                                                                                                                                            |
| ----------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Set\<Integer> getColumnOrdinals()   | Get the ordinals for each of the columns represented in this data record.                                                                                                                                                                                                                              |
| String getColumnName(int column)    | Get the name of the given column.                                                                                                                                                                                                                                                                      |
| Int getColumnType(int column)       | Get the JDBC data type of the given column.                                                                                                                                                                                                                                                            |
| Int getPrecision(int column)        | Get the precision for the given column.                                                                                                                                                                                                                                                                |
| Object[] getRowData()               | Gets the data for the current row as an array of Objects.<br /><br /> Each Object must match the Java language Type that is used to represent the indicated JDBC data type for the given column.  For more information, see 'Understanding the JDBC Driver Data Types' for the appropriate mappings. |
| Int getScale(int column)            | Get the scale for the given column.                                                                                                                                                                                                                                                                    |
| Boolean isAutoIncrement(int column) | Indicates whether the column represents an identity column.                                                                                                                                                                                                                                            |
| Boolean next()                      | Advances to the next data row.                                                                                                                                                                                                                                                                         |
  
### SQLServerBulkCSVFileRecord  

A simple implementation of the ISQLServerBulkRecord interface that can be used to read in the basic Java data types from a delimited file where each line represents a row of data.  
  
Implementation Notes and Limitations:  
  
1. The maximum amount of data allowed in any given row is limited by the available memory because the data is read one line at a time.  
  
2. Streaming of large data types such as varchar(max), varbinary(max), nvarchar(max), sqlxml, ntext isn't supported.  
  
3. The delimiter specified for the CSV file shouldn't appear anywhere in the data and should be escaped properly if it is a restricted character in Java regular expressions.  
  
4. In the CSV file implementation, double quotes are treated as part of the data. For example, the line hello,"world","hello,world" would be treated as having four columns with the values hello, "world", "hello and world" if the delimiter is a comma.  
  
5. New line characters are used as row terminators and aren't allowed anywhere in the data.  
  
| Constructor                                                                                                                                                                 | Description                                                                                                                                                                                                                                                                                                                        |
| --------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| SQLServerBulkCSVFileRecord(String fileToParse, String encoding, String delimiter, Boolean firstLineIsColumnNamesSQLServerBulkCSVFileRecord(String, String, String, boolean) | Initializes a new instance of the SQLServerBulkCSVFileRecord class that will parse each line in the fileToParse with the provided delimiter and encoding. If firstLineIsColumnNames is set to True, the first line in the file will be parsed as column names.  If encoding is NULL, the default encoding will be used.            |
| SQLServerBulkCSVFileRecord(String fileToParse, String encoding, Boolean firstLineIsColumnNamesSQLServerBulkCSVFileRecord(String, String, boolean)                           | Initializes a new instance of the SQLServerBulkCSVFileRecord class that will parse each line in the fileToParse with a comma as the delimiter and provided encoding. If firstLineIsColumnNames is set to True, the first line in the file will be parsed as column names.  If encoding is NULL, the default encoding will be used. |
| SQLServerBulkCSVFileRecord(String fileToParse, Boolean firstLineIsColumnNamesSQLServerBulkCSVFileRecord(String, boolean)                                                    | Initializes a new instance of the SQLServerBulkCSVFileRecord class that will parse each line in the fileToParse with a comma as the delimiter and default encoding. If firstLineIsColumnNames is set to True, the first line in the file will be parsed as column names.                                                           |
  
| Method                                                                                                 | Description                                                                                         |
| ------------------------------------------------------------------------------------------------------ | --------------------------------------------------------------------------------------------------- |
| Void addColumnMetadata(int positionInFile, String columnName, int jdbcType, int precision, int scale)  | Adds metadata for the given column in the file.                                                     |
| Void close()                                                                                           | Releases any resources associated with the file reader.                                             |
| Void setTimestampWithTimezoneFormat(DateTim eFormatter dateTimeFormatter                               | Sets the format for parsing Timestamp data from the file as java.sql.Types.TIMESTAMP_WITH_TIMEZONE. |
| Void setTimestampWithTimezoneFormat(String dateTimeFormat)setTimeWithTimezoneFormat(DateTimeFormatter) | Sets the format for parsing Time data from the file as java.sql.Types.TIME_WITH_TIMEZONE.           |
| Void setTimeWithTimezoneFormat(DateTimeForm atter dateTimeFormatter)                                   | Sets the format for parsing Time data from the file as java.sql.Types.TIME_WITH_TIMEZONE.           |
| Void setTimeWithTimezoneFormat(String timeFormat)                                                      | Sets the format for parsing Time data from the file as java.sql.Types.TIME_WITH_TIMEZONE.           |
  
## See Also  

[Overview of the JDBC Driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)  
  
