---
title: "SQLProcedureColumns (Access Driver)"
description: "SQLProcedureColumns (Access Driver)"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "Access driver [ODBC], SQLProcedureColumns"
  - "SQLProcedureColumns function [ODBC], Access Driver"
---
# SQLProcedureColumns (Access Driver)
> [!NOTE]  
>  This topic provides Access Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Application developers should look for driver-defined columns starting at the end of the result set and proceeding backward.  
  
|Column|Comments|  
|------------|--------------|  
|COLUMN_TYPE|SQL_PARAM_INPUT or SQL_RESULT_COL|  
|ORDINAL|This is a driver-specific column that is returned at the end of the result set. The SQL type of the column is an integer.|
