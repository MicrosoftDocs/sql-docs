---
title: "sys.fn_my_permissions (Transact-SQL)"
description: "sys.fn_my_permissions (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/25/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.fn_my_permissions_TSQL"
  - "fn_my_permissions_TSQL"
  - "sys.fn_my_permissions"
  - "fn_my_permissions"
helpviewer_keywords:
  - "fn_my_permissions function"
  - "sys.fn_my_permissions function"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# sys.fn_my_permissions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns a list of the permissions effectively granted to the principal on a securable. A related function is [HAS_PERMS_BY_NAME](../../t-sql/functions/has-perms-by-name-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
fn_my_permissions ( securable , 'securable_class' )
```

## Arguments

#### *securable*

The name of the securable. If the securable is the server or a database, this value should be set to `NULL`. *securable* is a scalar expression of type **sysname**. *securable* can be a multipart name.

#### '*securable_class*'

The name of the class of securable for which permissions are listed. *securable_class* is **sysname**, with a default of `NULL`.

This argument must be one of the following values: `APPLICATION ROLE`, `ASSEMBLY`, `ASYMMETRIC KEY`, `CERTIFICATE`, `CONTRACT`, `DATABASE`, `ENDPOINT`, `FULLTEXT CATALOG`, `LOGIN`, `MESSAGE TYPE`, `OBJECT`, `REMOTE SERVICE BINDING`, `ROLE`, `ROUTE`, `SCHEMA`, `SERVER`, `SERVICE`, `SYMMETRIC KEY`, `TYPE`, `USER`, `XML SCHEMA COLLECTION`. A value of `NULL` defaults to `SERVER`.

## Columns returned

The following table lists the columns that `fn_my_permissions` returns. Each row that is returned describes a permission held by the current security context on the securable. Returns `NULL` if the query fails.

| Column name | Type | Description |
| --- | --- | --- |
| `entity_name` | **sysname** | Name of the securable on which the listed permissions are effectively granted. |
| `subentity_name` | **sysname** | Column name if the securable has columns, otherwise `NULL`. |
| `permission_name` | **nvarchar** | Name of the permission. |

## Remarks

This table-valued function returns a list of the effective permissions held by the calling principal on a specified securable. An effective permission is any one of the following options:

- A permission granted directly to the principal, and not denied.
- A permission implied by a higher-level permission held by the principal and not denied.
- A permission granted to a role or group of which the principal is a member, and not denied.
- A permission held by a role or group of which the principal is a member, and not denied.

The permission evaluation is always performed in the security context of the caller. To determine whether some other principal has an effective permission, the caller must have `IMPERSONATE` permission on that principal.

For schema-level entities, one-, two-, or three-part non-null names are accepted. For database-level entities, a one-part name is accepted, with a null value meaning the *current database*. For the server itself, a null value (meaning the *current server*) is required. `fn_my_permissions` can't check permissions on a linked server.

The following query returns a list of built-in securable classes:

```sql
SELECT DISTINCT class_desc
FROM fn_builtin_permissions(DEFAULT)
ORDER BY class_desc;
GO
```

If `DEFAULT` is supplied as the value of *securable* or *securable_class*, the value is interpreted as `NULL`.

The `fn_my_permissions` function isn't supported in Azure Synapse Analytics dedicated SQL pools.

## Permissions

Requires membership in the **public** role.

## Examples

### A. List effective permissions on the server

The following example returns a list of the effective permissions of the caller on the server.

```sql
SELECT * FROM fn_my_permissions(NULL, 'SERVER');
GO
```

### B. List effective permissions on the database

The following example returns a list of the effective permissions of the caller on the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
SELECT * FROM fn_my_permissions(NULL, 'DATABASE');
GO
```

### C. List effective permissions on a view

The following example returns a list of the effective permissions of the caller on the `vIndividualCustomer` view in the `Sales` schema of the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO
SELECT * FROM fn_my_permissions('Sales.vIndividualCustomer', 'OBJECT')
ORDER BY subentity_name, permission_name;
GO
```

### D. List effective permissions of another user

The following example returns a list of the effective permissions of database user `Wanida` on the `Employee` table in the `HumanResources` schema of the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. The caller requires `IMPERSONATE` permission on user `Wanida`.

```sql
EXECUTE AS USER = 'Wanida';

SELECT *
FROM fn_my_permissions('HumanResources.Employee', 'OBJECT')
ORDER BY subentity_name, permission_name;

REVERT;
GO
```

### E. List effective permissions on a certificate

The following example returns a list of the effective permissions of the caller on a certificate named `Shipping47` in the current database.

```sql
SELECT * FROM fn_my_permissions('Shipping47', 'CERTIFICATE');
GO
```

### F. List effective permissions on an XML Schema Collection

The following example returns a list of the effective permissions of the caller on an XML Schema Collection named `ProductDescriptionSchemaCollection` in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO
SELECT * FROM fn_my_permissions(
    'ProductDescriptionSchemaCollection',
    'XML SCHEMA COLLECTION'
);
GO
```

### G. List effective permissions on a database user

The following example returns a list of the effective permissions of the caller on a user named `MalikAr` in the current database.

```sql
SELECT * FROM fn_my_permissions('MalikAr', 'USER');
GO
```

### H. List effective permissions of another login

The following example returns a list of the effective permissions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login `WanidaBenshoof` on the `Employee` table in the `HumanResources` schema of the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. The caller requires `IMPERSONATE` permission on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login `WanidaBenshoof`.

```sql
EXECUTE AS LOGIN = 'WanidaBenshoof';

SELECT *
FROM fn_my_permissions('AdventureWorks2022.HumanResources.Employee', 'OBJECT')
ORDER BY subentity_name, permission_name;

REVERT;
GO
```

## Related content

- [Permissions (Database Engine)](../security/permissions-database-engine.md)
- [Securables](../security/securables.md)
- [Permissions Hierarchy (Database Engine)](../security/permissions-hierarchy-database-engine.md)
- [sys.fn_builtin_permissions (Transact-SQL)](sys-fn-builtin-permissions-transact-sql.md)
- [EXECUTE AS (Transact-SQL)](../../t-sql/statements/execute-as-transact-sql.md)
