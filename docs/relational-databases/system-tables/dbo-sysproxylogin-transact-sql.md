---
title: "dbo.sysproxylogin (Transact-SQL)"
description: dbo.sysproxylogin (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dbo.sysproxylogin_TSQL"
  - "sysproxylogin_TSQL"
  - "dbo.sysproxylogin"
  - "sysproxylogin"
helpviewer_keywords:
  - "sysproxylogin system table"
dev_langs:
  - "TSQL"
ms.assetid: 433d33cb-bdf2-47bb-af78-2a40b7c8dfce
---
# dbo.sysproxylogin (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Records which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins are associated with each SQL Server Agent proxy account. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**proxy_id**|**int**|ID of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account. This value corresponds to the **proxy_id** column in the **sysproxies** table.|  
|**sid**|**varbinary(85)**|Microsoft Windows *security_identifier* for the SQL Server login.|  
|**principal_id**|**int**|ID of the user or group that has permission to use the proxy account for a specified subsystem step.|  
|**flags**|**int**|Type of login:<br /><br /> **0** = Windows user or group, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.<br /><br /> **1** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] fixed system role<br /><br /> **2** = **msdb** database role|  
  
## Remarks  
 Only members of the **sysadmin** fixed server role can access this table.  
  
## See Also  
 [dbo.sysproxies &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysproxies-transact-sql.md)  
  
  
