---
description: "sp_msx_get_account (Transact-SQL)"
title: "sp_msx_get_account (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_msx_get_account_TSQL"
  - "sp_msx_get_account"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_msx_get_account"
ms.assetid: 7b478049-e2d0-4bac-865a-b97fd1d8dfbc
author: markingmyname
ms.author: maghan
---
# sp_msx_get_account (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Lists information on the credential that the target server uses to log in to the master server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_msx_get_account  
```  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 Returns the following result set:  
  
|Column Name|Type|Description|  
|-----------------|----------|-----------------|  
|msx_connection|**int**|Master server connection number.|  
|msx_credential_id|**int**|ID of the credential used for this master server connection.|  
|msx_credential_name|**sysname**|Name of the credential used for this master server connection.|  
|msx_login_name|**nvarchar(4000)**|Domain name and user name of the Windows user for the credential.|  
  
## Remarks  
 Returns an empty result set if there is no credential specified for this target server. To set the credential, use sp_msx_set_account.  
  
## Permissions  
 Requires membership in the sysadmin fixed server role.  
  
## Examples  
 The following example lists information for the credential that this target server uses to log in to the master server.  
  
```  
USE msdb ;  
GO  
  
EXECUTE dbo.sp_msx_get_account ;  
GO  
```  
  
 Here is a sample result set:  
  
 `msx_connection msx_credential_id msx_credential_name  msx_login_name`  
  
 `-------------- ----------------- -------------------- ------------------------------`  
  
 `1              65538             MsxAccount           AdventureWorks2012\MsxAccount`  
  
## See Also  
 [SQL Server Agent Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sql-server-agent-stored-procedures-transact-sql.md)   
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md)   
 [sp_msx_set_account &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-msx-set-account-transact-sql.md)  
  
  
