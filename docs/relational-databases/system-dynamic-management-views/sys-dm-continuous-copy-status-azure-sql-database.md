---
title: "sys.dm_continuous_copy_status"
titleSuffix: Azure SQL Database and Azure SQL Managed Instance
description: sys.dm_continuous_copy_status (Azure SQL Database and Azure SQL Managed Instance)
author: rwestMSFT
ms.author: randolphwest
ms.date: "4/18/2022"
ms.service: sql-database
ms.topic: "reference"
ms.custom: seo-dt-2019
f1_keywords:
  - "sys.dm_continuous_copy_status_TSQL"
  - "dm_continuous_copy_status_TSQL"
  - "dm_continuous_copy_status"
  - "sys.dm_continuous_copy_status"
helpviewer_keywords:
  - "dm_continuous_copy_status"
  - "sys.dm_continuous_copy_status"
dev_langs:
  - "TSQL"
ms.assetid: 411b2e71-4421-4ef5-900d-5af068750899
monikerRange: "=azuresqldb-current"
---
# sys.dm_continuous_copy_status (Azure SQL Database and Azure SQL Managed Instance)
[!INCLUDE[Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

This view has been superseded by [sys.dm_geo_replication_link_status](../../relational-databases/system-dynamic-management-views/sys-dm-geo-replication-link-status-azure-sql-database.md) and is preserved for backwards compatibility.

|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|**copy_guid**|**uniqueidentifier**|Unique ID of the replica database.|  
|**partner_server**|**sysname**|Name of the linked logical server or linked managed instance.|  
|**partner_database**|**sysname**|Name of the linked database on the linked logical server or linked managed instance.|  
|**last_replication**|**datetimeoffset**|The timestamp of the last applied replicated transaction.|  
|**replication_lag_sec**|**int**|Time difference in seconds between the current time and the timestamp of the last successfully committed transaction on the primary database that has not been acknowledged by the active secondary database.|  
|**replication_state**|**tinyint**|The state of continuous-copy replication for this database. The following are the possible values and their descriptions.<br /><br /> 1: Seeding. The replication target is being seeded and is in a transactionally inconsistent state. Until seeding completes, you cannot connect to the active secondary database. <br />2: Catching up. The active secondary database is currently catching up to the primary database and is in a transactionally consistent state.<br />3: Re-seeding. The active secondary database is being automatically re-seeded because of an unrecoverable replication failure.<br />4: Suspended. This is not an active continuous-copy relationship. This state usually indicates that the bandwidth available for the interlink is insufficient for the level of transaction activity on the primary database. However, the continuous-copy relationship is still intact.|  
|**replication_state_desc**|**nvarchar(256)**|Description of replication_state, one of:<br /><br /> SEEDING<br /><br /> CATCH_UP<br /><br /> RE_SEEDING<br /><br /> SUSPENDED|  
|**is_rpo_limit_reached**|**bit**|This is always set to 0|  
|**is_target_role**|**bit**|0 = Source of copy relationship<br /><br /> 1 = Target of copy relationship|  
|**is_interlink_connected**|**bit**|1 = Interlink is connected.<br /><br /> 0 = Interlink is disconnected.|  
  
## Permissions  

To retrieve data, requires membership in the **db_owner** database role. The dbo user, members of the **dbmanager** database role, and the sa login can all query this view as well.  
  
## Remarks  

The **sys.dm_continuous_copy_status** view is created in the **resource** database and is visible in all databases, including the logical master. However, querying this view in the logical master returns an empty set.  
  
If a geo-replication link, also known as a continuous-copy relationship, is terminated on a database, the row for that database in the **sys.dm_continuous_copy_status** view disappears.  
  
Like the **sys.dm_database_copies** view, **sys.dm_continuous_copy_status** reflects the state of the continuous copy relationship in which the database is either a primary or active secondary database. Unlike **sys.dm_database_copies**, **sys.dm_continuous_copy_status** contains several columns that provide details about operations and performance. These columns include **last_replication**, and **replication_lag_sec**..  
  
## Next steps

Learn more about related concepts in the following articles:

- [sys.dm_geo_replication_link_status](../../relational-databases/system-dynamic-management-views/sys-dm-geo-replication-link-status-azure-sql-database.md)
- [Geo-Replication Dynamic Management Views and Functions (Azure SQL Database)](geo-replication-dynamic-management-views-and-functions-azure-sql-database.md)
- [sys.dm_database_copies &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-database-copies-azure-sql-database.md)
- [Active Geo-Replication Stored Procedures &#40;Transact-SQL&#41;](../system-stored-procedures/system-stored-procedures-transact-sql.md)
