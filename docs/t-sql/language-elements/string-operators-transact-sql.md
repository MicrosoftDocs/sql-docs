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
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "operators [Transact-SQL], string concatenation"
  - "plus sign (+)"
  - "string concatenation operators"
  - "+ (string concatenation)"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# String operators (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the following string operators. String concatenation operators can combine two or more of the following data types into one expression:

- character or binary strings
- columns
- combination of strings and column names

Wildcard string operators can match one or more characters in a string comparison operation. `LIKE` and `PATINDEX` are examples of two of these operations.

## In this section

- [= (String comparison or assignment)](string-comparison-assignment.md)
- [+ (String concatenation)](string-concatenation-transact-sql.md)
- [+= (String concatenation assignment)](string-concatenation-equal-transact-sql.md)
- [% (wildcard - characters to match)](percent-character-wildcard-character-s-to-match-transact-sql.md)
- [\[\] (wildcard - characters to match)](wildcard-character-s-to-match-transact-sql.md)
- [\[^\] (Wildcard - character(s) not to match)](wildcard-character-s-not-to-match-transact-sql.md)
- [_ (Wildcard - match one character)](wildcard-match-one-character-transact-sql.md)
