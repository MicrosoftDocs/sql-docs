---
title: "MSSQLSERVER_7911 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "errors-events"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "7911 (Database Engine error)"
ms.assetid: dd8390f3-0f77-4fb2-ba94-631a56e42bc6
caps.latest.revision: 16
author: "edmacauley"
ms.author: "edmaca"
manager: "craigg"
robots: noindex,nofollow
ms.workload: "Inactive"
---
# MSSQLSERVER_7911
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|7911|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC2_REPAIR_PAGE_DEALLOCATED|  
|Message Text|Repair: The page P_ID has been deallocated from object ID O_ID, index ID I_ID, partition ID PN_ID, alloc unit ID A_ID (type TYPE).|  
  
## Explanation  
This is an informational message from REPAIR that states that a page has been deallocated from the single-page slot array of an Index Allocation Map (IAM) page.  
  
## User Action  
None  
  
