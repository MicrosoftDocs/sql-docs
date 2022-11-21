---
description: "MSSQLSERVER_7995"
title: "MSSQLSERVER_7995 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "7995 (Database Engine error)"
ms.assetid: af6d6322-3cba-43d8-be97-e6ef15f8c933
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_7995
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|7995|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC2_SYSTEM_CATALOGS_CORRUPT|  
|Message Text|Database 'DBNAME': consistency errors in system catalogs prevent further DBCC CHECKNAME processing.|  
  
## Explanation  
The DBCC CHECKDB process consists of the following three stages:  
  
1.  Allocation checks. This is equivalent to running DBCC CHECKALLOC.  
  
2.  System tables consistency checks. This is equivalent to running DBCC CHECKTABLE on a small list of necessary system base tables.  
  
3.  Complete database consistency checks.  

MSSQLEngine_7995 is raised in stage 2 to indicate that DBCC CHECKDB has found errors that the command cannot repair or that REPAIR has not been specified. DBCC CHECKDB cannot continue with stage 3 because either the system base tables in question store the metadata for all objects in the database or the system base tables are corrupted.  
  
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
Examine the list of errors to see what REPAIR will do for each.  
  
