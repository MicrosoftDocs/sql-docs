---
title: "Slash Star (Block Comment) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/27/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "/*...*/_TSQL"
  - "Comment"
  - "/*...*/"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "nonexecuting text strings [SQL Server]"
  - "/*...*/ (comment)"
  - "remarks [SQL Server]"
  - "comments [SQL Server]"
ms.assetid: 4d9ab1b2-4bbb-4c16-beb1-cafc1af7417c
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---

# Slash Star (Block Comment) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]


  Indicates user-provided text. The text between the /* and \*/ is not evaluated by the server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
/*  
text_of_comment  
*/  
```  
  
## Arguments  
 *text_of_comment*  
 Is the text of the comment. This is one or more character strings.  
  
## Remarks  
 Comments can be inserted on a separate line or within a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. Multiple-line comments must be indicated by /* and \*/. A stylistic convention often used for multiple-line comments is to begin the first line with /\*, subsequent lines with \*\*, and end with \*/.  
  
 There is no maximum length for comments.  
  
 Nested comments are supported. If the /* character pattern occurs anywhere within an existing comment, it is treated as the start of a nested comment and, therefore, requires a closing \*/ comment mark. If the closing comment mark does not exist, an error is generated.  
  
 For example, the following code generates an error.  
  
```  
DECLARE @comment AS varchar(20);  
GO  
/*  
SELECT @comment = '/*';  
*/   
SELECT @@VERSION;  
GO   
```  
  
 To work around this error, make the following change.  
  
```  
DECLARE @comment AS varchar(20);  
GO  
/*  
SELECT @comment = '/*';  
*/ */  
SELECT @@VERSION;  
GO  
  
```  
  
## Examples  
 The following example uses comments to explain what the section of the code is supposed to do.  
  
```  
USE AdventureWorks2012;  
GO  
/*  
This section of the code joins the Person table with the Address table,   
by using the Employee and BusinessEntityAddress tables in the middle to   
get a list of all the employees in the AdventureWorks2012 database   
and their contact information.  
*/  
SELECT p.FirstName, p.LastName, a.AddressLine1, a.AddressLine2, a.City, a.PostalCode  
FROM Person.Person AS p  
JOIN HumanResources.Employee AS e ON p.BusinessEntityID = e.BusinessEntityID   
JOIN Person.BusinessEntityAddress AS ea ON e.BusinessEntityID = ea.BusinessEntityID  
JOIN Person.Address AS a ON ea.AddressID = a.AddressID;  
GO  
```  
  
## See Also  
 [-- &#40;Comment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/comment-transact-sql.md)   
 [Control-of-Flow Language &#40;Transact-SQL&#41;](~/t-sql/language-elements/control-of-flow.md)  
  
  

