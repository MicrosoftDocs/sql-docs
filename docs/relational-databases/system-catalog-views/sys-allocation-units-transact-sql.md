---
title: "sys.allocation_units (Transact-SQL)"
description: sys.allocation_units (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "09/01/2021"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.allocation_units_TSQL"
  - "sys.allocation_units"
  - "allocation_units_TSQL"
  - "allocation_units"
helpviewer_keywords:
  - "sys.allocation_units catalog view"
dev_langs:
  - "TSQL"
ms.assetid: ec9de780-68fd-4551-b70b-2d3ab3709b3e
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.allocation_units (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Contains a row for each allocation unit in the database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|allocation_unit_id|**bigint**|ID of the allocation unit. Is unique within a database.|  
|type|**tinyint**|Type of allocation unit:<br /><br /> 0 = Dropped<br /><br /> 1 = In-row data (all data types, except LOB data types)<br /><br /> 2 = Large object (LOB) data (**text**, **ntext**, **image**, **xml**, large value types, and CLR user-defined types)<br /><br /> 3 = Row-overflow data|  
|type_desc|**nvarchar(60)**|Description of the allocation unit type:<br /><br /> **DROPPED**<br /><br /> **IN_ROW_DATA**<br /><br /> **LOB_DATA**<br /><br /> **ROW_OVERFLOW_DATA**|  
|container_id|**bigint**|ID of the storage container associated with the allocation unit.<br /><br /> If type = 1 or 3 in a rowstore index container_id = sys.partitions.hobt_id.<br /><br /> If type = 1 or 3 in a columnstore index, container_id = sys.column_store_row_groups.delta_store_hobt_id.<br /><br /> If type is 2, then container_id = sys.partitions.partition_id.<br /><br /> 0 = Allocation unit marked for deferred drop|  
|data_space_id|**int**|ID of the filegroup in which this allocation unit resides.|  
|total_pages|**bigint**|Total number of pages allocated or reserved by this allocation unit.|  
|used_pages|**bigint**|Number of total pages actually in use.|  
|data_pages|**bigint**|Number of used pages that have:<br /><br /> In-row data<br /><br /> LOB data<br /><br /> Row-overflow data<br /><br /> <br /><br /> Note that the value returned excludes internal index pages and allocation-management pages.|  
  
> [!NOTE]  
>  When you drop or rebuild large indexes, drop large tables, or truncate large tables or partitions, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] defers the actual page deallocations, and their associated locks, until after the transaction commits. Deferred drop operations do not release allocated space immediately. Therefore, the values returned by sys.allocation_units immediately after dropping or truncating a large object may not reflect the actual disk space available.
>
>  When [Accelerated Database Recovery](../../relational-databases/accelerated-database-recovery-concepts.md) is enabled, deferred drop is used regardless of object size.
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  

## Examples
  
### Determine space used by object and type of an allocation unit

The following query returns all the user tables in a database and the amount of space used in each, by allocation unit type.

  
```sql
SELECT t.object_id AS ObjectID,
       OBJECT_NAME(t.object_id) AS ObjectName,
       SUM(u.total_pages) * 8 AS Total_Reserved_kb,
       SUM(u.used_pages) * 8 AS Used_Space_kb,
       u.type_desc AS TypeDesc,
       MAX(p.rows) AS RowsCount
FROM sys.allocation_units AS u
JOIN sys.partitions AS p ON u.container_id = p.hobt_id
JOIN sys.tables AS t ON p.object_id = t.object_id
GROUP BY t.object_id,
         OBJECT_NAME(t.object_id),
         u.type_desc
ORDER BY Used_Space_kb DESC,
         ObjectName;

```  

## See Also  
 [sys.partitions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
