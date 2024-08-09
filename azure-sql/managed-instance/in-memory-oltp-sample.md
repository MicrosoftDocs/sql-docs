---
title: In-memory sample
description: Try in-memory technologies in Azure SQL Managed Instance with OLTP and columnstore sample.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, srinia, strrodic 
ms.date: 12/29/2023
ms.service: azure-sql-managed-instance
ms.subservice: performance
ms.topic: tutorial
monikerRange: "=azuresql||=azuresql-mi"
---
# In-memory sample in Azure SQL Managed Instance
[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/in-memory-oltp-sample.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](in-memory-oltp-sample.md?view=azuresql-mi&preserve-view=true)

In-memory technologies in Azure SQL Managed Instance enable you to improve performance of your application, and potentially reduce cost of your database. By using in-memory technologies in Azure SQL Managed Instance, you can achieve performance improvements with various workloads.

In this article you'll see two samples that illustrate the use of in-memory OLTP, as well as columnstore indexes in Azure SQL Managed Instance.

For more information, see:

- [In-memory OLTP Overview and Usage Scenarios](/sql/relational-databases/in-memory-oltp/overview-and-usage-scenarios?view=azuresql-mi&preserve-view=true) (includes references to customer case studies and information to get started)
- [Documentation for in-memory OLTP](/sql/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization?view=azuresqlmi-current&preserve-view=true)
- [Columnstore Indexes Guide](/sql/relational-databases/indexes/columnstore-indexes-overview?view=azuresqlmi-current&preserve-view=true)
- Hybrid transactional/analytical processing (HTAP), also known as [real-time operational analytics](/sql/relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics?view=azuresqlmi-current&preserve-view=true)

For a more simplistic, but more visually appealing performance demo for in-memory OLTP, see:

- Release: [in-memory-oltp-demo-v1.0](https://github.com/Microsoft/sql-server-samples/releases/tag/in-memory-oltp-demo-v1.0)
- Source code: [in-memory-oltp-demo-source-code](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/in-memory-database)

## 1. Restore the in-memory OLTP sample database

You can restore the `AdventureWorksLT` sample database with a few T-SQL steps in [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms). For more information on restoring a database to your SQL managed instance, see [Quickstart: Restore a database to Azure SQL Managed Instance with SSMS](restore-sample-database-quickstart.md).

Then, the steps in this section explain how you can enrich your `AdventureWorksLT` database with in-memory OLTP objects and demonstrate performance benefits.

1. Open SSMS and connect to your SQL managed instance.

   > [!NOTE]
   > Connections to your Azure SQL Managed Instance from your on-premises workstation or an Azure VM can be made securely, without opening public access. Consider [Quickstart: Configure a point-to-site connection to Azure SQL Managed Instance from on-premises](point-to-site-p2s-configure.md) or [Quickstart: Configure an Azure VM to connect to Azure SQL Managed Instance](connect-vm-instance-configure.md).

1. In **Object Explorer**, right-click your managed instance and select **New Query** to open a new query window.

1. Run the following T-SQL statement, which uses publicly available preconfigured storage container and a shared access signature key to [create a credential](/sql/t-sql/statements/create-credential-transact-sql) in your SQL managed instance. With publicly-available storage, no SAS signature is required.

   ```sql
   CREATE CREDENTIAL [https://mitutorials.blob.core.windows.net/examples/]
   WITH IDENTITY = 'SHARED ACCESS SIGNATURE';
   ```

1. Run the following statement to restore the example `AdventureWorksLT` database.

   ```sql
   RESTORE DATABASE [AdventureWorksLT] 
   FROM URL = 'https://mitutorials.blob.core.windows.net/examples/AdventureWorksLT2022.bak';
   ```

1. Run the following statement to track the status of your restore process.

   ```sql
   SELECT session_id as SPID, command, a.text AS Query, start_time, percent_complete
      , dateadd(second,estimated_completion_time/1000, getdate()) as estimated_completion_time
   FROM sys.dm_exec_requests r
   CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) a
   WHERE r.command in ('BACKUP DATABASE','RESTORE DATABASE');
   ```

1. When the restore process finishes, view the `AdventureWorksLT` database in **Object Explorer**. You can verify that the `AdventureWorksLT` database is restored by using the [sys.dm_operation_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database?view=azuresqlmi-current&preserve-view=true) view.

### About the created memory-optimized items

**Tables**: The sample contains the following memory-optimized tables:

- `SalesLT.Product_inmem`
- `SalesLT.SalesOrderHeader_inmem`
- `SalesLT.SalesOrderDetail_inmem`
- `Demo.DemoSalesOrderHeaderSeed`
- `Demo.DemoSalesOrderDetailSeed`

You can filter to show only memory-optimized tables in **Object Explorer** in SSMS. When you right-click on **Tables**, then navigate to > **Filter** > **Filter Settings** > **Is Memory Optimized**. The value equals `1`.

Or you can query the catalog views, such as:

```sql
SELECT is_memory_optimized, name, type_desc, durability_desc
    FROM sys.tables
    WHERE is_memory_optimized = 1;
```

**Natively compiled stored procedure**: You can inspect `SalesLT.usp_InsertSalesOrder_inmem` through a catalog view query:

```sql
SELECT uses_native_compilation, OBJECT_NAME(object_id), definition
    FROM sys.sql_modules
    WHERE uses_native_compilation = 1;
```

## 2. Run the sample OLTP workload

The only difference between the following two *stored procedures* is that the first procedure uses memory-optimized versions of the tables, while the second procedure uses the regular on-disk tables:

- `SalesLT.usp_InsertSalesOrder_inmem`
- `SalesLT.usp_InsertSalesOrder_ondisk`

In this section, you see how to use the handy ostress.exe utility to execute the two stored procedures at stressful levels. You can compare how long it takes for the two stress runs to finish.


### Install RML utilities and ostress

Ideally, you would plan to run ostress.exe on an Azure virtual machine (VM). You would create an [Azure VM](/azure/virtual-machines/) in the same Azure region as your SQL managed instance. But you can run ostress.exe on your local workstation instead, as long as you can connect to your Azure SQL managed instance.

On the VM, or on whatever host you choose, install the Replay Markup Language (RML) utilities. The utilities include ostress.exe.

For more information, see:

- The ostress.exe discussion in [Sample Database for in-memory OLTP](/sql/relational-databases/in-memory-oltp/sample-database-for-in-memory-oltp).
- [Sample Database for in-memory OLTP](/sql/relational-databases/in-memory-oltp/sample-database-for-in-memory-oltp).
- The [blog for installing ostress.exe](https://techcommunity.microsoft.com/t5/sql-server-support/cumulative-update-2-to-the-rml-utilities-for-microsoft-sql/ba-p/317910).

<!--
dn511655.aspx is for SQL 2014,
[Extensions to AdventureWorks to Demonstrate in-memory OLTP]
(https://msdn.microsoft.com/library/dn511655&#x28;v=sql.120&#x29;.aspx)

whereas for SQL 2016+
[Sample Database for in-memory OLTP]
(https://msdn.microsoft.com/library/mt465764.aspx)
-->

### Script for ostress.exe

This section displays the T-SQL script that is embedded in our ostress.exe command line. The script uses items that were created by the T-SQL script that you installed earlier.

When you run ostress.exe, we recommend that you pass parameter values designed to stress the workload using both of the following strategies:

- Run a large number of concurrent connections, by using `-n100`.
- Have each connection repeat hundreds of times, by using `-r500`.

However, you might want to start with much smaller values like `-n10` and `-r50` to ensure that everything is working.

The following script inserts a sample sales order with five line items into the following memory-optimized *tables*:

- `SalesLT.SalesOrderHeader_inmem`
- `SalesLT.SalesOrderDetail_inmem`

```sql
DECLARE
    @i int = 0,
    @od SalesLT.SalesOrderDetailType_inmem,
    @SalesOrderID int,
    @DueDate datetime2 = sysdatetime(),
    @CustomerID int = rand() * 8000,
    @BillToAddressID int = rand() * 10000,
    @ShipToAddressID int = rand() * 10000;

INSERT INTO @od
    SELECT OrderQty, ProductID
    FROM Demo.DemoSalesOrderDetailSeed
    WHERE OrderID= cast((rand()*60) as int);

WHILE (@i < 20)
BEGIN;
    EXECUTE SalesLT.usp_InsertSalesOrder_inmem @SalesOrderID OUTPUT,
        @DueDate, @CustomerID, @BillToAddressID, @ShipToAddressID, @od;
    SET @i = @i + 1;
END
```

To make the *_ondisk* version of the preceding T-SQL script for ostress.exe, you would replace both occurrences of the *_inmem* substring with *_ondisk*. These replacements affect the names of tables and stored procedures.

#### Run the *_inmem* stress workload first

You can use an *RML Cmd Prompt* window to run our ostress.exe command line. The command-line parameters direct ostress to:

- Run 100 connections concurrently (-n100).
- Have each connection run the T-SQL script 50 times (-r50).

```console
ostress.exe -n100 -r50 -S<servername>.database.windows.net -U<login> -P<password> -d<database> -q -Q"DECLARE @i int = 0, @od SalesLT.SalesOrderDetailType_inmem, @SalesOrderID int, @DueDate datetime2 = sysdatetime(), @CustomerID int = rand() * 8000, @BillToAddressID int = rand() * 10000, @ShipToAddressID int = rand()* 10000; INSERT INTO @od SELECT OrderQty, ProductID FROM Demo.DemoSalesOrderDetailSeed WHERE OrderID= cast((rand()*60) as int); WHILE (@i < 20) begin; EXECUTE SalesLT.usp_InsertSalesOrder_inmem @SalesOrderID OUTPUT, @DueDate, @CustomerID, @BillToAddressID, @ShipToAddressID, @od; set @i += 1; end"
```

To run the preceding ostress.exe command line:

1. Reset the database data content by running the following command in SSMS, to delete all the data that was inserted by any previous runs:

    ```sql
    EXECUTE Demo.usp_DemoReset;
    ```

1. Copy the text of the preceding ostress.exe command line to your clipboard.

1. Replace the `<placeholders>` for the parameters `-S -U -P -d` with the correct real values.

1. Run your edited command line in an RML Cmd window.

#### Result is a duration

When ostress.exe finishes, it writes the run duration as its final line of output in the RML Cmd window. For example, a shorter test run lasted about 1.5 minutes:

`11/12/15 00:35:00.873 [0x000030A8] OSTRESS exiting normally, elapsed time: 00:01:31.867`

#### Reset, edit for *_ondisk*, then rerun

After you have the result from the *_inmem* run, perform the following steps for the *_ondisk* run:

1. Reset the database by running the following command in SSMS to delete all the data that was inserted by the previous run:

   ```sql
   EXECUTE Demo.usp_DemoReset;
   ```

1. Edit the ostress.exe command line to replace all *_inmem* with *_ondisk*.

1. Rerun ostress.exe for the second time, and capture the duration result.

1. Again, reset the database (for responsibly deleting what can be a large amount of test data).

#### Expected comparison results

Our in-memory tests have shown that performance improved by **nine times** for this simplistic workload, with `ostress` running on an Azure VM in the same Azure region as the database.

<a id="install_analytics_manuallink" name="install_analytics_manuallink"></a>

## 3. Install the in-memory analytics sample

In this section, you compare the IO and statistics results when you're using a columnstore index versus a traditional b-tree index.

For real-time analytics on an OLTP workload, it's often best to use a nonclustered columnstore index. For details, see [Columnstore Indexes Described](/sql/relational-databases/indexes/columnstore-indexes-overview).

### Prepare the columnstore analytics test

1. Restore a fresh `AdventureWorksLT` database to your SQL managed instance, overwriting the existing database you installed earlier, using `WITH REPLACE`.

   ```sql
   RESTORE DATABASE [AdventureWorksLT] 
   FROM URL = 'https://mitutorials.blob.core.windows.net/examples/AdventureWorksLT2022.bak'
   WITH REPLACE;
   ```

1. Copy the [sql_in-memory_analytics_sample](https://raw.githubusercontent.com/microsoft/sql-server-samples/master/samples/features/in-memory-database/in-memory-oltp/t-sql-scripts/sql_in-memory_analytics_sample.sql) to your clipboard.
   - The T-SQL script creates the necessary in-memory objects in the `AdventureWorksLT` sample database that you created in step 1.
   - The script creates dimension tables and two fact tables. The fact tables are populated with 3.5 million rows each.
   - The script might take 15 minutes to complete.

1. Paste the T-SQL script into SSMS, and then execute the script. The **COLUMNSTORE** keyword in the `CREATE INDEX` statement is crucial: `CREATE NONCLUSTERED COLUMNSTORE INDEX ...;`

1. Set `AdventureWorksLT` to the latest compatibility level, SQL Server 2022 (160): `ALTER DATABASE AdventureworksLT SET compatibility_level = 160;`

#### Key tables and columnstore indexes

- `dbo.FactResellerSalesXL_CCI` is a table that has a clustered columnstore index, which has advanced compression at the *data* level.

- `dbo.FactResellerSalesXL_PageCompressed` is a table that has an equivalent regular clustered index, which is compressed only at the *page* level.

## 4. Key queries to compare the columnstore index

There are [several T-SQL query types that you can run](https://raw.githubusercontent.com/microsoft/sql-server-samples/master/samples/features/in-memory-database/in-memory-oltp/t-sql-scripts/clustered_columnstore_sample_queries.sql) to see performance improvements. In step 2 in the T-SQL script, pay attention to this pair of queries. They differ only on one line:

- `FROM FactResellerSalesXL_PageCompressed AS a`
- `FROM FactResellerSalesXL_CCI AS a`

A clustered columnstore index is in the `FactResellerSalesXL_CCI` table.

The following T-SQL script prints the logical I/O activity and time statistics, using [SET STATISTICS IO](/sql/t-sql/statements/set-statistics-io-transact-sql?view=azuresqldb-current&preserve-view=true) and [SET STATISTICS TIME](/sql/t-sql/statements/set-statistics-time-transact-sql?view=azuresqldb-current&preserve-view=true) for each query.

```sql
/*********************************************************************
Step 2 -- Overview
-- Page Compressed BTree table v/s Columnstore table performance differences
-- Enable actual Query Plan in order to see Plan differences when Executing
*/
-- Ensure Database is in 130 compatibility mode
ALTER DATABASE AdventureworksLT SET compatibility_level = 160
GO

-- Execute a typical query that joins the Fact Table with dimension tables
-- Note this query will run on the Page Compressed table, Note down the time
SET STATISTICS IO ON
SET STATISTICS TIME ON
GO

SELECT c.Year
    ,e.ProductCategoryKey
    ,FirstName + ' ' + LastName AS FullName
    ,count(SalesOrderNumber) AS NumSales
    ,sum(SalesAmount) AS TotalSalesAmt
    ,Avg(SalesAmount) AS AvgSalesAmt
    ,count(DISTINCT SalesOrderNumber) AS NumOrders
    ,count(DISTINCT a.CustomerKey) AS CountCustomers
FROM FactResellerSalesXL_PageCompressed AS a
INNER JOIN DimProduct AS b ON b.ProductKey = a.ProductKey
INNER JOIN DimCustomer AS d ON d.CustomerKey = a.CustomerKey
Inner JOIN DimProductSubCategory AS e on e.ProductSubcategoryKey = b.ProductSubcategoryKey
INNER JOIN DimDate AS c ON c.DateKey = a.OrderDateKey
GROUP BY e.ProductCategoryKey,c.Year,d.CustomerKey,d.FirstName,d.LastName
GO
SET STATISTICS IO OFF
SET STATISTICS TIME OFF
GO


-- This is the same Prior query on a table with a clustered columnstore index CCI
-- The comparison numbers are even more dramatic the larger the table is (this is an 11 million row table only)
SET STATISTICS IO ON
SET STATISTICS TIME ON
GO
SELECT c.Year
    ,e.ProductCategoryKey
    ,FirstName + ' ' + LastName AS FullName
    ,count(SalesOrderNumber) AS NumSales
    ,sum(SalesAmount) AS TotalSalesAmt
    ,Avg(SalesAmount) AS AvgSalesAmt
    ,count(DISTINCT SalesOrderNumber) AS NumOrders
    ,count(DISTINCT a.CustomerKey) AS CountCustomers
FROM FactResellerSalesXL_CCI AS a
INNER JOIN DimProduct AS b ON b.ProductKey = a.ProductKey
INNER JOIN DimCustomer AS d ON d.CustomerKey = a.CustomerKey
Inner JOIN DimProductSubCategory AS e on e.ProductSubcategoryKey = b.ProductSubcategoryKey
INNER JOIN DimDate AS c ON c.DateKey = a.OrderDateKey
GROUP BY e.ProductCategoryKey,c.Year,d.CustomerKey,d.FirstName,d.LastName
GO

SET STATISTICS IO OFF
SET STATISTICS TIME OFF
GO
```

Depending on your SQL managed instance configuration, you can expect significant performance gains for this query by using the clustered columnstore index compared with the traditional index.

## Related content

- [Quickstart 1: In-memory OLTP Technologies for faster T-SQL Performance](/sql/relational-databases/in-memory-oltp/survey-of-initial-areas-in-in-memory-oltp?view=azuresqlmi-current&preserve-view=true)
- [Use in-memory OLTP to improve your application performance](in-memory-oltp-configure.md?view=azuresql-mi&preserve-view=true)
- [Monitor in-memory OLTP storage](in-memory-oltp-monitor-space.md?view=azuresql-mi&preserve-view=true)
- [In-memory OLTP](/sql/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization?view=azuresqlmi-current&preserve-view=true)
- [Columnstore indexes](/sql/relational-databases/indexes/columnstore-indexes-overview?view=azuresqlmi-current&preserve-view=true)
- [Real-time operational analytics with columnstore indexes](/sql/relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics?view=azuresqlmi-current&preserve-view=true)
- [Technical article: In-memory OLTP – Common Workload Patterns and Migration Considerations in SQL Server 2014](/previous-versions/dn673538(v=msdn.10))