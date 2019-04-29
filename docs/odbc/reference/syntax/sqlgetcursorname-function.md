---
title: "SQLGetCursorName Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLGetCursorName"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLGetCursorName"
helpviewer_keywords: 
  - "SQLGetCursorName function [ODBC]"
ms.assetid: e6e92199-7bb6-447c-8987-049a4c6ce05d
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetCursorName Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLGetCursorName** returns the cursor name associated with a specified statement.  
  
## Syntax  
  
```  
  
SQLRETURN SQLGetCursorName(  
     SQLHSTMT        StatementHandle,  
     SQLCHAR *       CursorName,  
     SQLSMALLINT     BufferLength,  
     SQLSMALLINT *   NameLengthPtr);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *CursorName*  
 [Output] Pointer to a buffer in which to return the cursor name.  
  
 If *CursorName* is NULL, *NameLengthPtr* will still return the total number of characters (excluding the null-termination character for character data) available to return in the buffer pointed to by *CursorName*.  
  
 *BufferLength*  
 [Input] Length of \**CursorName*, in characters. If the value in *\*CursorName* is a Unicode string (when calling **SQLGetCursorNameW**), the *BufferLength* argument must be an even number.  
  
 *NameLengthPtr*  
 [Output] Pointer to memory in which to return the total number of characters (excluding the null-termination character) available to return in \**CursorName*. If the number of characters available to return is greater than or equal to *BufferLength*, the cursor name in \**CursorName* is truncated to *BufferLength* minus the length of a null-termination character.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLGetCursorName** returns either SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLGetCursorName** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|The buffer \**CursorName* was not large enough to return the entire cursor name, so the cursor name was truncated. The length of the untruncated cursor name is returned in **NameLengthPtr*. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLGetCursorName** function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) An asynchronously executing function was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY015|No cursor name available|(DM) The driver was an ODBC 2*.x* driver, there was no open cursor on the statement, and no cursor name had been set with **SQLSetCursorName**.|  
|HY090|Invalid string or buffer length|(DM) The value specified in the argument *BufferLength* was less than 0.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
  
## Comments  
 Cursor names are used only in positioned update and delete statements (for example, **UPDATE** _table-name_ ...**WHERE CURRENT OF** _cursor-name_). For more information, see [Positioned Update and Delete Statements](../../../odbc/reference/develop-app/positioned-update-and-delete-statements.md). If the application does not call **SQLSetCursorName** to define a cursor name, the driver generates a name. This name begins with the letters SQL_CUR.  
  
> [!NOTE]
>  In ODBC 2*.x*, when there was no open cursor and no name had been set by a call to **SQLSetCursorName**, a call to **SQLGetCursorName** returned SQLSTATE HY015 (No cursor name available). In ODBC 3*.x*, this is no longer true; regardless of when **SQLGetCursorName** is called, the driver returns the cursor name.  
  
 **SQLGetCursorName** returns the name of a cursor whether or not the name was created explicitly or implicitly. A cursor name is implicitly generated if **SQLSetCursorName** is not called. **SQLSetCursorName** can be called to rename a cursor on a statement as long as the cursor is in an allocated or prepared state.  
  
 A cursor name that is set either explicitly or implicitly remains set until the *StatementHandle* with which it is associated is dropped, using **SQLFreeHandle** with a *HandleType* of SQL_HANDLE_STMT.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Executing an SQL statement|[SQLExecDirect Function](../../../odbc/reference/syntax/sqlexecdirect-function.md)|  
|Executing a prepared SQL statement|[SQLExecute Function](../../../odbc/reference/syntax/sqlexecute-function.md)|  
|Preparing a statement for execution|[SQLPrepare Function](../../../odbc/reference/syntax/sqlprepare-function.md)|  
|Setting a cursor name|[SQLSetCursorName Function](../../../odbc/reference/syntax/sqlsetcursorname-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
