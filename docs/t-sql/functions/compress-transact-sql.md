---
title: "COMPRESS (Transact-SQL)"
description: "The COMPRESS function compresses the input expression, using the Gzip algorithm."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/09/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "COMPRESS"
  - "COMPRESS_TSQL"
helpviewer_keywords:
  - "COMPRESS function"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqledge-current || = azure-sqldw-latest||=fabric"
---
# COMPRESS (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw.md)]

This function compresses the input expression, using the **Gzip** algorithm. The function returns a byte array of type **varbinary(max)**.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
COMPRESS ( expression )
```

## Arguments

### *expression*  

An expression of one of the following data types:

- **binary(*n*)**
- **char(*n*)**
- **nchar(*n*)**
- **nvarchar(max)**
- **nvarchar(*n*)**
- **varbinary(max)**
- **varbinary(*n*)**
- **varchar(max)**
- **varchar(*n*)**

For more information, see [Expressions (Transact-SQL)](../../t-sql/language-elements/expressions-transact-sql.md).

## Return type

**varbinary(max)**, representing the compressed content of the input.

## Remarks

Compressed data can't be indexed.

The `COMPRESS` function compresses the input expression data. You must invoke this function for each data section to compress. For more information about automatic data compression during storage at the row or page level, see [Data Compression](../../relational-databases/data-compression/data-compression.md).

## Examples

### A. Compress data during table insert

This example shows how to compress data inserted into a table:

```sql
INSERT INTO player (
    name,
    surname,
    info
    )
VALUES (
    N'Ovidiu',
    N'Cracium',
    COMPRESS(N'{"sport":"Tennis","age": 28,"rank":1,"points":15258, turn":17}')
    );

INSERT INTO player (
    name,
    surname,
    info
    )
VALUES (
    N'Michael',
    N'Raheem',
    COMPRESS(@info)
    );
```

### B. Archive compressed version of deleted rows

This statement first deletes old player records from the `player` table. To save space, it then stores the records in the `inactivePlayer` table, in a compressed format.

```sql
DELETE
FROM player
OUTPUT deleted.id,
    deleted.name,
    deleted.surname,
    deleted.datemodifier,
    COMPRESS(deleted.info)
INTO dbo.inactivePlayers
WHERE datemodified < @startOfYear;
```

## See also

- [String Functions (Transact-SQL)](../../t-sql/functions/string-functions-transact-sql.md)
- [DECOMPRESS (Transact-SQL)](../../t-sql/functions/decompress-transact-sql.md)
