---
title: "MSSQLSERVER_2516 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "2516 (Database Engine error)"
ms.assetid: 79ed14b5-f53c-40c6-8334-8a083f732207
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_2516
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2516|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_REPAIR_DIFF_MAP_INVALIDATED|  
|Message Text|Repair has invalidated the differential bitmap for database NAME. The differential backup chain is broken. You must perform a full database backup before you can perform a differential backup.|  
  
## Explanation  
This informational message is returned when error MSSQLEngine_2515 is repaired.  
  
## User Action  
Perform a full database backup.  
  
## See Also  
[Create a Full Database Backup &#40;SQL Server&#41;](~/relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)  
  
