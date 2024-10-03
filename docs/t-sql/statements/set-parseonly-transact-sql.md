---
title: "SET PARSEONLY (Transact-SQL)"
description: "Examines the syntax of each Transact-SQL statement and returns any error messages without compiling or executing the statement."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 06/06/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "PARSEONLY_TSQL"
  - "SET_PARSEONLY_TSQL"
  - "PARSEONLY"
  - "SET PARSEONLY"
helpviewer_keywords:
  - "parsing [SQL Server], SET PARSEONLY statement"
  - "checking syntax"
  - "PARSEONLY option"
  - "syntax [SQL Server], verifying"
  - "verifying syntax"
  - "SET PARSEONLY statement"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=fabric"
---
# SET PARSEONLY (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Examines the syntax of each [!INCLUDE[tsql](../../includes/tsql-md.md)] statement and returns any error messages without compiling or executing the statement.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SET PARSEONLY { ON | OFF }
[ ; ]
```

## Remarks

When `SET PARSEONLY` is `ON`, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] only parses the statement. When `SET PARSEONLY` is `OFF`, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] compiles and executes the statement.

The setting of `SET PARSEONLY` is set at parse time and not at execute or run time.

Don't use `PARSEONLY` in a stored procedure or a trigger. `SET PARSEONLY` returns offsets if the `OFFSETS` option is `ON` and no errors occur.

## Permissions

Requires membership in the **public** role.

## See also

- [SET Statements (Transact-SQL)](../../t-sql/statements/set-statements-transact-sql.md)
- [SET OFFSETS (Transact-SQL)](../../t-sql/statements/set-offsets-transact-sql.md)
