---
title: "Using Length and Indicator Values | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data buffers [ODBC], length"
  - "length/indicator buffers [ODBC]"
  - "length of data buffers [ODBC]"
  - "buffers [ODBC], length"
ms.assetid: 849792f1-cb1e-4bc2-b568-c0aff0b66199
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Length and Indicator Values
The length/indicator buffer is used to pass the byte length of the data in the data buffer or a special indicator such as SQL_NULL_DATA, which indicates that the data is NULL. Depending on the function in which it is used, a length/indicator buffer is defined to be an SQLINTEGER or an SQLSMALLINT. Therefore, a single argument is needed to describe it. If the data buffer is a nondeferred input buffer, this argument contains the byte length of the data itself or an indicator value. It is often named *StrLen_or_Ind* or a similar name. For example, the following code calls **SQLPutData** to pass a buffer full of data; the byte length (*ValueLen*) is passed directly because the data buffer (*ValuePtr*) is an input buffer.  
  
```  
SQLCHAR      ValuePtr[50];  
SQLINTEGER   ValueLen;  
  
// Call local function to place data in ValuePtr. In ValueLen, return the  
// number of bytes of data placed in ValuePtr. If there is not enough  
// data, this will be less than 50.  
FillBuffer(ValuePtr, sizeof(ValuePtr), &ValueLen);  
  
// Call SQLPutData to send the data to the driver.  
SQLPutData(hstmt, ValuePtr, ValueLen);  
```  
  
 If the data buffer is a deferred input buffer, a nondeferred output buffer, or an output buffer, the argument contains the address of the length/indicator buffer. It is often named *StrLen_or_IndPtr* or a similar name. For example, the following code calls **SQLGetData** to retrieve a buffer full of data; the byte length is returned to the application in the length/indicator buffer (*ValueLenOrInd*), whose address is passed to **SQLGetData** because the corresponding data buffer (*ValuePtr*) is a nondeferred output buffer.  
  
```  
SQLCHAR      ValuePtr[50];  
SQLINTEGER   ValueLenOrInd;  
SQLGetData(hstmt, 1, SQL_C_CHAR, ValuePtr, sizeof(ValuePtr), &ValueLenOrInd);  
```  
  
 Unless it is specifically prohibited, a length/indicator buffer argument can be 0 (if nondeferred input) or a null pointer (if output or deferred input). For input buffers, this causes the driver to ignore the byte length of the data. This returns an error when passing variable-length data but is common when passing non-null, fixed-length data, because neither a length nor an indicator value is needed. For output buffers, this causes the driver to not return the byte length of the data or an indicator value. This is an error if the data returned by the driver is NULL but is common when retrieving fixed-length, non-nullable data, because neither a length nor an indicator value is needed.  
  
 As when the address of a deferred data buffer is passed to the driver, the address of a deferred length/indicator buffer must remain valid until the buffer is unbound.  
  
 The following lengths are valid as length/indicator values:  
  
-   *n*, where *n* > 0.  
  
-   0.  
  
-   SQL_NTS. A string sent to the driver in the corresponding data buffer is null-terminated; this is a convenient way for C programmers to pass strings without having to calculate their byte length. This value is legal only when the application sends data to the driver. When the driver returns data to the application, it always returns the actual byte length of the data.  
  
 The following values are valid as length/indicator values. SQL_NULL_DATA is stored in the SQL_DESC_INDICATOR_PTR descriptor field; all other values are stored in the SQL_DESC_OCTET_LENGTH_PTR descriptor field.  
  
-   SQL_NULL_DATA. The data is a NULL data value, and the value in the corresponding data buffer is ignored. This value is legal only for SQL data sent to or retrieved from the driver.  
  
-   SQL_DATA_AT_EXEC. The data buffer does not contain any data. Instead, the data will be sent with **SQLPutData** when the statement is executed or when **SQLBulkOperations** or **SQLSetPos** is called. This value is legal only for SQL data sent to the driver. For more information, see [SQLBindParameter](../../../odbc/reference/syntax/sqlbindparameter-function.md), [SQLBulkOperations](../../../odbc/reference/syntax/sqlbulkoperations-function.md), and [SQLSetPos](../../../odbc/reference/syntax/sqlsetpos-function.md).  
  
-   Result of the SQL_LEN_DATA_AT_EXEC(*length*) macro. This value is similar to SQL_DATA_AT_EXEC. For more information, see [Sending Long Data](../../../odbc/reference/develop-app/sending-long-data.md).  
  
-   SQL_NO_TOTAL. The driver cannot determine the number of bytes of long data still available to return in an output buffer. This value is legal only for SQL data retrieved from the driver.  
  
-   SQL_DEFAULT_PARAM. A procedure is to use the default value of an input parameter in a procedure instead of the value in the corresponding data buffer.  
  
-   SQL_COLUMN_IGNORE. **SQLBulkOperations** or **SQLSetPos** is to ignore the value in the data buffer. When updating a row of data by a call to **SQLBulkOperations** or **SQLSetPos,** the column value is not changed. When inserting a new row of data by a call to **SQLBulkOperations**, the column value is set to its default or, if the column does not have a default, to NULL.
