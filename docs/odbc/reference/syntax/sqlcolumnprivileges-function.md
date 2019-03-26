---
title: "SQLColumnPrivileges Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLColumnPrivileges"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLColumnPrivileges"
helpviewer_keywords: 
  - "SQLColumnPrivileges function [ODBC]"
ms.assetid: ef233d9a-6ed5-4986-9d42-5e0b1a79fb6e
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLColumnPrivileges Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLColumnPrivileges** returns a list of columns and associated privileges for the specified table. The driver returns the information as a result set on the specified *StatementHandle*.  
  
## Syntax  
  
```  
  
SQLRETURN SQLColumnPrivileges(  
     SQLHSTMT      StatementHandle,  
     SQLCHAR *     CatalogName,  
     SQLSMALLINT   NameLength1,  
     SQLCHAR *     SchemaName,  
     SQLSMALLINT   NameLength2,  
     SQLCHAR *     TableName,  
     SQLSMALLINT   NameLength3,  
     SQLCHAR *     ColumnName,  
     SQLSMALLINT   NameLength4);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *CatalogName*  
 [Input] Catalog name. If a driver supports names for some catalogs but not for others,such as when the driver retrieves data from different DBMSs, an empty string ("") denotes those catalogs that do not have names. *CatalogName* cannot contain a string search pattern.  
  
 If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *CatalogName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *CatalogName* is an ordinary argument; it is treated literally, and its case is significant. For more information, see [Arguments in Catalog Functions](../../../odbc/reference/develop-app/arguments-in-catalog-functions.md).  
  
 *NameLength1*  
 [Input] Length in characters of **CatalogName*.  
  
 *SchemaName*  
 [Input] Schema name. If a driver supports schemas for some tables but not for others, such as when the driver retrieves data from different DBMSs, an empty string ("") denotes those tables that do not have schemas. *SchemaName* cannot contain a string search pattern.  
  
 If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *SchemaName* is treated as an identifier. If it is SQL_FALSE, *SchemaName* is an ordinary argument; it is treated literally, and its case is significant.  
  
 *NameLength2*  
 [Input] Length in characters of **SchemaName*.  
  
 *TableName*  
 [Input] Table name. This argument cannot be a null pointer. *TableName* cannot contain a string search pattern.  
  
 If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *TableName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *TableName* is an ordinary argument; it is treated literally, and its case is significant.  
  
 *NameLength3*  
 [Input] Length in characters of **TableName*.  
  
 *ColumnName*  
 [Input] String search pattern for column names.  
  
 If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *ColumnName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *ColumnName* is a pattern value argument; it is treated literally, and its case is significant.  
  
 *NameLength4*  
 [Input] Length in characters of **ColumnName*.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLColumnPrivileges** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value may be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values commonly returned by **SQLColumnPrivileges** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|24000|Invalid cursor state|A cursor was open on the *StatementHandle,* and **SQLFetch** or **SQLFetchScroll** had been called. This error is returned by the Driver Manager if **SQLFetch** or **SQLFetchScroll** has not returned SQL_NO_DATA, and is returned by the driver if **SQLFetch** or **SQLFetchScroll** has returned SQL_NO_DATA.<br /><br /> A cursor was open on the *StatementHandle*, but **SQLFetch** or **SQLFetchScroll** had not been called.|  
|40001|Serialization failure|The transaction was rolled back due to a resource deadlock with another transaction.|  
|40003|Statement completion unknown|The associated connection failed during the execution of this function, and the state of the transaction cannot be determined.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation error|The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*. Then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY009|Invalid use of null pointer|The *TableName* argument was a null pointer.<br /><br /> The SQL_ATTR_METADATA_ID statement attribute was set to SQL_TRUE, the *CatalogName* argument was a null pointer, and the SQL_CATALOG_NAME *InfoType* returns that catalog names are supported.<br /><br /> (DM) The SQL_ATTR_METADATA_ID statement attribute was set to SQL_TRUE, and the *SchemaName* or *ColumnName* argument was a null pointer.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The value of one of the name length arguments was less than 0 but not equal to SQL_NTS.|  
|||The value of one of the name length arguments exceeded the maximum length value for the corresponding name. (See "Comments.")|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|A catalog name was specified, and the driver or data source does not support catalogs.<br /><br /> A schema name was specified, and the driver or data source does not support schemas.<br /><br /> A string search pattern was specified for the column name, and the data source does not support search patterns for that argument.<br /><br /> The combination of the current settings of the SQL_CONCURRENCY and SQL_CURSOR_TYPE statement attributes was not supported by the driver or data source.<br /><br /> The SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_VARIABLE, and the SQL_ATTR_CURSOR_TYPE statement attribute was set to a cursor type for which the driver does not support bookmarks.|  
|HYT00|Timeout expired|The query timeout period expired before the data source returned the result set. The timeout period is set through **SQLSetStmtAttr**, SQL_ATTR_QUERY_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
 **SQLColumnPrivileges** returns the results as a standard result set, ordered by TABLE_CAT, TABLE_SCHEM, TABLE_NAME, COLUMN_NAME, and PRIVILEGE.  
  
> [!NOTE]  
>  **SQLColumnPrivileges** might not return privileges for all columns. For example, a driver might not return information about privileges for pseudo-columns, such as Oracle ROWID. Applications can use any valid column, regardless of whether it is returned by **SQLColumnPrivileges**.  
  
 The lengths of VARCHAR columns are not shown in the table; the actual lengths depend on the data source. To determine the actual lengths of the CATALOG_NAME, SCHEMA_NAME, TABLE_NAME, and COLUMN_NAME columns, an application can call **SQLGetInfo** with the SQL_MAX_CATALOG_NAME_LEN, SQL_MAX_SCHEMA_NAME_LEN, SQL_MAX_TABLE_NAME_LEN, and SQL_MAX_COLUMN_NAME_LEN options.  
  
> [!NOTE]  
>  For more information about the general use, arguments, and returned data of ODBC catalog functions, see [Catalog Functions](../../../odbc/reference/develop-app/catalog-functions.md).  
  
 The following columns have been renamed for ODBC 3.*x*. The column name changes do not affect backward compatibility because applications bind by column number.  
  
|ODBC 2.0 column|ODBC 3.*x* column|  
|---------------------|-----------------------|  
|TABLE_QUALIFIER|TABLE_CAT|  
|TABLE_OWNER|TABLE_SCHEM|  
  
 The following table lists the columns in the result set. Additional columns beyond column 8 (IS_GRANTABLE) can be defined by the driver. An application should gain access to driver-specific columns by counting down from the end of the result set rather than specifying an explicit ordinal position. For more information, see [Data Returned by Catalog Functions](../../../odbc/reference/develop-app/data-returned-by-catalog-functions.md).  
  
|Column name|Column number|Data type|Comments|  
|-----------------|-------------------|---------------|--------------|  
|TABLE_CAT  (ODBC 1.0)|1|Varchar|Catalog identifier; NULL if not applicable to the data source. If a driver supports catalogs for some tables but not for others, such as when the driver retrieves data from different DBMSs, it returns an empty string ("") for those tables that do not have catalogs.|  
|TABLE_SCHEM (ODBC 1.0)|2|Varchar|Schema identifier; NULL if not applicable to the data source. If a driver supports schemas for some tables but not for others, such as when the driver retrieves data from different DBMSs, it returns an empty string ("") for those tables that do not have schemas.|  
|TABLE_NAME (ODBC 1.0)|3|Varchar not NULL|Table identifier.|  
|COLUMN_NAME (ODBC 1.0)|4|Varchar not NULL|Column name. The driver returns an empty string for a column that does not have a name.|  
|GRANTOR (ODBC 1.0)|5|Varchar|Name of the user who granted the privilege; NULL if not applicable to the data source.<br /><br /> For all rows in which the value in the GRANTEE column is the owner of the object, the GRANTOR column will be "_SYSTEM".|  
|GRANTEE (ODBC 1.0)|6|Varchar not NULL|Name of the user to whom the privilege was granted.|  
|PRIVILEGE (ODBC 1.0)|7|Varchar not NULL|Identifies the column privilege. May be one of the following (or others supported by the data source when implementation-defined):<br /><br /> SELECT: The grantee is permitted to retrieve data for the column.<br /><br /> INSERT: The grantee is permitted to provide data for the column in new rows that are inserted into the associated table.<br /><br /> UPDATE: The grantee is permitted to update data in the column.<br /><br /> REFERENCES: The grantee is permitted to refer to the column within a constraint (for example, a unique, referential, or table check constraint).|  
|IS_GRANTABLE (ODBC 1.0)|8|Varchar|Indicates whether the grantee is permitted to grant the privilege to other users; "YES", "NO", or "NULL" if unknown or not applicable to the data source.<br /><br /> A privilege is either grantable or not grantable, but not both. The result set returned by **SQLColumnPrivileges** will never contain two rows for which all columns except the IS_GRANTABLE column contain the same value.|  
  
## Code Example  
 For a code example of a similar function, see [SQLColumns Function](../../../odbc/reference/syntax/sqlcolumns-function.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a column in a result set|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Returning the columns in a table or tables|[SQLColumns Function](../../../odbc/reference/syntax/sqlcolumns-function.md)|  
|Fetching a block of data or scrolling through a result set|[SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|  
|Fetching multiple rows of data|[SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md)|  
|Returning privileges for a table or tables|[SQLTablePrivileges Function](../../../odbc/reference/syntax/sqltableprivileges-function.md)|  
|Returning a list of tables in a data source|[SQLTables Function](../../../odbc/reference/syntax/sqltables-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
