---
title: "SQLGetFunctions Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLGetFunctions"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLGetFunctions"
helpviewer_keywords: 
  - "SQLGetFunctions function [ODBC]"
ms.assetid: 0451d2f9-0f4f-46ba-b252-670956a52183
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetFunctions Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLGetFunctions** returns information about whether a driver supports a specific ODBC function. This function is implemented in the Driver Manager; it can also be implemented in drivers. If a driver implements **SQLGetFunctions**, the Driver Manager calls the function in the driver. Otherwise, it executes the function itself.  
  
## Syntax  
  
```  
  
SQLRETURN SQLGetFunctions(  
     SQLHDBC           ConnectionHandle,  
     SQLUSMALLINT      FunctionId,  
     SQLUSMALLINT *    SupportedPtr);  
```  
  
## Arguments  
 *ConnectionHandle*  
 [Input] Connection handle.  
  
 *FunctionId*  
 [Input] A **#define** value that identifies the ODBC function of interest; **SQL_API_ODBC3_ALL_FUNCTIONS orSQL_API_ALL_FUNCTIONS**. **SQL_API_ODBC3_ALL_FUNCTIONS** is used by an ODBC 3*.x* application to determine support of ODBC 3*.x* and earlier functions. **SQL_API_ALL_FUNCTIONS** is used by an ODBC 2*.x* application to determine support of ODBC 2*.x* and earlier functions.  
  
 For a list of **#define** values that identify ODBC functions, see the tables in "Comments."  
  
 *SupportedPtr*  
 [Output]  If *FunctionId* identifies a single ODBC function, *SupportedPtr* points to a single SQLUSMALLINT value that is SQL_TRUE if the specified function is supported by the driver, and SQL_FALSE if it is not supported.  
  
 If *FunctionId* is SQL_API_ODBC3_ALL_FUNCTIONS, *SupportedPtr* points to a SQLSMALLINT array with a number of elements equal to SQL_API_ODBC3_ALL_FUNCTIONS_SIZE. This array is treated by the Driver Manager as a 4,000-bit bitmap that can be used to determine whether an ODBC 3*.x* or earlier function is supported. The SQL_FUNC_EXISTS macro is called to determine function support. (See "Comments.") An ODBC 3*.x* application can call **SQLGetFunctions** with SQL_API_ODBC3_ALL_FUNCTIONS against either an ODBC 3*.x* or ODBC 2*.x* driver.  
  
 If *FunctionId* is SQL_API_ALL_FUNCTIONS, *SupportedPtr* points to an SQLUSMALLINT array of 100 elements. The array is indexed by **#define** values used by *FunctionId* to identify each ODBC function; some elements of the array are unused and reserved for future use. An element is SQL_TRUE if it identifies an ODBC 2*.x* or earlier function supported by the driver. It is SQL_FALSE if it identifies an ODBC function not supported by the driver or does not identify an ODBC function.  
  
 The arrays returned in **SupportedPtr* use zero-based indexing.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLGetFunctions** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_DBC and a *Handle* of *ConnectionHandle*. The following table lists the SQLSTATE values commonly returned by **SQLGetFunctions** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------|-----|-----------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY010|Function sequence error|(DM) **SQLGetFunctions** was called before **SQLConnect**, **SQLBrowseConnect**, or **SQLDriverConnect**.<br /><br /> (DM) **SQLBrowseConnect** was called for the *ConnectionHandle* and returned SQL_NEED_DATA. This function was called before **SQLBrowseConnect** returned SQL_SUCCESS_WITH_INFO or SQL_SUCCESS.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *ConnectionHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY095|Function type out of range|(DM) An invalid *FunctionId* value was specified.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
  
## Comments  
 **SQLGetFunctions** always returns that **SQLGetFunctions**, **SQLDataSources**, and **SQLDrivers** are supported. It does this because these functions are implemented in the Driver Manager. The Driver Manager will map an ANSI function to the corresponding Unicode function if the Unicode function exists and will map a Unicode function to the corresponding ANSI function if the ANSI function exists. For information about how applications use **SQLGetFunctions**, see [Interface Conformance Levels](../../../odbc/reference/develop-app/interface-conformance-levels.md).  
  
 The following is a list of valid values for *FunctionId* for functions that conform to the ISO 92 standards-compliance level:  
  
|FunctionId Value|FunctionId Value|  
|----------|----------|  
|SQL_API_SQLALLOCHANDLE|SQL_API_SQLGETDESCFIELD|  
|SQL_API_SQLBINDCOL|SQL_API_SQLGETDESCREC|  
|SQL_API_SQLCANCEL|SQL_API_SQLGETDIAGFIELD|  
|SQL_API_SQLCLOSECURSOR|SQL_API_SQLGETDIAGREC|  
|SQL_API_SQLCOLATTRIBUTE|SQL_API_SQLGETENVATTR|  
|SQL_API_SQLCONNECT|SQL_API_SQLGETFUNCTIONS|  
|SQL_API_SQLCOPYDESC|SQL_API_SQLGETINFO|  
|SQL_API_SQLDATASOURCES|SQL_API_SQLGETSTMTATTR|  
|SQL_API_SQLDESCRIBECOL|SQL_API_SQLGETTYPEINFO|  
|SQL_API_SQLDISCONNECT|SQL_API_SQLNUMRESULTCOLS|  
|SQL_API_SQLDRIVERS|SQL_API_SQLPARAMDATA|  
|SQL_API_SQLENDTRAN|SQL_API_SQLPREPARE|  
|SQL_API_SQLEXECDIRECT|SQL_API_SQLPUTDATA|  
|SQL_API_SQLEXECUTE|SQL_API_SQLROWCOUNT|  
|SQL_API_SQLFETCH|SQL_API_SQLSETCONNECTATTR|  
|SQL_API_SQLFETCHSCROLL|SQL_API_SQLSETCURSORNAME|  
|SQL_API_SQLFREEHANDLE|SQL_API_SQLSETDESCFIELD|  
|SQL_API_SQLFREESTMT|SQL_API_SQLSETDESCREC|  
|SQL_API_SQLGETCONNECTATTR|SQL_API_SQLSETENVATTR|  
|SQL_API_SQLGETCURSORNAME|SQL_API_SQLSETSTMTATTR|  
|SQL_API_SQLGETDATA| |  
  
 The following is a list of valid values for *FunctionId* for functions conforming to the Open Group standards-compliance level:  
  
|FunctionId Value|FunctionId Value|  
|-|-|  
|SQL_API_SQLCOLUMNS|SQL_API_SQLSTATISTICS|  
|SQL_API_SQLSPECIALCOLUMNS|SQL_API_SQLTABLES|  
  
 The following is a list of valid values for *FunctionId* for functions conforming to the ODBC standards-compliance level.  
  
|FunctionId Value|FunctionId Value|  
|-|-|  
|SQL_API_SQLBINDPARAMETER|SQL_API_SQLNATIVESQL|  
|SQL_API_SQLBROWSECONNECT|SQL_API_SQLNUMPARAMS|  
|SQL_API_SQLBULKOPERATIONS[1]|SQL_API_SQLPRIMARYKEYS|  
|SQL_API_SQLCOLUMNPRIVILEGES|SQL_API_SQLPROCEDURECOLUMNS|  
|SQL_API_SQLDESCRIBEPARAM|SQL_API_SQLPROCEDURES|  
|SQL_API_SQLDRIVERCONNECT|SQL_API_SQLSETPOS|  
|SQL_API_SQLFOREIGNKEYS|SQL_API_SQLTABLEPRIVILEGES|  
|SQL_API_SQLMORERESULTS| |  
  
 [1]   When working with an ODBC 2*.x* driver, **SQLBulkOperations** will be returned as supported only if both of the following are true: the ODBC 2*.x* driver supports **SQLSetPos**, and the information type SQL_POS_OPERATIONS returns the SQL_POS_ADD bit as set.  
  
 The following is a list of valid values for *FunctionId* for functions introduced in ODBC 3.8 or later:  
  
|FunctionId Value|  
|-|  
|SQL_API_SQLCANCELHANDLE [2]|  
  
 [2]   **SQLCancelHandle** will be returned as supported only if the driver supports both **SQLCancel** and **SQLCancelHandle**. If **SQLCancel** is supported but **SQLCancelHandle** is not, the application can still call **SQLCancelHandle** on a statement handle, because it will be mapped to **SQLCancel**.  
  
## SQL_FUNC_EXISTS Macro  
 The SQL_FUNC_EXISTS(*SupportedPtr*, *FunctionID*) macro is used to determine support of ODBC 3*.x* or earlier functions after **SQLGetFunctions** has been called with a *FunctionId* argument of SQL_API_ODBC3_ALL_FUNCTIONS. The application calls SQL_FUNC_EXISTS with the *SupportedPtr* argument set to the *SupportedPtr* passed in *SQLGetFunctions*, and with the *FunctionID* argument set to the **#define** for the function. SQL_FUNC_EXISTS returns SQL_TRUE if the function is supported, and SQL_FALSE otherwise.  
  
> [!NOTE]
>  When working with an ODBC 2*.x* driver, the ODBC 3*.x* Driver Manager will return SQL_TRUE for **SQLAllocHandle** and **SQLFreeHandle** because **SQLAllocHandle** is mapped to **SQLAllocEnv**, **SQLAllocConnect**, or **SQLAllocStmt**, and because **SQLFreeHandle** is mapped to **SQLFreeEnv**, **SQLFreeConnect**, or **SQLFreeStmt**. **SQLAllocHandle** or **SQLFreeHandle** with a *HandleType* argument of SQL_HANDLE_DESC is not supported, however, even though SQL_TRUE is returned for the functions, because there is no ODBC 2*.x* function to map to in this case.  
  
## Code Example  
 The following three examples show how an application uses **SQLGetFunctions** to determine if a driver supports **SQLTables**, **SQLColumns**, and **SQLStatistics**. If the driver does not support these functions, the application disconnects from the driver. The first example calls **SQLGetFunctions** once for each function.  
  
```  
SQLUSMALLINT TablesExists, ColumnsExists, StatisticsExists;  
RETCODE retcodeTables, retcodeColumns, retcodeStatistics  
  
retcodeTables = SQLGetFunctions(hdbc, SQL_API_SQLTABLES, &TablesExists);  
retcodeColumns = SQLGetFunctions(hdbc, SQL_API_SQLCOLUMNS, &ColumnsExists);  
retcodeStatistics = SQLGetFunctions(hdbc, SQL_API_SQLSTATISTICS, &StatisticsExists);  
  
// SQLGetFunctions is completed successfully and SQLTables, SQLColumns, and SQLStatistics are supported by the driver.  
if (retcodeTables == SQL_SUCCESS && TablesExists == SQL_TRUE &&   
retcodeColumns == SQL_SUCCESS && ColumnsExists == SQL_TRUE &&   
retcodeStatistics == SQL_SUCCESS && StatisticsExists == SQL_TRUE)   
{  
  
   // Continue with application  
  
}  
  
SQLDisconnect(hdbc);  
```  
  
 In the second example, an ODBC 3.x application calls **SQLGetFunctions** and passes it an array in which **SQLGetFunctions** returns information about all ODBC 3.x and earlier functions.  
  
```  
RETCODE retcodeTables, retcodeColumns, retcodeStatistics  
SQLUSMALLINT fExists[SQL_API_ODBC3_ALL_FUNCTIONS_SIZE];  
  
retcode = SQLGetFunctions(hdbc, SQL_API_ODBC3_ALL_FUNCTIONS, fExists);  
  
// SQLGetFunctions is completed successfully and SQLTables, SQLColumns, and SQLStatistics are supported by the driver.  
if (reccode == SQL_SUCCESS &&   
SQL_FUNC_EXISTS(fExists, SQL_API_SQLTABLES) == SQL_TRUE &&  
   SQL_FUNC_EXISTS(fExists, SQL_API_SQLCOLUMNS) == SQL_TRUE &&  
   SQL_FUNC_EXISTS(fExists, SQL_API_SQLSTATISTICS) == SQL_TRUE)   
{  
  
   // Continue with application  
  
}  
  
SQLDisconnect(hdbc);  
```  
  
 The third example is an ODBC 2.x application calls **SQLGetFunctions** and passes it an array of 100 elements in which **SQLGetFunctions** returns information about all ODBC 2.x and earlier functions.  
  
```  
#define FUNCTIONS 100  
  
RETCODE retcodeTables, retcodeColumns, retcodeStatistics  
SQLUSMALLINT fExists[FUNCTIONS];  
  
retcode = SQLGetFunctions(hdbc, SQL_API_ALL_FUNCTIONS, fExists);  
  
/* SQLGetFunctions is completed successfully and SQLTables, SQLColumns, and SQLStatistics are supported by the driver. */  
if (retcode == SQL_SUCCESS &&   
fExists[SQL_API_SQLTABLES] == SQL_TRUE &&  
   fExists[SQL_API_SQLCOLUMNS] == SQL_TRUE &&  
   fExists[SQL_API_SQLSTATISTICS] == SQL_TRUE)   
{  
  
   /* Continue with application */  
  
}  
  
SQLDisconnect(hdbc);  
```  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Returning the setting of a connection attribute|[SQLGetConnectAttr Function](../../../odbc/reference/syntax/sqlgetconnectattr-function.md)|  
|Returning information about a driver or data source|[SQLGetInfo Function](../../../odbc/reference/syntax/sqlgetinfo-function.md)|  
|Returning the setting of a statement attribute|[SQLGetStmtAttr Function](../../../odbc/reference/syntax/sqlgetstmtattr-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
