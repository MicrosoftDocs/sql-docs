---
description: "Direct Execution ODBC"
title: "Direct Execution ODBC | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL statements [ODBC], direct execution"
  - "direct execution [ODBC]"
  - "SQL statements [ODBC], executing"
ms.assetid: dd00a535-b136-494f-913b-410838e3de7e
author: David-Engel
ms.author: v-davidengel
---
# Direct Execution ODBC
Direct execution is the simplest way to execute a statement. When the statement is submitted for execution, the data source compiles it into an access plan and then executes that access plan.  
  
 Direct execution is commonly used by generic applications that build and execute statements at run time. For example, the following code builds an SQL statement and executes it a single time:  
  
```  
SQLCHAR *SQLStatement;  
  
// Build an SQL statement.  
BuildStatement(SQLStatement);  
  
// Execute the statement.  
SQLExecDirect(hstmt, SQLStatement, SQL_NTS);  
```  
  
 Direct execution works best for statements that will be executed a single time. Its major drawback is that the SQL statement is parsed every time it is executed. In addition, the application cannot retrieve information about the result set created by the statement (if any) until after the statement is executed; this is possible if the statement is prepared and executed in two separate steps.  
  
 To execute a statement directly, the application performs the following actions:  
  
1.  Sets the values of any parameters. For more information, see [Statement Parameters](../../../odbc/reference/develop-app/statement-parameters.md), later in this section.  
  
2.  Calls **SQLExecDirect** and passes it a string containing the SQL statement.  
  
3.  When **SQLExecDirect** is called, the driver:  
  
    -   Modifies the SQL statement to use the data source's SQL grammar without parsing the statement; this includes replacing the escape sequences discussed in [Escape Sequences in ODBC](../../../odbc/reference/develop-app/escape-sequences-in-odbc.md). The application can retrieve the modified form of an SQL statement by calling **SQLNativeSql**. Escape sequences are not replaced if the SQL_ATTR_NOSCAN statement attribute is set.  
  
    -   Retrieves the current parameter values and converts them as necessary. For more information, see [Statement Parameters](../../../odbc/reference/develop-app/statement-parameters.md), later in this section.  
  
    -   Sends the statement and converted parameter values to the data source for execution.  
  
    -   Returns any errors. These include sequencing or state diagnostics such as SQLSTATE 24000 (Invalid cursor state), syntactic errors such as SQLSTATE 42000 (Syntax error or access violation), and semantic errors such as SQLSTATE 42S02 (Base table or view not found).
