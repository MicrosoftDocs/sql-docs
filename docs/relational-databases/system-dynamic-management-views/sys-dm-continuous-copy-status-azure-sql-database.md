---
title: "sys.dm_continuous_copy_status (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql-database
ms.reviewer: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_continuous_copy_status_TSQL"
  - "dm_continuous_copy_status_TSQL"
  - "dm_continuous_copy_status"
  - "sys.dm_continuous_copy_status"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dm_continuous_copy_status"
  - "sys.dm_continuous_copy_status"
ms.assetid: 411b2e71-4421-4ef5-900d-5af068750899
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sys.dm_continuous_copy_status (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  Returns a row for each user database (V11) that is currently engaged in a Geo-replication continuous-copy relationship. If more than one continuous copy relationship is initiated for a given primary database this table contains one row for each active secondary database.  
  
If you are using SQL Database V12 you should use [sys.dm_geo_replication_link_status](../../relational-databases/system-dynamic-management-views/sys-dm-geo-replication-link-status-azure-sql-database.md) (because *sys.dm_continuous_copy_status* only applies to V11).

  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|**copy_guid**|**uniqueidentifier**|Unique ID of the replica database.|  
|**partner_server**|**sysname**|Name of the linked SQL Database server.|  
|**partner_database**|**sysname**|Name of the linked database on the linked SQL Database server.|  
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
  
 If the continuous-copy relationship is terminated on a database, the row for that database in the **sys.dm_continuous_copy_status** view disappears.  
  
 Like the **sys.dm_database_copies** view, **sys.dm_continuous_copy_status** reflects the state of the continuous copy relationship in which the database is either a primary or active secondary database. Unlike **sys.dm_database_copies**, **sys.dm_continuous_copy_status** contains several columns that provide details about operations and performance. These columns include **last_replication**, and **replication_lag_sec**..  
  
## See Also  
 [sys.dm_database_copies &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-database-copies-azure-sql-database.md)   
 [Active Geo-Replication Stored Procedures &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/81658ee4-4422-4d73-bf7a-86a07422cb0d)  
  
  
