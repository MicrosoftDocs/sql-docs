---
title: "MSSQLSERVER_41307 | Microsoft Docs"
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
  - "41307 (Database Engine error)"
ms.assetid: 56f56410-b07d-4379-b01c-702c95761070
caps.latest.revision: 9
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_41307
  
## Details  
  
|||  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41307|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|HK_HEKATON_ROW_LIMIT|  
|Message Text|The row size limit of *number* bytes for memory optimized tables has been exceeded. Please simplify the table definition.|  
  
## Explanation  
The row size limit for memory optimized tables is 8,060 bytes. For more information, see [Table and Row Size in Memory-Optimized Tables](~/relational-databases/in-memory-oltp/table-and-row-size-in-memory-optimized-tables.md). For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
  
## See Also  
[In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
