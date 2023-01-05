---
description: "MSSQLSERVER_2534"
title: "MSSQLSERVER_2534 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "2534 (Database Engine error)"
ms.assetid: 121cf99d-0722-494c-be24-3369c1a0badc
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_2534
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|2534|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_PAGE_ALLOCATED_TO_OTHER_OBJECT|  
|Message Text|Table error: Page P_ID, whose header indicates it as being allocated to object ID O_ID, index ID I_ID, partition ID PN_ID, alloc unit ID A_ID (type TYPE), is allocated by another object.|  
  
## Explanation  
The header of the page contains the allocation unit ID, *A_ID*, but none of the Index Allocation Map (IAM) pages of that allocation unit allocates the page. Therefore, the header of the page contains the wrong allocation unit ID, and the page will have a matching MSSQLServer_2533 error that corresponds to the allocation unit ID to which the page is actually allocated.  
  
## User Action  
  
### Look for Hardware Failure  
Run hardware diagnostics and correct any problems. Also examine the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows system and application logs and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log to see whether the error occurred as the result of hardware failure. Fix any hardware-related problems that are contained in the logs.  
  
If you have persistent data corruption problems, try to swap out different hardware components to isolate the problem. Check to make sure that the system does not have write-caching enabled on the disk controller. If you suspect write-caching to be the problem, contact your hardware vendor.  
  
Finally, you might find it useful to switch to a new hardware system. This switch may include reformatting the disk drives and reinstalling the operating system.  
  
### Restore from Backup  
If the problem is not hardware related and a known clean backup is available, restore the database from the backup.  
  
### Run DBCC CHECKDB  
If no clean backup is available, run DBCC CHECKDB without a REPAIR clause to determine the extent of the corruption. DBCC CHECKDB will recommend a REPAIR clause to use. Then, run DBCC CHECKDB with the appropriate REPAIR clause to repair the corruption.  
  
> [!CAUTION]  
> If you are not sure what effect DBCC CHECKDB with a REPAIR clause has on your data, contact your primary support provider before running this statement.  
  
If running DBCC CHECKDB with one of the REPAIR clauses does not correct the problem, contact your primary support provider.  
  
### Results of Running REPAIR Options  
REPAIR will rebuild the index if an index exists.  
  
> [!CAUTION]  
> Running REPAIR for the matching [MSSQLSERVER_2533](~/relational-databases/errors-events/mssqlserver-2533-database-engine-error.md) error deallocates the page before the rebuild.  
  
> [!CAUTION]  
> This repair may cause data loss.  
  
