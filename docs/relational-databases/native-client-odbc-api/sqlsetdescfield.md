---
description: "SQLSetDescField"
title: "SQLSetDescField | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQLSetDescField function"
ms.assetid: de4bed15-15be-4825-994c-1046255e725a
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLSetDescField
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  SQLSetDescField can be used to set descriptor fields for table-valued parameters and table-valued parameter columns. For information about the available fields, see [Table-Valued Parameter Descriptor Fields](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameter-descriptor-fields.md) and [Descriptor Fields for Table-Valued Parameter Constituent Columns](../../relational-databases/native-client-odbc-table-valued-parameters/descriptor-fields-for-table-valued-parameter-constituent-columns.md).  
  
## Remarks  
 Table-valued parameter columns are only available when the descriptor header field SQL_SOPT_SS_PARAM_FOCUS is set to the ordinal of a record that has SQL_DESC_TYPE set to SQL_SS_TABLE. For more information about SQL_SOPT_SS_PARAM_FOCUS, see [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md).  
  
 If an attempt is made to set SQL_SOPT_SS_PARAM_FOCUS to the ordinal of a parameter that is not a table-valued parameter, SQLSetStmtAttr returns SQL_ERROR, and a diagnostic record is created with SQLSTATE = HY024 and the message "Invalid attribute value". SQL_SOPT_SS_PARAM_FOCUS is not changed when SQL_ERROR is returned.  
  
 Setting SQL_SOPT_SS_PARAM_FOCUS to 0 restores access to descriptor records for parameters.  
  
 For more information about table-valued parameters, see [Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md).  
  
## SQLSetDescField Support for Enhanced Date and Time Features  
 Date/time features have been enhanced in ODBC. For information about the descriptor field provided for the new date/time types, see [Parameter and Result Metadata](../../relational-databases/native-client-odbc-date-time/metadata-parameter-and-result.md).  
  
 For more information, see [Date and Time Improvements &#40;ODBC&#41;](../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md).  
  
## SQLSetDescField Support for Large CLR UDTs  
 SQLSetDescField supports large CLR user-defined types (UDTs). For more information, see [Large CLR User-Defined Types &#40;ODBC&#41;](../../relational-databases/native-client/odbc/large-clr-user-defined-types-odbc.md).  
  
## SQLSetDescField Support for Sparse Columns  
 SQLSetDecField can be used to set SQL_SOPT_SS_NAME_SCOPE in the application parameter descriptor (APD) to the values SQL_SS_NAME_SCOPE_EXTENDED and SQL_SS_NAME_SCOPE_SPARSE_COLUMN_SET.  
  
 For more information, see [Sparse Columns Support &#40;ODBC&#41;](../../relational-databases/native-client/odbc/sparse-columns-support-odbc.md).  
  
## See Also  
 [SQLSetDescField](../../odbc/reference/syntax/sqlsetdescfield-function.md)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
