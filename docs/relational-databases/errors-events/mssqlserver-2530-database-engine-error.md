---
title: "MSSQLSERVER_2530_deleted | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
ms.assetid: 5d4be07a-38a5-4b25-819c-4dcb4636cc15
caps.latest.revision: 11
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_2530_deleted
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2530|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_INDEX_IS_OFFLINE|  
|Message Text|The index "%.*ls" on table "%.\*ls" is disabled.|  
  
## Explanation  
The DBCC statement cannot proceed because the specified index is disabled. After an index is disabled, it remains in a disabled state until it is rebuilt or dropped and re-created.  
  
## User Action  
  
1.  Enable the disabled index by using one of the following methods:  
  
    -   ALTER INDEX statement with the REBUILD clause  
  
    -   CREATE INDEX with the DROP_EXISTING clause  
  
    -   DBCC DBREINDEX  
  
2.  Rerun the DBCC statement.  
  
## See Also  
[Enable Indexes and Constraints](../Topic/Enable%20Indexes%20and%20Constraints.md)  
[ALTER INDEX &#40;Transact-SQL&#41;](../Topic/ALTER%20INDEX%20(Transact-SQL).md)  
[CREATE INDEX &#40;Transact-SQL&#41;](../Topic/CREATE%20INDEX%20(Transact-SQL).md)  
[DBCC DBREINDEX &#40;Transact-SQL&#41;](../Topic/DBCC%20DBREINDEX%20(Transact-SQL).md)  
  
