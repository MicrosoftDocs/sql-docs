---
title: "EXECUTE AS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "EXECUTE AS"
  - "EXECUTE_AS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "REVERT statement"
  - "WITH NO REVERT clause"
  - "sessions [SQL Server], execution context"
  - "EXECUTE AS"
  - "execution context [SQL Server]"
  - "switching execution context"
ms.assetid: 613b8271-7f7d-4378-b7a2-5a7698551dbd
author: VanMSFT
ms.author: vanto
manager: craigg
---
# EXECUTE AS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Sets the execution context of a session.  
  
 By default, a session starts when a user logs in and ends when the user logs off. All operations during a session are subject to permission checks against that user. When an **EXECUTE AS** statement is run, the execution context of the session is switched to the specified login or user name. After the context switch, permissions are checked against the login and user security tokens for that account instead of the person calling the **EXECUTE AS** statement. In essence, the user or login account is impersonated for the duration of the session or module execution, or the context switch is explicitly reverted.  
  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
{ EXEC | EXECUTE } AS <context_specification>  
[;]  
  
<context_specification>::=  
{ LOGIN | USER } = 'name'  
    [ WITH { NO REVERT | COOKIE INTO @varbinary_variable } ]   
| CALLER  
```  
  
## Arguments  
 LOGIN  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies the execution context to be impersonated is a login. The scope of impersonation is at the server level.  
  
> [!NOTE]  
>  This option is not available in a contained database or in SQL Database.  
  
 USER  
 Specifies the context to be impersonated is a user in the current database. The scope of impersonation is restricted to the current database. A context switch to a database user does not inherit the server-level permissions of that user.  
  
> [!IMPORTANT]  
>  While the context switch to the database user is active, any attempt to access resources outside of the database will cause the statement to fail. This includes USE *database* statements, distributed queries, and queries that reference another database that uses three- or four-part identifiers.  
  
 **'** _name_ **'**  
 Is a valid user or login name. *name* must be a member of the **sysadmin** fixed server role, or exist as a principal in [sys.database_principals](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md) or [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md), respectively.  
  
 *name* can be specified as a local variable.  
  
 *name* must be a singleton account, and cannot be a group, role, certificate, key, or built-in account, such as NT AUTHORITY\LocalService, NT AUTHORITY\NetworkService, or NT AUTHORITY\LocalSystem.  
  
 For more information, see [Specifying a User or Login Name](#_user) later in this topic.  
  
 NO REVERT  
 Specifies that the context switch cannot be reverted back to the previous context. The **NO REVERT** option can only be used at the adhoc level.  
  
 For more information about reverting to the previous context, see [REVERT &#40;Transact-SQL&#41;](../../t-sql/statements/revert-transact-sql.md).  
  
 COOKIE INTO **@**_varbinary_variable_  
 Specifies the execution context can only be reverted back to the previous context if the calling REVERT WITH COOKIE statement contains the correct **@**_varbinary_variable_ value. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] passes the cookie to **@**_varbinary_variable_. The **COOKIE INTO** option can only be used at the adhoc level.  
  
 **@** _varbinary_variable_ is **varbinary(8000)**.  
  
> [!NOTE]  
>  The cookie **OUTPUT** parameter for is currently documented as **varbinary(8000)** which is the correct maximum length. However the current implementation returns **varbinary(100)**. Applications should reserve **varbinary(8000)** so that the application continues to operate correctly if the cookie return size increases in a future release.  
  
 CALLER  
 When used inside a module, specifies the statements inside the module are executed in the context of the caller of the module.  
  
 When used outside a module, the statement has no action.  
  
## Remarks  
 The change in execution context remains in effect until one of the following occurs:  
  
-   Another EXECUTE AS statement is run.  
  
-   A REVERT statement is run.  
  
-   The session is dropped.  
  
-   The stored procedure or trigger where the command was executed exits.  
  
You can create an execution context stack by calling the EXECUTE AS statement multiple times across multiple principals. When called, the REVERT statement switches the context to the login or user in the next level up in the context stack. For a demonstration of this behavior, see [Example A](#_exampleA).  
  
##  <a name="_user"></a> Specifying a User or Login Name  
 The user or login name specified in EXECUTE AS \<context_specification> must exist as a principal in **sys.database_principals** or **sys.server_principals**, respectively, or the EXECUTE AS statement fails. Additionally, IMPERSONATE permissions must be granted on the principal. Unless the caller is the database owner, or is a member of the **sysadmin** fixed server role, the principal must exist even when the user is accessing the database or instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] through a Windows group membership. For example, assume the following conditions: 
  
-   **CompanyDomain\SQLUsers** group has access to the **Sales** database.  
  
-   **CompanyDomain\SqlUser1** is a member of **SQLUsers** and, therefore, has implicit access to the **Sales** database.  
  
 Although **CompanyDomain\SqlUser1** has access to the database through membership in the **SQLUsers** group, the statement `EXECUTE AS USER = 'CompanyDomain\SqlUser1'` fails because `CompanyDomain\SqlUser1` does not exist as a principal in the database.  
  
If the user is orphaned (the associated login no longer exists), and the user was not created with **WITHOUT LOGIN**, **EXECUTE AS** will fail for the user.  
  
## Best Practice  
 Specify a login or user that has the least privileges required to perform the operations in the session. For example, do not specify a login name with server-level permissions, if only database-level permissions are required; or do not specify a database owner account unless those permissions are required.  
  
> [!CAUTION]  
>  The EXECUTE AS statement can succeed as long as the [!INCLUDE[ssDE](../../includes/ssde-md.md)] can resolve the name. If a domain user exists, Windows might be able to resolve the user for the [!INCLUDE[ssDE](../../includes/ssde-md.md)], even though the Windows user does not have access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This can lead to a condition where a login with no access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] appears to be logged in, though the impersonated login would only have the permissions granted to public or guest.  
  
## Using WITH NO REVERT  
 When the EXECUTE AS statement includes the optional WITH NO REVERT clause, the execution context of a session cannot be reset using REVERT or by executing another EXECUTE AS statement. The context set by the statement remains in affect until the session is dropped.  
  
 When the WITH NO REVERT COOKIE = @*varbinary_variable* clause is specified, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] passes the cookie value to @*varbinary_variable*. The execution context set by that statement can only be reverted to the previous context if the calling REVERT WITH COOKIE = @*varbinary_variable* statement contains the same *@varbinary_variable* value.  
  
 This option is useful in an environment in which connection pooling is used. Connection pooling is the maintenance of a group of database connections for reuse by applications on an application server. Because the value passed to *@varbinary_variable* is known only to the caller of the EXECUTE AS statement, the caller can guarantee that the execution context they establish cannot be changed by anyone else.  
  
## Determining the Original Login  
 Use the [ORIGINAL_LOGIN](../../t-sql/functions/original-login-transact-sql.md) function to return the name of the login that connected to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can use this function to return the identity of the original login in sessions in which there are many explicit or implicit context switches.  
  
## Permissions  
 To specify **EXECUTE AS** on a login, the caller must have **IMPERSONATE** permission on the specified login name and must not be denied the **IMPERSONATE ANY LOGIN** permission. To specify **EXECUTE AS** on a database user, the caller must have **IMPERSONATE** permissions on the specified user name. When **EXECUTE AS CALLER** is specified, **IMPERSONATE** permissions are not required.  
  
## Examples  
  
###  <a name="_exampleA"></a> A. Using EXECUTE AS and REVERT to switch context  
 The following example creates a context execution stack using multiple principals. The `REVERT` statement is then used to reset the execution context to the previous caller. The `REVERT` statement is executed multiple times moving up the stack until the execution context is set to the original caller.  
  
```  
USE AdventureWorks2012;  
GO  
--Create two temporary principals  
CREATE LOGIN login1 WITH PASSWORD = 'J345#$)thb';  
CREATE LOGIN login2 WITH PASSWORD = 'Uor80$23b';  
GO  
CREATE USER user1 FOR LOGIN login1;  
CREATE USER user2 FOR LOGIN login2;  
GO  
--Give IMPERSONATE permissions on user2 to user1  
--so that user1 can successfully set the execution context to user2.  
GRANT IMPERSONATE ON USER:: user2 TO user1;  
GO  
--Display current execution context.  
SELECT SUSER_NAME(), USER_NAME();  
-- Set the execution context to login1.   
EXECUTE AS LOGIN = 'login1';  
--Verify the execution context is now login1.  
SELECT SUSER_NAME(), USER_NAME();  
--Login1 sets the execution context to login2.  
EXECUTE AS USER = 'user2';  
--Display current execution context.  
SELECT SUSER_NAME(), USER_NAME();  
-- The execution context stack now has three principals: the originating caller, login1 and login2.  
--The following REVERT statements will reset the execution context to the previous context.  
REVERT;  
--Display current execution context.  
SELECT SUSER_NAME(), USER_NAME();  
REVERT;  
--Display current execution context.  
SELECT SUSER_NAME(), USER_NAME();  
  
--Remove temporary principals.  
DROP LOGIN login1;  
DROP LOGIN login2;  
DROP USER user1;  
DROP USER user2;  
GO  
```  
  
### B. Using the WITH COOKIE clause  
 The following example sets the execution context of a session to a specified user and specifies the WITH NO REVERT COOKIE = @*varbinary_variable* clause. The `REVERT` statement must specify the value passed to the `@cookie` variable in the `EXECUTE AS` statement to successfully revert the context back to the caller. To run this example, the `login1` login and `user1` user created in example A must exist.  
  
```  
DECLARE @cookie varbinary(8000);  
EXECUTE AS USER = 'user1' WITH COOKIE INTO @cookie;  
-- Store the cookie in a safe location in your application.  
-- Verify the context switch.  
SELECT SUSER_NAME(), USER_NAME();  
--Display the cookie value.  
SELECT @cookie;  
GO  
-- Use the cookie in the REVERT statement.  
DECLARE @cookie varbinary(8000);  
-- Set the cookie value to the one from the SELECT @cookie statement.  
SET @cookie = <value from the SELECT @cookie statement>;  
REVERT WITH COOKIE = @cookie;  
-- Verify the context switch reverted.  
SELECT SUSER_NAME(), USER_NAME();  
GO  
```  
  
## See Also  
 [REVERT &#40;Transact-SQL&#41;](../../t-sql/statements/revert-transact-sql.md)   
 [EXECUTE AS Clause &#40;Transact-SQL&#41;](../../t-sql/statements/execute-as-clause-transact-sql.md)  
  
  

