---
title: "Display and Save Execution Plans | Microsoft Docs"
ms.custom: ""
ms.date: "08/21/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Showplan results"
  - "execution plans [SQL Server]"
  - "queries [SQL Server], tuning"
  - "execution plans [SQL Server], how-to topics"
  - "SQL Server Management Studio [SQL Server], execution plans"
  - "tuning queries [SQL Server]"
ms.assetid: bcd6f094-c613-4835-ae19-4caaadb4bb17
caps.latest.revision: 24
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Display and Save Execution Plans
  This section explains how to display execution plans and how to save execution plans to a file in XML format by using Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 Execution plans graphically display the data retrieval methods chosen by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Query Optimizer. Execution plans represent the execution cost of specific statements and queries in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using icons rather than the tabular representation produced by the [SET SHOWPLAN_ALL](../../t-sql/statements/set-showplan-all-transact-sql.md) or [SET SHOWPLAN_TEXT](../../t-sql/statements/set-showplan-text-transact-sql.md) statements. This graphical approach is very useful for understanding the performance characteristics of a query.  

 While SQL Server produces only one execution plan, there is the concept of **estimated** execution plan and **actual** execution plan.
 -  An [estimated execution plan](../../relational-databases/performance/display-the-estimated-execution-plan.md) returns the execution plan as produced by the Query Optimizer at compile-time. Producing the estimated execution plan does not actually execute the query or batch, and therefore does not contain any runtime information, such as actual resource usage metrics or runtime warnings. 
 -  An [actual execution plan](../../relational-databases/performance/display-an-actual-execution-plan.md) returns the execution plan as produced by the Query Optimizer after queries or batches execute. This includes runtime information about resource usage metrics and any runtime warnings.  

 For more information, see [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md).
  
## In This Section  
  
-   [Display the Estimated Execution Plan](../../relational-databases/performance/display-the-estimated-execution-plan.md)  
  
-   [Display an Actual Execution Plan](../../relational-databases/performance/display-an-actual-execution-plan.md)  
  
-   [Save an Execution Plan in XML Format](../../relational-databases/performance/save-an-execution-plan-in-xml-format.md)  
  
  
