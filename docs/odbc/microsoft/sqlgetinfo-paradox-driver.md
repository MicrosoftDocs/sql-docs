---
title: "SQLGetInfo (Paradox Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Paradox driver [ODBC], SQLGetInfo"
  - "SQLGetInfo function [ODBC], Paradox Driver"
ms.assetid: 43aab762-68f4-4128-b8f5-8878ea5f1258
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetInfo (Paradox Driver)
> [!NOTE]  
>  This topic provides Paradox Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 **SQLGetInfo** supports the SQL_FILE_USAGE information type. The returned value is a 16-bit integer that indicates how the driver directly treats files in a data source:  
  
-   SQL_FILE_NOT_SUPPORTED - The driver is not a single-tier driver.  
  
-   SQL_FILE_TABLE - A single-tier driver treats files in a data source as tables.  
  
-   SQL_FILE_QUALIFIER - A single-tier driver treats files in a data source as a qualifier.  
  
 The ODBC driver returns SQL_FILE_TABLE because each file is a table.  
  
## SQL_ALTER_TABLE  
 SQL_AT_ADD_COLUMN &#124; SQL_AT_DROP_COLUMN  
  
## SQL_DDL_INDEX  
 SQL_DL_CREATE_INDEX  
  
 SQL_DL_DROP_INDEX  
  
## SQL_DBMS_VER  
  
|ISAM|Version|Format of version numbers|  
|----------|-------------|-------------------------------|  
|Paradox|3.x|03.00.0000|  
||4.x|04.00.0000|  
||5.x|05.00.0000|  
  
## SQL_CATALOG_USAGE  
 SQL_QU_DML_STATEMENTS &#124; SQL_QU_TABLE_DEFINITION &#124; SQL_QU_INDEX_DEFINITION  
  
## SQL_TIMEDATE_FUNCTIONS  
 SQL_FN_TD_DAYOFMONTH &#124;  SQL_FN_TD_DAYOFWEEK &#124; SQL_FN_TD_DAYOFYEAR &#124;  SQL_FN_TD_HOUR &#124; SQL_FN_TD_MINUTE &#124; SQL_FN_TD_MONTH &#124;  SQL_FN_TD_SECOND &#124; SQL_FN_TD_WEEK &#124; SQL_FN_TD_YEAR
