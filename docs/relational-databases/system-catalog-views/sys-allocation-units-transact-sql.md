---
title: "sys.allocation_units (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.allocation_units_TSQL"
  - "sys.allocation_units"
  - "allocation_units_TSQL"
  - "allocation_units"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.allocation_units catalog view"
ms.assetid: ec9de780-68fd-4551-b70b-2d3ab3709b3e
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.allocation_units (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Contains a row for each allocation unit in the database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|allocation_unit_id|**bigint**|ID of the allocation unit. Is unique within a database.|  
|type|**tinyint**|Type of allocation unit:<br /><br /> 0 = Dropped<br /><br /> 1 = In-row data (all data types, except LOB data types)<br /><br /> 2 = Large object (LOB) data (**text**, **ntext**, **image**, **xml**, large value types, and CLR user-defined types)<br /><br /> 3 = Row-overflow data|  
|type_desc|**nvarchar(60)**|Description of the allocation unit type:<br /><br /> **DROPPED**<br /><br /> **IN_ROW_DATA**<br /><br /> **LOB_DATA**<br /><br /> **ROW_OVERFLOW_DATA**|  
|container_id|**bigint**|ID of the storage container associated with the allocation unit.<br /><br /> If type = 1 or 3, container_id = sys.partitions.hobt_id.<br /><br /> If type is 2, then container_id = sys.partitions.partition_id.<br /><br /> 0 = Allocation unit marked for deferred drop|  
|data_space_id|**int**|ID of the filegroup in which this allocation unit resides.|  
|total_pages|**bigint**|Total number of pages allocated or reserved by this allocation unit.|  
|used_pages|**bigint**|Number of total pages actually in use.|  
|data_pages|**bigint**|Number of used pages that have:<br /><br /> In-row data<br /><br /> LOB data<br /><br /> Row-overflow data<br /><br /> <br /><br /> Note that the value returned excludes internal index pages and allocation-management pages.|  
  
> [!NOTE]  
>  When you drop or rebuild large indexes, or drop or truncate large tables, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] defers the actual page deallocations, and their associated locks, until after the transaction commits. Deferred drop operations do not release allocated space immediately. Therefore, the values returned by sys.allocation_units immediately after dropping or truncating a large object may not reflect the actual disk space available.  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [sys.partitions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
