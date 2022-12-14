---
description: "sysmail_delete_principalprofile_sp (Transact-SQL)"
title: "sysmail_delete_principalprofile_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sysmail_delete_principalprofile_sp_TSQL"
  - "sysmail_delete_principalprofile_sp"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_delete_principalprofile_sp"
ms.assetid: 8fc14700-e17a-4073-9a96-7fc23e775c69
author: markingmyname
ms.author: maghan
---
# sysmail_delete_principalprofile_sp (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes permission for a database user or role to use a public or private Database Mail profile.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_delete_principalprofile_sp  { [ @principal_id = ] principal_id | [ @principal_name = ] 'principal_name' } ,  
    { [ @profile_id = ] profile_id | [ @profile_name = ] 'profile_name' }  
```  
  
## Arguments  
`[ @principal_id = ] principal_id`
 Is the ID of the database user or role in the **msdb** database for the association to delete. *principal_id* is **int**, with a default of NULL. To make a public profile into a private profile, provide the principal ID **0** or the principal name **'public'**. Either *principal_id* or *principal_name* must be specified.  
  
`[ @principal_name = ] 'principal_name'`
 Is the name of the database user or role in the **msdb** database for the association to delete. *principal_name* is **sysname**, with a default of NULL. To make a public profile into a private profile, provide the principal ID **0** or the principal name **'public'**. Either *principal_id* or *principal_name* must be specified.  
  
`[ @profile_id = ] profile_id`
 Is the ID of the profile for the association to delete. *profile_id* is **int**, with a default of NULL. Either *profile_id* or *profile_name* must be specified.  
  
`[ @profile_name = ] 'profile_name'`
 Is the name of the profile for the association to delete. *profile_name* is **sysname**, with a default of NULL. Either *profile_id* or *profile_name* must be specified.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 To make a public profile into a private profile, provide **'public'** for the principal name or **0** for the principal id.  
  
 Use caution when removing permissions for the default private profile for a user or the default public profile. When no default profile is available, **sp_send_dbmail** requires the name of a profile as an argument. Therefore, removing a default profile may cause calls to **sp_send_dbmail** to fail. For more information, see [sp_send_dbmail &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-send-dbmail-transact-sql.md).  
  
 The stored procedure **sysmail_delete_principalprofile_sp** is in the **msdb** database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database is not **msdb**.  
  
## Permissions  
 Execute permissions for this procedure default to members of the **sysadmin** fixed server role.  
  
## Examples  
 The following example shows deleting the association between the profile **AdventureWorks Administrator** and the login **ApplicationUser** in the **msdb** database.  
  
```  
EXECUTE msdb.dbo.sysmail_delete_principalprofile_sp  
    @principal_name = 'ApplicationUser',  
    @profile_name = 'AdventureWorks Administrator' ;  
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [Database Mail Configuration Objects](../../relational-databases/database-mail/database-mail-configuration-objects.md)   
 [Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)  
  
  
