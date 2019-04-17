---
title: "DROP BROKER PRIORITY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_BROKER_PRIORITY_TSQL"
  - "DROP BROKER PRIORITY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DROP BROKER PRIORITY statement"
ms.assetid: 09ee6c5b-af94-4a4b-a0e2-f9eac50e43aa
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# DROP BROKER PRIORITY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes a conversation priority from the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DROP BROKER PRIORITY ConversationPriorityName  
[;]  
```  
  
## Arguments  
 *ConversationPriorityName*  
 Specifies the name of the conversation priority to be removed.  
  
## Remarks  
 When you drop a conversation priority, any existing conversations continue to operate with the priority levels they were assigned from the conversation priority.  
  
## Permissions  
 Permission for creating a conversation priority defaults to members of the db_ddladmin or db_owner fixed database roles, and to the sysadmin fixed server role. Requires ALTER permission on the database.  
  
## Examples  
 The following example drops the conversation priority named `InitiatorAToTargetPriority`.  
  
```  
DROP BROKER PRIORITY InitiatorAToTargetPriority;  
  
```  
  
## See Also  
 [ALTER BROKER PRIORITY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-broker-priority-transact-sql.md)   
 [CREATE BROKER PRIORITY &#40;Transact-SQL&#41;](../../t-sql/statements/create-broker-priority-transact-sql.md)   
 [sys.conversation_priorities &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-conversation-priorities-transact-sql.md)  
  
  
