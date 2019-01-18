---
title: "MSdbms_map (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSdbms_map"
  - "MSdbms_map_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSdbms_map system table"
ms.assetid: df67e691-3a50-450a-99c5-8c4a041749ae
author: stevestein
ms.author: sstein
manager: craigg
---
# MSdbms_map (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSdbms_map** table contains source data type information as well as a link to default destination data type information for source and destination DBMS pairs. This table is stored in the **msdb** database and is used for heterogeneous publishing.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**map_id**|**int**|Uniquely identifies a data type mapping.|  
|**src_dbms_id**|**int**|Identifies the source DBMS by specifying its **dbms_id** in the [MSdbms](../../relational-databases/system-tables/msdbms-transact-sql.md) table.|  
|**dest_dbms_id**|**int**|Identifies the destination DBMS by specifying its **dbms_id** in the [MSdbms](../../relational-databases/system-tables/msdbms-transact-sql.md) table.|  
|**src_datatype_id**|**int**|Identifies the **datatype_id** from the [MSdbms_datatype](../../relational-databases/system-tables/msdbms-datatype-transact-sql.md) table for the source data type.|  
|**src_len_min**|**bigint**|The minimum length of the data type at the source DBMS, where a value of NULL indicates that length is not used.|  
|**src_len_max**|**bigint**|The maximum length of the data type at the source DBMS, where a value of NULL indicates that length is not used.|  
|**src_prec_min**|**bigint**|The minimum precision of the data type at the source DBMS, where a value of NULL indicates that precision is not used.|  
|**src_prec_max**|**bigint**|The maximum precision of the data type at the source DBMS, where a value of NULL indicates that precision is not used.|  
|**src_scale_min**|**int**|The minimum scale of the data type at the source DBMS, where a value of NULL indicates that scale is not used.|  
|**src_scale_max**|**int**|The maximum scale of the data type at the source DBMS, where a value of NULL indicates that scale is not used.|  
|**src_nullable**|**bit**|Indicates whether the destination column in the mapping allows NULL values, where a value of NULL means that this definition is not required.|  
|**default_datatype_mapping_id**|**int**|Identifies the default data type mapping by specifying its **map_id** in table [MSdbms_datatype_mapping](../../relational-databases/system-tables/msdbms-datatype-mapping-transact-sql.md).|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Specify Data Type Mappings for an Oracle Publisher](../../relational-databases/replication/publish/specify-data-type-mappings-for-an-oracle-publisher.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
