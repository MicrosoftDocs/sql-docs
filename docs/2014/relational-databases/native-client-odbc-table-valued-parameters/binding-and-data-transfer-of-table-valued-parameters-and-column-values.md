---
title: "Binding and Data Transfer of Table-Valued Parameters and Column Values | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "table-valued parameters (ODBC), binding and data transfer"
ms.assetid: 0a2ea462-d613-42b6-870f-c7fa086a6b42
author: MightyPen
ms.author: genemi
manager: craigg
---
# Binding and Data Transfer of Table-Valued Parameters and Column Values
  Table-valued parameters, like other parameters, must be bound before they are passed to the server. The application binds table-valued parameters the same way it binds other parameters: by using SQLBindParameter or equivalent calls to SQLSetDescField or SQLSetDescRec. The server data type for a table-valued parameter is SQL_SS_TABLE. The C type can be specified either as SQL_C_DEFAULT or SQL_C_BINARY.  
  
 In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or later, only input table-valued parameters are supported. Therefore, any attempt to set SQL_DESC_PARAMETER_TYPE to a value other than SQL_PARAM_INPUT will return SQL_ERROR with SQLSTATE = HY105 and the message "Invalid parameter type".  
  
 Entire table-valued parameter columns can be assigned default values by using the attribute SQL_CA_SS_COL_HAS_DEFAULT_VALUE. Individual table-valued parameter column values, however, cannot be assigned default values by using SQL_DEFAULT_PARAM in *StrLen_or_IndPtr* with SQLBindParameter. Table-valued parameters as a whole cannot be set to a default value by using SQL_DEFAULT_PARAM in *StrLen_or_IndPtr* with SQLBindParameter. If these rules are not followed, SQLExecute or SQLExecDirect will return SQL_ERROR. A diagnostic record will be generated with SQLSTATE=07S01 and the message "Invalid use of default parameter for parameter \<p>", where \<p> is the ordinal of the TVP in the query statement.  
  
 After binding the table-valued parameter, the application must then bind each table-valued parameter column. To do this, the application first calls SQLSetStmtAttr to set SQL_SOPT_SS_PARAM_FOCUS to the ordinal of a table-valued parameter. Then the application binds the columns of the table-valued parameter by calls to the following routines: SQLBindParameter, SQLSetDescRec, and SQLSetDescField. Setting SQL_SOPT_SS_PARAM_FOCUS to 0 restores the usual effect of SQLBindParameter, SQLSetDescRec, and SQLSetDescField in operating on regular top-level parameters.  
  
 No actual data is sent or received for the table-valued parameter itself, but data is sent and received for each of its constituent columns. Because the table-valued parameter is a pseudo column, the parameters for SQLBindParameter are used to refer to different attributes than other data types, as follows:  
  
|Parameter|Related attribute for non-table-valued parameter types, including columns|Related attribute for table-valued parameters|  
|---------------|--------------------------------------------------------------------------------|----------------------------------------------------|  
|*InputOutputType*|SQL_DESC_PARAMETER_TYPE in IPD.<br /><br /> For table-valued parameter columns, this must be the same as the setting for the table-valued parameter itself.|SQL_DESC_PARAMETER_TYPE in IPD.<br /><br /> This must be SQL_PARAM_INPUT.|  
|*ValueType*|SQL_DESC_TYPE, SQL_DESC_CONCISE_TYPE in APD.|SQL_DESC_TYPE, SQL_DESC_CONCISE_TYPE in APD.<br /><br /> This must be SQL_C_DEFAULT or SQL_C_BINARY.|  
|*ParameterType*|SQL_DESC_TYPE, SQL_DESC_CONCISE_TYPE in IPD.|SQL_DESC_TYPE, SQL_DESC_CONCISE_TYPE in IPD.<br /><br /> This must be SQL_SS_TABLE.|  
|*ColumnSize*|SQL_DESC_LENGTH or SQL_DESC_PRECISION in IPD.<br /><br /> This depends on the value of *ParameterType*.|SQL_DESC_ARRAY_SIZE<br /><br /> Can also be set using SQL_ATTR_PARAM_SET_SIZE when the parameter focus is set to the table-valued parameter.<br /><br /> For a table-valued parameter, this is the number of rows in the table-valued parameter column buffers.|  
|*DecimalDigits*|SQL_DESC_PRECISION or SQL_DESC_SCALE in IPD.|Unused. This must be 0.<br /><br /> If this parameter is not 0, SQLBindParameter will return SQL_ERROR, and a diagnostic record will be generated with SQLSTATE= HY104 and the message "Invalid precision or scale".|  
|*ParameterValuePtr*|SQL_DESC_DATA_PTR in APD.|SQL_CA_SS_TYPE_NAME.<br /><br /> This is optional for stored procedure calls, and NULL can be specified if it is not required. It must be specified for SQL statements that are not procedure calls.<br /><br /> This parameter also serves as a unique value that the application can use to identify this table-valued parameter when variable row binding is used. For more information, see the "Variable Table-Valued Parameter Row Binding" section, later in this topic.<br /><br /> When a table-valued parameter type name is specified on a call to SQLBindParameter, it must be specified as a Unicode value, even in applications that are built as ANSI applications. The value used for the parameter *StrLen_or_IndPtr* should be either SQL_NTS or the string length of the name multiplied by sizeof(WCHAR).|  
|*BufferLength*|SQL_DESC_OCTET_LENGTH in APD.|The length of the table-valued parameter type name in bytes.<br /><br /> This can be SQL_NTS if the type name is null terminated, or 0 if the table-valued parameter type name is not required.|  
|*StrLen_or_IndPtr*|SQL_DESC_OCTET_LENGTH_PTR in APD.|SQL_DESC_OCTET_LENGTH_PTR in APD.<br /><br /> For table-valued parameters, this is a row count rather than a data length.|  
  
 Two data transfer modes are supported for table-valued parameters: fixed row binding and variable row binding.  
  
## Fixed Table-Valued Parameter Row Binding  
 For fixed row binding, an application allocates buffers (or buffer arrays) large enough for all possible input column values. The application does the following:  
  
1.  Binds all parameters by using SQLBindParameter, SQLSetDescRec, or SQLSetDescField calls.  
  
    1.  Sets SQL_DESC_ARRAY_SIZE to the maximum number of rows that can be transferred for each table-valued parameter. This can be done in the SQLBindParameter call.  
  
2.  Calls SQLSetStmtAttr to set SQL_SOPT_SS_PARAM_FOCUS to the ordinal of each table-valued parameter.  
  
    1.  For each table-valued parameter, binds table-valued parameter columns by using SQLBindParameter, SQLSetDescRec, or SQLSetDescField calls.  
  
    2.  For each table-valued parameter column that is to have default values, calls SQLSetDescField to set SQL_CA_SS_COL_HAS_DEFAULT_VALUE to 1.  
  
3.  Calls SQLSetStmtAttr to set SQL_SOPT_SS_PARAM_FOCUS to 0. This must be done before SQLExecute or SQLExecDirect is called. Otherwise, SQL_ERROR is be returned and a diagnostic record is generated with SQLSTATE=HY024 and the message "Invalid attribute value, SQL_SOPT_SS_PARAM_FOCUS (must be zero at execution time)".  
  
4.  Sets *StrLen_or_IndPtr* or SQL_DESC_OCTET_LENGTH_PTR to SQL_DEFAULT_PARAM for a table-valued parameter with no rows, or the number of rows to be transferred on the next call of SQLExecute or SQLExecDirect if the table-valued parameter has rows. *StrLen_or_IndPtr* or SQL_DESC_OCTET_LENGTH_PTR cannot be set to SQL_NULL_DATA for a table-valued parameter as table-valued parameters are not nullable (though table-valued parameter constituent columns may be nullable). If this is set to an invalid value, SQLExecute or SQLExecDirect returns SQL_ERROR, and a diagnostic record is generated with SQLSTATE=HY090 and the message "Invalid string or buffer length for parameter \<p>", where p is the parameter number.  
  
5.  Calls SQLExecute or SQLExecDirect.  
  
 Input table-valued parameter column values can be passed in pieces if *StrLen_or_IndPtr* is set to SQL_LEN_DATA_AT_EXEC(*length*) or SQL_DATA_AT_EXEC for the column. This is similar to passing values in pieces when arrays of parameters are used. As with all data-at-execution parameters, SQLParamData does not indicate which row of the array the driver is requesting data for; the application must take care of this. The application cannot make any assumptions about the order in which the driver will request values.  
  
## Variable Table-Valued Parameter Row Binding  
 For variable row binding, rows are transferred in batches at execution time and the application passes rows to the driver on demand. This is similar to data-at-execution for individual parameter values. For variable row binding, the application does the following:  
  
1.  Binds parameters and table-valued parameter columns as described in steps 1 to 3 of the previous section, "Fixed Table-Valued Parameter Row Binding".  
  
2.  Sets *StrLen_or_IndPtr* or SQL_DESC_OCTET_LENGTH_PTR for any table-valued parameters that are to be passed at execution time to SQL_DATA_AT_EXEC. If neither is set, the parameter will be processed as described in the previous section.  
  
3.  Calls SQLExecute or SQLExecDirect. This will return SQL_NEED_DATA if there are any SQL_PARAM_INPUT or SQL_PARAM_INPUT_OUTPUT parameters to be handled as data-at-execution parameters. In this case, the application does the following:  
  
    -   Calls SQLParamData. This returns the *ParameterValuePtr* value for a data-at-execution parameter and a return code of SQL_NEED_DATA. When all parameter data has been passed to the driver, SQLParamData returns SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, or SQL_ERROR. For data-at-execution parameters, *ParameterValuePtr*, which is the same as the descriptor field SQL_DESC_DATA_PTR, can be regarded as a token to uniquely identify a parameter for which a value is required. This "token" is passed from the application to the driver at bind time, then passed back to the application at execution time.  
  
4.  To send table-valued parameter row data for null table-valued parameters, if the table-valued parameter has no rows, an application calls SQLPutData with *StrLen_or_Ind* set to SQL_DEFAULT_PARAM.  
  
     For non-NULL TVPs, an application:  
  
    -   Sets *Str_Len_or_Ind* for all the table-valued parameter columns to appropriate values, and populates data buffers for table-valued parameter columns that are not to be data-at-execution parameters. You can use data-at-execution for table-valued parameter columns in a similar way to that in which ordinary parameters can be passed to the driver in pieces.  
  
    -   Calls SQLPutData with *Str_Len_or_Ind* set to the number of rows to be sent to the server. Any value outside the range 0 to SQL_DESC_ARRAY_SIZE or SQL_DEFAULT_PARAM is an error, and will return SQLSTATE HY090, with the message "Invalid string or buffer length". 0 indicates that all rows have been sent and there is no more data for a table-valued parameter (as noted in the second bullet item in this list). SQL_DEFAULT_PARAM can only be used the first time the driver requests data for a table-valued parameter (as described in the first bullet item in this list).  
  
5.  When all rows have been sent, calls SQLPutData for the table-valued parameter with a *Str_Len_or_Ind* value of 0, then proceeds to step 3a above.  
  
6.  Calls SQLParamData again. If there are any data-at-execution parameters among the table-valued parameter columns, these will be identified by the value *ValuePtrPtr* returned by SQLParamData. When all column values are available, SQLParamData will again return the *ParameterValuePtr* value for the table-valued parameter, and the application begins again.  
  
## See Also  
 [Table-Valued Parameters &#40;ODBC&#41;](table-valued-parameters-odbc.md)  
  
  
