---
title: sp_prepare (Transact SQL)
description: Prepares a parameterized Transact-SQL statement and returns a statement handle for execution.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/30/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_prepare_TSQL"
  - "sp_prepare"
helpviewer_keywords:
  - "sp_prepare"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_prepare (Transact SQL)

[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

Prepares a parameterized [!INCLUDE [tsql](../../includes/tsql-md.md)] statement and returns a statement *handle* for execution. `sp_prepare` is invoked by specifying `ID = 11` in a tabular data stream (TDS) packet.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_prepare
    handle OUTPUT
    , params
    , stmt
    , options
[ ; ]
```

## Arguments

#### *handle*

A SQL Server-generated *prepared handle* identifier. *handle* is a required parameter with an **int** return value.

#### *params*

Identifies parameterized statements. *params* is a required OUTPUT parameter that calls for an **ntext**, **nchar**, or **nvarchar** input value. The *params* definition of variables is substituted for parameter markers in the statement. Input a `NULL` value if the statement isn't parameterized.

#### *stmt*

Defines the cursor result set. The *stmt* parameter is required and calls for an **ntext**, **nchar**, or **nvarchar** input value.

#### *options*

An optional parameter that returns a description of the cursor result set columns. *options* requires the following input value:

| Value | Description |
| --- | --- |
| `0x0001` | RETURN_METADATA |

## Examples

### A. Prepare and execute a statement

The following example prepares and executes a basic Transact-SQL statement.

```sql
DECLARE @handle INT;

EXEC sp_prepare @handle OUTPUT,
    N'@P1 NVARCHAR(128), @P2 NVARCHAR(100)',
    N'SELECT database_id, name FROM sys.databases WHERE name=@P1 AND state_desc = @P2';

EXEC sp_execute @handle,
    N'tempdb',
    N'ONLINE';

EXEC sp_unprepare @handle;
```

### B. Prepare and execute a statement using the handle

The following example prepares a statement in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database, and later executes it using the handle.

```sql
-- Prepare query
DECLARE @handle INT;

EXEC sp_prepare @handle OUTPUT,
    N'@Param INT',
    N'SELECT *
FROM Sales.SalesOrderDetail AS sod
INNER JOIN Production.Product AS p ON sod.ProductID = p.ProductID
WHERE SalesOrderID = @Param
ORDER BY Style DESC;';

-- Return handle for calling application
SELECT @handle;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
1
```

Execute the query twice using the handle value `1`, before discarding the prepared plan.

```sql
EXEC sp_execute 1, 49879;
GO

EXEC sp_execute 1, 48766;
GO

EXEC sp_unprepare 1;
GO
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
