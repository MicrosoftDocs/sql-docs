---
title: "SQLSetDescRec | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQLSetDescRec function"
ms.assetid: 203d02a2-aa09-462b-a489-a2cdd6f6023b
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetDescRec
  This topic discusses SQLSetDescRec functionality that is specific to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
## SQLSetDescRec and Table-Valued Parameters  
 SQLSetDescRec can be used to set descriptor fields for table-valued parameters and table-valued parameter columns. Table-valued parameter columns are only available when the descriptor header field SQL_SOPT_SS_PARAM_FOCUS is set to the ordinal of a record that has SQL_DESC_TYPE set to SQL_SS_TABLE. For more information about SQL_SOPT_SS_PARAM_FOCUS, see [SQLSetStmtAttr](sqlsetstmtattr.md).  
  
 The following table describes the mapping between parameters and descriptor fields.  
  
|Parameter|Related attribute for non-table-valued parameter types, including table-valued parameter columns|Related attribute for table-valued parameters|  
|---------------|--------------------------------------------------------------------------------------------------------|----------------------------------------------------|  
|*Type*|SQL_DESC_TYPE|SQL_SS_TABLE|  
|*SubType*|Ignored|For records of type SQL_DATETIME or SQL_INTERVAL, set this to SQL_DESC_DATETIME_INTERVAL_CODE.|  
|*Length*|SQL_DESC_OCTET_LENGTH|The length of the table-valued parameter type name. This can be SQL_NTS if the type name is null terminated, or zero if the table-valued parameter type name is not required.|  
|*Precision*|SQL_DESC_PRECISION|SQL_DESC_ARRAY_SIZE|  
|*Scale*|SQL_DESC_SCALE|Unused. This parameter should be zero.|  
|*DataPtr*|SQL_DESC_DATA_PTR in APD|SQL_CA_SS_TYPE_NAME<br /><br /> This parameter is optional for stored procedure calls, and NULL can be specified if it is not required. This parameter must be specified for SQL statements that are not procedure calls.<br /><br /> *DataPtr* also serves as a unique value that the application can use to identify this table-valued parameter when variable row binding is used.|  
|*StringLengthPtr*|SQL_DESC_OCTET_LENGTH_PTR|SQL_DESC_OCTET_LENGTH_PTR<br /><br /> For a table-valued parameter, this is the number of rows to transfer or SQL_DATA_AT_EXEC. This is a pointer to a value that holds the number of rows to transfer with SQLExecDirect.|  
|*IndicatorPtr*|SQL_DESC_INDICATOR_PTR|SQL_DESC_INDICATOR_PTR|  
  
 For more information about table-valued parameters, see [Table-Valued Parameters &#40;ODBC&#41;](../native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md).  
  
## SQLSetDescRec Support for Enhanced Date and Time Features  
 The values allowed for date/time types are as follows:  
  
||*Type*|*SubType*|*Length*|*Precision*|*Scale*|  
|-|------------|---------------|--------------|-----------------|-------------|  
|datetime|SQL_DATETIME|SQL_CODE_TIMESTAMP|4|3|3|  
|smalldatetime|SQL_SQL_DATETIME|SQL_CODE_TIMESTAMP|8|0|0|  
|date|SQL_DATETIME|SQL_CODE_DATE|6|0|0|  
|time|SQL_SS_TIME2|0|10|0..7|0..7|  
|datetime2|SQL_DATETIME|SQL_CODE_TIMESTAMP|16|0..7|0..7|  
|datetimeoffset|SQL_SS_TIMESTAMPOFFSET|0|20|0..7|0..7|  
  
 For more information, see [Date and Time Improvements &#40;ODBC&#41;](../native-client-odbc-date-time/date-and-time-improvements-odbc.md).  
  
## SQLSetDescRec Support for Large CLR UDTs  
 `SQLSetDescRec` supports large CLR user-defined types (UDTs). For more information, see [Large CLR User-Defined Types &#40;ODBC&#41;](../native-client/odbc/large-clr-user-defined-types-odbc.md).  
  
## See Also  
 [SQLSetDescRec](https://go.microsoft.com/fwlink/?LinkId=80704)   
 [ODBC API Implementation Details](odbc-api-implementation-details.md)  
  
  
