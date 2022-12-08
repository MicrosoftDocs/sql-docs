---
title: How Online Index Operations Work
description: How Online Index Operations Work
author: MikeRayMSFT
ms.author: mikeray
ms.date: 02/17/2017
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "online index operations"
  - "source indexes [SQL Server]"
  - "pre-existing indexes [SQL Server]"
  - "target indexes [SQL Server]"
  - "temporary mapping index [SQL Server]"
  - "index temporary mappings [SQL Server]"
ms.assetid: eef0c9d1-790d-46e4-a758-d0bf6742e6ae
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# How Online Index Operations Work
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)] 

  This topic defines the structures that exist during an online index operation and shows the activities associated with these structures.  
  
## Online Index Structures  
 To allow for concurrent user activity during an index data definition language (DDL) operation, the following structures are used during the online index operation: source and pre-existing indexes, target, and for rebuilding a heap or dropping a clustered index online, a temporary mapping index.  
  
-   **Source and pre-existing indexes**  
  
     The source is the original table or clustered index data. Pre-existing indexes are any nonclustered indexes that are associated with the source structure. For example, if the online index operation is rebuilding a clustered index that has four associated nonclustered indexes, the source is the existing clustered index and the pre-existing indexes are the nonclustered indexes.  
  
     The pre-existing indexes are available to concurrent users for select, insert, update, and delete operations. This includes bulk inserts (supported but not recommended) and implicit updates by triggers and referential integrity constraints. All pre-existing indexes are available for queries and searches. This means they may be selected by the query optimizer and, if necessary, specified in index hints.  
  
-   **Target**  
  
     The target or targets is the new index (or heap) or a set of new indexes that is being created or rebuilt. User insert, update, and delete operations to the source are applied by the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to the target during the index operation. For example, if the online index operation is rebuilding a clustered index, the target is the rebuilt clustered index; the [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not rebuild nonclustered indexes when a clustered index is rebuilt.  
  
     The target index is not searched while processing SELECT statements until the index operation is committed. Internally, the index is marked as write-only.  
  
-   **Temporary mapping index**  
  
     Online index operations that create, drop, or rebuild a clustered index also require a temporary mapping index. This temporary index is used by concurrent transactions to determine which records to delete in the new indexes that are being built when rows in the underlying table are updated or deleted. This nonclustered index is created in the same step as the new clustered index (or heap) and does not require a separate sort operation. Concurrent transactions also maintain the temporary mapping index in all their insert, update, and delete operations.  
  
## Online Index Activities  
 During a simple online index operation, such as creating a clustered index on a nonindexed table (heap), the source and target go through three phases: preparation, build, and final.  
  
 The following illustration shows the process for creating an initial clustered index online. The source object (the heap) has no other indexes. The source and target structure activities are shown for each phase; concurrent user select, insert, update, and delete operations are also shown. The preparation, build, and final phases are indicated together with the lock modes used in each phase.  
  
 ![Activities performed during online index operation](../../relational-databases/indexes/media/online-index.gif "Activities performed during online index operation")  
  
## Source Structure Activities  
 The following table lists the activities involving the source structures during each phase of the index operation and the corresponding locking strategy.  
  
|Phase|Source activity|Source locks|  
|-----------|---------------------|------------------|  
|Preparation<br /><br /> Short phase|System metadata preparation to create the new empty index structure.<br /><br /> A snapshot of the table is defined. That is, row versioning is used to provide transaction-level read consistency.<br /><br /> Concurrent user write operations on the source are blocked for a short period.<br /><br /> No concurrent DDL operations are allowed except creating multiple nonclustered indexes.|S (Shared) on the table*<br /><br /> IS (Intent Shared)<br /><br /> INDEX_BUILD_INTERNAL_RESOURCE\*\*|  
|Build<br /><br /> Main phase|The data is scanned, sorted, merged, and inserted into the target in bulk load operations.<br /><br /> Concurrent user select, insert, update, and delete operations are applied to both the pre-existing indexes and any new indexes being built.|IS<br /><br /> INDEX_BUILD_INTERNAL_RESOURCE**|  
|Final<br /><br /> Short phase|All uncommitted update transactions must complete before this phase starts. Depending on the acquired lock, all new user read or write transactions are blocked for a short period until this phase is completed.<br /><br /> System metadata is updated to replace the source with the target.<br /><br /> The source is dropped if it is required. For example, after rebuilding or dropping a clustered index.|INDEX_BUILD_INTERNAL_RESOURCE**<br /><br /> S on the table if creating a nonclustered index.\*<br /><br /> SCH-M (Schema Modification) if any source structure (index or table) is dropped.\*|  
  
 \* The index operation waits for any uncommitted update transactions to complete before acquiring the S lock or SCH-M lock on the table. If a long running query is taking place, the online index operation waits until the query has finished.
  
 ** The resource lock INDEX_BUILD_INTERNAL_RESOURCE prevents the execution of concurrent data definition language (DDL) operations on the source and pre-existing structures while the index operation is in progress. For example, this lock prevents concurrent rebuild of two indexes on the same table. Although this resource lock is associated with the Sch-M lock, it does not prevent data manipulation statements.  
  
 The previous table shows a single Shared (S) lock acquired during the build phase of an online index operation that involves a single index. When clustered and nonclustered indexes are built, or rebuilt, in a single online index operation (for example, during the initial clustered index creation on a table that contains one or more nonclustered indexes) two short-term S locks are acquired during the build phase followed by long-term Intent Shared (IS) locks. One S lock is acquired first for the clustered index creation and when creating the clustered index is completed, a second short-term S lock is acquired for creating the nonclustered indexes. After the nonclustered indexes are created, the S lock is downgraded to an IS lock until the final phase of the online index operation.  

For more information about how locks are used and how you can manage them, see [Arguments](../../t-sql/statements/alter-table-index-option-transact-sql.md#arguments).

### Target Structure Activities  
 The following table lists the activities that involve the target structure during each phase of the index operation and the corresponding locking strategy.  
  
|Phase|Target activity|Target locks|  
|-----------|---------------------|------------------|  
|Preparation|New index is created and set to write-only.|IS|  
|Build|Data is inserted from source.<br /><br /> User modifications (inserts, updates, deletes) applied to the source are applied.<br /><br /> This activity is transparent to the user.|IS|  
|Final|Index metadata is updated.<br /><br /> Index is set to read/write status.|S<br /><br /> or<br /><br /> SCH-M|  
  
 The target is not accessed by SELECT statements issued by the user until the index operation is completed.  
  
 After the preparation and final phase is completed, the query and update plans that are stored in the procedure cache are invalidated. Subsequent queries use the new index.  
  
 The lifetime of a cursor declared on a table that is involved in an online index operation is limited by the online index phases. Update cursors are invalidated at each phase. Read-only cursors are invalidated only after the final phase.  
  
## Related Content  
 [Perform Index Operations Online](../../relational-databases/indexes/perform-index-operations-online.md)  
  
 [Guidelines for Online Index Operations](../../relational-databases/indexes/guidelines-for-online-index-operations.md)  
  
## Next steps

[ALTER TABLE index options](../../t-sql/statements/alter-table-index-option-transact-sql.md#arguments)
