---
description: "SQLSetCursorName Function"
title: "SQLSetCursorName Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLSetCursorName"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLSetCursorName"
helpviewer_keywords: 
  - "SQLSetCursorName function [ODBC]"
ms.assetid: 4e055946-12d4-4589-9891-41617a50f34e
author: David-Engel
ms.author: v-davidengel
---
# SQLSetCursorName Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLSetCursorName** associates a cursor name with an active statement. If an application does not call **SQLSetCursorName**, the driver generates cursor names as needed for SQL statement processing.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLSetCursorName(  
     SQLHSTMT      StatementHandle,  
     SQLCHAR *     CursorName,  
     SQLSMALLINT   NameLength);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *CursorName*  
 [Input] Cursor name. For efficient processing, the cursor name should not include any leading or trailing spaces in the cursor name, and if the cursor name includes a delimited identifier, the delimiter should be positioned as the first character in the cursor name.  
  
 *NameLength*  
 [Input] Length in characters of **CursorName*.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLSetCursorName** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLSetCursorName** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|The cursor name exceeded the maximum limit, so only the maximum allowable number of characters was used.|  
|24000|Invalid cursor state|The statement corresponding to *StatementHandle* was already in an executed or cursor-positioned state.|  
|34000|Invalid cursor name|The cursor name specified in **CursorName* was invalid because it exceeded the maximum length as defined by the driver, or it started with "SQLCUR" or "SQL_CUR."|  
|3C000|Duplicate cursor name|The cursor name specified in **CursorName* already exists.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY009|Invalid use of null pointer|(DM) The argument *CursorName* was a null pointer.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This aynchronous function was still executing when the **SQLSetCursorName** function was called.<br /><br /> (DM) An asynchronously executing function was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The argument *NameLength* was less than 0 but not equal to SQL_NTS.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
  
## Comments  
 Cursor names are used only in positioned update and delete statements (for example, **UPDATE** _table-name_ ...**WHERE CURRENT OF** _cursor-name_). For more information, see [Positioned Update and Delete Statements](../../../odbc/reference/develop-app/positioned-update-and-delete-statements.md). If the application does not call **SQLSetCursorName** to define a cursor name, on execution of a query statement the driver generates a name that begins with the letters SQL_CUR and does not exceed 18 characters in length.  
  
 All cursor names within the connection must be unique. The maximum length of a cursor name is defined by the driver. For maximum interoperability, it is recommended that applications limit cursor names to no more than 18 characters. In ODBC 3*.x*, if a cursor name is a quoted identifier, it is treated in a case-sensitive manner and it can contain characters that the syntax of SQL would not permit or would treat specially, such as blanks or reserved keywords. If a cursor name must be treated in a case-sensitive manner, it must be passed as a quoted identifier.  
  
 A cursor name that is set either explicitly or implicitly remains set until the statement with which it is associated is dropped, using **SQLFreeHandle**. **SQLSetCursorName** can be called to rename a cursor on a statement as long as the cursor is in an allocated or prepared state.  
  
## Code Example  
 In the following example, an application uses **SQLSetCursorName** to set a cursor name for a statement. It then uses that statement to retrieve results from the CUSTOMERS table. Finally, it performs a positioned update to change the phone number of John Smith. Note that the application uses different statement handles for the **SELECT** and **UPDATE** statements.  
  
 For another code example, see [SQLSetPos](../../../odbc/reference/syntax/sqlsetpos-function.md).  
  
```cpp  
#define NAME_LEN 50  
#define PHONE_LEN 10  
  
SQLHSTMT     hstmtSelect,  
SQLHSTMT     hstmtUpdate;  
SQLRETURN    retcode;  
SQLHDBC      hdbc;  
SQLCHAR      szName[NAME_LEN], szPhone[PHONE_LEN];  
SQLINTEGER   cbName, cbPhone;  
  
/* Allocate the statements and set the cursor name. */  
  
SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmtSelect);  
SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmtUpdate);  
SQLSetCursorName(hstmtSelect, "C1", SQL_NTS);  
  
/* SELECT the result set and bind its columns to local buffers. */  
  
SQLExecDirect(hstmtSelect,  
      "SELECT NAME, PHONE FROM CUSTOMERS",  
      SQL_NTS);  
SQLBindCol(hstmtSelect, 1, SQL_C_CHAR, szName, NAME_LEN, &cbName);  
SQLBindCol(hstmtSelect, 2, SQL_C_CHAR, szPhone, PHONE_LEN, &cbPhone);  
  
/* Read through the result set until the cursor is */  
/* positioned on the row for John Smith. */  
  
do  
 retcode = SQLFetch(hstmtSelect);  
while ((retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) &&  
   (strcmp(szName, "Smith, John") != 0));  
  
/* Perform a positioned update of John Smith's name. */  
  
if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
   SQLExecDirect(hstmtUpdate,  
   "UPDATE EMPLOYEE SET PHONE=\"2064890154\" WHERE CURRENT OF C1",  
   SQL_NTS);  
}  
```  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Executing an SQL statement|[SQLExecDirect Function](../../../odbc/reference/syntax/sqlexecdirect-function.md)|  
|Executing a prepared SQL statement|[SQLExecute Function](../../../odbc/reference/syntax/sqlexecute-function.md)|  
|Returning a cursor name|[SQLGetCursorName Function](../../../odbc/reference/syntax/sqlgetcursorname-function.md)|  
|Setting cursor scrolling options|[SQLSetScrollOptions Function](../../../odbc/reference/syntax/sqlsetscrolloptions-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
