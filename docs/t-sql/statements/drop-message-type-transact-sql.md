---
title: "DROP MESSAGE TYPE (Transact-SQL)"
description: DROP MESSAGE TYPE (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP_MESSAGE_TYPE_TSQL"
  - "DROP MESSAGE TYPE"
helpviewer_keywords:
  - "message types [Service Broker], removing"
  - "deleting message types"
  - "dropping message types"
  - "DROP MESSAGE TYPE statement"
  - "removing message types"
dev_langs:
  - "TSQL"
---
# DROP MESSAGE TYPE (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

  Drops an existing message type.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP MESSAGE TYPE message_type_name  
[ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *message_type_name*  
 The name of the message type to delete. Server, database, and schema names cannot be specified.  
  
## Permissions  
 Permission for dropping a message type defaults to the owner of the message type, members of the db_ddladmin or db_owner fixed database roles, and members of the sysadmin fixed server role.  
  
## Remarks  
 You cannot drop a message type if any contracts refer to the message type.  
  
## Examples  
 The following example deletes the `//Adventure-Works.com/Expenses/SubmitExpense` message type from the database.  
  
```sql  
DROP MESSAGE TYPE [//Adventure-Works.com/Expenses/SubmitExpense] ;  
```  
  
## See Also  
 [ALTER MESSAGE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-message-type-transact-sql.md)   
 [CREATE MESSAGE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/create-message-type-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
