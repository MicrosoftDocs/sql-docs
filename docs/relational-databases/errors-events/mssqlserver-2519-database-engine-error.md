---
description: "MSSQLSERVER_2519"
title: "MSSQLSERVER_2519 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "2519 (Database Engine error)"
ms.assetid: 8dc6ad98-5db8-4c88-8dea-6d455e63b839
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_2519
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
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
[MSSQLSERVER_2518](~/relational-databases/errors-events/mssqlserver-2518-database-engine-error.md)  
  
