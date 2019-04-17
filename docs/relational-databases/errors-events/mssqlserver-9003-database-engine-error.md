---
title: "MSSQLSERVER_9003 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "9003 (Database Engine error)"
ms.assetid: 7fdfb391-5c6f-428b-b434-6c3d0b30fd7b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_9003
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|9003|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LOG_INVALID_LSN|  
|Message Text|The log scan number %S_LSN passed to log scan in database '%.*ls' is not valid. This error may indicate data corruption or that the log file (.ldf) does not match the data file (.mdf). If this error occurred during replication, re-create the publication. Otherwise, restore from backup if the problem results in a failure during startup.|  
  
## Explanation  
A component passed an invalid log sequence number to the log manager for the database. This could be replication, corruption, or an inconsistency between the primary data file and the log.  
  
## User Action  
If this occurred during replication, recreate the publication. Otherwise, restore from backup.  
  
