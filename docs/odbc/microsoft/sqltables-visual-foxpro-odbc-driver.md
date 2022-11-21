---
description: "SQLTables (Visual FoxPro ODBC Driver)"
title: "SQLTables (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLTables function [ODBC], Visual FoxPro ODBC Driver"
ms.assetid: 69e2a038-5def-423f-91aa-8756e069dd2a
author: David-Engel
ms.author: v-davidengel
---
# SQLTables (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Full  
  
 ODBC API Conformance: Level 1  
  
 Returns the list of table names specified by the parameter in the **SQLTables** statement. If no parameter is specified, returns the table names stored in the current data source. The driver returns the information as a result set.  
  
 Enumeration type calls will not receive a result set entry for remote views or local parameterized views. However, a call to **SQLTables** with a unique table name specifier will find a match for such a view if present with that name; this allows the API to be used to check for name conflicts prior to creation of a new table.  
  
> [!NOTE]  
>  The Visual FoxPro ODBC driver differentiates between [database tables](../../odbc/microsoft/visual-foxpro-terminology.md) and [free tables](../../odbc/microsoft/visual-foxpro-terminology.md), even when both types of tables are stored in the same directory on your system. If your data source is a directory of free tables, the Visual FoxPro ODBC Driver does not catalog or return the names of any tables that are associated with a database.  
  
 For more information, see [SQLTables](../../odbc/reference/syntax/sqltables-function.md) in the *ODBC Programmer's Reference*.
