---
description: "MSSQLSERVER_41302"
title: "MSSQLSERVER_41302 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "41302 (Database Engine error)"
ms.assetid: 01e75618-afec-4232-ba68-93ab7bc31003
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_41302
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41302|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|WRITE_WRITE_CONFLICT|  
|Message Text|The current transaction attempted to update a record that has been updated since this transaction started. The transaction was aborted.|  
  
## Explanation  
The transaction encountered a write/write conflict and the statement terminated.  
  
## User Action  
Retry the operation later in a different transaction. For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
  
## See Also  
[In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
