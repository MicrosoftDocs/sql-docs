---
title: "@@TEXTSIZE (Transact-SQL)"
description: "@@TEXTSIZE (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "@@TEXTSIZE"
  - "@@TEXTSIZE_TSQL"
helpviewer_keywords:
  - "SET statement, TEXTSIZE option"
  - "SELECT statement [SQL Server], text size returned"
  - "TEXTSIZE option"
  - "@@TEXTSIZE function"
  - "text size returned [SQL Server]"
dev_langs:
  - "TSQL"
---
# &#x40;&#x40;TEXTSIZE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the current value of the [TEXTSIZE](../../t-sql/statements/set-textsize-transact-sql.md) option.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
@@TEXTSIZE  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **integer**  
  
## Examples  
 The following example uses `SELECT` to display the `@@TEXTSIZE` value before and after it is changed with the `SET``TEXTSIZE` statement.  
  
```sql
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
  
  
