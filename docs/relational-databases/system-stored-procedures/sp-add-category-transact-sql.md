---
description: "sp_add_category (Transact-SQL)"
title: "sp_add_category (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_add_category"
  - "sp_add_category_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_add_category"
ms.assetid: 6cca32cd-d941-4378-aed6-a7c90cb7520a
author: MashaMSFT
ms.author: mathoma
---
# sp_add_category (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

  Adds the specified category of jobs, alerts, or operators to the server. For alternative method, see [Create Job Category using SQL Server Management Studio](../../ssms/agent/create-a-job-category.md).
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
 > [!IMPORTANT]  
 > On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.
  
## Syntax  
  
```  
  
sp_add_category   
     [ [ @class = ] 'class', ]   
     [ [ @type = ] 'type', ]   
     { [ @name = ] 'name' }  
```  
  
## Arguments  
`[ @class = ] 'class'`
 The class of the category to be added. *class* is **varchar(8)** with a default value of JOB, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|JOB|Adds a job category.|  
|ALERT|Adds an alert category.|  
|OPERATOR|Adds an operator category.|  
  
`[ @type = ] 'type'`
 The type of category to be added. *type* is **varchar(12)**, with a default value of **LOCAL**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|LOCAL|A local job category.|  
|MULTI-SERVER|A multiserver job category.|  
|NONE|A category for a class other than JOB**.**|  
  
`[ @name = ] 'name'`
 The name of the category to be added. The name must be unique within the specified class. *name* is **sysname**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_add_category** must be run from the **msdb** database.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_add_category**.  
  
## Examples  
 The following example creates a local job category named `AdminJobs`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_add_category  
    @class=N'JOB',  
    @type=N'LOCAL',  
    @name=N'AdminJobs' ;  
GO  
```  
  
## See Also  
 [sp_delete_category &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-category-transact-sql.md)   
 [sp_help_category &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-category-transact-sql.md)   
 [sp_update_category &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-category-transact-sql.md)   
 [dbo.sysjobs &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysjobs-transact-sql.md)   
 [dbo.sysjobservers &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysjobservers-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
