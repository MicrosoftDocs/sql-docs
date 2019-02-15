---
title: "Set the AUTO_SHRINK Database Option to OFF | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 16403850-d745-4754-b84f-5f01aaecd24e
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Set the AUTO_SHRINK Database Option to OFF
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This rule checks whether the AUTO_SHRINK database option is set to OFF. Frequently shrinking and expanding a database can lead to physical fragmentation.  
  
## Best Practices Recommendations  
 Set the AUTO_SHRINK database option to OFF. If you know that the space that you are reclaiming will not be needed in the future, you can reclaim the space by manually shrinking the database.  
  
## For More Information  
 Microsoft Knowledge Base article [315512](https://go.microsoft.com/fwlink/?linkid=117750)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  
