---
title: "sp_setapprole (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/12/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_setapprole"
  - "sp_setapprole_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_setapprole"
ms.assetid: cf0901c0-5f90-42d4-9d5b-8772c904062d
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sp_setapprole (Transact-SQL)

[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Activates the permissions associated with an application role in the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  

```sql
sp_setapprole [ @rolename = ] 'role',  
    [ @password = ] { encrypt N'password' }
      |  
        'password' [ , [ @encrypt = ] { 'none' | 'odbc' } ]  
        [ , [ @fCreateCookie = ] true | false ]  
    [ , [ @cookie = ] @cookie OUTPUT ]  
```

## Arguments

`[ @rolename = ] 'role'`
 Is the name of the application role defined in the current database. *role* is **sysname**, with no default. *role* must exist in the current database.  
  
`[ @password = ] { encrypt N'password' }`
 Is the password required to activate the application role. *password* is **sysname**, with no default. *password* can be obfuscated by using the ODBC **encrypt** function. When you use the **encrypt** function, the password must be converted to a Unicode string by placing **N** before the first quotation mark.  
  
 The encrypt option is not supported on connections that are using **SqlClient**.  
  
> [!IMPORTANT]  
> The ODBC **encrypt** function does not provide encryption. You should not rely on this function to protect passwords that are transmitted over a network. If this information will be transmitted across a network, use SSL or IPSec.
  
 **@encrypt = 'none'**  
 Specifies that no obfuscation be used. The password is passed to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as plain text. This is the default.  
  
 **@encrypt= 'odbc'**  
 Specifies that ODBC will obfuscate the password by using the ODBC **encrypt** function before sending the password to the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. This can be specified only when you are using either an ODBC client or the OLE DB Provider for SQL Server.  
  
`[ @fCreateCookie = ] true | false`
 Specifies whether a cookie is to be created. **true** is implicitly converted to 1. **false** is implicitly converted to 0.  
  
`[ @cookie = ] @cookie OUTPUT`
 Specifies an output parameter to contain the cookie. The cookie is generated only if the value of **@fCreateCookie** is **true**. **varbinary(8000)**  
  
> [!NOTE]  
> The cookie **OUTPUT** parameter for **sp_setapprole** is currently documented as **varbinary(8000)** which is the correct maximum length. However the current implementation returns **varbinary(50)**. Applications should continue to reserve **varbinary(8000)** so that the application continues to operate correctly if the cookie return size increases in a future release.
  
## Return Code Values

 0 (success) and 1 (failure)  
  
## Remarks

 After an application role is activated by using **sp_setapprole**, the role remains active until the user either disconnects from the server or executes **sp_unsetapprole**. **sp_setapprole** can be executed only by direct [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. **sp_setapprole** cannot be executed within another stored procedure or within a user-defined transaction.  
  
 For an overview of application roles, see [Application Roles](../../relational-databases/security/authentication-access/application-roles.md).  
  
> [!IMPORTANT]  
> To protect the application role password when it is transmitted across a network, you should always use an encrypted connection when enabling an application role.
> The [!INCLUDE[msCoName](../../includes/msconame-md.md)] ODBC **encrypt** option is not supported by **SqlClient**. If you must store credentials, encrypt them with the crypto API functions. The parameter *password* is stored as a one-way hash. To preserve compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], password complexity policy is not enforced by **sp_addapprole**. To enforce password complexity policy, use [CREATE APPLICATION ROLE](../../t-sql/statements/create-application-role-transact-sql.md).  
  
## Permissions

Requires membership in **public** and knowledge of the password for the role.  
  
## Examples  
  
### A. Activating an application role without the encrypt option

 The following example activates an application role named `SalesAppRole`, with the plain-text password `AsDeF00MbXX`, created with permissions specifically designed for the application used by the current user.

```sql
EXEC sys.sp_setapprole 'SalesApprole', 'AsDeF00MbXX';  
GO
```

### B. Activating an application role with a cookie and then reverting to the original context

 The following example activates the `Sales11` application role with password `fdsd896#gfdbfdkjgh700mM`, and creates a cookie. The example returns the name of the current user, and then reverts to the original context by executing `sp_unsetapprole`.  

```sql
DECLARE @cookie varbinary(8000);  
EXEC sys.sp_setapprole 'Sales11', 'fdsd896#gfdbfdkjgh700mM'  
    , @fCreateCookie = true, @cookie = @cookie OUTPUT;  
-- The application role is now active.  
SELECT USER_NAME();  
-- This will return the name of the application role, Sales11.  
EXEC sys.sp_unsetapprole @cookie;  
-- The application role is no longer active.  
-- The original context has now been restored.  
GO  
SELECT USER_NAME();  
-- This will return the name of the original user.
GO
```

## See Also

 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)
 [CREATE APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-application-role-transact-sql.md)
 [DROP APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-application-role-transact-sql.md)
 [sp_unsetapprole &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unsetapprole-transact-sql.md)
