---
title: "Procedure Invocation"
description: "Procedure Invocation"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQL grammar [ODBC], procedure invocation"
  - "procedure invocation [ODBC]"
---
# Procedure Invocation
When the Microsoft Access driver is used, procedures can be invoked from the driver by using the **SQLExecDirect** or **SQLPrepare** function with the following syntax: {CALL *procedure-name* [(*parameter*[,*parameter*]...)]}. Note that expressions are not supported as parameters to a called procedure.  
  
 If a procedure name includes a dash, the name must be delimited with back quotes (`).  
  
 A parameterized query can be called using the previous statement.
