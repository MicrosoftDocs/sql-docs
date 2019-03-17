---
title: "Return Codes ODBC | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "return codes [ODBC]"
  - "diagnostic information [ODBC], return codes"
ms.assetid: e893b719-4392-476f-911a-5ed6da6f7e94
author: MightyPen
ms.author: genemi
manager: craigg
---
# Return Codes ODBC
Each function in ODBC returns a code, known as its *return code,* which indicates the overall success or failure of the function. Program logic is generally based on return codes.  
  
 For example, the following code calls **SQLFetch** to retrieve the rows in a result set. It checks the return code of the function to determine if the end of the result set was reached (SQL_NO_DATA), if any warning information was returned (SQL_SUCCESS_WITH_INFO), or if an error occurred (SQL_ERROR).  
  
```  
SQLRETURN   rc;  
SQLHSTMT    hstmt;  
  
while ((rc=SQLFetch(hstmt)) != SQL_NO_DATA) {  
   if (rc == SQL_SUCCESS_WITH_INFO) {  
      // Call function to display warning information.  
   } else if (rc == SQL_ERROR) {  
      // Call function to display error information.  
      break;  
   }  
   // Process row.  
}  
```  
  
 The return code SQL_INVALID_HANDLE always indicates a programming error and should never be encountered at run time. All other return codes provide run-time information, although SQL_ERROR may indicate a programming error.  
  
 The following table defines the return codes.  
  
|Return code|Description|  
|-----------------|-----------------|  
|SQL_SUCCESS|Function completed successfully. The application calls **SQLGetDiagField** to retrieve additional information from the header record.|  
|SQL_SUCCESS_WITH_INFO|Function completed successfully, possibly with a nonfatal error (warning). The application calls **SQLGetDiagRec** or **SQLGetDiagField** to retrieve additional information.|  
|SQL_ERROR|Function failed. The application calls **SQLGetDiagRec** or **SQLGetDiagField** to retrieve additional information. The contents of any output arguments to the function are undefined.|  
|SQL_INVALID_HANDLE|Function failed due to an invalid environment, connection, statement, or descriptor handle. This indicates a programming error. No additional information is available from **SQLGetDiagRec** or **SQLGetDiagField**. This code is returned only when the handle is a null pointer or is the wrong type, such as when a statement handle is passed for an argument that requires a connection handle.|  
|SQL_NO_DATA|No more data was available. The application calls **SQLGetDiagRec** or **SQLGetDiagField** to retrieve additional information. One or more driver-defined status records in class 02xxx may be returned. **Note:**  In ODBC 2.*x*, this return code was named SQL_NO_DATA_FOUND.|  
|SQL_NEED_DATA|More data is needed, such as when parameter data is sent at execution time or additional connection information is required. The application calls **SQLGetDiagRec** or **SQLGetDiagField** to retrieve additional information, if any.|  
|SQL_STILL_EXECUTING|A function that was started asynchronously is still executing. The application calls **SQLGetDiagRec** or **SQLGetDiagField** to retrieve additional information, if any.|
