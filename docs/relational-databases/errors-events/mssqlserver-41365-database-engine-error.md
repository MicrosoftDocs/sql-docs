---
title: "MSSQLSERVER_41365_deleted | Microsoft Docs"
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
  - "41365 (Database Engine error)"
ms.assetid: 4fc7ec15-b722-4e3d-b7f9-3d39d171e96e
caps.latest.revision: 7
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_41365_deleted
  
## Details  
  
|||  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41365|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|HK_MERGE_SCHEDULE_ERROR|  
|Message Text|Merge request for transaction range [%ld, %ld] on database %.*ls was not scheduled. The checkpoint files representing the range are either not available for merge or part of an ongoing merge.|  
  
## Explanation  
The checkpoint files representing the range are either not available for merge or part of an ongoing merge.  
  
## User Action  
Provide a better transaction range for merge/wait before issuing the same request again. For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](../Topic/In-Memory%20OLTP%20(In-Memory%20Optimization).md).  
  
## See Also  
[In-Memory OLTP &#40;In-Memory Optimization&#41;](../Topic/In-Memory%20OLTP%20(In-Memory%20Optimization).md)  
  
