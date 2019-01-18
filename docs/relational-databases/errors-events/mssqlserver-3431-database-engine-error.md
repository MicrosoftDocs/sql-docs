---
title: "MSSQLSERVER_3431 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "3431 (Database Engine error)"
ms.assetid: 9541217f-e5c6-4a12-a19a-006058f1d3f3
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_3431
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|3431|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|UNRESOLVED_XACT|  
|Message Text|Could not recover database '%.*ls' (database ID %d) because of unresolved transaction outcomes. Microsoft Distributed Transaction Coordinator (MS DTC) transactions were prepared, but MS DTC was unable to determine the resolution. To resolve, either fix MS DTC, restore from a full backup, or repair the database.|  
  
## Explanation  
One or more distributed transactions that were using [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) were incomplete when the database was shut down. Recovery of this database failed because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot complete the transactions or roll back the transactions without more information from MS DTC.  
  
## User Action  
To recover this database, you must first resolve the problem with MS DTC. To investigate the problem with MS DTC, examine the Windows event logs. If you cannot resolve the problem with MS DTC and recover the database, restore a backup of database.  
  
