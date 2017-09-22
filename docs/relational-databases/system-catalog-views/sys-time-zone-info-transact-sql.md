---
title: "sys.time_zone_info (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "09/12/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
applies_to: 
  - "Azure SQL Database"
  - "SQL Data Warehouse"
  - "SQL Server (starting with 2016)"
f1_keywords: 
  - "sys.time_zone_info"
  - "sys.time_zone_info_TSQL"
  - "time_zone_info"
  - "time_zone_info_TSQL"
helpviewer_keywords: 
  - "sys.time_zone_info system table"
ms.assetid: 3f51a9a4-75f8-4a11-9552-8bf6118b68da
caps.latest.revision: 7
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.time_zone_info (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-all-md](../../includes/tsql-appliesto-ss2016-all-md.md)]

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
  
  
