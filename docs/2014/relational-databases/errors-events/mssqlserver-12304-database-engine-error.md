---
title: "MSSQLSERVER_12304 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "12304 (Database Engine error)"
ms.assetid: a2c252c2-e815-4ac8-a101-7af5b32e3233
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_12304
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|12304|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|HK_UNSUPPORTED_IDENTITY_TABLE_VAR|  
|Message Text|Using a memory optimized table type that uses the IDENTITY property with any of its columns is not supported when using the type outside the context of a natively compiled stored procedure.|  
  
## User Action  
 Do not use a memory-optimized table type that uses the identity property with any of its columns.  
  
## See Also  
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
  
