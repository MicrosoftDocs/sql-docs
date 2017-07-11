---
title: "MSSQLSERVER_17204 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "17204 (Database Engine error)"
ms.assetid: 40db66f9-dd5e-478c-891e-a06d363a2552
caps.latest.revision: 14
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_17204
  
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
  
