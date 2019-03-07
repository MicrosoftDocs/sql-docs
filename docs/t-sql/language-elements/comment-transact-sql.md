---
title: "-- (Comment) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "--_TSQL"
  - "Comment"
  - "--"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "nonexecuting text strings [SQL Server]"
  - "remarks [SQL Server]"
  - "-- (comment character)"
  - "comments [SQL Server]"
ms.assetid: 676ea8c2-52c1-4ef6-9354-320f1a091153
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# -- (Comment) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Indicates user-provided text. Comments can be inserted on a separate line, nested at the end of a [!INCLUDE[tsql](../../includes/tsql-md.md)] command line, or within a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. The server does not evaluate the comment.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
-- text_of_comment  
```  
  
## Arguments  
 *text_of_comment*  
 Is the character string that contains the text of the comment.  
  
## Remarks  
 Use two hyphens (--) for single-line or nested comments. Comments inserted with -- are terminated by the newline character. There is no maximum length for comments. The following table lists the keyboard shortcuts that you can use to comment or uncomment text.  
  
|Action|Standard|  
|------------|--------------|  
|Make the selected text a comment|CTRL+K, CTRL+C|  
|Uncomment the selected text|CTRL+K, CTRL+U|  
  
 For more information about keyboard shortcuts, see [SQL Server Management Studio Keyboard Shortcuts](../../tools/sql-server-management-studio/sql-server-management-studio-keyboard-shortcuts.md).  
  
 For multiline comments, see [Slash Star &#40;Block Comment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/slash-star-comment-transact-sql.md).  
  
## Examples  
 The following example uses the -- commenting characters.  
  
```  
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
  
  
