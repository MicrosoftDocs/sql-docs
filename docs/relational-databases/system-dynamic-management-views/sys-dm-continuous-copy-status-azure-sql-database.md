---
description: "sys.dm_continuous_copy_status (Azure SQL Database and Azure SQL Managed Instance)"
title: "sys.dm_continuous_copy_status"
titleSuffix: Azure SQL Database and Azure SQL Managed Instance
ms.date: "03/03/2022"
ms.service: sql-database
ms.reviewer: ""
ms.topic: "reference"
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
author: rwestMSFT
ms.author: randolphwest
monikerRange: "= azuresqldb-current"
ms.custom: seo-dt-2019
---
# sys.dm_continuous_copy_status (Azure SQL Database and Azure SQL Managed Instance)
[!INCLUDE[Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

This view is unused and is preserved for backwards compatibility. See [sys.dm_geo_replication_link_status](../../relational-databases/system-dynamic-management-views/sys-dm-geo-replication-link-status-azure-sql-database.md) to learn how to query data regarding each replication link between primary and secondary databases in a geo-replication partnership.

|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|**copy_guid**|**uniqueidentifier**|This column is unused and is preserved for backwards compatibility.|  
|**partner_server**|**sysname**|This column is unused and is preserved for backwards compatibility.|  
|**partner_database**|**sysname**|This column is unused and is preserved for backwards compatibility.|  
|**last_replication**|**datetimeoffset**|This column is unused and is preserved for backwards compatibility.|  
|**replication_lag_sec**|**int**|This column is unused and is preserved for backwards compatibility.|  
|**replication_state**|**tinyint**|This column is unused and is preserved for backwards compatibility.|  
|**replication_state_desc**|**nvarchar(256)**|This column is unused and is preserved for backwards compatibility.|  
|**is_rpo_limit_reached**|**bit**|This column is unused and is preserved for backwards compatibility.|  
|**is_target_role**|**bit**|This column is unused and is preserved for backwards compatibility.|  
|**is_interlink_connected**|**bit**|This column is unused and is preserved for backwards compatibility.|  
  
## Permissions  

To retrieve data, requires membership in the **db_owner** database role. The dbo user, members of the **dbmanager** database role, and the sa login can all query this view as well.  
  
## Remarks  

The **sys.dm_continuous_copy_status** view is created in the **resource** database and is visible in all databases, including the logical master. However, querying this view in the logical master returns an empty set.  
  
If the continuous-copy relationship is terminated on a database, the row for that database in the **sys.dm_continuous_copy_status** view disappears.  
  
Like the **sys.dm_database_copies** view, **sys.dm_continuous_copy_status** reflects the state of the continuous copy relationship in which the database is either a primary or active secondary database. Unlike **sys.dm_database_copies**, **sys.dm_continuous_copy_status** contains several columns that provide details about operations and performance. These columns include **last_replication**, and **replication_lag_sec**..  
  
## Next steps

Learn more about related concepts in the following articles:

- [sys.dm_geo_replication_link_status](../../relational-databases/system-dynamic-management-views/sys-dm-geo-replication-link-status-azure-sql-database.md)
- [Geo-Replication Dynamic Management Views and Functions (Azure SQL Database)](geo-replication-dynamic-management-views-and-functions-azure-sql-database.md)
- [sys.dm_database_copies &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-database-copies-azure-sql-database.md)
- [Active Geo-Replication Stored Procedures &#40;Transact-SQL&#41;](../system-stored-procedures/system-stored-procedures-transact-sql.md)