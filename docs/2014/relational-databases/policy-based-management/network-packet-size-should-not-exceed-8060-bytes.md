---
title: "Network Packet Size Should Not Exceed 8060 Bytes | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 86db5da1-afe4-4fbb-8bf8-33cedc7e4361
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Network Packet Size Should Not Exceed 8060 Bytes
  If the value specified for sp_configure 'network packet size' or if the network packet size of any logged-in user is more than 8060 bytes, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performs different memory allocation operations. This can cause an increase in the process virtual address space that is not reserved for the buffer pool.  
  
## Best Practices Recommendations  
 The network packet size should not exceed 8060 bytes.  
  
## For More Information  
 [Microsoft Knowledge Base article 903002](https://go.microsoft.com/fwlink/?linkid=117749)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  
