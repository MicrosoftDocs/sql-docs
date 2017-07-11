---
title: "Implementing IDENTITY in a Memory-Optimized Table | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-imoltp"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: c0a704a3-3a31-4c2c-b967-addacda62ef8
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Implementing IDENTITY in a Memory-Optimized Table
[!INCLUDE[tsql-appliesto-ss2014-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2014-asdb-xxxx-xxx-md.md)]

IDENTITY is supported on a memory-optimized table, as long as the seed and increment are both 1 (which is the default). Identity columns with definition of IDENTITY(x, y) where x != 1 or y != 1 are not supported on memory-optimized tables.   
    
To increase the IDENTITY seed, insert a new row with an explicit value for the identity column, using the session option `SET IDENTITY_INSERT table_name ON`. With the insert of the row, the IDENTITY seed is changed to the explicitly inserted value, plus 1. For example, to increase the seed to 1000, insert a row with value 999 in the identity column. Generated identity values will then start at 1000.     
  
## See Also  
 [Migrating to In-Memory OLTP](../../relational-databases/in-memory-oltp/migrating-to-in-memory-oltp.md)  
  
  