---
description: "MSSQLSERVER_7908"
title: "MSSQLSERVER_7908 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "7908 (Database Engine error)"
ms.assetid: 470045b0-ebe9-44a7-b456-480e7a516a2c
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_7908
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|7908|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC2_FS_INVALID_COLUMN_LEVEL_FILE|  
|Message Text|Table error: The file 'FILE' in partition ID PN_ID is not a valid Filestream file.|  
  
## Explanation

The name of a FILESTREAM file in a column directory is a ROWGUID. If a file name in a column directory cannot be converted to a ROWGUID, the file is not a valid file.  
  
## User Action  
  
### Look for Hardware Failure

Run hardware diagnostics and correct any problems. Also examine the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows system and application logs and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log to see whether the error occurred as the result of hardware failure. Fix any hardware-related problems that are contained in the logs.  
  
If you have persistent data corruption problems, try to swap out different hardware components to isolate the problem. Check to make sure that the system does not have write-caching enabled on the disk controller. If you suspect write-caching to be the problem, contact your hardware vendor.  
  
Finally, you might find it useful to switch to a new hardware system. This switch may include reformatting the disk drives and reinstalling the operating system.  
  
### Restore from Backup

If the problem is not hardware related and a known clean backup is available, restore the database from the backup.  
  
### Run DBCC CHECKDB

DBCC CHECKDB discovers this error, but cannot repair it. Restore the database from a good backup.
