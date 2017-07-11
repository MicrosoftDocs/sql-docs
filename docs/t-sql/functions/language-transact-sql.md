---
title: "@@LANGUAGE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "@@LANGUAGE_TSQL"
  - "@@LANGUAGE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "languages [SQL Server], current in use"
  - "@@LANGUAGE function"
  - "current language in use"
  - "names [SQL Server], language in use"
ms.assetid: 3e13b477-7dfa-4da6-9948-da2050d42527
caps.latest.revision: 21
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# @@LANGUAGE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the name of the language currently being used.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
@@LANGUAGE  
```  
  
## Return Types  
 **nvarchar**  
  
## Remarks  
 To view information about language settings, including valid official language names, run **sp_helplanguage** without a parameter specified.  
  
## Examples  
 The following example returns the language for the current session.  
  
```  
SELECT @@LANGUAGE AS 'Language Name';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Language Name                   
------------------------------  
us_english                      
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example returns the language for the current session.  
  
```  
SELECT @@LANGUAGE AS 'Language Name';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Language Name                   
------------------------------  
us_english                      
```  
  
## See Also  
 [Configuration Functions &#40;Transact-SQL&#41;](../../t-sql/functions/configuration-functions-transact-sql.md)   
 [SET LANGUAGE &#40;Transact-SQL&#41;](../../t-sql/statements/set-language-transact-sql.md)   
 [sp_helplanguage &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helplanguage-transact-sql.md)  
  
  

