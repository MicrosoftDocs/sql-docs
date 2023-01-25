---
title: "dbo.sysproxies (Transact-SQL)"
description: dbo.sysproxies (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dbo.sysproxies_TSQL"
  - "sysproxies_TSQL"
  - "dbo.sysproxies"
  - "sysproxies"
helpviewer_keywords:
  - "sysproxies system table"
dev_langs:
  - "TSQL"
ms.assetid: a73da875-be22-45fc-b5e2-ea7ebd48e2d6
---
# dbo.sysproxies (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Defines attributes of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**proxy_id**|**int**|ID of the proxy account.|  
|**name**|**sysname**|Name of the proxy account.|  
|**credential_id**|**int**|ID of the credential that the proxy account uses.|  
|**enabled**|**tinyint**|Status of the proxy account:<br /><br /> **0** = Disabled. **1** = Enabled.|  
|**description**|**nvarchar(512)**|Description that the user entered when the proxy account was created.|  
|**user_sid**|**varbinary(85)**|Microsoft Windows *security_identifier* of the user or group associated with the proxy credential at the time the proxy is added. To ensure that you have the latest information (for example, after an `ALTER CREDENTIAL` command), run `sp_update_proxy` to refresh.|
|**credential_date_created**|**datetime**|Date and time that the credential was created.|  
  
## Remarks  
 Only members of the **sysadmin** fixed server role can access the **sysproxies** table.  
  
## See Also  
 [dbo.sysproxylogin &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysproxylogin-transact-sql.md)   
 [dbo.sysproxysubsystem &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysproxysubsystem-transact-sql.md)   
 [dbo.syssubsystems &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-syssubsystems-transact-sql.md)  
  
  
