---
title: "MSSQLSERVER_8710 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "8710 (Database Engine error)"
ms.assetid: 78b9f9da-5489-4be0-94df-f065d86ed18c
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_8710
    
## Details  
  
|||  
|-|-|  
|Product Name|MSSQLSERVER|  
|Event ID|8710|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|QUERY2_CUBE_ILLEGAL_AGG_FUNC|  
|Message Text|Aggregate functions that are used with CUBE, ROLLUP, or GROUPING SET queries must provide for the merging of subaggregates. To fix this problem, remove the aggregate function or write the query using UNION ALL over GROUP BY clauses.|  
  
## Explanation  
 An aggregate function has been used with CUBE, ROLLUP, or GROUPING SETS that does not provide a method for merging subaggregates.  
  
## User Action  
 To fix this problem, remove the aggregate function or write the query using UNION ALL over GROUP BY clauses.  
  
  
