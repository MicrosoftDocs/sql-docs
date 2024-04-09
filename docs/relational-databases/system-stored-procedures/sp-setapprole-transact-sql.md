---
title: "sp_setapprole (Transact-SQL)"
description: sp_setapprole activates the permissions associated with an application role in the current database.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_setapprole"
  - "sp_setapprole_TSQL"
helpviewer_keywords:
  - "sp_setapprole"
dev_langs:
  - "TSQL"
---
# sp_setapprole (Transact-SQL)

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

Activates the permissions associated with an application role in the current database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_setapprole
    [ @rolename = ] N'rolename'
    , [ @password = ] N'password'
    [ , [ @encrypt = ] 'encrypt' ]
    [ , [ @fCreateCookie = ] fCreateCookie ]
    [ , [ @cookie = ] cookie OUTPUT ]
[ ; ]
```

## Arguments

#### [ @rolename = ] N'*rolename*'

The name of the application role defined in the current database. *@rolename* is **sysname**, with no default. *@rolename* must exist in the current database.

#### [ @password = ] { encrypt N'*password*' }

The password required to activate the application role. *@password* is **sysname**, with no default. *@password* can be obfuscated by using the ODBC `encrypt` function. When you use the `encrypt` function, the password must be converted to a Unicode string by placing `N` before the first quotation mark.

The encrypt option isn't supported on connections that use **SqlClient**.

> [!IMPORTANT]  
> The ODBC `encrypt` function doesn't provide encryption. You shouldn't rely on this function to protect passwords that are transmitted over a network. If this information will be transmitted across a network, use TLS or IPSec.

#### [ @encrypt = ] { 'none' | 'odbc' }

Specifies the encryption type before sending the password to the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. *@encrypt* is **varchar(10)**, and can be one of these values.

| Value | Description |
| --- | --- |
| `none` (default) | Specifies that no obfuscation is used. The password is passed to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] as plain text. |
| `odbc` | Specifies that ODBC obfuscates the password by using the ODBC `encrypt` function before sending the password to the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. This value can be specified only when you're using either an ODBC client or the OLE DB Provider for SQL Server. |

#### [ @fCreateCookie = ] { 'true' | 'false' }

Specifies whether a cookie is to be created. *@fCreateCookie* is **bit**, with a default of `0`.

`true` is implicitly converted to `1`. `false` is implicitly converted to `0`.

#### [ @cookie = ] *cookie* OUTPUT

Specifies an output parameter to contain the cookie. *@cookie* is an OUTPUT parameter of type **varbinary(8000)**. The cookie is generated only if the value of *@fCreateCookie* is `true`.

> [!NOTE]  
> Although the current implementation returns **varbinary(50)**, applications should reserve the documented **varbinary(8000)**, so that the application continues to operate correctly if the cookie return size increases in a future release.

## Return code values

`0` (success) and `1` (failure).

## Remarks

After an application role is activated by using `sp_setapprole`, the role remains active until the user either disconnects from the server or executes `sp_unsetapprole`. You can't use `sp_setapprole`  within another stored procedure, trigger or within a user-defined transaction. It can only be executed as direct [!INCLUDE [tsql](../../includes/tsql-md.md)] statements.

For an overview of application roles, see [Application Roles](../security/authentication-access/application-roles.md).

You should always use an encrypted connection when enabling an application role, to protect the application role password when you transmit it over a network.

The [!INCLUDE [msCoName](../../includes/msconame-md.md)] ODBC `encrypt` option isn't supported by **SqlClient**. If you must store credentials, encrypt them with the crypto API functions. The parameter *@password* is stored as a one-way hash. To preserve compatibility with earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], `sp_addapprole` doesn't enforce the password complexity policy. To enforce the password complexity policy, use [CREATE APPLICATION ROLE](../../t-sql/statements/create-application-role-transact-sql.md).

## Permissions

Requires membership in **public** and knowledge of the password for the role.

## Examples

### A. Activate an application role without the encrypt option

The following example activates an application role named `SalesAppRole`, with the plain-text password `AsDeF00MbXX`, created with permissions designed for the application used by the current user.

```sql
EXEC sys.sp_setapprole 'SalesApprole', 'AsDeF00MbXX';
GO
```

### B. Activate an application role with a cookie and then reverting to the original context

The following example activates the `Sales11` application role with password `fdsd896#gfdbfdkjgh700mM`, and creates a cookie. The example returns the name of the current user, and then reverts to the original context by executing `sp_unsetapprole`.

```sql
DECLARE @cookie VARBINARY(8000);

EXEC sys.sp_setapprole 'Sales11',
    'fdsd896#gfdbfdkjgh700mM',
    @fCreateCookie = true,
    @cookie = @cookie OUTPUT;
```

The application role is now active. `USER_NAME()` returns the name of the application role, `Sales11`.

```sql
SELECT USER_NAME();
```

Unset the application role.

```sql
EXEC sys.sp_unsetapprole @cookie;
GO
```

The application role is no longer active. The original context is restored. `USER_NAME()` returns the name of the original user.

```sql
SELECT USER_NAME();
GO
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [CREATE APPLICATION ROLE (Transact-SQL)](../../t-sql/statements/create-application-role-transact-sql.md)
- [DROP APPLICATION ROLE (Transact-SQL)](../../t-sql/statements/drop-application-role-transact-sql.md)
- [sp_unsetapprole (Transact-SQL)](sp-unsetapprole-transact-sql.md)
