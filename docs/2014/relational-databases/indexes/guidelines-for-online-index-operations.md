---
title: "Guidelines for Online Index Operations | Microsoft Docs"
ms.custom: ""
ms.date: "11/11/2016"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "clustered indexes, online operations"
  - "online index operations"
  - "indexes [SQL Server], online operations"
  - "disk space [SQL Server], indexes"
  - "nonclustered indexes [SQL Server], online operations"
  - "transaction logs [SQL Server], indexes"
ms.assetid: d82942e0-4a86-4b34-a65f-9f143ebe85ce
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Guidelines for Online Index Operations
  When you perform online index operations, the following guidelines apply:  
  
-   Clustered indexes must be created, rebuilt, or dropped offline when the underlying table contains the following large object (LOB) data types: `image`, **ntext**, and `text`.  
  
-   Indexes on local temp tables cannot be created, rebuilt, or dropped online. This restriction does not apply to indexes on global temp tables.  
  
> [!NOTE]  
>  Online index operations are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
 The following table shows the index operations that can be performed online and the indexes that are excluded from these online operations. Additional restrictions are also included.  
  
|Online index operation|Excluded indexes|Other restrictions|  
|----------------------------|----------------------|------------------------|  
|ALTER INDEX REBUILD|Disabled clustered index or disabled indexed view<br /><br /> XML index <br /><br />Columnstore index<br /><br /> Index on a local temp table|Specifying the keyword ALL may cause the operation to fail when the table contains an excluded index.<br /><br /> Additional restrictions on rebuilding disabled indexes apply. For more information, see [Disable Indexes and Constraints](disable-indexes-and-constraints.md).|  
|CREATE INDEX|XML index<br /><br /> Initial unique clustered index on a view<br /><br /> Index on a local temp table||  
|CREATE INDEX WITH DROP_EXISTING|Disabled clustered index or disabled indexed view<br /><br /> Index on a local temp table<br /><br /> XML index||  
|DROP INDEX|Disabled index<br /><br /> XML index<br /><br /> Nonclustered index<br /><br /> Index on a local temp table|Multiple indexes cannot be specified within a single statement.|  
|ALTER TABLE ADD CONSTRAINT (PRIMARY KEY or UNIQUE)|Index on a local temp table<br /><br /> Clustered index|Only one subclause is allowed at a time. For example, you cannot add and drop PRIMARY KEY or UNIQUE constraints in the same ALTER TABLE statement.|  
||||  
  
 The underlying table cannot be modified, truncated, or dropped while an online index operation is in process.  
  
 The online option setting (ON or OFF) specified when you create or drop a clustered index is applied to any nonclustered indexes that must be rebuilt. For example, if the clustered index is built online by using CREATE INDEX WITH DROP_EXISTING, ONLINE=ON, all associated nonclustered indexes are re-created online also.  
  
 When you create or rebuild a UNIQUE index online, the index builder and a concurrent user transaction may try to insert the same key, therefore violating uniqueness. If a row entered by a user is inserted into the new index (target) before the original row from the source table is moved to the new index, the online index operation will fail.  
  
 Although not common, the online index operation can cause a deadlock when it interacts with database updates because of user or application activities. In these rare cases, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] will select the user or application activity as a deadlock victim.  
  
 You can perform concurrent online index DDL operations on the same table or view only when you are creating multiple new nonclustered indexes, or reorganizing nonclustered indexes. All other online index operations performed at the same time fail. For example, you cannot create a new index online while rebuilding an existing index online on the same table.  
  
 An online operation cannot be performed when an index contains a column of the large object type, and in the same transaction there are update operations before this online operation. To work around this issue, place the online operation outside the transaction or place it before any updates in the transaction.  
  
## Disk Space Considerations  
 Generally, disk space requirements are the same for online and offline index operations. An exception is additional disk space required by the temporary mapping index. This temporary index is used in online index operations that create, rebuild, or drop a clustered index. Dropping a clustered index online requires as much space as creating a clustered index online. For more information, see [Disk Space Requirements for Index DDL Operations](disk-space-requirements-for-index-ddl-operations.md).  
  
## Performance Considerations  
 Although online index operations permit concurrent user update activity, the index operations will take longer if the update activity is very heavy. Typically, online index operations will be slower than equivalent offline index operations regardless of the concurrent update activity level.  
  
 Because both the source and target structures are maintained during the online index operation, the resource usage for insert, update, and delete transactions is increased, potentially up to double. This could cause a decrease in performance and greater resource usage, especially CPU time, during the index operation. Online index operations are fully logged.  
  
 Although we recommend online operations, you should evaluate your environment and specific requirements. It may be optimal to run index operations offline. In doing this, users have restricted access to the data during the operation, but the operation finishes faster and uses fewer resources.  
  
 On multiprocessor computers that are running [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], index statements may use more processors to perform the scan and sort operations associated with the index statement just like other queries do. You can use the MAXDOP index option to control the number of processors dedicated to the online index operation. In this way, you can balance the resources that are used by index operation with those of the concurrent users. For more information, see [Configure Parallel Index Operations](configure-parallel-index-operations.md). For more information about the editions of SQL Server that support Parallel indexed operations, see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
 Because an S-lock or Sch-M lock is held in the final phase of the index operation, be careful when you run an online index operation inside an explicit user transaction, such as BEGIN TRANSACTION...COMMIT block. Doing this causes the lock to be held until the end of the transaction, therefore impeding user concurrency.  
  
 Online index rebuilding may increase fragmentation when it is allowed to run with `MAX DOP > 1` and `ALLOW_PAGE_LOCKS = OFF` options. For more information, see [How It Works: Online Index Rebuild - Can Cause Increased Fragmentation](https://blogs.msdn.com/b/psssql/archive/2012/09/05/how-it-works-online-index-rebuild-can-cause-increased-fragmentation.aspx).  
  
## Transaction Log Considerations  
 Large-scale index operations, performed offline or online, can generate large data loads that can cause the transaction log to quickly fill. To make sure that the index operation can be rolled back, the transaction log cannot be truncated until the index operation has been completed; however, the log can be backed up during the index operation. Therefore, the transaction log must have sufficient space to store both the index operation transactions and any concurrent user transactions for the duration of the index operation. For more information, see [Transaction Log Disk Space for Index Operations](transaction-log-disk-space-for-index-operations.md).  
  
## Related Content  
 [How Online Index Operations Work](how-online-index-operations-work.md)  
  
 [Perform Index Operations Online](perform-index-operations-online.md)  
  
 [ALTER INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-index-transact-sql)  
  
 [CREATE INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-index-transact-sql)  
  
  
