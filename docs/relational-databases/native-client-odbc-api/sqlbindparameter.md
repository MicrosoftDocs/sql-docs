---
description: "SQLBindParameter"
title: "SQLBindParameter | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLBindParameter function"
ms.assetid: c302c87a-e7f4-4d2b-a0a7-de42210174ac
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLBindParameter
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  **SQLBindParameter** can eliminate the burden of data conversion when used to provide data for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver, resulting in significant performance gains for both the client and server components of applications. Other benefits include reduced loss of precision when inserting or updating approximate numeric data types.  
  
> [!NOTE]  
>  When inserting **char** and **wchar** type data into an image column, the size of the data being passed in is used, as opposed to the size of the data after conversion to a binary format.  
  
 If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver encounters an error on a single array element of an array of parameters, the driver continues to execute the statement for the remaining array elements. If the application has bound an array of parameter status elements for the statement, the rows of parameters generating errors can be determined from the array.  
  
 When using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver, specify SQL_PARAM_INPUT when binding input parameters. Only specify SQL_PARAM_OUTPUT or SQL_PARAM_INPUT_OUTPUT when binding stored procedure parameters defined with the OUTPUT keyword.  
  
 [SQLRowCount](../../relational-databases/native-client-odbc-api/sqlrowcount.md) is unreliable with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver if an array element of a bound-parameter array causes an error in statement execution. The ODBC statement attribute SQL_ATTR_PARAMS_PROCESSED_PTR reports the number of rows processed before the error occurs. The application can then traverse its parameter status array to discover the number of statements successfully executed, if necessary.  
  
## Binding Parameters for SQL Character Types  
 If the SQL data type passed in is a character type, *ColumnSize* is the size in characters (not bytes). If the length of the data string in bytes is greater than 8000, *ColumnSize* should be set to **SQL_SS_LENGTH_UNLIMITED**, indicating that there is no limit to the size of the SQL type.  
  
 For instance, if the SQL data type is **SQL_WVARCHAR**, *ColumnSize* should not be greater than 4000. If the actual data length is greater than 4000, then *ColumnSize* should be set to **SQL_SS_LENGTH_UNLIMITED** so that **nvarchar(max)** will be used by driver.  
  
## SQLBindParameter and Table-Valued Parameters  
 Like other parameter types, table-valued parameters are bound by SQLBindParameter.  
  
 After a table-valued parameter has been bound, its columns are also bound. To bind the columns, you call [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) to set SQL_SOPT_SS_PARAM_FOCUS to the ordinal of the table-valued parameter. Then, call SQLBindParameter for each column in the table-valued parameter. To return to the top-level parameter bindings, set SQL_SOPT_SS_PARAM_FOCUS to 0.  
  
 For information about mapping parameters to descriptor fields for table-valued parameters, see [Binding and Data Transfer of Table-Valued Parameters and Column Values](../../relational-databases/native-client-odbc-table-valued-parameters/binding-and-data-transfer-of-table-valued-parameters-and-column-values.md).  
  
 For more information about table-valued parameters, see [Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md).  
  
## SQLBindParameter Support for Enhanced Date and Time Features  
 Parameter values of date/time types are converted as described in [Conversions from C to SQL](../../relational-databases/native-client-odbc-date-time/datetime-data-type-conversions-from-c-to-sql.md). Note that parameters of type **time** and **datetimeoffset** must have *ValueType* specified as **SQL_C_DEFAULT** or **SQL_C_BINARY** if their corresponding structures (**SQL_SS_TIME2_STRUCT** and **SQL_SS_TIMESTAMPOFFSET_STRUCT**) are used.  
  
 For more information, see [Date and Time Improvements &#40;ODBC&#41;](../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md).  
  
## SQLBindParameter Support for Large CLR UDTs  
 **SQLBindParameter** supports large CLR user-defined types (UDTs). For more information, see [Large CLR User-Defined Types &#40;ODBC&#41;](../../relational-databases/native-client/odbc/large-clr-user-defined-types-odbc.md).  
  
## See Also  
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)   
 [SQLBindParameter Function](../../odbc/reference/syntax/sqlbindparameter-function.md)  
  
