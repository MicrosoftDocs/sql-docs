---
title: "SQLBindCol Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLBindCol"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLBindCol"
helpviewer_keywords: 
  - "SQLBindCol function [ODBC]"
ms.assetid: 41a37655-84cd-423f-9daa-e0b47b88dc54
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLBindCol Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLBindCol** binds application data buffers to columns in the result set.  
  
## Syntax  
  
```  
  
SQLRETURN SQLBindCol(  
      SQLHSTMT       StatementHandle,  
      SQLUSMALLINT   ColumnNumber,  
      SQLSMALLINT    TargetType,  
      SQLPOINTER     TargetValuePtr,  
      SQLLEN         BufferLength,  
      SQLLEN *       StrLen_or_Ind);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *ColumnNumber*  
 [Input] Number of the result set column to bind. Columns are numbered in increasing column order starting at 0, where column 0 is the bookmark column. If bookmarks are not used - that is, the SQL_ATTR_USE_BOOKMARKS statement attribute is set to SQL_UB_OFF - then column numbers start at 1.  
  
 *TargetType*  
 [Input] The identifier of the C data type of the \**TargetValuePtr* buffer. When it is retrieving data from the data source with **SQLFetch**, **SQLFetchScroll**, **SQLBulkOperations**, or **SQLSetPos**, the driver converts the data to this type; when it sends data to the data source with **SQLBulkOperations** or **SQLSetPos**, the driver converts the data from this type. For a list of valid C data types and type identifiers, see the [C Data Types](../../../odbc/reference/appendixes/c-data-types.md) section in Appendix D: Data Types.  
  
 If the *TargetType* argument is an interval data type, the default interval leading precision (2) and the default interval seconds precision (6), as set in the SQL_DESC_DATETIME_INTERVAL_PRECISION and SQL_DESC_PRECISION fields of the ARD, respectively, are used for the data. If the *TargetType* argument is SQL_C_NUMERIC, the default precision (driver-defined) and default scale (0), as set in the SQL_DESC_PRECISION and SQL_DESC_SCALE fields of the ARD, are used for the data. If any default precision or scale is not appropriate, the application should explicitly set the appropriate descriptor field by a call to **SQLSetDescField** or **SQLSetDescRec**.  
  
 You can also specify an extended C data type. For more information, see [C Data Types in ODBC](../../../odbc/reference/develop-app/c-data-types-in-odbc.md).  
  
 *TargetValuePtr*  
 [Deferred Input/Output] Pointer to the data buffer to bind to the column. **SQLFetch** and **SQLFetchScroll** return data in this buffer. **SQLBulkOperations** returns data in this buffer when *Operation* is SQL_FETCH_BY_BOOKMARK; it retrieves data from this buffer when *Operation* is SQL_ADD or SQL_UPDATE_BY_BOOKMARK. **SQLSetPos** returns data in this buffer when *Operation* is SQL_REFRESH; it retrieves data from this buffer when *Operation* is SQL_UPDATE.  
  
 If *TargetValuePtr* is a null pointer, the driver unbinds the data buffer for the column. An application can unbind all columns by calling **SQLFreeStmt** with the SQL_UNBIND option. An application can unbind the data buffer for a column but still have a length/indicator buffer bound for the column, if the *TargetValuePtr* argument in the call to **SQLBindCol** is a null pointer but the *StrLen_or_IndPtr* argument is a valid value.  
  
 *BufferLength*  
 [Input] Length of the **TargetValuePtr* buffer in bytes.  
  
 The driver uses *BufferLength* to avoid writing past the end of the \**TargetValuePtr* buffer when it returns variable-length data, such as character or binary data. Notice that the driver counts the null-termination character when it returns character data to \**TargetValuePtr*. **TargetValuePtr* must therefore contain space for the null-termination character or the driver will truncate the data.  
  
 When the driver returns fixed-length data, such as an integer or a date structure, the driver ignores *BufferLength* and assumes the buffer is large enough to hold the data. Therefore, it is important for the application to allocate a large enough buffer for fixed-length data or the driver will write past the end of the buffer.  
  
 **SQLBindCol** returns SQLSTATE HY090 (Invalid string or buffer length) when *BufferLength* is less than 0 but not when *BufferLength* is 0. However, if *TargetType* specifies a character type, an application should not set *BufferLength* to 0, because ISO CLI-compliant drivers return SQLSTATE HY090 (Invalid string or buffer length) in that case.  
  
 *StrLen_or_IndPtr*  
 [Deferred Input/Output] Pointer to the length/indicator buffer to bind to the column. **SQLFetch** and **SQLFetchScroll** return a value in this buffer. **SQLBulkOperations** retrieves a value from this buffer when *Operation* is SQL_ADD, SQL_UPDATE_BY_BOOKMARK, or SQL_DELETE_BY_BOOKMARK. **SQLBulkOperations** returns a value in this buffer when *Operation* is SQL_FETCH_BY_BOOKMARK. **SQLSetPos** returns a value in this buffer when *Operation* is SQL_REFRESH; it retrieves a value from this buffer when *Operation* is SQL_UPDATE.  
  
 **SQLFetch**, **SQLFetchScroll**, **SQLBulkOperations**, and **SQLSetPos** can return the following values in the length/indicator buffer:  
  
-   The length of the data available to return  
  
-   SQL_NO_TOTAL  
  
-   SQL_NULL_DATA  
  
 The application can put the following values in the length/indicator buffer for use with **SQLBulkOperations** or **SQLSetPos**:  
  
-   The length of the data being sent  
  
-   SQL_NTS  
  
-   SQL_NULL_DATA  
  
-   SQL_DATA_AT_EXEC  
  
-   The result of the SQL_LEN_DATA_AT_EXEC macro  
  
-   SQL_COLUMN_IGNORE  
  
 If the indicator buffer and the length buffer are separate buffers, the indicator buffer can return only SQL_NULL_DATA, whereas the length buffer can return all other values.  
  
 For more information, see [SQLBulkOperations Function](../../../odbc/reference/syntax/sqlbulkoperations-function.md), [SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md), [SQLSetPos Function](../../../odbc/reference/syntax/sqlsetpos-function.md), and [Using Length/Indicator Values](../../../odbc/reference/develop-app/using-length-and-indicator-values.md).  
  
 If *StrLen_or_IndPtr* is a null pointer, no length or indicator value is used. This is an error when fetching data and the data is NULL.  
  
 See [ODBC 64-Bit Information](../../../odbc/reference/odbc-64-bit-information.md), if your application will run on a 64-bit operating system.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLBindCol** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values typically returned by **SQLBindCol** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07006|Restricted data type attribute violation|(DM) The *ColumnNumber* argument was 0, and the *TargetType* argument was not SQL_C_BOOKMARK or SQL_C_VARBOOKMARK.|  
|07009|Invalid descriptor index|The value specified for the argument *ColumnNumber* exceeded the maximum number of columns in the result set.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory that is required to support execution or completion of the function.|  
|HY003|Invalid application buffer type|The argument *TargetType* was neither a valid data type nor SQL_C_DEFAULT.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when **SQLBindCol** was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) An asynchronously executing function was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The value specified for the argument *BufferLength* was less than 0.<br /><br /> (DM) The driver was an ODBC 2.*x* driver, the *ColumnNumber* argument was set to 0, and the value specified for the argument *BufferLength* was not equal to 4.|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The driver or data source does not support the conversion specified by the combination of the *TargetType* argument and the driver-specific SQL data type of the corresponding column.<br /><br /> The argument *ColumnNumber* was 0 and the driver does not support bookmarks.<br /><br /> The driver supports only ODBC 2.*x* and the argument *TargetType* was one of the following:<br /><br /> SQL_C_NUMERIC SQL_C_SBIGINT SQL_C_UBIGINT<br /><br /> and any of the interval C data types listed in [C Data Types](../../../odbc/reference/appendixes/c-data-types.md) in Appendix D: Data Types.<br /><br /> The driver only supports ODBC versions prior to 3.50, and the argument *TargetType* was SQL_C_GUID.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
  
## Comments  
 **SQLBindCol** is used to associate, or *bind,* columns in the result set to data buffers and length/indicator buffers in the application. When the application calls **SQLFetch**, **SQLFetchScroll**, or **SQLSetPos** to fetch data, the driver returns the data for the bound columns in the specified buffers; for more information, see [SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md). When the application calls **SQLBulkOperations** to update or insert a row or **SQLSetPos** to update a row, the driver retrieves the data for the bound columns from the specified buffers; for more information, see [SQLBulkOperations Function](../../../odbc/reference/syntax/sqlbulkoperations-function.md) or [SQLSetPos Function](../../../odbc/reference/syntax/sqlsetpos-function.md). For more information about binding, see [Retrieving Results (Basic)](../../../odbc/reference/develop-app/retrieving-results-basic.md).  
  
 Notice that columns do not have to be bound to retrieve data from them. An application can also call **SQLGetData** to retrieve data from columns. Although it is possible to bind some columns in a row and call **SQLGetData** for others, this is subject to some restrictions. For more information, see [SQLGetData](../../../odbc/reference/syntax/sqlgetdata-function.md).  
  
## Binding, Unbinding, and Rebinding Columns  
 A column can be bound, unbound, or rebound at any time, even after data has been fetched from the result set. The new binding takes effect the next time that a function that uses bindings is called. For example, suppose an application binds the columns in a result set and calls **SQLFetch**. The driver returns the data in the bound buffers. Now suppose the application binds the columns to a different set of buffers. The driver does not put the data for the just-fetched row in the newly bound buffers. Instead, it waits until **SQLFetch** is called again and then places the data for the next row in the newly bound buffers.  
  
> [!NOTE]  
>  The statement attribute SQL_ATTR_USE_BOOKMARKS should always be set before binding a column to column 0. This is not required but is strongly recommended.  
  
## Binding Columns  
 To bind a column, an application calls **SQLBindCol** and passes the column number, type, address, and length of a data buffer, and the address of a length/indicator buffer. For information about how these addresses are used, see "Buffer Addresses," later in this section. For more information about binding columns, see [Using SQLBindCol](../../../odbc/reference/develop-app/using-sqlbindcol.md).  
  
 The use of these buffers is deferred; that is, the application binds them in **SQLBindCol** but the driver accesses them from other functions - namely **SQLBulkOperations**, **SQLFetch**, **SQLFetchScroll**, or **SQLSetPos**. It is the application's responsibility to make sure that the pointers specified in **SQLBindCol** remain valid as long as the binding remains in effect. If the application allows these pointers to become invalid - for example, it frees a buffer - and then calls a function that expects them to be valid, the consequences are undefined. For more information, see [Deferred Buffers](../../../odbc/reference/develop-app/deferred-buffers.md).  
  
 The binding remains in effect until it is replaced by a new binding, the column is unbound, or the statement is freed.  
  
## Unbinding Columns  
 To unbind a single column, an application calls **SQLBindCol** with *ColumnNumber* set to the number of that column and *TargetValuePtr* set to a null pointer. If *ColumnNumber* refers to an unbound column, **SQLBindCol** still returns SQL_SUCCESS.  
  
 To unbind all columns, an application calls **SQLFreeStmt** with *fOption* set to SQL_UNBIND. This can also be accomplished by setting the SQL_DESC_COUNT field of the ARD to zero.  
  
## Rebinding Columns  
 An application can perform either of two operations to change a binding:  
  
-   Call **SQLBindCol** to specify a new binding for a column that is already bound. The driver overwrites the old binding with the new one.  
  
-   Specify an offset to be added to the buffer address that was specified by the binding call to **SQLBindCol**. For more information, see the next section, "Binding Offsets."  
  
## Binding Offsets  
 A binding offset is a value that is added to the addresses of the data and length/indicator buffers (as specified in the *TargetValuePtr* and *StrLen_or_IndPtr* argument) before they are dereferenced. When offsets are used, the bindings are a "template" of how the application's buffers are laid out, and the application can move this "template" to different areas of memory by changing the offset. Because the same offset is added to each address in each binding, the relative offsets between buffers for different columns must be the same within each set of buffers. This is always true when row-wise binding is used; the application must carefully lay out its buffers for this to be true when column-wise binding is used.  
  
 Using a binding offset has basically the same effect as rebinding a column by calling **SQLBindCol**. The difference is that a new call to **SQLBindCol** specifies new addresses for the data buffer and length/indicator buffer, whereas use of a binding offset does not change the addresses but just adds an offset to them. The application can specify a new offset whenever it wants, and this offset is always added to the originally bound addresses. In particular, if the offset is set to 0 or if the statement attribute is set to a null pointer, the driver uses the originally bound addresses.  
  
 To specify a binding offset, the application sets the SQL_ATTR_ROW_BIND_OFFSET_PTR statement attribute to the address of an SQLINTEGER buffer. Before the application calls a function that uses bindings, it puts an offset in bytes in this buffer. To determine the address of the buffer to use, the driver adds the offset to the address in the binding. The sum of the address and the offset must be a valid address, but the address to which the offset is added does not have to be valid. For more information about how binding offsets are used, see "Buffer Addresses," later in this section.  
  
## Binding Arrays  
 If the rowset size (the value of the SQL_ATTR_ROW_ARRAY_SIZE statement attribute) is greater than 1, the application binds arrays of buffers instead of single buffers. For more information, see [Block Cursors](../../../odbc/reference/develop-app/block-cursors.md).  
  
 The application can bind arrays in two ways:  
  
-   Bind an array to each column. This is referred to as *column-wise binding* because each data structure (array) contains data for a single column.  
  
-   Define a structure to hold the data for a whole row and bind an array of these structures. This is referred to as *row-wise binding* because each data structure contains the data for a single row.  
  
 Each array of buffers must have at least as many elements as the size of the rowset.  
  
> [!NOTE]  
>  An application must verify that alignment is valid. For more information about alignment considerations, see [Alignment](../../../odbc/reference/develop-app/alignment.md).  
  
## Column-Wise Binding  
 In column-wise binding, the application binds separate data and length/indicator arrays to each column.  
  
 To use column-wise binding, the application first sets the SQL_ATTR_ROW_BIND_TYPE statement attribute to SQL_BIND_BY_COLUMN. (This is the default.) For each column to be bound, the application performs the following steps:  
  
1.  Allocates a data buffer array.  
  
2.  Allocates an array of length/indicator buffers.  
  
    > [!NOTE]  
    >  If the application writes directly to descriptors when column-wise binding is used, separate arrays can be used for length and indicator data.  
  
3.  Calls **SQLBindCol** with the following arguments:  
  
    -   *TargetType* is the type of a single element in the data buffer array.  
  
    -   *TargetValuePtr* is the address of the data buffer array.  
  
    -   *BufferLength* is the size of a single element in the data buffer array. The *BufferLength* argument is ignored when the data is fixed-length data.  
  
    -   *StrLen_or_IndPtr* is the address of the length/indicator array.  
  
 For more information about how this information is used, see "Buffer Addresses," later in this section. For more information about column-wise binding, see [Column-Wise Binding](../../../odbc/reference/develop-app/column-wise-binding.md).  
  
## Row-Wise Binding  
 In row-wise binding, the application defines a structure that contains data and length/indicator buffers for each column to be bound.  
  
 To use row-wise binding, the application performs the following steps:  
  
1.  Defines a structure to hold a single row of data (including both data and length/indicator buffers) and allocates an array of these structures.  
  
    > [!NOTE]  
    >  If the application writes directly to descriptors when row-wise binding is used, separate fields can be used for length and indicator data.  
  
2.  Sets the SQL_ATTR_ROW_BIND_TYPE statement attribute to the size of the structure that contains a single row of data or to the size of an instance of a buffer into which the results columns will be bound. The length must include space for all the bound columns, and any padding of the structure or buffer, to make sure that when the address of a bound column is incremented with the specified length, the result will point to the beginning of the same column in the next row. When using the *sizeof* operator in ANSI C, this behavior is guaranteed.  
  
3.  Calls **SQLBindCol** with the following arguments for each column to be bound:  
  
    -   *TargetType* is the type of the data buffer member to be bound to the column.  
  
    -   *TargetValuePtr* is the address of the data buffer member in the first array element.  
  
    -   *BufferLength* is the size of the data buffer member.  
  
    -   *StrLen_or_IndPtr* is the address of the length/indicator member to be bound.  
  
 For more information about how this information is used, see "Buffer Addresses," later in this section. For more information about column-wise binding, see [Row-Wise Binding](../../../odbc/reference/develop-app/row-wise-binding.md).  
  
## Buffer Addresses  
 The *buffer address* is the actual address of the data or length/indicator buffer. The driver calculates the buffer address just before it writes to the buffers (such as during fetch time). It is calculated from the following formula, which uses the addresses specified in the *TargetValuePtr* and *StrLen_or_IndPtr* arguments, the binding offset, and the row number:  
  
 *Bound Address* + *Binding Offset* + ((*Row Number* - 1) x *Element Size*)  
  
 where the formula's variables are defined as described in the following table.  
  
|Variable|Description|  
|--------------|-----------------|  
|*Bound Address*|For data buffers, the address specified with the *TargetValuePtr* argument in **SQLBindCol**.<br /><br /> For length/indicator buffers, the address specified with the *StrLen_or_IndPtr* argument in **SQLBindCol**. For more information, see "Additional Comments" in the "Descriptors and SQLBindCol" section.<br /><br /> If the bound address is 0, no data value is returned, even if the address as calculated by the previous formula is nonzero.|  
|*Binding Offset*|If row-wise binding is used, the value stored at the address specified with the SQL_ATTR_ROW_BIND_OFFSET_PTR statement attribute.<br /><br /> If column-wise binding is used or if the value of the SQL_ATTR_ROW_BIND_OFFSET_PTR statement attribute is a null pointer, *Binding Offset* is 0.|  
|*Row Number*|The 1-based number of the row in the rowset. For single-row fetches, which are the default, this is 1.|  
|*Element Size*|The size of an element in the bound array.<br /><br /> If column-wise binding is used, this is **sizeof(SQLINTEGER)** for length/indicator buffers. For data buffers, it is the value of the *BufferLength* argument in **SQLBindCol** if the data type is variable length, and the size of the data type if the data type is fixed length.<br /><br /> If row-wise binding is used, this is the value of the SQL_ATTR_ROW_BIND_TYPE statement attribute for both data and length/indicator buffers.|  
  
## Descriptors and SQLBindCol  
 The following sections describe how **SQLBindCol** interacts with descriptors.  
  
> [!CAUTION]  
>  Calling **SQLBindCol** for one statement can affect other statements. This occurs when the ARD associated with the statement is explicitly allocated and is also associated with other statements. Because **SQLBindCol** modifies the descriptor, the modifications apply to all statements with which this descriptor is associated. If this is not the required behavior, the application should dissociate this descriptor from the other statements before it calls **SQLBindCol**.  
  
## Argument Mappings  
 Conceptually, **SQLBindCol** performs the following steps in sequence:  
  
1.  Calls **SQLGetStmtAttr** to obtain the ARD handle.  
  
2.  Calls **SQLGetDescField** to get this descriptor's SQL_DESC_COUNT field, and if the value in the *ColumnNumber* argument exceeds the value of SQL_DESC_COUNT, calls **SQLSetDescField** to increase the value of SQL_DESC_COUNT to *ColumnNumber*.  
  
3.  Calls **SQLSetDescField** multiple times to assign values to the following fields of the ARD:  
  
    -   Sets SQL_DESC_TYPE and SQL_DESC_CONCISE_TYPE to the value of *TargetType*, except that if *TargetType* is one of the concise identifiers of a datetime or interval subtype, it sets SQL_DESC_TYPE to SQL_DATETIME or SQL_INTERVAL, respectively; sets SQL_DESC_CONCISE_TYPE to the concise identifier; and sets SQL_DESC_DATETIME_INTERVAL_CODE to the corresponding datetime or interval subcode.  
  
    -   Sets one or more of SQL_DESC_LENGTH, SQL_DESC_PRECISION, SQL_DESC_SCALE, and SQL_DESC_DATETIME_INTERVAL_PRECISION, as appropriate for *TargetType*.  
  
    -   Sets the SQL_DESC_OCTET_LENGTH field to the value of *BufferLength*.  
  
    -   Sets the SQL_DESC_DATA_PTR field to the value of *TargetValue*.  
  
    -   Sets the SQL_DESC_INDICATOR_PTR field to the value of *StrLen_or_Ind*. (See the following paragraph.)  
  
    -   Sets the SQL_DESC_OCTET_LENGTH_PTR field to the value of *StrLen_or_Ind*. (See the following paragraph.)  
  
 The variable that the *StrLen_or_Ind* argument refers to is used for both indicator and length information. If a fetch encounters a null value for the column, it stores SQL_NULL_DATA in this variable; otherwise, it stores the data length in this variable. Passing a null pointer as *StrLen_or_Ind* keeps the fetch operation from returning the data length but makes the fetch fail if it encounters a null value and has no way to return SQL_NULL_DATA.  
  
 If the call to **SQLBindCol** fails, the content of the descriptor fields that it would have set in the ARD are undefined and the value of the SQL_DESC_COUNT field of the ARD is unchanged.  
  
## Implicit Resetting of COUNT Field  
 **SQLBindCol** sets SQL_DESC_COUNT to the value of the *ColumnNumber* argument only when this would increase the value of SQL_DESC_COUNT. If the value in the *TargetValuePtr* argument is a null pointer and the value in the *ColumnNumber* argument is equal to SQL_DESC_COUNT (that is, when unbinding the highest bound column), then SQL_DESC_COUNT is set to the number of the highest remaining bound column.  
  
## Cautions Regarding SQL_DEFAULT  
 To retrieve column data successfully, the application must determine correctly the length and starting point of the data in the application buffer. When the application specifies an explicit *TargetType*, application misconceptions are easily detected. However, when the application specifies a *TargetType* of SQL_DEFAULT, **SQLBindCol** can be applied to a column of a different data type from the one intended by the application, either from changes to the metadata or by applying the code to a different column. In this case, the application may not always determine the start or length of the fetched column data. This may lead to unreported data errors or memory violations.  
  
## Code Example  
 In the following example, an application executes a **SELECT** statement on the Customers table to return a result set of the customer IDs, names, and phone numbers, sorted by name. It then calls **SQLBindCol** to bind the columns of data to local buffers. Finally, the application fetches each row of data with **SQLFetch** and prints each customer's name, ID, and phone number.  
  
 For more code examples, see [SQLBulkOperations Function](../../../odbc/reference/syntax/sqlbulkoperations-function.md), [SQLColumns Function](../../../odbc/reference/syntax/sqlcolumns-function.md), [SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md), and [SQLSetPos Function](../../../odbc/reference/syntax/sqlsetpos-function.md).  
  
```  
// SQLBindCol_ref.cpp  
// compile with: odbc32.lib  
#include <windows.h>  
#include <stdio.h>  
  
#define UNICODE  
#include <sqlext.h>  
  
#define NAME_LEN 50  
#define PHONE_LEN 20  
  
void show_error() {  
   printf("error\n");  
}  
  
int main() {  
   SQLHENV henv;  
   SQLHDBC hdbc;  
   SQLHSTMT hstmt = 0;  
   SQLRETURN retcode;  
   SQLWCHAR szName[NAME_LEN], szPhone[PHONE_LEN], sCustID[NAME_LEN];  
   SQLLEN cbName = 0, cbCustID = 0, cbPhone = 0;  
  
   // Allocate environment handle  
   retcode = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &henv);  
  
   // Set the ODBC version environment attribute  
   if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
      retcode = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER*)SQL_OV_ODBC3, 0);   
  
      // Allocate connection handle  
      if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
         retcode = SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc);  
  
         // Set login timeout to 5 seconds  
         if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
            SQLSetConnectAttr(hdbc, SQL_LOGIN_TIMEOUT, (SQLPOINTER)5, 0);  
  
            // Connect to data source  
            retcode = SQLConnect(hdbc, (SQLWCHAR*) L"NorthWind", SQL_NTS, (SQLWCHAR*) NULL, 0, NULL, 0);  
  
            // Allocate statement handle  
            if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {   
               retcode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt);   
  
               retcode = SQLExecDirect(hstmt, (SQLWCHAR *) L"SELECT CustomerID, ContactName, Phone FROM CUSTOMERS ORDER BY 2, 1, 3", SQL_NTS);  
               if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
  
                  // Bind columns 1, 2, and 3  
                  retcode = SQLBindCol(hstmt, 1, SQL_C_CHAR, &sCustID, 100, &cbCustID);  
                  retcode = SQLBindCol(hstmt, 2, SQL_C_CHAR, szName, NAME_LEN, &cbName);  
                  retcode = SQLBindCol(hstmt, 3, SQL_C_CHAR, szPhone, PHONE_LEN, &cbPhone);   
  
                  // Fetch and print each row of data. On an error, display a message and exit.  
                  for (i ; ; i++) {  
                     retcode = SQLFetch(hstmt);  
                     if (retcode == SQL_ERROR || retcode == SQL_SUCCESS_WITH_INFO)  
                        show_error();  
                     if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO)  
                        wprintf(L"%d: %S %S %S\n", i + 1, sCustID, szName, szPhone);  
                     else  
                        break;  
                  }  
               }  
  
               // Process data  
               if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
                  SQLCancel(hstmt);  
                  SQLFreeHandle(SQL_HANDLE_STMT, hstmt);  
               }  
  
               SQLDisconnect(hdbc);  
            }  
  
            SQLFreeHandle(SQL_HANDLE_DBC, hdbc);  
         }  
      }  
      SQLFreeHandle(SQL_HANDLE_ENV, henv);  
   }  
}  
```  
  
 Also see, [Sample ODBC Program](../../../odbc/reference/sample-odbc-program.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Returning information about a column in a result set|[SQLDescribeCol Function](../../../odbc/reference/syntax/sqldescribecol-function.md)|  
|Fetching a block of data or scrolling through a result set|[SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|  
|Fetching multiple rows of data|[SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md)|  
|Releasing column buffers on the statement|[SQLFreeStmt Function](../../../odbc/reference/syntax/sqlfreestmt-function.md)|  
|Fetching part or all of a column of data|[SQLGetData Function](../../../odbc/reference/syntax/sqlgetdata-function.md)|  
|Returning the number of result set columns|[SQLNumResultCols Function](../../../odbc/reference/syntax/sqlnumresultcols-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
