---
title: "MSSQLSERVER_2546 | Microsoft Docs"
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
  - "2546 (Database Engine error)"
ms.assetid: c8f0e1b4-c7c4-45f2-9221-746714172313
caps.latest.revision: 20
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_2546
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2546|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_INDEX_MARKED_DISABLED|  
|Message Text|Index 'INDEX_NAME' on table 'OBJECT_NAME' is marked as disabled. Rebuild the index to bring it online.|  
  
## Explanation  
The specified index is marked as offline or is disabled. Therefore, this index cannot be checked.  
  
## User Action  
Rebuild the index by using ALTER INDEX.  
  
## See Also  
[ALTER INDEX &#40;Transact-SQL&#41;](~/t-sql/statements/alter-index-transact-sql.md)  
[Reorganize and Rebuild Indexes](~/relational-databases/indexes/reorganize-and-rebuild-indexes.md)  
  
