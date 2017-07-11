---
title: "MSSQLSERVER_41325 | Microsoft Docs"
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
  - "41325 (Database Engine error)"
ms.assetid: 97c6a8bb-139a-44f3-9ed5-f46d047e8ed3
caps.latest.revision: 7
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_41325
  
## Details  
  
|||  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41325|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|HK_TX_COMMIT_SR_VALIDATION|  
|Message Text|The current transaction failed to commit due to a serializable validation failure.|  
  
## Explanation  
The transaction encountered a validation failure and is now doomed.  
  
## User Action  
Retry the failed transaction. For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
  
## See Also  
[In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
