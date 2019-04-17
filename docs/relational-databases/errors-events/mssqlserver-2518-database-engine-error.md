---
title: "MSSQLSERVER_2518 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "2518 (Database Engine error)"
ms.assetid: 54a13abc-4c33-43f0-b55d-e2e74a1381c8
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_2518
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2518|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_NO_EXPRESSION_EVAL_CLR_DISABLED|  
|Message Text|Object ID O_ID (object "O_NAME"): Computed columns and user-defined types cannot be checked for this object because the common language runtime (CLR) is disabled.|  
  
## Explanation  
This informational message indicates that the query processor could not provide DBCC with an internal object to allow for computed columns and common language runtime (CLR) user-defined types to be evaluated. This problem occurred because one of the columns involved the CLR, but the CLR is not enabled. The internal object covers all columns. Therefore, the inability to evaluate a single column prevents creating the internal object. This means that computed columns will not be checked for correctness or be used when DBCC checks the consistency between indexes and base tables.  
  
## User Action  
Enable CLR and rerun the DBCC statement.  
  
## See Also  
[Enabling CLR Integration](~/relational-databases/clr-integration/clr-integration-enabling.md)  
  
