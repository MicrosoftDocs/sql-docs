---
title: "Relational operators (Transact-SQL)"
description: A relational operator is a syntax element that can accept one or more named or unnamed input parameters and returns a result set.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/26/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "Relational operators [Transact-SQL], about operators"
  - "Relational operators [Transact-SQL]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# Relational operators (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

A relational operator is a syntax element that can accept one or more named or unnamed input parameters and returns a result set. Relational operators are used as table source in a DML statement.

SQL Server implements the following relational operators:

- [GENERATE_SERIES (Transact-SQL)](../functions/generate-series-transact-sql.md)
- [OPENDATASOURCE (Transact-SQL)](../functions/opendatasource-transact-sql.md)
- [OPENQUERY (Transact-SQL)](../functions/openquery-transact-sql.md)
- [OPENROWSET (Transact-SQL)](../functions/openrowset-transact-sql.md)
- [OPENXML (Transact-SQL)](../functions/openxml-transact-sql.md)
- [OPENJSON (Transact-SQL)](../functions/openjson-transact-sql.md)
- [PREDICT (Transact-SQL)](../queries/predict-transact-sql.md)
- [STRING_SPLIT (Transact-SQL)](../functions/string-split-transact-sql.md)

## Use

Use a relational operator like a [table-valued function](../statements/create-function-transact-sql.md#c-create-a-multi-statement-table-valued-function) in a query or T-SQL statement.

## Related content

- [What are the SQL database functions?](../functions/functions.md)
