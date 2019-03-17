---
title: "Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "stored procedures [ODBC]"
  - "stored procedures [ODBC], about ODBC stored procedures"
  - "ODBC applications, statements"
  - "statements [ODBC], stored procedures"
  - "ODBC applications, stored procedures"
ms.assetid: c64d5f3a-376b-48ef-84f3-b6148ac8600a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Procedures
  A stored procedure is a precompiled executable object that contains one or more [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements. Stored procedures can have input and output parameters and can also put out an integer return code. An application can enumerate available stored procedures by using catalog functions.  
  
 ODBC applications that target [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] should only use direct execution to call a stored procedure. When connected to earlier versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver implements [SQLPrepare Function](https://go.microsoft.com/fwlink/?LinkId=59360) by creating a temporary stored procedure, which is then called on **SQLExecute**. It adds overhead to have **SQLPrepare** create a temporary stored procedure that only calls the target stored procedure versus directly executing the target stored procedure. Even when connected to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], preparing a call requires an extra round trip across the network and the building of an execution plan that just calls the stored procedure execution plan.  
  
 ODBC applications should use the ODBC CALL syntax when executing a stored procedure. The driver is optimized to use a remote procedure call mechanism to call the procedure when the ODBC CALL syntax is used. This is more efficient than the mechanism used to send a [!INCLUDE[tsql](../../../includes/tsql-md.md)] EXECUTE statement to the server.  
  
 For more information, see [Running Stored Procedures](../../native-client-odbc-stored-procedures/running-stored-procedures.md).  
  
## See Also  
 [Executing Statements &#40;ODBC&#41;](executing-statements-odbc.md)  
  
  
