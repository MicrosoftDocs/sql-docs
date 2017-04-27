---
title: "MSSQLSERVER_17130 | Microsoft Docs"
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
  - "17130 (Database Engine error)"
ms.assetid: 7ce6afca-221d-402f-89df-da7e74a339a8
caps.latest.revision: 17
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_17130
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|17130|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|INIT_NOLOCKSPACE|  
|Message Text|Not enough memory for the configured number of locks. Attempting to start with a smaller lock hash table, which may impact performance. Contact the database administrator to configure more memory for this instance of the Database Engine.|  
  
## Explanation  
Not enough memory for the desired lock manager hash table size.  An attempt will be made to allocate a smaller hash table.  
  
## User Action  
Check server memory configuration parameters (min/max server memory), check for memory pressures. Provide SQL Server more memory.  
  
