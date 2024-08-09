---
title: "DROP ASSEMBLY (Transact-SQL)"
description: DROP ASSEMBLY removes an assembly and all its associated files from the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/02/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP ASSEMBLY"
  - "DROP_ASSEMBLY_TSQL"
helpviewer_keywords:
  - "removing assemblies"
  - "DROP ASSEMBLY statement"
  - "deleting assemblies"
  - "assemblies [CLR integration], removing"
  - "dropping assemblies"
  - "WITH NO DEPENDENTS option"
dev_langs:
  - "TSQL"
---
# DROP ASSEMBLY (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]

Removes an assembly and all its associated files from the current database. Assemblies are created by using [CREATE ASSEMBLY](create-assembly-transact-sql.md) and modified by using [ALTER ASSEMBLY](alter-assembly-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DROP ASSEMBLY [ IF EXISTS ] assembly_name [ , ...n ]
[ WITH NO DEPENDENTS ]
[ ; ]
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### IF EXISTS

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.

Conditionally drops the assembly only if it already exists.

#### *assembly_name*

The name of the assembly you want to drop.

#### WITH NO DEPENDENTS

If specified, `WITH NO DEPENDENTS` drops only *assembly_name*, and none of the dependent assemblies referenced by the assembly. If not specified, `DROP ASSEMBLY` drops *assembly_name* and all dependent assemblies.

## Remarks

Dropping an assembly removes an assembly and all its associated files, such as source code and debug files, from the database.

If `WITH NO DEPENDENTS` isn't specified, `DROP ASSEMBLY` drops *assembly_name* and all dependent assemblies. If an attempt to drop any dependent assemblies fails, `DROP ASSEMBLY` returns an error.

`DROP ASSEMBLY` returns an error if the assembly is referenced by another assembly that exists in the database or if it's used by common language runtime (CLR) functions, procedures, triggers, user-defined types, or aggregates in the current database.

`DROP ASSEMBLY` doesn't interfere with any code referencing the assembly that is currently running. However, after `DROP ASSEMBLY` executes, any attempts to invoke the assembly code will fail.

## Permissions

Requires ownership of the assembly, or `CONTROL` permission on it.

## Examples

The following example assumes the assembly `HelloWorld` is already created in the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

```sql
DROP ASSEMBLY Helloworld;
```

## Related content

- [CREATE ASSEMBLY (Transact-SQL)](create-assembly-transact-sql.md)
- [ALTER ASSEMBLY (Transact-SQL)](alter-assembly-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
- [Get information about assemblies](../../relational-databases/clr-integration/assemblies-getting-information.md)
