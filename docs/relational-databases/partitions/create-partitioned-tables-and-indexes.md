---
title: "Create partitioned tables and indexes"
titleSuffix: SQL Server, Azure SQL Database, Azure SQL Managed Instance
description: Create partitioned tables and indexes
author: VanMSFT
ms.author: vanto
ms.date: "4/20/2022"
ms.service: sql
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.createpartition.progress.f1"
  - "sql13.swb.createpartition.partitioncolumn.f1"
  - "sql13.swb.createpartition.createjob.f1"
  - "sql13.swb.createpartition.finish.f1"
  - "sql13.swb.createpartition.selectoutput.f1"
  - "sql13.swb.createpartition.partitionfunction.f1"
  - "sql13.swb.createpartition.partitionscheme.f1"
  - "sql13.swb.createpartition.getstart.f1"
  - "sql13.swb.createpartition.mappartition.f1"
  - "sql13.swb.createpartition.summary.f1"
helpviewer_keywords:
  - "partitioned indexes [SQL Server], creating"
  - "partition schemes [SQL Server], creating"
  - "partition functions [SQL Server], creating"
  - "partitioned tables [SQL Server], creating"
  - "partition functions [SQL Server]"
  - "partition schemes [SQL Server]"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create partitioned tables and indexes
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

You can create a [partitioned table or index](partitioned-tables-and-indexes.md) in SQL Server, Azure SQL Database, and Azure SQL Managed Instance by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The data in partitioned tables and indexes is horizontally divided into units that can be spread across more than one filegroup in a database, or stored in a single filegroup. Partitioning can make large tables and indexes more manageable and scalable.

Creating a partitioned table or index typically happens in three or four parts:  
  
1.  Optionally [create a filegroup](../../t-sql/statements/alter-database-transact-sql-set-options.md) or filegroups and corresponding data files that will hold the partitions specified by the partition scheme. The main reason to place partitions on multiple filegroups is to ensure you can independently perform backup and restore operations on filegroups. If this is not required, you may choose to assign all partitions to a single filegroup, using either an existing filegroup, such as `PRIMARY`, or a new filegroup with related data file(s). In nearly all scenarios, you will achieve all [benefits of partitioning](partitioned-tables-and-indexes.md#benefits-of-partitioning) whether or not you use multiple filegroups.
  
2.  Create a [partition function](../../t-sql/statements/create-partition-function-transact-sql.md) that maps the rows of a table or index into partitions based on the values of a specified column. You can use a single partition function to partition multiple objects.
  
3.  Create a [partition scheme](../../t-sql/statements/create-partition-scheme-transact-sql.md) that maps the partitions of a partitioned table or index to one filegroup or to multiple filegroups. You can use a single partition scheme to partition multiple objects.
  
4.  Create or alter a table or index and specify the partition scheme as the storage location, along with the column that will serve as the partitioning column.
 
> [!NOTE]
> Partitioning is fully supported in Azure SQL Database. Because only the `PRIMARY` filegroup is supported in Azure SQL Database, all partitions must be placed on the `PRIMARY` filegroup.

Table partitioning is also available in dedicated SQL pools in Azure Synapse Analytics, with some syntax differences. Learn more in [Partitioning tables in dedicated SQL pool](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-partition).
  
## Permissions  

Creating a partitioned table requires CREATE TABLE permission in the database and ALTER permission on the schema in which the table is being created. Creating a partitioned index requires ALTER permission on the table or view where the index is being created. Creating either a partitioned table or index requires any one of the following additional permissions:  
  
-   ALTER ANY DATASPACE permission. This permission defaults to members of the **sysadmin** fixed server role and the **db_owner** and **db_ddladmin** fixed database roles.  
  
-   CONTROL or ALTER permission on the database in which the partition function and partition scheme are being created.  
  
-   CONTROL SERVER or ALTER ANY DATABASE permission on the server of the database in which the partition function and partition scheme are being created.  

## Create a partitioned table on one filegroup using Transact-SQL

If you do not need to independently perform backup and restore operations on filegroups, partitioning a table using a single filegroup simplifies management of the partitioned table over time.

This example is suitable for Azure SQL Database, which does not support adding files and filegroups. Table partitioning is supported in Azure SQL Database by creating partitions in the `PRIMARY` filegroup. For SQL Server and Azure SQL Managed Instance, you may wish to specify a user-created filegroup, depending on your filegroup and file management practices.

The example steps through creating a partitioned table in SQL Server Management Studio (SSMS) using Transact-SQL and assigns all partitions to the `PRIMARY` filegroup. The example:

- Creates a [RANGE RIGHT partition function](partitioned-tables-and-indexes.md#partition-function) named `myRangePF1` with three boundary values using the [datetime2](../../t-sql/data-types/datetime2-transact-sql.md) data type. Three boundary values will result in a partitioned table with four partitions.
- Creates a partition scheme named `myRangePS1` that uses the `ALL TO` syntax to assign all partitions in the `myRangePF1` partition function to the `PRIMARY` filegroup.
- Creates a table named `PartitionTable` on the `myRangePS1` partition scheme, specifying a column named `col1` as the partitioning column.
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
1.  On the Standard bar, select **New Query**.  
  
1.  Copy and paste the following example into the query window and select **Execute**. This example creates a partition function and a partition scheme. A new table is created with the partition scheme specified as the storage location.

```sql
CREATE PARTITION FUNCTION myRangePF1 (datetime2(0))  
    AS RANGE RIGHT FOR VALUES ('2022-04-01', '2022-05-01', '2022-06-01') ;  
GO  

CREATE PARTITION SCHEME myRangePS1  
    AS PARTITION myRangePF1  
    ALL TO ('PRIMARY') ;  
GO  

CREATE TABLE dbo.PartitionTable (col1 datetime2(0) PRIMARY KEY, col2 char(10))  
    ON myRangePS1 (col1) ;  
GO
```


##  <a name="TsqlProcedure"></a> Create a partitioned table on multiple filegroups with Transact-SQL

Follow the steps in this section to create one or more filegroups, corresponding files, and a partitioned table using Transact-SQL in SSMS.

Both SQL Server and Azure SQL Managed Instance support creating filegroups and files. Azure SQL Managed Instance automatically configures the path for all database files added, so the `ALTER DATABASE ADD FILE` command in Azure SQL Managed Instance does not allow the `FILENAME` parameter. Azure SQL Database supports creating partitioned tables only in the `PRIMARY` filegroup. Find example code for Azure SQL Database in [Create a partitioned table on one filegroup using Transact-SQL](#create-a-partitioned-table-on-one-filegroup-using-transact-sql).
  
Run the following example against an empty database. The example:

- Adds four new filegroups to a database.
- Adds one file to each filegroup.
- Creates a [RANGE RIGHT partition function](partitioned-tables-and-indexes.md#partition-function) called `myRangePF1` with three boundary values that will partition a table into four partitions.
- Creates a partition scheme called `myRangePS1` that applies `myRangePF1` to the four new filegroups.
- Creates a partitioned table called `PartitionTable` that uses `myRangePS1` to partition `col1`.
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard toolbar, select **New Query**.  
  
3.  This example creates a new database and uses it. It then creates new filegroups, a partition function, and a partition scheme. A new table is created with the partition scheme specified as the storage location. Copy and paste the following example into the query window. 

    If you are using a managed instance, remove the `FILENAME` parameter and associated value from the `ALTER DATABASE ADD FILE` command. The managed instance will determine the file path for you automatically.

    If you are using a SQL Server instance, customize the value for the `FILENAME` parameter to a location appropriate for your instance.

    If you wish to use an existing database, remove the `CREATE DATABASE` command and alter the `USE` statement to the appropriate database name.

    SELECT **Execute**.
  
    ```sql  
    CREATE DATABASE PartitionTest;
    GO

    USE PartitionTest;
    GO
    
    ALTER DATABASE PartitionTest  
    ADD FILEGROUP test1fg;  
    GO  
    ALTER DATABASE PartitionTest  
    ADD FILEGROUP test2fg;  
    GO  
    ALTER DATABASE PartitionTest  
    ADD FILEGROUP test3fg;  
    GO  
    ALTER DATABASE PartitionTest  
    ADD FILEGROUP test4fg;   
  
    ALTER DATABASE PartitionTest   
    ADD FILE   
    (  
        NAME = partitiontest1,  
        FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\partitiontest1.ndf',  
        SIZE = 5MB,  
        FILEGROWTH = 5MB  
    )  
    TO FILEGROUP test1fg;  
    ALTER DATABASE PartitionTest   
    ADD FILE   
    (  
        NAME = partitiontest2,  
        FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\partitiontest2.ndf',  
        SIZE = 5MB,  
        FILEGROWTH = 5MB  
    )  
    TO FILEGROUP test2fg;  
    GO  
    ALTER DATABASE PartitionTest   
    ADD FILE   
    (  
        NAME = partitiontest3,  
        FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\partitiontest3.ndf',  
        SIZE = 5MB,  
        FILEGROWTH = 5MB  
    )  
    TO FILEGROUP test3fg;  
    GO  
    ALTER DATABASE PartitionTest   
    ADD FILE   
    (  
        NAME = partitiontest4,  
        FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\partitiontest4.ndf',  
        SIZE = 5MB,  
        FILEGROWTH = 5MB  
    )  
    TO FILEGROUP test4fg;  
    GO  
    
    CREATE PARTITION FUNCTION myRangePF1 (datetime2(0))  
        AS RANGE RIGHT FOR VALUES ('2022-04-01', '2022-05-01', '2022-06-01') ;  
    GO  
    
    CREATE PARTITION SCHEME myRangePS1  
        AS PARTITION myRangePF1  
        TO (test1fg, test2fg, test3fg, test4fg) ;  
    GO  
    
    CREATE TABLE PartitionTable (col1 datetime2(0) PRIMARY KEY, col2 char(10))  
        ON myRangePS1 (col1) ;  
    GO  
    ```  

## <a name="SSMSProcedure"></a> Partition a table with SSMS

Follow the steps in this section to optionally create filegroups and corresponding files, then create a partitioned table or partition an existing table using the **Create Partition Wizard** in SQL Server Management Studio (SSMS). The **Create Partition Wizard** is available in SSMS for SQL Server and Azure SQL Managed Instance. For Azure SQL Database, refer to [Create a partitioned table on one filegroup using Transact-SQL](#create-a-partitioned-table-on-one-filegroup-using-transact-sql).
  
### Create new filegroups (optional)

If you wish to place your partitioned table on one or more new [filegroups](partitioned-tables-and-indexes.md#filegroups), follow the steps in this section. Both SQL Server and Azure SQL Managed Instance support creating filegroups and files. For Azure SQL Managed Instance, the path for any files created will automatically be configured for you.
  
1. In Object Explorer, right-click the database in which you want to create a partitioned table and select **Properties**.  
  
1. In the **Database Properties -** *database_name* dialog box, under **Select a page**, select **Filegroups**.  
  
1. Under **Rows**, select **Add**. In the new row, enter the filegroup name.  
  
    > [!WARNING]  
    >  When specifying multiple filegroups, you must always have one extra filegroup in addition to the number of filegroups specified for the boundary values when you are creating partitions.  
  
1. Continue adding rows until you have created all of the filegroups for the partitioned table or tables.  
  
1. Select **OK**.  
  
1. Under **Select a page**, select **Files**.  
  
1. Under **Rows**, select **Add**. In the new row, enter a filename and select a filegroup.  
  
1. Continue adding rows until you have created at least one file for each filegroup.  
  
### Create a partitioned table  

1. Optionally, expand the **Tables** folder and create a table as you normally would. For more information, see [Create Tables &#40;Database Engine&#41;](../../relational-databases/tables/create-tables-database-engine.md). Alternatively, you can specify an existing table in the next step.  

1.  Right-click the table that you wish to partition, point to **Storage**, and then select **Create Partition...**.  
  
1.  In the **Create Partition Wizard**, on the **Welcome to the Create Partition Wizard** page, select **Next**.  
  
1.  On the **Select a Partitioning Column** page, in the **Available partitioning columns** grid, select the column on which you want to partition your table. Only columns with data types that can be used to partition data will be displayed in the **Available partitioning columns** grid. If you select a computed column as the partitioning column, the column must be created as a persisted column.  
  
     The choices you have for the partitioning column and the values range are determined primarily by the extent to which your data can be grouped in a logical way. For example, you may choose to divide your data into logical groupings by months or quarters of a year. The queries you plan to make against your data will determine whether this logical grouping is adequate for managing your table partitions. All data types are valid for use as partitioning columns, except **text**, **ntext**, **image**, **xml**, **timestamp**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, alias data types, or CLR user-defined data types.  
  
     The following additional options are available on this page:  
  
     **Collocate this table to the selected partitioned table**  
     Allows you to select a partitioned table that contains related data to join with this table on the partitioning column. Tables with partitions joined on the partitioning columns are typically queried more efficiently.  
  
     **Storage-align non-unique indexes and unique indexes with an indexed partition column**  
     Aligns all indexes of the table that are partitioned with the same partition scheme. When a table and its indexes are aligned, you can move partitions in and out of partitioned tables more effectively, because your data is partitioned in the same way.  
  
     After selecting the partitioning column and any other options, select **Next**.  
  
1. On the **Select a Partition Function** page, under **Select partition function**, select either **New partition function** or **Existing partition function**. If you choose **New partition function**, enter the name of the function. If you choose **Existing partition function**, select the name of the function you'd like to use from the list. The **Existing partition function** option will not be available if there are no other partition functions on the database.  
  
     After completing this page, select **Next**.  
  
1.  On the **Select a Partition Scheme** page, under **Select partition scheme**, select either **New partition scheme** or **Existing partition scheme**. If you choose **New partition scheme**, enter the name of the scheme. If you choose **Existing partition scheme**, select the name of the scheme you'd like to use from the list. The **Existing partition scheme** option will not be available if there are no other partition schemes on the database.  
  
     After completing this page, select **Next**.  
  
1.  On the **Map Partitions** page, under **Range**, select either **Left boundary** or **Right boundary**. **Left boundary** specifies that the highest bounding value will be included within a partition. **Right boundary** specifies that the lowest bounding value will be included in each partition. Learn more about right and left ranges in [Partition function](partitioned-tables-and-indexes.md#partition-function).

    When specifying multiple boundary points, you must always enter one extra row in addition to the rows assigning boundary values to a filegroup. 
  
    In the **Select filegroups and specify boundary values** grid, under **Filegroup**, select the filegroup into which you want to partition your data. Under **Boundary**, enter the boundary value for each filegroup. If you wish to assign multiple or all partitions to the same filegroup, select the same filegroup name for each row. If you select a filegroup on a single row and boundary value is left empty, the partition function maps the whole table or index into a single partition using the partition function name.  
  
    The following additional options are available on this page:  
  
    **Set Boundaries...**  
    Opens the **Set Boundary Values** dialog box to select the boundary values and date ranges you want for your partitions. This option is only available when you have selected a partitioning column that contains one of the following data types: **date**, **datetime**, **smalldatetime**, **datetime2**, or **datetimeoffset**.  
  
    **Estimate storage**  
    Estimates rowcount, required space, and available space for storage for each filegroup specified for the partitions. These values are displayed in the grid as read-only values.  
  
    The **Set Boundary Values** dialog box allows for the following additional options:  
  
    **Start date**  
    Selects the starting date for the range values of your partitions.  
  
    **End date**  
    Selects the ending date for the range values of your partitions. If you selected **Left boundary** on the **Map Partitions** page, this date will be the last value for each filegroup/partition. If you selected **Right boundary** on the **Map Partitions** page, this date will be the first value in the next-to-last filegroup.  
  
     **Date range**  
     Selects the date granularity or range value increment you want for each partition.  
  
     After completing this page, select **Next**.  
  
7.  In the **Select an Output Option** page, specify how you want to complete your partitioned table. Select **Create Script** to create a SQL script based the previous pages in the wizard. Select **Run immediately** to create the new partitioned table after completing all remaining pages in the wizard. Select **Schedule** to create the new partitioned table at a predetermined time in the future.  
  
     If you select **Create script**, the following options are available under **Script options**:  
  
     **Script to file**  
     Generates the script as a .sql file. Enter a file name and location in the **File name** box or select **Browse** to open the **Script File Location** dialog box. From **Save As**, select **Unicode text** or **ANSI text**.  
  
     **Script to Clipboard**  
     Saves the script to the Clipboard.  
  
     **Script to New Query Window**  
     Generates the script to a new Query Editor window. This is the default selection.  
  
     If you select **Schedule**, select **Change schedule**.  
  
    1.  In the **New Job Schedule** dialog box, in the **Name** box, enter the job schedule's name.  
  
    2.  On the **Schedule type** list, select the type of schedule:  
  
        -   **Start automatically when SQL Server Agent starts**  
  
        -   **Start whenever the CPUs become idle**  
  
        -   **Recurring**. Select this option if your new partitioned table updates with new information regularly.  
  
        -   **One time**. This is the default selection.  
  
    3.  Select or clear the **Enabled** check box to enable or disable the schedule.  
  
    4.  If you select **Recurring**:  
  
        1.  Under **Frequency**, on the **Occurs** list, specify the frequency of occurrence:  
  
            -   If you select **Daily**, in the **Recurs every** box, enter how often the job schedule repeats in days.  
  
            -   If you select **Weekly**, in the **Recurs every** box, enter how often the job schedule repeats in weeks. Select the day or days of the week on which the job schedule is run.  
  
            -   If you select **Monthly**, select either **Day** or **The**.  
  
                -   If you select **Day**, enter both the date of the month you want the job schedule to run and how often the job schedule repeats in months. For example, if you want the job schedule to run on the 15th day of the month every other month, select **Day** and enter "15" in the first box and "2" in the second box. The largest number allowed in the second box is "99".  
  
                -   If you select **The**, select the specific day of the week within the month that you want the job schedule to run and how often the job schedule repeats in months. For example, if you want the job schedule to run on the last weekday of the month every other month, select **Day**, select **last** from the first list and **weekday** from the second list, and then enter "2" in the last box. You can also select **first**, **second**, **third**, or **fourth**, as well as specific weekdays (for example: Sunday or Wednesday) from the first two lists. The largest number allowed in the last box is "99".  
  
        2.  Under **Daily frequency**, specify how often the job schedule repeats on the day the job schedule runs:  
  
            -   If you select **Occurs once at**, enter the specific time of day when the job schedule should run in the **Occurs once at** box. Enter the hour, minute, and second of the day, as well as AM or PM.  
  
            -   If you select **Occurs every**, specify how often the job schedule runs during the day chosen under **Frequency**. For example, if you want the job schedule to repeat every 2 hours during the day that the job schedule is run, select **Occurs every**, enter "2" in the first box, and then select **hour(s)** from the list. From this list you can also select **minute(s)** and **second(s)**. The largest number allowed in the first box is "100".  
  
                 In the **Starting at** box, enter the time that the job schedule should start running. In the **Ending at** box, enter the time that the job schedule should stop repeating. Enter the hour, minute, and second of the day, as well as AM or PM.  
  
        3.  Under **Duration**, in **Start date**, enter the date that you want the job schedule to start running. Select **End date** or **No end date** to indicate when the job schedule should stop running. If you select **End date**, enter the date that you want to job schedule to stop running.  
  
    5.  If you select **One Time**, under **One-time occurrence**, in the **Date** box, enter the date that the job schedule will be run. In the **Time** box, enter the time that the job schedule will be run. Enter the hour, minute, and second of the day, as well as AM or PM.  
  
    6.  Under **Summary**, in **Description**, verify that all job schedule settings are correct.  
  
    7.  Select **OK**.  
  
     After completing this page, select **Next**.  
  
8.  On the **Review Summary** page, under **Review your selections**, expand all available options to verify that all partition settings are correct. If everything is as expected, select **Finish**.  
  
9. On the **Create Partition Wizard Progress** page, monitor status information about the actions of the Create Partition Wizard. Depending on the options that you selected in the wizard, the progress page might contain one or more actions. The top box displays the overall status of the wizard and the number of status, error, and warning messages that the wizard has received.  
  
     The following options are available on the **Create Partition Wizard Progress** page:  
  
     **Details**  
     Provides the action, status, and any messages that are returned from action taken by the wizard.  
  
     **Action**  
     Specifies the type and name of each action.  
  
     **Status**  
     Indicates whether the wizard action as a whole returned the value of **Success** or **Failure**.  
  
     **Message**  
     Provides any error or warning messages that are returned from the process.  
  
     **Report**  
     Creates a report that contains the results of the Create Partition Wizard. The options are **View Report**, **Save Report to File**, **Copy Report to Clipboard**, and **Send Report as Email**.  
  
     **View Report**  
     Opens the **View Report** dialog box, which contains a text report of the progress of the Create Partition Wizard.  
  
     **Save Report to File**  
     Opens the **Save Report As** dialog box.  
  
     **Copy Report to Clipboard**  
     Copies the results of the wizard's progress report to the Clipboard.  
  
     **Send Report as Email**  
     Copies the results of the wizard's progress report into an email message.  
  
     When complete, select **Close**.  
  
 The **Create Partition Wizard** creates the partition function and scheme and then applies the partitioning to the specified table. To verify the table partitioning, in Object Explorer, right-click the table and select **Properties**. Select the **Storage** page. The page displays information such as the name of the partition function and scheme and the number of partitions.

## Query metadata of partitioned tables and indexes

You can query metadata to determine if a table is partitioned, the boundary points for a partitioned table, the partitioning column for a partitioned table, the number of rows in each partition, and if [data compression](../data-compression/data-compression.md) has been implemented on partitions.

### Determine if a table is partitioned  
  
The following query returns one or more rows if the table `PartitionTable` is partitioned, or if any nonclustered indexes on the table are partitioned. If the table is not partitioned and no nonclustered indexes on the table are partitioned, no rows are returned.  
  
```sql
SELECT SCHEMA_NAME(t.schema_id) AS SchemaName, *   
FROM sys.tables AS t   
JOIN sys.indexes AS i   
    ON t.[object_id] = i.[object_id]   
JOIN sys.partition_schemes ps   
    ON i.data_space_id = ps.data_space_id   
WHERE t.name = 'PartitionTable';   
GO  
```  
  
### Determine the boundary values for a partitioned table  
  
The following query returns the boundary values for each partition in the `PartitionTable` table.  

The query uses the `type` column in [sys.indexes](../system-catalog-views/sys-indexes-transact-sql.md) to return only information for the clustered index of the table, or for the base table if the table is a [heap](../indexes/heaps-tables-without-clustered-indexes.md). To include any partitioned nonclustered indexes in the query results, remove or comment out `AND i.type <= 1` from the query.
  
```sql
SELECT SCHEMA_NAME(t.schema_id) AS SchemaName, t.name AS TableName, i.name AS IndexName, 
    p.partition_number, p.partition_id, i.data_space_id, f.function_id, f.type_desc, 
    r.boundary_id, r.value AS BoundaryValue   
FROM sys.tables AS t  
JOIN sys.indexes AS i  
    ON t.object_id = i.object_id  
JOIN sys.partitions AS p  
    ON i.object_id = p.object_id AND i.index_id = p.index_id   
JOIN  sys.partition_schemes AS s   
    ON i.data_space_id = s.data_space_id  
JOIN sys.partition_functions AS f   
    ON s.function_id = f.function_id  
LEFT JOIN sys.partition_range_values AS r   
    ON f.function_id = r.function_id and r.boundary_id = p.partition_number  
WHERE 
    t.name = 'PartitionTable' 
    AND i.type <= 1  
ORDER BY SchemaName, t.name, i.name, p.partition_number;  
```  
  
### Determine the partition column for a partitioned table  
  
The following query returns the name of the partitioning column for table `PartitionTable`. 

The query uses the `type` column in [sys.indexes](../system-catalog-views/sys-indexes-transact-sql.md) to return only information for the clustered index of the table, or for the base table if the table is a [heap](../indexes/heaps-tables-without-clustered-indexes.md). To include any partitioned nonclustered indexes in the query results, remove or comment out `AND i.type <= 1` from the query.
  
```sql

SELECT   
    t.[object_id] AS ObjectID
    , SCHEMA_NAME(t.schema_id) AS SchemaName
    , t.name AS TableName   
    , ic.column_id AS PartitioningColumnID   
    , c.name AS PartitioningColumnName
    , i.name as IndexName
FROM sys.tables AS t   
JOIN sys.indexes AS i   
    ON t.[object_id] = i.[object_id]   
    AND i.[type] <= 1 -- clustered index or a heap   
JOIN sys.partition_schemes AS ps   
    ON ps.data_space_id = i.data_space_id   
JOIN sys.index_columns AS ic   
    ON ic.[object_id] = i.[object_id]   
    AND ic.index_id = i.index_id   
    AND ic.partition_ordinal >= 1 -- because 0 = non-partitioning column   
JOIN sys.columns AS c   
    ON t.[object_id] = c.[object_id]   
    AND ic.column_id = c.column_id   
WHERE t.name = 'PartitionTable';   
GO  
```  

### Determine the rows describe the possible range of values in each partition

The following query returns the rows by partition for table `PartitionTable`, and a description of the 'comparison operators' for the partition function in use. *Query original provided by Kalen Delaney.*

The query uses the `type` column in [sys.indexes](../system-catalog-views/sys-indexes-transact-sql.md) to return only information for the clustered index of the table, or for the base table if the table is a [heap](../indexes/heaps-tables-without-clustered-indexes.md). To include any partitioned nonclustered indexes in the query results, remove or comment out `AND i.type <= 1` from the query.

```sql
SELECT SCHEMA_NAME(t.schema_id) AS SchemaName, t.name AS TableName, i.name AS IndexName, 
    p.partition_number AS PartitionNumber, f.name AS PartitionFunctionName, p.rows AS Rows, rv.value AS BoundaryValue, 
CASE WHEN ISNULL(rv.value, rv2.value) IS NULL THEN 'N/A' 
ELSE
    CASE WHEN f.boundary_value_on_right = 0 AND rv2.value IS NULL THEN '>=' 
        WHEN f.boundary_value_on_right = 0 THEN '>' 
        ELSE '>=' 
    END + ' ' + ISNULL(CONVERT(varchar(64), rv2.value), 'Min Value') + ' ' + 
        CASE f.boundary_value_on_right WHEN 1 THEN 'and <' 
                ELSE 'and <=' END 
        + ' ' + ISNULL(CONVERT(varchar(64), rv.value), 'Max Value') 
END AS TextComparison
FROM sys.tables AS t  
JOIN sys.indexes AS i  
    ON t.object_id = i.object_id  
JOIN sys.partitions AS p  
    ON i.object_id = p.object_id AND i.index_id = p.index_id   
JOIN  sys.partition_schemes AS s   
    ON i.data_space_id = s.data_space_id  
JOIN sys.partition_functions AS f   
    ON s.function_id = f.function_id  
LEFT JOIN sys.partition_range_values AS r   
    ON f.function_id = r.function_id and r.boundary_id = p.partition_number  
LEFT JOIN sys.partition_range_values AS rv
    ON f.function_id = rv.function_id
    AND p.partition_number = rv.boundary_id     
LEFT JOIN sys.partition_range_values AS rv2
    ON f.function_id = rv2.function_id
    AND p.partition_number - 1= rv2.boundary_id
WHERE 
    t.name = 'PartitionTable'
    AND i.type <= 1 
ORDER BY t.name, p.partition_number;
```

The `TextComparison` column describes the possible range of values in each partition based on the definition of the [partition function](partitioned-tables-and-indexes.md#partition-function). Here is a view of example results from the query:

| SchemaName | TableName      | IndexName         | PartitionNumber | PartitionFunctionName | rows | BoundaryValue           | TextComparison                                   |
|------------|----------------|-------------------|-----------------|-----------------------|------|-------------------------|--------------------------------------------------|
| dbo        | PartitionTable | PK_PartitionTable | 1               | PFTest                | 0    | 2022-03-01 00:00:00.000 | >= Min Value and < Mar  1 2022 12:00AM           |
| dbo        | PartitionTable | PK_PartitionTable | 2               | PFTest                | 2    | 2022-04-01 00:00:00.000 | >= Mar  1 2022 12:00AM and < Apr  1 2022 12:00AM |
| dbo        | PartitionTable | PK_PartitionTable | 3               | PFTest                | 1    | 2022-05-01 00:00:00.000 | >= Apr  1 2022 12:00AM and < May  1 2022 12:00AM |
| dbo        | PartitionTable | PK_PartitionTable | 4               | PFTest                | 0    | 2022-06-01 00:00:00.000 | >= May  1 2022 12:00AM and < Jun  1 2022 12:00AM |
| dbo        | PartitionTable | PK_PartitionTable | 5               | PFTest                | 1    | 2022-07-01 00:00:00.000 | >= Jun  1 2022 12:00AM and < Jul  1 2022 12:00AM |
| dbo        | PartitionTable | PK_PartitionTable | 6               | PFTest                | 0    | NULL                    | >= Jul  1 2022 12:00AM and < Max Value           |

## <a name="Restrictions"></a> Limitations

Learn about limitations as well as performance considerations for partitioning in [Limitations](partitioned-tables-and-indexes.md#limitations) 

## Next steps

Learn more about related concepts in the following articles:

- [Partitioned tables and indexes](partitioned-tables-and-indexes.md)
- [Scaling out with Azure SQL Database](/azure/azure-sql/database/elastic-scale-introduction)
- [Partitioning tables in dedicated SQL pool](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-partition)
- [SQL Server and Azure SQL index architecture and design guide](../sql-server-index-design-guide.md)
- [ALTER DATABASE File and Filegroup Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md)  
- [CREATE PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-function-transact-sql.md)  
- [CREATE PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-scheme-transact-sql.md)  
- [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)
