---
title: "ORIGINAL_LOGIN (Transact-SQL)"
description: "ORIGINAL_LOGIN (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "ORIGINAL_LOGIN_TSQL"
  - "ORIGINAL_LOGIN"
helpviewer_keywords:
  - "logins [SQL Server], context switches"
  - "context switching [SQL Server], login names"
  - "original login names [SQL Server]"
  - "ORIGINAL_LOGIN function"
  - "names [SQL Server], logins"
dev_langs:
  - "TSQL"
---
# ORIGINAL_LOGIN (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the name of the login that connected to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can use this function to return the identity of the original login in sessions in which there are many explicit or implicit context switches.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
ORIGINAL_LOGIN( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **sysname**  
  
## Remarks  
 This function can be useful in auditing the identity of the original connecting context. Whereas functions such as [SESSION_USER](../../t-sql/functions/session-user-transact-sql.md) and [CURRENT_USER](../../t-sql/functions/current-user-transact-sql.md) return the current executing context, ORIGINAL_LOGIN returns the identity of the login that first connected to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in that session.  
 
  
## Examples  
 The following example switches the execution context of the current session from the caller of the statements to `login1`. The functions `SUSER_SNAME` and `ORIGINAL_LOGIN` are used to return the current session user (the user to whom the context was switched), and the original login account. 
 
  >[!NOTE]
  > Although the ORIGINAL_LOGIN function is supported on Azure SQL Database, the following script will fail because *Execute as LOGIN* is not supported on Azure SQL Database. 
  
```sql  
USE AdventureWorks2012;  
GO  
--Create a temporary login and user.  
CREATE LOGIN login1 WITH PASSWORD = 'J345#$)thb';  
CREATE USER user1 FOR LOGIN login1;  
GO  
--Execute a context switch to the temporary login account.  
DECLARE @original_login sysname;  
DECLARE @current_context sysname;  
EXECUTE AS LOGIN = 'login1';  
SET @original_login = ORIGINAL_LOGIN();  
SET @current_context = SUSER_SNAME();  
SELECT 'The current executing context is: '+ @current_context;  
SELECT 'The original login in this session was: '+ @original_login  
GO  
-- Return to the original execution context  
-- and remove the temporary principal.  
REVERT;  
GO  
DROP LOGIN login1;  
DROP USER user1;  
GO  
```  
  
## See Also  
 [EXECUTE AS &#40;Transact-SQL&#41;](../../t-sql/statements/execute-as-transact-sql.md)   
 [REVERT &#40;Transact-SQL&#41;](../../t-sql/statements/revert-transact-sql.md)  
  
  
