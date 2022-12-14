---
title: "Application pattern - partitioning memory-optimized tables"
description: Learn about the In-Memory OLTP application design pattern that stores current, active data in a memory-optimized table and older data in a partitioned table.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 05/03/2020
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.custom:
  - "seo-dt-2019"
  - "issue-PR=4700-14820"
ms.assetid: 3f867763-a8e6-413a-b015-20e9672cc4d1
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Application Pattern for Partitioning Memory-Optimized Tables

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

[!INCLUDE[inmemory](../../includes/inmemory-md.md)] supports an application design pattern that lavishes performance resources on relatively current data. This pattern can apply when current data is read or updated far more frequently than older data is. In this case, we say the current data is *active* or *hot*, and the older data is *cold*.

The main idea is to store *hot* data in a memory-optimized table. On a weekly or monthly basis, older data that has become *cold* is moved to a partitioned table. The partitioned table has its data stored on a disk or other hard drive, not in memory.

Typically, this design uses a **datetime** key to enable the move process to efficiently distinguish between hot versus cold data.

## Advanced partitioning

The design intends to mimic having a partitioned table that also has one memory-optimized partition. For this design to work, you must ensure that the tables all share a common schema. The code sample later in this article shows the technique.

New data is presumed to be hot by definition. Hot data is inserted and updated in the memory-optimized table. Cold data is maintained in the traditional partitioned table. Periodically, a stored procedure adds a new partition. The partition contains the latest cold data that has been moved out of the memory-optimized table.

If an operation needs only hot data, it can use natively compiled stored procedures to access the data. Operations that might access hot or cold data must use interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)], to join the memory-optimized table with the partitioned table.

### Add a partition

Data that has recently become cold must be moved into the partitioned table. The steps for this periodic partition swap are as follows:

1. For the data in the memory-optimized table, determine the datetime that is the boundary or cutoff between hot versus newly cold data.
2. Insert the newly cold data, from the In-Memory OLTP table, into a *cold\_staging* table.
3. Delete the same cold data from the memory-optimized table.
4. Swap the cold\_staging table into a partition.
5. Add the partition.

#### Maintenance window

One of the preceding steps is to delete the newly cold data from the memory-optimized table. There is a time interval between this deletion and the final step that adds the new partition. During this interval, any application that attempts to read the newly cold data will fail.

For a related sample, see [Application-Level Partitioning](../../relational-databases/in-memory-oltp/application-level-partitioning.md).

## Code Sample

The following Transact-SQL sample is displayed in a series of smaller code blocks, only for the ease of presentation. You could append them all into one large code block for your testing.

As a whole, the T-SQL sample shows how to use a memory-optimized table with a partitioned disk-based table.

The first phases of the T-SQL sample create the database, and then create objects such as tables in the database. Later phases show how to move data from a memory-optimized table into a partitioned table.

### Create a database

This section of the T-SQL sample creates a test database. The database is configured to support both memory-optimized tables and partitioned tables.

```sql
CREATE DATABASE PartitionSample;
GO

-- Add a FileGroup, enabled for In-Memory OLTP.
-- Change file path as needed.

ALTER DATABASE PartitionSample
    ADD FILEGROUP PartitionSample_mod
    CONTAINS MEMORY_OPTIMIZED_DATA;

ALTER DATABASE PartitionSample
    ADD FILE(
        NAME = 'PartitionSample_mod',
        FILENAME = 'c:\data\PartitionSample_mod')
    TO FILEGROUP PartitionSample_mod;
GO
```

### Create a memory-optimized table for hot data

This section creates the memory-optimized table that holds the latest data, which is mostly still hot data.

```sql
USE PartitionSample;
GO

-- Create a memory-optimized table for the HOT Sales Order data.
-- Notice the index that uses datetime2.

CREATE TABLE dbo.SalesOrders_hot (
   so_id INT IDENTITY PRIMARY KEY NONCLUSTERED,
   cust_id INT NOT NULL,
   so_date DATETIME2 NOT NULL INDEX ix_date NONCLUSTERED,
   so_total MONEY NOT NULL,
   INDEX ix_date_total NONCLUSTERED (so_date desc, so_total desc)
) WITH (MEMORY_OPTIMIZED=ON);
GO
```

### Create a partitioned table for cold data

This section creates the partitioned table that holds the cold data.

```sql
-- Create a partition and table for the COLD Sales Order data.
-- Notice the index that uses datetime2.

CREATE PARTITION FUNCTION [ByDatePF](datetime2) AS RANGE RIGHT
   FOR VALUES();
GO

CREATE PARTITION SCHEME [ByDateRange]
   AS PARTITION [ByDatePF]
   ALL TO ([PRIMARY]);
GO

CREATE TABLE dbo.SalesOrders_cold (
   so_id INT NOT NULL,
   cust_id INT NOT NULL,
   so_date DATETIME2 NOT NULL,
   so_total MONEY NOT NULL,
   CONSTRAINT PK_SalesOrders_cold PRIMARY KEY (so_id, so_date),
   INDEX ix_date_total NONCLUSTERED (so_date desc, so_total desc)
) ON [ByDateRange](so_date);
GO
```

### Create a table to store cold data during move

This section creates the cold\_staging table. A view that unions the hot and cold data from the two tables is also created.

```sql
-- A table used to briefly stage the newly cold data, during moves to a partition.

CREATE TABLE dbo.SalesOrders_cold_staging (
   so_id INT NOT NULL,
   cust_id INT NOT NULL,
   so_date datetime2 NOT NULL,
   so_total MONEY NOT NULL,
   CONSTRAINT PK_SalesOrders_cold_staging PRIMARY KEY (so_id, so_date),
   INDEX ix_date_total NONCLUSTERED (so_date desc, so_total desc),
   CONSTRAINT CHK_SalesOrders_cold_staging CHECK (so_date >= '1900-01-01')
);
GO

-- A view, for retrieving the aggregation of hot plus cold data.

CREATE VIEW dbo.SalesOrders
AS SELECT so_id,
          cust_id,
          so_date,
          so_total,
          1 AS 'is_hot'
       FROM dbo.SalesOrders_hot
   UNION ALL
   SELECT so_id,
          cust_id,
          so_date,
          so_total,
          0 AS 'is_cold'
       FROM dbo.SalesOrders_cold;
GO
```

### Create the stored procedure

This section creates the stored procedure that you run periodically. The procedure moves newly cold data from the memory-optimized table into the partitioned table.

```sql
-- A stored procedure to move all newly cold sales orders data
-- to its staging location.

CREATE PROCEDURE dbo.usp_SalesOrdersOffloadToCold @splitdate datetime2
   AS
   BEGIN
      BEGIN TRANSACTION;

      -- Insert the cold data as a temporary heap.
      INSERT INTO dbo.SalesOrders_cold_staging WITH (TABLOCKX)
      SELECT so_id , cust_id , so_date , so_total
         FROM dbo.SalesOrders_hot WITH (serializable)
         WHERE so_date <= @splitdate;

      -- Delete the moved data from the hot table.
      DELETE FROM dbo.SalesOrders_hot WITH (SERIALIZABLE)
         WHERE so_date <= @splitdate;

      -- Update the partition function, and switch in the new partition.
      ALTER PARTITION SCHEME [ByDateRange] NEXT USED [PRIMARY];

      DECLARE @p INT = (
        SELECT MAX(partition_number)
            FROM sys.partitions
            WHERE object_id = OBJECT_ID('dbo.SalesOrders_cold'));

      EXEC sp_executesql
        N'ALTER TABLE dbo.SalesOrders_cold_staging
            SWITCH TO dbo.SalesOrders_cold partition @i',
        N'@i int',
        @i = @p;

      ALTER PARTITION FUNCTION [ByDatePF]()
      SPLIT RANGE( @splitdate);

      -- Modify a constraint on the cold_staging table, to align with new partition.
      ALTER TABLE dbo.SalesOrders_cold_staging
         DROP CONSTRAINT CHK_SalesOrders_cold_staging;

      DECLARE @s nvarchar( 100) = CONVERT( nvarchar( 100) , @splitdate , 121);
      DECLARE @sql nvarchar( 1000) = N'alter table dbo.SalesOrders_cold_staging 
         add constraint CHK_SalesOrders_cold_staging check (so_date > ''' + @s + ''')';
      PRINT @sql;
      EXEC sp_executesql @sql;

      COMMIT;
END;
GO
```

### Prepare sample data, and demo the stored procedure

This section generates and inserts sample data, and then runs the stored procedure as a demonstration.

```sql
-- Insert sample values into the hot table.
INSERT INTO dbo.SalesOrders_hot VALUES(1,SYSDATETIME(), 1);
GO
INSERT INTO dbo.SalesOrders_hot VALUES(1, SYSDATETIME(), 1);
GO
INSERT INTO dbo.SalesOrders_hot VALUES(1, SYSDATETIME(), 1);
GO

-- Verify that the hot data is in the table, by selecting from the view.
SELECT * FROM dbo.SalesOrders;
GO

-- Treat all data in the hot table as cold data:
-- Run the stored procedure, to move (offload) all sales orders to date to cold storage.
DECLARE @t datetime2 = SYSDATETIME();
EXEC dbo.usp_SalesOrdersOffloadToCold @t;

-- Again, read hot plus cold data from the view.
SELECT * FROM dbo.SalesOrders;
GO

-- Retrieve the name of every partition.
SELECT OBJECT_NAME( object_id) , * FROM sys.dm_db_partition_stats ps
   WHERE object_id = OBJECT_ID( 'dbo.SalesOrders_cold');

-- Insert more data into the hot table.
INSERT INTO dbo.SalesOrders_hot VALUES(2, SYSDATETIME(), 1);
GO
INSERT INTO dbo.SalesOrders_hot VALUES(2, SYSDATETIME(), 1);
GO
INSERT INTO dbo.SalesOrders_hot VALUES(2, SYSDATETIME(), 1);
GO

-- Read hot plus cold data from the view.
SELECT * FROM dbo.SalesOrders;
GO

-- Again, run the stored procedure, to move all sales orders to date to cold storage.
DECLARE @t datetime2 = SYSDATETIME();
EXEC dbo.usp_SalesOrdersOffloadToCold @t;

-- Read hot plus cold data from the view.
SELECT * FROM dbo.SalesOrders;
GO

-- Again, retrieve the name of every partition.
-- The stored procedure can modify the partitions.
SELECT OBJECT_NAME( object_id) , partition_number , row_count
  FROM sys.dm_db_partition_stats ps
  WHERE object_id = OBJECT_ID( 'dbo.SalesOrders_cold')
    AND index_id = 1;
```

### Drop all the demo objects

Remember to clean the demo test database off of your test system.

```sql
-- You must first leave the context of the PartitionSample database.

-- USE <A-Database-Name-Here>;
GO

DROP DATABASE PartitionSample;
GO
```

## See Also

[Memory-Optimized Tables](./sample-database-for-in-memory-oltp.md)
