---
title: "MSSQLSERVER_17204 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "17204 (Database Engine error)"
ms.assetid: 40db66f9-dd5e-478c-891e-a06d363a2552
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_17204
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|17204|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBLKIO_DEVOPENFAILED|  
|Message Text|%ls: Could not open file %ls for file number %d.  OS error: %ls.|  
  
## Explanation  
SQL Server was unable to open the specified file due to the specified error.  
  
## User Action  
Diagnose and correct the operating system, then retry the operation.  
  
