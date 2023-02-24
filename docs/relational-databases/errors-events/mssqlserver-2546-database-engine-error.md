---
description: "MSSQLSERVER_2546"
title: "MSSQLSERVER_2546 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "2546 (Database Engine error)"
ms.assetid: c8f0e1b4-c7c4-45f2-9221-746714172313
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_2546
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
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
  
