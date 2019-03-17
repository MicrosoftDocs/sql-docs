---
title: "System-Versioned Temporal Tables with Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "07/12/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
ms.assetid: 23274522-e5cf-4095-bed8-bf986d6342e0
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# System-Versioned Temporal Tables with Memory-Optimized Tables
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  System-versioned temporal tables for [Memory-Optimized Tables](../../relational-databases/in-memory-oltp/memory-optimized-tables.md) are designed to provide cost-effective solution for scenarios where [data audit and point in time analysis](https://msdn.microsoft.com/library/mt631669.aspx) are required on top of data collected with In-Memory OLTP workloads. They provide high transactional throughput, lock-free concurrency and at the same time, ability to store large amount of history data that can be easily queried.  
  
## Overview  
 System-versioned temporal tables automatically keep a full history of data changes and expose convenient Transact-SQL extensions for point in time analysis. In a typical scenario, data history is retained for a very long period of time (multiple months, even years), even though it is not regularly queried.  
  
 Data audit and time-based analysis can be demanded in different environments, especially in OLTP systems that process extremely large numbers of requests and where In-Memory OLTP technology is used. However, using memory-optimized tables in temporal scenarios is challenging because a huge amount of generated historical data commonly exceeds the limit of available RAM memory. At the same time using RAM to store read-only historical data that is accessed less frequently as it becomes older is not an optimal solution.  
  
 System-versioned temporal tables for memory-optimzed tables provide high transactional throughput, lock-free concurrency and at the same time, ability to store large amount of history data by using in-memory tables for storing current data (the temporal table) and disk-based tables for historical data. The impact on DML operations is minimalized through the use of an internal, auto-generated memory-optimized staging table that stores recent history and enables DMLs to be executed from natively compiled code.  
  
 The following diagram illustrates this architecture.![Temporal In-Memory Architecture](../../relational-databases/tables/media/temporal-in-memory-architecture.png "Temporal In-Memory Architecture")  
  
## Implementation Details  
 The following facts about system-versioned temporal tables with memory optimized tables  are considerations of which you need to be aware when creating a system-versioned memory-optimized table. For syntax options and for an example, see [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md).  
  
-   Only durable memory-optimized tables can be system-versioned (**DURABILITY = SCHEMA_AND_DATA**).  
  
-   History table for memory-optimized system-versioned table must be disk-based, regardless if it was created by the end user or the system.  
  
-   Queries that affect only the current table (in-memory) can be used in [natively compiled T-SQL modules](https://msdn.microsoft.com/library/dn133184.aspx). Temporal queries using the FOR SYSTEM TIME clause are not supported in natively compiled modules. Use of the FOR SYSTEM TIME clause with memory-optimized tables in ad hoc queries and non-native modules is supported.  
  
-   When **SYSTEM_VERSIONING = ON**, an internal memory-optimized staging table is automatically created to accept the most recent system-versioned changes that are results of update and delete operations on memory-optimized current table.  
  
-   Data from the internal memory-optimized staging table is regularly moved to the disk-based history table by the asynchronous data flush task. This data flush mechanism has a goal to keep the internal memory buffers at less than 10% of the memory consumption of their parent objects. You can track the total memory consumption of memory-optimized system-versioned temporal table by querying [sys.dm_db_xtp_memory_consumers &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-memory-consumers-transact-sql.md) and summarizing the data for the internal memory-optimized staging table and the current temporal table.  
  
-   You can enforce a data flush by invoking [sp_xtp_flush_temporal_history](../../relational-databases/system-stored-procedures/temporal-table-sp-xtp-flush-temporal-history.md).  
  
-   When **SYSTEM_VERSIONING = OFF** or when schema of system-versioned table is modified by adding, dropping or altering columns, the entire contents of the internal staging buffer is moved into the disk-based history table.  
  
-   Querying of historical data is effectively under SNAPSHOT isolation level and always returns a union between in-memory staging buffer and disk based table without duplicates.   
  
-   **ALTER TABLE** operations that change the table schema internally must perform a data flush, which may prolong the duration of the operation.  
  
## The Internal Memory-Optimized Staging Table  
 The internal memory-optimized staging table is an internal object that is created by the system to optimize DML operations.  
  
-   The table name is generated in the following format: **Memory_Optimized_History_Table_<object_id>** where *<object_id>* is identifier of the current temporal table.  
  
-   The table replicates the schema of current temporal table plus one BIGINT column. This additional column guarantees the uniqueness of the rows moved to internal history buffer.  
  
-   The additional column has the following name format: **Change_ID[_< suffix>]**, where *_\<suffix>* is optionally added in the case where the table already has a *Change_ID* column.  
  
-   The maximum row size for a system-versioned memory-optimized table is reduced by 8 bytes because of the additional BIGINT column in staging table. The new maximum is now 8052 bytes.  
  
-   The internal memory-optimized staging table is not represented in Object Explorer of SQL Server Management Studio.  
  
-   Metadata about this table as well as its connection with current temporal table can be found in [sys.internal_tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-internal-tables-transact-sql.md).  
  
## The Data Flush Task  
 The data flush is a regularly activated task that checks whether any memory-optimized table meets a memory size-based condition for data movement. Data movement starts when memory consumption of internal staging table reaches 8% of memory consumption of current temporal table.  
  
 The data flush task is activated regularly with a schedule that varies based on the existing workload. With a heavy workload, as frequent as every 5 seconds, and with a light workload, as infrequent as every 1 minute. One thread is spawned for each internal memory-optimized staging table that needs cleanup.  
  
 Data flush deletes all records from in-memory internal buffer that are older than the oldest currently running transaction to move these records to the disk-based history table.  
  
 You can enforce a data flush by invoking [sp_xtp_flush_temporal_history](../../relational-databases/system-stored-procedures/temporal-table-sp-xtp-flush-temporal-history.md) and specifying the schema and table name:   
**sys.sp_xtp_flush_temporal_history  @schema_name, @object_name**. With this user-executed command, the same data movement process is invoked as when data flush task is invoked by the system on internal schedule.  
  
## See Also  
 [Temporal Tables](../../relational-databases/tables/temporal-tables.md)   
 [Getting Started with System-Versioned Temporal Tables](../../relational-databases/tables/getting-started-with-system-versioned-temporal-tables.md)   
 [Temporal Table Usage Scenarios](../../relational-databases/tables/temporal-table-usage-scenarios.md)   
 [Temporal Table System Consistency Checks](../../relational-databases/tables/temporal-table-system-consistency-checks.md)   
 [Partitioning with Temporal Tables](../../relational-databases/tables/partitioning-with-temporal-tables.md)   
 [Temporal Table Considerations and Limitations](../../relational-databases/tables/temporal-table-considerations-and-limitations.md)   
 [Temporal Table Security](../../relational-databases/tables/temporal-table-security.md)   
 [Manage Retention of Historical Data in System-Versioned Temporal Tables](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)   
 [Temporal Table Metadata Views and Functions](../../relational-databases/tables/temporal-table-metadata-views-and-functions.md)  
  
  
