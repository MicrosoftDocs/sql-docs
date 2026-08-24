---
title: "ALTER APPLICATION ROLE (Transact-SQL)"
description: ALTER APPLICATION ROLE (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: 04/29/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER_APPLICATION_ROLE_TSQL"
  - "ALTER APPLICATION ROLE"
helpviewer_keywords:
  - "modifying application roles"
  - "passwords [SQL Server], application roles"
  - "ALTER APPLICATION ROLE statement"
  - "application roles [SQL Server], modifying"
dev_langs:
  - "TSQL"
ms.custom:
  - build-2025
---

# ALTER APPLICATION ROLE (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Changes the name, password, or default schema of an application role.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
ALTER APPLICATION ROLE application_role_name
    WITH <set_item> [ , ...n ]

<set_item> ::=
    NAME = new_application_role_name
    | PASSWORD = 'password'
    | DEFAULT_SCHEMA = schema_name
```

## Arguments

#### *application_role_name*

The name of the application role to be modified.

#### NAME = *new_application_role_name*

Specifies the new name of the application role. This name must not already be used to refer to any principal in the database.

#### PASSWORD = '*password*'

Specifies the password for the application role. `password` must meet the Windows password policy requirements of the computer that is running the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You should always use strong passwords.

#### DEFAULT_SCHEMA = *schema_name*

Specifies the first schema that will be searched by the server when it resolves the names of objects. *schema_name* can be a schema that doesn't exist in the database.

## Remarks

If the new application role name already exists in the database, the statement fails. When the name, password, or default schema of an application role is changed the ID associated with the role isn't changed.

> [!IMPORTANT]  
> Password expiration policy isn't applied to application role passwords. For this reason, take extra care in selecting strong passwords. Applications that invoke application roles must store their passwords.

Application roles are visible in the `sys.database_principals` catalog view.

> [!NOTE]  
> [!INCLUDE [sscautionuserschema-md](../../includes/sscautionuserschema-md.md)]

[!INCLUDE [encryption-algorithm-history-md](../../includes/encryption-algorithm-history.md)]

## Permissions

Requires ALTER ANY APPLICATION ROLE permission on the database. To change the default schema, the user also needs ALTER permission on the application role. An application role can alter its own default schema, but not its name or password.

## Examples

### A. Changing the name of application role

The following example changes the name of the application role `weekly_receipts` to `receipts_ledger`.

```sql
USE AdventureWorks2022;
CREATE APPLICATION ROLE weekly_receipts
    WITH PASSWORD = '987Gbv8$76sPYY5m23' ,
    DEFAULT_SCHEMA = Sales;
GO
ALTER APPLICATION ROLE weekly_receipts
    WITH NAME = receipts_ledger;
GO
```

### B. Changing the password of application role

The following example changes the password of the application role `receipts_ledger`.

```sql
ALTER APPLICATION ROLE receipts_ledger
    WITH PASSWORD = '897yUUbv867y$200nk2i';
GO
```

### C. Changing the name, password, and default schema

The following example changes the name, password, and default schema of the application role `receipts_ledger` all at the same time.

```sql
ALTER APPLICATION ROLE receipts_ledger
    WITH NAME = weekly_ledger,
    PASSWORD = '897yUUbv77bsrEE00nk2i',
    DEFAULT_SCHEMA = Production;
GO
```

## Related content

- [Application Roles](../../relational-databases/security/authentication-access/application-roles.md)
- [CREATE APPLICATION ROLE (Transact-SQL)](create-application-role-transact-sql.md)
- [DROP APPLICATION ROLE (Transact-SQL)](drop-application-role-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
