---
description: "Verify Max Worker Threads Setting"
title: "Verify Max Worker Threads Setting | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 2d94adfd-3ba1-493a-b29a-b436f9d583df
author: VanMSFT
ms.author: vanto
---
# Verify Max Worker Threads Setting
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This rule checks the max worker threads server option for potentially incorrect settings. Setting the max worker threads option to a small value may prevent enough threads from servicing incoming client requests in a timely manner and could lead to "thread starvation". However, setting the option to a large value can waste address space, because each active thread consumes up to 4 MB on 64-bit servers.  
  
## Best Practices Recommendations  
 Set the max worker threads option to 0. This enables [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to automatically determine the correct number of active worker threads based on user requests.  
  
## For More Information  
 [Configure the max worker threads Server Configuration Option](../../database-engine/configure-windows/configure-the-max-worker-threads-server-configuration-option.md)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  
