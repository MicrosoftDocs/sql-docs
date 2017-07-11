---
title: "MSSQLSERVER_617 | Microsoft Docs"
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
  - "617 (Database Engine error)"
ms.assetid: 213545d9-08a7-4427-bfd1-8b7e16644281
caps.latest.revision: 14
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_617
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|617|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|NODESHASH|  
|Message Text|Descriptor for object ID %ld in database ID %d not found in the hash table during attempt to unhash it. A work table is missing an entry. Rerun the query. If a cursor is involved, close and reopen the cursor.|  
  
## Explanation  
SQL Server cannot locate the work table for a specific entry.  
  
## User Action  
  
1.  If a cursor is involved, close the cursor and re-open the cursor.  
  
2.  Run the query again.  
  
