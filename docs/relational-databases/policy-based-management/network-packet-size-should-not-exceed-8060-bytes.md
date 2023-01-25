---
description: "Network Packet Size Should Not Exceed 8060 Bytes"
title: "Network Packet Size Should Not Exceed 8060 Bytes | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 86db5da1-afe4-4fbb-8bf8-33cedc7e4361
author: VanMSFT
ms.author: vanto
---
# Network Packet Size Should Not Exceed 8060 Bytes
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  If the value specified for sp_configure 'network packet size' or if the network packet size of any logged-in user is more than 8060 bytes, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performs different memory allocation operations. This can cause an increase in the process virtual address space that is not reserved for the buffer pool.  
  
## Best Practices Recommendations  
 The network packet size should not exceed 8060 bytes.  
  
## For More Information  
 [Microsoft Knowledge Base article 903002](https://www.betaarchive.com/wiki/index.php?title=Microsoft_KB_Archive/903002)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  
