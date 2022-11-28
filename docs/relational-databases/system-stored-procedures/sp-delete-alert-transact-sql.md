---
description: "sp_delete_alert (Transact-SQL)"
title: "sp_delete_alert (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_delete_alert_TSQL"
  - "sp_delete_alert"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_delete_alert"
ms.assetid: a831315e-793d-41c4-8333-b324bb2bc614
author: markingmyname
ms.author: maghan
---
# sp_delete_alert (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes an alert.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_delete_alert [ @name = ] 'name'  
```  
  
## Arguments  
`[ @name = ] 'name'`
 The name of the alert. *name* is **sysname**, with no default.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 Removing an alert also removes any notifications associated with the alert.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute this procedure.  
  
## Examples  
 The following example removes an alert named `Test Alert`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_delete_alert  
   @name = N'Test Alert' ;  
GO  
```  
  
## See Also  
 [sp_add_alert &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-alert-transact-sql.md)   
 [sp_help_alert &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-alert-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
