---
title: -- (Comment) (Transact-SQL)
description: "-- (Comment) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "07/25/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "--_TSQL"
  - "Comment"
  - "--"
helpviewer_keywords:
  - "nonexecuting text strings [SQL Server]"
  - "remarks [SQL Server]"
  - "-- (comment character)"
  - "comments [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# -- (Comment) (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Indicates user-provided text. Comments can be inserted on a separate line, nested at the end of a [!INCLUDE[tsql](../../includes/tsql-md.md)] command line, or within a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. The server does not evaluate the comment.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- text_of_comment  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *text_of_comment*  
 Is the character string that contains the text of the comment.  
  
## Remarks  
Use two hyphens (**--**) for single-line or nested comments. Comments inserted with **--** are terminated by a new line, which is specified with a carriage return character (U+000A), line feed character (U+000D), or a combination of the two. There is no maximum length for comments. The following table lists the keyboard shortcuts that you can use to comment or uncomment text.
  
|Action|Standard|  
|------------|--------------|  
|Make the selected text a comment|CTRL+K, CTRL+C|  
|Uncomment the selected text|CTRL+K, CTRL+U|  
  
 For more information about keyboard shortcuts, see [SQL Server Management Studio Keyboard Shortcuts](../../ssms/sql-server-management-studio-keyboard-shortcuts.md).  
  
 For multiline comments, see [Slash Star &#40;Block Comment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/slash-star-comment-transact-sql.md).  
  
## Examples  
 The following example uses the -- commenting characters.  
  
```sql  
-- Choose the AdventureWorks2012 database.  
USE AdventureWorks2012;  
GO  
-- Choose all columns and all rows from the Address table.  
SELECT *  
FROM Person.Address  
ORDER BY PostalCode ASC; -- We do not have to specify ASC because   
-- that is the default.  
GO  
```  
  
## See Also  
 [Control-of-Flow Language &#40;Transact-SQL&#41;](~/t-sql/language-elements/control-of-flow.md)  
  
  
