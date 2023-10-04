---
title: "String operators (Transact-SQL)"
description: "String operators (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: jopilov
ms.date: 06/06/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "operators [Transact-SQL], string concatenation"
  - "plus sign (+)"
  - "string concatenation operators"
  - "+ (string concatenation)"
dev_langs:
  - "TSQL"
---
# String operators (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the following string operators. String concatenation operators can combine two or more of the following data types into one expression:

- character or binary strings
- columns
- combination of strings and column names

Wildcard string operators can match one or more characters in a string comparison operation. `LIKE` and `PATINDEX` are examples of two of these operations.

## In this section

- [= (String comparison or assignment)](string-comparison-assignment.md)
- [+ (String concatenation)](string-concatenation-transact-sql.md)
- [+= (String concatenation assignment)](string-concatenation-equal-transact-sql.md)
- [% (Wildcard - character(s) to match)](percent-character-wildcard-character-s-to-match-transact-sql.md)
- [&#91; &#93; (Wildcard - character(s) to match)](wildcard-character-s-to-match-transact-sql.md)
- [&#91;^&#93; (Wildcard - character(s) not to match)](wildcard-character-s-not-to-match-transact-sql.md)
- [_ (Wildcard - match one character)](wildcard-match-one-character-transact-sql.md)
