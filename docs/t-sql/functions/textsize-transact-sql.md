---
title: "@@TEXTSIZE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "@@TEXTSIZE"
  - "@@TEXTSIZE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "SET statement, TEXTSIZE option"
  - "SELECT statement [SQL Server], text size returned"
  - "TEXTSIZE option"
  - "@@TEXTSIZE function"
  - "text size returned [SQL Server]"
ms.assetid: 4308a7b9-8e8f-49e9-8246-8224e32f4953
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# &#x40;&#x40;TEXTSIZE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the current value of the [TEXTSIZE](../../t-sql/statements/set-textsize-transact-sql.md) option.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
@@TEXTSIZE  
```  
  
## Return Types  
 **integer**  
  
## Examples  
 The following example uses `SELECT` to display the `@@TEXTSIZE` value before and after it is changed with the `SET``TEXTSIZE` statement.  
  
```  
-- Set the TEXTSIZE option to the default size of 4096 bytes.  
SET TEXTSIZE 0  
SELECT @@TEXTSIZE AS 'Text Size'  
SET TEXTSIZE 2048  
SELECT @@TEXTSIZE AS 'Text Size'  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
Text Size
-----------
4096
Text Size
-----------
2048
 ```  
  
## See Also  
 [Configuration Functions &#40;Transact-SQL&#41;](../../t-sql/functions/configuration-functions-transact-sql.md)   
 [SET TEXTSIZE &#40;Transact-SQL&#41;](../../t-sql/statements/set-textsize-transact-sql.md)  
  
  
