---
title: "SQLDescribeParam | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLDescribeParam function"
ms.assetid: 396e74b1-5d08-46dc-b404-2ef2003e4689
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLDescribeParam
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  To describe the parameters of any SQL statement, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver builds and executes a [!INCLUDE[tsql](../../includes/tsql-md.md)] SELECT statement when SQLDescribeParam is called on a prepared ODBC statement handle. The metadata of the result set determines the characteristics of the parameters in the prepared statement. SQLDescribeParam can return any error code that SQLExecute or SQLExecDirect might return.  
  
 Improvements in the database engine starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] allow SQLDescribeParam to obtain more accurate descriptions of the expected results. These more accurate results may differ from the values returned by SQLDescribeParam in previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Metadata Discovery](../../relational-databases/native-client/features/metadata-discovery.md).  
  
 Also new in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], *ParameterSizePtr* now returns a value that aligns with the definition for the size, in characters, of the column or expression of the corresponding parameter marker as defined in the [ODBC specification](https://go.microsoft.com/fwlink/?LinkId=207044). In previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, *ParameterSizePtr* could be the corresponding value of **SQL_DESC_OCTET_LENGTH** for the type, or an irrelevant column size value that was supplied to SQLBindParameter for a type, the value of which should be ignored (**SQL_INTEGER**, for example).  
  
 The driver does not support calling SQLDescribeParam in the following situations:  
  
-   After SQLExecDirect for any [!INCLUDE[tsql](../../includes/tsql-md.md)] UPDATE or DELETE statements containing the FROM clause.  
  
-   For any ODBC or [!INCLUDE[tsql](../../includes/tsql-md.md)] statement containing a parameter in a HAVING clause, or compared to the result of a SUM function.  
  
-   For any ODBC or [!INCLUDE[tsql](../../includes/tsql-md.md)] statement depending on a subquery containing parameters.  
  
-   For ODBC SQL statements containing parameter markers in both expressions of a comparison, like, or quantified predicate.  
  
-   For any queries where one of the parameters is a parameter to a function.  
  
-   When there are comments (/* \*/) in the [!INCLUDE[tsql](../../includes/tsql-md.md)] command.  
  
 When processing a batch of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, the driver also does not support calling SQLDescribeParam for parameter markers in statements after the first statement in the batch.  
  
 When describing the parameters of prepared stored procedures, SQLDescribeParam uses the system stored procedure [sp_sproc_columns](../../relational-databases/system-stored-procedures/sp-sproc-columns-transact-sql.md) to retrieve parameter characteristics. sp_sproc_columns can report data for stored procedures within the current user database. Preparing a fully qualified stored procedure name allows SQLDescribeParam to execute across databases. For example, the system stored procedure [sp_who](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md) can be prepared and executed in any database as:  
  
```  
SQLPrepare(hstmt, "{call sp_who(?)}", SQL_NTS);  
```  
  
 Executing SQLDescribeParam after successful preparation returns an empty row set when connected to any database but **master**. The same call, prepared as follows, causes SQLDescribeParam to succeed regardless of the current user database:  
  
```  
SQLPrepare(hstmt, "{call master..sp_who(?)}", SQL_NTS);  
```  
  
 For large value data types, the value returned in *DataTypePtr* is SQL_VARCHAR, SQL_VARBINARY, or SQL_NVARCHAR. To indicate that the size of the large value data type parameter is "unlimited," the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver sets *ParameterSizePtr* to 0. Actual size values are returned for standard **varchar** parameters.  
  
> [!NOTE]  
>  If the parameter has already been bound with a maximum size for the SQL_VARCHAR, SQL_VARBINARY, or SQL_WVARCHAR parameters, the bound size of the parameter is returned, not "unlimited."  
  
 To bind an "unlimited" size input parameter, data-at-execution must be used. It is not possible to bind an "unlimited" size output parameter (there is no method for streaming data from an output parameter, like [SQLGetData](../../relational-databases/native-client-odbc-api/sqlgetdata.md) does for result sets).  
  
 For output parameters, a buffer must be bound and if the value is too large, the buffer is filled and a SQL_SUCCESS_WITH_INFO message and is returned along with the "string data; right truncation" warning. The data that was truncated is then discarded.  
  
## SQLDescribeParam and Table-Valued Parameters  
 An application can retrieve table-valued parameter information for a prepared statement with SQLDescribeParam. For more information, see [Table-Valued Parameter Metadata for Prepared Statements](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameter-metadata-for-prepared-statements.md).  
  
 For more information about table-valued parameters in general, see [Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md).  
  
## SQLDescribeParam Support for Enhanced Date and Time Features  
 The values returned for date/time types are as follows:  
  
||*DataTypePtr*|*ParameterSizePtr*|*DecimalDigitsPtr*|  
|-|-------------------|------------------------|------------------------|  
|datetime|SQL_TYPE_TIMESTAMP|23|3|  
|smalldatetime|SQL_TYPE_TIMESTAMP|16|0|  
|date|SQL_TYPE_DATE|10|0|  
|time|SQL_SS_TIME2|8, 10..16|0..7|  
|datetime2|SQL_TYPE_TIMESTAMP|19, 21..27|0..7|  
|datetimeoffset|SQL_SS_TIMESTAMPOFFSET|26, 28..34|0..7|  
  
 For more information, see [Date and Time Improvements &#40;ODBC&#41;](../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md).  
  
## SQLDescribeParam Support for Large CLR UDTs  
 **SQLDescribeParam** supports large CLR user-defined types (UDTs). For more information, see [Large CLR User-Defined Types &#40;ODBC&#41;](../../relational-databases/native-client/odbc/large-clr-user-defined-types-odbc.md).  
  
## See Also  
 [SQLDescribeParam Function](https://go.microsoft.com/fwlink/?LinkId=59339)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
  
