---
title: "Executing Statements (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, statements"
  - "statements [ODBC]"
  - "ODBC applications, statements"
  - "statements [ODBC], executing"
ms.assetid: 063fc40d-ff81-490d-9c9b-2faefb729f37
author: MightyPen
ms.author: genemi
manager: craigg
---
# Executing Statements (ODBC)
  The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver offers a variety ways to execute SQL statements in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database:  
  
-   Direct execution  
  
-   Prepared execution  
  
 Direct execution involves building a character string containing a [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement and submitting it for execution using the **SQLExecDirect** function. Prepared execution involves building a character string containing a [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement and then executing it in two stages. The first stage uses the [SQLPrepare Function](https://go.microsoft.com/fwlink/?LinkId=59360) function to parse and compile the execution plan for the statement in the [!INCLUDE[ssDE](../../../includes/ssde-md.md)]. The second stage uses the **SQLExecute** function to execute the previously prepared execution plan. This saves the parsing and compiling overhead on each execution. Prepared execution is commonly used by applications to repeatedly execute the same, parameterized SQL statement.  
  
 Both direct and prepared execution can execute a single [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement or a batch of SQL statements, or they can call a stored procedure.  
  
## In This Section  
  
-   [Direct Execution](direct-execution.md)  
  
-   [Prepared Execution](prepared-execution.md)  
  
-   [Procedures](procedures.md)  
  
-   [Batches of Statements](batches-of-statements.md)  
  
-   [Effects of ISO Options](effects-of-iso-options.md)  
  
## See Also  
 [Executing Queries &#40;ODBC&#41;](../executing-queries-odbc.md)  
  
  
