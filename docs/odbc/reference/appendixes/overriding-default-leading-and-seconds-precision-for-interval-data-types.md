---
description: "Overriding Default Leading and Seconds Precision for Interval Data Types"
title: "Override Leading and Seconds Precision for Interval Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "data types [ODBC], interval data types"
  - "precision [ODBC], interval data types"
  - "seconds precision [ODBC]"
  - "interval data type [ODBC], precision"
  - "interval leading precision [ODBC]"
  - "interval precision [ODBC]"
ms.assetid: 3d65493f-dce7-4d29-9f59-c63a4e47918c
author: David-Engel
ms.author: v-davidengel
---
# Overriding Default Leading and Seconds Precision for Interval Data Types
When the SQL_DESC_TYPE field of an ARD is set to a datetime or interval C type, by calling either **SQLBindCol** or **SQLSetDescField**, the SQL_DESC_PRECISION field (which contains the interval seconds precision) is set to the following defaults:  
  
-   6 for timestamp and all interval data types with a second component.  
  
-   0 for all other data types.  
  
 For all interval data types, the SQL_DESC_DATETIME_INTERVAL_PRECISION descriptor field, which contains the interval leading field precision, is set to a default value of 2.  
  
 When the SQL_DESC_TYPE field in an APD is set to a datetime or interval C type, by calling either **SQLBindParameter** or **SQLSetDescField**, the SQL_DESC_PRECISION and SQL_DESC_DATETIME_INTERVAL_PRECISION fields in the APD are set to the default given previously. This is true for input parameters but not for input/output or output parameters.  
  
 A call to **SQLSetDescRec** sets the interval leading precision to the default but sets the interval seconds precision (in the SQL_DESC_PRECISION field) to the value of its *Precision* argument.  
  
 If either of the defaults given previously is not acceptable to an application, the application should set the SQL_DESC_PRECISION or SQL_DESC_DATETIME_INTERVAL_PRECISION field by calling **SQLSetDescField**.  
  
 If the application calls **SQLGetData** to return data into a datetime or interval C type, the default interval leading precision and interval seconds precision are used. If either default is not acceptable, the application must call **SQLSetDescField** to set either descriptor field, or **SQLSetDescRec** to set SQL_DESC_PRECISION. The call to **SQLGetData** should have a *TargetType* of SQL_ARD_TYPE to use the values in the descriptor fields.  
  
 When **SQLPutData** is called, the interval leading precision and interval seconds precision are read from the fields of the descriptor record that correspond to the data-at-execution parameter or column, which are APD fields for calls to **SQLExecute** or **SQLExecDirect**, or ARD fields for calls to **SQLBulkOperations** or **SQLSetPos**.
