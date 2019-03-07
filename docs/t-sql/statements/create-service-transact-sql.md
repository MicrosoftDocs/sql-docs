---
title: "CREATE SERVICE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE_SERVICE_TSQL"
  - "SERVICE"
  - "CREATE SERVICE"
  - "SERVICE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "services [Service Broker], creating"
  - "CREATE SERVICE statement"
  - "contracts [Service Broker], service creation"
ms.assetid: fb804fa2-48eb-4878-a12f-4e0d5f4bc9e3
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# CREATE SERVICE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates a new service. A [!INCLUDE[ssSB](../../includes/sssb-md.md)] service is a name for a specific task or set of tasks. [!INCLUDE[ssSB](../../includes/sssb-md.md)] uses the name of the service to route messages, deliver messages to the correct queue within a database, and enforce the contract for a conversation.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CREATE SERVICE service_name  
   [ AUTHORIZATION owner_name ]  
   ON QUEUE [ schema_name. ]queue_name  
   [ ( contract_name | [DEFAULT][ ,...n ] ) ]  
[ ; ]  
```  
  
## Arguments  
 *service_name*  
 Is the name of the service to create. A new service is created in the current database and owned by the principal specified in the AUTHORIZATION clause. Server, database, and schema names cannot be specified. The *service_name* must be a valid **sysname**.  
  
> [!NOTE]  
>  Do not create a service that uses the keyword ANY for the *service_name*. When you specify ANY for a service name in CREATE BROKER PRIORITY, the priority is considered for all services. It is not limited to a service whose name is ANY.  
  
 AUTHORIZATION *owner_name*  
 Sets the owner of the service to the specified database user or role. When the current user is **dbo** or **sa**, *owner_name* may be the name of any valid user or role. Otherwise, *owner_name* must be the name of the current user, the name of a user that the current user has IMPERSONATE permission for, or the name of a role to which the current user belongs.  
  
 ON QUEUE [ _schema_name_**.** ] *queue_name*  
 Specifies the queue that receives messages for the service. The queue must exist in the same database as the service. If no *schema_name* is provided, the schema is the default schema for the user that executes the statement.  
  
 *contract_name*  
 Specifies a contract for which this service may be a target. Service programs initiate conversations to this service using the contracts specified. If no contracts are specified, the service may only initiate conversations.  
  
 **[**DEFAULT**]**  
 Specifies that the service may be a target for conversations that follow the DEFAULT contract. In the context of this clause, DEFAULT is not a keyword, and must be delimited as an identifier. The DEFAULT contract allows both sides of the conversation to send messages of message type DEFAULT. Message type DEFAULT uses validation NONE.  
  
## Remarks  
 A service exposes the functionality provided by the contracts with which it is associated, so that they can be used by other services. The CREATE SERVICE statement specifies the contracts that this service is the target for. A service can only be a target for conversations that use the contracts specified by the service. A service that specifies no contracts exposes no functionality to other services.  
  
 Conversations initiated from this service may use any contract. You create a service without specifying contracts when the service only initiates conversations.  
  
 When [!INCLUDE[ssSB](../../includes/sssb-md.md)] accepts a new conversation from a remote service, the name of the target service determines the queue where the broker places messages in the conversation.  
  
## Permissions  
 Permission for creating a service defaults to members of the **db_ddladmin** or **db_owner** fixed database roles and the **sysadmin** fixed server role. The user executing the CREATE SERVICE statement must have REFERENCES permission on the queue and all contracts specified.  
  
 REFERENCES permission for a service defaults to the owner of the service, members of the **db_ddladmin** or **db_owner** fixed database roles, and members of the **sysadmin** fixed server role. SEND permissions for a service default to the owner of the service, members of the **db_owner** fixed database role, and members of the **sysadmin** fixed server role.  
  
 A service may not be a temporary object. Service names beginning with **#** are allowed, but are permanent objects.  
  
## Examples  
  
### A. Creating a service with one contract  
 The following example creates the service `//Adventure-Works.com/Expenses` on the `ExpenseQueue` queue in the `dbo` schema. Dialogs that target this service must follow the contract `//Adventure-Works.com/Expenses/ExpenseSubmission`.  
  
```  
CREATE SERVICE [//Adventure-Works.com/Expenses]  
    ON QUEUE [dbo].[ExpenseQueue]  
    ([//Adventure-Works.com/Expenses/ExpenseSubmission]) ;  
```  
  
### B. Creating a service with multiple contracts  
 The following example creates the service `//Adventure-Works.com/Expenses` on the `ExpenseQueue` queue. Dialogs that target this service must either follow the contract `//Adventure-Works.com/Expenses/ExpenseSubmission` or the contract `//Adventure-Works.com/Expenses/ExpenseProcessing`.  
  
```  
CREATE SERVICE [//Adventure-Works.com/Expenses] ON QUEUE ExpenseQueue  
    ([//Adventure-Works.com/Expenses/ExpenseSubmission],  
     [//Adventure-Works.com/Expenses/ExpenseProcessing]) ;  
```  
  
### C. Creating a service with no contracts  
 The following example creates the service `//Adventure-Works.com/Expenses on the ExpenseQueue` queue. This service has no contract information. Therefore, the service can only be the initiator of a dialog.  
  
```  
CREATE SERVICE [//Adventure-Works.com/Expenses] ON QUEUE ExpenseQueue ;  
```  
  
## See Also  
 [ALTER SERVICE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-service-transact-sql.md)   
 [DROP SERVICE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-service-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
