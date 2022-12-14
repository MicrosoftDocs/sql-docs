---
title: "CREATE BROKER PRIORITY (Transact-SQL)"
description: CREATE BROKER PRIORITY (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CREATE BROKER PRIORITY"
  - "PRIORITY_TSQL"
  - "CREATE_BROKER_PRIORITY_TSQL"
  - "PRIORITY"
  - "CREATE BROKER"
  - "BROKER_TSQL"
  - "BROKER"
  - "CREATE_BROKER_TSQL"
  - "BROKER PRIORITY"
  - "BROKER_PRIORITY_TSQL"
helpviewer_keywords:
  - "CREATE BROKER PRIORITY statement"
dev_langs:
  - "TSQL"
---
# CREATE BROKER PRIORITY (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Defines a priority level and the set of criteria for determining which [!INCLUDE[ssSB](../../includes/sssb-md.md)] conversations to assign the priority level. The priority level is assigned to any conversation endpoint that uses the same combination of contracts and services that are specified in the conversation priority. Priorities range in value from 1 (low) to 10 (high). The default is 5.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
CREATE BROKER PRIORITY ConversationPriorityName  
FOR CONVERSATION  
[ SET ( [ CONTRACT_NAME = {ContractName | ANY } ]  
        [ [ , ] LOCAL_SERVICE_NAME = {LocalServiceName | ANY } ]  
        [ [ , ] REMOTE_SERVICE_NAME = {'RemoteServiceName' | ANY } ]  
        [ [ , ] PRIORITY_LEVEL = {PriorityValue | DEFAULT } ]  
       )  
]  
[;]  
  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *ConversationPriorityName*  
 Specifies the name for this conversation priority. The name must be unique in the current database, and must conform to the rules for [!INCLUDE[ssDE](../../includes/ssde-md.md)] [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 SET  
 Specifies the criteria for determining if the conversation priority applies to a conversation. If specified, SET must contain at least one criterion: CONTRACT_NAME, LOCAL_SERVICE_NAME, REMOTE_SERVICE_NAME, or PRIORITY_LEVEL. If SET is not specified, the defaults are set for all three criteria.  
  
 CONTRACT_NAME = {*ContractName* | **ANY**}  
 Specifies the name of a contract to be used as a criterion for determining if the conversation priority applies to a conversation. *ContractName* is a [!INCLUDE[ssDE](../../includes/ssde-md.md)] identifier, and must specify the name of a contract in the current database.  
  
 *ContractName*  
 Specifies that the conversation priority can be applied only to conversations where the BEGIN DIALOG statement that started the conversation specified ON CONTRACT *ContractName*.  
  
 ANY  
 Specifies that the conversation priority can be applied to any conversation, regardless of which contract it uses.  
  
 The default is ANY.  
  
 LOCAL_SERVICE_NAME = {*LocalServiceName* | **ANY**}  
 Specifies the name of a service to be used as a criterion to determine if the conversation priority applies to a conversation endpoint.  
  
 *LocalServiceName* is a [!INCLUDE[ssDE](../../includes/ssde-md.md)] identifier. It must specify the name of a service in the current database.  
  
 *LocalServiceName*  
 Specifies that the conversation priority can be applied to the following:  
  
-   Any initiator conversation endpoint whose initiator service name matches *LocalServiceName*.  
  
-   Any target conversation endpoint whose target service name matches *LocalServiceName*.  
  
 ANY  
 -   Specifies that the conversation priority can be applied to any conversation endpoint, regardless of the name of the local service used by the endpoint.  
  
 The default is ANY.  
  
 REMOTE_SERVICE_NAME = {'*RemoteServiceName*' | **ANY**}  
 Specifies the name of a service to be used as a criterion to determine if the conversation priority applies to a conversation endpoint.  
  
 *RemoteServiceName* is a literal of type **nvarchar(256)**. [!INCLUDE[ssSB](../../includes/sssb-md.md)] uses a byte-by-byte comparison to match the *RemoteServiceName* string. The comparison is case-sensitive and does not consider the current collation. The target service can be in the current instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], or a remote instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
 '*RemoteServiceName*'  
 Specifies that the conversation priority can be applied to the following:  
  
-   Any initiator conversation endpoint whose associated target service name matches *RemoteServiceName*.  
  
-   Any target conversation endpoint whose associated initiator service name matches *RemoteServiceName*.  
  
 ANY  
 Specifies that the conversation priority can be applied to any conversation endpoint, regardless of the name of the remote service associated with the endpoint.  
  
 The default is ANY.  
  
 PRIORITY_LEVEL = { *PriorityValue* | **DEFAULT** }  
 Specifies the priority to assign any conversation endpoint that use the contracts and services specified in the conversation priority. *PriorityValue* must be an integer literal from 1 (lowest priority) to 10 (highest priority). The default is 5.  
  
## Remarks  
 [!INCLUDE[ssSB](../../includes/sssb-md.md)] assigns priority levels to conversation endpoints. The priority levels control the priority of the operations associated with the endpoint. Each conversation has two conversation endpoints:  
  
-   The initiator conversation endpoint associates one side of the conversation with the initiator service and initiator queue. The initiator conversation endpoint is created when the BEGIN DIALOG statement is run. The operations associated with the initiator conversation endpoint include:  
  
    -   Sends from the initiator service.  
  
    -   Receives from the initiator queue.  
  
    -   Getting the next conversation group from the initiator queue.  
  
-   The target conversation endpoint associates the other side of the conversation with the target service and queue. The target conversation endpoint is created when the conversation is used to send a message to the target queue. The operations associated with the target conversation endpoint include:  
  
    -   Receives from the target queue.  
  
    -   Sends from the target service.  
  
    -   Getting the next conversation group from the target queue.  
  
 [!INCLUDE[ssSB](../../includes/sssb-md.md)] assigns conversation priority levels when conversation endpoints are created. The conversation endpoint retains the priority level until the conversation ends. New priorities or changes to existing priorities are not applied to existing conversations.  
  
 [!INCLUDE[ssSB](../../includes/sssb-md.md)] assigns a conversation endpoint the priority level from the conversation priority whose contract and services criteria best match the properties of the endpoint. The following table shows the match precedence:  
  
|Operation contract|Operation local service|Operation remote service|  
|------------------------|-----------------------------|------------------------------|  
|*ContractName*|*LocalServiceName*|*RemoteServiceName*|  
|*ContractName*|*LocalServiceName*|ANY|  
|*ContractName*|ANY|*RemoteServiceName*|  
|*ContractName*|ANY|ANY|  
|ANY|*LocalServiceName*|*RemoteServiceName*|  
|ANY|*LocalServiceName*|ANY|  
|ANY|ANY|*RemoteServiceName*|  
|ANY|ANY|ANY|  
  
 [!INCLUDE[ssSB](../../includes/sssb-md.md)] first looks for a priority whose specified contract, local service, and remote service matches those that the operation uses. If one is not found, [!INCLUDE[ssSB](../../includes/sssb-md.md)] looks for a priority with a contract and local service that matches those that the operation uses, and where the remote service was specified as ANY. This continues for all the variations that are listed in the precedence table. If no match is found, the operation is assigned the default priority of 5.  
  
 [!INCLUDE[ssSB](../../includes/sssb-md.md)] independently assigns a priority level to each conversation endpoint. To have [!INCLUDE[ssSB](../../includes/sssb-md.md)] assign priority levels to both the initiator and target conversation endpoints, you must ensure that both endpoints are covered by conversation priorities. If the initiator and target conversation endpoints are in separate databases, you must create conversation priorities in each database. The same priority level is usually specified for both of the conversation endpoints for a conversation, but you can specify different priority levels.  
  
 Priority levels are always applied to operations that receive messages or conversation group identifiers from a queue. Priority levels are also applied when transmitting messages from one instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to another.  
  
 Priority levels are not used when transmitting messages:  
  
-   From a database where the HONOR_BROKER_PRIORITY database option is set to OFF. For more information, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md).  
  
-   Between services in the same instance of the Database Engine.  
  
-   All [!INCLUDE[ssSB](../../includes/sssb-md.md)] operations in a database are assigned default priorities of 5 if no conversation priorities have been created in the database.  
  
## Permissions  
 Permission for creating a conversation priority defaults to members of the db_ddladmin or db_owner fixed database roles, and to the sysadmin fixed server role. Requires ALTER permission on the database.  
  
## Examples  
  
### A. Assigning a priority level to both directions of a conversation.  
 These two conversation priorities ensure that all operations that use `SimpleContract` between `TargetService` and the `InitiatorAService` are assigned priority level 3.  
  
```sql  
CREATE BROKER PRIORITY InitiatorAToTargetPriority  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = SimpleContract,  
         LOCAL_SERVICE_NAME = InitiatorServiceA,  
         REMOTE_SERVICE_NAME = N'TargetService',  
         PRIORITY_LEVEL = 3);  
CREATE BROKER PRIORITY TargetToInitiatorAPriority  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = SimpleContract,  
         LOCAL_SERVICE_NAME = TargetService,  
         REMOTE_SERVICE_NAME = N'InitiatorServiceA',  
         PRIORITY_LEVEL = 3);  
```  
  
### B. Setting the priority level for all conversations that use a contract  
 Assigns a priority level of `7` to all operations that use a contract named `SimpleContract`. This assumes that there are no other priorities that specify both `SimpleContract` and either a local or a remote service.  
  
```sql 
CREATE BROKER PRIORITY SimpleContractDefaultPriority  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = SimpleContract,  
         LOCAL_SERVICE_NAME = ANY,  
         REMOTE_SERVICE_NAME = ANY,  
         PRIORITY_LEVEL = 7);  
```  
  
### C. Setting a base priority level for a database.  
 Defines conversation priorities for two specific services, and then defines a conversation priority that will match all other conversation endpoints. This does not replace the default priority, which is always 5, but does minimize the number of items that are assigned the default.  
  
```sql 
CREATE BROKER PRIORITY [//Adventure-Works.com/Expenses/ClaimPriority]  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = ANY,  
         LOCAL_SERVICE_NAME = //Adventure-Works.com/Expenses/ClaimService,  
         REMOTE_SERVICE_NAME = ANY,  
         PRIORITY_LEVEL = 9);  
CREATE BROKER PRIORITY [//Adventure-Works.com/Expenses/ApprovalPriority]  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = ANY,  
         LOCAL_SERVICE_NAME = //Adventure-Works.com/Expenses/ClaimService,  
         REMOTE_SERVICE_NAME = ANY,  
         PRIORITY_LEVEL = 6);  
CREATE BROKER PRIORITY [//Adventure-Works.com/Expenses/BasePriority]  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = ANY,  
         LOCAL_SERVICE_NAME = ANY,  
         REMOTE_SERVICE_NAME = ANY,  
         PRIORITY_LEVEL = 3);  
```  
  
### D. Creating three priority levels for a target service by using services  
 Supports a system that provides three levels of performance: Gold (high), Silver (medium), and Bronze (low). There is one contract, but each level has a separate initiator service. All initiator services communicate to a central target service.  
  
```sql  
CREATE BROKER PRIORITY GoldInitToTargetPriority  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = SimpleContract,  
         LOCAL_SERVICE_NAME = GoldInitiatorService,  
         REMOTE_SERVICE_NAME = N'TargetService',  
         PRIORITY_LEVEL = 6);  
CREATE BROKER PRIORITY GoldTargetToInitPriority  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = SimpleContract,  
         LOCAL_SERVICE_NAME = TargetService,  
         REMOTE_SERVICE_NAME = N'GoldInitiatorService',  
         PRIORITY_LEVEL = 6);  
CREATE BROKER PRIORITY SilverInitToTargetPriority  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = SimpleContract,  
         LOCAL_SERVICE_NAME = SilverInitiatorService,  
         REMOTE_SERVICE_NAME = N'TargetService',  
         PRIORITY_LEVEL = 4);  
CREATE BROKER PRIORITY SilverTargetToInitPriority  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = SimpleContract,  
         LOCAL_SERVICE_NAME = TargetService,  
         REMOTE_SERVICE_NAME = N'SilverInitiatorService',  
         PRIORITY_LEVEL = 4);  
CREATE BROKER PRIORITY BronzeInitToTargetPriority  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = SimpleContract,  
         LOCAL_SERVICE_NAME = BronzeInitiatorService,  
         REMOTE_SERVICE_NAME = N'TargetService',  
         PRIORITY_LEVEL = 2);  
CREATE BROKER PRIORITY BronzeTargetToInitPriority  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = SimpleContract,  
         LOCAL_SERVICE_NAME = TargetService,  
         REMOTE_SERVICE_NAME = N'BronzeInitiatorService',  
         PRIORITY_LEVEL = 2);  
```  
  
### E. Creating three priority levels for multiple services using contracts  
 Supports a system that provides three levels of performance: Gold (high), Silver (medium), and Bronze (low). Each level has a separate contract. These priorities apply to any services that are referenced by conversations that use the contracts.  
  
```sql  
CREATE BROKER PRIORITY GoldPriority  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = GoldContract,  
         LOCAL_SERVICE_NAME = ANY,  
         REMOTE_SERVICE_NAME = ANY,  
         PRIORITY_LEVEL = 6);  
CREATE BROKER PRIORITY SilverPriority  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = SilverContract,  
         LOCAL_SERVICE_NAME = ANY,  
         REMOTE_SERVICE_NAME = ANY,  
         PRIORITY_LEVEL = 4);  
CREATE BROKER PRIORITY BronzePriority  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = BronzeContract,  
         LOCAL_SERVICE_NAME = ANY,  
         REMOTE_SERVICE_NAME = ANY,  
         PRIORITY_LEVEL = 2);  
```  
  
## See Also  
 [ALTER BROKER PRIORITY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-broker-priority-transact-sql.md)   
 [BEGIN DIALOG CONVERSATION &#40;Transact-SQL&#41;](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)   
 [CREATE CONTRACT &#40;Transact-SQL&#41;](../../t-sql/statements/create-contract-transact-sql.md)   
 [CREATE QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/create-queue-transact-sql.md)   
 [CREATE SERVICE &#40;Transact-SQL&#41;](../../t-sql/statements/create-service-transact-sql.md)   
 [DROP BROKER PRIORITY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-broker-priority-transact-sql.md)   
 [GET CONVERSATION GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/get-conversation-group-transact-sql.md)   
 [RECEIVE &#40;Transact-SQL&#41;](../../t-sql/statements/receive-transact-sql.md)   
 [SEND &#40;Transact-SQL&#41;](../../t-sql/statements/send-transact-sql.md)   
 [sys.conversation_priorities &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-conversation-priorities-transact-sql.md)  
  
  
