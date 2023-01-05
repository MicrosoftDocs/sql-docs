---
description: "Errors and Batches"
title: "Errors and Batches | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "batches [ODBC], errors"
  - "sql_success_with_info [ODBC]"
  - "sql_success [ODBC]"
  - "SQL statements [ODBC], batches"
  - "sql_error [ODBC]"
ms.assetid: 6debd41d-9f4c-4f4c-a44b-2993da5306f0
author: David-Engel
ms.author: v-davidengel
---
# Errors and Batches
When an error occurs while executing a batch of SQL statements, one of the following four outcomes are possible. (Each possible outcome is data source-specific and might even depend on the statements included in the batch.)  
  
-   No statements in the batch are executed.  
  
-   No statements in the batch are executed and the transaction is rolled back.  
  
-   All of the statements before the error statement are executed.  
  
-   All of the statements except the error statement are executed.  
  
 In the first two cases, **SQLExecute** and **SQLExecDirect** return SQL_ERROR. In the latter two cases, they may return SQL_SUCCESS_WITH_INFO or SQL_SUCCESS, depending on the implementation. In all cases, further error information can be retrieved with **SQLGetDiagField**, **SQLGetDiagRec**, or **SQLError**. However, the nature and depth of this information is data source-specific. Furthermore, this information is unlikely to exactly identify the statement in error.
