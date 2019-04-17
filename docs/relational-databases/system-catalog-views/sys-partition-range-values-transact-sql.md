---
title: "sys.partition_range_values (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.partition_range_values"
  - "partition_range_values_TSQL"
  - "partition_range_values"
  - "sys.partition_range_values_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.partition_range_values catalog view"
ms.assetid: 9aee483e-61f3-4613-bec6-f084161f45ac
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.partition_range_values (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Contains a row for each range boundary value of a partition function of type R.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**function_id**|**int**|ID of the partition function for this range boundary value.|  
|**boundary_id**|**int**|ID (1-based ordinal) of the boundary value tuple, with left-most boundary starting at an ID of 1.|  
|**parameter_id**|**int**|ID of the parameter of the function to which this value corresponds. The values in this column correspond with those in the **parameter_id** column of the **sys.partition_parameters** catalog view for any particular **function_id**.|  
|**value**|**sql_variant**|The actual boundary value.|  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Partition Function Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/partition-function-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [sys.partition_functions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-functions-transact-sql.md)   
 [sys.partition_parameters &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-parameters-transact-sql.md)  
  
  
