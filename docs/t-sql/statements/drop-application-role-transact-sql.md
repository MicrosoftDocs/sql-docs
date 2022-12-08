---
title: "DROP APPLICATION ROLE (Transact-SQL)"
description: DROP APPLICATION ROLE (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP_APPLICATION_ROLE_TSQL"
  - "DROP APPLICATION ROLE"
helpviewer_keywords:
  - "dropping application roles"
  - "deleting application roles"
  - "removing application roles"
  - "application roles [SQL Server], removing"
  - "DROP APPLICATION ROLE statement"
dev_langs:
  - "TSQL"
---
# DROP APPLICATION ROLE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Removes an application role from the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP APPLICATION ROLE rolename  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *rolename*  
 Specifies the name of the application role to be dropped.  
  
## Remarks  
 If the application role owns any securables it cannot be dropped. Before dropping an application role that owns securables, you must first transfer ownership of the securables, or drop them.  
  
> [!CAUTION]  
>  [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]  
  
## Permissions  
 Requires ALTER ANY APPLICATION ROLE permission on the database.  
  
## Examples  
 Drop application role "weekly_ledger" from the database.  
  
```sql  
DROP APPLICATION ROLE weekly_ledger;  
GO  
```  
  
## See Also  
 [Application Roles](../../relational-databases/security/authentication-access/application-roles.md)   
 [CREATE APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-application-role-transact-sql.md)   
 [ALTER APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-application-role-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
