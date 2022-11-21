---
title: "Implementing IDENTITY in a Memory-Optimized Table"
description: Learn about IDENTITY in memory-optimized tables in SQL Server. Memory-optimized tables support IDENTITY for a seed and increment value of one.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.assetid: c0a704a3-3a31-4c2c-b967-addacda62ef8
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Implementing IDENTITY in a Memory-Optimized Table
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

IDENTITY is supported on a memory-optimized table, as long as the seed and increment are both 1 (which is the default). Identity columns with definition of IDENTITY(x, y) where x != 1 or y != 1 are not supported on memory-optimized tables.   
    
To increase the IDENTITY seed, insert a new row with an explicit value for the identity column, using the session option `SET IDENTITY_INSERT table_name ON`. With the insert of the row, the IDENTITY seed is changed to the explicitly inserted value, plus 1. For example, to increase the seed to 1000, insert a row with value 999 in the identity column. Generated identity values will then start at 1000.     
  
## See Also  
 [Migrating to In-Memory OLTP](./plan-your-adoption-of-in-memory-oltp-features-in-sql-server.md)  
  
