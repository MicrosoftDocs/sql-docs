---
title: "Cursor Functions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "functions [SQL Server], cursors"
  - "cursor functions"
ms.assetid: 7d9daa10-4c50-4212-9400-42120222b2b8
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Cursor Functions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

These scalar functions return information about cursors:
  
|||  
|-|-|  
|[@@CURSOR_ROWS](../../t-sql/functions/cursor-rows-transact-sql.md)|[CURSOR_STATUS](../../t-sql/functions/cursor-status-transact-sql.md)|  
|[@@FETCH_STATUS](../../t-sql/functions/fetch-status-transact-sql.md)||  
  
All cursor functions are nondeterministic. In other words, these functions do not always return the same results each time they execute, even with the same set of input values. See [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md) for more information about function determinism.
  
## See also
[Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)
  
  
