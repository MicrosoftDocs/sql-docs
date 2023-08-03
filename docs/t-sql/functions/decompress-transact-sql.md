---
title: "DECOMPRESS (Transact-SQL)"
description: "DECOMPRESS function decompresses an input expression value, using the Gzip algorithm."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/09/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DECOMPRESS"
  - "DECOMPRESS_TSQL"
helpviewer_keywords:
  - "DECOMPRESS function"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqledge-current || = azure-sqldw-latest ||=fabric"
---
# DECOMPRESS (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw.md)]

This function decompresses an input expression value, using the **Gzip** algorithm. `DECOMPRESS` returns a byte array in the **varbinary(max)** data type.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DECOMPRESS ( expression )
```

## Arguments

#### *expression*

A **varbinary(*n*)**, **varbinary(max)**, or **binary(*n*)** value. For more information, see [Expressions (Transact-SQL)](../../t-sql/language-elements/expressions-transact-sql.md).

## Return types

A value of data type **varbinary(max)**. `DECOMPRESS` uses the **Gzip** algorithm to decompress the input argument. You should explicitly cast the result to a target type if necessary.

## Remarks

## Examples

### A. Decompress Data at Query Time

This example shows how to return compressed table data:

```sql
SELECT _id,
    name,
    surname,
    datemodified,
    CAST(DECOMPRESS(info) AS NVARCHAR(MAX)) AS info
FROM player;
```

### B. Display compressed data using computed column

> [!NOTE]  
> This example does not apply to Azure Synapse Analytics.

This example shows how to create a table for decompressed data storage:

```sql
CREATE TABLE example_table (
    _id INT PRIMARY KEY IDENTITY,
    name NVARCHAR(MAX),
    surname NVARCHAR(MAX),
    info VARBINARY(MAX),
    info_json AS CAST(DECOMPRESS(info) AS NVARCHAR(MAX))
);
```

## See also

- [String Functions (Transact-SQL)](../../t-sql/functions/string-functions-transact-sql.md)
- [COMPRESS (Transact-SQL)](../../t-sql/functions/compress-transact-sql.md)
