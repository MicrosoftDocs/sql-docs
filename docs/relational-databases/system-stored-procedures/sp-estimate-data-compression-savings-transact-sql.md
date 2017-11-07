---
title: "sp_estimate_data_compression_savings (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_estimate_data_compression_savings_TSQL"
  - "sp_estimate_data_compression_savings"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "compression [SQL Server], estimating"
  - "sp_estimate_data_compression_savings"
ms.assetid: 6f6c7150-e788-45e0-9d08-d6c2f4a33729
caps.latest.revision: 27
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sp_estimate_data_compression_savings (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the current size of the requested object and estimates the object size for the requested compression state. Compression can be evaluated for whole tables or parts of tables. This includes heaps, clustered indexes, nonclustered indexes, indexed views, and table and index partitions. The objects can be compressed by using row compression or page compression. If the table, index, or partition is already compressed, you can use this procedure to estimate the size of the table, index, or partition if it is recompressed.  
  
> [!NOTE]  
>  Compression and **sp_estimate_data_compression_savings** are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
 To estimate the size of the object if it were to use the requested compression setting, this stored procedure samples the source object and loads this data into an equivalent table and index created in tempdb. The table or index create in tempdb is then compressed to the requested setting and the estimated compression savings is computed.  
  
 To change the compression state of a table, index, or partition, use the [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md) statements. For general information about compression, see [Data Compression](../../relational-databases/data-compression/data-compression.md).  
  
> [!NOTE]  
>  If the existing data is fragmented, you might be able to reduce its size without using compression by rebuilding the index. For indexes, the fill factor will be applied during an index rebuild. This could increase the size of the index.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).|  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_estimate_data_compression_savings   
     [ @schema_name = ] 'schema_name'    
   , [ @object_name = ] 'object_name'   
   , [@index_id = ] index_id   
   , [@partition_number = ] partition_number   
   , [@data_compression = ] 'data_compression'   
[;]  
```  
  
## Arguments  
 [ @schema_name= ] '*schema_name*'  
 Is the name of the database schema that contains the table or indexed view. *schema_name* is **sysname**. If *schema_name* is NULL, the default schema of the current user is used.  
  
 [ @object_name= ] '*object_name*'  
 Is the name of the table or indexed view that the index is on. *object_name* is **sysname**.  
  
 [ @index_id= ] '*index_id*'  
 Is the ID of the index. *index_id* is **int**, and can be one of the following values: the ID number of an index, NULL, or 0 if *object_id* is a heap. To return information for all indexes for a base table or view, specify NULL. If you specify NULL, you must also specify NULL for *partition_number*.  
  
 [ @partition_number= ] '*partition_number*'  
 Is the partition number in the object. *partition_number* is **int**, and can be one of the following values: the partition number of an index or heap, NULL or 1 for a nonpartitioned index or heap.  
  
 To specify the partition, you can also specify the [$partition](../../t-sql/functions/partition-transact-sql.md) function. To return information for all partitions of the owning object, specify NULL.  
  
 [ @data_compression= ] '*data_compression*'  
 Is the type of compression to be evaluated. *data_compression* can be one of the following values: NONE, ROW, or PAGE.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 The following result set is returned to provide current and estimated size for the table, index, or partition.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_name|**sysname**|Name of the table or the indexed view.|  
|schema_name|**sysname**|Schema of the table or indexed view.|  
|index_id|**int**|Index ID of an index:<br /><br /> 0 = Heap<br /><br /> 1 = Clustered index<br /><br /> > 1 = Nonclustered index|  
|partition_number|**int**|Partition number. Returns 1 for a nonpartitioned table or index.|  
|size_with_current_compression_setting (KB)|**bigint**|Size of the requested table, index, or partition as it currently exists.|  
|size_with_requested_compression_setting (KB)|**bigint**|Estimated size of the table, index, or partition that uses the requested compression setting; and, if applicable, the existing fill factor, and assuming there is no fragmentation.|  
|sample_size_with_current_compression_setting (KB)|**bigint**|Size of the sample with the current compression setting. This includes any fragmentation.|  
|sample_size_with_requested_compression_setting (KB)|**bigint**|Size of the sample that is created by using the requested compression setting; and, if applicable, the existing fill factor and no fragmentation.|  
  
## Remarks  
 Use sp_estimate_data_compression_savings to estimate the savings that can occur when you enable a table or partition for row or page compression. For instance if the average size of the row can be reduced by 40 percent, you can potentially reduce the size of the object by 40 percent. You might not receive a space savings because this depends on the fill factor and the size of the row. For example, if you have a row that is 8000 bytes long and you reduce its size by 40 percent, you can still fit only one row on a data page. There is no savings.  
  
 If the results of running sp_estimate_data_compression_savings indicate that the table will grow, this means that many rows in the table use almost the whole precision of the data types, and the addition of the small overhead needed for the compressed format is more than the savings from compression. In this rare case, do not enable compression.  
  
 If a table is enabled for compression, use sp_estimate_data_compression_savings to estimate the average size of the row if the table is uncompressed.  
  
 An (IS) lock is acquired on the table during this operation. If an (IS) lock cannot be obtained, the procedure will be blocked. The table is scanned under the read committed isolation level.  
  
 If the requested compression setting is same as the current compression setting, the stored procedure will return the estimated size with no data fragmentation and using the existing fill factor.  
  
 If the index or partition ID does not exist, no results are returned.  
  
## Permissions  
 Requires SELECT permission on the table.  
  
## Limitations and Restrictions  
 This procedure does not apply to columnstore tables, and therefore does not accept the data compression parameters COLUMNSTORE and COLUMNSTORE_ARCHIVE.  
  
## Examples  
 The following example estimates the size of the `Production.WorkOrderRouting` table if it is compressed by using `ROW` compression.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_estimate_data_compression_savings 'Production', 'WorkOrderRouting', NULL, NULL, 'ROW' ;  
GO  
```  
  
## See Also  
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)   
 [sys.partitions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md)   
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [Unicode Compression Implementation](../../relational-databases/data-compression/unicode-compression-implementation.md)  
  
  
