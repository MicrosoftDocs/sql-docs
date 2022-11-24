---
title: "sys.dm_db_xtp_index_stats (Transact-SQL)"
description: For In-Memory OLTP tables, sys.dm_db_xtp_index_stats contains statistics collected since the last database restart.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_xtp_index_stats"
  - "dm_db_xtp_index_stats"
  - "sys.dm_db_xtp_index_stats_TSQL"
  - "dm_db_xtp_index_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_db_xtp_index_stats dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_xtp_index_stats (Transact-SQL)
[!INCLUDE[sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Contains statistics collected since the last database restart.  
  
 For more information, see [[!INCLUDE[inmemory](../../includes/inmemory-md.md)] &#40;In-Memory Optimization&#41;](../in-memory-oltp/overview-and-usage-scenarios.md) and [Guidelines for Using Indexes on Memory-Optimized Tables](/previous-versions/sql/sql-server-2016/dn133166(v=sql.130)).  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**bigint**|ID of the object to which this index belongs.|  
|xtp_object_id|**bigint**|Internal ID corresponding to the current version of the object.<br /><br /> Note: Applies to [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)].|  
|index_id|**bigint**|ID of the index. The index_id is unique only within the object.|  
|scans_started|**bigint**|Number of [!INCLUDE[inmemory](../../includes/inmemory-md.md)] index scans performed. Every select, insert, update, or delete requires an index scan.|  
|scans_retries|**bigint**|Number of index scans that needed to be retried,|  
|rows_returned|**bigint**|Cumulative number of rows returned since the table was created or the start of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|rows_touched|**bigint**|Cumulative number of rows accessed since the table was created or the start of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|rows_expiring|**bigint**|Internal use only.|  
|rows_expired|**bigint**|Internal use only.|  
|rows_expired_removed|**bigint**|Internal use only.|  
|phantom_scans_started|**bigint**|Internal use only.|  
|phatom_scans_retries|**bigint**|Internal use only.|  
|phantom_rows_touched|**bigint**|Internal use only.|  
|phantom_expiring_rows_encountered|**bigint**|Internal use only.|  
|phantom_expired_rows_encountered|**bigint**|Internal use only.|  
|phantom_expired_removed_rows_encountered|**bigint**|Internal use only.|  
|phantom_expired_rows_removed|**bigint**|Internal use only.|  
|object_address|**varbinary(8)**|Internal use only.|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission on the current database.  
  
## See also

- [Introduction to Memory-Optimized Tables](../in-memory-oltp/introduction-to-memory-optimized-tables.md)
- [Memory-Optimized Table Dynamic Management Views](../../relational-databases/system-dynamic-management-views/memory-optimized-table-dynamic-management-views-transact-sql.md)

## Next steps 

- [[!INCLUDE[inmemory](../../includes/inmemory-md.md)] Overview and Usage Scenarios](../in-memory-oltp/overview-and-usage-scenarios.md)
- [Optimize performance by using in-memory technologies in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/in-memory-oltp-overview)
