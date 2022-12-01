---
description: "sysmail_delete_profileaccount_sp (Transact-SQL)"
title: "sysmail_delete_profileaccount_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sysmail_delete_profileaccount_sp"
  - "sysmail_delete_profileaccount_sp_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_delete_profileaccount_sp"
ms.assetid: b58d06f2-d6c9-4c8e-95bd-027c50f4621a
author: markingmyname
ms.author: maghan
---
# sysmail_delete_profileaccount_sp (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes an account from a Database Mail profile.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_delete_profileaccount_sp  {   [ @profile_id = ] profile_id | [ @profile_name = ] 'profile_name' } ,  
    {   [ @account_id = ] account_id | [ @account_name = ] 'account_name' }  
```  
  
## Arguments  
`[ @profile_id = ] profile_id`
 The profile ID of the profile to delete. *profile_id* is **int**, with a default of NULL. Either the *profile_id* or the *profile_name* may be specified.  
  
`[ @profile_name = ] 'profile_name'`
 The profile name of the profile to delete. *profile_name* is **sysname**, with a default of NULL. Either the *profile_id* or the *profile_name* may be specified.  
  
`[ @account_id = ] account_id`
 The account ID to delete. *account_id* is **int**, with a default of NULL. Either the *account_id* or the *account_name* may be specified.  
  
`[ @account_name = ] 'account_name'`
 The name of the account to delete. *account_name* is **sysname**, with a default of NULL. Either the *account_id* or the *account_name* may be specified.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 Returns an error if the account specified is not associated with the profile specified.  
  
 When an account is specified but no profile is specified, this stored procedure removes the specified account from all profiles. For example, if you are preparing to shut down an existing SMTP server, you remove accounts that use that SMTP server from all profiles, rather than removing each account from each profile.  
  
 When a profile is specified but no account is specified, this stored procedure removes all accounts from the specified profile. For example, if you are changing the SMTP servers a profile uses, it may be convenient to remove all accounts from the profile and then add new accounts as necessary.  
  
 The stored procedure **sysmail_delete_profileaccount_sp** is in the **msdb** database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database is not **msdb**.  
  
## Permissions  
 Execute permissions for this procedure default to members of the **sysadmin** fixed server role.  
  
## Examples  
 The following example shows removing  the account `Audit Account` from the profile `AdventureWorks Administrator`.  
  
```  
EXECUTE msdb.dbo.sysmail_delete_profileaccount_sp  
    @profile_name = 'AdventureWorks Administrator',  
    @account_name = 'Audit Account' ;  
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [Create a Database Mail Account](../../relational-databases/database-mail/create-a-database-mail-account.md)   
 [Database Mail Configuration Objects](../../relational-databases/database-mail/database-mail-configuration-objects.md)   
 [Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)  
  
  
