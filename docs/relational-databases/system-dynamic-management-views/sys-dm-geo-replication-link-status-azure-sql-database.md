---
title: "sys.dm_geo_replication_link_status"
titleSuffix: Azure SQL Database and Azure SQL Managed Instance
description: sys.dm_geo_replication_link_status (Azure SQL Database and Azure SQL Managed Instance)
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/10/2022
ms.service: sql-database
ms.topic: conceptual
ms.custom: seo-dt-2019
f1_keywords:
  - "dm_geo_replication_link_status"
  - "dm_geo_replication_link_status_TSQL"
  - "sys.dm_geo_replication_link_status"
  - "sys.dm_geo_replication_link_status_TSQL"
helpviewer_keywords:
  - "dm_geo_replication_link_status dynamic management view"
  - "sys.dm_geo_replication_link_status dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: d763d679-470a-4c21-86ab-dfe98d37e9fd
monikerRange: "=azuresqldb-current"
---
# sys.dm_geo_replication_link_status (Azure SQL Database and Azure SQL Managed Instance)

[!INCLUDE[Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

Contains a row for each replication link between primary and secondary databases in a geo-replication partnership. This includes both primary and secondary databases. If more than one continuous replication link exists for a given primary database, this table contains a row for each of the relationships. The view is created in all databases, including the **master** database. However, querying this view in the **master** database returns an empty set.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|link_guid|**uniqueidentifier**|Unique ID of the replication link.|  
|partner_server|**sysname**|Name of the SQL Database server containing the linked database.|  
|partner_database|**sysname**|Name of the linked database on the linked SQL Database server.|  
|last_replication|**datetimeoffset**|The time when the primary received the acknowledgment that the last log block has been hardened by the secondary, based on the primary database clock. Log blocks are sent to the geo-secondary continuously, without waiting for transactions to commit on the primary.  This value is available on the primary database only.|  
|replication_lag_sec|**int**|Time difference in seconds between the last_replication value and the timestamp of that transaction's commit on the primary based on the primary database clock.  This value is available on the primary database only.|  
|replication_state|**tinyint**|The state of geo-replication for this database, one of:<br /><br />1 = Seeding. The geo-replication target is being seeded but the two databases are not yet synchronized. Until seeding completes, you cannot connect to the secondary database. Removing secondary database from the primary will cancel the seeding operation.<br /><br />2 = Catch-up. The secondary database is in a transactionally consistent state and is being constantly synchronized with the primary database.<br /><br />4 = Suspended. This is not an active continuous-copy relationship. This state usually indicates that the bandwidth available for the interlink is insufficient for the level of transaction activity on the primary database. However, the continuous-copy relationship is still intact.| 
|replication_state_desc|**nvarchar(256)**|PENDING<br /><br /> SEEDING<br /><br /> CATCH_UP<br /><br /> SUSPENDED| 
|role|**tinyint**|Geo-replication role, one of:<br /><br /> 0 = Primary. The database_id  refers to the primary database in the geo-replication partnership.<br /><br /> 1 = Secondary.  The database_id  refers to the primary database in the geo-replication partnership.|  
|role_desc|**nvarchar(256)**|PRIMARY<br /><br /> SECONDARY|  
|secondary_allow_connections|**tinyint**|The secondary type, one of:<br /><br /> 0 = No direct connections are allowed to the secondary database and the database is not available for read access.<br /><br /> 2 = All connections are allowed to the database in the secondary replication for read-only access.|  
|secondary_allow_connections_desc|**nvarchar(256)**|No<br /><br /> All|  
|last_commit|**datetimeoffset**|The time of last transaction committed to the database. If retrieved on the primary database, it indicates the last commit time on the primary database. If retrieved on the secondary database, it indicates the last commit time on the secondary database. If retrieved on the secondary database when the primary of the replication link is down, it indicates until what point the secondary has caught up.|
  
> [!NOTE]  
>  If the replication relationship is terminated by removing the secondary database, the row for that database in the **sys.dm_geo_replication_link_status** view disappears.  
  
## Permissions  

Requires the `VIEW DATABASE STATE` permission in the database.  
  
## Examples 

This Transact-SQL query shows replication lags and last replication time of secondary databases.  
  
```sql
SELECT   
     link_guid  
   , partner_server  
   , last_replication  
   , replication_lag_sec   
FROM sys.dm_geo_replication_link_status;  
```  
  
## Next steps

Learn more about related concepts in the following articles:

- [ALTER DATABASE &#40;Azure SQL Database&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
- [sys.geo_replication_links &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-geo-replication-links-azure-sql-database.md)   
- [sys.dm_operation_status &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database.md)   
- [sp_wait_for_database_copy_sync](../system-stored-procedures/active-geo-replication-sp-wait-for-database-copy-sync.md)
- [Active geo-replication](/azure/azure-sql/database/active-geo-replication-overview)
- [Auto-failover groups overview & best practices (Azure SQL Database)](/azure/azure-sql/database/auto-failover-group-sql-db)
