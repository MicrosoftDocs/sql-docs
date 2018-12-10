---
title: "SQLGetDiagField Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLGetDiagField"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLGetDiagField"
helpviewer_keywords: 
  - "SQLGetDiagField function [ODBC]"
ms.assetid: 1dbc4398-97a8-4585-bb77-1f7ea75e24c4
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetDiagField Function
**Conformance**  
 Version Introduced: ODBC 3.0 Standards Compliance: ISO 92  
  
 **Summary**  
 **SQLGetDiagField** returns the current value of a field of a record of the diagnostic data structure (associated with a specified handle) that contains error, warning, and status information.  
  
## Syntax  
  
```  
  
SQLRETURN SQLGetDiagField(  
     SQLSMALLINT     HandleType,  
     SQLHANDLE       Handle,  
     SQLSMALLINT     RecNumber,  
     SQLSMALLINT     DiagIdentifier,  
     SQLPOINTER      DiagInfoPtr,  
     SQLSMALLINT     BufferLength,  
     SQLSMALLINT *   StringLengthPtr);  
```  
  
## Arguments  
 *HandleType*  
 [Input] A handle type identifier that describes the type of handle for which diagnostics are required. Must be one of the following:  
  
-   SQL_HANDLE_DBC  
  
-   SQL_HANDLE_DBC_INFO_TOKEN  
  
-   SQL_HANDLE_DESC  
  
-   SQL_HANDLE_ENV  
  
-   SQL_HANDLE_STMT  
  
 SQL_HANDLE_DBC_INFO_TOKEN handle is used only by the Driver Manager and driver. Applications should not use this handle type. For more information about SQL_HANDLE_DBC_INFO_TOKEN, see [Developing Connection-Pool Awareness in an ODBC Driver](../../../odbc/reference/develop-driver/developing-connection-pool-awareness-in-an-odbc-driver.md).  
  
 *Handle*  
 [Input] A handle for the diagnostic data structure, of the type indicated by *HandleType*. If *HandleType* is SQL_HANDLE_ENV, *Handle* can be either a shared or an unshared environment handle.  
  
 *RecNumber*  
 [Input] Indicates the status record from which the application seeks information. Status records are numbered from 1. If the *DiagIdentifier* argument indicates any field of the diagnostics header, *RecNumber* is ignored. If not, it should be more than 0.  
  
 *DiagIdentifier*  
 [Input] Indicates the field of the diagnostic whose value is to be returned. For more information, see the "*DiagIdentifier* Argument" section in "Comments."  
  
 *DiagInfoPtr*  
 [Output] Pointer to a buffer in which to return the diagnostic information. The data type depends on the value of *DiagIdentifier*. If *DiagInfoPtr* is an integer type, applications should use a buffer of SQLULEN and initialize the value to 0 before calling this function, as some drivers may only write the lower 32-bit or 16-bit of a buffer and leave the higher-order bit unchanged.  
  
 If *DiagInfoPtr* is NULL, *StringLengthPtr* will still return the total number of bytes (excluding the null-termination character for character data) available to return in the buffer pointed to by *DiagInfoPtr*.  
  
 *BufferLength*  
 [Input] If *DiagIdentifier* is an ODBC-defined diagnostic and *DiagInfoPtr* points to a character string or a binary buffer, this argument should be the length of \**DiagInfoPtr*. If *DiagIdentifier* is an ODBC-defined field and \**DiagInfoPtr* is an integer, *BufferLength* is ignored. If the value in *\*DiagInfoPtr* is a Unicode string (when calling **SQLGetDiagFieldW**), the *BufferLength* argument must be an even number.  
  
 If *DiagIdentifier* is a driver-defined field, the application indicates the nature of the field to the Driver Manager by setting the *BufferLength* argument. *BufferLength* can have the following values:  
  
-   If *DiagInfoPtr* is a pointer to a character string, *BufferLength* is the length of the string or SQL_NTS.  
  
-   If *DiagInfoPtr* is a pointer to a binary buffer, the application places the result of the SQL_LEN_BINARY_ATTR(*length*) macro in *BufferLength*. This places a negative value in *BufferLength*.  
  
-   If *DiagInfoPtr* is a pointer to a value other than a character string or binary string, *BufferLength* should have the value SQL_IS_POINTER.  
  
-   If *\*DiagInfoPtr* contains a fixed-length data type, *BufferLength* is SQL_IS_INTEGER, SQL_IS_UINTEGER, SQL_IS_SMALLINT, or SQL_IS_USMALLINT, as appropriate.  
  
 *StringLengthPtr*  
 [Output] Pointer to a buffer in which to return the total number of bytes (excluding the number of bytes required for the null-termination character) available to return in \**DiagInfoPtr*, for character data. If the number of bytes available to return is greater than or equal to *BufferLength*, the text in \**DiagInfoPtr* is truncated to *BufferLength* minus the length of a null-termination character.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, SQL_INVALID_HANDLE, or SQL_NO_DATA.  
  
## Diagnostics  
 **SQLGetDiagField** does not post diagnostic records for itself. It uses the following return values to report the outcome of its own execution:  
  
-   SQL_SUCCESS: The function successfully returned diagnostic information.  
  
-   SQL_SUCCESS_WITH_INFO: \**DiagInfoPtr* was too small to hold the requested diagnostic field. Therefore, the data in the diagnostic field was truncated. To determine that a truncation occurred, the application must compare *BufferLength* to the actual number of bytes available, which is written to **StringLengthPtr*.  
  
-   SQL_INVALID_HANDLE: The handle indicated by *HandleType* and *Handle* was not a valid handle.  
  
-   SQL_ERROR: One of the following occurred:  
  
    -   *The DiagIdentifier* argument was not one of the valid values.  
  
    -   *The DiagIdentifier* argument was SQL_DIAG_CURSOR_ROW_COUNT, SQL_DIAG_DYNAMIC_FUNCTION, SQL_DIAG_DYNAMIC_FUNCTION_CODE, or SQL_DIAG_ROW_COUNT, but *Handle* was not a statement handle. (The Driver Manager returns this diagnostic.)  
  
    -   *The RecNumber* argument was negative or 0 when *DiagIdentifier* indicated a field from a diagnostic record. *RecNumber* is ignored for header fields.  
  
    -   The value requested was a character string and *BufferLength* was less than zero.  
  
    -   When using asynchronous notification, the asynchronous operation on the handle was not complete.  
  
-   SQL_NO_DATA: *RecNumber* was greater than the number of diagnostic records that existed for the handle specified in *Handle.* The function also returns SQL_NO_DATA for any positive *RecNumber* if there are no diagnostic records for *Handle*.  
  
## Comments  
 An application typically calls **SQLGetDiagField** to accomplish one of three goals:  
  
1.  To obtain specific error or warning information when a function call has returned SQL_ERROR or SQL_SUCCESS_WITH_INFO (or SQL_NEED_DATA for the **SQLBrowseConnect** function).  
  
2.  To determine the number of rows in the data source that were affected when insert, delete, or update operations were performed with a call to **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** (from the SQL_DIAG_ROW_COUNT header field), or to determine the number of rows that exist in the current open cursor, if the driver can provide this information (from the SQL_DIAG_CURSOR_ROW_COUNT header field).  
  
3.  To determine which function was executed by a call to **SQLExecDirect** or **SQLExecute** (from the SQL_DIAG_DYNAMIC_FUNCTION and SQL_DIAG_DYNAMIC_FUNCTION_CODE header fields).  
  
 Any ODBC function can post zero or more diagnostic records every time that it is called, so an application can call **SQLGetDiagField** after any ODBC function call. There is no limit to the number of diagnostic records that can be stored at any one time. **SQLGetDiagField** retrieves only the diagnostic information most recently associated with the diagnostic data structure specified in the *Handle* argument. If the application calls an ODBC function other than **SQLGetDiagField** or **SQLGetDiagRec**, any diagnostic information from a previous call with the same handle is lost.  
  
 An application can scan all diagnostic records by incrementing *RecNumber*, as long as **SQLGetDiagField** returns SQL_SUCCESS. The number of status records is indicated in the SQL_DIAG_NUMBER header field. Calls to **SQLGetDiagField** are nondestructive to the header and record fields. The application can call **SQLGetDiagField** again later to retrieve a field from a record, as long as a function other than the diagnostic functions has not been called in the interim, which would post records on the same handle.  
  
 An application can call **SQLGetDiagField** to return any diagnostic field at any time, except for SQL_DIAG_CURSOR_ROW_COUNT or SQL_DIAG_ROW_COUNT, which will return SQL_ERROR if *Handle* is not a statement handle. If any other diagnostic field is undefined, the call to **SQLGetDiagField** will return SQL_SUCCESS (provided no other diagnostic is encountered) and an undefined value is returned for the field.  
  
 For more information, see [Using SQLGetDiagRec and SQLGetDiagField](../../../odbc/reference/develop-app/using-sqlgetdiagrec-and-sqlgetdiagfield.md) and [Implementing SQLGetDiagRec and SQLGetDiagField](../../../odbc/reference/develop-app/implementing-sqlgetdiagrec-and-sqlgetdiagfield.md).  
  
 Calling an API other than the one that's being executed asynchronously will generate HY010 "Function sequence error". However, the error record cannot be retrieved before the asynchronous operation completes.  
  
## HandleType Argument  
 Each handle type can have diagnostic information associated with it. The *HandleType* argument indicates the handle type of *Handle*.  
  
 Some header and record fields cannot be returned for environment, connection, statement, and descriptor handles. Those handles for which a field is not applicable are indicated in the "Header Fields" and "Record Fields" sections following.  
  
 If *HandleType* is SQL_HANDLE_ENV, *Handle* can be either a shared or unshared environment handle.  
  
 No driver-specific header diagnostic fields should be associated with an environment handle.  
  
 The only diagnostic header fields that are defined for a descriptor handle are SQL_DIAG_NUMBER and SQL_DIAG_RETURNCODE.  
  
## DiagIdentifier Argument  
 This argument indicates the identifier of the field required from the diagnostic data structure. If *RecNumber* is greater than or equal to 1, the data in the field describes the diagnostic information returned by a function. If *RecNumber* is 0, the field is in the header of the diagnostic data structure and therefore contains data pertaining to the function call that returned the diagnostic information, not to the specific information.  
  
 Drivers can define driver-specific header and record fields in the diagnostic data structure.  
  
 An ODBC 3*.x* application working with an ODBC 2*.x* driver will be able to call **SQLGetDiagField** only with a *DiagIdentifier* argument of SQL_DIAG_CLASS_ORIGIN, SQL_DIAG_CLASS_SUBCLASS_ORIGIN, SQL_DIAG_CONNECTION_NAME, SQL_DIAG_MESSAGE_TEXT, SQL_DIAG_NATIVE, SQL_DIAG_NUMBER, SQL_DIAG_RETURNCODE, SQL_DIAG_SERVER_NAME, or SQL_DIAG_SQLSTATE. All other diagnostic fields will return SQL_ERROR.  
  
## Header Fields  
 The header fields listed in the following table can be included in the *DiagIdentifier* argument.  
  
|DiagIdentifier|Return type|Returns|  
|--------------------|-----------------|-------------|  
|SQL_DIAG_CURSOR_ROW_COUNT|SQLLEN|This field contains the count of rows in the cursor. Its semantics depend on the **SQLGetInfo** information types SQL_DYNAMIC_CURSOR_ATTRIBUTES2, SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES2, SQL_KEYSET_CURSOR_ATTRIBUTES2, and SQL_STATIC_CURSOR_ATTRIBUTES2, which indicate which row counts are available for each cursor type (in the SQL_CA2_CRC_EXACT and SQL_CA2_CRC_APPROXIMATE bits).<br /><br /> The contents of this field are defined only for statement handles and only after **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** has been called. Calling **SQLGetDiagField** with a *DiagIdentifier* of SQL_DIAG_CURSOR_ROW_COUNT on other than a statement handle will return SQL_ERROR.|  
|SQL_DIAG_DYNAMIC_FUNCTION|SQLCHAR *|This is a string that describes the SQL statement that the underlying function executed. (See "Values of the Dynamic Function fields," later in this section, for specific values.) The contents of this field are defined only for statement handles and only after a call to **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults**. Calling **SQLGetDiagField** with a *DiagIdentifier* of SQL_DIAG_DYNAMIC_FUNCTION on other than a statement handle will return SQL_ERROR. The value of this field is undefined before a call to **SQLExecute** or **SQLExecDirect**.|  
|SQL_DIAG_DYNAMIC_FUNCTION_CODE|SQLINTEGER|This is a numeric code that describes the SQL statement that was executed by the underlying function. (See "Values of the Dynamic Function Fields," later in this section, for specific value.) The contents of this field are defined only for statement handles and only after a call to **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults**. Calling **SQLGetDiagField** with a *DiagIdentifier* of SQL_DIAG_DYNAMIC_FUNCTION_CODE on other than a statement handle will return SQL_ERROR. The value of this field is undefined before a call to **SQLExecute** or **SQLExecDirect**.|  
|SQL_DIAG_NUMBER|SQLINTEGER|The number of status records that are available for the specified handle.|  
|SQL_DIAG_RETURNCODE|SQLRETURN|Return code returned by the function. For a list of return codes, see [Return Codes](../../../odbc/reference/develop-app/return-codes-odbc.md). The driver does not have to implement SQL_DIAG_RETURNCODE; it is always implemented by the Driver Manager. If no function has yet been called on the *Handle*, SQL_SUCCESS will be returned for SQL_DIAG_RETURNCODE.|  
|SQL_DIAG_ROW_COUNT|SQLLEN|The number of rows affected by an insert, delete, or update performed by **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos**. It is driver-defined after a *cursor specification* has been executed. The contents of this field are defined only for statement handles. Calling **SQLGetDiagField** with a *DiagIdentifier* of SQL_DIAG_ROW_COUNT on other than a statement handle will return SQL_ERROR. The data in this field is also returned in the *RowCountPtr* argument of **SQLRowCount**. The data in this field is reset after every nondiagnostic function call, whereas the row count returned by **SQLRowCount** remains the same until the statement is set back to the prepared or allocated state.|  
  
## Record Fields  
 The record fields listed in the following table can be included in the *DiagIdentifier* argument.  
  
|DiagIdentifier|Return type|Returns|  
|--------------------|-----------------|-------------|  
|SQL_DIAG_CLASS_ORIGIN|SQLCHAR *|A string that indicates the document that defines the class portion of the SQLSTATE value in this record. Its value is "ISO 9075" for all SQLSTATEs defined by Open Group and ISO call-level interface. For ODBC-specific SQLSTATEs (all those whose SQLSTATE class is "IM"), its value is "ODBC 3.0".|  
|SQL_DIAG_COLUMN_NUMBER|SQLINTEGER|If the SQL_DIAG_ROW_NUMBER field is a valid row number in a rowset or a set of parameters, this field contains the value that represents the column number in the result set or the parameter number in the set of parameters. Result set column numbers always start at 1; if this status record pertains to a bookmark column, the field can be zero. Parameter numbers start at 1. It has the value SQL_NO_COLUMN_NUMBER if the status record is not associated with a column number or parameter number. If the driver cannot determine the column number or parameter number that this record is associated with, this field has the value SQL_COLUMN_NUMBER_UNKNOWN.<br /><br /> The contents of this field are defined only for statement handles.|  
|SQL_DIAG_CONNECTION_NAME|SQLCHAR *|A string that indicates the name of the connection that the diagnostic record relates to. This field is driver-defined. For diagnostic data structures associated with the environment handle and for diagnostics that do not relate to any connection, this field is a zero-length string.|  
|SQL_DIAG_MESSAGE_TEXT|SQLCHAR *|An informational message on the error or warning. This field is formatted as described in [Diagnostic Messages](../../../odbc/reference/develop-app/diagnostic-messages.md). There is no maximum length to the diagnostic message text.|  
|SQL_DIAG_NATIVE|SQLINTEGER|A driver/data source-specific native error code. If there is no native error code, the driver returns 0.|  
|SQL_DIAG_ROW_NUMBER|SQLLEN|This field contains the row number in the rowset, or the parameter number in the set of parameters, with which the status record is associated. Row numbers and parameter numbers start with 1. This field has the value SQL_NO_ROW_NUMBER if this status record is not associated with a row number or parameter number. If the driver cannot determine the row number or parameter number that this record is associated with, this field has the value SQL_ROW_NUMBER_UNKNOWN.<br /><br /> The contents of this field are defined only for statement handles.|  
|SQL_DIAG_SERVER_NAME|SQLCHAR *|A string that indicates the server name that the diagnostic record relates to. It is the same as the value returned for a call to **SQLGetInfo** with the SQL_DATA_SOURCE_NAME option. For diagnostic data structures associated with the environment handle and for diagnostics that do not relate to any server, this field is a zero-length string.|  
|SQL_DIAG_SQLSTATE|SQLCHAR *|A five-character SQLSTATE diagnostic code. For more information, see [SQLSTATEs](../../../odbc/reference/develop-app/sqlstates.md).|  
|SQL_DIAG_SUBCLASS_ORIGIN|SQLCHAR *|A string with the same format and valid values as SQL_DIAG_CLASS_ORIGIN, that identifies the defining portion of the subclass portion of the SQLSTATE code. The ODBC-specific SQLSTATES for which "ODBC 3.0" is returned include the following:<br /><br /> 01S00, 01S01, 01S02, 01S06, 01S07, 07S01, 08S01, 21S01, 21S02, 25S01, 25S02, 25S03, 42S01, 42S02, 42S11, 42S12, 42S21, 42S22, HY095, HY097, HY098, HY099, HY100, HY101, HY105, HY107, HY109, HY110, HY111, HYT00, HYT01, IM001, IM002, IM003, IM004, IM005, IM006, IM007, IM008, IM010, IM011, IM012.|  
  
## Values of the Dynamic Function Fields  
 The following table describes the values of SQL_DIAG_DYNAMIC_FUNCTION and SQL_DIAG_DYNAMIC_FUNCTION_CODE that apply to each type of SQL statement executed by a call to **SQLExecute** or **SQLExecDirect**. The driver can add driver-defined values to those listed.  
  
|SQL statement<br /><br /> executed|Value of<br /><br /> SQL_DIAG_DYNAMIC_FUNCTION|Value of<br /><br /> SQL_DIAG_DYNAMIC_FUNCTION_CODE|  
|--------------------------------|-----------------------------------------------|-----------------------------------------------------|  
|*alter-domain-statement*|"ALTER DOMAIN"|SQL_DIAG_ALTER_DOMAIN|  
|*alter-table-statement*|"ALTER TABLE"|SQL_DIAG_ALTER_TABLE|  
|*assertion-definition*|"CREATE ASSERTION"|SQL_DIAG_CREATE_ASSERTION|  
|*character-set-definition*|"CREATE CHARACTER SET"|SQL_DIAG_CREATE_CHARACTER_SET|  
|*collation-definition*|"CREATE COLLATION"|SQL_DIAG_CREATE_COLLATION|  
|*create-index-statement*|"CREATE INDEX"|SQL_DIAG_CREATE_INDEX|  
|*create-table-statement*|"CREATE TABLE"|SQL_DIAG_CREATE_TABLE|  
|*create-view-statement*|"CREATE VIEW"|SQL_DIAG_CREATE_VIEW|  
|*cursor-specification*|"SELECT CURSOR"|SQL_DIAG_SELECT_CURSOR|  
|*delete-statement-positioned*|"DYNAMIC DELETE CURSOR"|SQL_DIAG_DYNAMIC_DELETE_CURSOR|  
|*delete-statement-searched*|"DELETE WHERE"|SQL_DIAG_DELETE_WHERE|  
n-definition*|"CREATE DOMAIN"|SQL_DIAG_CREATE_DOMAIN|  
|*drop-assertion-statement*|"DROP ASSERTION"|SQL_DIAG_DROP_ASSERTION|  
|*drop-character-set-stmt*|"DROP CHARACTER SET"|SQL_DIAG_DROP_CHARACTER_SET|  
|*drop-collation-statement*|"DROP COLLATION"|SQL_DIAG_DROP_COLLATION|  
|*drop-domain-statement*|"DROP DOMAIN"|SQL_DIAG_DROP_DOMAIN|  
|*drop-index-statement*|"DROP INDEX"|SQL_DIAG_DROP_INDEX|  
|*drop-schema-statement*|"DROP SCHEMA"|SQL_DIAG_DROP_SCHEMA|  
|*drop-table-statement*|"DROP TABLE"|SQL_DIAG_DROP_TABLE|  
|*drop-translation-statement*|"DROP TRANSLATION"|SQL_DIAG_DROP_TRANSLATION|  
|*drop-view-statement*|"DROP VIEW"|SQL_DIAG_DROP_VIEW|  
-statement*|"GRANT"|SQL_DIAG_GRANT|  
|*insert-statement*|"INSERT"|SQL_DIAG_INSERT|  
|*ODBC-procedure-extension*|"CALL"|SQL_DIAG_ CALL|  
|*revoke-statement*|"REVOKE"|SQL_DIAG_REVOKE|  
|*schema-definition*|"CREATE SCHEMA"|SQL_DIAG_CREATE_SCHEMA|  
|*translation-definition*|"CREATE TRANSLATION"|SQL_DIAG_CREATE_TRANSLATION|  
|*update-statement-positioned*|"DYNAMIC UPDATE CURSOR"|SQL_DIAG_DYNAMIC_UPDATE_CURSOR|  
|*update-statement-searched*|"UPDATE WHERE"|SQL_DIAG_UPDATE_WHERE|  
|Unknown|*empty string*|SQL_DIAG_UNKNOWN_STATEMENT|  
  
## Sequence of Status Records  
 Status records are positioned in a sequence based on row number and the type of the diagnostic. The Driver Manager determines the final order in which to return status records that it generates. The driver determines the final order in which to return status records that it generates.  
  
 If diagnostic records are posted by both the Driver Manager and the driver, the Driver Manager is responsible for ordering them.  
  
 If there are two or more status records, the sequence of the records is determined first by row number. The following rules apply to determining the sequence of diagnostic records by row:  
  
-   Records that do not correspond to any row appear in front of records that correspond to a particular row, because SQL_NO_ROW_NUMBER is defined to be -1.  
  
-   Records for which the row number is unknown appear in front of all other records, because SQL_ROW_NUMBER_UNKNOWN is defined to be -2.  
  
-   For all records that pertain to specific rows, records are sorted by the value in the SQL_DIAG_ROW_NUMBER field. All errors and warnings of the first row affected are listed, and then all errors and warnings of the next row affected, and so on.  
  
> [!NOTE]
>  The ODBC 3*.x* Driver Manager does not order status records in the diagnostic queue if SQLSTATE 01S01 (Error in row) is returned by an ODBC 2*.x* driver or if SQLSTATE 01S01 (Error in row) is returned by an ODBC 3*.x* driver when **SQLExtendedFetch** is called or **SQLSetPos** is called on a cursor that has been positioned with **SQLExtendedFetch**.  
  
 Within each row, or for all those records that do not correspond to a row or for which the row number is unknown, or for all those records with a row number equal to SQL_NO_ROW_NUMBER, the first record listed is determined by using a set of sorting rules. After the first record, the order of the other records affecting a row is undefined. An application cannot assume that errors precede warnings after the first record. Applications should scan the complete diagnostic data structure to obtain complete information about an unsuccessful call to a function.  
  
 The following rules are used to determine the first record within a row. The record with the highest rank is the first record. The source of a record (Driver Manager, driver, gateway, and so on) is not considered when ranking records.  
  
-   **Errors** Status records that describe errors have the highest rank. The following rules are applied to sort errors:  
  
    -   Records that indicate a transaction failure or possible transaction failure outrank all other records.  
  
    -   If two or more records describe the same error condition, then SQLSTATEs defined by the Open Group CLI specification (classes 03 through HZ) outrank ODBC- and driver-defined SQLSTATEs.  
  
-   **Implementation-defined No Data Values** Status records that describe driver-defined No Data values (class 02) have the second highest rank.  
  
-   **Warnings** Status records that describe warnings (class 01) have the lowest rank. If two or more records describe the same warning condition, then warning SQLSTATEs defined by the Open Group CLI specification outrank ODBC-defined and driver-defined SQLSTATEs.  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Obtaining multiple fields of a diagnostic data structure|[SQLGetDiagRec Function](../../../odbc/reference/syntax/sqlgetdiagrec-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
