---
title: "MSSQLSERVER_17128 | Microsoft Docs"
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
  - "17128 (Database Engine error)"
ms.assetid: 7b15a5e6-fd41-47ce-ba87-54f72acea4bb
caps.latest.revision: 14
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_17128
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|17128|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|INIT_NOBUFSPACE|  
|Message Text|initdata: No memory for kernel buffers.|  
  
## Explanation  
The buffer pool’s initial memory allocations or reservations have failed, and SQL Server exits.  
  
## User Action  
Usually caused by starting SQL Server on an extremely small machine – far smaller than the minimum system requirements.  
  
