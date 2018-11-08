---
title: "ALTER BROKER PRIORITY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_BROKER_TSQL"
  - "ALTER BROKER PRIORITY"
  - "ALTER BROKER"
  - "ALTER_BROKER_PRIORITY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ALTER BROKER PRIORITY statement"
  - "ssbdiagnose"
ms.assetid: 15fda1b2-e4dd-4f9d-935a-2e38926075b2
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER BROKER PRIORITY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the properties of a [!INCLUDE[ssSB](../../includes/sssb-md.md)] conversation priority.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ALTER BROKER PRIORITY ConversationPriorityName  
FOR CONVERSATION  
{ SET ( [ CONTRACT_NAME = {ContractName | ANY } ]  
        [ [ , ] LOCAL_SERVICE_NAME = {LocalServiceName | ANY } ]  
        [ [ , ] REMOTE_SERVICE_NAME = {'RemoteServiceName' | ANY } ]  
        [ [ , ] PRIORITY_LEVEL = { PriorityValue | DEFAULT } ]  
              )  
}  
[;]  
  
```  
  
## Arguments  
 *ConversationPriorityName*  
 Specifies the name of the conversation priority to be changed. The name must refer to a conversation priority in the current database.  
  
 SET  
 Specifies the criteria for determining if the conversation priority applies to a conversation. SET is required and must contain at least one criterion: CONTRACT_NAME, LOCAL_SERVICE_NAME, REMOTE_SERVICE_NAME, or PRIORITY_LEVEL.  
  
 CONTRACT_NAME = {*ContractName* | **ANY**}  
 Specifies the name of a contract to be used as a criterion for determining if the conversation priority applies to a conversation. *ContractName* is a [!INCLUDE[ssDE](../../includes/ssde-md.md)] identifier, and must specify the name of a contract in the current database.  
  
 *ContractName*  
 Specifies that the conversation priority can be applied only to conversations where the BEGIN DIALOG statement that started the conversation specified ON CONTRACT *ContractName*.  
  
 ANY  
 Specifies that the conversation priority can be applied to any conversation, regardless of which contract it uses.  
  
 If CONTRACT_NAME is not specified, the contract property of the conversation priority is not changed.  
  
 LOCAL_SERVICE_NAME = {*LocalServiceName* | **ANY**}  
 Specifies the name of a service to be used as a criterion to determine if the conversation priority applies to a conversation endpoint.  
  
 *LocalServiceName* is a [!INCLUDE[ssDE](../../includes/ssde-md.md)] identifier and must specify the name of a service in the current database.  
  
 *LocalServiceName*  
 Specifies that the conversation priority can be applied to the following:  
  
-   Any initiator conversation endpoint whose initiator service name matches *LocalServiceName*.  
  
-   Any target conversation endpoint whose target service name matches *LocalServiceName*.  
  
 ANY  
 -   Specifies that the conversation priority can be applied to any conversation endpoint, regardless of the name of the local service used by the endpoint.  
  
 If LOCAL_SERVICE_NAME is not specified, the local service property of the conversation priority is not changed.  
  
 REMOTE_SERVICE_NAME = {'*RemoteServiceName*' | **ANY**}  
 Specifies the name of a service to be used as a criterion to determine if the conversation priority applies to a conversation endpoint.  
  
 *RemoteServiceName* is a literal of type **nvarchar(256)**. [!INCLUDE[ssSB](../../includes/sssb-md.md)] uses a byte-by-byte comparison to match the *RemoteServiceName* string. The comparison is case-sensitive and does not consider the current collation. The target service can be in the current instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], or a remote instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
 '*RemoteServiceName*'  
 Specifies the conversation priority be assigned to the following:  
  
-   Any initiator conversation endpoint whose associated target service name matches *RemoteServiceName*.  
  
-   Any target conversation endpoint whose associated initiator service name matches *RemoteServiceName*.  
  
 ANY  
 Specifies that the conversation priority applies to any conversation endpoint, regardless of the name of the remote service associated with the endpoint.  
  
 If REMOTE_SERVICE_NAME is not specified, the remote service property of the conversation priority is not changed.  
  
 PRIORITY_LEVEL = { *PriorityValue* | **DEFAULT** }  
 Specifies the priority level to assign any conversation endpoint that use the contracts and services that are specified in the conversation priority. *PriorityValue* must be an integer literal from 1 (lowest priority) to 10 (highest priority).  
  
 If PRIORITY_LEVEL is not specified, the priority level property of the conversation priority is not changed.  
  
## Remarks  
 No properties that are changed by ALTER BROKER PRIORITY are applied to existing conversations. The existing conversations continue with the priority that was assigned when they were started.  
  
 For more information, see [CREATE BROKER PRIORITY &#40;Transact-SQL&#41;](../../t-sql/statements/create-broker-priority-transact-sql.md).  
  
## Permissions  
 Permission for creating a conversation priority defaults to members of the **db_ddladmin** or **db_owner** fixed database roles, and to the **sysadmin** fixed server role. Requires ALTER permission on the database.  
  
## Examples  
  
### A. Changing only the priority level of an existing conversation priority.  
 Changes the priority level, but does not change the contract, local service, or remote service properties.  
  
```  
ALTER BROKER PRIORITY SimpleContractDefaultPriority  
    FOR CONVERSATION  
    SET (PRIORITY_LEVEL = 3);  
```  
  
### B. Changing all of the properties of an existing conversation priority.  
 Changes the priority level, contract, local service, and remote service properties.  
  
```  
ALTER BROKER PRIORITY SimpleContractPriority  
    FOR CONVERSATION  
    SET (CONTRACT_NAME = SimpleContractB,  
         LOCAL_SERVICE_NAME = TargetServiceB,  
         REMOTE_SERVICE_NAME = N'InitiatorServiceB',  
         PRIORITY_LEVEL = 8);  
```  
  
## See Also  
 [CREATE BROKER PRIORITY &#40;Transact-SQL&#41;](../../t-sql/statements/create-broker-priority-transact-sql.md)   
 [DROP BROKER PRIORITY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-broker-priority-transact-sql.md)   
 [sys.conversation_priorities &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-conversation-priorities-transact-sql.md)  
  
  
