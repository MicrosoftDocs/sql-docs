---
title: "Compare and Analyze Execution Plans | Microsoft Docs"
description: Learn how to compare and analyze execution plans by using SQL Server Management Studio. Execution plans display data retrieval methods of the Query Optimizer.
ms.custom: ""
ms.date: "11/21/2018"
ms.service: sql
ms.reviewer: "wiassaf"
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Showplan results"
  - "execution plans [SQL Server]"
  - "queries [SQL Server], tuning"
  - "execution plans [SQL Server], how-to topics"
  - "SQL Server Management Studio [SQL Server], execution plans"
  - "tuning queries [SQL Server]"
ms.assetid: bcd6f094-c613-4835-ae19-4caaadb4bb17
author: rwestMSFT
ms.author: randolphwest
---
# Compare and Analyze Execution Plans
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
This section explains how to compare and analyze execution plans by using Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. This feature is available starting with [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] v17.4.  
  
Execution plans graphically display the data retrieval methods chosen by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Query Optimizer. Execution plans represent the execution cost of specific statements and queries in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using icons rather than the tabular representation produced by the [SET SHOWPLAN_ALL](../../t-sql/statements/set-showplan-all-transact-sql.md) or [SET SHOWPLAN_TEXT](../../t-sql/statements/set-showplan-text-transact-sql.md) statements. This graphical approach is very useful for understanding the performance characteristics of a query. 

[!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] includes functionality that allows users compare two execution plans, for example between perceived good and bad plans for the same query, and perform root cause analysis. Also included is the functionality to perform single query plan analysis, allowing insights into scenarios that may be affecting the performance of a query through analysis of its execution plan.

For more information on query execution plans, see [estimated execution plan](../../relational-databases/performance/display-the-estimated-execution-plan.md), [actual execution plan](../../relational-databases/performance/display-an-actual-execution-plan.md), and the [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md).
  
## In This Section  
[Compare Execution Plans](../../relational-databases/performance/display-the-estimated-execution-plan.md)     
[Analyze an Actual Execution Plan](../../relational-databases/performance/display-an-actual-execution-plan.md)      
  
