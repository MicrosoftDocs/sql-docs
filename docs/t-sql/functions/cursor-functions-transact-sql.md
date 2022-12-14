---
title: "Cursor Functions (Transact-SQL)"
description: "Cursor Functions (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "functions [SQL Server], cursors"
  - "cursor functions"
dev_langs:
  - "TSQL"
---
# Cursor Functions (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

These scalar functions return information about cursors:
  
- [@@CURSOR_ROWS](../../t-sql/functions/cursor-rows-transact-sql.md)
- [@@FETCH_STATUS](../../t-sql/functions/fetch-status-transact-sql.md)
- [CURSOR_STATUS](../../t-sql/functions/cursor-status-transact-sql.md)
  
All cursor functions are nondeterministic. In other words, these functions do not always return the same results each time they execute, even with the same set of input values. See [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md) for more information about function determinism.
  
## See also

[Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)
