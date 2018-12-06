---
title: "Batching Stored Procedure Calls | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "stored procedures [ODBC], batching"
  - "ODBC, stored procedures"
  - "SQL Server Native Client ODBC driver, stored procedures"
  - "batches [ODBC]"
  - "ODBC CALL escape sequence"
ms.assetid: b7f53e11-15f0-4602-8134-b166160888f0
author: MightyPen
ms.author: genemi
manager: craigg
---
# Batching Stored Procedure Calls
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver automatically batches stored procedure calls to the server when appropriate. The driver only does this when the ODBC CALL escape sequence is used; it does not do this for the [!INCLUDE[tsql](../../includes/tsql-md.md)] EXECUTE statement. Batching stored procedure calls can reduce the number of round trips to the server and significantly increase performance.  
  
 The driver batches procedure calls to the server when you execute a batch that contains multiple ODBC CALL escape sequences. It also batches procedure calls when bound parameter arrays are used with an ODBC CALL escape sequence. For example, if you use either row-wise or column-wise parameter binding to bind an array with five elements to the parameters of an ODBC CALL SQL statement, when **SQLExecute** or **SQLExecDirect** is called, the driver sends a single batch with five procedure calls to the server.  
  
## See Also  
 [Running Stored Procedures](running-stored-procedures.md)  
  
  
