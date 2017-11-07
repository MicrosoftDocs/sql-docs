---
title: "Set the AUTO_SHRINK Database Option to OFF | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 16403850-d745-4754-b84f-5f01aaecd24e
caps.latest.revision: 11
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Set the AUTO_SHRINK Database Option to OFF
  This rule checks whether the AUTO_SHRINK database option is set to OFF. Frequently shrinking and expanding a database can lead to physical fragmentation.  
  
## Best Practices Recommendations  
 Set the AUTO_SHRINK database option to OFF. If you know that the space that you are reclaiming will not be needed in the future, you can reclaim the space by manually shrinking the database.  
  
## For More Information  
 Microsoft Knowledge Base article [315512](http://go.microsoft.com/fwlink/?linkid=117750)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  