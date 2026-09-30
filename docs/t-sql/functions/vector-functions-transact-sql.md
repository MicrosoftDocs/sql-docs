---
title: "Vector Functions (Transact-SQL)"
description: Vector functions perform operations on vector type allowing applications to store and manipulate vectors in SQL Server.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: pookam, randolphwest
ms.date: 09/15/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.collection:
  - ce-skilling-ai-copilot
ms.update-cycle: 180-days
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "vector search, system functions"
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# Vector functions

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

The following scalar functions perform operations on [vectors](../../sql-server/ai/vectors.md) in binary format, allowing applications to store and manipulate vectors in the SQL Database Engine.

All vector functions support the [**vector** data type](../data-types/vector-data-type.md).

| Function | Description |
| --- | --- |
| [VECTOR_DISTANCE](vector-distance-transact-sql.md) | Calculates the distance between two vectors using a specified distance metric. |
| [VECTOR_SEARCH](vector-search-transact-sql.md) | Return the closest vectors to a given query vector and distance metric using an approximate vector search algorithm. |
| [VECTOR_NORM](vector-norm-transact-sql.md) | Takes a vector as an input and returns the norm of the vector (which is a measure of its length or magnitude) in a given [norm type](https://mathworld.wolfram.com/VectorNorm.html). |
| [VECTOR_NORMALIZE](vector-normalize-transact-sql.md) | Takes a vector as an input and returns the normalized vector, which is a vector scaled to have a length of 1 in a given [norm type](https://mathworld.wolfram.com/VectorNorm.html). Adjusts a vector so that its length is normalized following the rules of specified norm type. |
| [VECTORPROPERTY](vectorproperty-transact-sql.md) | Returns specific properties of a given vector. |

## Platform availability

- `VECTOR_SEARCH` is generally available (GA) in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [fabric-sqldb-md](../../includes/fabric-sqldb.md)], and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] with the **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy). It's in preview in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] with the **SQL Server 2025** update policy.
- The [vector data type](/sql/t-sql/data-types/vector-data-type?view=azuresqlmi-current&preserve-view=true) and [vector functions](/sql/t-sql/functions/vector-functions-transact-sql?view=azuresqlmi-current&preserve-view=true) are generally available in SQL Server 2025, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric. 
- [Vector indexes](/sql/t-sql/statements/create-vector-index-transact-sql?view=azuresqlmi-current&preserve-view=true) are generally available in Azure SQL Database, SQL database in Fabric, and Azure SQL Managed Instance in the **Always up to date** [update policy](/azure/azure-sql/managed-instance/update-policy?view=azuresql-mi&preserve-view=true).
- [Vector indexes](/sql/t-sql/statements/create-vector-index-transact-sql?view=azuresqlmi-current&preserve-view=true) are preview features in SQL Server 2025 and Azure SQL Managed Instance in the **SQL Server 2025** [update policy](/azure/azure-sql/managed-instance/update-policy?view=azuresql-mi&preserve-view=true).

## Related content

- [Vector data type](../data-types/vector-data-type.md)
- [Vector search and vector indexes in the SQL Database Engine](../../sql-server/ai/vectors.md)
- [Intelligent applications and AI](/azure/azure-sql/database/ai-artificial-intelligence-intelligent-applications)
