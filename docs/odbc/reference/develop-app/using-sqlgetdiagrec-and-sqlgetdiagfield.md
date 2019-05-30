---
title: "Using SQLGetDiagRec and SQLGetDiagField | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "diagnostic information [ODBC], SqlGetDiagField"
  - "SQLGetDiagField function [ODBC], and SQLGetDiagRec"
  - "SQLGetDiagRec function [ODBC], and SQLGetDiagField"
  - "diagnostic information [ODBC], SqlGetDiagRec"
  - "retrieving diagnostic information [ODBC]"
ms.assetid: 4f486bb1-fad8-4064-ac9d-61f2de85b68b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using SQLGetDiagRec and SQLGetDiagField
Applications call **SQLGetDiagRec** or **SQLGetDiagField** to retrieve diagnostic information. These functions accept an environment, connection, statement, or descriptor handle and return diagnostics from the function that last used that handle. The diagnostics logged on a particular handle are discarded when a new function is called using that handle. If the function returned multiple diagnostic records, the application calls these functions multiple times; the total number of status records is retrieved by calling **SQLGetDiagField** for the header record (record 0) with the SQL_DIAG_NUMBER option.  
  
 Applications retrieve individual diagnostic fields by calling **SQLGetDiagField** and specifying the field to retrieve. Certain diagnostic fields do not have any meaning for certain types of handles. For a list of diagnostic fields and their meanings, see the [SQLGetDiagField](../../../odbc/reference/syntax/sqlgetdiagfield-function.md) function description.  
  
 Applications retrieve the SQLSTATE, native error code, and diagnostic message in a single call by calling **SQLGetDiagRec**; **SQLGetDiagRec** cannot be used to retrieve information from the header record.  
  
 For example, the following code prompts the user for an SQL statement and executes it. If any diagnostic information was returned, it calls **SQLGetDiagField** to get the number of status records and **SQLGetDiagRec** to get the SQLSTATE, native error code, and diagnostic message from those records.  
  
```  
SQLCHAR       SqlState[6], SQLStmt[100], Msg[SQL_MAX_MESSAGE_LENGTH];  
SQLINTEGER    NativeError;  
SQLSMALLINT   i, MsgLen;  
SQLRETURN     rc1, rc2;  
SQLHSTMT      hstmt;  
  
// Prompt the user for an SQL statement.  
GetSQLStmt(SQLStmt);  
  
// Execute the SQL statement and return any errors or warnings.  
rc1 = SQLExecDirect(hstmt, SQLStmt, SQL_NTS);  
if ((rc1 == SQL_SUCCESS_WITH_INFO) || (rc1 == SQL_ERROR)) {
   SQLLEN numRecs = 0;
   SQLGetDiagField(SQL_HANDLE_STMT, hstmt, 0, SQL_DIAG_NUMBER, &numRecs, 0, 0);
   // Get the status records.
   i = 1;  
   while (i <= numRecs && (rc2 = SQLGetDiagRec(SQL_HANDLE_STMT, hstmt, i, SqlState, &NativeError,  
            Msg, sizeof(Msg), &MsgLen)) != SQL_NO_DATA) {  
      DisplayError(SqlState,NativeError,Msg,MsgLen);  
      i++;  
   }  
}  
  
if ((rc1 == SQL_SUCCESS) || (rc1 == SQL_SUCCESS_WITH_INFO)) {  
   // Process statement results, if any.  
}  
```
