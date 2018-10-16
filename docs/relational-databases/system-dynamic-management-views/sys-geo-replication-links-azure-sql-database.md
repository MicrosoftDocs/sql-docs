---
title: "sys.geo_replication_links (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "10/18/2016"
ms.prod: ""
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: conceptual
f1_keywords: 
  - "dm_geo_replication_links_TSQL"
  - "dm_geo_replication_links"
  - "sys.dm_geo_replication_links"
  - "sys.dm_geo_replication_links_TSQL"
helpviewer_keywords: 
  - "sys.dm_geo_replication_links dynamic management view"
  - "dm_geo_replication_links dynamic management view"
ms.assetid: 58911798-1d60-4f28-87ab-2def2bfc3de7
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sys.geo_replication_links (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  Contains a row for each replication link between primary and secondary databases in a geo-replication partnership. This view resides in the logical master database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id|**int**|ID of the current database in the sys.databases view.|  
|start_date|**datetimeoffset**|UTC time at a regional SQL Database datacenter when the database replication was initiated|  
|modify_date|**datetimeoffset**|UTC time at regional SQL Database datacenter when the database geo-replication has completed. The new database is synchronized with the primary database as of this time. .|  
|link_guid|**uniqueidentifier**|Unique ID of the geo-replication link.|  
|partner_server|**sysname**|Name of the logical server containing the geo-replicated database.|  
|partner_database|**sysname**|Name of the geo-replicated database on the linked logical server.|  
|replication_state|**tinyint**|The state of geo-replication for this database, one of:.<br /><br /> 0 = Pending. Creation of the active secondary database is scheduled but the necessary preparation steps are not yet completed.<br /><br /> 1 = Seeding. The geo-replication target is being seeded but the two databases are not yet synchronized. Until seeding completes, you cannot connect to the secondary database. Removing secondary database from the primary will cancel the seeding operation.<br /><br /> 2 = Catch-up. The secondary database is  in a transactionally consistent state and is being constantly  synchronized with the primary database.|  
|replication_state_desc|**nvarchar(256)**|PENDING<br /><br /> SEEDING<br /><br /> CATCH_UP|  
|role|**tinyint**|Geo-replication role, one of:<br /><br /> 0 = Primary. The database_id  refers to the primary database in the geo-replication partnership.<br /><br /> 1 = Secondary.  The database_id  refers to the primary database in the geo-replication partnership.|  
|role_desc|**nvarchar(256)**|PRIMARY<br /><br /> SECONDARY|  
|secondary_allow_connections|**tinyint**|The secondary type, one of:<br /><br /> 0 = No. The secondary database is not accessible until failover.<br /><br /> 1 = ReadOnly. The secondary database is accessible only to client connections with ApplicationIntent=ReadOnly.<br /><br /> 2 = All. The secondary database is accessible to any client connection.|  
|secondary_allow_connections _desc|**nvarchar(256)**|No<br /><br /> All<br /><br /> Read-Only|  
  
## Permissions  
 This view is only available in the **master** database to the server-level principal login.  
  
## Example  
 Show all databases with geo-replication links.  
  
```  
SELECT   
     database_id  
   , start_date  
   , partner_server  
   , partner_database  
   , replication_state  
   , role_desc  
   , secondary_allow_connections_desc   
FROM sys.geo_replication_links;  
```  
  
## See Also  
 [ALTER DATABASE (Azure SQL Database)](../../t-sql/statements/alter-database-azure-sql-database.md)   
 [sys.dm_geo_replication_link_status &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-geo-replication-link-status-azure-sql-database.md)   
 [sys.dm_operation_status &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database.md)  
  
  
