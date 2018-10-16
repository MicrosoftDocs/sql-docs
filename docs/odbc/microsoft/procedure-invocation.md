---
title: "Procedure Invocation | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL grammar [ODBC], procedure invocation"
  - "procedure invocation [ODBC]"
ms.assetid: b9ff2c3a-2003-4832-adbe-08dd0f5ad948
author: MightyPen
ms.author: genemi
manager: craigg
---
# Procedure Invocation
When the Microsoft Access driver is used, procedures can be invoked from the driver by using the **SQLExecDirect** or **SQLPrepare** function with the following syntax: {CALL *procedure-name* [(*parameter*[,*parameter*]...)]}. Note that expressions are not supported as parameters to a called procedure.  
  
 If a procedure name includes a dash, the name must be delimited with back quotes (`).  
  
 A parameterized query can be called using the previous statement.
