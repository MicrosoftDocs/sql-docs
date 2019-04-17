---
title: "SQLNativeSql Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLNativeSql"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLNativeSql"
helpviewer_keywords: 
  - "SQLNativeSql function [ODBC]"
ms.assetid: b8efc247-27ab-4a00-92b6-1400785783fe
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLNativeSql Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLNativeSql** returns the SQL string as modified by the driver. **SQLNativeSql** does not execute the SQL statement.  
  
## Syntax  
  
```  
  
SQLRETURN SQLNativeSql(  
     SQLHDBC        ConnectionHandle,  
     SQLCHAR *      InStatementText,  
     SQLINTEGER     TextLength1,  
     SQLCHAR *      OutStatementText,  
     SQLINTEGER     BufferLength,  
     SQLINTEGER *   TextLength2Ptr);  
```  
  
## Arguments  
 *ConnectionHandle*  
 [Input] Connection handle.  
  
 *InStatementText*  
 [Input] SQL text string to be translated.  
  
 *TextLength1*  
 [Input] Length in characters of **InStatementText* text string.  
  
 *OutStatementText*  
 [Output] Pointer to a buffer in which to return the translated SQL string.  
  
 If *OutStatementText* is NULL, *TextLength2Ptr* will still return the total number of characters (excluding the null-termination character for character data) available to return in the buffer pointed to by *OutStatementText*.  
  
 *BufferLength*  
 [Input] Number of characters in the \**OutStatementText* buffer. If the value returned in *\*InStatementText* is a Unicode string (when calling **SQLNativeSqlW**), the *BufferLength* argument must be an even number.  
  
 *TextLength2Ptr*  
 [Output] Pointer to a buffer in which to return the total number of characters (excluding null-termination) available to return in \**OutStatementText*. If the number of characters available to return is greater than or equal to *BufferLength*, the translated SQL string in \**OutStatementText* is truncated to *BufferLength* minus the length of a null-termination character.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLNativeSql** returns either SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_DBC and a *Handle* of *ConnectionHandle*. The following table lists the SQLSTATE values commonly returned by **SQLNativeSql** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|The buffer \**OutStatementText* was not large enough to return the entire SQL string, so the SQL string was truncated. The length of the untruncated SQL string is returned in **TextLength2Ptr*. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08003|Connection not open|The *ConnectionHandle* was not in a connected state.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|22007|Invalid datetime format|**InStatementText* contained an escape clause with an invalid date, time, or timestamp value.|  
|24000|Invalid cursor state|The cursor referred to in the statement was positioned before the start of the result set or after the end of the result set. This error may not be returned by a driver having a native DBMS cursor implementation.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY009|Invalid use of null pointer|(DM) **InStatementText* was a null pointer.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the *ConnectionHandle* and was still executing when this function was called.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The argument *TextLength1* was less than 0, but not equal to SQL_NTS.|  
|||(DM) The argument *BufferLength* was less than 0 and the argument *OutStatementText* was not a null pointer.|  
|HY109|Invalid cursor position|The current row of the cursor had been deleted or had not been fetched. This error may not be returned by a driver having a native DBMS cursor implementation.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *ConnectionHandle* does not support the function.|  
  
## Comments  
 The following are examples of what **SQLNativeSql** might return for the following input SQL string containing the scalar function CONVERT. Assume that the column empid is of type INTEGER in the data source:  
  
```  
SELECT { fn CONVERT (empid, SQL_SMALLINT) } FROM employee  
```  
  
 A driver for Microsoft SQL Server might return the following translated SQL string:  
  
```  
SELECT convert (smallint, empid) FROM employee  
```  
  
 A driver for ORACLE Server might return the following translated SQL string:  
  
```  
SELECT to_number (empid) FROM employee  
```  
  
 A driver for Ingres might return the following translated SQL string:  
  
```  
SELECT int2 (empid) FROM employee  
```  
  
 For more information, see [Direct Execution](../../../odbc/reference/develop-app/direct-execution-odbc.md) and [Prepared Execution](../../../odbc/reference/develop-app/prepared-execution-odbc.md).  
  
## Related Functions  
 None.  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
