---
title: "sys.partition_functions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.partition_functions_TSQL"
  - "partition_functions"
  - "sys.partition_functions"
  - "partition_functions_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.partition_functions catalog view"
ms.assetid: 96515727-728b-4bea-804a-36ce915b8b75
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.partition_functions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Contains a row for each partition function in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the partition function. Is unique within the database.|  
|**function_id**|**int**|Partition function ID. Is unique within the database.|  
|**type**|**char(2)**|Function type.<br /><br /> R = Range|  
|**type_desc**|**nvarchar(60)**|Function type.<br /><br /> RANGE|  
|**fanout**|**int**|Number of partitions created by the function.|  
|**boundary_value_on_right**|**bit**|For range partitioning.<br /><br /> 1 = Boundary value is included in the RIGHT range of the boundary.<br /><br /> 0 = LEFT.|  
|**is_system**||**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> 1 = Object is used for full-text index fragments.<br /><br /> 0 = Object is not used for full-text index fragments.|  
|**create_date**|**datetime**|Date the function was created.|  
|**modify_date**|**datetime**|Date the function was last modified using an ALTER statement.|  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Partition Function Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/partition-function-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [sys.partition_range_values &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-range-values-transact-sql.md)   
 [sys.partition_parameters &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partition-parameters-transact-sql.md)  
  
  
