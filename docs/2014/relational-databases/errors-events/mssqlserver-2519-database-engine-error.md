---
title: "MSSQLSERVER_2519 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "2519 (Database Engine error)"
ms.assetid: 8dc6ad98-5db8-4c88-8dea-6d455e63b839
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_2519
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2519|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_NO_EXPRESSION_EVALUATOR|  
|Message Text|Computed columns and user-defined types cannot be checked for object ID O_ID (object "O_NAME") because the internal expression evaluator could not be initialized.|  
  
## Explanation  
 This informational message indicates that the query processor could not provide DBCC with an internal object to allow for the evaluation of computed columns and common language runtime (CLR) user-defined types. This means that computed columns and CLR user-defined types will not be checked for correctness or be used when DBCC checks the consistency between indexes and base tables.  
  
## User Action  
 None  
  
## See Also  
 [MSSQLSERVER_2518](mssqlserver-2518-database-engine-error.md)  
  
  
