---
description: "MSSQLSERVER_41365"
title: "MSSQLSERVER_41365 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "41365 (Database Engine error)"
ms.assetid: 4fc7ec15-b722-4e3d-b7f9-3d39d171e96e
author: MashaMSFT
ms.author: mathoma
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
  
