---
title: "Predicates"
description: "Predicates"
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/25/2022
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
---
# Predicates

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

A *predicate* is an expression that evaluates to TRUE, FALSE, or UNKNOWN. Predicates are used in the search condition of [WHERE](../../t-sql/queries/where-transact-sql.md) clauses and [HAVING](../../t-sql/queries/select-having-transact-sql.md) clauses, the join conditions of [FROM](../../t-sql/queries/from-transact-sql.md) clauses, and other constructs where a Boolean value is required.


[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the following predicates:

- [CONTAINS](contains-transact-sql.md)
- [FREETEXT](freetext-transact-sql.md)
- [IS &#91;NOT&#93; DISTINCT FROM](is-distinct-from-transact-sql.md)
- [IS &#91;NOT&#93; NULL](is-null-transact-sql.md)

## See also

- [BETWEEN &#40;Transact-SQL&#41;](../../t-sql/language-elements/between-transact-sql.md)
- [EXISTS &#40;Transact-SQL&#41;](../../t-sql/language-elements/exists-transact-sql.md)
- [IN &#40;Transact-SQL&#41;](../../t-sql/language-elements/in-transact-sql.md)
- [LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)
- [Search Condition &#40;Transact-SQL&#41;](../../t-sql/queries/search-condition-transact-sql.md)
