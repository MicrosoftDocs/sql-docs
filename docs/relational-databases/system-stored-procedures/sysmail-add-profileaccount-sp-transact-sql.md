---
title: "sysmail_add_profileaccount_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysmail_add_profileaccount_sp"
  - "sysmail_add_profileaccount_sp_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_add_profileaccount_sp"
ms.assetid: 7cbf430f-1997-45ea-9707-0086184de744
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysmail_add_profileaccount_sp (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a Database Mail account to a Database Mail profile. Execute **sysmail_add_profileaccount_sp** after a Database Account is created with [sysmail_add_account_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-add-account-sp-transact-sql.md), and a Database Profile is created with [sysmail_add_profile_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-add-profile-sp-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_add_profileaccount_sp { [ @profile_id = ] profile_id | [ @profile_name = ] 'profile_name' } ,  
    { [ @account_id = ] account_id | [ @account_name = ] 'account_name' }  
    [ , [ @sequence_number = ] sequence_number ]  
```  
  
## Arguments  
`[ @profile_id = ] profile_id`
 The profile id to add the account to. *profile_id* is **int**, with a default of NULL. Either the *profile_id* or the *profile_name* must be specified.  
  
`[ @profile_name = ] 'profile_name'`
 The profile name to add the account to. *profile_name* is **sysname**, with a default of NULL. Either the *profile_id* or the *profile_name* must be specified.  
  
`[ @account_id = ] account_id`
 The account id to add to the profile. *account_id* is **int**, with a default of NULL. Either the *account_id* or the *account_name* must be specified.  
  
`[ @account_name = ] 'account_name'`
 The name of the account to add to the profile. *account_name* is **sysname**, with a default of NULL. Either the *account_id* or the *account_name* must be specified.  
  
`[ @sequence_number = ] sequence_number`
 The sequence number of the account within the profile. *sequence_number* is **int**, with no default. The sequence number determines the order in which accounts are used in the profile.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 Both the profile and the account must already exist. Otherwise, the stored procedure returns an error.  
  
 Notice that this stored procedure does not change the sequence number of an account already associated with the specified profile. For more information about updating the sequence number of an account, see [sysmail_update_profileaccount_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-update-profileaccount-sp-transact-sql.md).  
  
 The sequence number determines the order in which Database Mail uses accounts in the profile. For a new e-mail message, Database Mail starts with the account that has the lowest sequence number. Should that account fail, Database Mail uses the account with the next highest sequence number, and so on until either Database Mail sends the message successfully, or the account with the highest sequence number fails. If the account with the highest sequence number fails, the Database Mail pauses attempts to send the mail for the amount of time configured in the *AccountRetryDelay* parameter of **sysmail_configure_sp**, then starts the process of attempting to send the mail again, starting with the lowest sequence number. Use the *AccountRetryAttempts* parameter of **sysmail_configure_sp**, to configure the number of times that the external mail process attempts to send the e-mail message using each account in the specified profile.  
  
 If more than one account exists with the same sequence number, Database Mail will only use one of those accounts for a given e-mail message. In this case, Database Mail makes no guarantees as to which of the accounts is used for that sequence number or that the same account is used from message to message.  
  
 The stored procedure **sysmail_add_profileaccount_sp** is in the **msdb** database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database is not **msdb**.  
  
## Permissions  
 Execute permissions for this procedure default to members of the **sysadmin** fixed server role.  
  
## Examples  
 The following example associates the profile `AdventureWorks Administrator` with the account `Audit Account`. The audit account has a sequence number of 1.  
  
```  
EXECUTE msdb.dbo.sysmail_add_profileaccount_sp  
    @profile_name = 'AdventureWorks Administrator',  
    @account_name = 'Audit Account',  
    @sequence_number = 1 ;  
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [Create a Database Mail Account](../../relational-databases/database-mail/create-a-database-mail-account.md)   
 [Database Mail Configuration Objects](../../relational-databases/database-mail/database-mail-configuration-objects.md)   
 [Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)  
  
  
