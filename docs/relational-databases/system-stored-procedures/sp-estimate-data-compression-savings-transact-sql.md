---
title: "sp_estimate_data_compression_savings (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
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
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_estimate_data_compression_savings (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the current size of the requested object and estimates the object size for the requested compression state. Compression can be evaluated for whole tables or parts of tables. This includes heaps, clustered indexes, nonclustered indexes, columnstore indexes, indexed views, and table and index partitions. The objects can be compressed by using row, page, columnstore or columnstore archive compression. If the table, index, or partition is already compressed, you can use this procedure to estimate the size of the table, index, or partition if it is recompressed.  
  
> [!NOTE]
>  Compression and **sp_estimate_data_compression_savings** are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
 To estimate the size of the object if it were to use the requested compression setting, this stored procedure samples the source object and loads this data into an equivalent table and index created in tempdb. The table or index create in tempdb is then compressed to the requested setting and the estimated compression savings is computed.  
  
 To change the compression state of a table, index, or partition, use the [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md) statements. For general information about compression, see [Data Compression](../../relational-databases/data-compression/data-compression.md).  
  
> [!NOTE]  
>  If the existing data is fragmented, you might be able to reduce its size without using compression by rebuilding the index. For indexes, the fill factor will be applied during an index rebuild. This could increase the size of the index.  

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
 Is the type of compression to be evaluated. *data_compression* can be one of the following values: NONE, ROW, PAGE, COLUMNSTORE, or COLUMNSTORE_ARCHIVE.  
  
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
 Use sp_estimate_data_compression_savings to estimate the savings that can occur when you enable a table or partition for row, page, columnstore or columnstore archive compression. For instance if the average size of the row can be reduced by 40 percent, you can potentially reduce the size of the object by 40 percent. You might not receive a space savings because this depends on the fill factor and the size of the row. For example, if you have a row that is 8000 bytes long and you reduce its size by 40 percent, you can still fit only one row on a data page. There is no savings.  
  
 If the results of running sp_estimate_data_compression_savings indicate that the table will grow, this means that many rows in the table use almost the whole precision of the data types, and the addition of the small overhead needed for the compressed format is more than the savings from compression. In this rare case, do not enable compression.  
  
 If a table is enabled for compression, use sp_estimate_data_compression_savings to estimate the average size of the row if the table is uncompressed.  
  
 An (IS) lock is acquired on the table during this operation. If an (IS) lock cannot be obtained, the procedure will be blocked. The table is scanned under the read committed isolation level.  
  
 If the requested compression setting is same as the current compression setting, the stored procedure will return the estimated size with no data fragmentation and using the existing fill factor.  
  
 If the index or partition ID does not exist, no results are returned.  
  
## Permissions  
 Requires SELECT permission on the table.  
  
## Limitations and Restrictions  
 Prior to SQL Server 2019, this procedure did not apply to columnstore indexes, and therefore did not accept the data compression parameters COLUMNSTORE and COLUMNSTORE_ARCHIVE.  Starting with SQL Server 2019, columnstore indexes can be used both as a source object for estimation, and as a requested compression type.

## Considerations for Columnstore Indexes
 Starting with SQL Server 2019, sp_estimate_compression_savings supports estimating both columnstore and columnstore archive compression. Unlike page and row compression, applying columnstore compression to an object requires creating a new columnstore index. For this reason, when using the COLUMNSTORE and COLUMNSTORE_ARCHIVE options of this procedure, the type of the source object provided to the procedure determines the type of columnstore index used for the compressed size estimate. The following table illustrates the reference objects used to estimate compression savings for each source object type when the @data_compression parameter is set to either COLUMNSTORE or COLUMNSTORE_ARCHIVE.

 |Source Object|Reference Object|
 |-----------------|---------------|
 |Heap|Clustered columnstore index|
 |Clustered index|Clustered columnstore index|
 |Non-clustered index|Non-clustered columnstore index (including the key columns and any included columns of the provided non-clustered index, as well as the partition column of the table, if any)|
 |Non-clustered columnstore index|Non-clustered columnstore index (including the same columns as the provided non-clustered columnstore index)|
 |Clustered columnstore index|Clustered columnstore index|

> [!NOTE]  
> When estimating columnstore compression from a rowstore source object (clustered index, non-clustered index or heap), if there are any columns in the source object that have a data type that is not supported in a columnstore index, sp_estimate_compression_savings will fail with an error.

 Similarly, when the @data_compression parameter is set to NONE, ROW, or PAGE and the source object is a columnstore index, the following table outlines the reference objects used.

 |Source Object|Reference Object|
 |-----------------|---------------|
 |Clustered columnstore index|Heap|
 |Non-clustered columnstore index|Non-clustered index (including the columns contained in the non-clustered columnstore index as key columns, and the partition column of the table, if any, as an included column)|

> [!NOTE]  
> When estimating rowstore compression (NONE, ROW or PAGE) from a columnstore source object, be sure that the source index does not contain more than 32 columns as this is the limit supported in a rowstore (non-clustered) index.
  
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
  
  
