---
title: "sysmail_help_principalprofile_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/02/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysmail_help_principalprofile_sp_TSQL"
  - "sysmail_help_principalprofile_sp"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_help_principalprofile_sp"
ms.assetid: 0cfd6464-09c7-4f03-9d25-58001c096a9e
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysmail_help_principalprofile_sp (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Lists information about associations between Database Mail profiles and database principals.  
  
 
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_help_principalprofile_sp [ {   [ @principal_id = ] principal_id | [ @principal_name = ] 'principal_name' } ]  
    [ [ , ] {   [ @profile_id = ] profile_id | [ @profile_name = ] 'profile_name' } ]  
```  
  
## Arguments  
`[ @principal_id = ] principal_id`
 Is the ID of the database user or role in the **msdb** database for the association to list. *principal_id* is **int**, with a default of NULL. Either *principal_id* or *principal_name* may be specified.  
  
`[ @principal_name = ] 'principal_name'`
 Is the name of the database user or role in the **msdb** database for the association to list. *principal_name* is **sysname**, with a default of NULL. Either *principal_id* or *principal_name* may be specified.  
  
`[ @profile_id = ] profile_id`
 Is the ID of the profile for the association to list. *profile_id* is **int**, with a default of NULL. Either *profile_id* or *profile_name* may be specified.  
  
`[ @profile_name = ] 'profile_name'`
 Is the name of the profile for the association to list. *profile_name* is **sysname**, with a default of NULL. Either *profile_id* or *profile_name* may be specified.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 Returns a result set that contains the columns listed in the following table.  
  
||||  
|-|-|-|  
|Column name|Data type|Description|  
|**principal_id**|**int**|The ID of the database user.|  
|**principal_name**|**sysname**|The name of the database user.|  
|**profile_id**|**int**|The ID number of the Database Mail profile.|  
|**profile_name**|**sysname**|The name of the Database Mail profile.|  
|**is_default**|**bit**|The flag that states whether the profile is the default profile for the user.|  
  
## Remarks  
 If **sysmail_help_principalprofile_sp** is invoked without parameters, the result set returned lists all of the associations in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Otherwise, the result set contains information for associations that match the provided parameters. For example, the procedure lists all of the associations for a profile when the profile name is provided.  
  
 **sysmail_help_principalprofile_sp** is in the **msdb** database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database is not **msdb**.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
  
### A. Listing information for a specific association  
 The following example shows listing the information for all associations between the `AdventureWorks Administrator` profile and the `ApplicationLogin` principal in the `msdb` database.  
  
```  
EXECUTE msdb.dbo.sysmail_help_principalprofile_sp  
    @principal_name = 'danw',  
    @profile_name = 'AdventureWorks Administrator' ;  
```  
  
 Here is a sample result set, reformatted for line length.  
  
```  
principal_id principal_name     profile_id  profile_name                   is_default  
------------ ------------------ ----------- ------------------------------ ----------  
5            danw               9           AdventureWorks Administrator   1  
```  
  
### B. Listing information for all associations  
 The following example shows listing the information for all associations in the instance.  
  
```  
EXECUTE msdb.dbo.sysmail_help_principalprofile_sp ;  
```  
  
 Here is a sample result set, reformatted for line length.  
  
```  
principal_id principal_name     profile_id  profile_name                   is_default  
------------ ------------------ ----------- ------------------------------ ----------  
6            terrid             3           Product Update Profile         1  
5            danw               9           AdventureWorks Administrator   1  
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)  
  
  
