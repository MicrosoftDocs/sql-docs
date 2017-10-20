---
title: "APP_NAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "APP_NAME_TSQL"
  - "APP_NAME"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "name checking for current session [SQL Server]"
  - "sessions [SQL Server], application names"
  - "applications [SQL Server], names"
  - "current session application names"
  - "APP_NAME function"
ms.assetid: e491e192-9b30-4243-bc19-33c133fe08a8
caps.latest.revision: 35
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# APP_NAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the application name for the current session if set by the application.
  
> [!IMPORTANT]  
>  The application name is provided by the client and is not verified in any way. Do not use **APP_NAME** as part of a security check.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
  
APP_NAME  ( )  
```  
  
## Return Types  
**nvarchar(128)**
  
## Remarks  
Use **APP_NAME** when you want to perform different actions for different applications. For example, formatting a date differently for different applications, or returning an informational message to certain applications.
  
To set an application name in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], in the **Connect to Database Engine** dialog box, click **Options**. On the **Additional Connection Parameters** tab, provide an **app** attribute in the format `;app='application_name'`
  
## Examples  
The following example checks whether the client application that initiated this process is a `SQL Server Management Studio` session and provides a date in either US or ANSI format.
  
```sql
USE AdventureWorks2012;  
GO  
IF APP_NAME() = 'Microsoft SQL Server Management Studio - Query'  
PRINT 'This process was started by ' + APP_NAME() + '. The date is ' + CONVERT ( varchar(100) , GETDATE(), 101) + '.';  
ELSE   
PRINT 'This process was started by ' + APP_NAME() + '. The date is ' + CONVERT ( varchar(100) , GETDATE(), 102) + '.';  
GO  
```  
  
## See also
[System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-for-transact-sql.md)  
[Functions](../../t-sql/functions/functions.md)
  
  
