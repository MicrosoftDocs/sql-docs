---
title: "APP_NAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
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
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# APP_NAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This function returns the application name for the current session, if the application sets that name value.
  
> [!IMPORTANT]  
>  The client provides the application name, and `APP_NAME` does not verify the application name value in any way. Do not use `APP_NAME` as part of a security check.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
  
APP_NAME  ( )  
```  
  
## Return Types  
**nvarchar(128)**
  
## Remarks  
Use `APP_NAME` to distinguish between different applications, as a way to perform different actions for those applications. For example, `APP_NAME` can distinguish between different applications, which allows for a different date format for each application. It can also allow for the return of an informational message to certain applications.
  
To set an application name in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], click **Options** in the **Connect to Database Engine** dialog box. On the **Additional Connection Parameters** tab, provide an **app** attribute in the format `;app='application_name'`
  
## Example  
This example checks whether the client application that initiated this process is a `SQL Server Management Studio` session. It then provides a date value in either US or ANSI format.
  
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
  
  
