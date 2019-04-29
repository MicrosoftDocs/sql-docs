---
title: "Temporal Table Considerations and Limitations | Microsoft Docs"
ms.custom: ""
ms.date: "05/22/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
ms.assetid: c8a21481-0f0e-41e3-a1ad-49a84091b422
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Temporal Table Considerations and Limitations
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  There are some considerations and limitations to be aware of when working with temporal tables, due to the nature of system-versioning.  
  
 Consider the following when working with temporal tables.  
  
-   A temporal table must have a primary key defined in order to correlate records between the current table and the history table, and the history table cannot have a primary key defined.  
  
-   The SYSTEM_TIME period columns used to record the **SysStartTime** and **SysEndTime** values must be defined with a datatype of datetime2.  
  
-   If the name of a history table is specified during history table creation, you must specify the schema and table name.  
  
-   By default, the history table is **PAGE** compressed.  
  
-   If current table is partitioned, the history table is created on default file group because partitioning configuration is not replicated automatically from the current table to the history table.  
  
-   Temporal and history tables cannot be **FILETABLE** and can contain columns of any supported datatype other than **FILESTREAM** since **FILETABLE** and **FILESTREAM** allow data manipulation outside of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and thus system versioning cannot be guaranteed.  
  
-   While temporal tables support blob data types, such as **(n)varchar(max)**, **varbinary(max)**, **(n)text**, and **image**, they will incur significant storage costs and have performance implications due to their size. As such, when designing your system, care should be taken when using these data types.  
  
-   History table must be created in the same database as the current table. Temporal querying over **Linked Server** is not supported.  
  
-   History table cannot have constraints (primary key, foreign key, table or column constraints).  
  
-   Indexed views are not supported on top of temporal queries (queries that use **FOR SYSTEM_TIME** clause)  
  
-   Online option (**WITH (ONLINE = ON**) has no effect on **ALTER TABLE ALTER COLUMN** in case of system-versioned temporal table. ALTER column is not performed as online regardless of which value was specified for  ONLINE option.  
  
-   **INSERT** and **UPDATE** statements cannot reference the SYSTEM_TIME period columns. Attempts to insert values directly into these columns will be blocked.  
  
-   **TRUNCATE TABLE** is not supported while **SYSTEM_VERSIONING** is **ON**  
  
-   Direct modification of the data in a history table is not permitted.  
  
-   **ON DELETE CASCADE** and **ON UPDATE CASCADE** are not permitted on the current table. In other words, when temporal table is referencing table in the foreign key relationship (corresponding to *parent_object_id* in sys.foreign_keys) CASCADE options are not allowed. To work around this limitation, use application logic or after triggers to maintain consistency on delete in primary key table (corresponding to  *referenced_object_id* in sys.foreign_keys). If primary key table is temporal and referencing table is non-temporal, there's no such  limitation. 

    **NOTE:** This limitation applies to SQL Server 2016 only. CASCADE options are supported in [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] and SQL Server 2017 starting from CTP 2.0.  
  
-   **INSTEAD OF** triggers are not permitted on either the current or the history table to avoid invalidating the DML logic. **AFTER** triggers are permitted only on the current table. They are blocked on the history table to avoid invalidating the DML logic.  
  
-   Usage of replication technologies is limited.  
  
    -   **Always On:** Fully supported  
  
    -   **Change Data Capture and Change Data Tracking:** Supported only on the current table  
  
    -   **Snapshot and transactional replication**: Only supported for a single publisher without temporal being enabled and one subscriber with temporal enabled. In this case, the publisher is used for an OLTP workload while subscriber serves for offloading reporting (including 'AS OF' querying).    
        Use of multiple subscribers is not supported since this scenario may lead to inconsistent temporal data as each of them would depend on the local system clock.  
  
    -   **Merge replication:** Not supported for temporal tables  
  
-   Regular queries only affect data in the current table. To query data in the history table, you must use temporal queries. These are discussed later in this document in the section entitled Querying Temporal Data.  
  
-   An optimal indexing strategy will include a clustered columns store index and / or a B-tree rowstore index on the current table and a clustered columnstore index on the history table for optimal storage size and performance. If you create / use your own history table, we strongly recommend that you create this type of index consisting of period columns starting with the end of period column to speed up temporal querying as well as speeding up the queries that are part of the data consistency check. The default history table has a clustered rowstore index created for you based on the period columns (end, start). At a minimum, a non-clustered rowstore index is recommended.  
  
-   The following objects/properties are not replicated from the current to the history table when the history table is created  
  
    -   Period definition  
  
    -   Identity definition  
  
    -   Indexes  
  
    -   Statistics  
  
    -   Check constraints  
  
    -   Triggers  
  
    -   Partitioning configuration  
  
    -   Permissions  
  
    -   Row-level security predicates  
  
-   A history table cannot be configured as current table in a chain of history tables.  
  

## See Also  
 [Temporal Tables](../../relational-databases/tables/temporal-tables.md)   
 [Getting Started with System-Versioned Temporal Tables](../../relational-databases/tables/getting-started-with-system-versioned-temporal-tables.md)   
 [Temporal Table System Consistency Checks](../../relational-databases/tables/temporal-table-system-consistency-checks.md)   
 [Partitioning with Temporal Tables](../../relational-databases/tables/partitioning-with-temporal-tables.md)   
 [Temporal Table Security](../../relational-databases/tables/temporal-table-security.md)   
 [Manage Retention of Historical Data in System-Versioned Temporal Tables](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)   
 [System-Versioned Temporal Tables with Memory-Optimized Tables](../../relational-databases/tables/system-versioned-temporal-tables-with-memory-optimized-tables.md)   
 [Temporal Table Metadata Views and Functions](../../relational-databases/tables/temporal-table-metadata-views-and-functions.md)  
  
  
