---
title: "SQLGetTypeInfo (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLGetTypeInfo function [ODBC], Visual FoxPro ODBC Driver"
ms.assetid: 5f25e20b-a4ef-42da-aeb6-00e0510fb1cc
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetTypeInfo (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Full  
  
 ODBC API Conformance: Level 1  
  
 Returns information about the data types supported by a data source. The driver returns the information in an SQL result set. The following table lists ODBC data types and the corresponding Visual FoxPro data type.  
  
|ODBC type|Visual FoxPro type|  
|---------------|------------------------|  
|SQL_BIGINT|Not supported. There is no 64-bit Visual FoxPro type.|  
|SQL_BIT|Logical|  
|SQL_CHAR|Character|  
|SQL_DATE|Date|  
|SQL_DECIMAL|Numeric|  
|SQL_DOUBLE|Double|  
|SQL_FLOAT|Double|  
|SQL_INTEGER|Integer|  
|SQL_LONGVARBINARY|Memo (Binary)|  
|SQL_LONGVARCHAR|Memo|  
|SQL_NUMERIC|Numeric*, Currency, Float|  
|SQL_REAL|Double|  
|SQL_SMALLINT|Integer|  
|SQL_TIME|Not supported. There is no Visual FoxPro *time* type.|  
|SQL_TIMESTAMP|DateTime|  
|SQL_TINYINT|Integer|  
|SQL_VARBINARY|Memo (Binary)*, General|  
|SQL_VARCHAR|Character|  
  
 *Default type  
  
 For more information about Visual FoxPro data types, see [CREATE TABLE](../../odbc/microsoft/create-table-sql-command.md). For more information about this function, see [SQLGetTypeInfo](../../odbc/reference/syntax/sqlgettypeinfo-function.md) in the *ODBC Programmer's Reference*.
