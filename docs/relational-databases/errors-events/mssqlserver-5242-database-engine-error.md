---
title: "MSSQLSERVER_5242 | Microsoft Docs"
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
  - "5242 (Database Engine error)"
ms.assetid: 712b1a10-2f87-41df-a111-1ed9f14102d4
caps.latest.revision: 15
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_5242
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|5242|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|An inconsistency was detected during an internal operation in database '%.*ls'(ID:%d) on page %S_PGID. Please contact technical support. Reference number %ld.|  
  
## Explanation  
A structural inconsistency occurred on the database page that is referenced in the error message.  
  
## See Also  
[DBCC CHECKDB &#40;Transact-SQL&#41;](~/t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)  
[Database Files and Filegroups](~/relational-databases/databases/database-files-and-filegroups.md)  
  
