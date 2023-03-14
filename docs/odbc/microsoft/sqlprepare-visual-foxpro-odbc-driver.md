---
title: "SQLPrepare (Visual FoxPro ODBC Driver)"
description: "SQLPrepare (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "SQLPrepare function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLPrepare (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Full  
  
 ODBC API Conformance: Core Level  
  
 Prepares an SQL statement by planning how to optimize and execute the statement. The SQL statement is compiled for execution by [SQLExecDirect](../../odbc/microsoft/sqlexecdirect-visual-foxpro-odbc-driver.md).  
  
 If your table, view, or field names contain spaces, enclose the names in back quote (`) marks. For example, if your database contains a table named My Table and the field My Field, enclose each element of the identifier as follows:  
  
```  
SELECT * FROM `My Table`.`My Field`  
```  
  
 For more information, see [SQLPrepare](../../odbc/reference/syntax/sqlprepare-function.md) in the *ODBC Programmer's Reference*.
