---
title: "Handling Errors and Messages | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC error handling, about error handling"
  - "errors [ODBC]"
  - "SQL Server Native Client ODBC driver, errors"
  - "messages [ODBC], about messages"
  - "ODBC error handling"
  - "SQL_INVALID_HANDLE return code"
  - "errors [ODBC], about error handling"
  - "messages [ODBC]"
ms.assetid: 74ea9630-e482-4a46-bb45-f5234f079b48
author: MightyPen
ms.author: genemi
manager: craigg
---
# Handling Errors and Messages
  When an application calls an ODBC function, the driver executes the function and returns diagnostic information in two ways: A return code indicates the overall success or failure of an ODBC function, and diagnostic records provide detailed information about the function. Diagnostic records include a header record and status records. At least one diagnostic record, the header record, is returned even if the function succeeds.  
  
 Diagnostic information is used at development time to catch programming errors, such as invalid handles and syntax errors in hard-coded SQL statements. It is also used at run time to catch run-time errors and warnings, such as data truncation, rule violations, and syntax errors in SQL statements entered by the user. Program logic is generally based on return codes.  
  
 For example, after an application calls **SQLFetch** to retrieve the rows in a result set, the return code indicates whether the end of the result set was reached (SQL_NO_DATA), if any informational messages were returned (SQL_SUCCESS_WITH_INFO), or if an error occurred (SQL_ERROR).  
  
 If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver returns anything other than SQL_SUCCESS, the application can call **SQLGetDiagRec** to retrieve any informational or error messages. Use **SQLGetDiagRec** to scroll up and down the message set if there is more than one message.  
  
 The return code SQL_INVALID_HANDLE always indicates a programming error and should never be encountered at run time. All other return codes provide run-time information, although SQL_ERROR may indicate a programming error.  
  
 The original [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] native API, DB-Library for C, allows an application to install callback error-handling and message-handling functions that return errors or messages. Some [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, such as PRINT, RAISERROR, DBCC, and SET, return their results to the DB-Library message handler function instead of to a result set. However, the ODBC API has no such callback capability. When the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver detects messages coming back from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], it sets the ODBC return code to SQL_SUCCESS_WITH_INFO or SQL_ERROR and returns the message as one or more diagnostic records. Therefore, an ODBC application must carefully test for these return codes and call **SQLGetDiagRec** to retrieve message data.  
  
 For information about tracing errors, see [Data Access Tracing](https://go.microsoft.com/fwlink/?LinkId=125805). For information about enhancements to error tracing added in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], see [Accessing Diagnostic Information in the Extended Events Log](../native-client/features/accessing-diagnostic-information-in-the-extended-events-log.md).  
  
## In This Section  
  
-   [Processing Statements That Generate Messages](processing-statements-that-generate-messages.md)  
  
-   [Diagnostic Records and Fields](diagnostic-records-and-fields.md)  
  
-   [Native Error Numbers](native-error-numbers.md)  
  
-   [SQLSTATE &#40;ODBC Error Codes&#41;](sqlstate-odbc-error-codes.md)  
  
-   [Error Messages](error-messages.md)  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](../native-client/odbc/sql-server-native-client-odbc.md)  
  
  
