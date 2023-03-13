---
title: "sp_update_category (Transact-SQL)"
description: "sp_update_category (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_update_category"
  - "sp_update_category_TSQL"
helpviewer_keywords:
  - "sp_update_category"
dev_langs:
  - "TSQL"
---
# sp_update_category (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Changes the name of a category.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_update_category  
     [@class =] 'class' ,   
     [@name =] 'old_name' ,  
     [@new_name =] 'new_name'  
```  
  
## Arguments  
`[ @class = ] 'class'`
 The class of the category to update. *class*is **varchar(8)**, with no default, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**ALERT**|Updates an alert category.|  
|**JOB**|Updates a job category.|  
|**OPERATOR**|Updates an operator category.|  
  
`[ @name = ] 'old_name'`
 The current name of the category. *old_name*is **sysname**, with no default.  
  
`[ @new_name = ] 'new_name'`
 The new name for the category. *new_name*is **sysname**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_update_category** must be run from the **msdb** database.  
  
## Permissions  
 To run this stored procedure, users must be granted the **sysadmin** fixed server role.  
  
## Examples  
 The following example renames a job category from `AdminJobs` to `Administrative Jobs`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_update_category  
  @class = N'JOB',  
  @name = N'AdminJobs',  
  @new_name = N'Administrative Jobs' ;  
GO  
```  
  
## See Also  
 [sp_add_category &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-category-transact-sql.md)   
 [sp_delete_category &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-category-transact-sql.md)   
 [sp_help_category &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-category-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
