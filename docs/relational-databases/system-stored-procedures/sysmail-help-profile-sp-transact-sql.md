---
title: "sysmail_help_profile_sp (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysmail_help_profile_sp_TSQL"
  - "sysmail_help_profile_sp"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmail_help_profile_sp"
ms.assetid: d7169a8e-92b1-49eb-9124-3b2f69755ddb
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysmail_help_profile_sp (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Lists information about one or more mail profiles.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sysmail_help_profile_sp  [   [ @profile_id = ] profile_id | [ @profile_name = ] 'profile_name' ]  
```  
  
## Arguments  
 [ **@profile_id** = ] *profile_id*  
 The profile id to return information for. *profile_id* is **int**, with a default of NULL.  
  
 [ **@profile_name** = ] **'***profile_name***'**  
 The profile name to return information for. *profile_name* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 Returns a result set with the following columns.  
  
||||  
|-|-|-|  
|Column name|Data type|Description|  
|**profile_id**|**int**|The profile id for the profile.|  
|**name**|**sysname**|The profile name for the profile.|  
|**description**|**nvarchar(256)**|The description for the profile.|  
  
## Remarks  
 When a profile name or profile id is specified, **sysmail_help_profile_sp** returns information about that profile. Otherwise, **sysmail_help_profile_sp** returns information about every profile in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
 The stored procedure **sysmail_help_profile_sp** is in the **msdb** database and is owned by the **dbo** schema. The procedure must be executed with a three-part name if the current database is not **msdb**.  
  
## Permissions  
 Execute permissions for this procedure default to members of the **sysadmin** fixed server role.  
  
## Examples  
 **A. Listing all profiles**  
  
 The following example shows listing all profiles in the instance.  
  
```  
EXECUTE msdb.dbo.sysmail_help_profile_sp;  
```  
  
 Here is a sample result set, reformatted for line length:  
  
```  
profile_id  name                          description  
----------- ----------------------------- ------------------------------  
56          AdventureWorks Administrator  Administrative mail profile.    
57          AdventureWorks Operator       Operator mail profile.          
```  
  
 **B. Listing a specific profile**  
  
 The following example shows listing information for the profile `AdventureWorks Administrator`.  
  
```  
EXECUTE msdb.dbo.sysmail_help_profile_sp  
    @profile_name = 'AdventureWorks Administrator' ;  
```  
  
 Here is a sample result set, reformatted for line length:  
  
```  
profile_id  name                          description  
----------- ----------------------------- ------------------------------  
56          AdventureWorks Administrator  Administrative mail profile.    
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)  
  
  
