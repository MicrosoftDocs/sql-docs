---
description: "sp_revokedbaccess (Transact-SQL)"
title: "sp_revokedbaccess (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_revokedbaccess_TSQL"
  - "sp_revokedbaccess"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_revokedbaccess"
ms.assetid: c997cfa1-539d-485c-a664-9c6f76bfe0c2
author: VanMSFT
ms.author: vanto
---
# sp_revokedbaccess (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes a database user from the current database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [DROP USER](../../t-sql/statements/drop-user-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_revokedbaccess [ @name_in_db = ] 'name'  
```  
  
## Arguments  
`[ @name_in_db = ] 'name'`
 Is the name of the database user to be removed. *name* is a **sysname** with no default. *name* can be the name of a server login, a Windows login, or a Windows group, and must exist in the current database. When you specify a Windows login or Windows group, specify the name by which it is known in the database.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 When the database user is removed, the permissions and aliases that depend on the user are also removed.  
  
 **sp_revokedbaccess** can remove only database users from the current database. Before removing a database user that owns objects in the current database, you must either transfer ownership of the objects or drop them from the database. For more information, see [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md).  
  
 **sp_revokedbaccess** cannot be executed within a user-defined transaction.  
  
## Permissions  
 Requires ALTER ANY USER permission on the database.  
  
## Examples  
 The following example removes the database user mapped to `Edmonds\LolanSo` from the current database.  
  
```  
EXEC sp_revokedbaccess 'Edmonds\LolanSo';  
GO  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [DROP USER &#40;Transact-SQL&#41;](../../t-sql/statements/drop-user-transact-sql.md)   
 [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md)  
  
  
