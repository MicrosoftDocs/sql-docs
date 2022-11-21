---
title: "sysdbmaintplan_databases (Transact-SQL)"
description: sysdbmaintplan_databases (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysdbmaintplan_databases"
  - "sysdbmaintplan_databases_TSQL"
helpviewer_keywords:
  - "sysdbmaintplan_databases system table"
dev_langs:
  - "TSQL"
ms.assetid: f8413a44-8fcc-4899-84f2-b4afe0f8ec08
---
# sysdbmaintplan_databases (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This table is included to preserve existing information for instances upgraded from a previous version of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions do not change the contents of this table. This table is stored in the **msdb** database.  
  
 [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**plan_id**|**Uniqueidentifier**|Maintenance plan ID.|  
|**database_name**|**sysname**|Name of the database associated with the database maintenance plan.|  
  
  
