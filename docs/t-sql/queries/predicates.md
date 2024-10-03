---
title: "Predicates"
description: Predicates are expressions, used in search conditions, that evaluate to TRUE, FALSE, or UNKNOWN.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/09/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "HAVING clause, predicates"
  - "FROM clause, predicates"
  - "unknown data [SQL Server]"
  - "TRUE"
  - "WHERE clause, predicates"
  - "FALSE"
  - "predicates [full-text search]"
  - "expressions [SQL Server], predicates"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# Predicates

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

A *predicate* is an expression that evaluates to `TRUE`, `FALSE`, or `UNKNOWN`. Predicates are used in the search condition of [WHERE](where-transact-sql.md) clauses and [HAVING](select-having-transact-sql.md) clauses, the join conditions of [FROM](from-transact-sql.md) clauses, and other constructs where a Boolean value is required.

For more information, including how to specify a search condition, see [Search condition](search-condition-transact-sql.md).

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides the following predicates:

- [CONTAINS (Transact-SQL)](contains-transact-sql.md)
- [FREETEXT (Transact-SQL)](freetext-transact-sql.md)
- [IS &#91;NOT&#93; DISTINCT FROM (Transact-SQL)](is-distinct-from-transact-sql.md)
- [IS NULL (Transact-SQL)](is-null-transact-sql.md)

## Related content

- [BETWEEN (Transact-SQL)](../language-elements/between-transact-sql.md)
- [EXISTS (Transact-SQL)](../language-elements/exists-transact-sql.md)
- [IN (Transact-SQL)](../language-elements/in-transact-sql.md)
- [LIKE (Transact-SQL)](../language-elements/like-transact-sql.md)
- [Search condition (Transact-SQL)](search-condition-transact-sql.md)
