---
title: "MSSQLSERVER_41333 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "41333 (Database Engine error)"
ms.assetid: c3c3ae9a-1e4c-4de6-ba72-2f393375b053
caps.latest.revision: 7
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_41333
  
## Details  
  
|||  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41333|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|CROSS_CONTAINER_ISOLATION_FAILURE|  
|Message Text|The following transactions must access memory optimized tables and natively compiled stored procedures under snapshot isolation: RepeatableRead transactions, Serializable transactions, and transactions that access tables that are not memory optimized in RepeatableRead or Serializable isolation.|  
  
## Explanation  
There are restrictions against the user of the higher isolation levels between disk based transactions and XTP transactions.  
  
## User Action  
Don't attempt high level isolation operations on memory-optimized tables (and natively compiled procedures) and disk based tables.  
  
For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
  
## See Also  
[In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
