---
title: "MSSQLSERVER_8712 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "8712 (Database Engine error)"
ms.assetid: 292fb3bc-062e-41e4-a566-b5d3d0b21977
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_8712
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|8712|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|USEPLAN_ERR_NO_INDEX|  
|Message Text|Index '%.*ls', specified in the USE PLAN hint, does not exist. Specify an existing index, or create an index with the specified name.|  
  
## Explanation  
 An index that is specified in the USE PLAN hint does not exist.  
  
## User Action  
 Ensure all indexes that are specified in the USE PLAN hint exist.  
  
## See Also  
 [Query Hints &#40;Transact-SQL&#41;](/sql/t-sql/queries/hints-transact-sql-query)   
 [Plan Guides](../performance/plan-guides.md)  
  
  
