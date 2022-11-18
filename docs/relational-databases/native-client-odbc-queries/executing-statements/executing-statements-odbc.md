---
description: "Executing Statements (ODBC)"
title: "Executing Statements (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, statements"
  - "statements [ODBC]"
  - "ODBC applications, statements"
  - "statements [ODBC], executing"
ms.assetid: 063fc40d-ff81-490d-9c9b-2faefb729f37
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Executing Statements (ODBC)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver offers a variety ways to execute SQL statements in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database:  
  
-   Direct execution  
  
-   Prepared execution  
  
 Direct execution involves building a character string containing a [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement and submitting it for execution using the **SQLExecDirect** function. Prepared execution involves building a character string containing a [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement and then executing it in two stages. The first stage uses the [SQLPrepare Function](../../../odbc/reference/syntax/sqlprepare-function.md) function to parse and compile the execution plan for the statement in the [!INCLUDE[ssDE](../../../includes/ssde-md.md)]. The second stage uses the **SQLExecute** function to execute the previously prepared execution plan. This saves the parsing and compiling overhead on each execution. Prepared execution is commonly used by applications to repeatedly execute the same, parameterized SQL statement.  
  
 Both direct and prepared execution can execute a single [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement or a batch of SQL statements, or they can call a stored procedure.  
  
## In This Section  
  
-   [Direct Execution](../../../relational-databases/native-client-odbc-queries/executing-statements/direct-execution.md)  
  
-   [Prepared Execution](../../../relational-databases/native-client-odbc-queries/executing-statements/prepared-execution.md)  
  
-   [Procedures](../../../relational-databases/native-client-odbc-queries/executing-statements/procedures.md)  
  
-   [Batches of Statements](../../../relational-databases/native-client-odbc-queries/executing-statements/batches-of-statements.md)  
  
-   [Effects of ISO Options](../../../relational-databases/native-client-odbc-queries/executing-statements/effects-of-iso-options.md)  
  
## See Also  
 [Executing Queries &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-queries/executing-queries-odbc.md)  
  
