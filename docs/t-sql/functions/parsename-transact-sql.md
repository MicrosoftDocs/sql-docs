---
title: "PARSENAME (Transact-SQL)"
description: Returns the specified part of an object name. The parts of an object that can be retrieved are the object name, schema name, database name, and server name.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 05/10/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "PARSENAME_TSQL"
  - "PARSENAME"
helpviewer_keywords:
  - "PARSENAME function"
  - "parsing [SQL Server], PARSENAME function"
  - "names [SQL Server], objects"
  - "objects [SQL Server], names"
  - "part of object names [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# PARSENAME (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns the specified part of an object name. The parts of an object that can be retrieved are the object name, schema name, database name, and server name.

`PARSENAME` doesn't indicate whether an object by the specified name exists. `PARSENAME` just returns the specified part of the specified object name.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
PARSENAME ('object_name' , object_piece )
```

## Arguments

#### '*object_name*'

The parameter that holds the name of the object for which to retrieve the specified object part. This parameter is an optionally qualified object name. If all parts of the object name are qualified, this name can have four parts: the server name, the database name, the schema name, and the object name.

Each part of the '*object_name*' string is **sysname**, which is equivalent to **nvarchar(128)** or 256 bytes. If any part of the string exceeds 256 bytes, `PARSENAME` returns `NULL` for that part, as it's not a valid **sysname**.

#### *object_piece*

The object part to return. *object_piece* is **int**, and can be one of these values:

| Value | Description |
| --- | --- |
| 1 | Object name |
| 2 | Schema name |
| 3 | Database name |
| 4 | Server name |

## Return types

**sysname**

## Remarks

`PARSENAME` returns `NULL` if one of the following conditions is true:

- Either *object_name* or *object_piece* is `NULL`.

- A syntax error occurs.

- The requested object part has a length of `0` and isn't a valid [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] identifier. A zero-length object name renders the complete qualified name as not valid.

## Examples

The following example uses `PARSENAME` to return information about the `Person` table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
-- Uses AdventureWorks

SELECT PARSENAME('AdventureWorks2022.Person.Person', 1) AS 'Object Name';
SELECT PARSENAME('AdventureWorks2022.Person.Person', 2) AS 'Schema Name';
SELECT PARSENAME('AdventureWorks2022.Person.Person', 3) AS 'Database Name';
SELECT PARSENAME('AdventureWorks2022.Person.Person', 4) AS 'Server Name';
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Object Name
------------------------------
Person

Schema Name
------------------------------
Person

Database Name
------------------------------
AdventureWorks2022

Server Name
------------------------------
(null)
```

## Related content

- [QUOTENAME (Transact-SQL)](quotename-transact-sql.md)
- [ALTER TABLE (Transact-SQL)](../statements/alter-table-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](../statements/create-table-transact-sql.md)
- [System Functions by category for Transact-SQL](../../relational-databases/system-functions/system-functions-category-transact-sql.md)
