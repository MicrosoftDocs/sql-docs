---
title: "Rowset Functions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "functions [SQL Server], rowsets"
  - "rowsets [SQL Server], functions"
  - "rowsets [SQL Server]"
ms.assetid: ac24d700-3144-4ab5-9fa8-8c014001cc71
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Rowset Functions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  The following rowset functions return an object that can be used in place of a table reference in a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
|||  
|-|-|  
|[OPENDATASOURCE](../../t-sql/functions/opendatasource-transact-sql.md)|[OPENJSON](../../t-sql/functions/openjson-transact-sql.md)|  
|[OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md)|[OPENQUERY](../../t-sql/functions/openquery-transact-sql.md)|  
|[OPENXML](../../t-sql/functions/openxml-transact-sql.md)||  
  
 All rowset functions are nondeterministic. This means these functions do not always return the same results every time they are called, even with the same set of input values. For more information about function determinism, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
## See Also  
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)  
  
  
