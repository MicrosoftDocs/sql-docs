---
title: "ALTER MESSAGE TYPE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_MESSAGE_TYPE_TSQL"
  - "ALTER MESSAGE TYPE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ALTER MESSAGE TYPE statement"
  - "modifying message types"
  - "message types [Service Broker], modifying"
ms.assetid: 98c94176-2bdf-4725-b4bc-d33b6b14817d
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER MESSAGE TYPE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the properties of a message type.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ALTER MESSAGE TYPE message_type_name  
   VALIDATION =  
    {  NONE   
     | EMPTY   
     | WELL_FORMED_XML   
     | VALID_XML WITH SCHEMA COLLECTION schema_collection_name }  
[ ; ]  
```  
  
## Arguments  
 *message_type_name*  
 The name of the message type to change. Server, database, and schema names cannot be specified.  
  
 VALIDATION  
 Specifies how [!INCLUDE[ssSB](../../includes/sssb-md.md)] validates the message body for messages of this type.  
  
 NONE  
 No validation is performed. The message body might contain any data, or might be NULL.  
  
 EMPTY  
 The message body must be NULL.  
  
 WELL_FORMED_XML  
 The message body must contain well-formed XML.  
  
 VALID_XML_WITH_SCHEMA = *schema_collection_name*  
 The message body must contain XML that complies with a schema in the specified schema collection. The *schema_collection_name* must be the name of an existing XML schema collection.  
  
## Remarks  
 Changing the validation of a message type does not affect messages that have already been delivered to a queue.  
  
 To change the AUTHORIZATION for a message type, use the ALTER AUTHORIZATION statement.  
  
## Permissions  
 Permission for altering a message type defaults to the owner of the message type, members of the **db_ddladmin** or **db_owner** fixed database roles, and members of the **sysadmin** fixed server role.  
  
 When the ALTER MESSAGE TYPE statement specifies a schema collection, the user executing the statement must have REFERENCES permission on the schema collection specified.  
  
## Examples  
 The following example changes the message type `//Adventure-Works.com/Expenses/SubmitExpense` to require that the message body contain a well-formed XML document.  
  
```  
ALTER MESSAGE TYPE  
    [//Adventure-Works.com/Expenses/SubmitExpense]  
    VALIDATION = WELL_FORMED_XML ;  
```  
  
## See Also  
 [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md)   
 [CREATE MESSAGE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/create-message-type-transact-sql.md)   
 [DROP MESSAGE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-message-type-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
