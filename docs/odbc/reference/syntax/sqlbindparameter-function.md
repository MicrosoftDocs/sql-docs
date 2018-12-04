---
title: "SQLBindParameter Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLBindParameter"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLBindParameter"
helpviewer_keywords: 
  - "SQLBindParameter function [ODBC]"
ms.assetid: 38349d4b-be03-46f9-9d6a-e50dd144e225
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLBindParameter Function
**Conformance**  
 Version Introduced: ODBC 2.0 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLBindParameter** binds a buffer to a parameter marker in an SQL statement. **SQLBindParameter** supports binding to a Unicode C data type, even if the underlying driver does not support Unicode data.  
  
> [!NOTE]  
>  This function replaces the ODBC 1.0 function **SQLSetParam**. For more information, see "Comments."  
  
## Syntax  
  
```  
  
SQLRETURN SQLBindParameter(  
      SQLHSTMT        StatementHandle,  
      SQLUSMALLINT    ParameterNumber,  
      SQLSMALLINT     InputOutputType,  
      SQLSMALLINT     ValueType,  
      SQLSMALLINT     ParameterType,  
      SQLULEN         ColumnSize,  
      SQLSMALLINT     DecimalDigits,  
      SQLPOINTER      ParameterValuePtr,  
      SQLLEN          BufferLength,  
      SQLLEN *        StrLen_or_IndPtr);  
```  
  
## Arguments  
 *StatementHandle*  
 [Input] Statement handle.  
  
 *ParameterNumber*  
 [Input] Parameter number, ordered sequentially in increasing parameter order, starting at 1.  
  
 *InputOutputType*  
 [Input] The type of the parameter. For more information, see "*InputOutputType* Argument" in "Comments."  
  
 *ValueType*  
 [Input] The C data type of the parameter. For more information, see "*ValueType* Argument" in "Comments."  
  
 *ParameterType*  
 [Input] The SQL data type of the parameter. For more information, see "*ParameterType* Argument" in "Comments."  
  
 *ColumnSize*  
 [Input] The size of the column or expression of the corresponding parameter marker. For more information, see "*ColumnSize* Argument" in "Comments."  
  
 If your application will run on a 64-bit Windows operating system, see [ODBC 64-Bit Information](../../../odbc/reference/odbc-64-bit-information.md).  
  
 *DecimalDigits*  
 [Input] The decimal digits of the column or expression of the corresponding parameter marker. For more information about column size, see [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md).  
  
 *ParameterValuePtr*  
 [Deferred Input] A pointer to a buffer for the parameter's data. For more information, see "*ParameterValuePtr* Argument" in "Comments."  
  
 *BufferLength*  
 [Input/Output] Length of the *ParameterValuePtr* buffer in bytes. For more information, see "*BufferLength* Argument" in "Comments."  
  
 See [ODBC 64-Bit Information](../../../odbc/reference/odbc-64-bit-information.md), if your application will run on a 64-bit operating system.  
  
 *StrLen_or_IndPtr*  
 [Deferred Input] A pointer to a buffer for the parameter's length. For more information, see "*StrLen_or_IndPtr* Argument" in "Comments."  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 When **SQLBindParameter** returns SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value can be obtained by calling **SQLGetDiagRec** with a *HandleType* of SQL_HANDLE_STMT and a *Handle* of *StatementHandle*. The following table lists the SQLSTATE values typically returned by **SQLBindParameter** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|07006|Restricted data type attribute violation|The data type identified by the *ValueType* argument cannot be converted to the data type identified by the *ParameterType* argument. Notice that this error may be returned by **SQLExecDirect**, **SQLExecute**, or **SQLPutData** at execution time, instead of by **SQLBindParameter**.|  
|07009|Invalid descriptor index|(DM) The value specified for the argument *ParameterNumber* was less than 1.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the **MessageText* buffer describes the error and its cause.|  
|HY001|Memory allocation  error|The driver was unable to allocate memory that is required to support execution or completion of the function.|  
|HY003|Invalid application buffer type|The value specified by the argument *ValueType* was not a valid C data type or SQL_C_DEFAULT.|  
|HY004|Invalid SQL data type|The value specified for the argument *ParameterType* was neither a valid ODBC SQL data type identifier nor a driver-specific SQL data type identifier supported by the driver.|  
|HY009|Invalid argument value|(DM) The argument *ParameterValuePtr* was a null pointer, the argument *StrLen_or_IndPtr* was a null pointer, and the argument *InputOutputType* was not SQL_PARAM_OUTPUT.<br /><br /> (DM) SQL_PARAM_OUTPUT, where the argument *ParameterValuePtr* was a null pointer, the C type was char or binary, and the BufferLength (*cbValueMax*) was greater than 0.|  
|HY010|Function sequence  error|(DM) An asynchronously executing function was called for the connection handle that is associated with the *StatementHandle*. This asynchronous function was still executing when **SQLBindParameter** was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** was called for the *StatementHandle* and returned SQL_PARAM_DATA_AVAILABLE. This function was called before data was retrieved for all streamed parameters.<br /><br /> (DM) An asynchronously executing function was called for the *StatementHandle* and was still executing when this function was called.<br /><br /> (DM) **SQLExecute**, **SQLExecDirect**, **SQLBulkOperations**, or **SQLSetPos** was called for the *StatementHandle* and returned SQL_NEED_DATA. This function was called before data was sent for all data-at-execution parameters or columns.|  
|HY013|Memory management error|The function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY021|Inconsistent descriptor information|The descriptor information checked during a consistency check was not consistent. (See the "Consistency Checks" section in **SQLSetDescField**.)<br /><br /> The value specified for the argument *DecimalDigits* was outside the range of values supported by the data source for a column of the SQL data type specified by the *ParameterType* argument.|  
|HY090|Invalid string or buffer length|(DM) The value in *BufferLength* was less than 0. (See the description of the SQL_DESC_DATA_PTR field in **SQLSetDescField**.)|  
|HY104|Invalid precision or scale value|The value specified for the argument *ColumnSize* or *DecimalDigits* was outside the range of values supported by the data source for a column of the SQL data type specified by the *ParameterType* argument.|  
|HY105|Invalid parameter type|(DM) The value specified for the argument *InputOutputType* was invalid. (See "Comments.")|  
|HY117|Connection is suspended due to unknown transaction state. Only disconnect and read-only functions are allowed.|(DM) For more information about suspended state, see [SQLEndTran Function](../../../odbc/reference/syntax/sqlendtran-function.md).|  
|HYC00|Optional feature not implemented|The driver or data source does not support the conversion specified by the combination of the value specified for the argument *ValueType* and the driver-specific value specified for the argument *ParameterType*.<br /><br /> The value specified for the argument *ParameterType* was a valid ODBC SQL data type identifier for the version of ODBC supported by the driver but was not supported by the driver or data source.<br /><br /> The driver supports only ODBC 2.*x* and the argument *ValueType* was one of the following:<br /><br /> SQL_C_NUMERIC SQL_C_SBIGINT SQL_C_UBIGINT<br /><br /> and all the interval C data types listed in [C Data Types](../../../odbc/reference/appendixes/c-data-types.md) in Appendix D: Data Types.<br /><br /> The driver only supports ODBC versions prior to 3.50, and the argument *ValueType* was SQL_C_GUID.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver associated with the *StatementHandle* does not support the function.|  
  
## Comments  
 An application calls **SQLBindParameter** to bind each parameter marker in an SQL statement. Bindings remain in effect until the application calls **SQLBindParameter** again, calls **SQLFreeStmt** with the SQL_RESET_PARAMS option, or calls **SQLSetDescField** to set the SQL_DESC_COUNT header field of the APD to 0.  
  
 For more information about parameters, see [Statement Parameters](../../../odbc/reference/develop-app/statement-parameters.md). For more information about parameter data types and parameter markers, see [Parameter Data Types](../../../odbc/reference/appendixes/parameter-data-types.md) and [Parameter Markers](../../../odbc/reference/appendixes/parameter-markers.md) in Appendix C: SQL Grammar.  
  
## ParameterNumber Argument  
 If *ParameterNumber* in the call to **SQLBindParameter** is greater than the value of SQL_DESC_COUNT, **SQLSetDescField** is called to increase the value of SQL_DESC_COUNT to *ParameterNumber*.  
  
## InputOutputType Argument  
 The *InputOutputType* argument specifies the type of the parameter. This argument sets the SQL_DESC_PARAMETER_TYPE field of the IPD. All parameters in SQL statements that do not call procedures, such as **INSERT** statements, are *input**parameters*. Parameters in procedure calls can be input, input/output, or output parameters. (An application calls **SQLProcedureColumns** to determine the type of a parameter in a procedure call; parameters whose type cannot be determined are assumed to be input parameters.)  
  
 The *InputOutputType* argument is one of the following values:  
  
-   SQL_PARAM_INPUT. The parameter marks a parameter in an SQL statement that does not call a procedure, such as an **INSERT** statement, or it marks an input parameter in a procedure. For example, the parameters in **INSERT INTO Employee VALUES (?, ?, ?)** are input parameters, whereas the parameters in **{call AddEmp(?, ?, ?)}** can be, but are not necessarily, input parameters.  
  
     When the statement is executed, the driver sends data for the parameter to the data source; the \**ParameterValuePtr* buffer must contain a valid input value, or the **StrLen_or_IndPtr* buffer must contain SQL_NULL_DATA, SQL_DATA_AT_EXEC, or the result of the SQL_LEN_DATA_AT_EXEC macro.  
  
     If an application cannot determine the type of a parameter in a procedure call, it sets *InputOutputType* to SQL_PARAM_INPUT; if the data source returns a value for the parameter, the driver discards it.  
  
-   SQL_PARAM_INPUT_OUTPUT. The parameter marks an input/output parameter in a procedure. For example, the parameter in **{call GetEmpDept(?)}** is an input/output parameter that accepts an employee's name and returns the name of the employee's department.  
  
     When the statement is executed, the driver sends data for the parameter to the data source; the \**ParameterValuePtr* buffer must contain a valid input value, or the \**StrLen_or_IndPtr* buffer must contain SQL_NULL_DATA, SQL_DATA_AT_EXEC, or the result of the SQL_LEN_DATA_AT_EXEC macro. After the statement is executed, the driver returns data for the parameter to the application; if the data source does not return a value for an input/output parameter, the driver sets the **StrLen_or_IndPtr* buffer to SQL_NULL_DATA.  
  
    > [!NOTE]  
    >  When an ODBC 1.0 application calls **SQLSetParam** in an ODBC 2.0 driver, the Driver Manager converts this to a call to **SQLBindParameter** in which the *InputOutputType* argument is set to SQL_PARAM_INPUT_OUTPUT.  
  
-   SQL_PARAM_OUTPUT. The parameter marks the return value of a procedure or an output parameter in a procedure; in either case, these are known as *output parameters*. For example, the parameter in **{?=call GetNextEmpID}** is an output parameter that returns the next employee ID.  
  
     After the statement is executed, the driver returns data for the parameter to the application, unless the *ParameterValuePtr* and *StrLen_or_IndPtr* arguments are both null pointers, in which case the driver discards the output value. If the data source does not return a value for an output parameter, the driver sets the **StrLen_or_IndPtr* buffer to SQL_NULL_DATA.  
  
-   SQL_PARAM_INPUT_OUTPUT_STREAM. Indicates that an input/output parameter should be streamed. **SQLGetData** can read parameter values in parts. *BufferLength* is ignored because the buffer length will be determined at the call of **SQLGetData**. The value of the *StrLen_or_IndPtr* buffer must contain SQL_NULL_DATA, SQL_DEFAULT_PARAM, SQL_DATA_AT_EXEC, or the result of the SQL_LEN_DATA_AT_EXEC macro. A parameter must be bound as a data-at-execution (DAE) parameter at input if it will be streamed at output. *ParameterValuePtr* can be any non-null pointer value that will be returned by **SQLParamData** as the user-defined token whose value was passed with *ParameterValuePtr* for both input and output. For more information, see [Retrieving Output Parameters Using SQLGetData](../../../odbc/reference/develop-app/retrieving-output-parameters-using-sqlgetdata.md).  
  
-   SQL_PARAM_OUTPUT_STREAM. Same as SQL_PARAM_INPUT_OUTPUT_STREAM, for an output parameter. **StrLen_or_IndPtr* is ignored at input.  
  
 The following table lists different combinations of *InputOutputType* and **StrLen_or_IndPtr*:  
  
|*InputOutputType*|**StrLen_or_IndPtr*|Outcome|Remark on ParameterValuePtr|  
|-----------------------|----------------------------|-------------|---------------------------------|  
|SQL_PARAM_INPUT|SQL_LEN_DATA_AT_EXEC(*len*) or SQL_DATA_AT_EXEC|Input in parts|*ParameterValuePtr* can be any pointer value that will be returned by **SQLParamData** as the user-defined token whose value was passed with *ParameterValuePtr*.|  
|SQL_PARAM_INPUT|Not SQL_LEN_DATA_AT_EXEC(*len*) or SQL_DATA_AT_EXEC|Input bound buffer|*ParameterValuePtr* is the address of the input buffer.|  
|SQL_PARAM_OUTPUT|Ignored at input.|Output bound buffer|*ParameterValuePtr* is the address of the output buffer.|  
|SQL_PARAM_OUTPUT_STREAM|Ignored at input.|Streamed output|*ParameterValuePtr* can be any pointer value, which will be returned by **SQLParamData** as the user-defined token whose value was passed with *ParameterValuePtr*.|  
|SQL_PARAM_INPUT_OUTPUT|SQL_LEN_DATA_AT_EXEC(*len*) or SQL_DATA_AT_EXEC|Input in parts and output bound buffer|*ParameterValuePtr* is the address of the output buffer, which will also be returned by **SQLParamData** as the user-defined token whose value was passed with *ParameterValuePtr*.|  
|SQL_PARAM_INPUT_OUTPUT|Not SQL_LEN_DATA_AT_EXEC(*len*) or SQL_DATA_AT_EXEC|Input bound buffer and output bound buffer|*ParameterValuePtr* is the address of the shared input/output buffer.|  
L_PARAM_INPUT_OUTPUT_STREAM|SQL_LEN_DATA_AT_EXEC(*len*) or SQL_DATA_AT_EXEC|Input in parts and streamed output|*ParameterValuePtr* can be any non-null pointer value, which will be returned by **SQLParamData** as the user-defined token whose value was passed with *ParameterValuePtr* for both input and output.|  
  
> [!NOTE]  
>  The driver must decide which SQL types are allowed when an application binds an output or input-output parameter as streamed. The driver manager will not generate an error for an invalid SQL type.  
  
## ValueType Argument  
 The *ValueType* argument specifies the C data type of the parameter. This argument sets the SQL_DESC_TYPE, SQL_DESC_CONCISE_TYPE, and SQL_DESC_DATETIME_INTERVAL_CODE fields of the APD. This must be one of the values in the [C Data Types](../../../odbc/reference/appendixes/c-data-types.md) section of Appendix D: Data Types.  
  
 If the *ValueType* argument is one of the interval data types, the SQL_DESC_TYPE field of the *ParameterNumber* record of the APD is set to SQL_INTERVAL, the SQL_DESC_CONCISE_TYPE field of the APD is set to the concise interval data type, and the SQL_DESC_DATETIME_INTERVAL_CODE field of the *ParameterNumber* record is set to a subcode for the specific interval data type. (See [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md).) The default interval leading precision (2) and default interval seconds precision (6), as set in the SQL_DESC_DATETIME_INTERVAL_PRECISION and SQL_DESC_PRECISION fields of the APD, respectively, are used for the data. If either default precision is not appropriate, the application should explicitly set the descriptor field by a call to **SQLSetDescField** or **SQLSetDescRec**.  
  
 If the *ValueType* argument is one of the datetime data types, the SQL_DESC_TYPE field of the *ParameterNumber* record of the APD is set to SQL_DATETIME, the SQL_DESC_CONCISE_TYPE field of the *ParameterNumber* record of the APD is set to the concise datetime C data type, and the SQL_DESC_DATETIME_INTERVAL_CODE field of the *ParameterNumber* record is set to a subcode for the specific datetime data type. (See [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md).)  
  
 If the *ValueType* argument is an SQL_C_NUMERIC data type, the default precision (which is driver-defined) and the default scale (0), as set in the SQL_DESC_PRECISION and SQL_DESC_SCALE fields of the APD, are used for the data. If the default precision or scale is not appropriate, the application should explicitly set the descriptor field by a call to **SQLSetDescField** or **SQLSetDescRec**.  
  
 SQL_C_DEFAULT specifies that the parameter value be transferred from the default C data type for the SQL data type specified with *ParameterType*.  
  
 You can also specify an extended C data type. For more information, see [C Data Types in ODBC](../../../odbc/reference/develop-app/c-data-types-in-odbc.md).  
  
 For more information, see [Default C Data Types](../../../odbc/reference/appendixes/default-c-data-types.md), [Converting Data from C to SQL Data Types](../../../odbc/reference/appendixes/converting-data-from-c-to-sql-data-types.md), and [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md) in Appendix D: Data Types.  
  
## ParameterType Argument  
 This must be one of the values listed in the [SQL Data Types](../../../odbc/reference/appendixes/sql-data-types.md) section of Appendix D: Data Types, or it must be a driver-specific value. This argument sets the SQL_DESC_TYPE, SQL_DESC_CONCISE_TYPE, and SQL_DESC_DATETIME_INTERVAL_CODE fields of the IPD.  
  
 If the *ParameterType* argument is one of the datetime identifiers, the SQL_DESC_TYPE field of the IPD is set to SQL_DATETIME, the SQL_DESC_CONCISE_TYPE field of the IPD is set to the concise datetime SQL data type, and the SQL_DESC_DATETIME_INTERVAL_CODE field is set to the appropriate datetime subcode value.  
  
 If *ParameterType* is one of the interval identifiers, the SQL_DESC_TYPE field of the IPD is set to SQL_INTERVAL, the SQL_DESC_CONCISE_TYPE field of the IPD is set to the concise SQL interval data type, and the SQL_DESC_DATETIME_INTERVAL_CODE field of the IPD is set to the appropriate interval subcode. The SQL_DESC_DATETIME_INTERVAL_PRECISION field of the IPD is set to the interval leading precision, and the SQL_DESC_PRECISION field is set to the interval seconds precision, if applicable. If the default value of SQL_DESC_DATETIME_INTERVAL_PRECISION or SQL_DESC_PRECISION is not appropriate, the application should explicitly set it by calling **SQLSetDescField**. For more information about any of these fields, see [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md).  
  
 If the *ValueType* argument is a SQL_NUMERIC data type, the default precision (which is driver-defined) and the default scale (0), as set in the SQL_DESC_PRECISION and SQL_DESC_SCALE fields of the IPD, are used for the data. If the default precision or scale is not appropriate, the application should explicitly set the descriptor field by a call to **SQLSetDescField** or **SQLSetDescRec**.  
  
 For information about how data is converted, see [Converting Data from C to SQL Data Types](../../../odbc/reference/appendixes/converting-data-from-c-to-sql-data-types.md) and [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md) in Appendix D: Data Types.  
  
## ColumnSize Argument  
 The *ColumnSize* argument specifies the size of the column or expression that corresponds to the parameter marker, the length of that data, or both. This argument sets different fields of the IPD, depending on the SQL data type (the *ParameterType* argument). The following rules apply to this mapping:  
  
-   If *ParameterType* is SQL_CHAR, SQL_VARCHAR, SQL_LONGVARCHAR, SQL_BINARY, SQL_VARBINARY, SQL_LONGVARBINARY, or one of the concise SQL datetime or interval data types, the SQL_DESC_LENGTH field of the IPD is set to the value of *ColumnSize*. (For more information, see the [Column Size, Decimal Digits, Transfer Octet Length, and Display Size](../../../odbc/reference/appendixes/column-size-decimal-digits-transfer-octet-length-and-display-size.md) section in Appendix D: Data Types.)  
  
-   If *ParameterType* is SQL_DECIMAL, SQL_NUMERIC, SQL_FLOAT, SQL_REAL, or SQL_DOUBLE, the SQL_DESC_PRECISION field of the IPD is set to the value of *ColumnSize*.  
  
-   For other data types, the *ColumnSize* argument is ignored.  
  
 For more information, see "Passing Parameter Values" and SQL_DATA_AT_EXEC in "*StrLen_or_IndPtr* Argument."  
  
## DecimalDigits Argument  
 If *ParameterType* is SQL_TYPE_TIME, SQL_TYPE_TIMESTAMP, SQL_INTERVAL_SECOND, SQL_INTERVAL_DAY_TO_SECOND, SQL_INTERVAL_HOUR_TO_SECOND, or SQL_INTERVAL_MINUTE_TO_SECOND, the SQL_DESC_PRECISION field of the IPD is set to *DecimalDigits*. If *ParameterType* is SQL_NUMERIC or SQL_DECIMAL, the SQL_DESC_SCALE field of the IPD is set to *DecimalDigits*. For all other data types, the *DecimalDigits* argument is ignored.  
  
## ParameterValuePtr Argument  
 The *ParameterValuePtr* argument points to a buffer that, when **SQLExecute** or **SQLExecDirect** is called, contains the actual data for the parameter. The data must be in the form specified by the *ValueType* argument. This argument sets the SQL_DESC_DATA_PTR field of the APD. An application can set the *ParameterValuePtr* argument to a null pointer, as long as *\*StrLen_or_IndPtr* is SQL_NULL_DATA or SQL_DATA_AT_EXEC. (This applies only to input or input/output parameters.)  
  
 If \**StrLen_or_IndPtr* is the result of the SQL_LEN_DATA_AT_EXEC(*length*) macro or SQL_DATA_AT_EXEC, then *ParameterValuePtr* is an application-defined pointer value that is associated with the parameter. It is returned to the application through **SQLParamData**. For example, *ParameterValuePtr* might be a non-zero token such as a parameter number, a pointer to data, or a pointer to a structure that the application used to bind input parameters. However, note that if the parameter is an input/output parameter, *ParameterValuePtr* must be a pointer to a buffer where the output value will be stored. If the value in the SQL_ATTR_PARAMSET_SIZE statement attribute is greater than 1, the application can use the value pointed to by the SQL_ATTR_PARAMS_PROCESSED_PTR statement attribute together with the *ParameterValuePtr* argument. For example, *ParameterValuePtr* might point to an array of values and the application might use the value pointed to by SQL_ATTR_PARAMS_PROCESSED_PTR to retrieve the correct value from the array. For more information, see "Passing Parameter Values" later in this section.  
  
 If the *InputOutputType* argument is SQL_PARAM_INPUT_OUTPUT or SQL_PARAM_OUTPUT, *ParameterValuePtr* points to a buffer in which the driver returns the output value. If the procedure returns one or more result sets, the \**ParameterValuePtr* buffer is not guaranteed to be set until all result sets/row counts have been processed. If the buffer is not set until processing is complete, the output parameters and return values are unavailable until **SQLMoreResults** returns SQL_NO_DATA. Calling **SQLCloseCursor** or **SQLFreeStmt** with an Option of SQL_CLOSE will cause these values to be discarded.  
  
 If the value in the SQL_ATTR_PARAMSET_SIZE statement attribute is greater than 1, *ParameterValuePtr* points to an array. A single SQL statement processes the complete array of input values for an input or input/output parameter and returns an array of output values for an input/output or output parameter.  
  
## BufferLength Argument  
 For character and binary C data, the *BufferLength* argument specifies the length of the \**ParameterValuePtr* buffer (if it is a single element) or the length of an element in the \**ParameterValuePtr* array (if the value in the SQL_ATTR_PARAMSET_SIZE statement attribute is greater than 1). This argument sets the SQL_DESC_OCTET_LENGTH record field of the APD. If the application specifies multiple values, *BufferLength* is used to determine the location of values in the **ParameterValuePtr* array, both on input and on output. For input/output and output parameters, it is used to determine whether to truncate character and binary C data on output:  
  
-   For character C data, if the number of bytes available to return is greater than or equal to *BufferLength*, the data in \**ParameterValuePtr* is truncated to *BufferLength* less the length of a null-termination character and is null-terminated by the driver.  
  
-   For binary C data, if the number of bytes available to return is greater than *BufferLength*, the data in \**ParameterValuePtr* is truncated to *BufferLength* bytes.  
  
 For all other types of C data, the *BufferLength* argument is ignored. The length of the \**ParameterValuePtr* buffer (if it is a single element) or the length of an element in the \**ParameterValuePtr* array (if the application calls **SQLSetStmtAttr** with an *Attribute* argument of SQL_ATTR_PARAMSET_SIZE to specify multiple values for each parameter) is assumed to be the length of the C data type.  
  
 For streamed output or streamed input/output parameters, the *BufferLength* argument is ignored because the buffer length is specified in **SQLGetData**.  
  
> [!NOTE]  
>  When an ODBC 1.0 application calls **SQLSetParam** in an ODBC 3.*x* driver, the Driver Manager converts this to a call to **SQLBindParameter** in which the *BufferLength* argument is always SQL_SETPARAM_VALUE_MAX. Because the Driver Manager returns an error if an ODBC 3.*x* application sets *BufferLength* to SQL_SETPARAM_VALUE_MAX, an ODBC 3.*x* driver can use this to determine when it is called by an ODBC 1.0 application.  
  
> [!NOTE]  
>  In **SQLSetParam**, the way in which an application specifies the length of the **ParameterValuePtr* buffer so that the driver can return character or binary data, and the way in which an application sends an array of character or binary parameter values to the driver, are driver-defined.  
  
## StrLen_or_IndPtr Argument  
 The *StrLen_or_IndPtr* argument points to a buffer that, when **SQLExecute** or **SQLExecDirect** is called, contains one of the following. (This argument sets the SQL_DESC_OCTET_LENGTH_PTR and SQL_DESC_INDICATOR_PTR record fields of the application parameter pointers.)  
  
-   The length of the parameter value stored in **ParameterValuePtr*. This is ignored except for character or binary C data.  
  
-   SQL_NTS. The parameter value is a null-terminated string.  
  
-   SQL_NULL_DATA. The parameter value is NULL.  
  
-   SQL_DEFAULT_PARAM. A procedure is to use the default value of a parameter, instead of a value retrieved from the application. This value is valid only in a procedure called in ODBC canonical syntax, and then only if the *InputOutputType* argument is SQL_PARAM_INPUT, SQL_PARAM_INPUT_OUTPUT, or SQL_PARAM_INPUT_OUTPUT_STREAM. When \**StrLen_or_IndPtr* is SQL_DEFAULT_PARAM, the *ValueType*, *ParameterType*, *ColumnSize*, *DecimalDigits*, *BufferLength*, and *ParameterValuePtr* arguments are ignored for input parameters and are used only to define the output parameter value for input/output parameters.  
  
-   The result of the SQL_LEN_DATA_AT_EXEC(*length*) macro. The data for the parameter will be sent with **SQLPutData**. If the *ParameterType* argument is SQL_LONGVARBINARY, SQL_LONGVARCHAR, or a long, data source-specific data type, and the driver returns "Y" for the SQL_NEED_LONG_DATA_LEN information type in **SQLGetInfo**, *length* is the number of bytes of data to be sent for the parameter; otherwise, *length* must be a nonnegative value and is ignored. For more information, see "Passing Parameter Values," later in this section.  
  
     For example, to specify that 10,000 bytes of data will be sent with **SQLPutData** in one or more calls, for an SQL_LONGVARCHAR parameter, an application sets **StrLen_or_IndPtr* to SQL_LEN_DATA_AT_EXEC(10000).  
  
-   SQL_DATA_AT_EXEC. The data for the parameter will be sent with **SQLPutData**. This value is used by ODBC 1.0 applications when they call ODBC 3.*x* drivers. For more information, see "Passing Parameter Values," later in this section.  
  
 If *StrLen_or_IndPtr* is a null pointer, the driver assumes that all input parameter values are non-NULL and that character and binary data is null-terminated. If *InputOutputType* is SQL_PARAM_OUTPUT or SQL_PARAM_OUTPUT_STREAM and *ParameterValuePtr* and *StrLen_or_IndPtr* are both null pointers, the driver discards the output value.  
  
> [!NOTE]  
>  Application developers are strongly discouraged from specifying a null pointer for *StrLen_or_IndPtr* when the data type of the parameter is SQL_C_BINARY. To make sure that a driver does not unexpectedly truncate SQL_C_BINARY data, *StrLen_or_IndPtr* should contain a pointer to a valid length value.  
  
 If the *InputOutputType* argument is SQL_PARAM_INPUT_OUTPUT, SQL_PARAM_OUTPUT, SQL_PARAM_INPUT_OUTPUT_STREAM, or SQL_PARAM_OUTPUT_STREAM, *StrLen_or_IndPtr* points to a buffer in which the driver returns SQL_NULL_DATA, the number of bytes available to return in \**ParameterValuePtr* (excluding the null-termination byte of character data), or SQL_NO_TOTAL (if the number of bytes available to return cannot be determined). If the procedure returns one or more result sets, the **StrLen_or_IndPtr* buffer is not guaranteed to be set until all results have been fetched.  
  
 If the value in the SQL_ATTR_PARAMSET_SIZE statement attribute is greater than 1, *StrLen_or_IndPtr* points to an array of SQLLEN values. These can be any of the values listed earlier in this section and are processed with a single SQL statement.  
  
## Passing Parameter Values  
 An application can pass the value for a parameter either in the \**ParameterValuePtr* buffer or with one or more calls to **SQLPutData**. Parameters whose data is passed with **SQLPutData** are known as *data-at-execution* parameters. These are typically used to send data for SQL_LONGVARBINARY and SQL_LONGVARCHAR parameters, and can be mixed with other parameters.  
  
 To pass parameter values, an application performs the following sequence of steps:  
  
1.  Calls **SQLBindParameter** for each parameter to bind buffers for the parameter's value (*ParameterValuePtr* argument) and length/indicator (*StrLen_or_IndPtr* argument). For data-at-execution parameters, *ParameterValuePtr* is an application-defined pointer value such as a parameter number or a pointer to data. The value will be returned later and can be used to identify the parameter.  
  
2.  Places values for input and input/output parameters in the \**ParameterValuePtr* and **StrLen_or_IndPtr* buffers:  
  
    -   For normal parameters, the application places the parameter value in the \**ParameterValuePtr* buffer and the length of that value in the **StrLen_or_IndPtr* buffer. For more information, see [Setting Parameter Values](../../../odbc/reference/develop-app/setting-parameter-values.md).  
  
    -   For data-at-execution parameters, the application puts the result of the SQL_LEN_DATA_AT_EXEC(*length*) macro (when calling an ODBC 2.0 driver) in the **StrLen_or_IndPtr* buffer.  
  
3.  Calls **SQLExecute** or **SQLExecDirect** to execute the SQL statement.  
  
    -   If there are no data-at-execution parameters, the process is complete.  
  
    -   If there are any data-at-execution parameters, the function returns SQL_NEED_DATA.  
  
4.  Calls **SQLParamData** to retrieve the application-defined value specified in the *ParameterValuePtr* argument of **SQLBindParameter** for the first data-at-execution parameter to be processed. **SQLParamData** returns SQL_NEED_DATA.  
  
    > [!NOTE]  
    >  Although data-at-execution parameters resemble data-at-execution columns, the value returned by **SQLParamData** is different for each. Data-at-execution parameters are parameters in an SQL statement for which data will be sent with **SQLPutData** when the statement is executed with **SQLExecDirect** or **SQLExecute**. They are bound with **SQLBindParameter**. The value returned by **SQLParamData** is a pointer value passed to **SQLBindParameter** in the *ParameterValuePtr* argument. Data-at-execution columns are columns in a rowset for which data will be sent with **SQLPutData** when a row is updated or added with **SQLBulkOperations** or updated with **SQLSetPos**. They are bound with **SQLBindCol**. The value returned by **SQLParamData** is the address of the row in the **TargetValuePtr* buffer (set by a call to **SQLBindCol**) that is being processed.  
  
5.  Calls **SQLPutData** one or more times to send data for the parameter. More than one call is needed if the data value is larger than the \**ParameterValuePtr* buffer specified in **SQLPutData**; multiple calls to **SQLPutData** for the same parameter are allowed only when sending character C data to a column with a character, binary, or data source-specific data type or when sending binary C data to a column with a character, binary, or data source-specific data type.  
  
6.  Calls **SQLParamData** again to signal that all data has been sent for the parameter.  
  
    -   If there are more data-at-execution parameters, **SQLParamData** returns SQL_NEED_DATA and the application-defined value for the next data-at-execution parameter to be processed. The application repeats steps 4 and 5.  
  
    -   If there are no more data-at-execution parameters, the process is complete. If the statement was successfully executed, **SQLParamData** returns SQL_SUCCESS or SQL_SUCCESS_WITH_INFO; if the execution failed, it returns SQL_ERROR. At this point, **SQLParamData** can return any SQLSTATE that can be returned by the function that is used to execute the statement (**SQLExecDirect** or **SQLExecute**).  
  
         Output values for any input/output or output parameters are available in the \**ParameterValuePtr* and **StrLen_or_IndPtr* buffers after the application retrieves all result sets generated by the statement.  
  
 Calling **SQLExecute** or **SQLExecDirect** puts the statement in an SQL_NEED_DATA state. At this point, the application can call only **SQLCancel**, **SQLGetDiagField**, **SQLGetDiagRec**, **SQLGetFunctions**, **SQLParamData**, or **SQLPutData** with the statement or the *connection handle* associated with the statement. If it calls any other function with the statement or the connection associated with the statement, the function returns SQLSTATE HY010 (Function sequence error). The statement leaves the SQL_NEED_DATA state when **SQLParamData** or **SQLPutData** returns an error, **SQLParamData** returns SQL_SUCCESS or SQL_SUCCESS_WITH_INFO, or the statement is canceled.  
  
 If the application calls **SQLCancel** while the driver still needs data for data-at-execution parameters, the driver cancels statement execution; the application can then call **SQLExecute** or **SQLExecDirect** again.  
  
## Retrieving Streamed Output Parameters  
 When an application sets *InputOutputType* to SQL_PARAM_INPUT_OUTPUT_STREAM or SQL_PARAM_OUTPUT_STREAM, the output parameter value must be retrieved by one or more calls to **SQLGetData**. When the driver has a streamed output parameter value to return to the application, it will return SQL_PARAM_DATA_AVAILABLE in response to a call to the following functions: **SQLMoreResults**, **SQLExecute**, and **SQLExecDirect**. An application calls **SQLParamData** to determine which parameter value is available.  
  
 For more information about SQL_PARAM_DATA_AVAILABLE and streamed output parameters, see [Retrieving Output Parameters Using SQLGetData](../../../odbc/reference/develop-app/retrieving-output-parameters-using-sqlgetdata.md).  
  
## Using Arrays of Parameters  
 When an application prepares a statement with parameter markers and passes in an array of parameters, there are two different ways this can be executed. One way is for the driver to rely on the array-processing capabilities of the back end, in which case the whole statement with the array of parameters is treated as one atomic unit. Oracle is an example of a data source that supports array processing capabilities. Another way to implement this feature is for the driver to generate a batch of SQL statements, one SQL statement for each set of parameters in the parameter array, and execute the batch. Arrays of parameters cannot be used with an **UPDATE WHERE CURRENT OF** statement.  
  
 When an array of parameters is processed, individual result sets/row counts (one for each parameter set) can be available or result sets/rows counts can be rolled up into one. The SQL_PARAM_ARRAY_ROW_COUNTS option in **SQLGetInfo** indicates whether row counts are available for each set of parameters (SQL_PARC_BATCH) or only one row count is available (SQL_PARC_NO_BATCH).  
  
 The SQL_PARAM_ARRAY_SELECTS option in **SQLGetInfo** indicates whether a result set is available for each set of parameters (SQL_PAS_BATCH) or only one result set is available (SQL_PAS_NO_BATCH). If the driver does not allow a result set-generating statement to be executed with an array of parameters, SQL_PARAM_ARRAY_SELECTS returns SQL_PAS_NO_SELECT.  
  
 For more information, see [SQLGetInfo Function](../../../odbc/reference/syntax/sqlgetinfo-function.md).  
  
 To support arrays of parameters, the SQL_ATTR_PARAMSET_SIZE statement attribute is set to specify the number of values for each parameter. If the field is greater than 1, the SQL_DESC_DATA_PTR, SQL_DESC_INDICATOR_PTR, and SQL_DESC_OCTET_LENGTH_PTR fields of the APD must point to arrays. The cardinality of each array is equal to the value of SQL_ATTR_PARAMSET_SIZE.  
  
 The SQL_DESC_ROWS_PROCESSED_PTR field of the APD points to a buffer that contains the number of sets of parameters that have been processed, including error sets. As each set of parameters is processed, the driver stores a new value in the buffer. No number will be returned if this is a null pointer. When arrays of parameters are used, the value pointed to by the SQL_DESC_ROWS_PROCESSED_PTR field of the APD is populated even if SQL_ERROR is returned by the setting function. If SQL_NEED_DATA is returned, the value pointed to by the SQL_DESC_ROWS_PROCESSED_PTR field of the APD is set to the set of parameters that is being processed.  
  
 What occurs when an array of parameters is bound and an **UPDATE WHERE CURRENT OF** statement is executed is driver-defined.  
  
## Column-Wise Parameter Binding  
 In column-wise binding, the application binds separate parameter and length/indicator arrays to each parameter.  
  
 To use column-wise binding, the application first sets the SQL_ATTR_PARAM_BIND_TYPE statement attribute to SQL_PARAM_BIND_BY_COLUMN. (This is the default.) For each column to be bound, the application performs the following steps:  
  
1.  Allocates a parameter buffer array.  
  
2.  Allocates an array of length/indicator buffers.  
  
    > [!NOTE]  
    >  If the application writes directly to descriptors when column-wise binding is used, separate arrays can be used for length and indicator data.  
  
3.  Calls **SQLBindParameter** with the following arguments:  
  
    -   *ValueType* is the C type of a single element in the parameter buffer array.  
  
    -   *ParameterType* is the SQL type of the parameter.  
  
    -   *ParameterValuePtr* is the address of the parameter buffer array.  
  
    -   *BufferLength* is the size of a single element in the parameter buffer array. The *BufferLength* argument is ignored when the data is fixed-length data.  
  
    -   *StrLen_or_IndPtr* is the address of the length/indicator array.  
  
 For more information about how this information is used, see "ParameterValuePtr Argument" in "Comments," later in this section. For more information about column-wise binding of parameters, see [Binding Arrays of Parameters](../../../odbc/reference/develop-app/binding-arrays-of-parameters.md).  
  
## Row-Wise Parameter Binding  
 In row-wise binding, the application defines a structure that contains parameter and length/indicator buffers for each parameter to be bound.  
  
 To use row-wise binding, the application performs the following steps:  
  
1.  Defines a structure to hold a single set of parameters (including both parameter and length/indicator buffers) and allocates an array of these structures.  
  
    > [!NOTE]  
    >  If the application writes directly to descriptors when row-wise binding is used, separate fields can be used for length and indicator data.  
  
2.  Sets the SQL_ATTR_PARAM_BIND_TYPE statement attribute to the size of the structure that contains a single set of parameters or to the size of an instance of a buffer into which the parameters will be bound. The length must include space for all the bound parameters, and any padding of the structure or buffer, to make sure that when the address of a bound parameter is incremented with the specified length, the result will point to the beginning of the same parameter in the next row. When you use the *sizeof* operator in ANSI C, this behavior is guaranteed.  
  
3.  Calls **SQLBindParameter** with the following arguments for each parameter to be bound:  
  
    -   *ValueType* is the type of the parameter buffer member to be bound to the column.  
  
    -   *ParameterType* is the SQL type of the parameter.  
  
    -   *ParameterValuePtr* is the address of the parameter buffer member in the first array element.  
  
    -   *BufferLength* is the size of the parameter buffer member.  
  
    -   *StrLen_or_IndPtr* is the address of the length/indicator member to be bound.  
  
 For more information about how this information is used, see "*ParameterValuePtr* Argument," later in this section. For more information about row-wise binding of parameters, see the [Binding Arrays of Parameters](../../../odbc/reference/develop-app/binding-arrays-of-parameters.md).  
  
## Error Information  
 If a driver does not implement parameter arrays as batches (the SQL_PARAM_ARRAY_ROW_COUNTS option is equal to SQL_PARC_NO_BATCH), error situations are handled as if one statement were executed. If the driver does implement parameter arrays as batches, an application can use the SQL_DESC_ARRAY_STATUS_PTR header field of the IPD to determine which parameter of an SQL statement or which parameter in an array of parameters caused **SQLExecDirect** or **SQLExecute** to return an error. This field contains status information for each row of parameter values. If the field indicates that an error has occurred, fields in the diagnostic data structure will indicate the row and parameter number of the parameter that failed. The number of elements in the array will be defined by the SQL_DESC_ARRAY_SIZE header field in the APD, which can be set by the SQL_ATTR_PARAMSET_SIZE statement attribute.  
  
> [!NOTE]  
>  The SQL_DESC_ARRAY_STATUS_PTR header field in the APD is used to ignore parameters. For more information about ignoring parameters, see the next section, "Ignoring a Set of Parameters."  
  
 When **SQLExecute** or **SQLExecDirect** returns SQL_ERROR, the elements in the array pointed to by the SQL_DESC_ARRAY_STATUS_PTR field in the IPD will contain SQL_PARAM_ERROR, SQL_PARAM_SUCCESS, SQL_PARAM_SUCCESS_WITH_INFO, SQL_PARAM_UNUSED, or SQL_PARAM_DIAG_UNAVAILABLE.  
  
 For each element in this array, the diagnostic data structure contains one or more status records. The SQL_DIAG_ROW_NUMBER field of the structure indicates the row number of the parameter values that caused the error. If it is possible to determine the particular parameter in a row of parameters that caused the error, the parameter number will be entered in the SQL_DIAG_COLUMN_NUMBER field.  
  
 SQL_PARAM_UNUSED is entered when a parameter has not been used because an error occurred in an earlier parameter that forced **SQLExecute** or **SQLExecDirect** to abort. For example, if there are 50 parameters and an error occurred while executing the fortieth set of parameters that caused **SQLExecute** or **SQLExecDirect** to abort, then SQL_PARAM_UNUSED is entered in the status array for parameters 41 through 50.  
  
 SQL_PARAM_DIAG_UNAVAILABLE is entered when the driver treats arrays of parameters as a monolithic unit, so it does not generate this individual parameter level of error information.  
  
 Some errors in the processing of a single set of parameters cause processing of the subsequent sets of parameters in the array to stop. Other errors do not affect the processing of subsequent parameters. Which errors will stop processing is driver-defined. If processing is not stopped, all parameters in the array are processed, SQL_SUCCESS_WITH_INFO is returned as a result of the error, and the buffer defined by SQL_ATTR_PARAMS_PROCESSED_PTR is set to the total number of sets of parameters processed (as defined by the SQL_ATTR_PARAMSET_SIZE statement attribute), which includes error sets.  
  
> [!CAUTION]  
>  ODBC behavior when an error occurs in the processing of an array of parameters is different in ODBC 3.*x* than it was in ODBC 2.*x*. In ODBC 2.*x*, the function returned SQL_ERROR and processing ceased. The buffer pointed to by the *pirow* argument of **SQLParamOptions** contained the number of the error row. In ODBC 3.*x*, the function returns SQL_SUCCESS_WITH_INFO and processing may either stop or continue. If it continues, the buffer specified by SQL_ATTR_PARAMS_PROCESSED_PTR will be set to the value of all parameters processed, including those that resulted in an error. This change in behavior may cause problems for existing applications.  
  
 When **SQLExecute** or **SQLExecDirect** returns before completing the processing of all parameter sets in a parameter array, such as when SQL_ERROR or SQL_NEED_DATA is returned, the status array contains statuses for those parameters that have already been processed. The location pointed to by the SQL_DESC_ROWS_PROCESSED_PTR field in the IPD contains the row number in the parameter array that caused the SQL_ERROR or SQL_NEED_DATA error code. When an array of parameters is sent to a SELECT statement, the availability of status array values is driver-defined; they may be available after the statement has been executed or as result sets are fetched.  
  
## Ignoring a Set of Parameters  
 The SQL_DESC_ARRAY_STATUS_PTR field of the APD (as set by the SQL_ATTR_PARAM_STATUS_PTR statement attribute) can be used to indicate that a set of bound parameters in an SQL statement should be ignored. To direct the driver to ignore one or more sets of parameters during execution, an application should follow these steps:  
  
1.  Call **SQLSetDescField** to set the SQL_DESC_ARRAY_STATUS_PTR header field of the APD to point to an array of SQLUSMALLINT values to contain status information. This field can also be set by calling **SQLSetStmtAttr** with an *Attribute* of SQL_ATTR_PARAM_OPERATION_PTR, which allows an application to set the field without obtaining a descriptor handle.  
  
2.  Set each element of the array defined by the SQL_DESC_ARRAY_STATUS_PTR field of the APD to one of two values:  
  
    -   SQL_PARAM_IGNORE, to indicate that the row is excluded from statement execution.  
  
    -   SQL_PARAM_PROCEED, to indicate that the row is included in statement execution.  
  
3.  Call **SQLExecDirect** or **SQLExecute** to execute the prepared statement.  
  
 The following rules apply to the array defined by the SQL_DESC_ARRAY_STATUS_PTR field of the APD:  
  
-   The pointer is set to null by default.  
  
-   If the pointer is null, all sets of parameters are used, as if all elements were set to SQL_ROW_PROCEED.  
  
-   Setting an element to SQL_PARAM_PROCEED does not guarantee that the operation will use that particular set of parameters.  
  
-   SQL_PARAM_PROCEED is defined as 0 in the header file.  
  
 An application can set the SQL_DESC_ARRAY_STATUS_PTR field in the APD to point to the same array as that pointed to by the SQL_DESC_ARRAY_STATUS_PTR field in the IRD. This is useful when binding parameters to row data. Parameters can then be ignored according to the status of the row data. In addition to SQL_PARAM_IGNORE, the following codes cause a parameter in an SQL statement to be ignored: SQL_ROW_DELETED, SQL_ROW_UPDATED, and SQL_ROW_ERROR. In addition to SQL_PARAM_PROCEED, the following codes cause an SQL statement to proceed: SQL_ROW_SUCCESS, SQL_ROW_SUCCESS_WITH_INFO, and SQL_ROW_ADDED.  
  
## Rebinding Parameters  
 An application can perform either of two operations to change a binding:  
  
-   Call **SQLBindParameter** to specify a new binding for a column that is already bound. The driver overwrites the old binding with the new one.  
  
-   Specify an offset to be added to the buffer address that was specified by the binding call to **SQLBindParameter**. For more information, see the next section, "Rebinding with Offsets."  
  
## Rebinding with Offsets  
 Rebinding of parameters is especially useful when an application has a buffer area setup that can contain many parameters but a call to **SQLExecDirect** or **SQLExecute** uses only a few of the parameters. The remaining space in the buffer area can be used for the next set of parameters by modifying the existing binding by an offset.  
  
 The SQL_DESC_BIND_OFFSET_PTR header field in the APD points to the binding offset. If the field is non-null, the driver dereferences the pointer and, if none of the values in the SQL_DESC_DATA_PTR, SQL_DESC_INDICATOR_PTR, and SQL_DESC_OCTET_LENGTH_PTR fields is a null pointer, adds the dereferenced value to those fields in the descriptor records at execution time. The new pointer values are used when the SQL statements are executed. The offset remains valid after rebinding. Because SQL_DESC_BIND_OFFSET_PTR is a pointer to the offset rather than the offset itself, an application can change the offset directly, without having to call [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md) or [SQLSetDescRec](../../../odbc/reference/syntax/sqlsetdescrec-function.md) to change the descriptor field. The pointer is set to null by default. The SQL_DESC_BIND_OFFSET_PTR field of the ARD can be set by a call to [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md) or by a call to [SQLSetStmtAttr](../../../odbc/reference/syntax/sqlsetstmtattr-function.md)with an *fAttribute* of SQL_ATTR_PARAM_BIND_OFFSET_PTR.  
  
 The binding offset is always added directly to the values in the SQL_DESC_DATA_PTR, SQL_DESC_INDICATOR_PTR, and SQL_DESC_OCTET_LENGTH_PTR fields. If the offset is changed to a different value, the new value is still added directly to the value in each descriptor field. The new offset is not added to the sum of the field value and any earlier offsets.  
  
## Descriptors  
 How a parameter is bound is determined by fields of the APDs and IPDs. The arguments in **SQLBindParameter** are used to set those descriptor fields. The fields can also be set by the **SQLSetDescField** functions, although **SQLBindParameter** is more efficient to use because the application does not have to obtain a descriptor handle to call **SQLBindParameter**.  
  
> [!CAUTION]  
>  Calling **SQLBindParameter** for one statement can affect other statements. This occurs when the ARD associated with the statement is explicitly allocated and is also associated with other statements. Because **SQLBindParameter** modifies the fields of the APD, the modifications apply to all statements with which this descriptor is associated. If this is not the required behavior, the application should dissociate this descriptor from the other statements before it calls **SQLBindParameter**.  
  
 Conceptually, **SQLBindParameter** performs the following steps in sequence:  
  
1.  Calls [SQLGetStmtAttr](../../../odbc/reference/syntax/sqlgetstmtattr-function.md) to obtain the APD handle.  
  
2.  Calls [SQLGetDescField](../../../odbc/reference/syntax/sqlgetdescfield-function.md) to get the APD's SQL_DESC_COUNT field, and if the value of the *ColumnNumber* argument exceeds the value of SQL_DESC_COUNT, calls **SQLSetDescField** to increase the value of SQL_DESC_COUNT to *ColumnNumber*.  
  
3.  Calls [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md) multiple times to assign values to the following fields of the APD:  
  
    -   Sets SQL_DESC_TYPE and SQL_DESC_CONCISE_TYPE to the value of *ValueType*, except that if *ValueType* is one of the concise identifiers of a datetime or interval subtype, it sets SQL_DESC_TYPE to SQL_DATETIME or SQL_INTERVAL, respectively, sets SQL_DESC_CONCISE_TYPE to the concise identifier, and sets SQL_DESC_DATETIME_INTERVAL_CODE to the corresponding datetime or interval subcode.  
  
    -   Sets the SQL_DESC_OCTET_LENGTH field to the value of *BufferLength*.  
  
    -   Sets the SQL_DESC_DATA_PTR field to the value of *ParameterValue*.  
  
    -   Sets the SQL_DESC_OCTET_LENGTH_PTR field to the value of *StrLen_or_Ind*.  
  
    -   Sets the SQL_DESC_INDICATOR_PTR field also to the value of *StrLen_or_Ind*.  
  
     The *StrLen_or_Ind* parameter specifies both the indicator information and the length for the parameter value.  
  
4.  Calls [SQLGetStmtAttr](../../../odbc/reference/syntax/sqlgetstmtattr-function.md) to obtain the IPD handle.  
  
5.  Calls [SQLGetDescField](../../../odbc/reference/syntax/sqlgetdescfield-function.md) to get the IPD's SQL_DESC_COUNT field, and if the value of the *ColumnNumber* argument exceeds the value of SQL_DESC_COUNT, calls **SQLSetDescField** to increase the value of SQL_DESC_COUNT to *ColumnNumber*.  
  
6.  Calls [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md) multiple times to assign values to the following fields of the IPD:  
  
    -   Sets SQL_DESC_TYPE and SQL_DESC_CONCISE_TYPE to the value of *ParameterType*, except that if *ParameterType* is one of the concise identifiers of a datetime or interval subtype, it sets SQL_DESC_TYPE to SQL_DATETIME or SQL_INTERVAL, respectively, sets SQL_DESC_CONCISE_TYPE to the concise identifier, and sets SQL_DESC_DATETIME_INTERVAL_CODE to the corresponding datetime or interval subcode.  
  
    -   Sets one or more of SQL_DESC_LENGTH, SQL_DESC_PRECISION, and SQL_DESC_DATETIME_INTERVAL_PRECISION, as appropriate for *ParameterType*.  
  
    -   Sets SQL_DESC_SCALE to the value of *DecimalDigits*.  
  
 If the call to **SQLBindParameter** fails, the content of the descriptor fields that it would have set in the APD are undefined, and the SQL_DESC_COUNT field of the APD is unchanged. In addition, the SQL_DESC_LENGTH, SQL_DESC_PRECISION, SQL_DESC_SCALE, and SQL_DESC_TYPE fields of the appropriate record in the IPD are undefined and the SQL_DESC_COUNT field of the IPD is unchanged.  
  
## Conversion of Calls to and from SQLSetParam  
 When an ODBC 1.0 application calls **SQLSetParam** in an ODBC 3.*x* driver, the ODBC 3.*x* Driver Manager maps the call as shown in the following table.  
  
|Call by ODBC 1.0 application|Call to ODBC 3.*x* driver|  
|----------------------------------|-------------------------------|  
|SQLSetParam(      StatementHandle,      ParameterNumber,      ValueType,      ParameterType,      LengthPrecision,      ParameterScale,      ParameterValuePtr,      StrLen_or_IndPtr);|SQLBindParameter(      StatementHandle,      ParameterNumber,      SQL_PARAM_INPUT_OUTPUT,      ValueType,      ParameterType,      *ColumnSize*,      *DecimalDigits*,      ParameterValuePtr,      SQL_SETPARAM_VALUE_MAX,      StrLen_or_IndPtr);|  
  
## Code Example  
 In the following example, an application prepares an SQL statement to insert data into the ORDERS table. For each parameter in the statement, the application calls **SQLBindParameter** to specify the ODBC C data type and the SQL data type of the parameter, and to bind a buffer to each parameter. For each row of data, the application assigns data values to each parameter and calls **SQLExecute** to execute the statement.  
  
 The following sample assumes that you have an ODBC data source on your computer called Northwind that is associated with the Northwind database.  
  
 For more code examples, see [SQLBulkOperations Function](../../../odbc/reference/syntax/sqlbulkoperations-function.md), [SQLProcedures Function](../../../odbc/reference/syntax/sqlprocedures-function.md), [SQLPutData Function](../../../odbc/reference/syntax/sqlputdata-function.md), and [SQLSetPos Function](../../../odbc/reference/syntax/sqlsetpos-function.md).  
  
```  
// SQLBindParameter_Function.cpp  
// compile with: ODBC32.lib  
#include <windows.h>  
#include <sqltypes.h>  
#include <sqlext.h>  
  
#define EMPLOYEE_ID_LEN 10  
  
SQLHENV henv = NULL;  
SQLHDBC hdbc = NULL;  
SQLRETURN retcode;  
SQLHSTMT hstmt = NULL;  
SQLSMALLINT sCustID;  
  
SQLCHAR szEmployeeID[EMPLOYEE_ID_LEN];  
SQL_DATE_STRUCT dsOrderDate;  
SQLINTEGER cbCustID = 0, cbOrderDate = 0, cbEmployeeID = SQL_NTS;  
  
int main() {  
   retcode = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &henv);  
   retcode = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER*)SQL_OV_ODBC3, 0);   
  
   retcode = SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc);   
   retcode = SQLSetConnectAttr(hdbc, SQL_LOGIN_TIMEOUT, (SQLPOINTER)5, 0);  
  
   retcode = SQLConnect(hdbc, (SQLCHAR*) "Northwind", SQL_NTS, (SQLCHAR*) NULL, 0, NULL, 0);  
   retcode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt);  
  
   retcode = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_CHAR, EMPLOYEE_ID_LEN, 0, szEmployeeID, 0, &cbEmployeeID);  
   retcode = SQLBindParameter(hstmt, 2, SQL_PARAM_INPUT, SQL_C_SSHORT, SQL_INTEGER, 0, 0, &sCustID, 0, &cbCustID);  
   retcode = SQLBindParameter(hstmt, 3, SQL_PARAM_INPUT, SQL_C_TYPE_DATE, SQL_TIMESTAMP, sizeof(dsOrderDate), 0, &dsOrderDate, 0, &cbOrderDate);  
  
   retcode = SQLPrepare(hstmt, (SQLCHAR*)"INSERT INTO Orders(CustomerID, EmployeeID, OrderDate) VALUES (?, ?, ?)", SQL_NTS);  
  
   strcpy_s((char*)szEmployeeID, _countof(szEmployeeID), "BERGS");  
   sCustID = 5;  
   dsOrderDate.year = 2006;  
   dsOrderDate.month = 3;  
   dsOrderDate.day = 17;  
  
   retcode = SQLExecute(hstmt);  
}  
```  
  
## Code Example  
 In the following example, an application executes a SQL Server stored procedure using a named parameter.  
  
```  
// SQLBindParameter_Function_2.cpp  
// compile with: ODBC32.lib  
// sample assumes the following stored procedure:  
// use northwind  
// DROP PROCEDURE SQLBindParameter  
// GO  
//   
// CREATE PROCEDURE SQLBindParameter @quote int  
// AS  
// delete from orders where OrderID >= @quote  
// GO  
#include <windows.h>  
#include <sqltypes.h>  
#include <sqlext.h>  
  
SQLHDESC hIpd = NULL;  
SQLHENV henv = NULL;  
SQLHDBC hdbc = NULL;  
SQLRETURN retcode;  
SQLHSTMT hstmt = NULL;  
SQLCHAR szQuote[50] = "100084";  
SQLINTEGER cbValue = SQL_NTS;  
  
int main() {  
   retcode = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &henv);  
   retcode = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER*)SQL_OV_ODBC3, 0);   
  
   retcode = SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc);   
   retcode = SQLSetConnectAttr(hdbc, SQL_LOGIN_TIMEOUT, (SQLPOINTER)5, 0);  
  
   retcode = SQLConnect(hdbc, (SQLCHAR*) "Northwind", SQL_NTS, (SQLCHAR*) NULL, 0, NULL, 0);  
   retcode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt);  
  
   retcode = SQLPrepare(hstmt, (SQLCHAR*)"{call SQLBindParameter(?)}", SQL_NTS);  
   retcode = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_CHAR, 50, 0, szQuote, 0, &cbValue);  
   retcode = SQLGetStmtAttr(hstmt, SQL_ATTR_IMP_PARAM_DESC, &hIpd, 0, 0);  
   retcode = SQLSetDescField(hIpd, 1, SQL_DESC_NAME, "@quote", SQL_NTS);  
  
   retcode = SQLExecute(hstmt);  
}  
```  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Returning information about a parameter in  a statement|[SQLDescribeParam Function](../../../odbc/reference/syntax/sqldescribeparam-function.md)|  
|Executing an SQL statement|[SQLExecDirect Function](../../../odbc/reference/syntax/sqlexecdirect-function.md)|  
|Executing a prepared SQL statement|[SQLExecute Function](../../../odbc/reference/syntax/sqlexecute-function.md)|  
|Releasing parameter buffers on the statement|[SQLFreeStmt Function](../../../odbc/reference/syntax/sqlfreestmt-function.md)|  
|Returning the number of statement  parameters|[SQLNumParams Function](../../../odbc/reference/syntax/sqlnumparams-function.md)|  
|Returning the next parameter to send data  for|[SQLParamData Function](../../../odbc/reference/syntax/sqlparamdata-function.md)|  
|Specifying multiple parameter values|[SQLParamOptions Function](../../../odbc/reference/syntax/sqlparamoptions-function.md)|  
|Sending parameter data at execution time|[SQLPutData Function](../../../odbc/reference/syntax/sqlputdata-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)   
 [Retrieving Output Parameters Using SQLGetData](../../../odbc/reference/develop-app/retrieving-output-parameters-using-sqlgetdata.md)
