---
title: "SQLPutData | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQLPutData function"
ms.assetid: d39aaa5b-7fbc-4315-a7f2-5a7787e04f25
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLPutData
  The following restrictions apply when using SQLPutData to send more than 65,535 bytes of data (for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 4.21a) or 400 KB of data (for SQL Server version 6.0 and later) for a SQL_LONGVARCHAR (`text`), SQL_WLONGVARCHAR (`ntext`) or SQL_LONGVARBINARY (`image`) column:  
  
-   The referenced parameter can be the *insert_value* in an INSERT statement.  
  
-   The referenced parameter can be an *expression* in the SET clause of an UPDATE statement.  
  
 Canceling a sequence of SQLPutData calls that provide data in blocks to a server running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] causes a partial update of the column's value when using version 6.5 or earlier. The `text`, `ntext`, or `image` column that was referenced when SQLCancel was called is set to an intermediate placeholder value.  
  
> [!NOTE]  
>  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver does not support connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 6.5 and earlier.  
  
## Diagnostics  
 There is one [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client specific SQLSTATE for SQLPutData:  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|22026|String data, length mismatch|If the length of data in bytes to be sent has been specified by an application, for example, with SQL_LEN_DATA_AT_EXEC(*n*) where *n* is greater than 0, the total number of bytes given by the application via SQLPutData must match the specified length.|  
  
## SQLPutData and Table-Valued Parameters  
 SQLPutData is used by an application when using variable row binding with table-valued parameters. The *StrLen_Or_Ind* parameter indicates that it is ready for the driver to collect data for the next row or rows of table-valued parameter data, or that no more rows are available:  
  
-   A value greater than 0 indicates that the next set of row values is available.  
  
-   A value of 0 indicates that there are no more rows to be sent.  
  
-   Any value less than 0 is an error and results in a diagnostic record being logged with SQLState HY090 and the messaage "Invalid string or buffer length".  
  
 The *DataPtr* parameter is ignored, but must be set to a non-NULL value. For more information, see the section on Variable TVP row binding in [Binding and Data Transfer of Table-Valued Parameters and Column Values](../native-client-odbc-table-valued-parameters/binding-and-data-transfer-of-table-valued-parameters-and-column-values.md).  
  
 If *StrLen_Or_Ind* has any value other than SQL_DEFAULT_PARAM or a number between 0 and the SQL_PARAMSET_SIZE (that is, the *ColumnSize* parameter of SQLBindParameter), it is an error. This error causes SQLPutData to return SQL_ERROR: SQLSTATE=HY090, "Invalid string or buffer length".  
  
 For more information about table-valued parameters, see [Table-Valued Parameters &#40;ODBC&#41;](../native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md).  
  
## SQLPutData Support for Enhanced Date and Time Features  
 Parameter values of date/time types are converted as described in [Conversions from C to SQL](../native-client-odbc-date-time/datetime-data-type-conversions-from-c-to-sql.md).  
  
 For more information, see [Date and Time Improvements &#40;ODBC&#41;](../native-client-odbc-date-time/date-and-time-improvements-odbc.md).  
  
## SQLPutData Support for Large CLR UDTs  
 `SQLPutData` supports large CLR user-defined types (UDTs). For more information, see [Large CLR User-Defined Types &#40;ODBC&#41;](../native-client/odbc/large-clr-user-defined-types-odbc.md).  
  
## See Also  
 [SQLPutData Function](https://go.microsoft.com/fwlink/?LinkId=59365)   
 [ODBC API Implementation Details](odbc-api-implementation-details.md)  
  
  
