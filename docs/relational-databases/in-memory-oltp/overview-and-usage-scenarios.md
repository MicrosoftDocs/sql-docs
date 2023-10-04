---
title: "Overview and Usage Scenarios"
description: Learn about In-Memory OLTP, a technology in SQL Server and Azure SQL Database for optimized transaction processing. Review examples and additional resources.
author: "kevin-farlee"
ms.author: "kfarlee"
ms.reviewer: wiassaf, randolphwest
ms.date: 04/26/2022
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# In-Memory OLTP overview and usage scenarios

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

[!INCLUDE[inmemory](../../includes/inmemory-md.md)] is the premier technology available in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)] for optimizing performance of transaction processing, data ingestion, data load, and transient data scenarios. This article includes an overview of the technology and outlines usage scenarios for [!INCLUDE[inmemory](../../includes/inmemory-md.md)]. Use this information to determine whether [!INCLUDE[inmemory](../../includes/inmemory-md.md)] is right for your application. The article concludes with an example that shows [!INCLUDE[inmemory](../../includes/inmemory-md.md)] objects, reference to a perf demo, and references to resources you can use for next steps.

This article covers the [!INCLUDE[inmemory](../../includes/inmemory-md.md)] technology in both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)]. For more information specific to in-memory data in Azure SQL, see [Optimize performance by using in-memory technologies in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/in-memory-oltp-overview) and [Blog: [!INCLUDE[inmemory](../../includes/inmemory-md.md)] in Azure SQL Database](https://azure.microsoft.com/blog/in-memory-oltp-in-azure-sql-database/).

## [!INCLUDE[inmemory](../../includes/inmemory-md.md)] Overview

[!INCLUDE[inmemory](../../includes/inmemory-md.md)] can provide great performance gains, for the right workloads. While customers have seen up to 30X performance gain in some cases, how much gain you see depends on the workload.

Now, where does this performance gain come from? In essence, [!INCLUDE[inmemory](../../includes/inmemory-md.md)] improves performance of transaction processing by making data access and transaction execution more efficient, and by removing lock and latch contention between concurrently executing transactions. [!INCLUDE[inmemory](../../includes/inmemory-md.md)] isn't fast because it's in-memory; it's fast because it's optimized around the data being in-memory. Data storage, access, and processing algorithms were redesigned from the ground up to take advantage of the latest enhancements in in-memory and high concurrency computing.

Now, just because data lives in-memory doesn't mean you lose it when there's a failure. By default, all transactions are fully durable, meaning that you have the same durability guarantees you get for any other table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]: as part of transaction commit, all changes are written to the transaction log on disk. If there's a failure at any time after the transaction commits, your data is there when the database comes back online. In addition, [!INCLUDE[inmemory](../../includes/inmemory-md.md)] works with all high availability and disaster recovery capabilities of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], like [Always On availability groups](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md), [Always On Failover Cluster Instances (SQL Server)](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md), backup/restore, etc.

To use [!INCLUDE[inmemory](../../includes/inmemory-md.md)] in your database, you use one or more of the following types of objects:

- *Memory-optimized tables* are used for storing user data. You declare a table to be memory-optimized at create time.
- *Non-durable tables* are used for transient data, either for caching or for intermediate result set (replacing traditional temp tables). A non-durable table is a memory-optimized table that is declared with DURABILITY=SCHEMA_ONLY, meaning that changes to these tables don't incur any IO. This avoids consuming log IO resources for cases where durability isn't a concern.
- *Memory-optimized table types* are used for table-valued parameters (TVPs), as well as intermediate result sets in stored procedures. These can be used instead of traditional table types. Table variables and TVPs that are declared using a memory-optimized table type inherit the benefits of non-durable memory-optimized tables: efficient data access, and no IO.
- *Natively compiled T-SQL modules* are used to further reduce the time taken for an individual transaction by reducing CPU cycles required to process the operations. You declare a Transact-SQL module to be natively compiled at create time. At this time, the following T-SQL modules can be natively compiled: stored procedures, triggers, and scalar user-defined functions.


[!INCLUDE[inmemory](../../includes/inmemory-md.md)] is built into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)]. Because these objects behave similar to their traditional counterparts, you can often gain performance benefits while making only minimal changes to the database and the application. Plus, you can have both memory-optimized and traditional disk-based tables in the same database, and run queries across the two. You will find a Transact-SQL script with an example for each of these types of objects towards the bottom of this article.

## Usage scenarios for [!INCLUDE[inmemory](../../includes/inmemory-md.md)]

[!INCLUDE[inmemory](../../includes/inmemory-md.md)] isn't a magic go-fast button, and isn't suitable for all workloads. For example, memory-optimized tables don't bring down your CPU utilization if most of the queries are performing aggregation over large ranges of data. Columnstore indexes help with that scenario.

Here's a list of scenarios and application patterns where we have seen customers be successful with [!INCLUDE[inmemory](../../includes/inmemory-md.md)].

### High-throughput and low-latency transaction processing

This is the core scenario for which we built [!INCLUDE[inmemory](../../includes/inmemory-md.md)]: support large volumes of transactions, with consistent low latency for individual transactions.

Common workload scenarios are: trading of financial instruments, sports betting, mobile gaming, and ad delivery. Another common pattern we've seen is a "catalog" that is frequently read and/or updated. One example is where you have large files, each distributed over multiple cluster nodes, and you catalog the location of each shard of each file in a memory-optimized table.

#### Implementation considerations

Use memory-optimized tables for your core transaction tables, that is, the tables with the most performance-critical transactions. Use natively compiled stored procedures to optimize execution of the logic associated with the business transaction. The more of the logic you can push down into stored procedures in the database, the more benefit you see from [!INCLUDE[inmemory](../../includes/inmemory-md.md)].

To get started in an existing application:

1. Use the [transaction performance analysis report](determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp.md) to identify the objects you want to migrate.
2. Use the [memory-optimization](memory-optimization-advisor.md) and [native compilation](native-compilation-advisor.md) advisors to help with migration.

### Data ingestion, including IoT (Internet-of-Things)

[!INCLUDE[inmemory](../../includes/inmemory-md.md)] is good at ingesting large volumes of data from many different sources at the same time. And it's often beneficial to ingest data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database compared with other destinations, because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] makes running queries against the data fast, and allows you to get real-time insights.

Common application patterns are:

- Ingesting sensor readings and events, and allow notifications as well as historical analysis.
- Managing batch updates, even from multiple sources, while minimizing the impact on the concurrent read workload.

#### Implementation considerations

Use a memory-optimized table for the data ingestion. If the ingestion consists mostly of inserts (rather than updates) and [!INCLUDE[inmemory](../../includes/inmemory-md.md)] storage footprint of the data is a concern, either

- Use a job to regularly batch-offload data to a disk-based table with a [clustered columnstore index](../indexes/columnstore-indexes-overview.md), using a job that does `INSERT INTO <disk-based table> SELECT FROM <memory-optimized table>`; or
- Use a [temporal memory-optimized table](../tables/system-versioned-temporal-tables-with-memory-optimized-tables.md) to manage historical data - in this mode, historical data lives on disk, and data movement is managed by the system.

The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] samples repository contains a smart grid application that uses a temporal memory-optimized table, a memory-optimized table type, and a natively compiled stored procedure, to speed up data ingestion, while managing the [!INCLUDE[inmemory](../../includes/inmemory-md.md)] storage footprint of the sensor data:

- [smart-grid-release](https://github.com/Microsoft/sql-server-samples/releases/tag/iot-smart-grid-v1.0)
- [smart-grid-source-code](https://github.com/Microsoft/sql-server-samples/tree/master/samples/applications/iot-smart-grid)

### Caching and session state

The [!INCLUDE[inmemory](../../includes/inmemory-md.md)] technology makes the database engine in SQL Server or Azure SQL databases an attractive platform for maintaining session state (for example, for an ASP.NET application) and for caching.

ASP.NET session state is a successful use case for [!INCLUDE[inmemory](../../includes/inmemory-md.md)]. With [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], one customer was about to achieve 1.2 Million requests per second. In the meantime, they have started using [!INCLUDE[inmemory](../../includes/inmemory-md.md)] for the caching needs of all mid-tier applications in the enterprise. Details: [How bwin is using [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] [!INCLUDE[inmemory](../../includes/inmemory-md.md)] to achieve unprecedented performance and scale](https://blogs.msdn.microsoft.com/sqlcat/2016/10/26/how-bwin-is-using-sql-server-2016-in-memory-oltp-to-achieve-unprecedented-performance-and-scale/)

#### Implementation considerations

You can use non-durable memory-optimized tables as a simple key-value store by storing a BLOB in a varbinary(max) column. Alternatively, you can implement a semi-structured cache with [JSON support](https://azure.microsoft.com/blog/json-support-is-generally-available-in-azure-sql-database/) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)]. Finally, you can create a full relational cache through non-durable tables with a full relational schema, including various data types and constraints.

Get started with memory-optimizing ASP.NET session state by using the scripts published on GitHub to replace the objects created by the built-in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] session state provider: [aspnet-session-state](https://github.com/Microsoft/sql-server-samples/tree/master/samples/applications/aspnet-session-state)

#### Customer case study

- bwin increased throughput with ASP.NET session state even further and implemented an enterprise-wide mid-tier caching system, with [!INCLUDE[inmemory](../../includes/inmemory-md.md)] in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]: [How bwin is using [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] [!INCLUDE[inmemory](../../includes/inmemory-md.md)] to achieve unprecedented performance and scale](https://blogs.msdn.microsoft.com/sqlcat/2016/10/26/how-bwin-is-using-sql-server-2016-in-memory-oltp-to-achieve-unprecedented-performance-and-scale/).

### tempdb object replacement

Use non-durable tables and memory-optimized table types to replace your traditional `tempdb` based structures, such as temporary tables, table variables, and table-valued parameters (TVPs).

Memory-optimized table variables and non-durable tables typically reduce CPU and completely remove log IO, when compared with traditional table variables and #temp table.

#### Implementation considerations

To get started see: [Improving temp table and table variable performance using memory optimization.](/archive/blogs/sqlserverstorageengine/improving-temp-table-and-table-variable-performance-using-memory-optimization)

#### Customer case study

- One customer was able to improve performance by 40%, just by replacing traditional TVPs with memory-optimized TVPs: [High Speed IoT Data Ingestion Using [!INCLUDE[inmemory](../../includes/inmemory-md.md)] in Azure](/archive/blogs/sqlserverstorageengine/a-technical-case-study-high-speed-iot-data-ingestion-using-in-memory-oltp-in-azure)

### ETL (Extract Transform Load)

ETL workflows often include load of data into a staging table, transformations of the data, and load into the final tables.

Use non-durable memory-optimized tables for the data staging. They completely remove all IO, and make data access more efficient.

#### Implementation considerations

If you perform transformations on the staging table as part of the workflow, you can use natively compiled stored procedures to speed up these transformations. If you can do these transformations in parallel, you get additional scaling benefits from the memory-optimization.

## Sample script

Before you can start using [!INCLUDE[inmemory](../../includes/inmemory-md.md)], you need to create a MEMORY_OPTIMIZED_DATA filegroup. In addition, we recommend using database compatibility level 130 (or higher), and set the database option MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT to ON.

You can use the script at the following location to create the filegroup in the default data folder, and configure the recommended settings:

- [enable-in-memory-oltp.sql](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/in-memory-database/in-memory-oltp/t-sql-scripts/enable-in-memory-oltp.sql)

The following sample script illustrates [!INCLUDE[inmemory](../../includes/inmemory-md.md)] objects you can create in your database.

First start by configuring the database for [!INCLUDE[inmemory](../../includes/inmemory-md.md)].

```sql
-- configure recommended DB option
ALTER DATABASE CURRENT SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT=ON;
GO
```

You can create tables with different durability:

```sql
-- memory-optimized table
CREATE TABLE dbo.table1
( c1 INT IDENTITY PRIMARY KEY NONCLUSTERED,
  c2 NVARCHAR(MAX))
WITH (MEMORY_OPTIMIZED=ON);
GO
-- non-durable table
CREATE TABLE dbo.temp_table1
( c1 INT IDENTITY PRIMARY KEY NONCLUSTERED,
  c2 NVARCHAR(MAX))
WITH (MEMORY_OPTIMIZED=ON,
      DURABILITY=SCHEMA_ONLY);
GO
```

You can create a [table type](../../t-sql/statements/create-type-transact-sql.md) as an in-memory table.

```sql
-- memory-optimized table type
CREATE TYPE dbo.tt_table1 AS TABLE
( c1 INT IDENTITY,
  c2 NVARCHAR(MAX),
  is_transient BIT NOT NULL DEFAULT (0),
  INDEX ix_c1 HASH (c1) WITH (BUCKET_COUNT=1024))
WITH (MEMORY_OPTIMIZED=ON);
GO
```

You can create a [natively compiled stored procedure](creating-natively-compiled-stored-procedures.md). For more information, see [Calling Natively Compiled Stored Procedures from Data Access Applications](calling-natively-compiled-stored-procedures-from-data-access-applications.md).

```sql
-- natively compiled stored procedure
CREATE PROCEDURE dbo.usp_ingest_table1
  @table1 dbo.tt_table1 READONLY
WITH NATIVE_COMPILATION, SCHEMABINDING
AS
BEGIN ATOMIC
    WITH (TRANSACTION ISOLATION LEVEL=SNAPSHOT,
          LANGUAGE=N'us_english')

  DECLARE @i INT = 1

  WHILE @i > 0
  BEGIN
    INSERT dbo.table1
    SELECT c2
    FROM @table1
    WHERE c1 = @i AND is_transient=0

    IF @@ROWCOUNT > 0
      SET @i += 1
    ELSE
    BEGIN
      INSERT dbo.temp_table1
      SELECT c2
      FROM @table1
      WHERE c1 = @i AND is_transient=1

      IF @@ROWCOUNT > 0
        SET @i += 1
      ELSE
        SET @i = 0
    END
  END

END
GO
-- sample execution of the proc
DECLARE @table1 dbo.tt_table1;
INSERT @table1 (c2, is_transient) VALUES (N'sample durable', 0);
INSERT @table1 (c2, is_transient) VALUES (N'sample non-durable', 1);
EXECUTE dbo.usp_ingest_table1 @table1=@table1;
SELECT c1, c2 from dbo.table1;
SELECT c1, c2 from dbo.temp_table1;
GO
```

## Resources to learn more

- [[!INCLUDE[inmemory](../../includes/inmemory-md.md)] Technologies for Faster T-SQL Performance](./survey-of-initial-areas-in-in-memory-oltp.md)
- [Performance and resource utilization benefits of [!INCLUDE[inmemory](../../includes/inmemory-md.md)] in Azure SQL Database](https://azure.microsoft.com/blog/in-memory-oltp-in-azure-sql-database/)
- [Improving temp table and table variable performance using memory optimization](/archive/blogs/sqlserverstorageengine/improving-temp-table-and-table-variable-performance-using-memory-optimization)
- [Demonstration: Performance Improvement of In-Memory OLTP](demonstration-performance-improvement-of-in-memory-oltp.md)
- [Sample Database for In-Memory OLTP](sample-database-for-in-memory-oltp.md)
- Perf demo using [!INCLUDE[inmemory](../../includes/inmemory-md.md)] can be found at: [in-memory-oltp-perf-demo-v1.0](https://github.com/Microsoft/sql-server-samples/releases/tag/in-memory-oltp-demo-v1.0)
- [17-minute video explaining [!INCLUDE[inmemory](../../includes/inmemory-md.md)] and showing the demo](https://www.youtube.com/watch?v=UHhYhSCJil4)

## See also

- [[!INCLUDE[inmemory](../../includes/inmemory-md.md)] overview and usage scenarios](overview-and-usage-scenarios.md)
- [Optimize Performance using In-Memory Technologies in Azure SQL](/azure/sql-database/sql-database-in-memory)
- [System-Versioned Temporal Tables with Memory-Optimized Tables](../tables/system-versioned-temporal-tables-with-memory-optimized-tables.md)

## Next steps

- Create a memory-optimized filegroup: [The Memory Optimized Filegroup](the-memory-optimized-filegroup.md)
- [Script to enable [!INCLUDE[inmemory](../../includes/inmemory-md.md)] and set recommended options](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/in-memory-database/in-memory-oltp/t-sql-scripts/enable-in-memory-oltp.sql)
