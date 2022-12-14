---
description: "SQLGetData Function"
title: "SQLGetData Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLGetData"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLGetData"
helpviewer_keywords: 
  - "SQLGetData function [ODBC]"
ms.assetid: e3c1356a-5db7-4186-85fd-8b74633317e8
author: David-Engel
ms.author: v-davidengel
---
# SQLGetData Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLGetData** retrieves data for a single column in the result set or for a single parameter after **SQLParamData** returns SQL_PARAM_DATA_AVAILABLE. It can be called multiple times to retrieve variable-length data in parts.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLGetData(  
      SQLHSTMT       StatementHandle,  
      SQLUSMALLINT   Col_or_Param_Num,  
      SQLSMALLINT    TargetType,  
      SQLPOINTER     TargetValuePtr,  
      SQLLEN         BufferLength,  
      SQLLEN *       StrLen_or_IndPtr);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *Col_or_Param_Num*  
 [Input] For retrieving column data, it is the number of the column for which to return data. Result set columns are numbered in increasing column order starting at 1. The bookmark column is column number 0; this can be specified only if bookmarks are enabled.  
  
 For retrieving parameter data, it is the ordinal of the parameter, which starts at 1.  
  
 *TargetType*  
 [Input] The type identifier of the C data type of the **TargetValuePtr* buffer. For a list of valid C data types and type identifiers, see the [C Data Types](../../../odbc/reference/appendixes/c-data-types.md) section in Appendix D: Data Types.  
  
 If *TargetType* is SQL_ARD_TYPE, the driver uses the type identifier specified in the SQL_DESC_CONCISE_TYPE field of the ARD. If *TargetType* is SQL_APD_TYPE, **SQLGetData** will use the same C data type that was specified in **SQLBindParameter**. Otherwise, the C data type specified in **SQLGetData** overrides the C data type specified in **SQLBindParameter**. If it is SQL_C_DEFAULT, the driver selects the default C data type based on the SQL data type of the source.  
  
 You can also specify an extended C data type. For more information, see [C Data Types in ODBC](../../../odbc/reference/develop-app/c-data-types-in-odbc.md).  
  
 *TargetValuePtr*  
 [Output] Pointer to the buffer in which to return the data.  
  
 *TargetValuePtr* cannot be NULL.  
  
 *BufferLength*  
 [Input] Length of the **TargetValuePtr* buffer in bytes.  
  
 The driver uses *BufferLength* to avoid writing past the end of the \**TargetValuePtr* buffer when returning variable-length data, such as character or binary data. Note that the driver counts the null-termination character when returning character data to \**TargetValuePtr*. **TargetValuePtr* must therefore contain space for the null-termination character, or the driver will truncate the data.  
  
 When the driver returns fixed-length data, such as an integer or a date structure, the driver ignores *BufferLength* and assumes the buffer is large enough to hold the data. It is therefore important for the application to allocate a large enough buffer for fixed-length data or the driver will write past the end of the buffer.  
  
 **SQLGetData** returns SQLSTATE HY090 (Invalid string or buffer length) when *BufferLength* is less than 0 but not when *BufferLength* is 0.  
  
 *StrLen_or_IndPtr*  
 [Output] Pointer to the buffer in which to return the length or indicator value. If this is a null pointer, no length or indicator value is returned. This returns an error when the data being fetched is NULL.  
  
 **SQLGetData** can return the following values in the length/indicator buffer:  
  
-   The length of the data available to return  
  
-   SQL_NO_TOTAL  
  
-   SQL_NULL_DATA  
  
 For more information, see [Using Length/Indicator Values](../../../odbc/reference/develop-app/using-length-and-indicator-values.md) and "Comments" in this topic.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NO_DATA, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLGetData** returns either SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLGetData** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|Not all of the data for the specified column, *Col_or_Param_Num*, could be retrieved in a single call to the function. SQL_NO_TOTAL or the length of the data remaining in the specified column prior to the current call to **SQLGetData** is returned in \**StrLen_or_IndPtr*. (Function returns SQL_SUCCESS_WITH_INFO.)<br /><br /> For more information on using multiple calls to **SQLGetData** for a single column, see "Comments."|  
|01S07|Fractional truncation|The data returned for one or more columns was truncated. For numeric data types, the fractional part of the number was truncated. For time, timestamp, and interval data types containing a time component, the fractional portion of the time was truncated.<br /><br /> (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07006|Restricted data type attribute violation|The data value of a column in the result set cannot be converted to the C data type specified by the argument *TargetType*.|  
|07009|Invalid descriptor index|The value specified for the argument *Col_or_Param_Num* was 0, and the SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_OFF.<br /><br /> The value specified for the argument *Col_or_Param_Num* was greater than the number of columns in the result set.<br /><br /> The *Col_or_Param_Num* value was not equal to the ordinal of the parameter that is available.<br /><br /> (DM) The specified column was bound. This description does not apply to drivers that return the SQL_GD_BOUND bitmask for the SQL_GETDATA_EXTENSIONS option in **SQLGetInfo**.<br /><br /> (DM) The number of the specified column was less than or equal to the number of the highest bound column. This description does not apply to drivers that return the SQL_GD_ANY_COLUMN bitmask for the SQL_GETDATA_EXTENSIONS option in **SQLGetInfo**.<br /><br /> (DM) The application has already called **SQLGetData** for the current row; the number of the column specified in the current call was less than the number of the column specified in the preceding call; and the driver does not return the SQL_GD_ANY_ORDER bitmask for the SQL_GETDATA_EXTENSIONS option in **SQLGetInfo**.<br /><br /> (DM) The *TargetType* argument was SQL_ARD_TYPE, and the *Col_or_Param_Num* descriptor record in the ARD failed the consistency check.<br /><br /> (DM) The *TargetType* argument was SQL_ARD_TYPE, and the value in the SQL_DESC_COUNT field of the ARD was less than the *Col_or_Param_Num* argument.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|22002|Indicator variable required but not supplied|*StrLen_or_IndPtr* was a null pointer and NULL data was retrieved.|  
|22003|Numeric value out of range|Returning the numeric value (as numeric or string) for the column would have caused the whole (as opposed to fractional) part of the number to be truncated.<br /><br /> For more information, see [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md).|  
|22007|Invalid datetime format|The character column in the result set was bound to a C date, time, or timestamp structure, and the value in the column was an invalid date, time, or timestamp, respectively. For more information, see [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md).|  
|22012|Division by zero|A value from an arithmetic expression that resulted in division by zero was returned.|  
|22015|Interval field overflow|Assigning from an exact numeric or interval SQL type to an interval C type caused a loss of significant digits in the leading field.<br /><br /> When returning data to an interval C type, there was no representation of the value of the SQL type in the interval C type.|  
|22018|Invalid character value for cast specification|A character column in the result set was returned to a character C buffer, and the column contained a character for which there was no representation in the character set of the buffer.<br /><br /> The C type was an exact or approximate numeric, a datetime, or an interval data type; the SQL type of the column was a character data type; and the value in the column was not a valid literal of the bound C type.|  
|24000|Invalid cursor state|(DM) The function was called without first calling **SQLFetch** or **SQLFetchScroll** to position the cursor on the row of data required.<br /><br /> (DM) The *StatementHandle* was in an executed state, but no result set was associated with the *StatementHandle*.<br /><br /> A cursor was open on the *StatementHandle* and **SQLFetch** or **SQLFetchScroll** had been called, but the cursor was positioned before the start of the result set or after the end of the result set.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY003|Program type out of range|(DM) The argument *TargetType* was not a valid data type, SQL_C_DEFAULT, SQL_ARD_TYPE (in case of retrieving column data), or SQL_APD_TYPE (in case of retrieving parameter data).<br /><br /> (DM) The argument *Col_or_Param_Num* was 0, and the argument *TargetType* was not SQL_C_BOOKMARK for a fixed-length bookmark or SQL_C_VARBOOKMARK for a variable-length bookmark.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*, and then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application, and then the function was called again on the *StatementHandle*.|  
|HY009|Invalid use of null pointer|(DM) The argument *TargetValuePtr* was a null pointer.|  
|HY010|Function sequence error|(DM) The specified *StatementHandle* was not in an executed state. The function was called without first calling **SQLExecDirect**, **SQLExecute** or a catalog function.<br /><br /> (DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the **SQLGetData** function was called.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.<br /><br /> (DM) The *StatementHandle* was in an executed state, but no result set was associated with the *StatementHandle*.<br /><br /> A call to **SQLExeceute**, **SQLExecDirect**, or **SQLMoreResults** returned SQL_PARAM_DATA_AVAILABLE, but **SQLGetData** was called, instead of **SQLParamData**.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The value specified for argument *BufferLength* was less than 0.<br /><br /> The value specified for argument *BufferLength* was less than 4, the *Col_or_Param_Num* argument was set to 0, and the driver was an ODBC 2*.x* driver.|  
|HY109|Invalid cursor position|The cursor was positioned (by **SQLSetPos**, **SQLFetch**, **SQLFetchScroll**, or **SQLBulkOperations**) on a row that had been deleted or could not be fetched.<br /><br /> The cursor was a forward-only cursor, and the rowset size was greater than one.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The driver or data source does not support use of **SQLGetData** with multiple rows in **SQLFetchScroll**. This description does not apply to drivers that return the SQL_GD_BLOCK bitmask for the SQL_GETDATA_EXTENSIONS option in **SQLGetInfo**.<br /><br /> The driver or data source does not support the conversion specified by the combination of the *TargetType* argument and the SQL data type of the corresponding column. This error applies only when the SQL data type of the column was mapped to a driver-specific SQL data type.<br /><br /> The driver supports only ODBC 2*.x*, and the argument *TargetType* was one of the following:<br /><br /> SQL_C_NUMERIC SQL_C_SBIGINT SQL_C_UBIGINT<br /><br /> and any of the interval C data types listed in [C Data Types](../../../odbc/reference/appendixes/c-data-types.md) in Appendix D: Data Types.<br /><br /> The driver only supports ODBC versions prior to 3.50, and the argument *TargetType* was SQL_C_GUID.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver corresponding to the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
 **SQLGetData** returns the data in a specified column. **SQLGetData** can be called only after one or more rows have been fetched from the result set by **SQLFetch**, **SQLFetchScroll**, or **SQLExtendedFetch**. If variable-length data is too large to be returned in a single call to **SQLGetData** (due to a limitation in the application), **SQLGetData** can retrieve it in parts. It is possible to bind some columns in a row and call **SQLGetData** for others, although this is subject to some restrictions. For more information, see [Getting Long Data](../../../odbc/reference/develop-app/getting-long-data.md).  
  
 For information about using **SQLGetData** with streamed output parameters, see [Retrieving Output Parameters Using SQLGetData](../../../odbc/reference/develop-app/retrieving-output-parameters-using-sqlgetdata.md).  
  
## Using SQLGetData  
 If the driver does not support extensions to **SQLGetData**, the function can return data only for unbound columns with a number greater than that of the last bound column. Furthermore, within a row of data, the value of the *Col_or_Param_Num* argument in each call to **SQLGetData** must be greater than or equal to the value of *Col_or_Param_Num* in the previous call; that is, data must be retrieved in increasing column number order. Finally, if no extensions are supported, **SQLGetData** cannot be called if the rowset size is greater than 1.  
  
 Drivers can relax any of these restrictions. To determine what restrictions a driver relaxes, an application calls **SQLGetInfo** with any of the following SQL_GETDATA_EXTENSIONS options:  
  
-   SQL_GD_OUTPUT_PARAMS = **SQLGetData** can be called to return output parameter values. For more information, see [Retrieving Output Parameters Using SQLGetData](../../../odbc/reference/develop-app/retrieving-output-parameters-using-sqlgetdata.md).  
  
-   SQL_GD_ANY_COLUMN. If this option is returned, **SQLGetData** can be called for any unbound column, including those before the last bound column.  
  
-   SQL_GD_ANY_ORDER. If this option is returned, **SQLGetData** can be called for unbound columns in any order.  
  
-   SQL_GD_BLOCK. If this option is returned by **SQLGetInfo** for the SQL_GETDATA_EXTENSIONS InfoType, the driver supports calls to **SQLGetData** when the rowset size is greater than 1 and the application can call **SQLSetPos** with the SQL_POSITION option to position the cursor on the correct row before calling **SQLGetData.**  
  
-   SQL_GD_BOUND. If this option is returned, **SQLGetData** can be called for bound columns as well as unbound columns.  
  
 There are two exceptions to these restrictions and a driver's ability to relax them. First, **SQLGetData** should never be called for a forward-only cursor when the rowset size is greater than 1. Second, if a driver supports bookmarks, it must always support the ability to call **SQLGetData** for column 0, even if it does not allow applications to call **SQLGetData** for other columns before the last bound column. (When an application is working with an ODBC 2*.x* driver, **SQLGetData** will successfully return a bookmark when called with *Col_or_Param_Num* equal to 0 after a call to **SQLFetch**, because **SQLFetch** is mapped by the ODBC 3*.x* Driver Manager to **SQLExtendedFetch** with a *FetchOrientation* of SQL_FETCH_NEXT, and **SQLGetData** with a *Col_or_Param_Num* of 0 is mapped by the ODBC 3*.x* Driver Manager to **SQLGetStmtOption** with an *fOption* of SQL_GET_BOOKMARK.)  
  
 **SQLGetData** cannot be used to retrieve the bookmark for a row just inserted by calling **SQLBulkOperations** with the SQL_ADD option, because the cursor is not positioned on the row. An application can retrieve the bookmark for such a row by binding column 0 before calling **SQLBulkOperations** with SQL_ADD, in which case **SQLBulkOperations** returns the bookmark in the bound buffer. **SQLFetchScroll** can then be called with SQL_FETCH_BOOKMARK to reposition the cursor on that row.  
  
 If the *TargetType* argument is an interval data type, the default interval leading precision (2) and the default interval seconds precision (6), as set in the SQL_DESC_DATETIME_INTERVAL_PRECISION and SQL_DESC_PRECISION fields of the ARD, respectively, are used for the data. If the *TargetType* argument is an SQL_C_NUMERIC data type, the default precision (driver-defined) and default scale (0), as set in the SQL_DESC_PRECISION and SQL_DESC_SCALE fields of the ARD, are used for the data. If any default precision or scale is not appropriate, the application should explicitly set the appropriate descriptor field by a call to **SQLSetDescField** or **SQLSetDescRec**. It can set the SQL_DESC_CONCISE_TYPE field to SQL_C_NUMERIC and call **SQLGetData** with a *TargetType* argument of SQL_ARD_TYPE, which will cause the precision and scale values in the descriptor fields to be used.  
  
> [!NOTE]
>  In ODBC 2*.x*, applications set *TargetType* to SQL_C_DATE, SQL_C_TIME, or SQL_C_TIMESTAMP to indicate that \**TargetValuePtr* is a date, time, or timestamp structure. In ODBC 3*.x*, applications set *TargetType* to SQL_C_TYPE_DATE, SQL_C_TYPE_TIME, or SQL_C_TYPE_TIMESTAMP. The Driver Manager makes appropriate mappings if necessary, based on the application and driver version.  
  
## Retrieving Variable-Length Data in Parts  
 **SQLGetData** can be used to retrieve data from a column that contains variable-length data in parts - that is, when the identifier of the SQL data type of the column is SQL_CHAR, SQL_VARCHAR, SQL_LONGVARCHAR, SQL_WCHAR, SQL_WVARCHAR, SQL_WLONGVARCHAR, SQL_BINARY, SQL_VARBINARY, SQL_LONGVARBINARY, or a driver-specific identifier for a variable-length type.  
  
 To retrieve data from a column in parts, the application calls **SQLGetData** multiple times in succession for the same column. On each call, **SQLGetData** returns the next part of the data. It is up to the application to reassemble the parts, taking care to remove the null-termination character from intermediate parts of character data. If there is more data to return or not enough buffer was allocated for the terminating character, **SQLGetData** returns SQL_SUCCESS_WITH_INFO and SQLSTATE 01004 (Data truncated). When it returns the last part of the data, **SQLGetData** returns SQL_SUCCESS. Neither SQL_NO_TOTAL nor zero can be returned on the last valid call to retrieve data from a column, because the application would then have no way of knowing how much of the data in the application buffer is valid. If **SQLGetData** is called after this, it returns SQL_NO_DATA. For more information, see the next section, "Retrieving Data with SQLGetData."  
  
 Variable-length bookmarks can be returned in parts by **SQLGetData**. As with other data, a call to **SQLGetData** to return variable-length bookmarks in parts will return SQLSTATE 01004 (String data, right truncated) and SQL_SUCCESS_WITH_INFO when there is more data to be returned. This is different than the case when a variable-length bookmark is truncated by a call to **SQLFetch** or **SQLFetchScroll**, which returns SQL_ERROR and SQLSTATE 22001 (String data, right truncated).  
  
 **SQLGetData** cannot be used to return fixed-length data in parts. If **SQLGetData** is called more than one time in a row for a column containing fixed-length data, it returns SQL_NO_DATA for all calls after the first.  
  
## Retrieving Streamed Output Parameters  
 If a driver supports streamed output parameters, an application can call **SQLGetData** with a small buffer many times to retrieve a large parameter value. For more information about streamed output parameter, see [Retrieving Output Parameters Using SQLGetData](../../../odbc/reference/develop-app/retrieving-output-parameters-using-sqlgetdata.md).  
  
## Retrieving Data with SQLGetData  
 To return data for the specified column, **SQLGetData** performs the following sequence of steps:  
  
1.  Returns SQL_NO_DATA if it has already returned all of the data for the column.  
  
2.  Sets \**StrLen_or_IndPtr* to SQL_NULL_DATA if the data is NULL. If the data is NULL and *StrLen_or_IndPtr* was a null pointer, **SQLGetData** returns SQLSTATE 22002 (Indicator variable required but not supplied).  
  
     If the data for the column is not NULL, **SQLGetData** proceeds to step 3.  
  
3.  If the SQL_ATTR_MAX_LENGTH statement attribute is set to a nonzero value, if the column contains character or binary data, and if **SQLGetData** has not previously been called for the column, the data is truncated to SQL_ATTR_MAX_LENGTH bytes.  
  
    > [!NOTE]  
    >  The SQL_ATTR_MAX_LENGTH statement attribute is intended to reduce network traffic. It is generally implemented by the data source, which truncates the data before returning it across the network. Drivers and data sources are not required to support it. Therefore, to guarantee that data is truncated to a particular size, an application should allocate a buffer of that size and specify the size in the *BufferLength* argument.  
  
4.  Converts the data to the type specified in *TargetType*. The data is given the default precision and scale for that data type. If *TargetType* is SQL_ARD_TYPE, the data type in the SQL_DESC_CONCISE_TYPE field of the ARD is used. If *TargetType* is SQL_ARD_TYPE, the data is given the precision and scale in the SQL_DESC_DATETIME_INTERVAL_PRECISION, SQL_DESC_PRECISION, and SQL_DESC_SCALE fields of the ARD, depending on the data type in the SQL_DESC_CONCISE_TYPE field. If any default precision or scale is not appropriate, the application should explicitly set the appropriate descriptor field by a call to **SQLSetDescField** or **SQLSetDescRec**.  
  
5.  If the data was converted to a variable-length data type, such as character or binary, **SQLGetData** checks whether the length of the data exceeds *BufferLength*. If the length of character data (including the null-termination character) exceeds *BufferLength*, **SQLGetData** truncates the data to *BufferLength* less the length of a null-termination character. It then null-terminates the data. If the length of binary data exceeds the length of the data buffer, **SQLGetData** truncates it to *BufferLength* bytes.  
  
     If the data buffer supplied is too small to hold the null-termination character, **SQLGetData** returns SQL_SUCCESS_WITH_INFO and SQLSTATE 01004.  
  
     **SQLGetData** never truncates data converted to fixed-length data types; it always assumes that the length of **TargetValuePtr* is the size of the data type.  
  
6.  Places the converted (and possibly truncated) data in \**TargetValuePtr*. Note that **SQLGetData** cannot return data out of line.  
  
7.  Places the length of the data in \**StrLen_or_IndPtr*. If *StrLen_or_IndPtr* was a null pointer, **SQLGetData** does not return the length.  
  
    -   For character or binary data, this is the length of the data after conversion and before truncation due to *BufferLength*. If the driver cannot determine the length of the data after conversion, as is sometimes the case with long data, it returns SQL_SUCCESS_WITH_INFO and sets the length to SQL_NO_TOTAL. (The last call to **SQLGetData** must always return the length of the data, not zero or SQL_NO_TOTAL.) If data was truncated due to the SQL_ATTR_MAX_LENGTH statement attribute, the value of this attribute - as opposed to the actual length - is placed in \**StrLen_or_IndPtr*. This is because this attribute is designed to truncate data on the server before conversion, so the driver has no way of figuring out what the actual length is. When **SQLGetData** is called multiple times in succession for the same column, this is the length of the data available at the start of the current call; that is, the length decreases with each subsequent call.  
  
    -   For all other data types, this is the length of the data after conversion; that is, it is the size of the type to which the data was converted.  
  
8.  If the data is truncated without loss of significance during conversion (for example, the real number 1.234 is truncated when converted to the integer 1) or because *BufferLength* is too small (for example, the string "abcdef" is placed in a 4-byte buffer), **SQLGetData** returns SQLSTATE 01004 (Data truncated) and SQL_SUCCESS_WITH_INFO. If data is truncated without loss of significance due to the SQL_ATTR_MAX_LENGTH statement attribute, **SQLGetData** returns SQL_SUCCESS and does not return SQLSTATE 01004 (Data truncated).  
  
 The contents of the bound data buffer (if **SQLGetData** is called on a bound column) and the length/indicator buffer are undefined if **SQLGetData** does not return SQL_SUCCESS or SQL_SUCCESS_WITH_INFO.  
  
 Successive calls to **SQLGetData** will retrieve data from the last column requested; prior offsets become invalid. For example, when the following sequence is performed:  
  
```cpp  
SQLGetData(icol=n), SQLGetData(icol=m), SQLGetData(icol=n)  
```  
  
 the second call to SQLGetData(icol=n) retrieves data from the start of the n column. Any offset in the data due to earlier calls to **SQLGetData** for the column is no longer valid.  
  
## Descriptors and SQLGetData  
 **SQLGetData** does not interact directly with any descriptor fields.  
  
 If *TargetType* is SQL_ARD_TYPE, the data type in the SQL_DESC_CONCISE_TYPE field of the ARD is used. If *TargetType* is either SQL_ARD_TYPE or SQL_C_DEFAULT, the data is given the precision and scale in the SQL_DESC_DATETIME_INTERVAL_PRECISION, SQL_DESC_PRECISION, and SQL_DESC_SCALE fields of the ARD, depending on the data type in the SQL_DESC_CONCISE_TYPE field.  
  
## Code Example  
 In the following example, an application executes a **SELECT** statement to return a result set of the customer IDs, names, and phone numbers sorted by name, ID, and phone number. For each row of data, it calls **SQLFetch** to position the cursor to the next row. It calls **SQLGetData** to retrieve the fetched data; the buffers for the data and the returned number of bytes are specified in the call to **SQLGetData**. Finally, it prints each employee's name, ID, and phone number.  
  
```cpp  
#define NAME_LEN 50  
#define PHONE_LEN 50  
  
SQLCHAR      szName[NAME_LEN], szPhone[PHONE_LEN];  
SQLINTEGER   sCustID, cbName, cbAge, cbBirthday;  
SQLRETURN    retcode;  
SQLHSTMT     hstmt;  
  
retcode = SQLExecDirect(hstmt,  
   "SELECT CUSTID, NAME, PHONE FROM CUSTOMERS ORDER BY 2, 1, 3",  
   SQL_NTS);  
  
if (retcode == SQL_SUCCESS) {  
   while (TRUE) {  
      retcode = SQLFetch(hstmt);  
      if (retcode == SQL_ERROR || retcode == SQL_SUCCESS_WITH_INFO) {  
         show_error();  
      }  
      if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO){  
  
         /* Get data for columns 1, 2, and 3 */  
  
         SQLGetData(hstmt, 1, SQL_C_ULONG, &sCustID, 0, &cbCustID);  
         SQLGetData(hstmt, 2, SQL_C_CHAR, szName, NAME_LEN, &cbName);  
         SQLGetData(hstmt, 3, SQL_C_CHAR, szPhone, PHONE_LEN,  
            &cbPhone);  
  
         /* Print the row of data */  
  
         fprintf(out, "%-5d %-*s %*s", sCustID, NAME_LEN-1, szName,   
            PHONE_LEN-1, szPhone);  
      } else {  
         break;  
      }  
   }  
}  
```  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Assigning storage for a column in a result set|[SQLBindCol](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Performing bulk operations that do not relate to the block cursor position|[SQLBulkOperations](../../../odbc/reference/syntax/sqlbulkoperations-function.md)|  
|Canceling statement processing|[SQLCancel](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Executing an SQL statement|[SQLExecDirect](../../../odbc/reference/syntax/sqlexecdirect-function.md)|  
|Executing a prepared SQL statement|[SQLExecute](../../../odbc/reference/syntax/sqlexecute-function.md)|  
|Fetching a block of data or scrolling through a result set|[SQLFetchScroll](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|  
|Fetching a single row of data or a block of data in a forward-only direction|[SQLFetch](../../../odbc/reference/syntax/sqlfetch-function.md)|  
|Sending parameter data at execution time|[SQLPutData](../../../odbc/reference/syntax/sqlputdata-function.md)|  
|Positioning the cursor, refreshing data in the rowset, or updating or deleting data in the rowset|[SQLSetPos](../../../odbc/reference/syntax/sqlsetpos-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)   
 [Retrieving Output Parameters Using SQLGetData](../../../odbc/reference/develop-app/retrieving-output-parameters-using-sqlgetdata.md)
