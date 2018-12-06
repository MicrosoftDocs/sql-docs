---
title: "ALTER SERVICE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER SERVICE"
  - "ALTER_SERVICE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "modifying services"
  - "contracts [Service Broker], modifying"
  - "ALTER SERVICE statement"
  - "services [Service Broker], modifying"
ms.assetid: 2b4608f7-bb2e-4246-aa29-b52c55995b3a
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER SERVICE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes an existing service.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ALTER SERVICE service_name   
   [ ON QUEUE [ schema_name . ]queue_name ]   
   [ ( < opt_arg > [ , ...n ] ) ]  
[ ; ]  
  
<opt_arg> ::=  
   ADD CONTRACT contract_name | DROP CONTRACT contract_name  
```  
  
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
  
```  
ALTER SERVICE [//Adventure-Works.com/Expenses]  
    ON QUEUE NewQueue ;  
```  
  
### B. Adding a new contract to the service  
 The following example changes the `//Adventure-Works.com/Expenses` service to allow dialogs on the contract `//Adventure-Works.com/Expenses`.  
  
```  
ALTER SERVICE [//Adventure-Works.com/Expenses]  
    (ADD CONTRACT [//Adventure-Works.com/Expenses/ExpenseSubmission]) ;  
```  
  
### C. Adding a new contract to the service, dropping existing contract  
 The following example changes the `//Adventure-Works.com/Expenses` service to allow dialogs on the contract `//Adventure-Works.com/Expenses/ExpenseProcessing` and to disallow dialogs on the contract `//Adventure-Works.com/Expenses/ExpenseSubmission`.  
  
```  
ALTER SERVICE [//Adventure-Works.com/Expenses]  
    (ADD CONTRACT [//Adventure-Works.com/Expenses/ExpenseProcessing],   
     DROP CONTRACT [//Adventure-Works.com/Expenses/ExpenseSubmission]) ;  
```  
  
## See Also  
 [CREATE SERVICE &#40;Transact-SQL&#41;](../../t-sql/statements/create-service-transact-sql.md)   
 [DROP SERVICE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-service-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
