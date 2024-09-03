---
title: "CONCAT_WS (Transact-SQL)"
description: This function returns a string resulting from the concatenation, or joining, of two or more string values in an end-to-end manner, using a string separator.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 09/22/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CONCAT_WS"
  - "CONCAT_WS_TSQL"
helpviewer_keywords:
  - "CONCAT_WS function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =azure-sqldw-latest || =fabric"
---
# CONCAT_WS (Transact-SQL)

[!INCLUDE [sqlserver2017-asdb-asdbmi-asa-fabricse-fabricdw](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi-asa-fabricse-fabricdw.md)]

This function returns a string resulting from the concatenation, or joining, of two or more string values in an end-to-end manner. It separates those concatenated string values with the delimiter specified in the first function argument. (`CONCAT_WS` indicates *concatenate with separator*.)

## Syntax

```syntaxsql
CONCAT_WS ( separator , argument1 , argument2 [ , argumentN ] ... )
```

## Arguments

#### *separator*

An expression of any character type (**char**, **nchar**, **nvarchar**, or **varchar**).

#### *argument1*, *argument2* [ , *argumentN* ]

An expression of any string value. The `CONCAT_WS` function requires at least two arguments, and no more than 254 arguments.

## Return types

A string value whose length and type depend on the input.

## Remarks

`CONCAT_WS` takes a variable number of string arguments and concatenates (or joins) them into a single string. It separates those concatenated string values with the delimiter specified in the first function argument. `CONCAT_WS` requires a separator argument and a minimum of two other string value arguments; otherwise, `CONCAT_WS` raises an error. `CONCAT_WS` implicitly converts all arguments to string types before concatenation.

The implicit conversion to strings follows the existing rules for data type conversions. For more information about behavior and data type conversions, see [CONCAT (Transact-SQL)](concat-transact-sql.md).

### Treatment of NULL values

`CONCAT_WS` ignores the `SET CONCAT_NULL_YIELDS_NULL { ON | OFF }` setting.

If `CONCAT_WS` receives arguments with all `NULL` values, it returns an empty string of type **varchar(1)**.

`CONCAT_WS` ignores null values during concatenation, and doesn't add the separator between null values. Therefore, `CONCAT_WS` can cleanly handle concatenation of strings that might have "blank" values - for example, a second address field. For more information, see [Example B](#b-skip-null-values).

If a scenario involves null values separated by a delimiter, consider the [ISNULL](isnull-transact-sql.md) function. For more information, see [Example C](#c-generate-csv-formatted-data-from-table).

## Examples

### A. Concatenate values with separator

This example concatenates three columns from the `sys.databases` table, separating the values with a hyphen surrounded by spaces (` - `).

```sql
SELECT CONCAT_WS(' - ', database_id, recovery_model_desc, containment_desc) AS DatabaseInfo
FROM sys.databases;
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
DatabaseInfo
-----------------
1 - SIMPLE - NONE
2 - SIMPLE - NONE
3 - FULL - NONE
4 - SIMPLE - NONE
```

### B. Skip NULL values

This example ignores `NULL` values in the arguments list, and uses a comma separator value (`,`).

```sql
SELECT CONCAT_WS(',', '1 Microsoft Way', NULL, NULL, 'Redmond', 'WA', 98052) AS Address;
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
Address
--------------------------------
1 Microsoft Way,Redmond,WA,98052
```

### C. Generate CSV-formatted data from table

This example uses a comma separator value (`,`), and adds the carriage return character `CHAR(13)` in the column separated values format of the result set.

```sql
SELECT STRING_AGG(CONCAT_WS(',', database_id, recovery_model_desc, containment_desc), CHAR(13)) AS DatabaseInfo
FROM sys.databases;
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
DatabaseInfo
-------------
1,SIMPLE,NONE
2,SIMPLE,NONE
3,FULL,NONE
4,SIMPLE,NONE
```

`CONCAT_WS` ignores `NULL` values in the columns. Wrap a nullable column with the `ISNULL` function, and provide a default value. For example:

```sql
SELECT STRING_AGG(
    CONCAT_WS(',', database_id, ISNULL(recovery_model_desc, ''),
    ISNULL(containment_desc, 'N/A')
    ), CHAR(13)) AS DatabaseInfo
FROM sys.databases;
```

## Related content

- [CONCAT (Transact-SQL)](concat-transact-sql.md)
- [FORMATMESSAGE (Transact-SQL)](formatmessage-transact-sql.md)
- [QUOTENAME (Transact-SQL)](quotename-transact-sql.md)
- [REPLACE (Transact-SQL)](replace-transact-sql.md)
- [REVERSE (Transact-SQL)](reverse-transact-sql.md)
- [STRING_AGG (Transact-SQL)](string-agg-transact-sql.md)
- [STRING_ESCAPE (Transact-SQL)](string-escape-transact-sql.md)
- [STUFF (Transact-SQL)](stuff-transact-sql.md)
- [TRANSLATE (Transact-SQL)](translate-transact-sql.md)
- [String Functions (Transact-SQL)](string-functions-transact-sql.md)
