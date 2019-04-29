---
title: "sysmail_help_profileaccount_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysmail_help_profileaccount_sp_TSQL"
  - "sysmail_help_profileaccount_sp"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_help_profileaccount_sp"
ms.assetid: 3ea68271-0a6b-4d77-991c-4757f48f747a
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysmail_help_profileaccount_sp (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Lists the accounts associated with one or more Database Mail profiles.  
    
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_help_profileaccount_sp  
   {   [ @profile_id = ] profile_id   
      | [ @profile_name = ] 'profile_name' }  
   [ , {   [ @account_id = ] account_id  
         | [ @account_name = ] 'account_name' } ]  
```  
  
## Arguments  
`[ @profile_id = ] profile_id`
 Is the profile ID of the profile to list. *profile_id* is **int**, with a default of NULL. Either *profile_id* or *profile_name* must be specified.  
  
`[ @profile_name = ] 'profile_name'`
 Is the profile name of the profile to list. *profile_name* is **sysname**, with a default of NULL. Either *profile_id* or *profile_name* must be specified.  
  
`[ @account_id = ] account_id`
 Is the account ID to list. *account_id* is **int**, with a default of NULL. When *account_id* and *account_name* are both NULL, lists all the accounts in the profile.  
  
`[ @account_name = ] 'account_name'`
 Is the name of the account to list. *account_name* is **sysname**, with a default of NULL. When *account_id* and *account_name* are both NULL, lists all the accounts in the profile.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 Returns a result set with the following columns.  
  
||||  
|-|-|-|  
|Column name|Data type|Description|  
|**profile_id**|**int**|The profile ID of the profile.|  
|**profile_name**|**sysname**|The name of the profile.|  
|**account_id**|**int**|The account ID of the account.|  
|**account_name**|**sysname**|The name of the account.|  
|**sequence_number**|**int**|The sequence number of the account within the profile.|  
  
## Remarks  
 When no *profile_id* or *profile_name* is specified, this stored procedure returns information for every profile in the instance.  
  
 The stored procedure **sysmail_help_profileaccount_sp** is in the **msdb** database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database is not **msdb**.  
  
## Permissions  
 Execute permissions for this procedure default to members of the **sysadmin** fixed server role.  
  
## Examples  
 **A. Listing the accounts for a specific profile by name**  
  
 The following example shows listing the information for the `AdventureWorks Administrator` profile by specifying the profile name.  
  
```  
EXECUTE msdb.dbo.sysmail_help_profileaccount_sp  
   @profile_name = 'AdventureWorks Administrator';  
```  
  
 Here is a sample result set, edited for line length:  
  
```  
profile_id  profile_name                 account_id  account_name         sequence_number  
----------- ---------------------------- ----------- -------------------- ---------------  
131         AdventureWorks Administrator 197         Admin-MainServer     1  
131         AdventureWorks Administrator 198         Admin-BackupServer   2  
```  
  
 **B. Listing the accounts for a specific profile by profile ID**  
  
 The following example shows listing the information for the `AdventureWorks Administrator` profile by specifying the profile ID for the profile.  
  
```  
EXECUTE msdb.dbo.sysmail_help_profileaccount_sp  
    @profile_id = 131 ;  
```  
  
 Here is a sample result set, edited for line length:  
  
```  
profile_id  profile_name                 account_id  account_name         sequence_number  
----------- ---------------------------- ----------- -------------------- ---------------  
131         AdventureWorks Administrator 197         Admin-MainServer     1  
131         AdventureWorks Administrator 198         Admin-BackupServer   2  
```  
  
 **C. Listing the accounts for all profiles**  
  
 The following example shows listing the accounts for all profiles in the instance.  
  
```  
EXECUTE msdb.dbo.sysmail_help_profileaccount_sp;  
```  
  
 Here is a sample result set, edited for line length:  
  
```  
profile_id  profile_name                 account_id  account_name         sequence_number  
----------- ---------------------------- ----------- -------------------- ---------------  
131         AdventureWorks Administrator 197         Admin-MainServer     1  
131         AdventureWorks Administrator 198         Admin-BackupServer   2  
106         AdventureWorks Operator      210         Operator-MainServer  1  
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [Create a Database Mail Account](../../relational-databases/database-mail/create-a-database-mail-account.md)   
 [Database Mail Configuration Objects](../../relational-databases/database-mail/database-mail-configuration-objects.md)   
 [Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)  
  
  
