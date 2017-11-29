---
title: "Unexpected System Failures | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "performance-monitor"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 1679bf9e-a2ef-4f90-8907-a002f7341a7d
caps.latest.revision: 8
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
ms.workload: "Inactive"
---
# Unexpected System Failures
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This rule checks for SYSTEM Event 6008 in the computer event log. This event indicates an unexpected system shutdown. The system might be unstable and might not provide the stability and integrity that is required to host an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Best Practices Recommendations  
 Immediately address the cause of the unexpected server restarts, or move the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to another computer.  
  
  
