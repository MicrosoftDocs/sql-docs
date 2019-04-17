---
title: "sysmail_delete_profile_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysmail_delete_profile_sp"
  - "sysmail_delete_profile_sp_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_delete_profile_sp"
ms.assetid: 71998653-4a02-446d-b6f7-50646a29e8a2
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysmail_delete_profile_sp (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Deletes a mail profile used by Database Mail.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_delete_profile_sp  { [ @profile_id = ] profile_id | [ @profile_name = ] 'profile_name' }  
```  
  
## Arguments  
`[ @profile_id = ] profile_id`
 Is the profile id of the profile to be deleted. *profile_id* is **int**, with a default of NULL. Either *profile_id* or *profile_name* must be specified.  
  
`[ @profile_name = ] 'profile_name'`
 Is the name of the profile to be deleted. *profile_name* is **sysname**, with a default of NULL. Either *profile_id* or *profile_name* must be specified.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 Deleting a profile does not delete the accounts used by the profile.  
  
 This stored procedure deletes the profile regardless of whether users have access to the profile. Use caution when removing the default private profile for a user or the default public profile for the **msdb** database. When no default profile is available, **sp_send_dbmail** requires the name of a profile as an argument. Therefore, removing a default profile may cause calls to **sp_send_dbmail** to fail. For more information, see [sp_send_dbmail &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-send-dbmail-transact-sql.md).  
  
 The stored procedure **sysmail_delete_profile_sp** is in the **msdb** database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database is not **msdb**.  
  
## Permissions  
 Execute permissions for this procedure default to members of the **sysadmin** fixed server role.  
  
## Examples  
 The following example shows deleting the profile named `AdventureWorks Administrator`.  
  
```  
EXECUTE msdb.dbo.sysmail_delete_profile_sp  
    @profile_name = 'AdventureWorks Administrator' ;  
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [Database Mail Configuration Objects](../../relational-databases/database-mail/database-mail-configuration-objects.md)   
 [Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)  
  
  
