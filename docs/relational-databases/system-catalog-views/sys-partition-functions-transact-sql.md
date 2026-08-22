---
title: "sys.partition_functions (Transact-SQL)"
description: sys.partition_functions (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.partition_functions_TSQL"
  - "partition_functions"
  - "sys.partition_functions"
  - "partition_functions_TSQL"
helpviewer_keywords:
  - "sys.partition_functions catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# sys.partition_functions (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

  Contains a row for each partition function in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the partition function. Is unique within the database.|  
|**function_id**|**int**|Partition function ID. Is unique within the database.|  
|**type**|**char(2)**|Function type.<br /><br /> R = Range|  
|**type_desc**|**nvarchar(60)**|Function type.<br /><br /> RANGE|  
|**fanout**|**int**|Number of partitions created by the function.|  
|**boundary_value_on_right**|**bit**|For range partitioning.<br /><br /> 1 = Boundary value is included in the RIGHT range of the boundary.<br /><br /> 0 = LEFT.|  
|**is_system**||**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> 1 = Object is used for full-text index fragments.<br /><br /> 0 = Object is not used for full-text index fragments.|  
|**create_date**|**datetime**|Date the function was created.|  
|**modify_date**|**datetime**|Date the function was last modified using an ALTER statement.|  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Related content

- [Partition Function Catalog Views (Transact-SQL)](partition-function-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [sys.partition_range_values (Transact-SQL)](sys-partition-range-values-transact-sql.md)
- [sys.partition_parameters (Transact-SQL)](sys-partition-parameters-transact-sql.md)
