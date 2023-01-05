---
description: "MSSQLSERVER_7984"
title: "MSSQLSERVER_7984 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "7984 (Database Engine error)"
ms.assetid: e3192f56-e4e2-41da-b132-65f1e7540b1a
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_7984
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|7984|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC2_PRE_CHECKS_BAD_PAGE_TYPE|  
|Message Text|System table pre-checks: Object ID O_ID. Page P_ID has unexpected page type PAGETYPE. Check statement terminated because of an irreparable error.|  
  
## Explanation  
A page with a type other than DATA_PAGE was found in the data level of the specified object. This error is raised during the first phase of the DBCC CHECKDB command checks. During this phase, DBCC CHECKDB performs primitive checks on the data pages of critical system base tables.  
  
> [!NOTE]  
> If any errors are found in the system tables, the errors cannot be repaired; therefore, the DBCC CHECKDB command ends immediately.  
  
## User Action  
  
### Look for Hardware Failure  
Run hardware diagnostics and correct any problems. Also examine the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows system and application logs and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log to see whether the error occurred as the result of hardware failure. Fix any hardware-related problems that are contained in the logs.  
  
If you have persistent data corruption problems, try to swap out different hardware components to isolate the problem. Check to make sure that the system does not have write-caching enabled on the disk controller. If you suspect write-caching to be the problem, contact your hardware vendor.  
  
Finally, you might find it useful to switch to a new hardware system. This switch may include reformatting the disk drives and reinstalling the operating system.  
  
### Restore from Backup  
If the problem is not hardware related and a known clean backup is available, restore the database from the backup.  
  
### Run DBCC CHECKDB  
Not applicable. This error cannot be repaired automatically. If you cannot restore the database from a backup, contact [!INCLUDE[msCoName](../../includes/msconame-md.md)] Service and Support (CSS).  
  
