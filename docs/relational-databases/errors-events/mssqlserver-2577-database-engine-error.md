---
description: "MSSQLSERVER_2577"
title: "MSSQLSERVER_2577 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "2577 (Database Engine error)"
ms.assetid: f53256a2-2fb0-47fd-9ed9-c45389104145
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_2577
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|2577|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_IAM_CHAIN_SEQUENCE_OUT_OF_ORDER|  
|Message Text|Chain sequence numbers out of order in the Index Allocation Map (IAM) chain for object ID O_ID, index ID I_ID, partition ID PN_ID, alloc unit ID A_ID (type TYPE). Page P_ID1 with sequence number SEQUENCE1 points to page P_ID2 with sequence number SEQUENCE2.|  
  
## Explanation  
Every Index Allocation Map (IAM) page has a sequence number. The sequence number is the position of the IAM page within the IAM chain. The rule is that sequence numbers increase by one for each IAM page. IAM page, *P_ID2*, has a sequence number that does not follow this rule.  
  
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
Running REPAIR will rebuild the IAM chain. REPAIR first splits the existing IAM chain in two halves. The first half of the chain will end with IAM page, *P_ID1*. The next page pointer of the *P_ID1* page will be set to (0:0). The second half of the chain will start with IAM page, *P_ID2*. The previous page pointer of the *P_ID2* page will be set to (0:0).  
  
REPAIR will then connect the two haves of the chain together and regenerate the sequence numbers for the IAM chain. Any IAM pages that cannot be repaired will be deallocated.  
  
> [!CAUTION]  
> This repair may cause data loss.  
  
