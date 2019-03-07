---
title: "MSSQLSERVER_3417 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "3417 (Database Engine error)"
ms.assetid: 005829c8-cf57-4828-818a-bbe8ee2e00f0
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_3417
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|3417|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|REC_BADMASTER|  
|Message Text|Cannot recover the master database. SQL Server is unable to run. Restore master from a full backup, repair it, or rebuild it. For more information about how to rebuild the master database, see SQL Server Books Online.|  
  
## Explanation  
SQL Server cannot start the **master** database. If the **master** or **tempdb** cannot be brought online, SQL Server cannot run. This error is usually preceded by other errors. Review error logs to find the root cause.  
  
## User Action  
Restore backup of the database or repair the database.  
  
