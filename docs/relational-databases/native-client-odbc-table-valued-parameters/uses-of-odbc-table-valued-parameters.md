---
title: "Uses of ODBC Table-Valued Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "table-valued parameters (ODBC), scenarios"
  - "ODBC, table-valued parameters"
ms.assetid: f1b73932-4570-4a8a-baa0-0f229d9c32ee
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Uses of ODBC Table-Valued Parameters
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  This topic discusses the primary user scenarios for using table-valued parameters with ODBC:  
  
-   Table-Valued Parameter with Fully Bound Multirow Buffers (Send Data as a TVP with All Values in Memory)  
  
-   Table-Valued Parameter with Row Streaming (Send Data as a TVP Using Data-At-Execution)  
  
-   Retrieving Table-Valued Parameter Metadata from the System Catalog  
  
-   Retrieving Table-Valued Parameter Metadata for a Prepared Statement  
  
## Table-Valued Parameter with Fully Bound Multirow Buffers (Send Data as a TVP with All Values in Memory)  
 When used with fully bound multirow buffers, all parameter values are available in memory. This is typical, for example, of an OLTP transaction, in which table-valued parameters can be packaged into a single stored procedure. Without table-valued parameters, this would involve either generating a complex multi-statement batch dynamically, or making multiple calls to the server.  
  
 The table-valued parameter itself is bound by using [SQLBindParameter](https://go.microsoft.com/fwlink/?LinkId=59328) along with the other parameters. After all parameters have been bound, the application sets the parameter focus attribute, SQL_SOPT_SS_PARAM_FOCUS, on each table-valued parameter and calls SQLBindParameter for the columns of the table-valued parameter.  
  
 The server type for a table-valued parameter is a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific type, SQL_SS_TABLE. The binding C type for SQL_SS_TABLE must always be SQL_C_DEFAULT. No data is transferred for the table-valued parameter bound parameter; it is used to pass table metadata and to control how to pass data in the constituent columns of the table-valued parameter.  
  
 The length of the table-valued parameter is set to the number of rows being sent to the server. The *ColumnSize* parameter of SQLBindParameter for a table-valued parameter specifies the maximum number of rows that can be sent; this is the array size of the column buffers. *ParameterValuePtr* is the parameter buffer,for a table-valued parameter in SQLBindParameter. *ParameterValuePtr* and its associated *BufferLength* are used to pass the type name of the table-valued parameter when required. The type name is not required for stored procedure calls, but it is required for SQL statements.  
  
 When a table-valued parameter type name is specified on a call to SQLBindParameter, it must always be specified as a Unicode value, even in applications that are built as ANSI applications. When you specify a table-valued parameter type name by using SQLSetDescField, you can use a literal that conforms to the way the application is built. The ODBC Driver Manager will perform any required Unicode conversion.  
  
 Metadata for table-valued parameters and table-valued parameter columns can be manipulated individually and explicitly by using SQLGetDescRec, SQLSetDescRec, SQLGetDescField, and SQLSetDescField. However, overloading SQLBindParameter is usually more convenient and does not require explicit descriptor access in most cases. This approach is consistent with the definition of SQLBindParameter for other data types, except that for a table-valued parameter the affected descriptor fields are slightly different.  
  
 Sometimes, an application uses a table-valued parameter with dynamic SQL and the type name of the table-valued parameter must be supplied. If this is the case and the table-valued parameter is not defined in the current default schema for the connection, SQL_CA_SS_TYPE_CATALOG_NAME and SQL_CA_SS_TYPE_SCHEMA_NAME must be set by using SQLSetDescField. Because table type definitions and table-valued parameters must be in the same database, SQL_CA_SS_TYPE_CATALOG_NAME must not be set if the application uses table-valued parameters. Otherwise, SQLSetDescField will report an error.  
  
 Sample code for this scenario is in the procedure `demo_fixed_TVP_binding` in [Use Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-how-to/use-table-valued-parameters-odbc.md).  
  
## Table-Valued Parameter with Row Streaming (Send Data as a TVP Using Data-At-Execution)  
 In this scenario, the application supplies rows to the driver as it requests them and they are streamed to the server. This avoids having to buffer all rows in memory. This is representative of bulk insert/update scenarios. Table-valued parameters provide a performance point somewhere between parameter arrays and bulk copy. That is, table-valued parameters are about as easy to program as parameter arrays, but they provide greater flexibility at the server.  
  
 The table-valued parameter and its columns are bound as discussed in the previous section, Table-Valued Parameter with Fully Bound Multirow Buffers, but the length indicator of the table-valued parameter itself is set to SQL_DATA_AT_EXEC. The driver responds to SQLExecute or SQLExecuteDirect in the usual way for data-at-execution parameters-that is, by returning SQL_NEED_DATA. When the driver is ready to accept data for a table-valued parameter, SQLParamData returns the value of *ParameterValuePtr* in SQLBindParameter.  
  
 An application uses SQLPutData for a table-valued parameter to indicate the availability of data for table-valued parameter constituent columns. When SQLPutData is called for a table-valued parameter, *DataPtr* must always be null and *StrLen_or_Ind* must be either 0 or a number less than or equal to the array size specified for table-valued parameter buffers (the *ColumnSize* parameter of SQLBindParameter). 0 signifies that there are no more rows for the table-valued parameter, and the driver will proceed to process to the next actual procedure parameter. When *StrLen_or_Ind* is not 0, the driver will process the table-valued parameter constituent columns in the same way as non-table-valued parameter bound parameters: Each table-valued parameter column can specify its actual data length, SQL_NULL_DATA, or it can specify data at execution via its length/indicator buffer. Table-valued parameter column values can be passed by repeated calls to SQLPutData as usual when a character or binary value is to be passed in pieces.  
  
 When all table-valued parameter columns have been processed, the driver returns to the table-valued parameter to process further rows of table-valued parameter data. Therefore, for data-at-execution table-valued parameters, the driver does not follow the usual sequential scan of bound parameters. A bound table-valued parameter will be polled until SQLPutData is called with *StrLen_Or_IndPtr* equal to 0, at which time the driver skips table-valued parameter columns and moves to the next actual stored procedure parameter.  When SQLPutData passes an indicator value greater than or equal to 1, the driver processes table-valued parameter columns and rows sequentially until it has values for all bound rows and columns. Then the driver returns to the table-valued parameter. Between receiving the token for the table-valued parameter from SQLParamData and calling SQLPutData(hstmt, NULL, n) for a table-valued parameter, the application must set table-valued parameter constituent column data and indicator buffer contents for the next row or rows to be passed to the server.  
  
 Sample code for this scenario is in the routine `demo_variable_TVP_binding` in [Use Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-how-to/use-table-valued-parameters-odbc.md).  
  
## Retrieving Table-Valued Parameter Metadata from the System Catalog  
 When an application calls SQLProcedureColumns for a procedure that has table-valued parameter parameters, DATA_TYPE is returned as SQL_SS_TABLE and TYPE_NAME is the name of the table type for the table-valued parameter. Two additional columns are added to the result set returned by SQLProcedureColumns: SS_TYPE_CATALOG_NAME returns the name of the catalog where the table type of the table-value parameter is defined, and SS_TYPE_SCHEMA_NAME returns the name of the schema where the where the table type of the table-value parameter is defined. In conformance with the ODBC specification, SS_TYPE_CATALOG_NAME and SS_TYPE_SCHEMA_NAME appear before all driver specific columns that were added in previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and after all columns mandated by ODBC itself.  
  
 The new columns will be populated not only for table-valued parameters, but also for CLR user-defined type parameters. The existing schema and catalog columns of UDT parameters will still be populated, but having common schema and catalog columns for data types that require them will simplify application development in the future. (Note that XML schema collections are somewhat different and are not included in this change.)  
  
 An application uses SQLTables to determine the names of table types the same way it does for persistent tables, system tables, and views. A new table type, TABLE TYPE, is introduced to enable an application to identify table types associated with table-valued parameters. Table types and regular tables use different namespaces. This means that you can use the same name for both a table type and an actual table. To handle this, a new statement attribute, SQL_SOPT_SS_NAME_SCOPE, has been introduced. This attribute specifies whether SQLTables and other catalog functions that take a table name as a parameter should interpret the table name as the name of an actual table or the name of a table type.  
  
 An application uses SQLColumns to determine the columns for a table type in the same way it does for persistent tables, but must first set SQL_SOPT_SS_NAME_SCOPE to indicate that it is working with table types rather than actual tables. SQLPrimaryKeys can also be used with table types, again using SQL_SOPT_SS_NAME_SCOPE.  
  
 Sample code for this scenario is in the routine `demo_metadata_from_catalog_APIs` in [Use Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-how-to/use-table-valued-parameters-odbc.md).  
  
## Retrieving Table-Valued Parameter Metadata for a Prepared Statement  
 In this scenario, an application uses SQLNumParameters and SQLDescribeParam to retrieve metadata for table-valued parameters.  
  
 The IPD field SQL_CA_SS_TYPE_NAME is used to retrieve the type name for the table-valued parameter. The IPD fields SQL_CA_SS_TYPE_SCHEMA_NAME and SQL_CA_SS_TYPE_CATALOG_NAME are used to retrieve its catalog and schema, respectively.  
  
 Table type definitions and table-valued parameters must be in the same database. SQLSetDescField will report an error if an application sets SQL_CA_SS_TYPE_CATALOG_NAME when using table-valued parameters.  
  
 SQL_CA_SS_TYPE_CATALOG_NAME and SQL_CA_SS_TYPE_SCHEMA_NAME can also be used to retrieve the catalog and schema associated with CLR user-defined type parameters. SQL_CA_SS_TYPE_CATALOG_NAME and SQL_CA_SS_TYPE_SCHEMA_NAME are alternatives to the existing type specific catalog schema attributes for CLR UDT types.  
  
 An application uses SQLColumns to retrieve column metadata for a table-valued parameter in this scenario, too, because SQLDescribeParam does not return metadata for the columns of a table-valued parameter column.  
  
 Sample code for this use case is in the routine `demo_metadata_from_prepared_statement` in [Use Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-how-to/use-table-valued-parameters-odbc.md).  
  
## See Also  
 [Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md)  
  
  
