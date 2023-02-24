---
description: "SQLExecDirect (Visual FoxPro ODBC Driver)"
title: "SQLExecDirect (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLExecDirect function [ODBC], Visual FoxPro ODBC Driver"
ms.assetid: 5004060f-8510-4018-87a4-d41789e69d3e
author: David-Engel
ms.author: v-davidengel
---
# SQLExecDirect (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Full  
  
 ODBC API Conformance: Core Level  
  
 Executes a new, [preparable SQL statement](../../odbc/microsoft/visual-foxpro-terminology.md). The Visual FoxPro ODBC Driver uses the current values of the parameter marker variables if any parameters exist in the statement.  
  
 To create a batch command to submit more than one SQL statement at a time, use a semicolon (;) to separate each SQL statement in the batch.  
  
 If your table, view, or field names contain spaces, enclose the names in back quote marks. For example, if your database contains a table named My Table and the field My Field, enclose each element of the identifier as follows:  
  
```  
SELECT `My Table`.`Field1`, `My Table`.`Field2` FROM `My Table`  
```  
  
 For more information, see [SQLExecDirect](../../odbc/reference/syntax/sqlexecdirect-function.md) in the *ODBC Programmer's Reference*.
