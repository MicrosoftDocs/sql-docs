---
title: "DROP ROUTE (Transact-SQL)"
description: DROP ROUTE (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP ROUTE"
  - "DROP_ROUTE_TSQL"
helpviewer_keywords:
  - "dropping routes"
  - "DROP ROUTE statement"
  - "deleting routes"
  - "routes [Service Broker], removing"
  - "removing routes"
dev_langs:
  - "TSQL"
---
# DROP ROUTE (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Drops a route, deleting the information for the route from the routing table of the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP ROUTE route_name  
[ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *route_name*  
 The name of the route to drop. Server, database, and schema names cannot be specified.  
  
## Remarks  
 The routing table that stores the routes is a metadata table that can be read through the catalog view **sys.routes**. The routing table can only be updated through the CREATE ROUTE, ALTER ROUTE, and DROP ROUTE statements.  
  
 You can drop a route regardless of whether any conversations use the route. However, if there is no other route to the remote service, messages for those conversations will remain in the transmission queue until a route to the remote service is created or the conversation times out.  
  
## Permissions  
 Permission for dropping a route defaults to the owner of the route, members of the db_ddladmin or db_owner fixed database roles, and members of the sysadmin fixed server role.  
  
## Examples  
 The following example deletes the `ExpenseRoute` route.  
  
```sql  
DROP ROUTE ExpenseRoute ;  
```  
  
## See Also  
 [ALTER ROUTE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-route-transact-sql.md)   
 [CREATE ROUTE &#40;Transact-SQL&#41;](../../t-sql/statements/create-route-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sys.routes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-routes-transact-sql.md)  
  
  
