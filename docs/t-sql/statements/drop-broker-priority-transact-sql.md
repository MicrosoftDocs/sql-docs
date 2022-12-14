---
title: "DROP BROKER PRIORITY (Transact-SQL)"
description: DROP BROKER PRIORITY (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP_BROKER_PRIORITY_TSQL"
  - "DROP BROKER PRIORITY"
helpviewer_keywords:
  - "DROP BROKER PRIORITY statement"
dev_langs:
  - "TSQL"
---
# DROP BROKER PRIORITY (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes a conversation priority from the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP BROKER PRIORITY ConversationPriorityName  
[;]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *ConversationPriorityName*  
 Specifies the name of the conversation priority to be removed.  
  
## Remarks  
 When you drop a conversation priority, any existing conversations continue to operate with the priority levels they were assigned from the conversation priority.  
  
## Permissions  
 Permission for creating a conversation priority defaults to members of the db_ddladmin or db_owner fixed database roles, and to the sysadmin fixed server role. Requires ALTER permission on the database.  
  
## Examples  
 The following example drops the conversation priority named `InitiatorAToTargetPriority`.  
  
```sql  
DROP BROKER PRIORITY InitiatorAToTargetPriority;
```  
  
## See Also  
 [ALTER BROKER PRIORITY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-broker-priority-transact-sql.md)   
 [CREATE BROKER PRIORITY &#40;Transact-SQL&#41;](../../t-sql/statements/create-broker-priority-transact-sql.md)   
 [sys.conversation_priorities &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-conversation-priorities-transact-sql.md)  
  
  
