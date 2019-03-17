---
title: "sys.partition_parameters (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "partition_parameters_TSQL"
  - "partition_parameters"
  - "sys.partition_parameters_TSQL"
  - "sys.partition_parameters"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.partition_parameters catalog view"
ms.assetid: 2012ed9d-3ea3-4c29-9b78-dfa54a392dce
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.partition_parameters (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Contains a row for each parameter of a partition function.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**function_id**|**int**|ID of the partition function to which this parameter belongs.|  
|**parameter_id**|**int**|ID of the parameter. Is unique within the partition function, beginning with 1.|  
|**system_type_id**|**tinyint**|ID of the system type of the parameter. Corresponds to the **system_type_id** column of the **sys.types** catalog view.|  
|**max_length**|**smallint**|Maximum length of the parameter in bytes.|  
|**precision**|**tinyint**|Precision of the parameter if numeric-based; otherwise, 0.|  
|**scale**|**tinyint**|Scale of the parameter if numeric-based; otherwise, 0.|  
|**collation_name**|**sysname**|Name of the collation of the parameter if character-based; otherwise, NULL.|  
|**user_type_id**|**int**|ID of the type. Is unique within the database. For system data types, **user_type_id** = **system_type_id**.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Partition Function Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/partition-function-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [sys.partition_functions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-functions-transact-sql.md)   
 [sys.partition_range_values &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-range-values-transact-sql.md)  
  
  
