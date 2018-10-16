---
title: "dbo.sysproxies (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dbo.sysproxies_TSQL"
  - "sysproxies_TSQL"
  - "dbo.sysproxies"
  - "sysproxies"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysproxies system table"
ms.assetid: a73da875-be22-45fc-b5e2-ea7ebd48e2d6
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# dbo.sysproxies (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Defines attributes of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**proxy_id**|**int**|ID of the proxy account.|  
|**name**|**sysname**|Name of the proxy account.|  
|**credential_id**|**int**|ID of the credential that the proxy account uses.|  
|**enabled**|**tinyint**|Status of the proxy account:<br /><br /> **0** = Disabled. **1** = Enabled.|  
|**description**|**nvarchar(512)**|Description that the user entered when the proxy account was created.|  
|**user_sid**|**varbinary(85)**|Microsoft Windows *security_identifier* of the user or group associated with the proxy credential.|  
|**credential_date_created**|**datetime**|Date and time that the credential was created.|  
  
## Remarks  
 Only members of the **sysadmin** fixed server role can access the **sysproxies** table.  
  
## See Also  
 [dbo.sysproxylogin &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysproxylogin-transact-sql.md)   
 [dbo.sysproxysubsystem &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysproxysubsystem-transact-sql.md)   
 [dbo.syssubsystems &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-syssubsystems-transact-sql.md)  
  
  
