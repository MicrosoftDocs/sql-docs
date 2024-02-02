---
title: "SQLGetInfo (Text File Driver)"
description: "SQLGetInfo (Text File Driver)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLGetInfo function [ODBC], Text File Driver"
  - "text file driver [ODBC], SQLGetInfo"
---
# SQLGetInfo (Text File Driver)
> [!NOTE]  
>  This topic provides Text File Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 **SQLGetInfo** supports the SQL_FILE_USAGE information type. The returned value is a 16-bit integer that indicates how the driver directly treats files in a data source:  
  
-   SQL_FILE_NOT_SUPPORTED - The driver is not a single-tier driver.  
  
-   SQL_FILE_TABLE - A single-tier driver treats files in a data source as tables.  
  
-   SQL_FILE_QUALIFIER - A single-tier driver treats files in a data source as a qualifier.  
  
 The ODBC driver returns SQL_FILE_TABLE for the Textdriver, because each file is a table.  
  
## SQL_DBMS_VER  
  
|ISAM|Version|Format of version numbers|  
|----------|-------------|-------------------------------|  
|Text|1.0|01.00.0000|  
  
## SQL_CATALOG_USAGE  
 SQL_QU_DML_STATEMENTS &#124; SQL_QU_TABLE_DEFINITION  
  
## SQL_TIMEDATE_FUNCTIONS  
 SQL_FN_TD_CURDATE &#124;  SQL_FN_TD_CURTIME &#124;  SQL_FN_TD_DAYOFMONTH &#124;  SQL_FN_TD_DAYOFWEEK &#124; SQL_FN_TD_DAYOFYEAR &#124;  SQL_FN_TD_HOUR &#124; SQL_FN_TD_MINUTE &#124; SQL_FN_TD_MONTH &#124;  SQL_FN_TD_NOW &#124; SQL_FN_TD_SECOND &#124; SQL_FN_TD_WEEK &#124; SQL_FN_TD_YEAR
