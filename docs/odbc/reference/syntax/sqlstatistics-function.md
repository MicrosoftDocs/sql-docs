---
title: "SQLStatistics Function"
description: SQLStatistics retrieves a list of statistics about a single table and the indexes associated with the table. The driver returns the information as a result set.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, sunilbs, mcimfl
ms.date: 12/29/2025
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
f1_keywords:
  - "SQLStatistics"
helpviewer_keywords:
  - "SQLStatistics function [ODBC]"
apilocation: "sqlsrv32.dll"
apiname: "SQLStatistics"
apitype: "dllExport"
---
# SQLStatistics function

`SQLStatistics` retrieves a list of statistics about a single table and the indexes associated with the table. The driver returns the information as a result set.

## Conformance

**Version introduced**: ODBC 1.0 Standards Compliance: ISO 92

## Syntax

```cpp
SQLRETURN SQLStatistics(
     SQLHSTMT        StatementHandle ,
     SQLCHAR *       CatalogName ,
     SQLSMALLINT     NameLength1 ,
     SQLCHAR *       SchemaName ,
     SQLSMALLINT     NameLength2 ,
     SQLCHAR *       TableName ,
     SQLSMALLINT     NameLength3 ,
     SQLUSMALLINT    Unique ,
     SQLUSMALLINT    Reserved);
```

## Arguments

#### *StatementHandle*

[Input] Statement handle.

#### *CatalogName*

[Input] Catalog name. If a driver supports catalogs for some tables but not for others, such as when the driver retrieves data from different DBMSs, an empty string (`""`) indicates those tables that don't have catalogs. *CatalogName* can't contain a string search pattern.

If the `SQL_ATTR_METADATA_ID` statement attribute is set to `SQL_TRUE`, *CatalogName* is treated as an identifier and its case isn't significant. If it's `SQL_FALSE`, *CatalogName* is an ordinary argument; it's treated literally, and its case is significant. For more information, see [Arguments in Catalog Functions](../develop-app/arguments-in-catalog-functions.md).

#### *NameLength1*

[Input] Length in characters of *CatalogName*.

#### *SchemaName*

[Input] Schema name. If a driver supports schemas for some tables but not for others, such as when the driver retrieves data from different DBMSs, an empty string (`""`) indicates those tables that don't have schemas. *SchemaName* can't contain a string search pattern.

If the `SQL_ATTR_METADATA_ID` statement attribute is set to `SQL_TRUE`, *SchemaName* is treated as an identifier and its case isn't significant. If it's `SQL_FALSE`, *SchemaName* is an ordinary argument; it's treated literally, and its case is significant.

#### *NameLength2*

[Input] Length in characters of *SchemaName*.

#### *TableName*

[Input] Table name. This argument can't be a null pointer. *TableName* can't contain a string search pattern.

If the `SQL_ATTR_METADATA_ID` statement attribute is set to `SQL_TRUE`, *TableName* is treated as an identifier and its case isn't significant. If it's `SQL_FALSE`, *TableName* is an ordinary argument; it's treated literally, and its case is significant.

#### *NameLength3*

[Input] Length in characters of *TableName*.

#### *Unique*

[Input] Type of index: `SQL_INDEX_UNIQUE` or `SQL_INDEX_ALL`.

#### *Reserved*

[Input] Indicates the importance of the `CARDINALITY` and `PAGES` columns in the result set. The following options affect the return of the `CARDINALITY` and `PAGES` columns only; index information is returned even if `CARDINALITY` and `PAGES` aren't returned.

`SQL_ENSURE` requests that the driver unconditionally retrieves the statistics. (Drivers that conform only to the Open Group standard and don't support ODBC extensions won't be able to support `SQL_ENSURE`.)

`SQL_QUICK` requests that the driver retrieves the `CARDINALITY` and `PAGES` only if they're readily available from the server. In this case, the driver doesn't ensure that the values are current. (Applications that are written to the Open Group standard will always get `SQL_QUICK` behavior from ODBC *3.x*-compliant drivers.)

## Returns

`SQL_SUCCESS`, `SQL_SUCCESS_WITH_INFO`, `SQL_STILL_EXECUTING`, `SQL_ERROR`, or `SQL_INVALID_HANDLE`.

## Diagnostics

When `SQLStatistics` returns `SQL_ERROR` or `SQL_SUCCESS_WITH_INFO`, an associated `SQLSTATE` value can be obtained by calling `SQLGetDiagRec` with a *HandleType* of `SQL_HANDLE_STMT` and a *Handle* of *StatementHandle*. The following table lists the `SQLSTATE` values typically returned by `SQLStatistics` and explains each one in the context of this function; the notation `(DM)` precedes the descriptions of each `SQLSTATE` returned by the Driver Manager. The return code associated with each `SQLSTATE` value is `SQL_ERROR`, unless noted otherwise.

| SQLSTATE | Error | Description |
| --- | --- | --- |
| `01000` | General warning | Driver-specific informational message. (Function returns `SQL_SUCCESS_WITH_INFO`.) |
| `08S01` | Communication link failure | The communication link between the driver and the data source to which the driver was connected failed before the function completed processing. |
| `24000` | Invalid cursor state | A cursor was open on the *StatementHandle*, and `SQLFetch` or `SQLFetchScroll` had been called. This error is returned by the Driver Manager if `SQLFetch` or `SQLFetchScroll` hasn't returned `SQL_NO_DATA` and is returned by the driver if `SQLFetch` or `SQLFetchScroll` has returned `SQL_NO_DATA`.<br /><br />A cursor was open on the *StatementHandle*, but `SQLFetch` or `SQLFetchScroll` hadn't been called. |
| `40001` | Serialization failure | The transaction was rolled back because of a resource deadlock with another transaction. |
| `40003` | Statement completion unknown | The associated connection failed during the execution of this function, and the state of the transaction can't be determined. |
| `HY000` | General error | An error occurred for which there was no specific `SQLSTATE` and for which no implementation-specific `SQLSTATE` was defined. The error message returned by `SQLGetDiagRec` in the *\*MessageText* buffer describes the error and its cause. |
| `HY001` | Memory allocation error | The driver was unable to allocate memory that is required to support execution or completion of the function. |
| `HY008` | Operation canceled | Asynchronous processing was enabled for the *StatementHandle*. The function was called, and before it completed execution, `SQLCancel` or `SQLCancelHandle` was called on the *StatementHandle*, and then the function was called again on the *StatementHandle*.<br /><br />The function was called, and before it completed execution, `SQLCancel` or `SQLCancelHandle` was called on the *StatementHandle* from a different thread in a multithread application. |
| `HY009` | Invalid use of null pointer | The *TableName* argument was a null pointer.<br /><br />The `SQL_ATTR_METADATA_ID` statement attribute was set to `SQL_TRUE`, the *CatalogName* argument was a null pointer, and the `SQL_CATALOG_NAME` *InfoType* returns that catalog names are supported.<br /><br />(DM) The `SQL_ATTR_METADATA_ID` statement attribute was set to `SQL_TRUE`, and the *SchemaName* argument was a null pointer. |
| `HY010` | Function sequence error | (DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when the `SQLStatistics` function was called.<br /><br />(DM) `SQLExecute`, `SQLExecDirect`, or `SQLMoreResults` was called for the *StatementHandle* and returned `SQL_PARAM_DATA_AVAILABLE`. This function was called before data was retrieved for all streamed parameters.<br /><br />(DM) An asynchronously executing function (not this one) was called for the *StatementHandle* and was still executing when this function was called.<br /><br />(DM) `SQLExecute`, `SQLExecDirect`, `SQLBulkOperations`, or `SQLSetPos` was called for the *StatementHandle* and returned `SQL_NEED_DATA`. This function was called before data was sent for all data-at-execution parameters or columns. |
| `HY013` | Memory management error | The function call couldn't be processed because the underlying memory objects couldn't be accessed, possibly because of low memory conditions. |
| `HY090` | Invalid string or buffer length | (DM) The value of one of the name length arguments was less than 0 but not equal to `SQL_NTS`.<br /><br />The value of one of the name length arguments exceeded the maximum length value for the corresponding name. |
| `HY100` | Uniqueness option type out of range | (DM) An invalid *Unique* value was specified. |
| `HY101` | Accuracy option type out of range | (DM) An invalid *Reserved* value was specified. |
| `HY117` | Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed. | (DM) For more information about suspended state, see [SQLEndTran Function](sqlendtran-function.md). |
| `HYC00` | Optional feature not implemented | A catalog was specified, and the driver or data source doesn't support catalogs.<br /><br />A schema was specified, and the driver or data source doesn't support schemas.<br /><br />The combination of the current settings of the `SQL_ATTR_CONCURRENCY` and `SQL_ATTR_CURSOR_TYPE` statement attributes wasn't supported by the driver or data source.<br /><br />The `SQL_ATTR_USE_BOOKMARKS` statement attribute was set to `SQL_UB_VARIABLE`, and the `SQL_ATTR_CURSOR_TYPE` statement attribute was set to a cursor type for which the driver doesn't support bookmarks. |
| `HYT00` | Timeout expired | The query timeout period expired before the data source returned the requested result set. The timeout period is set through `SQLSetStmtAttr`, `SQL_ATTR_QUERY_TIMEOUT`. |
| `HYT01` | Connection timeout expired | The connection timeout period expired before the data source responded to the request. The connection timeout period is set through `SQLSetConnectAttr`, `SQL_ATTR_CONNECTION_TIMEOUT`. |
| `IM001` | Driver doesn't support this function | (DM) The driver associated with the *StatementHandle* doesn't support the function. |
| `IM017` | Polling is disabled in asynchronous notification mode | Whenever the notification model is used, polling is disabled. |
| `IM018` | `SQLCompleteAsync` hasn't been called to complete the previous asynchronous operation on this handle. | If the previous function call on the handle returns `SQL_STILL_EXECUTING` and if notification mode is enabled, `SQLCompleteAsync` must be called on the handle to do post-processing and complete the operation. |

## Comments

`SQLStatistics` returns information about a single table as a standard result set, ordered by `NON_UNIQUE`, `TYPE`, `INDEX_QUALIFIER`, `INDEX_NAME`, and `ORDINAL_POSITION`. The result set combines statistics information (in the `CARDINALITY` and `PAGES` columns of the result set) for the table with information about each index. For information about how this information might be used, see [Uses of Catalog Data](../develop-app/uses-of-catalog-data.md).

To determine the actual lengths of the `TABLE_CAT`, `TABLE_SCHEM`, `TABLE_NAME`, and `COLUMN_NAME` columns, an application can call `SQLGetInfo` with the `SQL_MAX_CATALOG_NAME_LEN`, `SQL_MAX_SCHEMA_NAME_LEN`, `SQL_MAX_TABLE_NAME_LEN`, and `SQL_MAX_COLUMN_NAME_LEN` options.

> [!NOTE]  
> For more information about the general use, arguments, and returned data of ODBC catalog functions, see [Catalog Functions](../develop-app/catalog-functions.md).

The following columns have been renamed for ODBC *3.x*. The column name changes don't affect backward compatibility because applications bind by column number.

| ODBC 2.0 column | ODBC *3.x* column |
| --- | --- |
| `TABLE_QUALIFIER` | `TABLE_CAT` |
| `TABLE_OWNER` | `TABLE_SCHEM` |
| `SEQ_IN_INDEX` | `ORDINAL_POSITION` |
| `COLLATION` | `ASC_OR_DESC` |

The following table lists the columns in the result set. Additional columns beyond column 13 (`FILTER_CONDITION`) can be defined by the driver. An application should gain access to driver-specific columns by counting down from the end of the result set instead of specifying an explicit ordinal position. For more information, see [Data Returned by Catalog Functions](../develop-app/data-returned-by-catalog-functions.md).

| Column name | Column number | Data type | Comments |
| --- | --- | --- | --- |
| `TABLE_CAT` (ODBC 1.0) | 1 | **varchar** | Catalog name of the table to which the statistic or index applies; `NULL` if not applicable to the data source. If a driver supports catalogs for some tables but not for others, such as when the driver retrieves data from different DBMSs, it returns an empty string (`""`) for those tables that don't have catalogs. |
| `TABLE_SCHEM` (ODBC 1.0) | 2 | **varchar** | Schema name of the table to which the statistic or index applies; `NULL` if not applicable to the data source. If a driver supports schemas for some tables but not for others, such as when the driver retrieves data from different DBMSs, it returns an empty string (`""`) for those tables that don't have schemas. |
| `TABLE_NAME` (ODBC 1.0) | 3 | **varchar** not `NULL` | Table name of the table to which the statistic or index applies. |
| `NON_UNIQUE` (ODBC 1.0) | 4 | **smallint** | Indicates whether the index doesn't allow duplicate values:<br /><br />`SQL_TRUE` if the index values can be nonunique.<br /><br />`SQL_FALSE` if the index values must be unique.<br /><br />`NULL` is returned if `TYPE` is `SQL_TABLE_STAT`. |
| `INDEX_QUALIFIER` (ODBC 1.0) | 5 | **varchar** | The identifier that is used to qualify the index name doing a `DROP INDEX`; `NULL` is returned if an index qualifier isn't supported by the data source or if `TYPE` is `SQL_TABLE_STAT`. If a non-null value is returned in this column, it must be used to qualify the index name on a `DROP INDEX` statement; otherwise, the `TABLE_SCHEM` should be used to qualify the index name. |
| `INDEX_NAME` (ODBC 1.0) | 6 | **varchar** | Index name; `NULL` is returned if `TYPE` is `SQL_TABLE_STAT`. |
| `TYPE` (ODBC 1.0) | 7 | **smallint** not `NULL` | Type of information being returned:<br /><br />`SQL_TABLE_STAT` indicates a statistic for the table (in the `CARDINALITY` or `PAGES` column).<br /><br />`SQL_INDEX_BTREE` indicates a B-Tree index.<br /><br />`SQL_INDEX_CLUSTERED` indicates a clustered index.<br /><br />`SQL_INDEX_CONTENT` indicates a content index.<br /><br />`SQL_INDEX_HASHED` indicates a hashed index.<br /><br />`SQL_INDEX_OTHER` indicates another type of index. |
| `ORDINAL_POSITION` (ODBC 1.0) | 8 | **smallint** | Column sequence number in index (starting with 1); `NULL` is returned if `TYPE` is `SQL_TABLE_STAT`. |
| `COLUMN_NAME` (ODBC 1.0) | 9 | **varchar** | Column name. If the column is based on an expression, such as `SALARY + BENEFITS`, the expression is returned; if the expression can't be determined, an empty string is returned. `NULL` is returned if `TYPE` is `SQL_TABLE_STAT`. |
| `ASC_OR_DESC` (ODBC 1.0) | 10 | **char(1)** | Sort sequence for the column: "A" for ascending; "D" for descending; `NULL` is returned if column sort sequence isn't supported by the data source or if `TYPE` is `SQL_TABLE_STAT`. |
| `CARDINALITY` (ODBC 1.0) | 11 | **integer** | Cardinality of table or index; number of rows in table if `TYPE` is `SQL_TABLE_STAT`; number of unique values in the index if `TYPE` isn't `SQL_TABLE_STAT`; `NULL` is returned if the value isn't available from the data source. |
| `PAGES` (ODBC 1.0) | 12 | **integer** | Number of pages used to store the index or table; number of pages for the table if `TYPE` is `SQL_TABLE_STAT`; number of pages for the index if `TYPE` isn't `SQL_TABLE_STAT`; `NULL` is returned if the value isn't available from the data source or if not applicable to the data source. |
| `FILTER_CONDITION` (ODBC 2.0) | 13 | **varchar** | If the index is a filtered index, this is the filter condition, such as `SALARY > 30000`; if the filter condition can't be determined, this is an empty string.<br /><br />`NULL` if the index isn't a filtered index, it can't be determined whether the index is a filtered index, or `TYPE` is `SQL_TABLE_STAT`. |

If the row in the result set corresponds to a table, the driver sets `TYPE` to `SQL_TABLE_STAT` and sets `NON_UNIQUE`, `INDEX_QUALIFIER`, `INDEX_NAME`, `ORDINAL_POSITION`, `COLUMN_NAME`, and `ASC_OR_DESC` to `NULL`. If `CARDINALITY` or `PAGES` aren't available from the data source, the driver sets them to `NULL`.

## Code example

For a code example of a similar function, see [SQLColumns Function](sqlcolumns-function.md).

## Related functions

| For information about | Article |
| --- | --- |
| Binding a buffer to a column in a result set | [SQLBindCol Function](sqlbindcol-function.md) |
| Canceling statement processing | [SQLCancel Function](sqlcancel-function.md) |
| Fetching a single row or a block of data in a forward-only direction. | [SQLFetch Function](sqlfetch-function.md) |
| Fetching a block of data or scrolling through a result set | [SQLFetchScroll Function](sqlfetchscroll-function.md) |
| Returning the columns of foreign keys | [SQLForeignKeys Function](sqlforeignkeys-function.md) |
| Returning the columns of a primary key | [SQLPrimaryKeys Function](sqlprimarykeys-function.md) |

## Related content

- [ODBC API reference](odbc-api-reference.md)
- [ODBC Header Files](../install/odbc-header-files.md)
