---
title: "Check Disk Input Output Subsystem for Read Retry Problems | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: cedf4097-5b73-4964-9935-74a101847019
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Check Disk Input Output Subsystem for Read Retry Problems
  This rule checks the event log for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error message 825. This message indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was unable to read data from the disk on the first try. This message indicates a major problem with the disk I/O subsystem. This message does not currently indicate a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] problem. However, the disk problem could cause data loss or database corruption if it is not resolved.  
  
## Best Practices Recommendations  
 The following actions might help you discover and resolve the underlying hardware problem:  
  
-   Review the error log and the variable text in this message for clues that explain the problem.  
  
-   Check the disk system. The problem could be related to the disks, the disk controllers, array cards, or disk drivers.  
  
-   Contact the disk manufacturer for the latest utilities for checking the status of the disk system.  
  
-   Contact the disk manufacturer for the latest driver updates.  
  
## For More Information  
 [MSSQLSERVER_825](../errors-events/mssqlserver-825-database-engine-error.md)  
  
 [SQL Server I/O Basics, Chapter 2](https://go.microsoft.com/fwlink/?linkid=69370)  
  
  
