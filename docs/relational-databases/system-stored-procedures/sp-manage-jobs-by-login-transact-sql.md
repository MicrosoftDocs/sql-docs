---
title: "sp_manage_jobs_by_login (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_manage_jobs_by_login"
  - "sp_manage_jobs_by_login_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_manage_jobs_by_login"
ms.assetid: 832ec15a-6e92-4eb5-8c4a-af4dba79fbaa
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_manage_jobs_by_login (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Deletes or reassigns jobs that belong to the specified login.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_manage_jobs_by_login  
     [ @action = ] 'action'  
     [, [@current_owner_login_name = ] 'current_owner_login_name']  
     [, [@new_owner_login_name = ] 'new_owner_login_name']  
```  
  
## Arguments  
`[ @action = ] 'action'`
 The action to take for the specified login. *action* is **varchar(10)**, with no default. When *action*is **DELETE**, **sp_manage_jobs_by_login** deletes all jobs owned by *current_owner_login_name*. When *action* is **REASSIGN**, all jobs are assigned to *new_owner_login_name*.  
  
`[ @current_owner_login_name = ] 'current_owner_login_name'`
 The login name of the current job owner. *current_owner_login_name* is **sysname**, with no default.  
  
`[ @new_owner_login_name = ] 'new_owner_login_name'`
 The login name of the new job owner. Use this parameter only if *action* is **REASSIGN**. *new_owner_login_name* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Permissions  
 To run this stored procedure, users must be granted the **sysadmin** fixed server role.  
  
## Examples  
 The following example reassigns all jobs from `danw` to `françoisa`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_manage_jobs_by_login  
    @action = N'REASSIGN',  
    @current_owner_login_name = N'danw',  
    @new_owner_login_name = N'françoisa' ;  
GO  
```  
  
## See Also  
 [sp_delete_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-job-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
