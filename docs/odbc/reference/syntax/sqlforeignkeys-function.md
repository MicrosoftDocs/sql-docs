---
description: "SQLForeignKeys Function"
title: "SQLForeignKeys Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLForeignKeys"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLForeignKeys"
helpviewer_keywords: 
  - "SQLForeignKeys function [ODBC]"
ms.assetid: 07f3f645-f643-4d39-9a10-70a72f24e608
author: David-Engel
ms.author: v-davidengel
---
# SQLForeignKeys Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLForeignKeys** can return:  
  
-   A list of foreign keys in the specified table (columns in the specified table that refer to primary keys in other tables).  
  
-   A list of foreign keys in other tables that refer to the primary key in the specified table.  
  
 The driver returns each list as a result set on the specified statement.  
  
## Syntax  
  
```cpp  
  
SQLRETURN SQLForeignKeys(  
     SQLHSTMT       StatementHandle,  
     SQLCHAR *      PKCatalogName,  
     SQLSMALLINT    NameLength1,  
     SQLCHAR *      PKSchemaName,  
     SQLSMALLINT    NameLength2,  
     SQLCHAR *      PKTableName,  
     SQLSMALLINT    NameLength3,  
     SQLCHAR *      FKCatalogName,  
     SQLSMALLINT    NameLength4,  
     SQLCHAR *      FKSchemaName,  
     SQLSMALLINT    NameLength5,  
     SQLCHAR *      FKTableName,  
     SQLSMALLINT    NameLength6);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *PKCatalogName*  
 [Input] Primary key table catalog name. If a driver supports catalogs for some tables but not for others, such as when the driver retrieves data from different DBMSs, an empty string ("") denotes those tables that do not have catalogs. *PKCatalogName* cannot contain a string search pattern.  
  
 If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *PKCatalogName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *PKCatalogName* is an ordinary argument; it is treated literally, and its case is significant. For more information, see [Arguments in Catalog Functions](../../../odbc/reference/develop-app/arguments-in-catalog-functions.md).  
  
 *NameLength1*  
 [Input] Length of **PKCatalogName*, in characters.  
  
 *PKSchemaName*  
 [Input] Primary key table schema name. If a driver supports schemas for some tables but not for others, such as when the driver retrieves data from different DBMSs, an empty string ("") denotes those tables that do not have schemas. *PKSchemaName* cannot contain a string search pattern.  
  
 If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *PKSchemaName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *PKSchemaName* is an ordinary argument; it is treated literally, and its case is significant.  
  
 *NameLength2*  
 [Input] Length of **PKSchemaName*, in characters.  
  
 *PKTableName*  
 [Input] Primary key table name. *PKTableName* cannot contain a string search pattern.  
  
 If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *PKTableName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *PKTableName* is an ordinary argument; it is treated literally, and its case is significant.  
  
 *NameLength3*  
 [Input] Length of **PKTableName*, in characters.  
  
 *FKCatalogName*  
 [Input] Foreign key table catalog name. If a driver supports catalogs for some tables but not for others, such as when the driver retrieves data from different DBMSs, an empty string ("") denotes those tables that do not have catalogs. *FKCatalogName* cannot contain a string search pattern.  
  
 If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *FKCatalogName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *FKCatalogName* is an ordinary argument; it is treated literally, and its case is significant.  
  
 *NameLength4*  
 [Input] Length of **FKCatalogName*, in characters.  
  
 *FKSchemaName*  
 [Input] Foreign key table schema name. If a driver supports schemas for some tables but not for others, such as when the driver retrieves data from different DBMSs, an empty string ("") denotes those tables that do not have schemas. *FKSchemaName* cannot contain a string search pattern.  
  
 If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *FKSchemaName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *FKSchemaName* is an ordinary argument; it is treated literally, and its case is significant.  
  
 *NameLength5*  
 [Input] Length of **FKSchemaName*, in characters.  
  
 *FKTableName*  
 [Input] Foreign key table name. *FKTableName* cannot contain a string search pattern.  
  
 If the SQL_ATTR_METADATA_ID statement attribute is set to SQL_TRUE, *FKTableName* is treated as an identifier and its case is not significant. If it is SQL_FALSE, *FKTableName* is an ordinary argument; it is treated literally, and its case is significant.  
  
 *NameLength6*  
 [Input] Length of **FKTableName*, in characters.  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_STILL_EXECUTING, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLForeignKeys** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values typically returned by **SQLForeignKeys** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was connected failed before the function completed processing.|  
|24000|Invalid cursor state|A cursor was open on the *StatementHandle*, and **SQLFetch** or **SQLFetchScroll** had been called. This error is returned by the Driver Manager if **SQLFetch** or **SQLFetchScroll** has not returned SQL_NO_DATA, and is returned by the driver if **SQLFetch** or **SQLFetchScroll** has returned SQL_NO_DATA.<br /><br /> A cursor was open on the *StatementHandle*, but **SQLFetch** or **SQLFetchScroll** had not been called.|  
|40001|Serialization failure|The transaction was rolled back because of a resource deadlock with another transaction.|  
|40003|Statement completion unknown|The associated connection failed during the execution of this function, and the state of the transaction cannot be determined.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|The driver was unable to allocate memory that is required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle*, and then the function was called again on the *StatementHandle*.<br /><br /> The function was called, and before it completed execution, **SQLCancel** or **SQLCancelHandle** was called on the *StatementHandle* from a different thread in a multithread application.|  
|HY009|Invalid use of null pointer|(DM) The arguments *PKTableName* and *FKTableName* were both null pointers.<br /><br /> The SQL_ATTR_METADATA_ID statement attribute was set to SQL_TRUE, the *FKCatalogName* or *PKCatalogName* argument was a null pointer, and the SQL_CATALOG_NAME *InfoType* returns that catalog names are supported.<br /><br /> (DM) The SQL_ATTR_METADATA_ID statement attribute was set to SQL_TRUE, and the *FKSchemaName*, *PKSchemaName*, *FKTableName*, or *PKTableName* argument was a null pointer.|  
|HY010|Function sequence error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the SQLForeignKeys function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The value of one of the name length arguments was less than 0 but not equal to SQL_NTS.|  
|||The value of one of the name length arguments exceeded the maximum length value for the corresponding name. (See "Comments.")|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|A catalog name was specified, and the driver or data source does not support catalogs.<br /><br /> A schema name was specified, and the driver or data source does not support schemas.|  
|||The combination of the current settings of the SQL_ATTR_CONCURRENCY and SQL_ATTR_CURSOR_TYPE statement attributes was not supported by the driver or data source.<br /><br /> The SQL_ATTR_USE_BOOKMARKS statement attribute was set to SQL_UB_VARIABLE, and the SQL_ATTR_CURSOR_TYPE statement attribute was set to a cursor type for which the driver does not support bookmarks.|  
|HYT00|Timeout expired|The query timeout period expired before the data source returned the result set. The timeout period is set through **SQLSetStmtAttr**, SQL_ATTR_QUERY_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
  
## Comments  
 For information about how the information returned by this function might be used, see [Uses of Catalog Data](../../../odbc/reference/develop-app/uses-of-catalog-data.md).  
  
 If \**PKTableName* contains a table name, **SQLForeignKeys** returns a result set that contains the primary key of the specified table and all the foreign keys that refer to it. The list of foreign keys in other tables does not include foreign keys that point to unique constraints in the specified table.  
  
 If \**FKTableName* contains a table name, **SQLForeignKeys** returns a result set that contains all the foreign keys in the specified table that point to primary keys in other tables, and the primary keys in the other tables to which they refer. The list of foreign keys in the specified table does not contain foreign keys that refer to unique constraints in other tables.  
  
 If both \**PKTableName* and \**FKTableName* contain table names, **SQLForeignKeys** returns the foreign keys in the table specified in \**FKTableName* that refer to the primary key of the table specified in **PKTableName*. This should be one key at most.  
  
> [!NOTE]  
>  For more information about the general use, arguments, and returned data of ODBC catalog functions, see [Catalog Functions](../../../odbc/reference/develop-app/catalog-functions.md).  
  
 **SQLForeignKeys** returns results as a standard result set. If the foreign keys associated with a primary key are requested, the result set is ordered by FKTABLE_CAT, FKTABLE_SCHEM, FKTABLE_NAME, and KEY_SEQ. If the primary keys associated with a foreign key are requested, the result set is ordered by PKTABLE_CAT, PKTABLE_SCHEM, PKTABLE_NAME, and KEY_SEQ. The following table lists the columns in the result set.  
  
 The lengths of VARCHAR columns are not shown in the table; the actual lengths depend on the data source. To determine the actual lengths of the PKTABLE_CAT or FKTABLE_CAT, PKTABLE_SCHEM or FKTABLE_SCHEM, PKTABLE_NAME or FKTABLE_NAME, and PKCOLUMN_NAME or FKCOLUMN_NAME columns, an application can call **SQLGetInfo** with the SQL_MAX_CATALOG_NAME_LEN, SQL_MAX_SCHEMA_NAME_LEN, SQL_MAX_TABLE_NAME_LEN, and SQL_MAX_COLUMN_NAME_LEN options.  
  
 The following columns have been renamed for ODBC 3*.x.* The column name changes do not affect backward compatibility because applications bind by column number.  
  
|ODBC 2.0 column|ODBC 3*.x* column|  
|---------------------|-----------------------|  
|PKTABLE_QUALIFIER|PKTABLE_CAT|  
|PKTABLE_OWNER|PKTABLE_SCHEM|  
|FKTABLE_QUALIFIER|FK_TABLE_CAT|  
|FKTABLE_OWNER|FKTABLE_SCHEM|  
  
 The following table lists the columns in the result set. Additional columns beyond column 14 (REMARKS) can be defined by the driver. An application should gain access to driver-specific columns by counting down from the end of the result set instead of specifying an explicit ordinal position. For more information, see [Data Returned by Catalog Functions](../../../odbc/reference/develop-app/data-returned-by-catalog-functions.md).  
  
|Column name|Column number|Data type|Comments|  
|-----------------|-------------------|---------------|--------------|  
|PKTABLE_CAT (ODBC 1.0)|1|Varchar|Primary key table catalog name; NULL if not applicable to the data source. If a driver supports catalogs for some tables but not for others, such as when the driver retrieves data from different DBMSs, it returns an empty string ("") for those tables that do not have catalogs.|  
|PKTABLE_SCHEM (ODBC 1.0)|2|Varchar|Primary key table schema name; NULL if not applicable to the data source. If a driver supports schemas for some tables but not for others, such as when the driver retrieves data from different DBMSs, it returns an empty string ("") for those tables that do not have schemas.|  
|PKTABLE_NAME (ODBC 1.0)|3|Varchar not NULL|Primary key table name.|  
|PKCOLUMN_NAME (ODBC 1.0)|4|Varchar not NULL|Primary key column name. The driver returns an empty string for a column that does not have a name.|  
|FKTABLE_CAT (ODBC 1.0)|5|Varchar|Foreign key table catalog name; NULL if not applicable to the data source. If a driver supports catalogs for some tables but not for others, such as when the driver retrieves data from different DBMSs, it returns an empty string ("") for those tables that do not have catalogs.|  
|FKTABLE_SCHEM (ODBC 1.0)|6|Varchar|Foreign key table schema name; NULL if not applicable to the data source. If a driver supports schemas for some tables but not for others, such as when the driver retrieves data from different DBMSs, it returns an empty string ("") for those tables that do not have schemas.|  
|FKTABLE_NAME (ODBC 1.0)|7|Varchar not NULL|Foreign key table name.|  
|FKCOLUMN_NAME (ODBC 1.0)|8|Varchar not NULL|Foreign key column name. The driver returns an empty string for a column that does not have a name.|  
|KEY_SEQ (ODBC 1.0)|9|Smallint not NULL|Column sequence number in key (starting with 1).|  
|UPDATE_RULE (ODBC 1.0)|10|Smallint|Action to be applied to the foreign key when the SQL operation is **UPDATE**. Can have one of the following values. (The referenced table is the table that has the primary key; the referencing table is the table that has the foreign key.)<br /><br /> SQL_CASCADE: When the primary key of the referenced table is updated, the foreign key of the referencing table is also updated.<br /><br /> SQL_NO_ACTION: If an update of the primary key of the referenced table would cause a "dangling reference" in the referencing table (that is, rows in the referencing table would have no counterparts in the referenced table), the update is rejected. If an update of the foreign key of the referencing table would introduce a value that does not exist as a value of the primary key of the referenced table, the update is rejected. (This action is the same as the SQL_RESTRICT action in ODBC 2*.x*.)<br /><br /> SQL_SET_NULL: When one or more rows in the referenced table are updated in such a way that one or more components of the primary key are changed, the components of the foreign key in the referencing table that correspond to the changed components of the primary key are set to NULL in all matching rows of the referencing table.<br /><br /> SQL_SET_DEFAULT: When one or more rows in the referenced table are updated in such a way that one or more components of the primary key are changed, the components of the foreign key in the referencing table that correspond to the changed components of the primary key are set to the applicable default values in all matching rows of the referencing table.<br /><br /> NULL if not applicable to the data source.|  
|DELETE_RULE (ODBC 1.0)|11|Smallint|Action to be applied to the foreign key when the SQL operation is **DELETE**. Can have one of the following values. (The referenced table is the table that has the primary key; the referencing table is the table that has the foreign key.)<br /><br /> SQL_CASCADE: When a row in the referenced table is deleted, all the matching rows in the referencing tables are also deleted.<br /><br /> SQL_NO_ACTION: If a delete of a row in the referenced table would cause a "dangling reference" in the referencing table (that is, rows in the referencing table would have no counterparts in the referenced table), the update is rejected. (This action is the same as the SQL_RESTRICT action in ODBC 2*.x*.)<br /><br /> SQL_SET_NULL: When one or more rows in the referenced table are deleted, each component of the foreign key of the referencing table is set to NULL in all matching rows of the referencing table.<br /><br /> SQL_SET_DEFAULT: When one or more rows in the referenced table are deleted, each component of the foreign key of the referencing table is set to the applicable default in all matching rows of the referencing table.<br /><br /> NULL if not applicable to the data source.|  
|FK_NAME (ODBC 2.0)|12|Varchar|Foreign key name. NULL if not applicable to the data source.|  
|PK_NAME (ODBC 2.0)|13|Varchar|Primary key name. NULL if not applicable to the data source.|  
|DEFERRABILITY (ODBC 3.0)|14|Smallint|SQL_INITIALLY_DEFERRED, SQL_INITIALLY_IMMEDIATE, SQL_NOT_DEFERRABLE.|  
  
## Code Example  
 As illustrated in the following table, this example uses three tables, named ORDERS, LINES, and CUSTOMERS.  
  
|ORDERS|LINES|CUSTOMERS|  
|------------|-----------|---------------|  
|ORDERID|ORDERID|CUSTID|  
|CUSTID|LINES|NAME|  
|OPENDATE|PARTID|ADDRESS|  
|SALESPERSON|QUANTITY|PHONE|  
|STATUS|||  
  
 In the ORDERS table, CUSTID identifies the customer to whom the sale has been made. It is a foreign key that refers to CUSTID in the CUSTOMERS table.  
  
 In the LINES table, ORDERID identifies the sales order with which the line item is associated. It is a foreign key that refers to ORDERID in the ORDERS table.  
  
 This example calls **SQLPrimaryKeys** to get the primary key of the ORDERS table. The result set will have one row; the significant columns are shown in the following table.  
  
|TABLE_NAME|COLUMN_NAME|KEY_SEQ|  
|-----------------|------------------|--------------|  
|ORDERS|ORDERID|1|  
  
 Next, the example calls **SQLForeignKeys** to get the foreign keys in other tables that reference the primary key of the ORDERS table. The result set will have one row; the significant columns are shown in the following table.  
  
|PKTABLE_NAME|PKCOLUMN_NAME|FKTABLE_NAME|FKCOLUMN_NAME|KEY_SEQ|  
|-------------------|--------------------|-------------------|--------------------|--------------|  
|ORDERS|CUSTID|LINES|CUSTID|1|  
  
 Finally, the example calls **SQLForeignKeys** to get the foreign keys in the ORDERS table that refer to the primary keys of other tables. The result set will have one row; the significant columns are shown in the following table.  
  
|PKTABLE_NAME|PKCOLUMN_NAME|FKTABLE_NAME|FKCOLUMN_NAME|KEY_SEQ|  
|-------------------|--------------------|-------------------|--------------------|--------------|  
|CUSTOMERS|CUSTID|ORDERS|CUSTID|1|  
  
```cpp  
#define TAB_LEN SQL_MAX_TABLE_NAME_LEN + 1  
#define COL_LEN SQL_MAX_COLUMN_NAME_LEN + 1  
  
LPSTR   szTable;              /* Table to display */  
  
UCHAR szPkTable[TAB_LEN];   /* Primary key table name */  
UCHAR szFkTable[TAB_LEN];   /* Foreign key table name */  
UCHAR szPkCol[COL_LEN];     /* Primary key column */  
UCHAR szFkCol[COL_LEN];     /* Foreign key column */  
  
SQLHSTMT      hstmt;  
SQLINTEGER    cbPkTable, cbPkCol, cbFkTable, cbFkCol, cbKeySeq;  
SQLSMALLINT   iKeySeq;  
SQLRETURN     retcode;  
  
// Bind the columns that describe the primary and foreign keys.  
// Ignore the table schema, name, and catalog for this example.  
  
SQLBindCol(hstmt, 3, SQL_C_CHAR, szPkTable, TAB_LEN, &cbPkTable);  
SQLBindCol(hstmt, 4, SQL_C_CHAR, szPkCol, COL_LEN, &cbPkCol);  
SQLBindCol(hstmt, 5, SQL_C_SSHORT, &iKeySeq, TAB_LEN, &cbKeySeq);  
SQLBindCol(hstmt, 7, SQL_C_CHAR, szFkTable, TAB_LEN, &cbFkTable);  
SQLBindCol(hstmt, 8, SQL_C_CHAR, szFkCol, COL_LEN, &cbFkCol);  
  
strcpy_s(szTable, sizeof(szTable), "ORDERS");  
  
/* Get the names of the columns in the primary key. */  
  
retcode = SQLPrimaryKeys(hstmt,  
         NULL, 0,             /* Catalog name */  
         NULL, 0,             /* Schema name */  
         szTable, SQL_NTS);   /* Table name */  
  
while ((retcode == SQL_SUCCESS) || (retcode == SQL SUCCESS_WITH_INFO)) {  
  
   /* Fetch and display the result set. This will be a list of the */  
   /* columns in the primary key of the ORDERS table. */  
  
   retcode = SQLFetch(hstmt);  
   if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO)  
      fprintf(out, "Table: %s Column: %s Key Seq: %hd \n", szPkTable, szPkCol,  
      iKeySeq);  
}  
  
/* Close the cursor (the hstmt is still allocated). */  
  
SQLFreeStmt(hstmt, SQL_CLOSE);  
  
/* Get all the foreign keys that refer to ORDERS primary key.*/   
  
retcode = SQLForeignKeys(hstmt,  
         NULL, 0,            /* Primary catalog */  
         NULL, 0,            /* Primary schema */  
         szTable, SQL_NTS,   /* Primary table */  
         NULL, 0,            /* Foreign catalog */  
         NULL, 0,            /* Foreign schema */  
         NULL, 0);           /* Foreign table */  
  
while ((retcode == SQL_SUCCESS) || (retcode == SQL_SUCCESS_WITH_INFO)) {  
  
/* Fetch and display the result set. This will be all of the */  
/* foreign keys in other tables that refer to the ORDERS */  
/* primary key. */  
  
   retcode = SQLFetch(hstmt);  
   if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO)  
      fprintf(out, "%-s ( %-s ) <-- %-s ( %-s )\n", szPkTable,  
               szPkCol, szFkTable, szFkCol);  
}  
  
/* Close the cursor (the hstmt is still allocated). */  
  
SQLFreeStmt(hstmt, SQL_CLOSE);  
  
/* Get all the foreign keys in the ORDERS table. */  
  
retcode = SQLForeignKeys(hstmt,  
         NULL, 0,             /* Primary catalog */  
         NULL, 0,             /* Primary schema */  
         NULL, 0,             /* Primary table */  
         NULL, 0,             /* Foreign catalog */  
         NULL, 0,             /* Foreign schema */  
         szTable, SQL_NTS);   /* Foreign table */  
  
while ((retcode == SQL_SUCCESS) || (retcode == SQL_SUCCESS_WITH_INFO)) {  
  
/* Fetch and display the result set. This will be all of the */  
/* primary keys in other tables that are referred to by foreign */  
/* keys in the ORDERS table. */  
  
   retcode = SQLFetch(hstmt);  
   if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO)  
      fprintf(out, "%-s ( %-s )--> %-s ( %-s )\n", szFkTable, szFkCol, szPkTable, szPkCol);  
}  
  
/* Free the hstmt. */  
SQLFreeStmt(hstmt, SQL_DROP);  
```  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Binding a buffer to a column in a result set|[SQLBindCol Function](../../../odbc/reference/syntax/sqlbindcol-function.md)|  
|Canceling statement processing|[SQLCancel Function](../../../odbc/reference/syntax/sqlcancel-function.md)|  
|Fetching a single row or a block of data in a forward-only direction|[SQLFetch Function](../../../odbc/reference/syntax/sqlfetch-function.md)|  
|Fetching a block of data or scrolling through a result set|[SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md)|  
|Returning the columns of a primary key|[SQLPrimaryKeys Function](../../../odbc/reference/syntax/sqlprimarykeys-function.md)|  
|Returning table statistics and indexes|[SQLStatistics Function](../../../odbc/reference/syntax/sqlstatistics-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
