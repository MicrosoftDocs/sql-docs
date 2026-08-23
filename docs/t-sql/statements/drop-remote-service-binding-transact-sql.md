---
title: "DROP REMOTE SERVICE BINDING (Transact-SQL)"
description: DROP REMOTE SERVICE BINDING (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP REMOTE SERVICE BINDING"
  - "DROP_REMOTE_SERVICE_BINDING_TSQL"
helpviewer_keywords:
  - "dropping remote service bindings"
  - "removing remote service bindings"
  - "deleting remote service bindings"
  - "remote service bindings [Service Broker], dropping"
  - "DROP REMOTE SERVICE BINDING statement"
dev_langs:
  - "TSQL"
---
# DROP REMOTE SERVICE BINDING (Transact-SQL)
[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

  Drops a remote service binding.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP REMOTE SERVICE BINDING binding_name  
[ ; ]  
```  
  
## Arguments
 *binding_name*  
 Is the name of the remote service binding to drop. Server, database, and schema names cannot be specified.  
  
## Permissions  
 Permission for dropping a remote service binding defaults to the owner of the remote service binding, members of the db_owner fixed database role, and members of the sysadmin fixed server role.  
  
## Examples  
 The following example deletes the remote service binding `APBinding` from the database.  
  
```sql 
DROP REMOTE SERVICE BINDING APBinding ;  
```  
  
## Related content

- [CREATE REMOTE SERVICE BINDING (Transact-SQL)](create-remote-service-binding-transact-sql.md)
- [ALTER REMOTE SERVICE BINDING (Transact-SQL)](alter-remote-service-binding-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
