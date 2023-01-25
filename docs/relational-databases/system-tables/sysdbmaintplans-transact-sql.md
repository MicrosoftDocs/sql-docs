---
title: "sysdbmaintplans (Transact-SQL)"
description: sysdbmaintplans (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysdbmaintplans_TSQL"
  - "sysdbmaintplans"
helpviewer_keywords:
  - "sysdbmaintplans system table"
dev_langs:
  - "TSQL"
ms.assetid: 0363296a-3082-48a9-9eb5-a1020b2f541a
---
# sysdbmaintplans (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This table is included in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to preserve existing information for instances upgraded from a previous version of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not change the contents of this table. This table is stored in the **msdb** database.  
  
 [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**plan_id**|**uniqueidentifier**|Database maintenance plan ID.|  
|**plan_name**|**sysname**|Database maintenance plan name.|  
|**date_created**|**datetime**|Date the database maintenance plan was created.|  
|**owner**|**sysname**|Owner of the database maintenance plan.|  
|**max_history_rows**|**int**|Maximum number of rows allotted for recording the history of the database maintenance plan in the system table.|  
|**remote_history_server**|**sysname**|Name of the remote server to which the history report could be written.|  
|**max_remote_history_rows**|**int**|Maximum number of rows allotted in the system table on a remote server to which the history report could be written.|  
|**user_defined_1**|**int**|Default is NULL.|  
|**user_defined_2**|**nvarchar(100)**|Default is NULL.|  
|**user_defined_3**|**datetime**|Default is NULL.|  
|**user_defined_4**|**uniqueidentifier**|Default is NULL.|  
|**log_shipping**|**bit**|Log shipping status:<br /><br /> **0** = Disabled **1** = Enabled|  
  
  
