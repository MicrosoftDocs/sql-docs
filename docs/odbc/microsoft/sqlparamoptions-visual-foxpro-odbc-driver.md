---
title: "SQLParamOptions (Visual FoxPro ODBC Driver)"
description: "SQLParamOptions (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "SQLParamOptions function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLParamOptions (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Full  
  
 ODBC API Conformance: Level 1  
  
 Allows an application to specify multiple values for the set of parameters assigned by [SQLBindParameter](../../odbc/microsoft/sqlbindparameter-visual-foxpro-odbc-driver.md). The ability to specify multiple values for a set of parameters is useful for bulk inserts and other work that requires the data source to process the same SQL statement multiple times with various parameter values. For example, an application can specify three sets of values for the set of parameters associated with an **INSERT** statement and then execute the **INSERT** statement once to perform the three insert operations.  
  
 For more information, see [SQLParamOptions](../../odbc/reference/syntax/sqlparamoptions-function.md) in the *ODBC Programmer's Reference*.
