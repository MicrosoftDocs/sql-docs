---
title: "sys.dm_database_copies (Azure SQL Database)"
description: sys.dm_database_copies (Azure SQL Database)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/30/2022"
ms.service: sql-database
ms.topic: "reference"
f1_keywords:
  - "dm_database_copies_TSQL"
  - "sys.dm_database_copies"
  - "dm_database_copies"
  - "sys.dm_database_copies_TSQL"
helpviewer_keywords:
  - "dm_database_copies"
  - "sys.dm_database_copies"
dev_langs:
  - "TSQL"
ms.assetid: d03d4657-86d1-4496-97e6-cc3bc292e0b1
monikerRange: "=azuresqldb-current"
---
# sys.dm_database_copies (Azure SQL Database)
[!INCLUDE[Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Returns information about ongoing database copy operations for a database in Azure SQL Database.
  
To return information about geo-replication links, use the [sys.geo_replication_links](../../relational-databases/system-dynamic-management-views/sys-geo-replication-links-azure-sql-database.md) or [sys.dm_geo_replication_link_status](../../relational-databases/system-dynamic-management-views/sys-dm-geo-replication-link-status-azure-sql-database.md) views.


|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|**int**|The ID of the current database in the `sys.databases` view.|  
|**start_date**|**datetimeoffset**|The UTC time at a regional [!INCLUDE[ssSDS](../../includes/sssds-md.md)] datacenter when the database copying was initiated.|  
|**modify_date**|**datetimeoffset**|The UTC time at regional [!INCLUDE[ssSDS](../../includes/sssds-md.md)] datacenter when the database copying has completed. The new database is transactionally consistent with the primary database as of this time. The completion information is updated every 1 minute.<br /><br />UTC time reflecting the last update of the percent_complete field.|  
|**percent_complete**|**real**|The percentage of bytes that have been copied. Values range from 0 to 100. [!INCLUDE[ssSDS](../../includes/sssds-md.md)] may automatically recover from some errors, such as failover, and restart the database copy. In this case, percent_complete would restart from 0.|  
|**error_code**|**int**|When greater than 0, the code indicating the error that has occurred while copying. Value equals 0 if no errors have occurred.|  
|**error_desc**|**nvarchar(4096)**|Description of the error that occurred while copying.|  
|**error_severity**|**int**|Returns 16 if the database copy failed.|  
|**error_state**|**int**|Returns 1 if copy failed.|  
|**copy_guid**|**uniqueidentifier**|Unique ID of the copy operation.|  
|**partner_server**|**sysname**|Name of the SQL Database server where the copy is created.|  
|**partner_database**|**sysname**|Name of the database copy on the partner server.|  
|**replication_state**|**tinyint**|The state of continuous-copy replication for this database. Values are:<br /><br /> 0=Pending. Creation of the database copy is scheduled but the necessary preparation steps are not yet completed or are temporarily blocked by the seeding quota.<br /><br /> 1=Seeding. The copy database being seeded is not yet fully synchronized with the source database. In this state you cannot connect to the copy. To cancel the seeding operation in progress, the copy database must be dropped.|  
|**replication_state_desc**|**nvarchar(256)**|Description of replication_state, one of:<br /><br /> PENDING<br /><br /> SEEDING<br />|  
|**maximum_lag**|**int**|Reserved field.|  
|**is_continuous_copy**|**bit**|0 =  Returns 0|  
|**is_target_role**|**bit**|0 =Source database<br /><br /> 1 = Copy database|  
|**is_interlink_connected**|bit|Reserved field.|  
|**is_offline_secondary**|bit|Reserved field.|  
  
## Permissions 

This view is only available in the **master** database on the [logical server](/azure/azure-sql/database/logical-servers) to the server-level principal login.  
  
## Remarks

You can use the **sys.dm_database_copies** view in the **master** database of the source or target [logical server](/azure/azure-sql/database/logical-servers) in Azure SQL Database. When the database copy completes successfully and the new database becomes ONLINE, the row in the **sys.dm_database_copies** view is removed automatically.  

## Next steps

Learn more about related concepts in the following articles:

- [Copy a transactionally consistent copy of a database in Azure SQL Database](/azure/azure-sql/database/database-copy)
- [Geo-Replication Dynamic Management Views and Functions (Azure SQL Database)](geo-replication-dynamic-management-views-and-functions-azure-sql-database.md)
- [sys.dm_geo_replication_link_status (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-geo-replication-link-status-azure-sql-database.md)
- [Active geo-replication](/azure/azure-sql/database/active-geo-replication-overview)
- [Auto-failover groups overview & best practices (Azure SQL Database)](/azure/azure-sql/database/auto-failover-group-sql-db)
- [Auto-failover groups overview & best practices (Azure SQL Managed Instance)](/azure/azure-sql/managed-instance/auto-failover-group-sql-mi)
