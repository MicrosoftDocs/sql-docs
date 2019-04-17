---
title: "DROP MESSAGE TYPE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_MESSAGE_TYPE_TSQL"
  - "DROP MESSAGE TYPE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "message types [Service Broker], removing"
  - "deleting message types"
  - "dropping message types"
  - "DROP MESSAGE TYPE statement"
  - "removing message types"
ms.assetid: 805e8ad5-8a93-49f0-88e5-e6fca8814dd5
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# DROP MESSAGE TYPE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Drops an existing message type.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DROP MESSAGE TYPE message_type_name  
[ ; ]  
```  
  
## Arguments  
 *message_type_name*  
 The name of the message type to delete. Server, database, and schema names cannot be specified.  
  
## Permissions  
 Permission for dropping a message type defaults to the owner of the message type, members of the db_ddladmin or db_owner fixed database roles, and members of the sysadmin fixed server role.  
  
## Remarks  
 You cannot drop a message type if any contracts refer to the message type.  
  
## Examples  
 The following example deletes the `//Adventure-Works.com/Expenses/SubmitExpense` message type from the database.  
  
```  
DROP MESSAGE TYPE [//Adventure-Works.com/Expenses/SubmitExpense] ;  
```  
  
## See Also  
 [ALTER MESSAGE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-message-type-transact-sql.md)   
 [CREATE MESSAGE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/create-message-type-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
