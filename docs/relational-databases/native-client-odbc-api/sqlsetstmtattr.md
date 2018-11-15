---
title: "SQLSetStmtAttr | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLSetStmtAttr function"
ms.assetid: 799c80fd-c561-4912-8562-9229076dfd19
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLSetStmtAttr
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver does not support the mixed (keyset/dynamic) cursor model. Attempts to set the keyset size using SQL_ATTR_KEYSET_SIZE will fail if the value set is not equal to 0.  
  
 The application sets SQL_ATTR_ROW_ARRAY_SIZE on all statements to declare the number of rows returned on a **SQLFetch** or [SQLFetchScroll](../../relational-databases/native-client-odbc-api/sqlfetchscroll.md) function call. On statements indicating a server cursor, the driver uses SQL_ATTR_ROW_ARRAY_SIZE to determine the size of the block of rows the server generates to satisfy a fetch request from the cursor. Within the block size of a dynamic cursor, row membership and ordering are fixed if the transaction isolation level is sufficient to ensure repeatable reads of committed transactions. The cursor is completely dynamic outside of the block indicated by this value. Server cursor block size is completely dynamic and can be changed at any point in fetch processing.  
  
## SQLSetStmtAttr and Table-Valued Parameters  
 SQLSetStmtAttr can be used to set SQL_SOPT_SS_PARAM_FOCUS in the application parameter descriptor (APD) before accessing descriptor fields for table-valued parameter columns.  
  
 If an attempt is made to set SQL_SOPT_SS_PARAM_FOCUS to the ordinal of a parameter that is not a table-valued parameter, SQLSetStmtAttr returns SQL_ERROR and a diagnostic record is created with SQLSTATE = HY024 and the message "Invalid attribute value". SQL_SOPT_SS_PARAM_FOCUS is not changed when SQL_ERROR is returned.  
  
 Setting SQL_SOPT_SS_PARAM_FOCUS to 0 restores access to descriptor records for parameters.  
  
 SQLSetStmtAttr can also be used to set SQL_SOPT_SS_NAME_SCOPE. For more information, see the SQL_SOPT_SS_NAME_SCOPE section, later in this topic.  
  
 For more information, see [Table-Valued Parameter Metadata for Prepared Statements](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameter-metadata-for-prepared-statements.md).  
  
 For more information about table-valued parameters, see [Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md).  
  
## SQLSetStmtAttr Support for Sparse Columns  
 SQLSetStmtAttr can be used to set SQL_SOPT_SS_NAME_SCOPE. For more information, see the SQL_SOPT_SS_NAME_SCOPE section, later in this topic.For more information about sparse columns, see [Sparse Columns Support &#40;ODBC&#41;](../../relational-databases/native-client/odbc/sparse-columns-support-odbc.md).  
  
## Statement Attributes  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver also supports the following driver-specific statement attributes.  
  
### SQL_SOPT_SS_CURSOR_OPTIONS  
 The SQL_SOPT_SS_CURSOR attribute specifies whether the driver will use driver-specific performance options on cursors. [SQLGetData](../../relational-databases/native-client-odbc-api/sqlgetdata.md) is not allowed when these options are set. The default setting is SQL_CO_OFF. The *ValuePtr* value is of type SQLLEN.  
  
|*ValuePtr* value|Description|  
|----------------------|-----------------|  
|SQL_CO_OFF|Default. Disables fast forward-only, read-only cursors and autofetch, enables **SQLGetData** on forward-only, read-only cursors. When SQL_SOPT_SS_CURSOR_OPTIONS is set to SQL_CO_OFF, the cursor type will not change. That is, fast forward-only cursor will remain a fast forward-only cursor. To change the cursor type, the application must now set a different cursor type using **SQLSetStmtAttr**/SQL_ATTR_CURSOR_TYPE.|  
|SQL_CO_FFO|Enables fast forward-only, read-only cursors, disables **SQLGetData** on forward-only, read-only cursors.|  
|SQL_CO_AF|Enables the autofetch option on any cursor type. When this option is set for a statement handle, **SQLExecute** or **SQLExecDirect** will generate an implicit **SQLFetchScroll** (SQL_FIRST). The cursor is opened and the first batch of rows is returned in a single roundtrip to the server.|  
|SQL_CO_FFO_AF|Enables fast forward-only cursors with the autofetch option. It is the same as if both SQL_CO_AF and SQL_CO_FFO are specified.|  
  
 When these options are set, the server closes the cursor automatically when it detects that the last row has been fetched. The application must still call [SQLFreeStmt](../../relational-databases/native-client-odbc-api/sqlfreestmt.md) (SQL_CLOSE) or [SQLCloseCursor](../../relational-databases/native-client-odbc-api/sqlclosecursor.md), but the driver does not have to send the close notification to the server.  
  
 If the select list contains a **text**, **ntext**, or **image** column, the fast forward-only cursor is converted to a dynamic cursor and **SQLGetData** is allowed.  
  
### SQL_SOPT_SS_DEFER_PREPARE  
 The SQL_SOPT_SS_DEFER_PREPARE attribute determines whether the statement is prepared immediately or deferred until **SQLExecute**, [SQLDescribeCol](../../relational-databases/native-client-odbc-api/sqldescribecol.md) or [SQLDescribeParam](../../relational-databases/native-client-odbc-api/sqldescribeparam.md) is executed. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7.0 and earlier, this property is ignored (no deferred prepare). The *ValuePtr* value is of type SQLLEN.  
  
|*ValuePtr* value|Description|  
|----------------------|-----------------|  
|SQL_DP_ON|Default. After calling [SQLPrepare Function](https://go.microsoft.com/fwlink/?LinkId=59360), the statement preparation is deferred until **SQLExecute** is called or metaproperty operation (**SQLDescribeCol** or **SQLDescribeParam**) is executed.|  
|SQL_DP_OFF|The statement is prepared as soon as **SQLPrepare** is executed.|  
  
### SQL_SOPT_SS_REGIONALIZE  
 The SQL_SOPT_SS_REGIONALIZE attribute is used to determine data conversion at the statement level. The attribute causes the driver to respect the client locale setting when converting date, time, and currency values to character strings. The conversion is from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] native data types to character strings only.  
  
 The *ValuePtr* value is of type SQLLEN.  
  
|*ValuePtr* value|Description|  
|----------------------|-----------------|  
|SQL_RE_OFF|Default. The driver does not convert date, time, and currency data to character string data using the client locale setting.|  
|SQL_RE_ON|The driver uses the client locale setting when converting date, time, and currency data to character string data.|  
  
 Regional conversion settings apply to currency, numeric, date, and time data types. The conversion setting is only applicable to output conversions when currency, numeric, date, or time values are converted to character strings.  
  
> [!NOTE]  
>  When the statement option SQL_SOPT_SS_REGIONALIZE is on, the driver uses the locale registry settings for the current user. The driver does not honor the current thread's locale if the application sets it by, for example, calling **SetThreadLocale**.  
  
 Altering the regional behavior of a data source can cause application failure. An application that parses date strings and expects date strings to appear as defined by ODBC, could be adversely affected by altering this value.  
  
### SQL_SOPT_SS_TEXTPTR_LOGGING  
 The SQL_SOPT_SS_TEXTPTR_LOGGING attribute toggles logging of operations on columns containing **text** or **image** data. The *ValuePtr* value is of type SQLLEN.  
  
|*ValuePtr* value|Description|  
|----------------------|-----------------|  
|SQL_TL_OFF|Disables logging of operations performed on **text** and **image** data.|  
|SQL_TL_ON|Default. Enables logging of operations performed on **text** and **image** data.|  
  
### SQL_SOPT_SS_HIDDEN_COLUMNS  
 The SQL_SOPT_SS_HIDDEN_COLUMNS attribute exposes, in the result set, columns hidden in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SELECT FOR BROWSE statement. The driver does not expose these columns by default. The *ValuePtr* value is of type SQLLEN.  
  
|*ValuePtr* value|Description|  
|----------------------|-----------------|  
|SQL_HC_OFF|Default. FOR BROWSE columns are hidden from the result set.|  
|SQL_HC_ON|Exposes FOR BROWSE columns.|  
  
### SQL_SOPT_SS_QUERYNOTIFICATION_MSGTEXT  
 The SQL_SOPT_SS_QUERYNOTIFICATION_MSGTEXT attribute returns the message text for the query notification request.  
  
### SQL_SOPT_SS_QUERYNOTIFICATION_OPTIONS  
 The SQL_SOPT_SS_QUERYNOTIFICATION_OPTIONS attribute specifies the options used for the query notification request. These are specified in a string with `name=value` syntax as specified below. The application is responsible for creating the service and reading notifications off of the queue.  
  
 The syntax of the query notifications options string is:  
  
 `service=<service-name>[;(local database=<database>|broker instance=<broker instance>)]`  
  
 For example:  
  
 `service=mySSBService;local database=mydb`  
  
### SQL_SOPT_SS_QUERYNOTIFICATION_TIMEOUT  
 The SQL_SOPT_SS_QUERYNOTIFICATION_TIMEOUT attribute specifies the number of seconds that the query notification is to remain active. The default value is 432000 seconds (5 days). The *ValuePtr* value is of type SQLLEN.  
  
### SQL_SOPT_SS_PARAM_FOCUS  
 The SQL_SOPT_SS_PARAM_FOCUS attribute specifies the focus for subsequent SQLBindParameter, SQLGetDescField, SQLSetDescField, SQLGetDescRec, and SQLSetDescRec calls.  
  
 The type for SQL_SOPT_SS_PARAM_FOCUS is SQLULEN.  
  
 The default is 0, which means that these calls address parameters that correspond to parameter markers in the SQL statement. When set to the parameter number of a table-valued parameter, these calls address columns of that table-valued parameter. When set to a value that is not the parameter number of a table-valued parameter, these calls return the error IM020: "Parameter focus does not refer to a table-valued parameter".  
  
### SQL_SOPT_SS_NAME_SCOPE  
 The SQL_SOPT_SS_NAME_SCOPE attribute specifies the name scope for subsequent catalog function calls. The result set returned by SQLColumns depends on the setting of SQL_SOPT_SS_NAME_SCOPE.  
  
 The type for SQL_SOPT_SS_NAME_SCOPE is SQLULEN.  
  
|*ValuePtr* value|Description|  
|----------------------|-----------------|  
|SQL_SS_NAME_SCOPE_TABLE|Default.<br /><br /> When using table-valued parameters, indicates that metadata for actual tables should be returned.<br /><br /> When using the sparse columns feature, SQLColumns will return only columns that are not members of the sparse **column_set**.|  
|SQL_SS_NAME_SCOPE_TABLE_TYPE|Indicates that the application requires metadata for a table type, rather than an actual table (catalog functions should return metadata for table types). The application then passes the TYPE_NAME of the table-valued parameter as the *TableName* parameter.|  
|SQL_SS_NAME_SCOPE_EXTENDED|When using the sparse columns feature, SQLColumns returns all columns, regardless of **column_set** membership.|  
|SQL_SS_NAME_SCOPE_SPARSE_COLUMN_SET|When using the sparse columns feature, SQLColumns returns only columns that are members of the sparse **column_set**.|  
|SQL_SS_NAME_SCOPE_DEFAULT|Equal to SQL_SS_NAME_SCOPE_TABLE.|  
  
 SS_TYPE_CATALOG_NAME and SS_TYPE_SCHEMA_NAME are used with the *CatalogName* and *SchemaName* parameters, respectively, to identify the catalog and schema for the table-valued parameter. When an application has finished retrieving metadata for table-valued parameters, it must set SQL_SOPT_SS_NAME_SCOPE back to its default value of SQL_SS_NAME_SCOPE_TABLE.  
  
 When SQL_SOPT_SS_NAME_SCOPE is set to SQL_SS_NAME_SCOPE_TABLE, queries to linked servers fail. Calls to SQLColumns or SQLPrimaryKeys with a catalog that contains a server component will fail.  
  
 If you attempt to set SQL_SOPT_SS_NAME_SCOPE to an invalid value, SQL_ERROR is returned and a diagnostic record is generated with SQLSTATE HY024 and the message "Invalid attribute value".  
  
 If a catalog function other then SQLTables, SQLColumns, or SQLPrimaryKeys is called when SQL_SOPT_SS_NAME_SCOPE has a value other than SQL_SS_NAME_SCOPE_TABLE, SQL_ERROR is returned. A diagnostic record is generated with SQLSTATE HY010 and the message "Function sequence error (SQL_SOPT_SS_NAME_SCOPE is not set to SQL_SS_NAME_SCOPE_TABLE)".  
  
## See Also  
 [SQLGetStmtAttr Function](https://go.microsoft.com/fwlink/?LinkId=59355)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
  
