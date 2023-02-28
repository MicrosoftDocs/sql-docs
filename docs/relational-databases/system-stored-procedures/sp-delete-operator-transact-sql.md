---
title: "sp_delete_operator (Transact-SQL)"
description: "sp_delete_operator (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_operator"
  - "sp_delete_operator_TSQL"
helpviewer_keywords:
  - "sp_delete_operator"
dev_langs:
  - "TSQL"
---
# sp_delete_operator (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes an operator.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_delete_operator [ @name = ] 'name'   
     [ , [ @reassign_to_operator = ] 'reassign_operator' ]   
```  
  
## Arguments  
`[ @name = ] 'name'`
 The name of the operator to delete. *name* is **sysname**, with no default.  
  
`[ @reassign_to_operator = ] 'reassign_operator'`
 The name of an operator to whom the specified operator's alerts can be reassigned. *reassign_operator* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 When an operator is removed, all the notifications associated with the operator are also removed.  
  
## Permissions  
 Members of the **sysadmin** fixed server role can execute **sp_delete_operator**.  
  
## Examples  
 The following example deletes operator `François Ajenstat`.  
  
```  
USE msdb ;  
GO  
  
EXEC sp_delete_operator @name = 'François Ajenstat' ;  
GO  
```  
  
## See Also  
 [sp_add_operator &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-operator-transact-sql.md)   
 [sp_help_operator &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-operator-transact-sql.md)   
 [sp_update_operator &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-operator-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
