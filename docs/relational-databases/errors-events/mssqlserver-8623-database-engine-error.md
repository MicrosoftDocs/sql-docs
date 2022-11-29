---
description: "MSSQLSERVER_8623"
title: "MSSQLSERVER_8623 | Microsoft Docs"
ms.custom: ""
ms.date: "08/04/2022"
ms.service: sql
ms.reviewer: jopilov
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "8623 (Database Engine error)"
ms.assetid: 
author: shaunbeasley
ms.author: shaunbe
---
# MSSQLSERVER_8623
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|8623|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|OPTIMIZER_NOPLAN_ERR|  
|Message Text|The query processor ran out of internal resources and could not produce a query plan. This is a rare event and only expected for extremely complex queries or queries that reference a very large number of tables or partitions. Please simplify the query. If you believe you've received this message in error, contact Customer Support Services for more information.|  
  
## Explanation  
The Query Optimizer is unable to generate a query plan due to either running out of resources or the query being too complex, two different states can be returned for this error

- State 1 - The query timed out due to the plan being too complex
- State 2 - The query ran out of resources - Memory


## User Action

Simplify the query by breaking the query into multiple queries along the largest dimension. First, remove any query elements that aren't necessary, then try adding a temp table and splitting the query in two. Note that if you move a part of the query to a subquery, function, or a common table expression that isn't sufficient because they get recombined into a single query by the compiler. You can also, try adding hints to force a plan earlier, for example OPTION (FORCE ORDER).
