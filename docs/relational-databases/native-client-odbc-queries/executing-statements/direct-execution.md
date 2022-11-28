---
description: "Direct Execution"
title: "Direct Execution | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC applications, statements"
  - "direct execution [ODBC]"
  - "SQLExecDirect function"
  - "statements [ODBC], direct execution"
ms.assetid: fa36e1af-ed98-4abc-97c1-c4cc5d227b29
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Direct Execution
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Direct execution is the most basic way to execute a statement. An application builds a character string containing a [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement and submits it for execution using the **SQLExecDirect** function. When the statement reaches the server, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] compiles it into an execution plan and then immediately runs the execution plan.  
  
 Direct execution is commonly used by applications that build and execute statements at run time and is the most efficient method for statements that will be executed a single time. Its drawback with many databases is that the SQL statement must be parsed and compiled each time it is executed, which adds overhead if the statement is executed multiple times.  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] significantly improves the performance of direct execution of commonly executed statements in multiuser environments and using SQLExecDirect with parameter markers for commonly executed SQL statements can approach the efficiency of prepared execution.  
  
 When connected to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver uses [sp_executesql](../../../relational-databases/system-stored-procedures/sp-executesql-transact-sql.md) to transmit the SQL statement or batch specified on **SQLExecDirect**. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] has logic to quickly determine if an SQL statement or batch executed with **sp_executesql** matches the statement or batch that generated an execution plan that already exists in memory. If a match is made, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] simply reuses the existing plan rather than compile a new plan. This means that commonly executed SQL statements executed with **SQLExecDirect** in a system with many users will benefit from many of the plan-reuse benefits that were only available to stored procedures in earlier versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 This benefit of reusing execution plans only works when several users are executing the same SQL statement or batch. Follow these coding conventions to increase the probability that the SQL statements executed by different clients are similar enough to be able to reuse execution plans:  
  
-   Do not include data constants in the SQL statements; instead use parameter markers bound to program variables. For more information, see [Using Statement Parameters](../../../relational-databases/native-client-odbc-queries/using-statement-parameters.md).  
  
-   Use fully qualified object names. Execution plans are not reused if object names are not qualified.  
  
-   Have application connections as possible use a common set of connection and statement options. Execution plans generated for a connection with one set of options (such as ANSI_NULLS) are not reused for a connection having another set of options. The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider both have the same default settings for these options.  
  
 If all statements executed with **SQLExecDirect** are coded using these conventions, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can reuse execution plans when the opportunity arises.  
  
## See Also  
 [Executing Statements &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-queries/executing-statements/executing-statements-odbc.md)  
  
  
