---
title: "ALTER SERVICE (Transact-SQL)"
description: ALTER SERVICE (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/12/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER SERVICE"
  - "ALTER_SERVICE_TSQL"
helpviewer_keywords:
  - "modifying services"
  - "contracts [Service Broker], modifying"
  - "ALTER SERVICE statement"
  - "services [Service Broker], modifying"
dev_langs:
  - "TSQL"
---
# ALTER SERVICE (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Changes an existing service.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql 
ALTER SERVICE service_name   
   [ ON QUEUE [ schema_name . ]queue_name ]   
   [ ( < opt_arg > [ , ...n ] ) ]  
[ ; ]  
  
<opt_arg> ::=  
   ADD CONTRACT contract_name | DROP CONTRACT contract_name  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

*service_name*  
Is the name of the service to change. Server, database, and schema names cannot be specified.  
  
ON QUEUE [ _schema_name_**.** ] *queue_name*  
Specifies the new queue for this service. [!INCLUDE[ssSB](../../includes/sssb-md.md)] moves all messages for this service from the current queue to the new queue.  
  
ADD CONTRACT *contract_name*  
Specifies a contract to add to the contract set exposed by this service.  
  
DROP CONTRACT *contract_name*  
Specifies a contract to delete from the contract set exposed by this service. [!INCLUDE[ssSB](../../includes/sssb-md.md)] sends an error message on any existing conversations with this service that use this contract.  
  
## Remarks

When the ALTER SERVICE statement deletes a contract from a service, the service can no longer be a target for conversations that use that contract. Therefore, [!INCLUDE[ssSB](../../includes/sssb-md.md)] does not allow new conversations to the service on that contract. Existing conversations that use the contract are unaffected.  
  
 To alter the AUTHORIZATION for a service, use the ALTER AUTHORIZATION statement.  
  
## Permissions

Permission for altering a service defaults to the owner of the service, members of the **db_ddladmin** or **db_owner** fixed database roles, and members of the **sysadmin** fixed server role.  
  
## Examples  

### A. Changing the queue for a service

The following example changes the `//Adventure-Works.com/Expenses` service to use the queue `NewQueue`.  
  
```sql  
ALTER SERVICE [//Adventure-Works.com/Expenses]  
    ON QUEUE NewQueue ;  
```  

### B. Adding a new contract to the service

The following example changes the `//Adventure-Works.com/Expenses` service to allow dialogs on the contract `//Adventure-Works.com/Expenses`.  
  
```sql  
ALTER SERVICE [//Adventure-Works.com/Expenses]  
    (ADD CONTRACT [//Adventure-Works.com/Expenses/ExpenseSubmission]) ;  
```

### C. Adding a new contract to the service, dropping existing contract

The following example changes the `//Adventure-Works.com/Expenses` service to allow dialogs on the contract `//Adventure-Works.com/Expenses/ExpenseProcessing` and to disallow dialogs on the contract `//Adventure-Works.com/Expenses/ExpenseSubmission`.  
  
```sql  
ALTER SERVICE [//Adventure-Works.com/Expenses]  
    (ADD CONTRACT [//Adventure-Works.com/Expenses/ExpenseProcessing],   
     DROP CONTRACT [//Adventure-Works.com/Expenses/ExpenseSubmission]) ;  
```  

### D. Altering the owner of a service

The following example changes the owner of `//Adventure-Works.com/Expenses` to the [dbo](../../relational-databases/security/authentication-access/principals-database-engine.md#dbo-user-and-dbo-schema) user.

```sql
ALTER AUTHORIZATION ON SERVICE::[//Adventure-Works.com/Expenses] TO dbo ;
GO
```

## See also

- [CREATE SERVICE &#40;Transact-SQL&#41;](../../t-sql/statements/create-service-transact-sql.md)
- [DROP SERVICE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-service-transact-sql.md)
- [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
