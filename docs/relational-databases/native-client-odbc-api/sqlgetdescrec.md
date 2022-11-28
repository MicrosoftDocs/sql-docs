---
description: "SQLGetDescRec"
title: "SQLGetDescRec | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQLGetDescRec function"
ms.assetid: f3389ff2-f3be-4035-9fb5-c9ebc2f15025
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLGetDescRec
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  This topic discusses SQLGetDescRec functionality that is specific to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
## SQLGetDescRec and Table-Valued Parameters  
 SQLGetDescRec can be used to get values for attributes of table-valued parameters and table-valued parameter columns. The *RecNumber* parameter of SQLGetDescRec corresponds to the *ParameterNumber* parameter of SQLBindParameter.  
  
 Table-valued parameter columns are only available when the descriptor header field SQL_SOPT_SS_PARAM_FOCUS is set to the ordinal of a record that has SQL_DESC_TYPE set to SQL_SS_TABLE. For more information about SQL_SOPT_SS_PARAM_FOCUS about, see [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md).  
  
 SQLGetDescRec returns the following data:  
  
|Parameter|Table-valued parameter|Table-valued parameter columns and other parameters|  
|---------------|-----------------------------|----------------------------------------------------------|  
|*Name*|The formal parameter name for a stored procedure call; otherwise, a 0 length string.|The table-valued parameter column name.|  
|*TypePtr*|SQL_DESC_TYPE. For table-vaued parameters, this is SQL_SS_TABLE.|SQL_DESC_TYPE|  
|*SubTypePtr*|Undefined|SQL_DESC_DATETIME_INTERVAL_CODE (For records of type SQL_DATETIME or SQL_INTERVAL.)|  
|*LengthPtr*|0|SQL_DESC_OCTET_LENGTH|  
|*PrecisionPtr*|0|SQL_DESC_PRECISION|  
|*ScalePtr*|0|SQL_DESC_SCALE|  
|*NullablePtr*|1|SQL_DESC_NULLABLE|  
  
 For more information about table-valued parameters, see [Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md).  
  
## SQLGetDescRec Support for Enhanced Date and Time Features  
 The values returned for date/time types are as follows:  
  
| Attribute | *TypePtr* | *SubTypePtr* | *LengthPtr* | *PrecisionPtr* | *ScalePtr* |  
| --------- | --------- | ------------ | ----------- | -------------- | ---------- |  
|datetime|SQL_DATETIME|SQL_CODE_TIMESTAMP|4|3|3|  
|smalldatetime|SQL_DATETIME|SQL_CODE_TIMESTAMP|8|0|0|  
|date|SQL_DATETIME|SQL_CODE_DATE|6|0|0|  
|time|SQL_SS_TIME2|0|10|0..7|0..7|  
|datetime2|SQL_DATETIME|SQL_CODE_TIMESTAMP|16|0..7|0..7|  
|datetimeoffset|SQL_SS_TIMESTAMPOFFSET|0|20|0..7|0..7|  
  
 For more information, see [Date and Time Improvements &#40;ODBC&#41;](../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md).  
  
## SQLGetDescRec Support for Large CLR UDTs  
 **SQLGetDescRec** supports large CLR user-defined types (UDTs). For more information, see [Large CLR User-Defined Types &#40;ODBC&#41;](../../relational-databases/native-client/odbc/large-clr-user-defined-types-odbc.md).  
  
## See Also  
 [SQLGetDescRec](../../odbc/reference/syntax/sqlgetdescrec-function.md)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
