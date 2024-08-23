---
title: "sp_unsetapprole (Transact-SQL)"
description: Deactivates an application role and reverts to the previous security context.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_unsetapprole_TSQL"
  - "sp_unsetapprole"
helpviewer_keywords:
  - "sp_unsetapprole"
dev_langs:
  - "TSQL"
---
# sp_unsetapprole (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deactivates an application role and reverts to the previous security context.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_unsetapprole [ @cookie = ] cookie
[ ; ]
```

## Arguments

#### [ @cookie = ] *cookie*

Specifies the cookie that was created when the application role was activated. *@cookie* is **varbinary(8000)**, with no default. The cookie is created by [sp_setapprole](sp-setapprole-transact-sql.md).

> [!NOTE]  
> The cookie `OUTPUT` parameter for `sp_setapprole` is currently documented as **varbinary(8000)** which is the correct maximum length. However the current implementation returns **varbinary(50)**. Applications should continue to reserve **varbinary(8000)** so that the application continues to operate correctly if the cookie return size increases in a future release.

## Return code values

0 (success) and 1 (failure)

## Remarks

After an application role is activated by using `sp_setapprole`, the role remains active until the user either disconnects from the server or executes `sp_unsetapprole`.

For an overview of application roles, see [Application Roles](../security/authentication-access/application-roles.md).

## Permissions

Requires membership in **public** and knowledge of the cookie saved when the application role was activated.

## Examples

### Activate an application role with a cookie, then reverting to the previous context

The following example activates the `Sales11` application role with password `fdsd896#gfdbfdkjgh700mM`, and creates a cookie. The example returns the name of the current user, and then reverts to the original context by executing `sp_unsetapprole`.

```sql
DECLARE @cookie VARBINARY(8000);

EXEC sp_setapprole 'Sales11',
    'fdsd896#gfdbfdkjgh700mM',
    @fCreateCookie = true,
    @cookie = @cookie OUTPUT;

-- The application role is now active.
SELECT USER_NAME();

-- Return the name of the application role, Sales11.
EXEC sp_unsetapprole @cookie;
    -- The application role is no longer active.
    -- The original context has now been restored.
GO

-- Return the name of the original user.
SELECT USER_NAME();
GO
```

## Related content

- [sp_setapprole (Transact-SQL)](sp-setapprole-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [CREATE APPLICATION ROLE (Transact-SQL)](../../t-sql/statements/create-application-role-transact-sql.md)
- [DROP APPLICATION ROLE (Transact-SQL)](../../t-sql/statements/drop-application-role-transact-sql.md)
