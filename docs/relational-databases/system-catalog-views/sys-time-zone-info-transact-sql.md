---
title: "sys.time_zone_info (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/06/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.time_zone_info"
  - "sys.time_zone_info_TSQL"
  - "time_zone_info"
  - "time_zone_info_TSQL"
helpviewer_keywords: 
  - "sys.time_zone_info system table"
ms.assetid: 3f51a9a4-75f8-4a11-9552-8bf6118b68da
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.time_zone_info (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2014-asdb-asdw-xxx-md](../../includes/tsql-appliesto-ss2014-asdb-asdw-xxx-md.md)]

  Returns information about supported time zones. All time zones installed on the computer are stored in the following registry hive:  
`KEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones`.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the time zone in Windows standard format. For example, **Cen. Australia Standard Time** or **Central European Standard Time**.|  
|**current_utc_offset**|**nvarchar(12)**|Current offset to UTC. For example, **+01:00** or **-07:00**.|  
|**is_currently_dst**|**bit**|True if currently observing daylight savings time.|  
  
## See Also  
 [GETUTCDATE &#40;Transact-SQL&#41;](../../t-sql/functions/getutcdate-transact-sql.md)   
 [AT TIME ZONE &#40;Transact-SQL&#41;](../../t-sql/queries/at-time-zone-transact-sql.md)   
 [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md)   
 [Server-wide Configuration Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/server-wide-configuration-catalog-views-transact-sql.md)  
