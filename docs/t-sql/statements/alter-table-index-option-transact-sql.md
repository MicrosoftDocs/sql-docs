---
title: "index_option (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/08/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "index_option"
ms.assetid: 8a14f12d-2fbf-4036-b8b2-8db3354e0eb7
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER TABLE index_option (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Specifies a set of options that can be applied to an index that is part of a constraint definition that is created by using [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
{   
    PAD_INDEX = { ON | OFF }  
  | FILLFACTOR = fillfactor  
  | IGNORE_DUP_KEY = { ON | OFF }  
  | STATISTICS_NORECOMPUTE = { ON | OFF }  
  | ALLOW_ROW_LOCKS = { ON | OFF }  
  | ALLOW_PAGE_LOCKS = { ON | OFF }  
  | SORT_IN_TEMPDB = { ON | OFF }   
  | ONLINE = { ON | OFF }  
  | MAXDOP = max_degree_of_parallelism  
  | DATA_COMPRESSION = { NONE |ROW | PAGE | COLUMNSTORE | COLUMNSTORE_ARCHIVE }  
      [ ON PARTITIONS ( { <partition_number_expression> | <range> }   
      [ , ...n ] ) ]  
  | ONLINE = { ON [ ( <low_priority_lock_wait> ) ] | OFF }  
}  
  
<range> ::=   
<partition_number_expression> TO <partition_number_expression>  
  
<single_partition_rebuild__option> ::=  
{  
    SORT_IN_TEMPDB = { ON | OFF }  
  | MAXDOP = max_degree_of_parallelism  
  | DATA_COMPRESSION = {NONE | ROW | PAGE | COLUMNSTORE | COLUMNSTORE_ARCHIVE } }  
  | ONLINE = { ON [ ( <low_priority_lock_wait> ) ] | OFF }  
}  
  
<low_priority_lock_wait>::=  
{  
    WAIT_AT_LOW_PRIORITY ( MAX_DURATION = <time> [ MINUTES ] ,   
                           ABORT_AFTER_WAIT = { NONE | SELF | BLOCKERS } )   
}  
```  
  
## Arguments  
 PAD_INDEX **=** { ON | **OFF** }  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies index padding. The default is OFF.  
  
 ON  
 The percentage of free space that is specified by FILLFACTOR is applied to the intermediate-level pages of the index.  
  
 OFF or *fillfactor* is not specified  
 The intermediate-level pages are filled to near capacity, leaving enough space for at least one row of the maximum size the index can have, given the set of keys on the intermediate pages.  
  
 FILLFACTOR **=**_fillfactor_  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies a percentage that indicates how full the [!INCLUDE[ssDE](../../includes/ssde-md.md)] should make the leaf level of each index page during index creation or alteration. The value specified must be an integer value from 1 to 100. The default is 0.  
  
> [!NOTE]  
>  Fill factor values 0 and 100 are identical in all respects.  
  
 IGNORE_DUP_KEY **=** { ON | **OFF** }  
 Specifies the response type when an insert operation attempts to insert duplicate key values into a unique index. The IGNORE_DUP_KEY option applies only to insert operations after the index is created or rebuilt. The option has no effect when executing [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md), [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md), or [UPDATE](../../t-sql/queries/update-transact-sql.md). The default is OFF.  
  
 ON  
 A warning message occurs when duplicate key values are inserted into a unique index. Only the rows violating the uniqueness constraint fail.  
  
 OFF  
 An error message occurs when duplicate key values are inserted into a unique index. The entire INSERT operation is rolled back.  
  
 IGNORE_DUP_KEY cannot be set to ON  for indexes created on a view, non-unique indexes, XML indexes, spatial indexes, and filtered indexes.  
  
 To view IGNORE_DUP_KEY, use [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md).  
  
 In backward compatible syntax, WITH IGNORE_DUP_KEY is equivalent to WITH IGNORE_DUP_KEY = ON.  
  
 STATISTICS_NORECOMPUTE **=** { ON | **OFF** }  
 Specifies whether statistics are recomputed. The default is OFF.  
  
 ON  
 Out-of-date statistics are not automatically recomputed.  
  
 OFF  
 Automatic statistics updating are enabled.  
  
 ALLOW_ROW_LOCKS **=** { **ON** | OFF }  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies whether row locks are allowed. The default is ON.  
  
 ON  
 Row locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when row locks are used.  
  
 OFF  
 Row locks are not used.  
  
 ALLOW_PAGE_LOCKS **=** { **ON** | OFF }  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies whether page locks are allowed. The default is ON.  
  
 ON  
 Page locks are allowed when accessing the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when page locks are used.  
  
 OFF  
 Page locks are not used.  
  
 SORT_IN_TEMPDB **=** { ON | **OFF** }  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies whether to store sort results in **tempdb**. The default is OFF.  
  
 ON  
 The intermediate sort results that are used to build the index are stored in **tempdb**. This may reduce the time required to create an index if **tempdb** is on a different set of disks than the user database. However, this increases the amount of disk space that is used during the index build.  
  
 OFF  
 The intermediate sort results are stored in the same database as the index.  
  
 ONLINE **=** { ON | **OFF** }  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies whether underlying tables and associated indexes are available for queries and data modification during the index operation. The default is OFF. REBUILD can be performed as an ONLINE operation.  
  
> [!NOTE]  
>  Unique nonclustered indexes cannot be created online. This includes indexes that are created due to a UNIQUE or PRIMARY KEY constraint.  
  
 ON  
 Long-term table locks are not held for the duration of the index operation. During the main phase of the index operation, only an Intent Share (IS) lock is held on the source table. This enables queries or updates to the underlying table and indexes to proceed. At the start of the operation, a Shared (S) lock is held on the source object for a very short period of time. At the end of the operation, for a short period of time, an S (Shared) lock is acquired on the source if a nonclustered index is being created; or an SCH-M (Schema Modification) lock is acquired when a clustered index is created or dropped online and when a clustered or nonclustered index is being rebuilt. Although the online index locks are short metadata locks, especially the Sch-M lock must wait for all blocking transactions to be completed on this table. During the wait time the Sch-M lock blocks all other transactions that wait behind this lock when accessing the same table. ONLINE cannot be set to ON when an index is being created on a local temporary table.  
  
> [!NOTE]  
>  Online index rebuild can set the *low_priority_lock_wait* options described later in this section. *low_priority_lock_wait* manages S and Sch-M lock priority during online index rebuild.  
  
 OFF  
 Table locks are applied for the duration of the index operation. This prevents all user access to the underlying table for the duration of the operation. An offline index operation that creates, rebuilds, or drops a clustered index, or rebuilds or drops a nonclustered index, acquires a Schema modification (Sch-M) lock on the table. This prevents all user access to the underlying table for the duration of the operation. An offline index operation that creates a nonclustered index acquires a Shared (S) lock on the table. This prevents updates to the underlying table but allows read operations, such as SELECT statements.  
  
 For more information, see [How Online Index Operations Work](../../relational-databases/indexes/how-online-index-operations-work.md).  
  
> [!NOTE]
>  Online index operations are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
 MAXDOP **=**_max_degree_of_parallelism_  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Overrides the **max degree of parallelism** configuration option for the duration of the index operation. For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md). Use MAXDOP to limit the number of processors used in a parallel plan execution. The maximum is 64 processors.  
  
 *max_degree_of_parallelism* can be:  
  
 - 1 - Suppresses parallel plan generation.  
 - \>1 - Restricts the maximum number of processors used in a parallel index operation to the specified number.  
 - 0 (default) - Uses the actual number of processors or fewer based on the current system workload.  
  
 For more information, see [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md).  
  
> [!NOTE]
>  Parallel index operations are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
 DATA_COMPRESSION  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies the data compression option for the specified table, partition number, or range of partitions. The options are as follows:  
  
 NONE  
 Table or specified partitions are not compressed. Applies only to rowstore tables; does not apply to columnstore tables.  
  
 ROW  
 Table or specified partitions are compressed by using row compression. Applies only to rowstore tables; does not apply to columnstore tables.  
  
 PAGE  
 Table or specified partitions are compressed by using page compression. Applies only to rowstore tables; does not apply to columnstore tables.  
  
 COLUMNSTORE  
 **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Applies only to columnstore tables. COLUMNSTORE specifies to decompress a partition that was compressed with the COLUMNSTORE_ARCHIVE option. When the data is restored, the COLUMNSTORE index continues to be compressed with the columnstore compression that is used for all columnstore tables.  
  
 COLUMNSTORE_ARCHIVE  
 **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Applies only to columnstore tables, which are tables stored with a clustered columnstore index. COLUMNSTORE_ARCHIVE further compresses the specified partition to a smaller size. This can be used for archival, or for other situations that require less storage and can afford more time for storage and retrieval  
  
 For more information about compression, see [Data Compression](../../relational-databases/data-compression/data-compression.md).  
  
ON PARTITIONS **(** { \<partition_number_expression> | \<range> } [ **,**...*n* ] **)**
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies the partitions to which the DATA_COMPRESSION setting applies. If the table is not partitioned, the ON PARTITIONS argument generates an error. If the ON PARTITIONS clause is not provided, the DATA_COMPRESSION option applies to all partitions of a partitioned table.  
  
\<partition_number_expression> can be specified in the following ways:  
  
-   Provide the number a partition, for example: ON PARTITIONS (2).  
-   Provide the partition numbers for several individual partitions separated by commas, for example: ON PARTITIONS (1, 5).  
-   Provide both ranges and individual partitions, for example: ON PARTITIONS (2, 4, 6 TO 8).  
  
\<range> can be specified as partition numbers separated by the word TO, for example: ON PARTITIONS (6 TO 8).  
  
 To set different types of data compression for different partitions, specify the DATA_COMPRESSION option more than once, for example:  
  
```sql  
--For rowstore tables  
REBUILD WITH   
(  
DATA_COMPRESSION = NONE ON PARTITIONS (1),   
DATA_COMPRESSION = ROW ON PARTITIONS (2, 4, 6 TO 8),   
DATA_COMPRESSION = PAGE ON PARTITIONS (3, 5)  
)  
  
--For columnstore tables  
REBUILD WITH   
(  
DATA_COMPRESSION = COLUMNSTORE ON PARTITIONS (1, 3, 5),   
DATA_COMPRESSION = COLUMNSTORE_ARCHIVE ON PARTITIONS (2, 4, 6 TO 8)  
)  
```  
  
**\<single_partition_rebuild__option>**  
 In most cases, rebuilding an index rebuilds all partitions of a partitioned index. The following options, when applied to a single partition, do not rebuild all partitions.  
  
-   SORT_IN_TEMPDB  
-   MAXDOP  
-   DATA_COMPRESSION  
  
**low_priority_lock_wait**  
 **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 A **SWITCH** or online index rebuild completes as soon as there are no blocking operations for this table. *WAIT_AT_LOW_PRIORITY* indicates that if the **SWITCH** or online index rebuild operation cannot be completed immediately, it waits. The operation holds low priority locks, allowing other operations that hold locks conflicting with the DDL statement to proceed. Omitting the **WAIT AT LOW PRIORITY** option is equivalent to `WAIT_AT_LOW_PRIORITY ( MAX_DURATION = 0 minutes, ABORT_AFTER_WAIT = NONE)`.  
  
MAX_DURATION = *time* [**MINUTES** ]  
 The wait time (an integer value specified in minutes) that the **SWITCH** or online index rebuild lock that must be acquired, waits when executing the DDL command. The SWITCH or online index rebuild operation attempts to complete immediately. If the operation is blocked for the **MAX_DURATION** time, one of the **ABORT_AFTER_WAIT** actions is executed. **MAX_DURATION** time is always in minutes, and the word **MINUTES** can be omitted.  
  
ABORT_AFTER_WAIT = [**NONE** | **SELF** | **BLOCKERS** } ]  
 NONE  
 Continues the **SWITCH** or online index rebuild operation without changing the lock priority (using regular priority).  
  
SELF  
 Exits the **SWITCH** or online index rebuild DDL operation currently being executed without taking any action.  
  
BLOCKERS  
 Kills all user transactions that block currently the **SWITCH** or online index rebuild DDL operation so that the operation can continue.  
 BLOCKERS requires the **ALTER ANY CONNECTION** permission.  
  
## Remarks  
 For a complete description of index options, see [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md).  
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [column_constraint &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-column-constraint-transact-sql.md)   
 [computed_column_definition &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-computed-column-definition-transact-sql.md)   
 [table_constraint &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-table-constraint-transact-sql.md)  
  
 
