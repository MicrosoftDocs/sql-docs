---
title: "SQLStatistics (Excel Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Excel driver [ODBC], SQLStatistics"
  - "SQLStatistics function [ODBC], Excel Driver"
ms.assetid: 02506664-8dcc-4bd0-a8bb-d49fcbdd5722
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLStatistics (Excel Driver)
> [!NOTE]  
>  This topic provides Excel Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
|Column|Comments|  
|------------|--------------|  
|TABLE_QUALIFIER|The path to a directory.<br /><br /> Pattern matching is not supported in the *szTableQualifier* argument.|  
|TABLE_OWNER|NULL is returned in this column because owner name is not supported.|  
|TABLE_NAME|Undelimited table name.<br /><br /> Pattern matching is not supported in the *szTableName* argument.|  
|INDEX_QUALIFIER|NULL is always returned.|  
|INDEX_NAME|Index-dependent.|  
|TYPE|Only SQL_TABLE_STAT or SQL_INDEX_OTHER will be returned for TYPE.|  
|SEQ_IN_INDEX|Index-dependent.|  
|COLUMN_NAME|Index-dependent.|  
|COLLATION|Index-dependent.|  
|PAGES|NULL is always returned.|  
  
 Filtering is based on uniqueness (the *fUnique* argument). The *fAccuracy* parameter is ignored.
