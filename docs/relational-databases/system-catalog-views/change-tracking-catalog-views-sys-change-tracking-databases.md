---
title: "sys.change_tracking_databases (Transact-SQL)"
description: Change Tracking Catalog Views - sys.change_tracking_databases
author: rwestMSFT
ms.author: randolphwest
ms.date: "08/08/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "change_tracking_databases"
  - "sys.change_tracking_databases_TSQL"
  - "sys.change_tracking_databases"
  - "change_tracking_databases_TSQL"
helpviewer_keywords:
  - "sys.change_tracking_databases"
  - "change tracking [SQL Server], sys.change_tracking_databases"
dev_langs:
  - "TSQL"
ms.assetid: bb233baa-2991-4904-a0eb-3772b81121a4
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Change Tracking Catalog Views - sys.change_tracking_databases
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns one row for each database that has change tracking enabled.  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id|**int**|ID of the database. This is unique within the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|is_auto_cleanup_on|**bit**|Indicates whether change tracking data is automatically cleaned up after the configured retention period:<br /><br /> 0 = Off<br /><br /> 1 = On|  
|retention_period|**int**|If autocleanup is being used, the retention period specifies how long the change tracking data is kept in the database.|  
|retention_period_units_desc|**nvarchar(60)**|Specifies the description of the retention period:<br /><br /> Minutes<br /><br /> Hours<br /><br /> Days|  
|retention_period_units|**tinyint**|Unit of time for the retention period:<br /><br /> 1 = Minutes<br /><br /> 2 = Hours<br /><br /> 3 = Days|  
  
## Permissions  
 The same permission checks are made for sys.change_tracking_databases as are made for sys.databases. If the caller of sys.change_tracking_databases is not the owner of the database, the minimum permissions that are required to see the corresponding row are ALTER ANY DATABASE or VIEW ANY DATABASE server-level permission, or CREATE DATABASE permission in the master database or current database.  
  
## See Also  
 [Change Tracking Catalog Views &#40;Transact-SQL&#41;](./catalog-views-transact-sql.md)   
 [Track Data Changes &#40;SQL Server&#41;](../../relational-databases/track-changes/track-data-changes-sql-server.md)  
  
