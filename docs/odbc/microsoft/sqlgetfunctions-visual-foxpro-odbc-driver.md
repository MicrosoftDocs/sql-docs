---
title: "SQLGetFunctions (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLGetFunctions function [ODBC], Visual FoxPro ODBC Driver"
ms.assetid: 8102932a-88b3-49d8-bf7a-c766f54878c0
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetFunctions (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Full  
  
 ODBC API Conformance: Level 1  
  
 Returns TRUE for all supported functions.  
  
 The Visual FoxPro ODBC Driver supports all ODBC API Core and Level 1 functions. The following table indicates whether the driver supports a specific Level 2 function.  
  
|*Function*|Supported|  
|----------------|---------------|  
|SQL_API_SQLBROWSECONNECT|No|  
|SQL_API_SQLCOLUMNPRIVELEGES|No|  
|SQL_API_SQLDATASOURCES|Yes|  
|SQL_API_SQLDESCRIBEPARAM|No|  
|SQL_API_SQLDRIVERS|Yes|  
|SQL_API_SQLEXTENDEDFETCH|Yes|  
|SQL_API_SQLFOREIGNKEYS|No|  
|SQL_API_SQLMORERESULTS|Yes|  
|SQL_API_SQLNATIVESQL|No|  
|SQL_API_SQLNUMPARAMS|Yes|  
|SQL_API_SQLPARAMOPTIONS|Yes|  
|SQL_API_SQLPRIMARYKEYS|Yes|  
|SQL_API_SQLPROCEDURECOLUMNS|No|  
|SQL_API_SQLPROCEDURES|No|  
|SQL_API_SQLSETPOS|Yes|  
|SQL_API_SQLSETSCROLLOPTIONS|Yes|  
|SQL_API_SQLTABLEPRIVILEGES|No|  
  
 For more information, see [SQLGetFunctions](../../odbc/reference/syntax/sqlgetfunctions-function.md) in the *ODBC Programmer's Reference*.
