---
title: "CREATE CONTRACT (Transact-SQL)"
description: CREATE CONTRACT (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CONTRACT_TSQL"
  - "CREATE_CONTRACT_TSQL"
  - "CREATE CONTRACT"
  - "CONTRACT"
helpviewer_keywords:
  - "CREATE CONTRACT statement"
  - "contracts [Service Broker], creating"
  - "message types [Service Broker], contracts"
dev_langs:
  - "TSQL"
---
# CREATE CONTRACT (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Creates a new contract. A contract defines the message types that are used in a [!INCLUDE[ssSB](../../includes/sssb-md.md)] conversation and also determines which side of the conversation can send messages of that type. Each conversation follows a contract. The initiating service specifies the contract for the conversation when the conversation starts. The target service specifies the contracts that the target service accepts conversations for.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
CREATE CONTRACT contract_name  
   [ AUTHORIZATION owner_name ]  
      (  {   { message_type_name | [ DEFAULT ] }  
          SENT BY { INITIATOR | TARGET | ANY }   
       } [ ,...n] )   
[ ; ]  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *contract_name*  
 Is the name of the contract to create. A new contract is created in the current database and owned by the principal specified in the AUTHORIZATION clause. Server, database, and schema names cannot be specified. The *contract_name* can be up to 128 characters.  
  
> [!NOTE]  
>  Do not create a contract that uses the keyword ANY for the *contract_name*. When you specify ANY for a contract name in CREATE BROKER PRIORITY, the priority is considered for all contracts. It is not limited to a contract whose name is ANY.  
  
 AUTHORIZATION *owner_name*  
 Sets the owner of the contract to the specified database user or role. When the current user is **dbo** or **sa**, *owner_name* can be the name of any valid user or role. Otherwise, *owner_name* must be the name of the current user, the name of a user that the current user has impersonate permissions for, or the name of a role to which the current user belongs. When this clause is omitted, the contract belongs to the current user.  
  
 *message_type_name*  
 Is the name of a message type to be included as part of the contract.  
  
 SENT BY  
 Specifies which endpoint can send a message of the indicated message type. Contracts document the messages that services can use to have specific conversations. Each conversation has two endpoints: the *initiator* endpoint, the service that started the conversation, and the *target* endpoint, the service that the initiator is contacting.  
  
 INITIATOR  
 Indicates that only the initiator of the conversation can send messages of the specified message type. A service that starts a conversation is referred to as the *initiator* of the conversation.  
  
 TARGET  
 Indicates that only the target of the conversation can send messages of the specified message type. A service that accepts a conversation that was started by another service is referred to as the *target* of the conversation.  
  
 ANY  
 Indicates that messages of this type can be sent by both the initiator and the target.  
  
 [ DEFAULT ]  
 Indicates that this contract supports messages of the default message type. By default, all databases contain a message type named DEFAULT. This message type uses a validation of NONE. In the context of this clause, DEFAULT is not a keyword, and must be delimited as an identifier. Microsoft SQL Server also provides a DEFAULT contract which specifies the DEFAULT message type.  
  
## Remarks  
 The order of message types in the contract is not significant. After the target has received the first message, [!INCLUDE[ssSB](../../includes/sssb-md.md)] allows either side of the conversation to send any message allowed for that side of the conversation at any time. For example, if the initiator of the conversation can send the message type **//Adventure-Works.com/Expenses/SubmitExpense**, [!INCLUDE[ssSB](../../includes/sssb-md.md)] allows the initiator to send any number of **SubmitExpense** messages during the conversation.  
  
 The message types and directions in a contract cannot be changed. To change the AUTHORIZATION for a contract, use the ALTER AUTHORIZATION statement.  
  
 A contract must allow the initiator to send a message. The CREATE CONTRACT statement fails when the contract does not contain at least one message type that is SENT BY ANY or SENT BY INITIATOR.  
  
 Regardless of the contract, a service can always receive the message types `https://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer`, `https://schemas.microsoft.com/SQL/ServiceBroker/Error`, and `https://schemas.microsoft.com/SQL/ServiceBroker/EndDialog`. [!INCLUDE[ssSB](../../includes/sssb-md.md)] uses these message types for system messages to the application.  
  
 A contract cannot be a temporary object. Contract names starting with # are permitted, but are permanent objects.  
  
## Permissions  
 By default, members of the **db_ddladmin** or **db_owner** fixed database roles and the **sysadmin** fixed server role can create contracts.  
  
 By default, the owner of the contract, members of the **db_ddladmin** or **db_owner** fixed database roles, and members of the **sysadmin** fixed server role have REFERENCES permission on a contract.  
  
 The user executing the CREATE CONTRACT statement must have REFERENCES permission on all message types specified.  
  
## Examples  
 **A. Creating a contract**  
  
 The following example creates an expense reimbursement contract based on three message types.  
  
```sql  
CREATE MESSAGE TYPE  
    [//Adventure-Works.com/Expenses/SubmitExpense]           
    VALIDATION = WELL_FORMED_XML ;           
  
CREATE MESSAGE TYPE  
    [//Adventure-Works.com/Expenses/ExpenseApprovedOrDenied]           
    VALIDATION = WELL_FORMED_XML ;           
  
CREATE MESSAGE TYPE           
    [//Adventure-Works.com/Expenses/ExpenseReimbursed]           
    VALIDATION= WELL_FORMED_XML ;           
  
CREATE CONTRACT            
    [//Adventure-Works.com/Expenses/ExpenseSubmission]           
    ( [//Adventure-Works.com/Expenses/SubmitExpense]           
          SENT BY INITIATOR,           
      [//Adventure-Works.com/Expenses/ExpenseApprovedOrDenied]           
          SENT BY TARGET,           
      [//Adventure-Works.com/Expenses/ExpenseReimbursed]           
          SENT BY TARGET           
    ) ;  
```  
  
## See Also  
 [DROP CONTRACT &#40;Transact-SQL&#41;](../../t-sql/statements/drop-contract-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
