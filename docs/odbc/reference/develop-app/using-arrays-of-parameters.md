---
title: "Using Arrays of Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "arrays of parameter values [ODBC]"
  - "parameter arrays [ODBC]"
ms.assetid: 5a28be88-e171-4f5b-bf4d-543c4383c869
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Arrays of Parameters
To use arrays of parameters, the application calls **SQLSetStmtAttr** with an *Attribute* argument of SQL_ATTR_PARAMSET_SIZE to specify the number of sets of parameters. It calls **SQLSetStmtAttr** with an *Attribute* argument of SQL_ATTR_PARAMS_PROCESSED_PTR to specify the address of a variable in which the driver can return the number of sets of parameters processed, including error sets. It calls **SQLSetStmtAttr** with an *Attribute* argument of SQL_ATTR_PARAM_STATUS_PTR to point to an array in which to return status information for each row of parameter values. The driver stores these addresses in the structure it maintains for the statement.  
  
> [!NOTE]  
>  In ODBC 2.*x*, **SQLParamOptions** was called to specify multiple values for a parameter. In ODBC 3.*x*, the call to **SQLParamOptions** has been replaced by calls to **SQLSetStmtAttr** to set the SQL_ATTR_PARAMSET_SIZE and SQL_ATTR_PARAMS_PROCESSED_ARRAY attributes.  
  
 Before executing the statement, the application sets the value of each element of each bound array. When the statement is executed, the driver uses the information it stored to retrieve the parameter values and send them to the data source; if possible, the driver should send these values as arrays. Although the use of arrays of parameters is best implemented by executing the SQL statement with all of the parameters in the array with a single call to the data source, this capability is not widely available in DBMSs today. However, drivers can simulate it by executing an SQL statement multiple times, each with a single set of parameters.  
  
 Before an application uses arrays of parameters, it must be sure that they are supported by the drivers used by the application. There are two ways to do this:  
  
-   Use only drivers known to support arrays of parameters. The application can hard-code the names of these drivers, or the user can be instructed to use only these drivers. Custom applications and vertical applications commonly use a limited set of drivers.  
  
-   Check for support of arrays of parameters at run time. A driver supports arrays of parameters if it is possible to set the SQL_ATTR_PARAMSET_SIZE statement attribute to a value greater than 1. Generic applications and vertical applications commonly check for support of arrays of parameters at run time.  
  
 The availability of row counts and result sets in parameterized execution can be determined by calling **SQLGetInfo** with the SQL_PARAM_ARRAY_ROW_COUNTS and SQL_PARAM_ARRAY_SELECTS options. For **INSERT**, **UPDATE**, and **DELETE** statements, the SQL_PARAM_ARRAY_ROW_COUNTS option indicates whether individual row counts (one for each parameter set) are available (SQL_PARC_BATCH) or whether row counts are rolled up into one (SQL_PARC_NO_BATCH). For **SELECT** statements, the SQL_PARAM_ARRAY_SELECTS option indicates whether a result set is available for each set of parameters (SQL_PAS_BATCH) or whether only one result set is available (SQL_PAS_NO_BATCH). If the driver does not allow result set-generating statements to be executed with an array of parameters, SQL_PARAM_ARRAY_SELECTS returns SQL_PAS_NO_SELECT. It is data source-specific whether arrays of parameters can be used with other types of statements, especially because the use of parameters in these statements would be data source-specific and would not follow ODBC SQL grammar.  
  
 The array pointed to by the SQL_ATTR_PARAM_OPERATION_PTR statement attribute can be used to ignore rows of parameters. If an element of the array is set to SQL_PARAM_IGNORE, the set of parameters corresponding to that element is excluded from the **SQLExecute** or **SQLExecDirect** call. The array pointed to by the SQL_ATTR_PARAM_OPERATION_PTR attribute is allocated and filled in by the application and read by the driver. If fetched rows are used as input parameters, the values of the row status array can be used in the parameter operation array.  
  
## Error Processing  
 If an error occurs while executing the statement, the execution function returns an error and sets the row number variable to the number of the row containing the error. It is data source-specific whether all rows except the error set are executed or whether all rows before (but not after) the error set are executed. Because it processes sets of parameters, the driver sets the buffer specified by the SQL_ATTR_PARAMS_PROCESSED_PTR statement attribute to the number of the row currently being processed. If all sets except the error set are executed, the driver sets this buffer to SQL_ATTR_PARAMSET_SIZE after all rows are processed.  
  
 If the SQL_ATTR_PARAM_STATUS_PTR statement attribute has been set, **SQLExecute** or **SQLExecDirect** returns the *parameter status array,* which provides the status of each set of parameters. The parameter status array is allocated by the application and filled in by the driver. Its elements indicate whether the SQL statement was executed successfully for the row of parameters or whether an error occurred while processing the set of parameters. If an error occurred, the driver sets the corresponding value in the parameter status array to SQL_PARAM_ERROR and returns SQL_SUCCESS_WITH_INFO. The application can check the status array to determine which rows were processed. Using the row number, the application can often correct the error and resume processing.  
  
 How the parameter status array is used is determined by the SQL_PARAM_ARRAY_ROW_COUNTS and SQL_PARAM_ARRAY_SELECTS options returned by a call to **SQLGetInfo**. For **INSERT**, **UPDATE**, and **DELETE** statements, the parameter status array is filled in with status information if SQL_PARC_BATCH is returned for SQL_PARAM_ARRAY_ROW_COUNTS, but not if SQL_PARC_NO_BATCH is returned. For **SELECT** statements, the parameter status array is filled in if SQL_PAS_BATCH is returned for SQL_PARAM_ARRAY_SELECT, but not if SQL_PAS_NO_BATCH or SQL_PAS_NO_SELECT is returned.  
  
## Data-at-Execution Parameters  
 If any of the values in the length/indicator array are SQL_DATA_AT_EXEC or the result of the SQL_LEN_DATA_AT_EXEC(*length*) macro, the data for those values is sent with **SQLPutData** in the usual way. The following aspects of this process bear special comment because they are not readily obvious:  
  
-   When the driver returns SQL_NEED_DATA, it must set the address of the row number variable to the row for which it needs data. As in the single-valued case, the application cannot make any assumptions about the order in which the driver will request parameter values within a single set of parameters. If an error occurs in the execution of a data-at-execution parameter, the buffer specified by the SQL_ATTR_PARAMS_PROCESSED_PTR statement attribute is set to the number of the row on which the error occurred, the status for the row in the row status array specified by the SQL_ATTR_PARAM_STATUS_PTR statement attribute is set to SQL_PARAM_ERROR, and the call to **SQLExecute**, **SQLExecDirect**, **SQLParamData**, or **SQLPutData** returns SQL_ERROR. The contents of this buffer are undefined if **SQLExecute**, **SQLExecDirect**, or **SQLParamData** return SQL_STILL_EXECUTING.  
  
-   Because the driver does not interpret the value in the *ParameterValuePtr* argument of **SQLBindParameter** for data-at-execution parameters, if the application provides a pointer to an array, **SQLParamData** does not extract and return an element of this array to the application. Instead, it returns the scalar value the application had supplied. This means the value returned by **SQLParamData** is not sufficient to specify the parameter for which the application needs to send data; the application also needs to consider the current row number.  
  
     When only some of the elements of an array of parameters are data-at-execution parameters, the application must pass the address of an array in *ParameterValuePtr* that contains elements for all the parameters. This array is interpreted normally for the parameters that are not data-at-execution parameters. For the data-at-execution parameters, the value that **SQLParamData** provides to the application, which normally could be used to identify the data that the driver is requesting on this occasion, is always the address of the array.
