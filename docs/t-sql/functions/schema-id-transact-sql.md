---
title: "SCHEMA_ID (Transact-SQL)"
description: SCHEMA_ID returns the schema ID associated with a schema name.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/18/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SCHEMA_ID"
  - "SCHEMA_ID_TSQL"
helpviewer_keywords:
  - "identification numbers [SQL Server], schemas"
  - "schemas [SQL Server], IDs"
  - "SCHEMA_ID function"
  - "IDs [SQL Server], schemas"
  - "default schema IDs"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# SCHEMA_ID (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns the schema ID associated with a schema name.

Database schemas act as namespaces or containers for objects, such as tables, views, procedures, and functions, that can be found in the `sys.objects` catalog view.

Each schema has an owner. The owner is a security [principal](../../relational-databases/security/authentication-access/principals-database-engine.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SCHEMA_ID ( [ schema_name ] )
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *schema_name*

The name of the schema. *schema_name* is **sysname**. If *schema_name* isn't specified, `SCHEMA_ID` returns the ID of the default schema of the caller.

## Return types

**int**

`NULL` is returned if *schema_name* isn't a valid schema.

## Remarks

`SCHEMA_ID` returns IDs of system schemas and user-defined schemas. `SCHEMA_ID` can be called in a select list, in a `WHERE` clause, and anywhere an expression is allowed.

## Examples

### A. Return the default schema ID of a caller

```sql
SELECT SCHEMA_ID();
```

### B. Return the schema ID of a named schema

```sql
SELECT SCHEMA_ID('dbo');
```

## Related content

- [Metadata Functions (Transact-SQL)](metadata-functions-transact-sql.md)
- [SCHEMA_NAME (Transact-SQL)](schema-name-transact-sql.md)
- [sys.schemas (Transact-SQL)](../../relational-databases/system-catalog-views/schemas-catalog-views-sys-schemas.md)
