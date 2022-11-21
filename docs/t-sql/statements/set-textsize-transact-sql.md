---
title: "SET TEXTSIZE (Transact-SQL)"
description: SET TEXTSIZE (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "04/12/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "TEXTSIZE_TSQL"
  - "TEXTSIZE"
  - "SET_TEXTSIZE_TSQL"
  - "SET TEXTSIZE"
helpviewer_keywords:
  - "SET TEXTSIZE statement"
  - "SELECT statement [SQL Server], text size returned"
  - "size [SQL Server], text and image data"
  - "TEXTSIZE option"
  - "text size returned [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET TEXTSIZE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Specifies the size, in bytes, of **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **text**, **ntext**, and **image** data returned to the client by a SELECT statement.  
  
> [!IMPORTANT]
>  **ntext**, **text**, and **image** data types will be removed in a future version of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using these data types in new development work, and plan to modify applications that currently use them. Use **nvarchar(max)**, **varchar(max)**, and **varbinary(max)** instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
SET TEXTSIZE { number }   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *number*  
 Is the length of **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **text**, **ntext**, or **image** data, in bytes. *number* is an integer with a maximum value of 2147483647 (2 GB).  A value of -1 indicates unlimited size. A value of 0 resets the size to the default value of 4 KB.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client (10.0 and higher) and ODBC Driver for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically specify `-1` (unlimited) when connecting.  
  
 **Drivers older than [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008:** The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider (version 9) for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically set TEXTSIZE to 2147483647 when connecting.  
  
## Remarks  
 Setting SET TEXTSIZE affects the @@TEXTSIZE function.  
  
 The setting of set TEXTSIZE is set at execute or run time and not at parse time.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## See Also  
 [@@TEXTSIZE &#40;Transact-SQL&#41;](../../t-sql/functions/textsize-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)  
  
  
