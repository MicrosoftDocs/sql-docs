---
description: "SQLDriverToDataSource Function"
title: "SQLDriverToDataSource Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLDriverToDataSource"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLDriverToDataSource"
helpviewer_keywords: 
  - "SQLDriverToDataSource function [ODBC]"
ms.assetid: 0de28eb5-8aa9-43e4-a87f-7dbcafe800dc
author: David-Engel
ms.author: v-davidengel
---
# SQLDriverToDataSource Function
**SQLDriverToDataSource** supports translations for ODBC drivers. This function is not called by ODBC-enabled applications; applications request translation through **SQLSetConnectAttr**. The driver associated with the *ConnectionHandle* specified in **SQLSetConnectAttr** calls the specified DLL to perform translations of all data flowing from the driver to the data source. A default translation DLL can be specified in the ODBC initialization file.  
  
## Syntax  
  
```cpp  
  
BOOL SQLDriverToDataSource(  
     UDWORD     fOption,  
     SWORD      fSqlType,  
     PTR        rgbValueIn,  
     SDWORD     cbValueIn,  
     PTR        rgbValueOut,  
     SDWORD     cbValueOutMax,  
     SDWORD *   pcbValueOut,  
     UCHAR *    szErrorMsg,  
     SWORD      cbErrorMsgMax,  
     SWORD *    pcbErrorMsg);  
```  
  
## Arguments  
 *fOption*  
 [Input] Option value.  
  
 *fSqlType*  
 [Input] The ODBC SQL data type. This argument tells the driver how to convert *rgbValueIn* into a form acceptable by the data source. For a list of valid SQL data types, see [SQL Data Types](../../../odbc/reference/appendixes/sql-data-types.md).  
  
 *rgbValueIn*  
 [Input] Value to translate.  
  
 *cbValueIn*  
 [Input] Length of *rgbValueIn*.  
  
 *rgbValueOut*  
 [Output] Result of the translation.  
  
> [!NOTE]  
>  The translation DLL does not null-terminate this value.  
  
 *cbValueOutMax*  
 [Input] Length of *rgbValueOut*.  
  
 *pcbValueOut*  
 [Output] The total number of bytes (excluding the null-termination byte) available to return in *rgbValueOut*.  
  
 For character or binary data, if this is greater than or equal to *cbValueOutMax*, the data in *rgbValueOut* is truncated to *cbValueOutMax* bytes.  
  
 For all other data types, the value of *cbValueOutMax* is ignored and the translation DLL assumes that the size of *rgbValueOut* is the size of the default C data type of the SQL data type specified with *fSqlType*.  
  
 The *pcbValueOut* argument can be a null pointer.  
  
 *szErrorMsg*  
 [Output] Pointer to storage for an error message. This is an empty string unless the translation failed.  
  
 *cbErrorMsgMax*  
 [Input] Length of *szErrorMsg*.  
  
 *pcbErrorMsg*  
 [Output] Pointer to the total number of bytes (excluding the null-termination byte) available to return in *szErrorMsg*. If this is greater than or equal to *cbErrorMsg*, the data in *szErrorMsg* is truncated to *cbErrorMsgMax* minus the null-termination character. The *pcbErrorMsg* argument can be a null pointer.  
  
## Returns  
 TRUE if the translation was successful, FALSE if the translation failed.  
  
## Comments  
 The driver calls **SQLDriverToDataSource** to translate all data (SQL statements, parameters, and so on) passing from the driver to the data source. The translation DLL might not translate some data, depending on the data's type and the purpose of the translation DLL. For example, a DLL that translates character data from one code page to another ignores all numeric and binary data.  
  
 The value of *fOption* is set to the value of *vParam* specified by calling **SQLSetConnectAttr** with the SQL_ATTR_TRANSLATE_OPTION attribute. It is a 32-bit value that has a specific meaning for a given translation DLL. For example, it could specify a certain character set translation.  
  
 If the same buffer is specified for *rgbValueIn* and *rgbValueOut*, the translation of data in the buffer will be performed in place.  
  
 Although *cbValueIn*, *cbValueOutMax*, and *pcbValueOut* are of the type SDWORD, **SQLDriverToDataSource** does not necessarily support huge pointers.  
  
 If **SQLDriverToDataSource** returns FALSE, data truncation might have occurred during translation. If *pcbValueOut* (the number of bytes available to return in the output buffer) is greater than *cbValueOutMax* (the length of the output buffer), then truncation occurred. The driver must determine whether or not the truncation was acceptable. If truncation did not occur, the **SQLDriverToDataSource** returned FALSE due to another error. In either case, a specific error message is returned in *szErrorMsg*.  
  
 For more information about translating data, see [Translation DLLs](../../../odbc/reference/develop-app/translation-dlls.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Translating data returned from the data source|[SQLDataSourceToDriver](../../../odbc/reference/syntax/sqldatasourcetodriver-function.md)|  
|Returning the setting of a connection attribute|[SQLGetConnectAttr](../../../odbc/reference/syntax/sqlgetconnectattr-function.md)|  
|Setting a connection attribute|[SQLSetConnectAttr](../../../odbc/reference/syntax/sqlsetconnectattr-function.md)|
