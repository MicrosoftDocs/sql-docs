---
title: "JSON_PATH_EXISTS (Transact-SQL)"
description: JSON_PATH_EXISTS tests whether a specified SQL/JSON path exists in the input JSON string.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: umajay, jovanpop
ms.date: 07/23/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-ver16 || >=sql-server-linux-ver16 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# JSON_PATH_EXISTS (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asmi-asa-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asmi-asa-fabricse-fabricdw-fabricsqldb.md)]

The `JSON_PATH_EXISTS` syntax tests whether a specified SQL/JSON path exists in the input JSON string.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
JSON_PATH_EXISTS( value_expression , sql_json_path )
```

## Arguments

#### *value_expression*

A character expression.

#### *sql_json_path*

A valid SQL/JSON path to test in the input.

## Return value

Returns an int value of `1` or `0` or `NULL`. Returns `NULL` if the *value_expression* or input is a SQL `NULL` value. Returns `1` if the given SQL/JSON path exists in the input or returns a non-empty sequence. Returns `0` otherwise.

The `JSON_PATH_EXISTS` function doesn't return errors.

## Examples

### Example 1

The following example returns 1 since the input JSON string contains the specified SQL/JSON path. This example uses a nested path where the key is present in another object.

```sql
DECLARE @jsonInfo AS NVARCHAR (MAX);

SET @jsonInfo = N'{"info":{"address":[{"town":"Paris"},{"town":"London"}]}}';

SELECT JSON_PATH_EXISTS(@jsonInfo, '$.info.address');
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
1
```

### Example 2

The following example returns 0 since the input JSON string doesn't contain the specified SQL/JSON path.

```sql
DECLARE @jsonInfo AS NVARCHAR (MAX);

SET @jsonInfo = N'{"info":{"address":[{"town":"Paris"},{"town":"London"}]}}';

SELECT JSON_PATH_EXISTS(@jsonInfo, '$.info.addresses');
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
0
```

### Example 3

The following example uses `JSON_PATH_EXISTS()` with a wildcard:

```sql
DECLARE @jsonInfo AS NVARCHAR (MAX);

SET @jsonInfo = N'{"info":{"address":[{"town":"Paris"},{"town":"London"}]}}';

SELECT JSON_PATH_EXISTS(@jsonInfo, '$.info.address[*].town'); -- Returns: 1
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
1
```

The following looks for at least one element in array has an object with key `town`, and finds one.

```sql
SET @jsonInfo = N'{"info":{"address":[{"town":"Paris"},{"city":"London"}]}}';

SELECT JSON_PATH_EXISTS(@jsonInfo, '$.info.address[*].town'); -- Returns: 1  (at least one element in array has an object with key "town")
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
1
```

The following looks for at least one element in array has an object with key `town`, but finds none.

```sql
SET @jsonInfo = N'{"info":{"address":[{"city":"Paris"},{"city":"London"}]}}';

SELECT JSON_PATH_EXISTS(@jsonInfo, '$.info.address[*].town'); -- Returns: 0 (no elements in array has an object with key "town")
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
0
```

## Related content

- [JSON data in SQL Server](../../relational-databases/json/json-data-sql-server.md)
