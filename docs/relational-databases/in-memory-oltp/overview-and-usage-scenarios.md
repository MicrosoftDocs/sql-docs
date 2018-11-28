---
title: "Overview and Usage Scenarios | Microsoft Docs"
ms.custom: ""
ms.date: "04/10/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 62c964c5-eae4-4cf1-9024-d5a19adbd652
author: "jodebrui"
ms.author: "jodebrui"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Overview and Usage Scenarios
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

In-Memory OLTP is the premier technology available in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)] for optimizing performance of transaction processing, data ingestion, data load, and transient data scenarios. This article includes an overview of the technology and outlines usage scenarios for In-Memory OLTP. Use this information to determine whether In-Memory OLTP is right for your application. The article concludes with an example that shows In-Memory OLTP objects, reference to a perf demo, and references to resources you can use for next steps.

This article covers the In-Memory OLTP technology in both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)]. The following blog post contains a deep dive into the performance and resource utilization benefits in [!INCLUDE[ssSDS](../../includes/sssds-md.md)]: 
- [In-Memory OLTP in Azure SQL Database](https://azure.microsoft.com/blog/in-memory-oltp-in-azure-sql-database/)

## In-Memory OLTP Overview

In-Memory OLTP can provide great performance gains, for the right workloads. One customer, bwin, managed to [achieve 1.2 Million requests per second](https://blogs.msdn.microsoft.com/sqlcat/2016/10/26/how-bwin-is-using-sql-server-2016-in-memory-oltp-to-achieve-unprecedented-performance-and-scale/) with a single machine running [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], leveraging In-Memory OLTP. Another customer, Quorum, managed to double their workload while [reducing their resource utilization by 70%](https://customers.microsoft.com/story/quorum-doubles-key-databases-workload-while-lowering-dtu-with-sql-database), by leveraging In-Memory OLTP in [!INCLUDE[ssSDS](../../includes/sssds-md.md)]. While customers have seen up to 30X performance gain in some cases, how much gain you see depends on the workload.

Now, where does this performance gain come from? In essence, In-Memory OLTP improves performance of transaction processing by making data access and transaction execution more efficient, and by removing lock and latch contention between concurrently executing transactions: it is not fast because it is in-memory; it is fast because it is optimized around the data being in-memory. Data storage, access, and processing algorithms were redesigned from the ground up to take advantage of the latest enhancements in in-memory and high concurrency computing.

Now, just because data lives in-memory does not mean you lose it when there is a failure. By default, all transactions are fully durable, meaning that you have the same durability guarantees you get for any other table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]: as part of transaction commit, all changes are written to the transaction logon disk. If there is a failure at any time after the transaction commits, your data is there when the database comes back online. In addition, In-Memory OLTP works with all high availability and disaster recovery capabilities of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], like Always On, backup/restore, etc.

To leverage In-Memory OLTP in your database, you use one or more of the following types of objects:

- *Memory-optimized tables* are used for storing user data. You declare a table to be memory-optimized at create time.
- *Non-durable tables* are used for transient data, either for caching or for intermediate result set (replacing traditional temp tables). A non-durable table is a memory-optimized table that is declared with DURABILITY=SCHEMA_ONLY, meaning that changes to these tables do not incur any IO. This avoids consuming log IO resources for cases where durability is not a concern.
- *Memory-optimized table types* are used for table-valued parameters (TVPs), as well as intermediate result sets in stored procedures. These can be used instead of traditional table types. Table variables and TVPs that are declared using a memory-optimized table type inherit the benefits of non-durable memory-optimized tables: efficient data access, and no IO.
- *Natively compiled T-SQL modules* are used to further reduce the time taken for an individual transaction by reducing CPU cycles required to process the operations. You declare a Transact-SQL module to be natively compiled at create time. At this time, the following T-SQL modules can be natively compiled: stored procedures, triggers, and scalar user-defined functions.

In-Memory OLTP is built into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)]. And because these objects behave similar to their traditional counterparts, you can often gain performance benefits while making only minimal changes to the database and the application. Plus, you can have both memory-optimized and traditional disk-based tables in the same database, and run queries across the two. You find a Transact-SQL script showing an example for each of these types of objects towards the bottom of this article.

## Usage Scenarios for In-Memory OLTP

In-Memory OLTP is not a magic go-fast button, and is not suitable for all workloads. For example, memory-optimized tables don't bring down your CPU utilization if most of the queries are performing aggregation over large ranges of data - Columnstore indexes help with that scenario.

Here is a list of scenarios and application patterns where we have seen customers be successful with In-Memory OLTP.

### High-throughput and low-latency transaction processing

This is the core scenario for which we built In-Memory OLTP: support large volumes of transactions, with consistent low latency for individual transactions.

Common workload scenarios are: trading of financial instruments, sports betting, mobile gaming, and ad delivery. Another common pattern we've seen is a "catalog" that is frequently read and/or updated. One example is where you have large files, each distributed over a number of cluster nodes, and you catalog the location of each shard of each file in a memory-optimized table.

#### Implementation considerations

Use memory-optimized tables for your core transaction tables, i.e., the tables with the most performance-critical transactions. Use natively compiled stored procedures to optimize execution of the logic associated with the business transaction. The more of the logic you can push down into stored procedures in the database, the more benefit you see from In-Memory OLTP.

To get started in an existing application:
1. use the [transaction performance analysis report](determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp.md) to identify the objects you want to migrate, 
2. and use the [memory-optimization](memory-optimization-advisor.md) and [native compilation](native-compilation-advisor.md) advisors to help with migration.

#### Customer Case Studies

- CMC Markets leverages In-Memory OLTP in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to achieve consistent low latency: [Because a second is too long to wait, this financial services firm is updating its trading software now.](https://customers.microsoft.com/story/because-a-second-is-too-long-to-wait-this-financial-services-firm-is-updating-its-trading-software)
- Derivco leverages In-Memory OLTP in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to support increased throughput and handle spikes in the workload: [When an online gaming company doesn't want to risk its future, it bets on [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)].](https://customers.microsoft.com/story/when-an-online-gaming-company-doesnt-want-to-risk-its-future-it-bets-on-sql-server-2016)


### Data ingestion, including IoT (Internet-of-Things)

In-Memory OLTP is good at ingesting large volumes of data from many different sources at the same time. And it is often beneficial to ingest data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database compared with other destinations, because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] makes running queries against the data fast, and allows you to get real-time insights.

Common application patterns are: 
-  Ingesting sensor readings and events, and allow notifications as well as historical analysis. 
-  Managing batch updates, even from multiple sources, while minimizing the impact on the concurrent read workload.

#### Implementation considerations

Use a memory-optimized table for the data ingestion. If the ingestion consists mostly of inserts (rather than updates) and In-Memory OLTP storage footprint of the data is a concern, either

- Use a job to regularly batch-offload data to a disk-based table with a [Clustered Columnstore index](../indexes/columnstore-indexes-overview.md), using a job that does `INSERT INTO <disk-based table> SELECT FROM <memory-optimized table>`; or
- Use a [temporal memory-optimized table](../tables/system-versioned-temporal-tables-with-memory-optimized-tables.md) to manage historical data - in this mode, historical data lives on disk, and data movement is managed by the system.

The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] samples repository contains a smart grid application that uses a temporal memory-optimized table, a memory-optimized table type, and a natively compiled stored procedure, to speed up data ingestion, while managing the In-Memory OLTP storage footprint of the sensor data: 

 - [smart-grid-release](https://github.com/Microsoft/sql-server-samples/releases/tag/iot-smart-grid-v1.0) 
 - [smart-grid-source-code](https://github.com/Microsoft/sql-server-samples/tree/master/samples/applications/iot-smart-grid)
 
#### Customer Case Studies

- [Quorum doubles key database's workload while lowering utilization by 70% by leveraging In-Memory OLTP in Azure SQL Database](https://customers.microsoft.com/story/quorum-doubles-key-databases-workload-while-lowering-dtu-with-sql-database)
- EdgeNet improved the performance of batch data load and removed the need to maintain a mid-tier cache, with In-Memory OLTP in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]: [Data Services Firm Gains Real-Time Access to Product Data with In-Memory Technology](https://customers.microsoft.com/story/data-services-firm-gains-real-time-access-to-product-d)
- Beth Israel Deaconess Medical Center was able to dramatically improve data ingestion rate from domain controllers, and handle spikes in the workload, with In-Memory OLTP in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]: [https://customers.microsoft.com/story/strengthening-data-security-and-creating-more-time-for]

### Caching and session state

The In-Memory OLTP technology makes SQL really attractive for maintaining session state (e.g., for an ASP.NET application) and for caching.

ASP.NET session state is a very successful use case for In-Memory OLTP. With [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], one customer was about to achieve 1.2 Million requests per second. In the meantime, they have started using In-Memory OLTP for the caching needs of all mid-tier applications in the enterprise. Details: [How bwin is using [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] In-Memory OLTP to achieve unprecedented performance and scale](https://blogs.msdn.microsoft.com/sqlcat/2016/10/26/how-bwin-is-using-sql-server-2016-in-memory-oltp-to-achieve-unprecedented-performance-and-scale/)

#### Implementation considerations

You can use non-durable memory-optimized tables as a simple key-value store by storing a BLOB in a varbinary(max) column. Alternatively, you can implement a semi-structured cache with [JSON support](https://azure.microsoft.com/blog/json-support-is-generally-available-in-azure-sql-database/) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)]. Finally, you can create a full relational cache through non-durable tables with a full relational schema, including various data types and constraints.

Get started with memory-optimizing ASP.NET session state by leveraging the scripts published on GitHub to replace the objects created by the built-in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] session state provider:

- [aspnet-session-state](https://github.com/Microsoft/sql-server-samples/tree/master/samples/applications/aspnet-session-state)

#### Customer case studies

- bwin was able to dramatically increase throughput and reduce hardware footprint for ASP.NET session state, with In-Memory OLTP in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]: [Gaming Site Can Scale to 250,000 Requests Per Second and Improve Player Experience](https://customers.microsoft.com/story/gaming-site-can-scale-to-250000-requests-per-second-an)
- bwin increased throughput with ASP.NET session state even further and implemented an enterprise-wide mid-tier caching system, with In-Memory OLTP in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]: [How bwin is using [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] In-Memory OLTP to achieve unprecedented performance and scale](https://blogs.msdn.microsoft.com/sqlcat/2016/10/26/how-bwin-is-using-sql-server-2016-in-memory-oltp-to-achieve-unprecedented-performance-and-scale/)

### Tempdb object replacement

Leverage non-durable tables and memory-optimized table types to replace your traditional TempDB based structures, such as temporary tables, table variables, and table-valued parameters (TVPs).

Memory-optimized table variables and non-durable tables typically reduce CPU and completely remove log IO, when compared with traditional table variables and #temp table.

#### Implementation considerations

To get started see: [Improving temp table and table variable performance using memory optimization.](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2016/03/21/improving-temp-table-and-table-variable-performance-using-memory-optimization/)

#### Customer Case Studies

- One customer was able to improve performance by 40%, just by replacing traditional TVPs with memory-optimized TVPs: [High Speed IoT Data Ingestion Using In-Memory OLTP in Azure](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2016/04/07/a-technical-case-study-high-speed-iot-data-ingestion-using-in-memory-oltp-in-azure/)
- SentryOne significantly improved data ingestion capacity with almost zero latency in their monitoring solution by swapping tables in tempdb to In-Memory OLTP tables as part of their enterprise scalability improvements: [Solution provider breaks through performance ceiling with data monitoring innovation.](https://customers.microsoft.com/story/sentryone-partner-professional-services-sql-server-azure)

### ETL (Extract Transform Load)

ETL workflows often include load of data into a staging table, transformations of the data, and load into the final tables.

#### Implementation considerations

Use non-durable memory-optimized tables for the data staging. They completely remove all IO, and make data access more efficient.

If you perform transformations on the staging table as part of the workflow, you can use natively compiled stored procedures to speed up these transformations. If you can do these transformations in parallel you get additional scaling benefits from the memory-optimization.

## Sample Script

Before you can start using In-Memory OLTP, you need to create a MEMORY_OPTIMIZED_DATA filegroup. In addition, we recommend using database compatibility level 130 (or higher), and set the database option MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT to ON.

You can use the script at the following location to create the filegroup in the default data folder, and configure the recommended settings:

- [enable-in-memory-oltp.sql](https://raw.githubusercontent.com/Microsoft/sql-server-samples/master/samples/features/in-memory/t-sql-scripts/enable-in-memory-oltp.sql)

The following script illustrates In-Memory OLTP objects you can create in your database:

```sql
-- configure recommended DB option
ALTER DATABASE CURRENT SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT=ON
GO
-- memory-optimized table
CREATE TABLE dbo.table1
( c1 INT IDENTITY PRIMARY KEY NONCLUSTERED,
  c2 NVARCHAR(MAX))
WITH (MEMORY_OPTIMIZED=ON)
GO
-- non-durable table
CREATE TABLE dbo.temp_table1
( c1 INT IDENTITY PRIMARY KEY NONCLUSTERED,
  c2 NVARCHAR(MAX))
WITH (MEMORY_OPTIMIZED=ON,
      DURABILITY=SCHEMA_ONLY)
GO
-- memory-optimized table type
CREATE TYPE dbo.tt_table1 AS TABLE
( c1 INT IDENTITY,
  c2 NVARCHAR(MAX),
  is_transient BIT NOT NULL DEFAULT (0),
  INDEX ix_c1 HASH (c1) WITH (BUCKET_COUNT=1024))
WITH (MEMORY_OPTIMIZED=ON)
GO
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
DECLARE @table1 dbo.tt_table1
INSERT @table1 (c2, is_transient) VALUES (N'sample durable', 0)
INSERT @table1 (c2, is_transient) VALUES (N'sample non-durable', 1)
EXECUTE dbo.usp_ingest_table1 @table1=@table1
SELECT c1, c2 from dbo.table1
SELECT c1, c2 from dbo.temp_table1
GO
```   

## Resources to learn more:

[In-Memory OLTP Technologies for Faster T-SQL Performance](https://msdn.microsoft.com/library/mt694156.aspx)   
Perf demo using In-Memory OLTP can be found at: [in-memory-oltp-perf-demo-v1.0](https://github.com/Microsoft/sql-server-samples/releases/tag/in-memory-oltp-demo-v1.0)   
[17-minute video explaining In-Memory OLTP and showing the demo](https://www.youtube.com/watch?v=l5l5eophmK4) (demo is at 8:25)   
[Script to enable In-Memory OLTP and set recommended options](https://raw.githubusercontent.com/Microsoft/sql-server-samples/master/samples/features/in-memory/t-sql-scripts/enable-in-memory-oltp.sql)   
[Main In-Memory OLTP documentation](in-memory-oltp-in-memory-optimization.md)   
[Performance and resource utilization benefits of In-Memory OLTP in Azure SQL Database](https://azure.microsoft.com/blog/in-memory-oltp-in-azure-sql-database/)  
[Improving temp table and table variable performance using memory optimization](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2016/03/21/improving-temp-table-and-table-variable-performance-using-memory-optimization/)   
[Optimize Performance using In-Memory Technologies in SQL Database](https://docs.microsoft.com/azure/sql-database/sql-database-in-memory)  
[System-Versioned Temporal Tables with Memory-Optimized Tables](../tables/system-versioned-temporal-tables-with-memory-optimized-tables.md)  
[In-Memory OLTP - Common Workload Patterns and Migration Considerations](https://msdn.microsoft.com/library/dn673538.aspx). 
