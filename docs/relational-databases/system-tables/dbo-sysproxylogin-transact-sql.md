---
title: "dbo.sysproxylogin (Transact-SQL)"
description: dbo.sysproxylogin (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "04/19/2024"
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
---
# dbo.sysproxylogin (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Shows which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins are associated with each SQL Server Agent proxy account. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**proxy_id**|**int**|ID of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account. This value corresponds to the **proxy_id** column in the **sysproxies** table.|  
|**sid**|**varbinary(85)**|Microsoft Windows *security_identifier* for the SQL Server login.|  
|**flags**|**int**|Type of `login`:<br /><br /> **0** = Windows user or group, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.<br /><br /> **1** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] fixed system role<br /><br /> **2** = **msdb** database role|  
  
## Remarks  
 Only members of the **sysadmin** fixed server role can access this table.  
  
## Related content

[dbo.sysproxies &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysproxies-transact-sql.md)  
  
  
