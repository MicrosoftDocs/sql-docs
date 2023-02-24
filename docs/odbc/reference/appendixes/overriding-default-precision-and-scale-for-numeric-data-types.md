---
description: "Overriding Default Precision and Scale for Numeric Data Types"
title: "Overriding Default Precision and Scale for Numeric Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "numeric data type [ODBC], precision and scale"
  - "precision [ODBC], numeric data types"
  - "data types [ODBC], numeric data types"
  - "numeric data type [ODBC]"
  - "numeric literals [ODBC]"
ms.assetid: 84292334-0e33-4a1b-84de-8c018dd787f3
author: David-Engel
ms.author: v-davidengel
---
# Overriding Default Precision and Scale for Numeric Data Types
When the SQL_DESC_TYPE field in an ARD is set to SQL_C_NUMERIC, by calling either **SQLBindCol** or **SQLSetDescField**, the SQL_DESC_SCALE field in the ARD is set to 0 and the SQL_DESC_PRECISION field is set to a driver-defined default precision. This is also true when the SQL_DESC_TYPE field in an APD is set to SQL_C_NUMERIC, by calling either **SQLBindParameter** or **SQLSetDescField**. This is true for input, input/output, or output parameters.  
  
 If either of the defaults described previously are not acceptable for an application, the application should set the SQL_DESC_SCALE or SQL_DESC_PRECISION field by calling **SQLSetDescField** or **SQLSetDescRec**.  
  
 If the application calls **SQLGetData** to return data into an SQL_C_NUMERIC structure, the default SQL_DESC_SCALE and SQL_DESC_PRECISION fields are used. If the defaults are not acceptable, the application must call **SQLSetDescRec** or **SQLSetDescField** to set the fields and then call **SQLGetData** with a *TargetType* of SQL_ARD_TYPE to use the values in the descriptor fields.  
  
 When **SQLPutData** is called, the call uses the SQL_DESC_SCALE and SQL_DESC_PRECISION fields of the descriptor record that corresponds to the data-at-execution parameter or column, which are APD fields for calls to **SQLExecute** or **SQLExecDirect**, or ARD fields for calls to **SQLBulkOperations** or **SQLSetPos**.
