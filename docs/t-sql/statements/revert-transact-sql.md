---
title: "REVERT (Transact-SQL)"
description: REVERT (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "07/26/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "REVERT_TSQL"
  - "REVERT"
helpviewer_keywords:
  - "REVERT statement"
  - "context switching [SQL Server], reverting"
  - "reverting execution context"
  - "REVERT WITH COOKIE statement"
  - "execution context [SQL Server]"
  - "COOKIE clause"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017||=azure-sqldw-latest"
---
# REVERT (Transact-SQL)
[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]   

  Switches the execution context back to the caller of the last EXECUTE AS statement.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
REVERT  
    [ WITH COOKIE = @varbinary_variable ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 WITH COOKIE = @*varbinary_variable*  
 Specifies the cookie that was created in a corresponding [EXECUTE AS](../../t-sql/statements/execute-as-transact-sql.md) stand-alone statement. *\@varbinary_variable* is **varbinary(100)**.  
  
## Remarks  
 REVERT can be specified within a module such as a stored procedure or user-defined function, or as a stand-alone statement. When specified inside a module, REVERT is applicable only to EXECUTE AS statements defined in the module. For example, the following stored procedure issues an `EXECUTE AS` statement followed by a `REVERT` statement.  
  
```sql  
CREATE PROCEDURE dbo.usp_myproc   
  WITH EXECUTE AS CALLER  
AS   
    SELECT SUSER_NAME(), USER_NAME();  
    EXECUTE AS USER = 'guest';  
    SELECT SUSER_NAME(), USER_NAME();  
    REVERT;  
    SELECT SUSER_NAME(), USER_NAME();  
GO  
```  
  
 Assume that in the session in which the stored procedure is run, the execution context of the session is explicitly changed to `login1`, as shown in the following example.  
  
```sql 
  -- Sets the execution context of the session to 'login1'.  
EXECUTE AS LOGIN = 'login1';  
GO  
EXECUTE dbo.usp_myproc;   
```  
  
 The `REVERT` statement that is defined inside `usp_myproc` switches the execution context set inside the module, but does not affect the execution context set outside the module. That is, the execution context for the session remains set to `login1`.  
  
 When specified as a standalone statement, REVERT applies to EXECUTE AS statements defined within a batch or session. REVERT has no effect if the corresponding EXECUTE AS statement contains the WITH NO REVERT clause. In this case, the execution context remains in effect until the session is dropped.  
  
## Using REVERT WITH COOKIE  
 The EXECUTE AS statement that is used to set the execution context of a session can include the optional clause WITH NO REVERT COOKIE = @*varbinary_variable*. When this statement is run, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] passes the cookie to @*varbinary_variable*. The execution context set by that statement can only be reverted to the previous context if the calling REVERT WITH COOKIE = @*varbinary_variable* statement contains the correct *\@varbinary_variable* value.  
  
 This mechanism is useful in an environment in which connection pooling is used. Connection pooling is the maintenance of a group of database connections for reuse by applications across multiple end users. Because the value passed to *\@varbinary_variable* is known only to the caller of the EXECUTE AS statement (in this case, the application), the caller can guarantee that the execution context they establish cannot be changed by the end user that invokes the application. After the execution context is reverted, the application can switch context to another principal.  
  
## Permissions  
 No permissions are required.  
  
## Examples  
  
### A. Using EXECUTE AS and REVERT to switch context  
 The following example creates a context execution stack by using multiple principals. The REVERT statement is then used to reset the execution context to the previous caller. The REVERT statement is executed multiple times moving up the stack until the execution context is set to the original caller.  
  
```sql  
USE AdventureWorks2012;  
GO  
-- Create two temporary principals.  
CREATE LOGIN login1 WITH PASSWORD = 'J345#$)thb';  
CREATE LOGIN login2 WITH PASSWORD = 'Uor80$23b';  
GO  
CREATE USER user1 FOR LOGIN login1;  
CREATE USER user2 FOR LOGIN login2;  
GO  
-- Give IMPERSONATE permissions on user2 to user1  
-- so that user1 can successfully set the execution context to user2.  
GRANT IMPERSONATE ON USER:: user2 TO user1;  
GO  
-- Display current execution context.  
SELECT SUSER_NAME(), USER_NAME();  
-- Set the execution context to login1.   
EXECUTE AS LOGIN = 'login1';  
-- Verify that the execution context is now login1.  
SELECT SUSER_NAME(), USER_NAME();  
-- Login1 sets the execution context to login2.  
EXECUTE AS USER = 'user2';  
-- Display current execution context.  
SELECT SUSER_NAME(), USER_NAME();  
-- The execution context stack now has three principals: the originating caller, login1, and login2.  
-- The following REVERT statements will reset the execution context to the previous context.  
REVERT;  
-- Display the current execution context.  
SELECT SUSER_NAME(), USER_NAME();  
REVERT;  
-- Display the current execution context.  
SELECT SUSER_NAME(), USER_NAME();  
  
-- Remove the temporary principals.  
DROP LOGIN login1;  
DROP LOGIN login2;  
DROP USER user1;  
DROP USER user2;  
GO  
```  
  
### B. Using the WITH COOKIE clause  
 The following example sets the execution context of a session to a specified user and specifies the WITH NO REVERT COOKIE = @*varbinary_variable* clause. The `REVERT` statement must specify the value passed to the `@cookie` variable in the `EXECUTE AS` statement to successfully revert the context back to the caller. To run this example, the `login1` login and `user1` user created in example A must exist.  
  
```sql 
DECLARE @cookie VARBINARY(100);  
EXECUTE AS USER = 'user1' WITH COOKIE INTO @cookie;  
-- Store the cookie somewhere safe in your application.  
-- Verify the context switch.  
SELECT SUSER_NAME(), USER_NAME();  
--Display the cookie value.  
SELECT @cookie;  
GO  
-- Use the cookie in the REVERT statement.  
DECLARE @cookie VARBINARY(100);  
-- Set the cookie value to the one from the SELECT @cookie statement.  
SET @cookie = <value from the SELECT @cookie statement>;  
REVERT WITH COOKIE = @cookie;  
-- Verify the context switch reverted.  
SELECT SUSER_NAME(), USER_NAME();  
GO  
```  
  
## See Also  
 [EXECUTE AS &#40;Transact-SQL&#41;](../../t-sql/statements/execute-as-transact-sql.md)   
 [EXECUTE AS Clause &#40;Transact-SQL&#41;](../../t-sql/statements/execute-as-clause-transact-sql.md)   
 [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md)   
 [SUSER_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/suser-name-transact-sql.md)   
 [USER_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/user-name-transact-sql.md)   
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [CREATE USER &#40;Transact-SQL&#41;](../../t-sql/statements/create-user-transact-sql.md)  
  
  
