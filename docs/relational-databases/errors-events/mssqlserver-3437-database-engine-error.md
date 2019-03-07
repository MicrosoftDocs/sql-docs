---
title: "MSSQLSERVER_3437 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "3437 (Database Engine error)"
ms.assetid: b38216e2-b650-43bd-97af-061d54f60031
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_3437
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|3437|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|NODTC|  
|Message Text|An error occurred while recovering database '%.*ls'. Unable to connect to Microsoft Distributed Transaction Coordinator (MS DTC) to check the completion status of transaction %S_XID. Fix MS DTC, and run recovery again.|  
  
## Explanation  
One or more distributed transactions that were using [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) were incomplete when the database was shut down. Recovery of this database failed because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot connect to MS DTC to complete or roll back the transactions.  
  
## User Action  
To recover this database, you must first resolve the problem with MS DTC. To investigate the problem with MS DTC, examine the Windows event logs. If you cannot resolve the problem with MS DTC and recover the database, restore a backup of database.  
  
