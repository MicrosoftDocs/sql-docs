---
title: "Retrieving Output Parameters Using SQLGetData | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLGetData function [ODBC], retrieving output parameters"
  - "output parameters [ODBC]"
  - "retrieving output parameters [ODBC]"
ms.assetid: 7a8c298a-2160-491d-a300-d36f45568d9c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Retrieving Output Parameters Using SQLGetData
Before ODBC 3.8, an application could only retrieve the output parameters of a query with a bound output buffer. However, it is difficult to allocate a very large buffer when the size of the parameter value is very large (for example, a large image). ODBC 3.8 introduces a new way to retrieve output parameters in parts. An application can now call **SQLGetData** with a small buffer multiple times to retrieve a large parameter value. This is similar to retrieving large column data.  
  
 To bind an output parameter or input/output parameter to be retrieved in parts, call **SQLBindParameter** with the *InputOutputType* argument set to SQL_PARAM_OUTPUT_STREAM or SQL_PARAM_INPUT_OUTPUT_STREAM. With SQL_PARAM_INPUT_OUTPUT_STREAM, an application can use **SQLPutData** to input data into the parameter, and then use **SQLGetData** to retrieve the output parameter. The input data must be in the data-at-execution (DAE) form, using **SQLPutData** instead of binding it to a preallocated buffer.  
  
 This feature can be used by ODBC 3.8 applications or recompiled ODBC 3.x and ODBC 2.x applications, and these applications must have an ODBC 3.8 driver that supports retrieving output parameters using **SQLGetData** and ODBC 3.8 Driver Manager. For information about how to enable an older application to use new ODBC features, see [Compatibility Matrix](../../../odbc/reference/develop-app/compatibility-matrix.md).  
  
## Usage Example  
 For example, consider executing a stored procedure, **{CALL sp_f(?,?)}**, where both parameters are bound as SQL_PARAM_OUTPUT_STREAM, and the stored procedure returns no result set (later in this topic you will find a more complex scenario):  
  
1.  For each parameter, call **SQLBindParameter** with *InputOutputType* set to SQL_PARAM_OUTPUT_STREAM and *ParameterValuePtr* set to a token, such as a parameter number, a pointer to data, or a pointer to a structure that the application uses to bind input parameters. This example will use the parameter ordinal as the token.  
  
2.  Execute the query with **SQLExecDirect** or **SQLExecute**. SQL_PARAM_DATA_AVAILABLE will be returned, indicating that there are streamed output parameters available for retrieval.  
  
3.  Call **SQLParamData** to get the parameter that is available for retrieval. **SQLParamData** will return SQL_PARAM_DATA_AVAILABLE with the token of the first available parameter, which is set in **SQLBindParameter** (step 1). The token is returned in the buffer that the *ValuePtrPtr* points to.  
  
4.  Call **SQLGetData** with the argument *Col*_or\_*Param_Num* set to the parameter ordinal to retrieve the data of the first available parameter. If **SQLGetData** returns SQL_SUCCESS_WITH_INFO and SQLState 01004 (data truncated), and the type is variable length on both the client and server, there is more data to retrieve from the first available parameter. You can continue to call **SQLGetData** until it returns SQL_SUCCESS or SQL_SUCCESS_WITH_INFO with a different **SQLState**.  
  
5.  Repeat step 3 and step 4 to retrieve the current parameter.  
  
6.  Call **SQLParamData** again. If it returns anything except SQL_PARAM_DATA_AVAILABLE, there is no more streamed parameter data to retrieve, and the return code will be the return code of the next statement that is executed.  
  
7.  Call **SQLMoreResults** to process the next set of parameters until it returns SQL_NO_DATA. **SQLMoreResults** will return SQL_NO_DATA in this example if the statement attribute SQL_ATTR_PARAMSET_SIZE was set to 1. Otherwise, **SQLMoreResults** will return SQL_PARAM_DATA_AVAILABLE to indicate that there are streamed output parameters available for the next set of parameters to retrieve.  
  
 Similar to a DAE input parameter, the token used in the argument *ParameterValuePtr* in **SQLBindParameter** (step 1) can be a pointer that points to an application data structure, which contains the ordinal of the parameter and more application-specific information, if necessary.  
  
 The order of the returned streamed output or input/output parameters is driver specific and might not always be the same as the order specified in the query.  
  
 If the application does not call **SQLGetData** in step 4, the parameter value is discarded. Similarly, if the application calls **SQLParamData** before all of a parameter value has been read by **SQLGetData**, the remainder of the value is discarded, and the application can process the next parameter.  
  
 If the application calls **SQLMoreResults** before all streamed output parameters are processed (**SQLParamData** does still return SQL_PARAM_DATA_AVAILABLE), all remaining parameters are discarded. Similarly, if the application calls **SQLMoreResults** before all of a parameter value has been read by **SQLGetData**, the remainder of the value and all remaining parameters are discarded, and the application can continue to process the next parameter set.  
  
 Note that an application can specify the C data type in both **SQLBindParameter** and **SQLGetData**. The C data type specified with **SQLGetData** overrides the C data type specified in **SQLBindParameter**, unless the C data type specified in **SQLGetData** is SQL_APD_TYPE.  
  
 Although a streamed output parameter is more useful when the data type of the output parameter is of type BLOB, this functionality can also be used with any data type. The data types supported by streamed output parameters are specified in the driver.  
  
 If there are SQL_PARAM_INPUT_OUTPUT_STREAM parameters to be processed, **SQLExecute** or **SQLExecDirect** will return SQL_NEED_DATA first. An application can call **SQLParamData** and **SQLPutData** to send DAE parameter data. When all DAE input parameters are processed, **SQLParamData** returns SQL_PARAM_DATA_AVAILABLE to indicate streamed output parameters are available.  
  
 When there are streamed output parameters and bound output parameters to be processed, the driver determines the order for processing output parameters. So, if an output parameter is bound to a buffer (the **SQLBindParameter** parameter *InputOutputType* is set to SQL_PARAM_INPUT_OUTPUT or SQL_PARAM_OUTPUT), the buffer may not be populated until **SQLParamData** returns SQL_SUCCESS or SQL_SUCCESS_WITH_INFO. An application should read a bound buffer only after **SQLParamData** returns SQL_SUCCESS or SQL_SUCCESS_WITH_INFO that is after all streamed output parameters are processed.  
  
 The data source can return a warning and result set, in addition to the streamed output parameter. In general, warnings and result sets are processed separately from a streamed output parameter by calling **SQLMoreResults**. Process warnings and the result set before processing the streamed output parameter.  
  
 The following table describes different scenarios of a single command sent to the server, and how the application should work.  
  
|Scenario|Return value from SQLExecute or SQLExecDirect|What to do next|  
|--------------|---------------------------------------------------|---------------------|  
|Data only includes streamed output parameters|SQL_PARAM_DATA_AVAILABLE|Use **SQLParamData** and **SQLGetData** to retrieve streamed output parameters.|  
|Data includes a result set and streamed output parameters|SQL_SUCCESS|Retrieve the result set with **SQLBindCol** and **SQLGetData**.<br /><br /> Call **SQLMoreResults** to start processing streamed output parameters. It should return SQL_PARAM_DATA_AVAILABLE.<br /><br /> Use **SQLParamData** and **SQLGetData** to retrieve streamed output parameters.|  
|Data includes a warning message and streamed output parameters|SQL_SUCCESS_WITH_INFO|Use **SQLGetDiagRec** and **SQLGetDiagField** to process warning messages.<br /><br /> Call **SQLMoreResults** to start processing streamed output parameters. It should return SQL_PARAM_DATA_AVAILABLE.<br /><br /> Use **SQLParamData** and **SQLGetData** to retrieve streamed output parameters.|  
|Data includes a warning message, result set and streamed output parameters|SQL_SUCCESS_WITH_INFO|Use **SQLGetDiagRec** and **SQLGetDiagField** to process warning messages. Then call **SQLMoreResults** to start processing the result set.<br /><br /> Retrieve a result set with **SQLBindCol** and **SQLGetData**.<br /><br /> Call **SQLMoreResults** to start processing streamed output parameters. **SQLMoreResults** should return SQL_PARAM_DATA_AVAILABLE.<br /><br /> Use **SQLParamData** and **SQLGetData** to retrieve streamed output parameters.|  
|Query with DAE input parameters, for example, a streamed input/output (DAE) parameter|SQL NEED_DATA|Call **SQLParamData** and **SQLPutData** to send DAE input parameter data.<br /><br /> After all DAE input parameters are processed, **SQLParamData** can return any return code that **SQLExecute** and **SQLExecDirect** can return. The cases in this table can then be applied.<br /><br /> If the return code is SQL_PARAM_DATA_AVAILABLE, streamed output parameters are available. An application must call **SQLParamData** again to retrieve the token for the streamed output parameter, as described in the first row of this table.<br /><br /> If the return code is SQL_SUCCESS, either there is a result set to process or the processing is complete.<br /><br /> If the return code is SQL_SUCCESS_WITH_INFO, there are warning messages to process.|  
  
 After **SQLExecute**, **SQLExecDirect**, or **SQLMoreResults** returns SQL_PARAM_DATA_AVAILABLE, a function sequence error will result if an application calls a function that is not in the following list:  
  
-   **SQLAllocHandle** / **SQLAllocHandleStd**  
  
-   **SQLDataSources** / **SQLDrivers**  
  
-   **SQLGetInfo** / **SQLGetFunctions**  
  
-   **SQLGetConnectAttr** / **SQLGetEnvAttr** / **SQLGetDescField** / **SQLGetDescRec**  
  
-   **SQLNumParams**  
  
-   **SQLDescribeParam**  
  
-   **SQLNativeSql**  
  
-   **SQLParamData**  
  
-   **SQLMoreResults**  
  
-   **SQLGetDiagField** / **SQLGetDiagRec**  
  
-   **SQLCancel**  
  
-   **SQLCancelHandle** (with statement handle)  
  
-   **SQLFreeStmt** (with Option = SQL_CLOSE, SQL_DROP or SQL_UNBIND)  
  
-   **SQLCloseCursor**  
  
-   **SQLDisconnect**  
  
-   **SQLFreeHandle** (with HandleType = SQL_HANDLE_STMT)  
  
-   **SQLGetStmtAttr**  
  
 Applications can still use **SQLSetDescField** or **SQLSetDescRec** to set the binding information. Field mapping will not be changed. However, fields inside the descriptor might return new values. For example, SQL_DESC_PARAMETER_TYPE might return SQL_PARAM_INPUT_OUTPUT_STREAM or SQL_PARAM_OUTPUT_STREAM.  
  
## Usage Scenario: Retrieve an Image in Parts from a Result Set  
 **SQLGetData** can be used to get data in parts when a stored procedure returns a result set that contains one row of metadata about an image and the image is returned in a large output parameter.  
  
```  
// CREATE PROCEDURE SP_TestOutputPara  
//      @ID_of_picture   as int,  
//      @Picture         as varbinary(max) out  
// AS  
//     output the image data through streamed output parameter  
// GO  
BOOL displayPicture(SQLUINTEGER idOfPicture, SQLHSTMT hstmt) {  
   SQLLEN      lengthOfPicture;    // The actual length of the picture.  
   BYTE        smallBuffer[100];   // A very small buffer.  
   SQLRETURN   retcode, retcode2;  
  
   // Bind the first parameter (input parameter)  
   SQLBindParameter(  
         hstmt,  
         1,                         // The first parameter.   
         SQL_PARAM_INPUT,           // Input parameter: The ID_of_picture.  
         SQL_C_ULONG,               // The C Data Type.  
         SQL_INTEGER,               // The SQL Data Type.  
         0,                         // ColumnSize is ignored for integer.  
         0,                         // DecimalDigits is ignored for integer.  
         &idOfPicture,              // The Address of the buffer for the input parameter.  
         0,                         // BufferLength is ignored for integer.  
         NULL);                     // This is ignored for integer.  
  
   // Bind the streamed output parameter.  
   SQLBindParameter(  
         hstmt,   
         2,                         // The second parameter.  
         SQL_PARAM_OUTPUT_STREAM,   // A streamed output parameter.   
         SQL_C_BINARY,              // The C Data Type.    
         SQL_VARBINARY,             // The SQL Data Type.  
         0,                         // ColumnSize: The maximum size of varbinary(max).  
         0,                         // DecimalDigits is ignored for binary type.  
         (SQLPOINTER)2,             // ParameterValuePtr: An application-defined  
                                    // token (this will be returned from SQLParamData).  
                                    // In this example, we used the ordinal   
                                    // of the parameter.  
         0,                         // BufferLength is ignored for streamed output parameters.  
         &lengthOfPicture);         // StrLen_or_IndPtr: The status variable returned.   
  
   retcode = SQLPrepare(hstmt, L"{call SP_TestOutputPara(?, ?)}", SQL_NTS);  
   if ( retcode == SQL_ERROR )  
      return FALSE;  
  
   retcode = SQLExecute(hstmt);  
   if ( retcode == SQL_ERROR )  
      return FALSE;  
  
   // Assume that the retrieved picture exists.  Use SQLBindCol or SQLGetData to retrieve the result-set.  
  
   // Process the result set and move to the streamed output parameters.  
   retcode = SQLMoreResults( hstmt );  
  
   // SQLGetData retrieves and displays the picture in parts.  
   // The streamed output parameter is available.  
   while (retcode == SQL_PARAM_DATA_AVAILABLE) {  
      SQLPOINTER token;   // Output by SQLParamData.  
      SQLLEN cbLeft;      // #bytes remained  
      retcode = SQLParamData(hstmt, &token);   // returned token is 2 (according to the binding)  
      if ( retcode == SQL_PARAM_DATA_AVAILABLE ) {  
         // A do-while loop retrieves the picture in parts.  
         do {  
            retcode2 = SQLGetData(   
               hstmt,   
               (UWORD) token,          // the value of the token is the ordinal.   
               SQL_C_BINARY,           // The C-type.  
               smallBuffer,            // A small buffer.   
               sizeof(smallBuffer),    // The size of the buffer.  
               &cbLeft);               // How much data we can get.  
         }  
         while ( retcode2 == SQL_SUCCESS_WITH_INFO );  
      }  
   }  
  
   return TRUE;  
}  
```  
  
## Usage Scenario: Send and Receive a Large Object as a Streamed Input/Output Parameter  
 **SQLGetData** can be used to get and send data in parts when a stored procedure passes a large object as an input/output parameter, streaming the value to and from the database. You do not have to store all of the data in memory.  
  
```  
// CREATE PROCEDURE SP_TestInOut  
//       @picture as varbinary(max) out  
// AS  
//    output the image data through output parameter   
// go  
  
BOOL displaySimilarPicture(BYTE* image, ULONG lengthOfImage, SQLHSTMT hstmt) {  
   BYTE smallBuffer[100];   // A very small buffer.  
   SQLRETURN retcode, retcode2;  
   SQLLEN statusOfPicture;  
  
   // First bind the parameters, before preparing the statement that binds the output streamed parameter.  
   SQLBindParameter(  
      hstmt,   
      1,                                 // The first parameter.  
      SQL_PARAM_INPUT_OUTPUT_STREAM,     // I/O-streamed parameter: The Picture.  
      SQL_C_BINARY,                      // The C Data Type.  
      SQL_VARBINARY,                     // The SQL Data Type.  
      0,                                 // ColumnSize: The maximum size of varbinary(max).  
      0,                                 // DecimalDigits is ignored.   
      (SQLPOINTER)1,                     // An application defined token.   
      0,                                 // BufferLength is ignored for streamed I/O parameters.  
      &statusOfPicture);                 // The status variable.  
  
   statusOfPicture = SQL_DATA_AT_EXEC;   // Input data in parts (DAE parameter at input).  
  
   retcode = SQLPrepare(hstmt, L"{call SP_TestInOut(?) }", SQL_NTS);  
   if ( retcode == SQL_ERROR )  
      return FALSE;  
  
   // Execute the statement.  
   retcode = SQLExecute(hstmt);  
   if ( retcode == SQL_ERROR )  
      return FALSE;  
  
   if ( retcode == SQL_NEED_DATA ) {  
      // Use SQLParamData to loop through DAE input parameters.  For  
      // each, use SQLPutData to send the data to database in parts.  
  
      // This example uses an I/O parameter with streamed output.  
      // Therefore, the last call to SQLParamData should return  
      // SQL_PARAM_DATA_AVAILABLE to indicate the end of the input phrase   
      // and report that a streamed output parameter is available.  
  
      // Assume retcode is set to the return value of the last call to  
      // SQLParamData, which is equal to SQL_PARAM_DATA_AVAILABLE.  
   }  
  
   // Start processing the streamed output parameters.  
   while ( retcode == SQL_PARAM_DATA_AVAILABLE ) {  
      SQLPOINTER token;   // Output by SQLParamData.  
      SQLLEN cbLeft;     // #bytes remained  
      retcode = SQLParamData(hstmt, &token);  
      if ( retcode == SQL_PARAM_DATA_AVAILABLE ) {  
         do {  
            retcode2 = SQLGetData(   
               hstmt,   
               (UWORD) token,          // the value of the token is the ordinal.   
               SQL_C_BINARY,           // The C-type.  
               smallBuffer,            // A small buffer.   
               sizeof(smallBuffer),    // The size of the buffer.  
               &cbLeft);               // How much data we can get.  
         }  
         while ( retcode2 == SQL_SUCCESS_WITH_INFO );  
      }  
   }   
  
   return TRUE;  
}  
```  
  
## See Also  
 [Statement Parameters](../../../odbc/reference/develop-app/statement-parameters.md)
