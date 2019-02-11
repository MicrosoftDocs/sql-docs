---
title: "sys.dm_repl_schemas (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_repl_schemas_TSQL"
  - "dm_repl_schemas"
  - "sys.dm_repl_schemas_TSQL"
  - "sys.dm_repl_schemas"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_repl_schemas dynamic management function"
ms.assetid: 6f5fefff-8492-4360-bd5b-a97287367914
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_repl_schemas (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about table columns published by replication.  
  
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**artcache_schema_address**|**varbinary(8)**|In-memory address of the cached schema structure for the published table article.|  
|**tabid**|**bigint**|ID of the replicated table.|  
|**indexid**|**smallint**|ID of a clustered index on the published table.|  
|**idSch**|**bigint**|ID of the table schema.|  
|**tabschema**|**nvarchar(510)**|Name of the table schema.|  
|**ccTabschema**|**smallint**|Character length of the table schema.|  
|**tabname**|**nvarchar(510)**|Name of the published table.|  
|**ccTabname**|**smallint**|Character length of the published table name.|  
|**rowsetid_delete**|**bigint**|ID of the deleted row.|  
|**rowsetid_insert**|**bigint**|ID of the inserted row.|  
|**num_pk_cols**|**int**|Number of primary key columns.|  
|**pcitee**|**binary(8000)**|Pointer to the query expression structure used to evaluate computed column.|  
|**re_numtextcols**|**int**|Number of binary large object columns in the replicated table.|  
|**re_schema_lsn_begin**|**binary(8000)**|Beginning log sequence number (LSN) of schema version logging.|  
|**re_schema_lsn_end**|**binary(8000)**|Ending LSN of schema version logging.|  
|**re_numcols**|**int**|Number of columns published.|  
|**re_colid**|**int**|Column identifier at the Publisher.|  
|**re_awcName**|**nvarchar(510)**|Name of the published column.|  
|**re_ccName**|**smallint**|Number of characters in the column name.|  
|**re_pk**|**tinyint**|Whether the published column is part of a primary key.|  
|**re_unique**|**tinyint**|Whether the published column is part of a unique index.|  
|**re_maxlen**|**smallint**|Maximum length of the published column.|  
|**re_prec**|**tinyint**|Precision of the published column.|  
|**re_scale**|**tinyint**|Scale of the published column.|  
|**re_collatid**|**bigint**|Collation ID for published column.|  
|**re_xvtype**|**smallint**|Type of the published column.|  
|**re_offset**|**smallint**|Offset of the published column.|  
|**re_bitpos**|**tinyint**|Bit position of the published column, in the byte vector.|  
|**re_fNullable**|**tinyint**|Specifies whether the published column supports NULL values.|  
|**re_fAnsiTrim**|**tinyint**|Specifies whether ANSI trim is used on the published column.|  
|**re_computed**|**smallint**|Specifies whether the published column is a computed column.|  
|**se_rowsetid**|**bigint**|ID of the rowset.|  
|**se_schema_lsn_begin**|**binary(8000)**|Beginning LSN of schema version logging.|  
|**se_schema_lsn_end**|**binary(8000)**|Ending LSN of schema version logging.|  
|**se_numcols**|**int**|Number of columns.|  
|**se_colid**|**int**|ID of the column at the Subscriber.|  
|**se_maxlen**|**smallint**|Maximum length of the column.|  
|**se_prec**|**tinyint**|Precision of the column.|  
|**se_scale**|**tinyint**|Scale of the column.|  
|**se_collatid**|**bigint**|Collation ID for column.|  
|**se_xvtype**|**smallint**|Type of the column.|  
|**se_offset**|**smallint**|Offset of the column.|  
|**se_bitpos**|**tinyint**|Bit position of the column, in the byte vector.|  
|**se_fNullable**|**tinyint**|Specifies whether the column supports NULL values.|  
|**se_fAnsiTrim**|**tinyint**|Specifies whether ANSI trim is used on the column.|  
|**se_computed**|**smallint**|Specifies whether the columnis a computed column.|  
|**se_nullBitInLeafRows**|**int**|Specifies whether the column value is NULL.|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission on the publication database to call **dm_repl_schemas**.  
  
## Remarks  
 Information is only returned for replicated database objects that are currently loaded in the replication article cache.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Replication Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/replication-related-dynamic-management-views-transact-sql.md)  
  
  

