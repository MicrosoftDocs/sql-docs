---
title: "sp_releaseapplock (Transact-SQL)"
description: sp_releaseapplock releases a lock on an application resource.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_releaseapplock_TSQL"
  - "sp_releaseapplock"
helpviewer_keywords:
  - "sp_releaseapplock"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_releaseapplock (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Releases a lock on an application resource.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_releaseapplock
    [ [ @Resource = ] N'Resource' ]
    [ , [ @LockOwner = ] 'LockOwner' ]
    [ , [ @DbPrincipal = ] N'DbPrincipal' ]
[ ; ]
```

## Arguments

#### [ @Resource = ] N'*Resource*'

A lock resource name specified by the client application. *@Resource* is **nvarchar(255)**, with a default of `NULL`. *@Resource* is binary-compared, thus is case-sensitive regardless of the collation settings of the current database.

The application must ensure that the resource is unique. The specified name is hashed internally into a value that can be stored in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] lock manager.

#### [ @LockOwner = ] '*LockOwner*'

The owner of the lock, which is the *@LockOwner* value when the lock was requested. *@LockOwner* is **varchar(32)**, with a default of `Transaction`. The value can also be `Session`. When the *@LockOwner* value is **Transaction**, by default or specified explicitly, `sp_getapplock` must be executed from within a transaction.

#### [ @DbPrincipal = ] N'*DbPrincipal*'

The user, role, or application role that has permissions to an object in a database. *@DbPrincipal* is **sysname**, with a default of `public`. The caller of the function must be a member of **database_principal**, **dbo**, or the **db_owner** fixed database role in order to call the function successfully.

## Return code values

`>= 0` (success), or `< 0` (failure).

| Value | Result |
| --- | --- |
| `0` | Lock was successfully released. |
| `-999` | Indicates parameter validation or other call error. |

## Remarks

When an application calls `sp_getapplock` multiple times for the same lock resource, `sp_releaseapplock` must be called the same number of times to release the lock.

When the server shuts down for any reason, the locks are released.

## Permissions

Requires membership in the **public** role.

## Examples

The following example releases the lock associated with the current transaction on the resource `Form1` in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO

EXEC sp_getapplock @DbPrincipal = 'dbo',
    @Resource = 'Form1',
    @LockMode = 'Shared';

EXEC sp_releaseapplock @DbPrincipal = 'dbo',
    @Resource = 'Form1';
GO
```

## Related content

- [APPLOCK_MODE (Transact-SQL)](../../t-sql/functions/applock-mode-transact-sql.md)
- [APPLOCK_TEST (Transact-SQL)](../../t-sql/functions/applock-test-transact-sql.md)
- [sp_getapplock (Transact-SQL)](sp-getapplock-transact-sql.md)
