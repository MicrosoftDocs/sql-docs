---
title: "MSSQLSERVER_8710"
description: "MSSQLSERVER_8710"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "8710 (Database Engine error)"
---
# MSSQLSERVER_8710
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
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
  
