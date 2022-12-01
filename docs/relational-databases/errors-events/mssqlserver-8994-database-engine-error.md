---
description: "MSSQLSERVER_8994"
title: "MSSQLSERVER_8994 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "8994 (Database Engine error)"
ms.assetid: 8f4b0e2f-04c0-46e4-9208-20a7085d7a1a
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_8994
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|8994|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC3_MISSING_FORWARDING_ROW|  
|Message Text|Object ID O_ID, forwarded row page P_ID1, slot S_ID1 should be pointed to by forwarding row page P_ID2, slot S_ID2. Did not encounter forwarding row. Possible allocation error.|  
  
## Explanation  
A forwarded row in a heap is missing the forwarding row that should point to it.  
  
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
  
#### Results of Running REPAIR Options  
The forwarded row will be converted to a regular data row.  
  
