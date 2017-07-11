---
title: "Cursor Functions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "functions [SQL Server], cursors"
  - "cursor functions"
ms.assetid: 7d9daa10-4c50-4212-9400-42120222b2b8
caps.latest.revision: 27
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Cursor Functions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  The following scalar functions return information about cursors:  
  
|||  
|-|-|  
|[@@CURSOR_ROWS](../../t-sql/functions/cursor-rows-transact-sql.md)|[CURSOR_STATUS](../../t-sql/functions/cursor-status-transact-sql.md)|  
|[@@FETCH_STATUS](../../t-sql/functions/fetch-status-transact-sql.md)||  
  
 All cursor functions are nondeterministic. This means these functions do not always return the same results every time they are called, even with the same set of input values. For more information about function determinism, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
## See Also  
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)  
  
  
