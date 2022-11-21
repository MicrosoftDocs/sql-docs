---
description: "sys.sysindexes (Transact-SQL)"
title: "sys.sysindexes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sysindexes"
  - "sysindexes_TSQL"
  - "sys.sysindexes"
  - "sys.sysindexes_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysindexes system table"
  - "sys.sysindexes compatibility view"
ms.assetid: f483d89c-35c4-4a08-8f8b-737fd80d13f5
author: rwestMSFT
ms.author: randolphwest
---
# sys.sysindexes (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each index and table in the current database. XML indexes are not supported in this view. Partitioned tables and indexes are not fully supported in this view; use the [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md) catalog view instead.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|ID of the table to which the index belongs.|  
|**status**|**int**|System-status information.<br /><br /> [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**first**|**binary(6)**|Pointer to the first or root page.<br /><br /> Unused when **indid** = 0.<br /><br /> NULL = Index is partitioned when **indid** > 1.<br /><br /> NULL = Table is partitioned when **indid** is 0 or 1.|  
|**indid**|**smallint**|ID of the index:<br /><br /> 0 = Heap<br /><br /> 1 = Clustered index<br /><br /> >1 = Nonclustered index|  
|**root**|**binary(6)**|For **indid** >= 1, **root** is the pointer to the root page.<br /><br /> Unused when **indid** = 0.<br /><br /> NULL = Index is partitioned when **indid** > 1.<br /><br /> NULL = Table is partitioned when **indid** is 0 or 1.|  
|**minlen**|**smallint**|Minimum size of a row.|  
|**keycnt**|**smallint**|Number of keys.|  
|**groupid**|**smallint**|Filegroup ID on which the object was created.<br /><br /> NULL = Index is partitioned when **indid** > 1.<br /><br /> NULL = Table is partitioned when **indid** is 0 or 1.|  
|**dpages**|**int**|For **indid** = 0 or **indid** = 1, **dpages** is the count of data pages used.<br /><br /> For **indid** > 1, **dpages** is the count of index pages used.<br /><br /> 0 = Index is partitioned when **indid** > 1.<br /><br /> 0 = Table is partitioned when **indid** is 0 or 1.<br /><br /> Does not yield accurate results if row-overflow occurs.|  
|**reserved**|**int**|For **indid** = 0 or **indid** = 1, **reserved** is the count of pages allocated for all indexes and table data.<br /><br /> For **indid** > 1, **reserved** is the count of pages allocated for the index.<br /><br /> 0 = Index is partitioned when **indid** > 1.<br /><br /> 0 = Table is partitioned when **indid** is 0 or 1.<br /><br /> Does not yield accurate results if row-overflow occurs.|  
|**used**|**int**|For **indid** = 0 or **indid** = 1, **used** is the count of the total pages used for all index and table data.<br /><br /> For **indid** > 1, **used** is the count of pages used for the index.<br /><br /> 0 = Index is partitioned when **indid** > 1.<br /><br /> 0 = Table is partitioned when **indid** is 0 or 1.<br /><br /> Does not yield accurate results if row-overflow occurs.|  
|**rowcnt**|**bigint**|Data-level row count based on **indid** = 0 and **indid** = 1.<br /><br /> 0 = Index is partitioned when **indid** > 1.<br /><br /> 0 = Table is partitioned when **indid** is 0 or 1.|  
|**rowmodctr**|**int**|Counts the total number of inserted, deleted, or updated rows since the last time statistics were updated for the table.<br /><br /> 0 = Index is partitioned when **indid** > 1.<br /><br /> 0 = Table is partitioned when **indid** is 0 or 1.<br /><br /> In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later, **rowmodctr** is not fully compatible with earlier versions. For more information, see Remarks.|  
|**reserved3**|**int**|Returns 0.<br /><br /> [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**reserved4**|**int**|Returns 0.<br /><br /> [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**xmaxlen**|**smallint**|Maximum size of a row|  
|**maxirow**|**smallint**|Maximum size of a nonleaf index row.<br /><br /> In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later, **maxirow** is not fully compatible with earlier versions.|  
|**OrigFillFactor**|**tinyint**|Original fill factor value used when the index was created. This value is not maintained; however, it can be helpful if you have to re-create an index and do not remember the fill factor value that was used.|  
|**StatVersion**|**tinyint**|Returns 0.<br /><br /> [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**reserved2**|**int**|Returns 0.<br /><br /> [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**FirstIAM**|**binary(6)**|NULL = Index is partitioned.<br /><br /> [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**impid**|**smallint**|Index implementation flag.<br /><br /> Returns 0.<br /><br /> [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**lockflags**|**smallint**|Used to constrain the considered lock granularities for an index. For example, to minimize locking cost, a lookup table that is essentially read-only could be set up to do only table-level locking.|  
|**pgmodctr**|**int**|Returns 0.<br /><br /> [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**keys**|**varbinary(816)**|List of the column IDs of the columns that make up the index key.<br /><br /> Returns NULL.<br /><br /> To display the index key columns, use [sys.sysindexkeys](../../relational-databases/system-compatibility-views/sys-sysindexkeys-transact-sql.md).|  
|**name**|**sysname**|Name of the index or statistic. Returns NULL when **indid** = 0. Modify your application to look for a NULL heap name.|  
|**statblob**|**image**|Statistics binary large object (BLOB).<br /><br /> Returns NULL.|  
|**maxlen**|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**rows**|**int**|Data-level row count based on **indid** = 0 and **indid** = 1, and the value is repeated for **indid** >1.|  
  
## Remarks  
 Columns defined as reserved should not be used.  
  
 The columns **dpages**, **reserved**, and **used** will not return accurate results if the table or index contains data in the ROW_OVERFLOW allocation unit. In addition, the page counts for each index are tracked separately and are not aggregated for the base table. To view page counts, use the [sys.allocation_units](../../relational-databases/system-catalog-views/sys-allocation-units-transact-sql.md) or [sys.partitions](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md) catalog views, or the [sys.dm_db_partition_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-partition-stats-transact-sql.md) dynamic management view.  
  
 In SQL Server 2000 and earlier, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] maintained row-level modification counters. Such counters are now maintained at the column level. Therefore, the **rowmodctr** column is calculated and produces results that are similar to the results in earlier versions, but are not exact.  
  
 If you use the value in **rowmodctr** to determine when to update statistics, consider the following solutions:  
  
-   Do nothing. The new **rowmodctr** value will frequently help you determine when to update statistics because the behavior is reasonably close to the results of earlier versions.  
  
-   Use AUTO_UPDATE_STATISTICS. For more information see, [Statistics](../../relational-databases/statistics/statistics.md).  
  
-   Use a time limit to determine when to update statistics. For example, every hour, every day, or every week.  
  
-   Use application-level information to determine when to update statistics. For example, every time the maximum value of an **identity** column changes by more than 10,000, or every time a bulk insert operation is performed.  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)  
  
  
