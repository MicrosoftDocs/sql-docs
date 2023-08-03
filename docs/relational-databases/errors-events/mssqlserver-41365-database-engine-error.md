---
title: "MSSQLSERVER_41365"
description: "MSSQLSERVER_41365"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "41365 (Database Engine error)"
---
# MSSQLSERVER_41365
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41365|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|HK_MERGE_SCHEDULE_ERROR|  
|Message Text|Merge request for transaction range [%ld, %ld] on database %.*ls was not scheduled. The checkpoint files representing the range are either not available for merge or part of an ongoing merge.|  
  
## Explanation  
The checkpoint files representing the range are either not available for merge or part of an ongoing merge.  
  
## User Action  
Provide a better transaction range for merge/wait before issuing the same request again. For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
  
## See Also  
[In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
