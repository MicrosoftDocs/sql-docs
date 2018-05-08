---
title: "MSSQLSERVER_2511 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.component: "errors-events"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: supportability
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "2511 (Database Engine error)"
ms.assetid: 9a00c0ed-eb4b-4fae-8016-192396006c37
caps.latest.revision: 12
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_2511
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2511|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_KEYS_OUT_OF_ORDER|  
|Message Text|Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). Keys out of order on page %S_PGID, slots %d and %d.|  
  
## Explanation  
Out-of-order keys were detected in the specified index. The page in which the keys are contained may be corrupted.  
  
## User Action  
Rebuild the specified index by using the ALTER INDEX REBUILD statement.  
  
