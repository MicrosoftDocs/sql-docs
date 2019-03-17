---
title: "SET STATISTICS TIME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SET_STATISTICS_TIME_TSQL"
  - "SET STATISTICS TIME"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "statistical information [SQL Server], statement processing"
  - "time [SQL Server], statement processing statistics"
  - "SET STATISTICS TIME statement"
  - "STATISTICS TIME option"
  - "statements [SQL Server], statistical information"
  - "parsing [SQL Server], SET STATISTICS TIME statement"
  - "compile times [SQL Server]"
  - "execution processing time [SQL Server]"
ms.assetid: eec2e1cd-a29d-4cf3-a271-be9d61506f15
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SET STATISTICS TIME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Displays the number of milliseconds required to parse, compile, and execute each statement.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
SET STATISTICS TIME { ON | OFF }  
```  
  
## Remarks  
 When SET STATISTICS TIME is ON, the time statistics for a statement are displayed. When OFF, the time statistics are not displayed.  
  
 The setting of SET STATISTICS TIME is set at execute or run time and not at parse time.  
  
 Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is unable to provide accurate statistics in fiber mode, which is activated when you enable the **lightweight pooling** configuration option.  
  
 The **cpu** column in the **sysprocesses** table is only updated when a query executes with SET STATISTICS TIME ON. When SET STATISTICS TIME is OFF, **0** is returned.  
  
 ON and OFF settings also affect the CPU column in the Process Info View for Current Activity in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
## Permissions  
 To use SET STATISTICS TIME, users must have the appropriate permissions to execute the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. The SHOWPLAN permission is not required.  
  
## Examples  
 This example shows the server execution, parse, and compile times.  
  
```  
USE AdventureWorks2012;  
GO         
SET STATISTICS TIME ON;  
GO  
SELECT ProductID, StartDate, EndDate, StandardCost   
FROM Production.ProductCostHistory  
WHERE StandardCost < 500.00;  
GO  
SET STATISTICS TIME OFF;  
GO  
```  
  
 Here is the result set:  
  
```  
SQL Server parse and compile time:   
   CPU time = 0 ms, elapsed time = 1 ms.  
SQL Server parse and compile time:   
   CPU time = 0 ms, elapsed time = 1 ms.  
  
(269 row(s) affected)  
  
SQL Server Execution Times:  
   CPU time = 0 ms,  elapsed time = 2 ms.  
SQL Server parse and compile time:   
   CPU time = 0 ms, elapsed time = 1 ms.  
  
```  
  
## See Also  
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [SET STATISTICS IO &#40;Transact-SQL&#41;](../../t-sql/statements/set-statistics-io-transact-sql.md)  
  
  
