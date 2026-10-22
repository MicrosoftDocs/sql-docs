---
title: "Logical Operators (Transact-SQL)"
description: Logical operators test for the truth of some condition.
author: uc-msft
ms.author: umajay
ms.date: 01/29/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "operators [Transact-SQL], logical"
  - "testing truth"
  - "truth testing"
  - "TRUE"
  - "FALSE"
  - "logical operators [SQL Server], Transact-SQL"
dev_langs:
  - "TSQL"
---
# Logical operators (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Logical operators test for the truth of some condition. Logical operators, like comparison operators, return a **Boolean** data type with a value of `TRUE`, `FALSE`, or `UNKNOWN`.

| Operator | Meaning |
| --- | --- |
| [ALL](all-transact-sql.md) | `TRUE` if all of a set of comparisons are `TRUE`. |
| [AND](and-transact-sql.md) | `TRUE` if both Boolean expressions are `TRUE`. |
| [ANY](any-transact-sql.md) | `TRUE` if any one of a set of comparisons are `TRUE`. |
| [BETWEEN](between-transact-sql.md) | `TRUE` if the operand is within a range. |
| [EXISTS](exists-transact-sql.md) | `TRUE` if a subquery contains any rows. |
| [IN](in-transact-sql.md) | `TRUE` if the operand is equal to one of a list of expressions. |
| [LIKE](like-transact-sql.md) | `TRUE` if the operand matches a pattern. |
| [NOT](not-transact-sql.md) | Reverses the value of any other Boolean operator. |
| [OR](or-transact-sql.md) | `TRUE` if either Boolean expression is `TRUE`. |
| [SOME &#124; ANY](some-any-transact-sql.md) | `TRUE` if some of a set of comparisons are `TRUE`. |

## Related content

- [Operator precedence (Transact-SQL)](operator-precedence-transact-sql.md)
