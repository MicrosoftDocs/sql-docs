---
title: "QUOTENAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "QUOTENAME_TSQL"
  - "QUOTENAME"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "delimited identifiers [SQL Server]"
  - "input strings [SQL Server]"
  - "Unicode [SQL Server], delimited identifiers"
  - "QUOTENAME function"
  - "valid identifiers [SQL Server]"
ms.assetid: 34d47f1e-2ac7-4890-8c9c-5f60f115e076
caps.latest.revision: 35
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# QUOTENAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a Unicode string with the delimiters added to make the input string a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] delimited identifier.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
QUOTENAME ( 'character_string' [ , 'quote_character' ] )   
```  
  
## Arguments  
 '*character_string*'  
 Is a string of Unicode character data. *character_string* is **sysname** and is limited to 128 characters. Inputs greater than 128 characters return NULL.  
  
 '*quote_character*'  
 Is a one-character string to use as the delimiter. Can be a single quotation mark ( **'** ), a left or right bracket ( **[]** ), or a double quotation mark ( **"** ). If *quote_character* is not specified, brackets are used.  
  
## Return Types  
 **nvarchar(258)**  
  
## Examples  
 The following example takes the character string `abc[]def` and uses the `[` and `]` characters to create a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] delimited identifier.  
  
```  
SELECT QUOTENAME('abc[]def');  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
[abc[]]def]  
  
(1 row(s) affected)  
```  
  
 Notice that the right bracket in the string `abc[]def` is doubled to indicate an escape character.  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example takes the character string `abc def` and uses the `[` and `]` characters to create a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] delimited identifier.  
  
```  
SELECT QUOTENAME('abc def');   
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
[abc def]  
  
(1 row(s) affected)  
```  
  
## See Also  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
  
  

