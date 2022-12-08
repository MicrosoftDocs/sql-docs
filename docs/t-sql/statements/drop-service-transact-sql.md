---
title: "DROP SERVICE (Transact-SQL)"
description: DROP SERVICE (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP_SERVICE_TSQL"
  - "DROP SERVICE"
helpviewer_keywords:
  - "deleting services"
  - "services [Service Broker], removing"
  - "dropping services"
  - "DROP SERVICE statement"
  - "removing services"
dev_langs:
  - "TSQL"
---
# DROP SERVICE (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

  Drops an existing service.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP SERVICE service_name  
[ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *service_name*  
 The name of the service to drop. Server, database, and schema names cannot be specified.  
  
## Remarks  
 You cannot drop a service if any conversation priorities refer to it.  
  
 Dropping a service deletes all messages for the service from the queue that the service uses. [!INCLUDE[ssSB](../../includes/sssb-md.md)] sends an error to the remote side of any open conversations that use the service.  
  
## Permissions  
 Permission for dropping a service defaults to the owner of the service, members of the db_ddladmin or db_owner fixed database roles, and members of the sysadmin fixed server role.  
  
## Examples  
 The following example drops the service `//Adventure-Works.com/Expenses`.  
  
```sql  
DROP SERVICE [//Adventure-Works.com/Expenses] ;  
```  
  
## See Also  
 [ALTER BROKER PRIORITY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-broker-priority-transact-sql.md)   
 [ALTER SERVICE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-service-transact-sql.md)   
 [CREATE SERVICE &#40;Transact-SQL&#41;](../../t-sql/statements/create-service-transact-sql.md)   
 [DROP BROKER PRIORITY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-broker-priority-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
