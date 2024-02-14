---
title: "Check disk IO subsystem for IO delay problems"
description: Learn how to enable a policy to check the disk IO subsystem for IO delay problems by checking the event log for error message 833 for Policy-Based Management with SQL Server.
author: VanMSFT
ms.author: vanto
ms.date: "12/15/2023"
ms.service: sql
ms.subservice: security
ms.topic: reference
helpviewer_keywords:
  - "Best Practices [Database Engine]"
---
# Check disk input and output subsystem for IO delay problems
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This rule checks the event log for error message 833. This message indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has issued a read or write request from disk, and that the request has taken longer than 15 seconds to return. This error is reported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and indicates a problem with the disk I/O subsystem. Delays this long can severely damage the performance of your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environment.  
  
## Best practices recommendations  
 Troubleshoot this error by examining the system event log for hardware-related error messages. Also, examine hardware-specific logs if they are available.  
  
 Use Performance Monitor to examine the following counters:  
  
-   Average Disk Sec/Transfer  
  
-   Average Disk Queue Length  
  
-   Current Disk Queue Length  
  
 For example, the Average Disk Sec/Transfer time on a computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is typically less than 15 milliseconds. If the Average Disk Sec/Transfer value increases, this indicates that the disk I/O subsystem is not optimally keeping up with the I/O demand.  
  
## Related content

 [Microsoft Knowledge Base article 897284](https://go.microsoft.com/fwlink/?linkid=117743)  
  
 [SQL Server I/O Basics, Chapter 2](/previous-versions/sql/sql-server-2005/administrator/cc917726(v=technet.10))
