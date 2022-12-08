---
description: "SQLPutData Function"
title: "SQLPutData Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLPutData"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLPutData"
helpviewer_keywords: 
  - "SQLPutData function [ODBC]"
ms.assetid: 9a60f004-1477-4c54-a20c-7378e1116713
author: David-Engel
ms.author: v-davidengel
---
# SQLPutData Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLPutData** allows an application to send data for a parameter or column to the driver at statement execution time. This function can be used to send character or binary data values in parts to a column with a character, binary, or data source-specific data type (for example, parameters of the SQL_LONGVARBINARY or SQL_LONGVARCHAR types). **SQLPutData** supports binding to a Unicode C data type, even if the underlying driver does not support Unicode data.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLPutData(  
      SQLHSTMT     StatementHandle,  
      SQLPOINTER   DataPtr,  
      SQLLEN       StrLen_or_Ind);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *DataPtr*  
 [Input] Pointer to a buffer containing the actual data for the parameter or column. The data must be in the C data type specified in the *ValueType* argument of **SQLBindParameter** (for parameter data) or the *TargetType* argument of **SQLBindCol** (for column data).  
  
 *StrLen_or_Ind*  
 [Input] Length of \**DataPtr*. Specifies the amount of data sent in a call to **SQLPutData**. The amount of data can vary with each call for a given parameter or column. *StrLen_or_Ind* is ignored unless it meets one of the following conditions:  
  
-   *StrLen_or_Ind* is SQL_NTS, SQL_NULL_DATA, or SQL_DEFAULT_PARAM.  
  
-   The C data type specified in **SQLBindParameter** or **SQLBindCol** is SQL_C_CHAR or SQL_C_BINARY.  
  
-   The C data type is SQL_C_DEFAULT, and the default C data type for the specified SQL data type is SQL_C_CHAR or SQL_C_BINARY.  
  
 For all other types of C data, if *StrLen_or_Ind* is not SQL_NULL_DATA or SQL_DEFAULT_PARAM, the driver assumes that the size of the \**DataPtr* buffer is the size of the C data type specified with *ValueType* or *TargetType* and sends the entire data value. For more information, see [Converting Data from C to SQL Data Types](../../../odbc/reference/appendixes/converting-data-from-c-to-sql-data-types.md) in Appendix D: Data Types.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLPutData** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLPutData** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|String or binary data returned for an output parameter resulted in the truncation of nonblank character or non-NULL binary data. If it was a string value, it was right-truncated. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07006|Restricted data type  attribute violation|The data value identified by the *ValueType* argument in **SQLBindParameter** for the bound parameter could not be converted to the data type identified by the *ParameterType* argument in **SQLBindParameter**.|  
|07S01|Invalid use of default parameter|A parameter value, set with **SQLBindParameter**, was SQL_DEFAULT_PARAM, and the corresponding parameter did not have a default value.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|22001|String data, right truncation|The assignment of a character or binary value to a column resulted in the truncation of nonblank (character) or non-null (binary) characters or bytes.<br /><br /> The SQL_NEED_LONG_DATA_LEN information type in **SQLGetInfo** was "Y", and more data was sent for a long parameter (the data type was SQL_LONGVARCHAR, SQL_LONGVARBINARY, or a long data source-specific data type) than was specified with the *StrLen_or_IndPtr* argument in **SQLBindParameter**.<br /><br /> The SQL_NEED_LONG_DATA_LEN information type in **SQLGetInfo** was "Y", and more data was sent for a long column (the data type was SQL_LONGVARCHAR, SQL_LONGVARBINARY, or a long data source-specific data type) than was specified in the length buffer corresponding to a column in a row of data that was added or updated with **SQLBulkOperations** or updated with **SQLSetPos**.|  
|22003|Numeric value out of range|The data sent for a bound numeric parameter or column caused the whole (as opposed to fractional) part of the number to be truncated when assigned to the associated table column.<br /><br /> Returning a numeric value (as numeric or string) for one or more input/output or output parameters would have caused the whole (as opposed to fractional) part of the number to be truncated.|  
|22007|Invalid datetime format|The data sent for a parameter or column that was bound to a date, time, or timestamp structure was, respectively, an invalid date, time, or timestamp.<br /><br /> An input/output or output parameter was bound to a date, time, or timestamp C structure, and a value in the returned parameter was, respectively, an invalid date, time, or timestamp. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|22008|Datetime field overflow|A datetime expression computed for an input/output or output parameter resulted in a date, time, or timestamp C structure that was invalid.|  
|22012|Division by zero|An arithmetic expression calculated for an input/output or output parameter resulted in division by zero.|  
|22015|Interval field overflow|The data sent for an exact numeric or interval column or parameter to an interval SQL data type caused a loss of significant digits.<br /><br /> Data was sent for an interval column or parameter with more than one field, was converted to a numeric data type, and had no representation in the numeric data type.<br /><br /> The data sent for column or parameter data was assigned to an interval SQL type, and there was no representation of the value of the C type in the interval SQL type.<br /><br /> The data sent for an exact numeric or interval C column or parameter to an interval C type caused a loss of significant digits.<br /><br /> The data sent for column or parameter data was assigned to an interval C structure, and there was no representation of the data in the interval data structure.|  
|22018|Invalid character value for cast specification|The C type was an exact or approximate numeric, a datetime, or an interval data type; the SQL type of the column was a character data type; and the value in the column or parameter was not a valid literal of the bound C type.<br /><br /> The SQL type was an exact or approximate numeric, a datetime, or an interval data type; the C type was SQL_C_CHAR; and the value in the column or parameter was not a valid literal of the bound SQL type.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*. Then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY009|Invalid use of null pointer|(DM) The argument *DataPtr* was a null pointer, and the argument *StrLen_or_Ind* was not 0, SQL_DEFAULT_PARAM, or SQL_NULL_DATA.|  
|HY010|Function sequence error|(DM) The previous function call was not a call to **SQLPutData** or **SQLParamData**.<br /><br /> (DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the SQLPutData function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY019|Non-character and non-binary data sent in pieces|**SQLPutData** was called more than once for a parameter or column, and it was not being used to send character C data to a column with a character, binary, or data source-specific data type or to send binary C data to a column with a character, binary, or data source-specific data type.|  
|HY020|Attempt to concatenate a null value|**SQLPutData** was called more than once since the call that returned SQL_NEED_DATA, and in one of those calls, the *StrLen_or_Ind* argument contained SQL_NULL_DATA or SQL_DEFAULT_PARAM.|  
|HY090|Invalid string or buffer length|The argument *DataPtr* was not a null pointer, and the argument *StrLen_or_Ind* was less than 0 but not equal to SQL_NTS or SQL_NULL_DATA.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
 If **SQLPutData** is called while sending data for a parameter in an SQL statement, it can return any SQLSTATE that can be returned by the function called to execute the statement (**SQLExecute** or **SQLExecDirect**). If it is called while sending data for a column being updated or added with **SQLBulkOperations** or being updated with **SQLSetPos**, it can return any SQLSTATE that can be returned by **SQLBulkOperations** or **SQLSetPos**.  
  
## Comments  
 **SQLPutData** can be called to supply data-at-execution data for two uses: parameter data to be used in a call to **SQLExecute** or **SQLExecDirect**, or column data to be used when a row is updated or added by a call to **SQLBulkOperations** or is updated by a call to **SQLSetPos**.  
  
 When an application calls **SQLParamData** to determine which data it should send, the driver returns an indicator that the application can use to determine which parameter data to send or where column data can be found. It also returns SQL_NEED_DATA, which is an indicator to the application that it should call **SQLPutData** to send the data. In the *DataPtr* argument to **SQLPutData**, the application passes a pointer to the buffer containing the actual data for the parameter or column.  
  
 When the driver returns SQL_SUCCESS for **SQLPutData**, the application calls **SQLParamData** again. **SQLParamData** returns SQL_NEED_DATA if more data needs to be sent, in which case the application calls **SQLPutData** again. It returns SQL_SUCCESS if all data-at-execution data has been sent. The application then calls **SQLParamData** again. If the driver returns SQL_NEED_DATA and another indicator in *\*ValuePtrPtr*, it requires data for another parameter or column and **SQLPutData** is called again. If the driver returns SQL_SUCCESS, then all data-at-execution data has been sent and the SQL statement can be executed or the **SQLBulkOperations** or **SQLSetPos** call can be processed.  
  
 For more information on how data-at-execution parameter data is passed at statement execution time, see "Passing Parameter Values" in [SQLBindParameter](../../../odbc/reference/syntax/sqlbindparameter-function.md) and [Sending Long Data](../../../odbc/reference/develop-app/sending-long-data.md). For more information on how data-at-execution column data is updated or added, see the section "Using SQLSetPos" in [SQLSetPos](../../../odbc/reference/syntax/sqlsetpos-function.md), "Performing Bulk Updates Using Bookmarks" in [SQLBulkOperations](../../../odbc/reference/syntax/sqlbulkoperations-function.md), and [Long Data and SQLSetPos and SQLBulkOperations](../../../odbc/reference/develop-app/long-data-and-sqlsetpos-and-sqlbulkoperations.md).  
  
> [!NOTE]  
>  An application can use **SQLPutData** to send data in parts only when sending character C data to a column with a character, binary, or data source-specific data type or when sending binary C data to a column with a character, binary, or data source-specific data type. If **SQLPutData** is called more than once under any other conditions, it returns SQL_ERROR and SQLSTATE HY019 (Non-character and non-binary data sent in pieces).  
  
## Example  
 The following sample assumes a data source name called Test. The associated database should have a table that you can create, as follows:  
  
```sql  
CREATE TABLE emp4 (NAME char(30), AGE int, BIRTHDAY datetime, Memo1 text)  
```  
  
```cpp  
// SQLPutData.cpp  
// compile with: odbc32.lib user32.lib  
#include <stdio.h>  
#include <windows.h>  
#include <sqlext.h>  
#include <odbcss.h>  
  
#define TEXTSIZE  12000  
#define MAXBUFLEN 256  
  
SQLHENV henv = SQL_NULL_HENV;  
SQLHDBC hdbc1 = SQL_NULL_HDBC;       
SQLHSTMT hstmt1 = SQL_NULL_HSTMT;  
  
void Cleanup() {  
   if (hstmt1 != SQL_NULL_HSTMT)  
      SQLFreeHandle(SQL_HANDLE_STMT, hstmt1);  
  
   if (hdbc1 != SQL_NULL_HDBC) {  
      SQLDisconnect(hdbc1);  
      SQLFreeHandle(SQL_HANDLE_DBC, hdbc1);  
   }  
  
   if (henv != SQL_NULL_HENV)  
      SQLFreeHandle(SQL_HANDLE_ENV, henv);  
}  
  
int main() {  
   RETCODE retcode;  
  
   // SQLBindParameter variables.  
   SQLLEN cbTextSize, lbytes;  
  
   // SQLParamData variable.  
   PTR pParmID;  
  
   // SQLPutData variables.  
   UCHAR  Data[] =   
      "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz"  
      "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz"  
      "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz"  
      "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz"  
      "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz"  
      "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz"  
      "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz"  
      "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz"  
      "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz"  
      "abcdefghijklmnopqrstuvwxyz";  
  
   SDWORD cbBatch = (SDWORD)sizeof(Data) - 1;  
  
   // Allocate the ODBC environment and save handle.  
   retcode = SQLAllocHandle (SQL_HANDLE_ENV, NULL, &henv);  
   if ( (retcode != SQL_SUCCESS_WITH_INFO) && (retcode != SQL_SUCCESS)) {  
      printf("SQLAllocHandle(Env) Failed\n\n");  
      Cleanup();  
      return(9);  
   }  
  
   // Notify ODBC that this is an ODBC 3.0 app.  
   retcode = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER) SQL_OV_ODBC3, SQL_IS_INTEGER);  
   if ( (retcode != SQL_SUCCESS_WITH_INFO) && (retcode != SQL_SUCCESS)) {  
      printf("SQLSetEnvAttr(ODBC version) Failed\n\n");  
      Cleanup();  
      return(9);      
   }  
  
   // Allocate ODBC connection handle and connect.  
   retcode = SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc1);  
   if ( (retcode != SQL_SUCCESS_WITH_INFO) && (retcode != SQL_SUCCESS)) {  
      printf("SQLAllocHandle(hdbc1) Failed\n\n");  
      Cleanup();  
      return(9);  
   }  
  
   // Sample uses Integrated Security, create SQL Server DSN using Windows NT authentication.   
   retcode = SQLConnect(hdbc1, (UCHAR*)"Test", SQL_NTS, (UCHAR*)"",SQL_NTS, (UCHAR*)"", SQL_NTS);  
   if ( (retcode != SQL_SUCCESS) && (retcode != SQL_SUCCESS_WITH_INFO) ) {  
      printf("SQLConnect() Failed\n\n");  
      Cleanup();  
      return(9);  
   }  
  
   // Allocate statement handle.  
   retcode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc1, &hstmt1);  
   if ( (retcode != SQL_SUCCESS) && (retcode != SQL_SUCCESS_WITH_INFO) ) {  
      printf("SQLAllocHandle(hstmt1) Failed\n\n");  
      Cleanup();  
      return(9);  
   }  
  
   // Set parameters based on total data to send.  
   lbytes = (SDWORD)TEXTSIZE;  
   cbTextSize = SQL_LEN_DATA_AT_EXEC(lbytes);  
  
   // Bind the parameter marker.  
   retcode = SQLBindParameter (hstmt1,           // hstmt  
                               1,                // ipar  
                               SQL_PARAM_INPUT,  // fParamType  
                               SQL_C_CHAR,       // fCType  
                               SQL_LONGVARCHAR,  // FSqlType  
                               lbytes,           // cbColDef  
                               0,                // ibScale  
                               (VOID *)1,        // rgbValue  
                               0,                // cbValueMax  
                               &cbTextSize);     // pcbValue  
  
   if ( (retcode != SQL_SUCCESS) && (retcode != SQL_SUCCESS_WITH_INFO) ) {  
      printf("SQLBindParameter Failed\n\n");  
      Cleanup();  
      return(9);  
   }  
  
   // Execute the command.  
   retcode =   
      SQLExecDirect(hstmt1, (UCHAR*)"INSERT INTO emp4 VALUES('Paul Borm', 46,'1950-11-12 00:00:00', ?)", SQL_NTS);  
   if ( (retcode != SQL_SUCCESS) && (retcode != SQL_NEED_DATA) && (retcode != SQL_SUCCESS_WITH_INFO) ) {  
      printf("SQLExecDirect Failed\n\n");  
      Cleanup();  
      return(9);  
   }  
  
   // Check to see if NEED_DATA; if yes, use SQLPutData.  
   retcode = SQLParamData(hstmt1, &pParmID);  
   if (retcode == SQL_NEED_DATA) {  
      while (lbytes > cbBatch) {  
         SQLPutData(hstmt1, Data, cbBatch);  
         lbytes -= cbBatch;  
      }  
      // Put final batch.  
      retcode = SQLPutData(hstmt1, Data, lbytes);   
   }  
  
   if ( (retcode != SQL_SUCCESS) && (retcode != SQL_SUCCESS_WITH_INFO) ) {  
      printf("SQLParamData Failed\n\n");  
      Cleanup();  
      return(9);  
   }  
  
   // Make final SQLParamData call.  
   retcode = SQLParamData(hstmt1, &pParmID);  
   if ( (retcode != SQL_SUCCESS) && (retcode != SQL_SUCCESS_WITH_INFO) ) {  
      printf("Final SQLParamData Failed\n\n");  
      Cleanup();  
      return(9);  
   }  
  
   // Clean up.  
   SQLFreeHandle(SQL_HANDLE_STMT, hstmt1);  
   SQLDisconnect(hdbc1);  
   SQLFreeHandle(SQL_HANDLE_DBC, hdbc1);  
   SQLFreeHandle(SQL_HANDLE_ENV, henv);  
}  
```  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a parameter|[SQLBindParameter Function](../../../odbc/reference/syntax/sqlbindparameter-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Executing an SQL statement|[SQLExecDirect Function](../../../odbc/reference/syntax/sqlexecdirect-function.md)|  
|Executing a prepared SQL statement|[SQLExecute Function](../../../odbc/reference/syntax/sqlexecute-function.md)|  
|Returning the next parameter to send data for|[SQLParamData Function](../../../odbc/reference/syntax/sqlparamdata-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
