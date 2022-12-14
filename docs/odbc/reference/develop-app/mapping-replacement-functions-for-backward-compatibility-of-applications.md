---
description: "Mapping Replacement Functions for Backward Compatibility of Applications"
title: "Mapping Replacement Functions for Compatibility of Apps - ODBC | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "mapping replacement functions [ODBC]"
  - "upgrading applications [ODBC], mapping replacement functions"
  - "compatibility [ODBC], mapping replacement functions"
  - "ODBC drivers [ODBC], backward compatibility"
  - "functions [ODBC], mapping replacement functions"
  - "application upgrades [ODBC], mapping replacement functions"
  - "backward compatibility [ODBC], mapping replacement functions"
ms.assetid: f5e6d9da-76ef-42cb-b3f5-f640857df732
author: David-Engel
ms.author: v-davidengel
---
# Mapping Replacement Functions for Backward Compatibility of Applications
An ODBC *3.x* application working through the ODBC *3.x* Driver Manager will work against an ODBC *2.x* driver as long as no new features are used. Both duplicated functionality and behavioral changes do, however, affect the way that the ODBC *3.x* application works on an ODBC *2.x* driver. When working with an ODBC *2.x* driver, the Driver Manager maps the following ODBC *3.x* functions, which have replaced one or more ODBC *2.x* functions, into the corresponding ODBC *2.x* functions.  
  
|ODBC *3.x* function|ODBC *2.x* function|  
|-------------------------|-------------------------|  
|**SQLAllocHandle**|**SQLAllocEnv**, **SQLAllocConnect**, or **SQLAllocStmt**|  
|**SQLBulkOperations**|**SQLSetPos**|  
|**SQLColAttribute**|**SQLColAttributes**|  
|**SQLEndTran**|**SQLTransact**|  
|**SQLFetch**|**SQLExtendedFetch**|  
|**SQLFetchScroll**|**SQLExtendedFetch**|  
|**SQLFreeHandle**|**SQLFreeEnv**, **SQLFreeConnect**, or **SQLFreeStmt**|  
|**SQLGetConnectAttr**|**SQLGetConnectOption**|  
|**SQLGetDiagRec**|**SQLError**|  
|**SQLGetStmtAttr**|**SQLGetStmtOption**[1]|  
|**SQLSetConnectAttr**|**SQLSetConnectOption**|  
|**SQLSetStmtAttr**|**SQLSetStmtOption**[1]|  
  
 [1]   Other actions might also be taken, depending on the attribute being requested.  
  
## SQLAllocHandle  
 The Driver Manager maps this to **SQLAllocEnv**, **SQLAllocConnect**, or **SQLAllocStmt**, as appropriate. The following call to **SQLAllocHandle**:  
  
```  
SQLAllocHandle(HandleType, InputHandle, OutputHandlePtr);  
```  
  
 will result in the Driver Manager performing the following (conceptual, no error checking) mapping:  
  
```  
switch (HandleType) {  
   case SQL_HANDLE_ENV: return (SQLAllocEnv(OutputHandlePtr));  
   case SQL_HANDLE_DBC: return (SQLAllocConnect (InputHandle, OutputHandlePtr));  
   case SQL_HANDLE_STMT: return (SQLAllocStmt (InputHandle, OutputHandlePtr));  
   default: // return SQL_ERROR, SQLSTATE HY092 ("Invalid attribute/option identifier")  
}  
```  
  
## SQLBulkOperations  
 The Driver Manager maps this to **SQLSetPos**. The following call to **SQLBulkOperations**:  
  
```  
SQLBulkOperations(hstmt, Operation);  
```  
  
 will result in the following sequence of steps:  
  
1.  If the Operation argument is SQL_ADD, the Driver Manager calls **SQLSetPos** as follows:  
  
    ```  
    SQLSetPos (hstmt, 0, SQL_ADD, SQL_LOCK_NO_CHANGE);  
    ```  
  
2.  If the Operation argument is not SQL_ADD, the driver returns SQLSTATE HY092 (Invalid attribute/option identifier).  
  
3.  If the application attempts to change the SQL_ATTR_ROW_STATUS_PTR between calls to **SQLFetch** or **SQLFetchScroll** and **SQLBulkOperations**, the Driver Manager will return SQLSTATE HY011 (Attribute cannot be set now).  
  
4.  If the Operation argument is SQL_ADD, the application must call **SQLBindCol** to bind the data to be inserted. It cannot call **SQLSetDescField** or **SQLSetDescRec** to bind the data to be inserted.  
  
5.  If the Operation argument is SQL_ADD and the number of rows to be inserted is not the same as the current rowset size, **SQLSetStmtAttr** must be called to set the SQL_ATTR_ROW_ARRAY_SIZE statement attribute to the number of rows to be inserted before calling **SQLBulkOperations**. To revert back to the previous rowset size, the application must set the SQL_ATTR_ROW_ARRAY_SIZE statement attribute before **SQLFetch**, **SQLFetchScroll**, or **SQLSetPos** is called.  
  
## SQLColAttribute  
 The Driver Manager maps this to **SQLColAttributes**. The following call to **SQLColAttribute**:  
  
```  
SQLColAttribute(StatementHandle, ColumnNumber, FieldIdentifier, CharacterAttributePtr, BufferLength, StringLengthPtr, NumericAttributePtr);  
```  
  
 will result in the following sequence of steps:  
  
1.  If *FieldIdentifier* is one of the following:  
  
     SQL_DESC_PRECISION, SQL_DESC_SCALE, SQL_DESC_LENGTH, SQL_DESC_OCTET_LENGTH, SQL_DESC_UNNAMED, SQL_DESC_BASE_COLUMN_NAME, SQL_DESC_LITERAL_PREFIX, SQL_DESC_LITERAL_SUFFIX, or SQL_DESC_LOCAL_TYPE_NAME  
  
     the Driver Manager returns SQL_ERROR with SQLSTATE HY091 (Invalid descriptor field identifier). No further rules of this section apply.  
  
2.  The Driver Manager maps SQL_COLUMN_COUNT, SQL_COLUMN_NAME, or SQL_COLUMN_NULLABLE to SQL_DESC_COUNT, SQL_DESC_NAME, or SQL_DESC_NULLABLE, respectively. (An ODBC *2.x* driver need only support SQL_COLUMN_COUNT, SQL_COLUMN_NAME, and SQL_COLUMN_NULLABLE, not SQL_DESC_COUNT, SQL_DESC_NAME, and SQL_DESC_NULLABLE.) The call to SQLColAttribute is mapped to:  
  
    ```  
    SQLColAttributes(StatementHandle, ColumnNumber, FieldIdentifier, CharacterAttributePtr, BufferLength, StringLengthPtr, NumericAttributePtr);  
    ```  
  
3.  All other *FieldIdentifier* values are passed through to the driver, with **SQLColAttribute** mapped to **SQLColAttributes** as shown previously.  
  
4.  If *BufferLength* is less than 0, the Driver Manager returns SQL_ERROR with SQLSTATE HY090 (Invalid string or buffer length). No further rules of this section apply.  
  
5.  If *FieldIdentifier* is SQL_DESC_CONCISE_TYPE and the returned type is a concise datetime data type, the Driver Manager maps the return values for date, time, and timestamp codes.  
  
6.  The Driver Manager performs necessary checks to see whether SQLSTATE HY010 (Function sequence error) needs to be raised. If so, the Driver Manager returns SQL_ERROR and SQLSTATE HY010 (Function sequence error). No further rules of this section apply.  
  
## SQLEndTran  
 The Driver Manager maps this to **SQLTransact**. The following call to **SQLEndTran**:  
  
```  
SQLEndTran(HandleType, Handle, CompletionType);  
```  
  
 will result in the Driver Manager performing the following (conceptual, no error checking) mapping:  
  
```  
switch (HandleType) {  
   case SQL_HANDLE_ENV:return(SQLTransact(Handle, SQL_NULL_HDBC, CompletionType));  
   case SQL_HANDLE_DBC:return(SQLTransact(SQL_NULL_HENV, Handle, CompletionType);  
   default: // return SQL_ERROR, SQLSTATE HY092 ("Invalid attribute/option identifier")  
}  
```  
  
## SQLFetch  
 The Driver Manager maps this to **SQLExtendedFetch** with a *FetchOrientation* argument of SQL_FETCH_NEXT. The following call to **SQLFetch**:  
  
```  
SQLFetch (StatementHandle);  
```  
  
 will result in the Driver Manager calling **SQLExtendedFetch**, as follows:  
  
```  
rc = SQLExtendedFetch(StatementHandle, FetchOrientation, FetchOffset, &RowCount, RowStatusArray);  
```  
  
 In this call, the *pcRow* argument is set to the value that the application sets the SQL_ATTR_ROWS_FETCHED_PTR statement attribute to through a call to **SQLSetStmtAttr**.  
  
> [!NOTE]  
>  When the application calls **SQLSetStmtAttr** to set SQL_ATTR_ROW_STATUS_PTR to point to a status array, the Driver Manager caches the pointer. *RowStatusArray* can be equal to a null pointer.  
  
 If the driver does not support **SQLExtendedFetch** and the cursor library is loaded, the Driver Manager uses the cursor library's **SQLExtendedFetch** to map **SQLFetch** to **SQLExtendedFetch**. If the driver does not support **SQLExtendedFetch** and the cursor library is not loaded, the Driver Manager passes the call to **SQLFetch** through to the driver. If the application calls **SQLSetStmtAttr** to set SQL_ATTR_ROW_STATUS_PTR, the Driver Manager ensures that the array is populated. If the application calls **SQLSetStmtAttr** to set SQL_ATTR_ROWS_FETCHED_PTR, the Driver Manager sets this field to 1.  
  
## SQLFetchScroll  
 The Driver Manager maps this to **SQLExtendedFetch**. The following call to **SQLFetchScroll**:  
  
```  
SQLFetchScroll(StatementHandle, FetchOrientation, FetchOffset);  
```  
  
 will result in the following sequence of steps:  
  
1.  When the application calls **SQLSetStmtAttr** to set SQL_ATTR_ROW_STATUS_PTR (which sets the SQL_DESC_ARRAY_STATUS_PTR field in the IRD) to point to a status array, the Driver Manager caches this pointer. Let this pointer be *RowStatusArray*; otherwise, let *RowStatusArray* be equal to a null pointer. If the *RowStatusArray* argument is set to a null pointer, the Driver Manager generates a row-status array.  
  
2.  If *FetchOrientation* is not one of SQL_FETCH_NEXT, SQL_FETCH_PRIOR, SQL_FETCH_ABSOLUTE, SQL_FETCH_RELATIVE, SQL_FETCH_FIRST, SQL_FETCH_LAST, or SQL_FETCH_BOOKMARK, the Driver Manager returns with SQL_ERROR and SQLSTATE HY106 (Fetch type out of range). No further rules of this section apply.  
  
3.  Case:  
  
-   If *FetchOrientation* is equal to SQL_FETCH_BOOKMARK, then:  
  
    -   If **SQLSetStmtAttr** was called earlier to set the value of SQL_ATTR_FETCH_BOOKMARK_PTR, then let *Bmk* be the value obtained by dereferencing the pointer SQL_DESC_FETCH_BOOKMARK_PTR.  
  
    -   Otherwise, return SQL_ERROR with SQLSTATE HY111 (Invalid bookmark value). No further rules of this section apply.  
  
     The Driver Manager now calls **SQLExtendedFetch**, as follows:  
  
    ```  
    rc = SQLExtendedFetch(StatementHandle, FetchOrientation, Bmk, pcRow, RowStatusArray);  
    ```  
  
-   Otherwise, the Driver Manager calls **SQLExtendedFetch**, as follows:  
  
    ```  
    rc = SQLExtendedFetch(StatementHandle, FetchOrientation, FetchOffset, pcRow, RowStatusArray);  
    ```  
  
     In these calls, the *pcRow* argument is set to the value that the application sets the SQL_ATTR_ROWS_FETCHED_PTR statement attribute to through a call to **SQLSetStmtAttr**.  
  
-   SQL_ATTR_ROW_ARRAY_SIZE is mapped to SQL_ROWSET_SIZE.  
  
-   If *rc* is equal to SQL_SUCCESS or SQL_SUCCESS_WITH_INFO, and if *FetchOrientation* is equal to SQL_FETCH_BOOKMARK and *FetchOffset* is not equal to 0, then the Driver Manager posts a warning, SQLSTATE 01S10 (Attempt to fetch by a bookmark offset, offset value ignored), and returns SQL_SUCCESS_WITH_INFO.  
  
## SQLFreeHandle  
 The Driver Manager maps this to **SQLFreeEnv**, **SQLFreeConnect**, or **SQLFreeStmt** as appropriate. The following call to **SQLFreeHandle**:  
  
```  
SQLFreeHandle(HandleType, Handle);  
```  
  
 will result in the Driver Manager performing the following (conceptual, no error checking) mapping:  
  
```  
switch (HandleType) {  
   case SQL_HANDLE_ENV: return (SQLFreeEnv(Handle));  
   case SQL_HANDLE_DBC: return (SQLFreeConnect(Handle));  
   case SQL_HANDLE_STMT: return (SQLFreeStmt(Handle, SQL_DROP));  
   default: // return SQL_ERROR, SQLSTATE HY092 ("Invalid attribute/option identifier")  
}  
```  
  
## SQLGetConnectAttr  
 The Driver Manager maps this to **SQLGetConnectOption**. The following call to **SQLGetConnectAttr**:  
  
```  
SQLGetConnectAttr(ConnectionHandle, Attribute, ValuePtr, BufferLength, StringLengthPtr);  
```  
  
 will result in the following sequence of steps:  
  
1.  If *Attribute* is not a driver-defined connection or statement attribute and is not an attribute defined in ODBC *2.x*, the Driver Manager returns SQL_ERROR with SQLSTATE HY092 (Invalid attribute/option identifier). No further rules in this section apply.  
  
2.  If *Attribute* is equal to SQL_ATTR_AUTO_IPD or SQL_ATTR_METADATA_ID, the Driver Manager returns SQL_ERROR with SQLSTATE HY092 (Invalid attribute/option identifier).  
  
3.  The Driver Manager performs necessary checks to see if SQLSTATE 08003 (Connection not open) or SQLSTATE HY010 (Function sequence error) needs to be raised. If so, the Driver Manager returns SQL_ERROR and posts the appropriate error message. No further rules of this section apply.  
  
4.  The Driver Manager calls **SQLGetConnectOption** as follows:  
  
    ```  
    SQLGetConnectOption (ConnectionHandle, Attribute, ValuePtr);  
    ```  
  
     Note that the *BufferLength* and *StringLengthPtr* are ignored.  
  
## SQLGetData  
 When an ODBC *3.x* application working with an ODBC *2.x* driver calls **SQLGetData** with the *ColumnNumber* argument equal to 0, the ODBC *3.x* Driver Manager maps this to a call to **SQLGetStmtOption** with the *Option* attribute set to SQL_GET_BOOKMARK.  
  
## SQLGetStmtAttr  
 The Driver Manager maps this to **SQLGetStmtOption**. The following call to **SQLGetStmtAttr**:  
  
```  
SQLGetStmtAttr(StatementHandle, Attribute, ValuePtr, BufferLength, StringLengthPtr);  
```  
  
 will result in the following sequence of steps:  
  
1.  If *Attribute* is not a driver-defined connection or statement attribute and is not an attribute defined in ODBC *2.x*, the Driver Manager returns SQL_ERROR with SQLSTATE HY092 (Invalid attribute/option identifier). No further rules in this section apply.  
  
2.  If *Attribute* is one of the following:  
  
     SQL_ATTR_APP_ROW_DESC  
  
     SQL_ATTR_APP_PARAM_DESC  
  
     SQL_ATTR_AUTO_IPD  
  
     SQL_ATTR_ROW_BIND_TYPE  
  
     SQL_ATTR_IMP_ROW_DESC  
  
     SQL_ATTR_IMP_PARAM_DESC  
  
     SQL_ATTR_METADATA_ID  
  
     SQL_ATTR_PARAM_BIND_TYPE  
  
     SQL_ATTR_PREDICATE_PTR  
  
     SQL_ATTR_PREDICATE_OCTET_LENGTH_PTR  
  
     SQL_ATTR_PARAM_BIND_OFFSET_PTR  
  
     SQL_ATTR_ROW_BIND_OFFSET_PTR  
  
     SQL_ATTR_ROW_OPERATION_PTR  
  
     SQL_ATTR_PARAM_OPERATION_PTR  
  
     the Driver Manager returns SQL_ERROR with SQLSTATE HY092 (Invalid attribute/option identifier). No further rules of this section apply.  
  
3.  The Driver Manager performs necessary checks to see whether SQLSTATE HY010 (Function sequence error) needs to be raised. If so, the Driver Manager returns SQL_ERROR and SQLSTATE HY010 (Function sequence error). No further rules of this section apply.  
  
4.  If *Attribute* is equal to SQL_ATTR_ROWS_FETCHED_PTR, the Driver Manager returns a pointer to the internal Driver Manager variable *cRow*, which it has used or will use in a call to **SQLExtendedFetch**. No further rules of this section apply.  
  
5.  If *Attribute* is equal to SQL_DESC_FETCH_BOOKMARK_PTR, the Driver Manager returns the appropriate pointer that it had cached during a call to **SQLSetStmtAttr**.  
  
6.  If *Attribute* is equal to SQL_ATTR_ROW_STATUS_PTR, the Driver Manager returns the appropriate pointer that it had cached during a call to **SQLSetStmtAttr**.  
  
7.  The Driver Manager calls **SQLGetStmtOption** as follows:  
  
    ```  
    SQLGetStmtOption (hstmt, fOption, pvParam);  
    ```  
  
     where *hstmt*, *fOption*, and *pvParam* will be set to the values of *StatementHandle*, *Attribute*, and *ValuePtr*, respectively. The *BufferLength* and *StringLengthPtr* are ignored.  
  
## SQLSetConnectAttr  
 The Driver Manager maps this to **SQLSetConnectOption**. The following call to **SQLSetConnectAttr**:  
  
```  
SQLSetConnectAttr(ConnectionHandle, Attribute, ValuePtr, StringLength);  
```  
  
 will result in the following sequence of steps:  
  
1.  If *Attribute* is not a driver-defined connection or statement attribute and is not an attribute defined in ODBC *2.x*, the Driver Manager returns SQL_ERROR with SQLSTATE HY092 (Invalid attribute/option identifier). No further rules in this section apply.  
  
2.  If *Attribute* is equal to SQL_ATTR_AUTO_IPD, the Driver Manager returns SQL_ERROR with SQLSTATE HY092 (Invalid attribute/option identifier).  
  
3.  The Driver Manager performs necessary checks to see whether SQLSTATE 08003 (Connection not open) or SQLSTATE HY010 (Function sequence error) need to be raised. If one of these errors needs to be raised, the Driver Manager returns SQL_ERROR and posts the appropriate error message. No further rules of this section apply.  
  
4.  The Driver Manager calls **SQLSetConnectOption** as follows:  
  
    ```  
    SQLSetConnectOption (hdbc, fOption, vParam);  
    ```  
  
     where *hdbc*, *fOption*, and *vParam* will be set to the values of *ConnectionHandle*, *Attribute*, and *ValuePtr*, respectively. *StringLengthPtr* is ignored.  
  
> [!NOTE]  
>  The ability to set statement attributes on the connection level has been deprecated. Statement attributes should never be set on the connection level by an ODBC *3.x* application.  
  
## SQLSetStmtAttr  
 The Driver Manager maps this to **SQLSetStmtOption**. The following call to **SQLSetStmtAttr**:  
  
```  
SQLSetStmtAttr(StatementHandle, Attribute, ValuePtr, StringLength);  
```  
  
 will result in the following sequence of steps:  
  
1.  If *Attribute* is not a driver-defined connection or statement attribute and is not an attribute defined in ODBC *2.x*, the Driver Manager returns SQL_ERROR with SQLSTATE HY092 (Invalid attribute/option identifier). No further rules in this section apply.  
  
2.  If *Attribute* is one of the following:  
  
     SQL_ATTR_APP_ROW_DESC  
  
     SQL_ATTR_APP_PARAM_DESC  
  
     SQL_ATTR_AUTO_IPD  
  
     SQL_ATTR_ROW_BIND_TYPE  
  
     SQL_ATTR_IMP_ROW_DESC  
  
     SQL_ATTR_IMP_PARAM_DESC  
  
     SQL_ATTR_METADATA_ID  
  
     SQL_ATTR_PARAM_BIND_TYPE  
  
     SQL_ATTR_PREDICATE_PTR  
  
     SQL_ATTR_PREDICATE_OCTET_LENGTH_PTR  
  
     SQL_ATTR_PARAM_BIND_OFFSET_PTR  
  
     SQL_ATTR_ROW_BIND_OFFSET_PTR  
  
     SQL_ATTR_ROW_OPERATION_PTR  
  
     SQL_ATTR_PARAM_OPERATION_PTR  
  
     the Driver Manager returns SQL_ERROR with SQLSTATE HY092 (Invalid attribute/option identifier). No further rules of this section apply.  
  
3.  The Driver Manager performs the necessary checks to see whether SQLSTATE HY010 (Function sequence error) need to be raised. If so, the Driver Manager returns SQL_ERROR and SQLSTATE HY010 (Function sequence error). No further rules of this section apply.  
  
4.  If *Attribute* is equal to SQL_ATTR_PARAMSET_SIZE or SQL_ATTR_PARAMS_PROCESSED_PTR, see the section "Mappings for Handling Parameter Arrays," later in this topic. No further rules of this section apply.  
  
5.  If *Attribute* is equal to SQL_ATTR_ROWS_FETCHED_PTR, the Driver Manager caches the pointer value for later use with **SQLFetchScroll**.  
  
6.  If *Attribute* is equal to SQL_ATTR_ROW_STATUS_PTR, the Driver Manager caches the pointer value for later use with **SQLFetchScroll** or **SQLSetPos**. No further rules of this section apply.  
  
7.  If *Attribute* is equal to SQL_ATTR_FETCH_BOOKMARK_PTR, the Driver Manager caches *ValuePtr* and will use the cached value later in a call to **SQLFetchScroll**. No further rules of this section apply.  
  
8.  The Driver Manager calls **SQLSetStmtOption** as follows:  
  
    ```  
    SQLSetStmtOption (hstmt, fOption, vParam);  
    ```  
  
     where *hstmt*, *fOption*, and *vParam* will be set to the values of *StatementHandle*, *Attribute*, and *ValuePtr*, respectively. The *StringLength* argument is ignored.  
  
     If an ODBC *2.x* driver supports character-string, driver-specific statement options, an ODBC *3.x* application should call **SQLSetStmtOption** to set those options.  
  
## Mappings for Handling Parameter Arrays  
 When the application calls:  
  
```  
SQLSetStmtAttr (StatementHandle, SQL_ATTR_PARAMSET_SIZE, Size, StringLength);  
```  
  
 the Driver Manager calls:  
  
```  
SQLParamOptions (StatementHandle, Size, &RowCount);  
```  
  
 The Driver Manager later returns a pointer to this variable when the application calls **SQLGetStmtAttr** to retrieve SQL_ATTR_PARAMS_PROCESSED_PTR. The Driver Manager cannot change this internal variable until the statement handle is returned to the prepared or allocated state.  
  
 An ODBC *3.x* application can call **SQLGetStmtAttr** to obtain the value of SQL_ATTR_PARAMS_PROCESSED_PTR even though it has not explicitly set the SQL_DESC_ARRAY_SIZE field in the APD. This situation could arise, for example, if the application has a generic routine that checks for the current "row" of parameters being processed when **SQLExecute** returns SQL_NEED_DATA. This routine is invoked whether or not the SQL_DESC_ARRAY_SIZE is 1 or is greater than 1. To account for this, the Driver Manager will need to define this internal variable whether or not the application has called **SQLSetStmtAttr** to set the SQL_DESC_ARRAY_SIZE field in APD. If SQL_DESC_ARRAY_SIZE has not been set, the Driver Manager has to make sure that this variable contains the value 1 prior to returning from **SQLExecDirect** or **SQLExecute**.  
  
## Error Handling  
 In ODBC *3.x*, calling **SQLFetch** or **SQLFetchScroll** populates the SQL_DESC_ARRAY_STATUS_PTR in the IRD, and the SQL_DIAG_ROW_NUMBER field of a given diagnostic record contains the number of the row in the rowset that this record pertains to. Using this, the application can correlate an error message with a given row position.  
  
 An ODBC *2.x* driver will be unable to provide this functionality. However, it will provide error demarcation with SQLSTATE 01S01 (Error in row). An ODBC *3.x* application that is using **SQLFetch** or **SQLFetchScroll** while going against an ODBC *2.x* driver needs to be aware of this fact. Note also that such an application will be unable to call **SQLGetDiagField** to actually get the SQL_DIAG_ROW_NUMBER field anyway. An ODBC *3.x* application working with an ODBC *2.x* driver will be able to call **SQLGetDiagField** only with a *DiagIdentifier* argument of SQL_DIAG_MESSAGE_TEXT, SQL_DIAG_NATIVE, SQL_DIAG_RETURNCODE, or SQL_DIAG_SQLSTATE. The ODBC *3.x* Driver Manager maintains the diagnostic data structure when working with an ODBC *2.x* driver, but the ODBC *2.x* driver returns only these four fields.  
  
 When an ODBC *2.x* application is working with an ODBC *2.x* driver, if an operation can cause multiple errors to be returned by the Driver Manager, different errors may be returned by the ODBC *3.x* Driver Manager than by the ODBC *2.x* Driver Manager.  
  
## Mappings for Bookmark Operations  
 The ODBC *3.x* Driver Manager performs the following mappings when an ODBC *3.x* application working with an ODBC *2.x* driver performs bookmark operations.  
  
### SQLBindCol  
 When an ODBC *3.x* application working with an ODBC *2.x* driver calls **SQLBindCol** to bind to column 0 with *fCType* equal to SQL_C_VARBOOKMARK, the ODBC *3.x* Driver Manager checks to see whether the *BufferLength* argument is less than 4 or greater than 4, and if so, returns SQLSTATE HY090 (Invalid string or buffer length). If the *BufferLength* argument is equal to 4, the Driver Manager calls **SQLBindCol** in the driver, after replacing *fCType* with SQL_C_BOOKMARK.  
  
### SQLColAttribute  
 When an ODBC *3.x* application working with an ODBC *2.x* driver calls **SQLColAttribute** with the *ColumnNumber* argument set to 0, the Driver Manager returns the *FieldIdentifier* values listed in the following table.  
  
|*FieldIdentifier*|Value|  
|-----------------------|-----------|  
|SQL_DESC_AUTO_UNIQUE_VALUE|SQL_FALSE|  
|SQL_DESC_CASE_SENSITIVE|SQL_FALSE|  
|SQL_DESC_CATALOG_NAME|"" (empty string)|  
|SQL_DESC_CONCISE_TYPE|SQL_BINARY|  
|SQL_DESC_COUNT|The same value returned by **SQLNumResultCols**|  
|SQL_DESC_DATETIME_INTERVAL_CODE|0|  
|SQL_DESC_DISPLAY_SIZE|8|  
|SQL_DESC_FIXED_PREC_SCALE|SQL_FALSE|  
|SQL_DESC_LABEL|"" (empty string)|  
|SQL_DESC_LENGTH|0|  
|SQL_DESC_LITERAL_PREFIX|"" (empty string)|  
|SQL_DESC_LITERAL_SUFFIX|"" (empty string)|  
|SQL_DESC_LOCAL_TYPE_NAME|"" (empty string)|  
|SQL_DESC_NAME|"" (empty string)|  
|SQL_DESC_NULLABLE|SQL_NO_NULLS|  
|SQL_DESC_OCTET_LENGTH|4|  
|SQL_DESC_PRECISION|4|  
|SQL_DESC_SCALE|0|  
|SQL_DESC_SCHEMA_NAME|"" (empty string)|  
|SQL_DESC_SEARCHABLE|SQL_PRED_NONE|  
|SQL_DESC_TABLE_NAME|"" (empty string)|  
|SQL_DESC_TYPE|SQL_BINARY|  
|SQL_DESC_TYPE_NAME|"" (empty string)|  
|SQL_DESC_UNNAMED|SQL_UNNAMED|  
|SQL_DESC_UNSIGNED|SQL_FALSE|  
|SQL_DESC_UPDATEABLE|SQL_ATTR_READ_ONLY|  
  
### SQLDescribeCol  
 When an ODBC *3.x* application working with an ODBC *2.x* driver calls **SQLDescribeCol** with the *ColumnNumber* argument set to 0, the Driver Manager returns the values listed in the following table.  
  
|Buffer|Value|  
|------------|-----------|  
|ColumnName|"" (empty string)|  
|*NameLengthPtr|0|  
|*DataTypePtr|SQL_BINARY|  
|*ColumnSizePtr|4|  
|*DecimalDigitsPtr|0|  
|*NullablePtr|SQL_NO_NULLS|  
  
### SQLGetData  
 When an ODBC *3.x* application working with an ODBC *2.x* driver makes the following call to **SQLGetData** to retrieve a bookmark:  
  
```  
SQLGetData(StatementHandle, 0, SQL_C_VARBOOKMARK, TargetValuePtr, BufferLength, StrLen_or_IndPtr)  
```  
  
 the call is mapped to **SQLGetStmtOption** with an *fOption* of SQL_GET_BOOKMARK, as follows:  
  
```  
SQLGetStmtOption(hstmt, SQL_GET_BOOKMARK, TargetValuePtr)  
```  
  
 where *hstmt* and *pvParam* are set to the values in *StatementHandle* and *TargetValuePtr*, respectively. The bookmark is returned in the buffer pointed to by the *pvParam* (*TargetValuePtr*) argument. The value in the buffer pointed to by the *StrLen_or_IndPtr* argument in the call to **SQLGetData** is set to 4.  
  
 This mapping is necessary to account for the case in which **SQLFetch** was called prior to the call to **SQLGetData** and the ODBC *2.x* driver did not support **SQLExtendedFetch**. In this case, **SQLFetch** would be passed through to the ODBC *2.x* driver, in which case bookmark retrieval is not supported.  
  
 **SQLGetData** cannot be called multiple times in an ODBC *2.x* driver to retrieve a bookmark in parts, so calling **SQLGetData** with the *BufferLength* argument set to a value less than 4 and the *ColumnNumber* argument set to 0 will return SQLSTATE HY090 (Invalid string or buffer length). **SQLGetData** can, however, be called multiple times to retrieve the same bookmark.  
  
### SQLSetStmtAttr  
 When an ODBC *3.x* application working with an ODBC *2.x* driver calls **SQLSetStmtAttr** to set the SQL_ATTR_USE_BOOKMARKS attribute to SQL_UB_VARIABLE, the Driver Manager sets the attribute to SQL_UB_ON in the underlying ODBC *2.x* driver.
