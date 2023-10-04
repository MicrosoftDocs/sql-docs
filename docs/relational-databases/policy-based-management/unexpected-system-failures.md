---
title: "Unexpected System Failures"
description: "Unexpected System Failures"
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Unexpected System Failures
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This rule checks for SYSTEM Event 6008 in the computer event log. This event indicates an unexpected system shutdown. The system might be unstable and might not provide the stability and integrity that is required to host an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Best Practices Recommendations  
 Immediately address the cause of the unexpected server restarts, or move the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to another computer.  
  
  
