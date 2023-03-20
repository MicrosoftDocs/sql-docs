---
title: "Set the AUTO_SHRINK Database Option to OFF"
description: "Set the AUTO_SHRINK Database Option to OFF"
author: VanMSFT
ms.author: vanto
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Set the AUTO_SHRINK Database Option to OFF
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This rule checks whether the AUTO_SHRINK database option is set to OFF. Frequently shrinking and expanding a database can lead to physical fragmentation.  
  
## Best Practices Recommendations  
 Set the AUTO_SHRINK database option to OFF. If you know that the space that you are reclaiming will not be needed in the future, you can reclaim the space by manually shrinking the database.  
  
## For More Information  
 Microsoft Knowledge Base article [315512](/troubleshoot/sql/admin/considerations-autogrow-autoshrink)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
