---
title: "Use Table-Valued Parameters (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
ms.assetid: 6f8da6ab-9de6-4d0a-9b7e-acb76a50a2e7
author: MightyPen
ms.author: genemi
manager: craigg
---
# Use Table-Valued Parameters (ODBC)
  This sample shows how to use table-valued parameters to insert multiple rows, with multiple columns, with one call to the server.  
  
 For more information about table-valued parameters, see [Table-Valued Parameters &#40;ODBC&#41;](../native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md). For more samples using table-valued parameters, see [ODBC Table-Valued Parameter Programming Examples](../../database-engine/dev-guide/odbc-table-valued-parameter-programming-examples.md).  
  
## Example  
 You will need an ODBC data source called TVPDemo. The default database for TVPDemo can be any test database on your computer. This data source must be based on the ODBC driver for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
 If you will build and run this sample as a 32-bit application on a 64-bit operating system, you must create the ODBC data source with the ODBC Administrator in %windir%\SysWOW64\odbcad32.exe.  
  
 This sample connects to your computer's default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. To connect to a named instance, change the definition of the ODBC data source to specify the instance using the following format: server\namedinstance. By default, [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] installs to a named instance.  
  
 Compile the (C++) code listing with odbc32.lib and user32.lib. Make sure your INCLUDE environment variable includes the directory that contains sqlncli.h.  
  
```  
// compile with: odbc32.lib user32.lib  
#pragma once  
#define WIN32_LEAN_AND_MEAN   // Exclude rarely-used stuff from Windows headers  
#include <stdio.h>  
#include <stdlib.h>  
#include <tchar.h>  
#include <windows.h>  
#include "sql.h"  
#include "sqlext.h"  
#include "sqlncli.h"  
  
// cardinality of order item related array variables  
#define ITEM_ARRAY_SIZE 20  
  
// struct to pass order entry data  
typedef struct OrdEntry_struct {  
   SQLINTEGER OrdNo;  
   SQLTCHAR OrdDate[24];  
   SQLTCHAR CustCode[6];  
   SQLUINTEGER ItemCount;  
   SQLINTEGER ProdCode[ITEM_ARRAY_SIZE];  
   SQLINTEGER Qty[ITEM_ARRAY_SIZE];  
} OrdEntryData;  
  
SQLHANDLE henv, hdbc, hstmt;  
  
void ODBCError(SQLHANDLE henv, SQLHANDLE hdbc, SQLHANDLE hstmt, SQLHANDLE hdesc, bool ShowError) {  
   SQLRETURN r = 0;  
   SQLTCHAR szSqlState[6] = {0};  
   SQLINTEGER fNativeError = 0;  
   SQLTCHAR szErrorMsg[256] = {0};  
   SQLSMALLINT cbErrorMsgMax = sizeof(szErrorMsg) - 1;  
   SQLSMALLINT cbErrorMsg = 0;  
   TCHAR text[1024] = {0}, title[256] = {0};  
  
   if (hdesc != NULL)  
      r = SQLGetDiagRec(SQL_HANDLE_DESC, hdesc, 1, szSqlState, &fNativeError, szErrorMsg, cbErrorMsgMax, &cbErrorMsg);  
   else {  
      if (hstmt != NULL)  
         r = SQLGetDiagRec(SQL_HANDLE_STMT, hstmt, 1, szSqlState, &fNativeError, szErrorMsg, cbErrorMsgMax, &cbErrorMsg);  
      else {  
         if (hdbc != NULL)  
            r = SQLGetDiagRec(SQL_HANDLE_DBC, hdbc, 1, szSqlState, &fNativeError, szErrorMsg, cbErrorMsgMax, &cbErrorMsg);  
         else  
            r = SQLGetDiagRec(SQL_HANDLE_ENV, henv, 1, szSqlState, &fNativeError, szErrorMsg, cbErrorMsgMax, &cbErrorMsg);  
      }  
   }  
  
   if (ShowError) {  
      _sntprintf_s(title, _countof(title), _TRUNCATE, _T("ODBC Error %i"), fNativeError);  
      _sntprintf_s(text, _countof(text), _TRUNCATE, _T("[%s] - %s"), szSqlState, szErrorMsg);  
  
      MessageBox(NULL, (LPCTSTR) text, (LPCTSTR) _T("ODBC Error"), MB_OK);  
   }  
}  
  
void connect() {  
   SQLRETURN r;  
  
   r = SQLAllocHandle(SQL_HANDLE_ENV, NULL, &henv);  
  
   // This is an ODBC v3 application  
   r = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER) SQL_OV_ODBC3, 0);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, NULL, NULL, NULL, true);  
      exit(-1);  
   }  
  
   r = SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc);  
  
   // Run in ANSI/implicit transaction mode  
   r = SQLSetConnectAttr(hdbc, SQL_ATTR_AUTOCOMMIT, (SQLPOINTER) SQL_AUTOCOMMIT_OFF, SQL_IS_INTEGER);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, NULL, NULL, NULL, true);  
      exit(-1);  
   }  
  
   TCHAR szConnStrIn[256] = _T("DSN=TVPDemo");  
  
   r = SQLDriverConnect(hdbc, NULL, (SQLTCHAR *) szConnStrIn, SQL_NTS, NULL, 0, NULL, SQL_DRIVER_NOPROMPT);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, NULL, NULL, true);  
      exit(-1);  
   }  
}  
  
void setup_ODBC_basics() {  
   SQLRETURN r;  
  
   r = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);  
      exit(-1);  
   }  
}  
  
void setup_TVP_demo () {  
   SQLRETURN r;  
   // Drop prior versions of table and procedure  
   r = SQLExecDirect(hstmt, (SQLTCHAR *) _T("drop procedure TVPOrderEntry"), SQL_NTS);  
   r = SQLExecDirect(hstmt, (SQLTCHAR *) _T("drop procedure TVPOrderInsert"), SQL_NTS);  
   r = SQLExecDirect(hstmt, (SQLTCHAR *) _T("drop procedure TVPItemInsert"), SQL_NTS);  
   r = SQLExecDirect(hstmt, (SQLTCHAR *) _T("drop type TVPParam"), SQL_NTS);  
   r = SQLExecDirect(hstmt, (SQLTCHAR *) _T("drop table TVPOrd"), SQL_NTS);  
   r = SQLExecDirect(hstmt, (SQLTCHAR *) _T("drop table TVPItem"), SQL_NTS);  
  
   // Create tables  
   r = SQLExecDirect(hstmt,   
      (SQLTCHAR *) _T("create table TVPOrd( OrdNo integer identity(1,1), OrdDate datetime, CustCode varchar(5))"), SQL_NTS);  
  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);  
      exit(-1);  
   }  
  
   r = SQLExecDirect(hstmt,   
      (SQLTCHAR *) _T("create table TVPItem( OrdNo integer, ItemNo integer identity(1,1), ProdCode integer, Qty integer)"), SQL_NTS);  
  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Create TABLE type for use as a TVP  
   r = SQLExecDirect(hstmt,   
      (SQLTCHAR *) _T("create type TVPParam as table(ProdCode integer, Qty integer)"), SQL_NTS);  
  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Create procedure for TVPOrd insert   
   r = SQLExecDirect(hstmt, (SQLTCHAR *)   
      _T("create procedure TVPOrderInsert(@CustCode varchar(5), \  
         @OrdNo integer output, @OrdDate datetime output)\  
         as \  
         set @OrdDate = GETDATE();\  
         insert into TVPOrd (OrdDate, CustCode) values (@OrdDate, @CustCode); \  
         select @OrdNo = SCOPE_IDENTITY()"), SQL_NTS);  
  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Create procedure for TVPItem insert   
   r = SQLExecDirect(hstmt,   
      (SQLTCHAR *) _T("create procedure TVPItemInsert(@OrdNo integer, \  
                      @ProdCode integer, @Qty integer)\  
                      as \  
                      insert into TVPItem (OrdNo, ProdCode, Qty) \  
                      values (@OrdNo, @ProdCode, @Qty)"), SQL_NTS);  
  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Create procedure with TVP parameters   
   r = SQLExecDirect(hstmt,   
      (SQLTCHAR *) _T("create procedure TVPOrderEntry(@CustCode varchar(5), @Items TVPParam READONLY, \  
                      @OrdNo integer output, @OrdDate datetime output)\  
                      as \  
                      set @OrdDate = GETDATE();\  
                      insert into TVPOrd (OrdDate, CustCode) values (@OrdDate, @CustCode); \  
                      select @OrdNo = SCOPE_IDENTITY(); \  
                      insert into TVPItem (OrdNo, ProdCode, Qty) \  
                      select @OrdNo, ProdCode, Qty from @Items"), SQL_NTS);  
  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLEndTran(SQL_HANDLE_DBC, hdbc, SQL_COMMIT);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
}  
  
void OrdEntry_Simple (OrdEntryData& order) {  
   // Simple order entry  
   SQLRETURN r;  
  
   SQLINTEGER ProdCode, Qty;  
  
   // Bind parameters for the Order  
   // 1 - Custcode input  
   r = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT,SQL_C_TCHAR, SQL_VARCHAR, 5, 0, &order.CustCode, sizeof(order.CustCode), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 2 - OrdNo output  
   r = SQLBindParameter(hstmt, 2, SQL_PARAM_OUTPUT, SQL_C_LONG, SQL_INTEGER, 0, 0, &order.OrdNo, sizeof(SQLINTEGER), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 3- OrdDate output  
   r = SQLBindParameter(hstmt, 3, SQL_PARAM_OUTPUT, SQL_C_TCHAR, SQL_TYPE_TIMESTAMP, 23, 3, order.OrdDate, sizeof(order.OrdDate), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Insert the order  
   r = SQLExecDirect(hstmt, (SQLTCHAR *) _T("{call TVPOrderInsert(?, ?, ?)}"),SQL_NTS);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Flush results & reset hstmt  
   r = SQLMoreResults(hstmt);  
   if (r != SQL_NO_DATA) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLFreeStmt(hstmt, SQL_RESET_PARAMS);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Bind parameters for the Items  
   // 1 - OrdNo   
   r = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_LONG, SQL_INTEGER, 10, 0, &order.OrdNo, sizeof(SQLINTEGER), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 2 - ProdCode  
   r = SQLBindParameter(hstmt, 2, SQL_PARAM_INPUT, SQL_C_LONG, SQL_INTEGER, 10, 0, &ProdCode, sizeof(SQLINTEGER), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 3 - Qty  
   r = SQLBindParameter(hstmt, 3, SQL_PARAM_INPUT, SQL_C_LONG, SQL_INTEGER, 10, 0, &Qty, sizeof(SQLINTEGER), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Insert items one at a time  
   r = SQLPrepare(hstmt, (SQLTCHAR *) _T("{call TVPItemInsert(?, ?, ?)}"),SQL_NTS);  
  
   for (unsigned int i = 0; i < order.ItemCount; i++) {  
      ProdCode = order.ProdCode[i];  
      Qty = order.Qty[i];  
      r = SQLExecute(hstmt);  
      if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
         ODBCError(henv, hdbc, hstmt, NULL, true);   
         exit(-1);  
      }  
   }  
  
   // Flush results & reset hstmt  
   r = SQLMoreResults(hstmt);  
   if (r != SQL_NO_DATA) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLFreeStmt(hstmt, SQL_RESET_PARAMS);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Commit the transaction  
   r = SQLEndTran(SQL_HANDLE_DBC, hdbc, SQL_COMMIT);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
}  
  
void OrdEntry_PA (OrdEntryData& order){  
   // best practice (not using TVPs) using a parameter array  
   SQLRETURN r;  
  
   // Array if OrdNo for use with array insert of Items  
   SQLINTEGER OrdNo[ITEM_ARRAY_SIZE];  
  
   // Bind parameters for the Order  
   // 1 - Custcode input  
   r = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT,SQL_C_TCHAR, SQL_VARCHAR, 5, 0, &order.CustCode, sizeof(order.CustCode), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 2 - OrdNo output  
   r = SQLBindParameter(hstmt, 2, SQL_PARAM_OUTPUT, SQL_C_LONG, SQL_INTEGER, 0, 0, &order.OrdNo, sizeof(SQLINTEGER), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 3- OrdDate output  
   r = SQLBindParameter(hstmt, 3, SQL_PARAM_OUTPUT, SQL_C_TCHAR, SQL_TYPE_TIMESTAMP, 23, 3, order.OrdDate, sizeof(order.OrdDate), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);  
      exit(-1);  
   }  
  
   // Insert the order  
   r = SQLExecDirect(hstmt, (SQLTCHAR *) _T("{call TVPOrderInsert(?, ?, ?)}"),SQL_NTS);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Flush results & reset hstmt  
   r = SQLMoreResults(hstmt);  
   if (r != SQL_NO_DATA) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLFreeStmt(hstmt, SQL_RESET_PARAMS);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Bind parameters for the Items  
   // 1 - OrdNo   
   r = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_LONG, SQL_INTEGER, 0, 0, OrdNo, sizeof(SQLINTEGER), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 2 - ProdCode  
   r = SQLBindParameter(hstmt, 2, SQL_PARAM_INPUT, SQL_C_LONG, SQL_INTEGER, 0, 0, order.ProdCode, sizeof(SQLINTEGER), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 3 - Qty  
   r = SQLBindParameter(hstmt, 3, SQL_PARAM_INPUT, SQL_C_LONG, SQL_INTEGER, 0, 0, order.Qty, sizeof(SQLINTEGER), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Set param array size for items  
   SQLULEN arraySize;  
   arraySize = order.ItemCount;  
   r = SQLSetStmtAttr(hstmt, SQL_ATTR_PARAMSET_SIZE, reinterpret_cast<SQLPOINTER> (arraySize), SQL_IS_UINTEGER);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Initialize OrdNo array   
   for (int i = 0; i < ITEM_ARRAY_SIZE; i++)  
      OrdNo[i] = order.OrdNo;  
  
   // Insert the Items  
   r = SQLExecDirect(hstmt, (SQLTCHAR *) _T("{call TVPItemInsert(?, ?, ?)}"),SQL_NTS);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Flush results & reset hstmt  
   r = SQLMoreResults(hstmt);  
   if (r != SQL_NO_DATA) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLFreeStmt(hstmt, SQL_RESET_PARAMS);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLSetStmtAttr(hstmt, SQL_ATTR_PARAMSET_SIZE, (SQLPOINTER) 1, SQL_IS_UINTEGER);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Commit the transaction  
   r = SQLEndTran(SQL_HANDLE_DBC, hdbc, SQL_COMMIT);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
}  
  
void OrdEntry_TVP (OrdEntryData& order){  
   // Order entry using a TVP  
   SQLRETURN r;  
  
   // Variable for TVP row count  
   SQLLEN cbTVP;  
  
   // Bind parameters for call to TVPOrderEntryDirect  
   // 1 - Custcode input  
   r = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT,SQL_C_TCHAR, SQL_VARCHAR, 5, 0, &order.CustCode, sizeof(order.CustCode), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 2 - Items TVP  
   r = SQLBindParameter(hstmt,   
      2,// ParameterNumber  
      SQL_PARAM_INPUT,// InputOutputType  
      SQL_C_DEFAULT,// ValueType   
      SQL_SS_TABLE,// Parametertype  
      ITEM_ARRAY_SIZE,// ColumnSize - for a TVP this the row array size  
      0,// DecimalDigits - for a TVP this is the number of columns in the TVP   
      NULL,// ParameterValuePtr - for a TVP this is the type name of the TVP  
                        // (not needed with stored proc)  
      NULL,// BufferLength - for a TVP this is the length of the type name or SQL_NTS  
                        // (not needed with stored proc)  
      &cbTVP);// StrLen_or_IndPtr - for a TVP this is the number of rows available  
  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 3 - OrdNo output  
   r = SQLBindParameter(hstmt, 3, SQL_PARAM_OUTPUT, SQL_C_LONG, SQL_INTEGER, 0, 0, &order.OrdNo, sizeof(SQLINTEGER), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 4- OrdDate output  
   r = SQLBindParameter(hstmt, 4, SQL_PARAM_OUTPUT, SQL_C_TCHAR, SQL_TYPE_TIMESTAMP, 23, 3, order.OrdDate, sizeof(order.OrdDate), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Bind columns for the TVP (param 2)  
   // First set focus on param 2  
   r = SQLSetStmtAttr(hstmt, SQL_SOPT_SS_PARAM_FOCUS, (SQLPOINTER) 2, SQL_IS_INTEGER);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Col 1 - ProdCode  
   r = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_LONG, SQL_INTEGER, 0, 0, order.ProdCode, sizeof(SQLINTEGER), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Col 2 - Qty  
   r = SQLBindParameter(hstmt, 2, SQL_PARAM_INPUT, SQL_C_LONG, SQL_INTEGER, 0, 0, order.Qty, sizeof(SQLINTEGER), NULL);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Reset param focus  
   r = SQLSetStmtAttr(hstmt, SQL_SOPT_SS_PARAM_FOCUS, (SQLPOINTER) 0, SQL_IS_INTEGER);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Initialize TVP row count  
   cbTVP = order.ItemCount; // Number of rows available for input  
  
   // Call one procedure which inserts both the order and items  
   r = SQLExecDirect(hstmt, (SQLTCHAR *) _T("{call TVPOrderEntry(?, ?, ?, ?)}"),SQL_NTS);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Flush results & reset hstmt  
   r = SQLMoreResults(hstmt);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO && r!= SQL_NO_DATA) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLFreeStmt(hstmt, SQL_RESET_PARAMS);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Commit the transaction  
   r = SQLEndTran(SQL_HANDLE_DBC, hdbc, SQL_COMMIT);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
}  
  
void demo_fixed_TVP_binding (SQLHANDLE hstmt){  
   // Bind a TVP using program arrays  
   SQLRETURN r;  
  
   // Variables for SQL parameters  
   SQLTCHAR CustCode[6];  
   SQLWCHAR *TVP = (SQLWCHAR *) L"TVPParam";  
   SQLINTEGER ProdCode[ITEM_ARRAY_SIZE], Qty[ITEM_ARRAY_SIZE];  
   SQLINTEGER OrdNo;  
   SQLTCHAR OrdDate[24];  
  
   // Variables for indicator/length variables associated with parameters  
   SQLLEN cbCustCode, cbTVP, cbProdCode[ITEM_ARRAY_SIZE], cbQty[ITEM_ARRAY_SIZE], cbOrdNo, cbOrdDate;  
  
   // Bind parameters for call to TVPOrderEntryDirect  
   // 1 - Custcode input  
   r = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_TCHAR, SQL_VARCHAR, 5, 0, CustCode, sizeof(CustCode), &cbCustCode);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 2 - Items TVP  
   r = SQLBindParameter(hstmt,   
      2,// ParameterNumber  
      SQL_PARAM_INPUT,// InputOutputType  
      SQL_C_DEFAULT,// ValueType   
      SQL_SS_TABLE,// Parametertype  
      ITEM_ARRAY_SIZE,// ColumnSize - for a TVP this the row array size  
      0,// DecimalDigits - for a TVP this is the number of columns in the TVP   
      TVP,// ParameterValuePtr - for a TVP this is the type name of the TVP  
                        // and also a token returned by SQLParamData  
      SQL_NTS,// BufferLength - for a TVP this is the length of the type name or SQL_NTS  
      &cbTVP);// StrLen_or_IndPtr - for a TVP this is the number of rows input and output  
  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 3 - OrdNo output  
   r = SQLBindParameter(hstmt, 3, SQL_PARAM_OUTPUT,SQL_C_LONG, SQL_INTEGER, 0, 0, &OrdNo, sizeof(SQLINTEGER), &cbOrdNo);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 4- OrdDate output  
   r = SQLBindParameter(hstmt, 4, SQL_PARAM_OUTPUT,SQL_C_TCHAR, SQL_TYPE_TIMESTAMP, 23, 3, OrdDate, sizeof(OrdDate), &cbOrdDate);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Bind columns for the TVP (param 2)  
   //First set focus on param 2  
   r = SQLSetStmtAttr(hstmt, SQL_SOPT_SS_PARAM_FOCUS, (SQLPOINTER) 2, SQL_IS_INTEGER);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Col 1 - ProdCode  
   r = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT,SQL_C_LONG, SQL_INTEGER, 0, 0, ProdCode, sizeof(SQLINTEGER), cbProdCode);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Col 2 - Qty  
   r = SQLBindParameter(hstmt, 2, SQL_PARAM_INPUT,SQL_C_LONG, SQL_INTEGER, 0, 0, Qty, sizeof(SQLINTEGER), cbQty);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Reset param focus  
   r = SQLSetStmtAttr(hstmt, SQL_SOPT_SS_PARAM_FOCUS, (SQLPOINTER) 0, SQL_IS_INTEGER);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Populate parameters  
   cbTVP = 0;   // Number of rows available for input  
   strcpy_s((char *) CustCode, sizeof(CustCode), "CUST1"); cbCustCode = SQL_NTS;  
  
   ProdCode[cbTVP] = 1215;  
   cbProdCode[cbTVP] = sizeof(SQLINTEGER);   
   Qty[cbTVP] = 5;  
   cbQty[cbTVP] = sizeof(SQLINTEGER);   
   cbTVP++;   // Number of rows available for input  
  
   ProdCode[cbTVP] = 1017;  
   cbProdCode[cbTVP] = sizeof(SQLINTEGER);   
   Qty[cbTVP] = 2;  
   cbQty[cbTVP] = sizeof(SQLINTEGER);   
   cbTVP++;   // Number of rows available for input  
  
   // Call the procedure  
   r = SQLExecDirect(hstmt, (SQLTCHAR *) _T("{call TVPOrderEntry(?, ?, ?, ?)}"),SQL_NTS);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Flush rowcounts  
   do {  
      r = SQLMoreResults(hstmt);  
      if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
         ODBCError(henv, hdbc, hstmt, NULL, true);   
         exit(-1);  
      }  
  
   } while (r != SQL_NO_DATA);  
  
   // Commit the transaction  
   r = SQLEndTran(SQL_HANDLE_DBC, hdbc, SQL_COMMIT);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
}  
  
void demo_variable_TVP_binding (SQLHANDLE hstmt) {  
   // Bind a TVP using program data at exec and output row streaming  
   SQLRETURN r;  
  
   // Variables for SQL parameters  
   SQLCHAR CustCode[6];  
   SQLWCHAR *TVP = (SQLWCHAR *) L"TVPParam";  
   SQLINTEGER ProdCode, Qty;  
   SQLINTEGER OrdNo;  
   char *OrdDate[23];  
  
   // Variables for indicator/length variables associated with parameters  
   SQLLEN cbCustCode, cbTVP, cbProdCode, cbQty, cbOrdNo, cbOrdDate;  
  
   // Token returned by SQLParamData to indicate which param data is needed for  
   SQLPOINTER ParamId;  
  
   // Bind parameters for call to TVPOrderEntry  
   r = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_TCHAR, SQL_VARCHAR, 5, 0, CustCode, sizeof(CustCode), &cbCustCode);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 2 - Items TVP  
   r = SQLBindParameter(hstmt,   
      2,// ParameterNumber  
      SQL_PARAM_INPUT,// InputOutputType  
      SQL_C_DEFAULT,// ValueType   
      SQL_SS_TABLE,// Parametertype  
      ITEM_ARRAY_SIZE,// ColumnSize - for a TVP this the row array size  
      0,// DecimalDigits - for a TVP this is the number of columns in the TVP   
      TVP,// ParameterValuePtr - for a TVP this is the type name of the TVP  
                        // and also a token returned by SQLParamData  
      SQL_NTS,// BufferLength - for a TVP this is the length of the type name or SQL_NTS  
      &cbTVP);// StrLen_or_IndPtr - for a TVP this is the number of rows input and output  
  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 3 - OrdNo output  
   r = SQLBindParameter(hstmt, 3, SQL_PARAM_OUTPUT,SQL_C_LONG, SQL_INTEGER, 0, 0, &OrdNo, sizeof(SQLINTEGER), &cbOrdNo);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // 4 - OrdDate output  
   r = SQLBindParameter(hstmt, 4, SQL_PARAM_OUTPUT,SQL_C_CHAR, SQL_TYPE_TIMESTAMP, 23, 3, &OrdDate, sizeof(OrdDate), &cbOrdDate);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Bind the TVP columns  
   // First set focus on param 2  
   r = SQLSetStmtAttr(hstmt, SQL_SOPT_SS_PARAM_FOCUS, (SQLPOINTER) 2, SQL_IS_INTEGER);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // ProdCode  
   r = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT,SQL_C_LONG, SQL_INTEGER, 0, 0, &ProdCode, sizeof(SQLINTEGER), &cbProdCode);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Qty  
   r = SQLBindParameter(hstmt, 2, SQL_PARAM_INPUT,SQL_C_LONG, SQL_INTEGER, 0, 0, &Qty, sizeof(SQLINTEGER), &cbQty);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Reset param focus  
   r = SQLSetStmtAttr(hstmt, SQL_SOPT_SS_PARAM_FOCUS, (SQLPOINTER) 0, SQL_IS_INTEGER);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   // Initialze the TVP for row streaming  
   cbTVP = SQL_DATA_AT_EXEC;  
  
   // Initialize output parameters for normal binding  
   cbOrdNo = sizeof(SQLINTEGER);  
   cbOrdDate = sizeof(OrdDate);  
  
   // Populate non-data-at-exec parameters  
   strcpy_s((char *) CustCode , sizeof(CustCode), "CUST1");  
   cbCustCode = SQL_NTS;  
  
   // Call the procedure  
   r = SQLExecDirect(hstmt, (SQLTCHAR *) _T("{call TVPOrderEntry(?, ?, ?, ?)}"), SQL_NTS);  
  
   // Check if para data needed and get 1st param id token  
   if (r == SQL_NEED_DATA)  
      r = SQLParamData(hstmt, &ParamId);  
  
   // Supply param row data  
   int rowNum = 0;  
   while (r == SQL_NEED_DATA) {  
      if (ParamId == TVP) {  
         switch (rowNum) {  
                case 0:   // Supply data for 1st row  
                   // Populate input TVP row constituent columns  
                   ProdCode = 1215;  
                   cbProdCode = sizeof(SQLINTEGER);   
                   Qty = 5;  
                   cbQty = sizeof(SQLINTEGER);  
  
                   // Returning 1 for Str_Len_or_Ind indicates a row is available  
                   r = SQLPutData(hstmt, SQLPOINTER(1), 1);  
                   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
                      ODBCError(henv, hdbc, hstmt, NULL, true);   
                      exit(-1);  
                   }  
  
                   rowNum++;  
                   break;  
  
                case 1:   // Supply data for 2nd row  
                   // Populate another TVP row as above  
                   ProdCode = 1017;  
                   cbProdCode = sizeof(SQLINTEGER);   
  
                   // This time supply Qty via SQLPutData ...  
                   Qty = 0;  
                   cbQty = SQL_DATA_AT_EXEC;  
  
                   r = SQLPutData(hstmt, SQLPOINTER(1), 1);  
                   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
                      ODBCError(henv, hdbc, hstmt, NULL, true);   
                      exit(-1);  
                   }  
  
                   rowNum++;  
                   break;  
  
                default:  
                   // Sending 0 indicates that now more TVP rows are available  
                   r = SQLPutData(hstmt, SQLPOINTER(1), 0);  
                   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
                      ODBCError(henv, hdbc, hstmt, NULL, true);   
                      exit(-1);  
                   }  
  
                   break;  
         }  
      }  
      else {  
         if (ParamId == &Qty) {  
            Qty = 2;  
            // For a character or binary parameter, SQLPutData can be called multiple times to pass the value in pieces  
            SQLPutData(hstmt, &Qty, sizeof(SQLINTEGER));  
            if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
               ODBCError(henv, hdbc, hstmt, NULL, true);   
               exit(-1);  
            }  
         }  
      }  
  
      // Signal that param data is available, get token for next param  
      r = SQLParamData(hstmt, &ParamId);  
   }  
  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      _tprintf_s(_T("Error streaming input rows\n"));  
      exit(-1);  
   }  
  
   // Commit the transaction  
   r = SQLEndTran(SQL_HANDLE_DBC, hdbc, SQL_COMMIT);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
}  
  
// Getting metadata for columns of a table type used for TVPs  
// is just the same as getting column metadata for a regular table  
void demo_metadata_for_table_type_columns(SQLTCHAR *TableTypeName) {  
   SQLHANDLE chstmt;  
   SQLRETURN r;  
  
   r = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &chstmt);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLSetStmtAttr(chstmt, SQL_SOPT_SS_NAME_SCOPE, (SQLPOINTER) SQL_SS_NAME_SCOPE_TABLE_TYPE, SQL_IS_UINTEGER);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLColumns(chstmt, NULL, 0, NULL, 0, TableTypeName, SQL_NTS, NULL, 0);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   if (r == SQL_SUCCESS) {  
      int colNo = 0;   
      SQLTCHAR columnType[256];  
      for ( ; ; ) {  
         r = SQLFetch(chstmt);  
         if (r != SQL_SUCCESS)   
            break;  
         SQLGetData(chstmt, 6, SQL_C_TCHAR, columnType, sizeof(columnType), NULL);  
         _tprintf(_T("\tColumn %i has type %s\n"), ++colNo, columnType);  
      }  
   }  
  
   r = SQLCloseCursor(chstmt);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLFreeHandle(SQL_HANDLE_STMT, chstmt);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
}  
  
// To get metadata for a TVP from a prepared statement, an application calls  
// SQLDescribeParam. SQLGetDescField can be used with SQL_DESC_TYPE_NAME in   
// the IPD to get the underlying table type name and its catalog and schema.  
// For a TVP, DecimalDigits returns the number of columns in the TVP.  
void demo_metadata_from_prepared_statement(SQLHANDLE hstmt) {  
   SQLRETURN r;  
   SQLUSMALLINT paramNo=1;  
   SQLSMALLINT DataType;  
   SQLULEN ParameterSize;  
   SQLSMALLINT DecimalDigits, Nullable, NumParams;  
   SQLHANDLE IPD;  
   SQLINTEGER StringLength;  
   SQLTCHAR parameterTypeName[256];  
  
   r = SQLPrepare(hstmt, (SQLTCHAR *) _T("{call TVPOrderEntry(?, ?, ?, ?)}"), SQL_NTS);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLNumParams(hstmt, &NumParams);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLGetStmtAttr(hstmt, SQL_ATTR_IMP_PARAM_DESC, &IPD, SQL_IS_POINTER, &StringLength);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   for (paramNo = 1; paramNo <= NumParams; paramNo++) {  
      r = SQLDescribeParam(hstmt, paramNo, &DataType, &ParameterSize, &DecimalDigits, &Nullable);  
      if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
         ODBCError(henv, hdbc, hstmt, NULL, true);   
         exit(-1);  
      }  
  
      if (r == SQL_SUCCESS) {  
         r = SQLGetDescField(IPD, paramNo, SQL_DESC_TYPE_NAME, parameterTypeName, sizeof(parameterTypeName), &StringLength);  
         if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
            ODBCError(henv, hdbc, hstmt, NULL, true);   
            exit(-1);  
         }  
  
         _tprintf(_T("Parameter %i has type %s\n"), paramNo, parameterTypeName);  
      }  
  
      if (DataType == SQL_SS_TABLE) {  
         r = SQLCancel(hstmt);  
         demo_metadata_for_table_type_columns(parameterTypeName);  
      }  
   }  
}  
  
// An application uses SQLProcedureColumns in the usual way to get parameter  
// information for procedures which use TVPs. However, this does not return  
// column metadata for TVPs. Instead an application uses the data type name for   
// the TVP with SQLColumns to get column metadata  
void demo_metadata_from_catalog_APIs(SQLTCHAR *procName) {  
   SQLHANDLE chstmt;  
   SQLRETURN r;  
  
   r = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &chstmt);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, NULL, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLProcedureColumns(chstmt, NULL, 0, NULL, 0, procName, SQL_NTS, NULL, 0);  
  
   if (r == SQL_SUCCESS) {  
      SQLSMALLINT paramType;  
      SQLLEN colNameInd;  
      SQLTCHAR colName[256];  
      SQLTCHAR colTypeName[256];  
      SQLSMALLINT colDataType;  
      SQLINTEGER colOrdinal;  
      int pNum = 0;  
      TCHAR *preamble;  
  
      for ( ; ; ) {  
         r = SQLFetch(chstmt);  
         pNum++;  
         if (r != SQL_SUCCESS)   
            break;  
         r = SQLGetData(chstmt, 4, SQL_C_TCHAR, colName, sizeof(colTypeName), &colNameInd);  
         if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
            ODBCError(henv, hdbc, hstmt, NULL, true);   
            exit(-1);  
         }  
  
         if (colNameInd < 0)  
            colName[0] = 0;  
  
         r = SQLGetData(chstmt, 5, SQL_C_SHORT, &paramType, sizeof(SQL_C_SHORT), NULL);  
         if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
            ODBCError(henv, hdbc, hstmt, NULL, true);   
            exit(-1);  
         }  
  
         switch(paramType) {  
                case SQL_PARAM_INPUT:  
                   preamble = _T("(Input)        Parameter");   
                   break;  
  
                case SQL_PARAM_INPUT_OUTPUT:  
                   preamble = _T("(Input/Output) Parameter");   
                   break;  
  
                case SQL_PARAM_OUTPUT:  
                   preamble = _T("(Output)       Parameter");   
                   break;  
  
                case SQL_RETURN_VALUE:  
                   preamble = _T("(Return)       Parameter");   
                   break;  
  
                case SQL_RESULT_COL:  
                   preamble = _T("Result Column");   
                   break;  
  
                case SQL_PARAM_TYPE_UNKNOWN:  
  
                default:  
                   preamble = _T("(Unknown) Parameter");   
                   break;  
         }  
  
         r = SQLGetData(chstmt, 6, SQL_C_SHORT, &colDataType, sizeof(SQL_C_SHORT), NULL);  
         if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
            ODBCError(henv, hdbc, chstmt, NULL, true);   
            exit(-1);  
         }  
  
         r = SQLGetData(chstmt, 7, SQL_C_TCHAR, colTypeName, sizeof(colTypeName), NULL);  
         if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
            ODBCError(henv, hdbc, chstmt, NULL, true);   
            exit(-1);  
         }  
  
         r = SQLGetData(chstmt, 18, SQL_C_LONG, &colOrdinal, sizeof(SQL_C_LONG), NULL);  
         if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
            ODBCError(henv, hdbc, chstmt, NULL, true);  
            exit(-1);  
         }  
  
         _tprintf(_T("%s %i has type %s\n"), preamble, colOrdinal, colTypeName);  
  
         if (colDataType == SQL_SS_TABLE) {  
            r = SQLCancel(chstmt);  
            if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
               ODBCError(henv, hdbc, chstmt, NULL, true);  
               exit(-1);  
            }  
  
            demo_metadata_for_table_type_columns(colTypeName);  
  
            r = SQLProcedureColumns(chstmt, NULL, 0, NULL, 0, procName, SQL_NTS, NULL, 0);  
            if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
               ODBCError(henv, hdbc, chstmt, NULL, true);   
               exit(-1);  
            }  
  
            for (int x = 0; x < pNum; x++)  
               r = SQLFetch(chstmt);  
  
         }  
      }  
   }  
  
   r = SQLCloseCursor(chstmt);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLFreeHandle(SQL_HANDLE_STMT, chstmt);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
}  
  
void printOrder(SQLINTEGER OrdNo) {  
   SQLRETURN r;  
   SQLTCHAR OrdDate[24], CustCode[6];  
   SQLINTEGER ProdCode, Qty;  
   SQLLEN cbOrdNo, cbCustCode, cbOrdDate, cbProdCode, cbQty;   
  
   r = SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_LONG, SQL_INTEGER,10, 0, &OrdNo, sizeof(SQLINTEGER), &cbOrdNo);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   cbOrdNo = 0;  
  
   r = SQLExecDirect(hstmt, (SQLTCHAR *) _T("select CustCode, OrdDate from TVPOrd where OrdNo=?"), SQL_NTS);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLFetch(hstmt);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);  
      exit(-1);  
   }  
  
   r = SQLGetData(hstmt,1, SQL_C_TCHAR, CustCode, sizeof(CustCode), &cbCustCode);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);  
      exit(-1);  
   }  
  
   r = SQLGetData(hstmt,2, SQL_C_TCHAR, OrdDate, sizeof(OrdDate), &cbOrdDate);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);  
      exit(-1);  
   }  
  
   _tprintf(_T("OrderNo %i - Date %s - Cust %s\n"), OrdNo, OrdDate, CustCode);  
  
   r = SQLCloseCursor(hstmt);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);  
      exit(-1);  
   }  
  
   r = SQLExecDirect(hstmt, (SQLTCHAR *) _T("select ProdCode, Qty from TVPItem where OrdNo=?"), SQL_NTS);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);  
      exit(-1);  
   }  
  
   r = SQLFetch(hstmt);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO && r != SQL_NO_DATA) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   while (r == SQL_SUCCESS || r == SQL_SUCCESS_WITH_INFO) {   
      r = SQLGetData(hstmt,1, SQL_C_LONG, &ProdCode, sizeof(ProdCode), &cbProdCode);  
      if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
         ODBCError(henv, hdbc, hstmt, NULL, true);  
         exit(-1);  
      }  
  
      r = SQLGetData(hstmt,2, SQL_C_LONG, &Qty, sizeof(Qty), &cbQty);  
      if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
         ODBCError(henv, hdbc, hstmt, NULL, true);  
         exit(-1);  
      }  
  
      _tprintf(_T("        Product %i - Quantity %i\n"), ProdCode, Qty);  
  
      r = SQLFetch(hstmt);  
      if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO && r != SQL_NO_DATA) {  
         ODBCError(henv, hdbc, hstmt, NULL, true);  
         exit(-1);  
      }  
   }  
  
   r = SQLCloseCursor(hstmt);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
  
   r = SQLFreeStmt(hstmt, SQL_RESET_PARAMS);  
   if (r != SQL_SUCCESS && r != SQL_SUCCESS_WITH_INFO) {  
      ODBCError(henv, hdbc, hstmt, NULL, true);   
      exit(-1);  
   }  
}  
  
void testSimpleOrderEntry() {  
  
   OrdEntryData order;  
  
   order.OrdNo = 0;  
   order.OrdDate[0] = _T('\0');  
   _tcscpy_s((TCHAR *) order.CustCode, _countof(order.CustCode), _T("CUST1"));  
   order.ProdCode[0] = 10;  
   order.Qty[0] = 1;  
   order.ProdCode[1] = 20;  
   order.Qty[1] = 2;  
   order.ProdCode[2] = 30;  
   order.Qty[2] = 3;  
   order.ProdCode[3] = 40;  
   order.Qty[3] = 4;  
   order.ItemCount = 4;  
  
   OrdEntry_Simple(order);  
   printOrder(order.OrdNo);  
}  
  
void testPAOrderEntry() {  
  
   OrdEntryData order;  
  
   order.OrdNo = 0;  
   order.OrdDate[0] = _T('\0');  
   _tcscpy_s ((TCHAR *) order.CustCode, _countof(order.CustCode), _T("CUST2"));  
   order.ProdCode[0] = 100;  
   order.Qty[0] = 10;  
   order.ProdCode[1] = 200;  
   order.Qty[1] = 20;  
   order.ProdCode[2] = 300;  
   order.Qty[2] = 30;  
   order.ProdCode[3] = 400;  
   order.Qty[3] = 40;  
   order.ItemCount = 4;  
  
   OrdEntry_PA(order);  
   printOrder(order.OrdNo);  
}  
  
void testTVPOrderEntry() {  
   OrdEntryData order;  
  
   order.OrdNo = 0;  
   order.OrdDate[0] = _T('\0');  
   _tcscpy_s((TCHAR *) order.CustCode, _countof(order.CustCode), _T("CUST3"));  
   order.ProdCode[0] = 1000;  
   order.Qty[0] = 100;  
   order.ProdCode[1] = 2000;  
   order.Qty[1] = 200;  
   order.ProdCode[2] = 3000;  
   order.Qty[2] = 300;  
   order.ProdCode[3] = 4000;  
   order.Qty[3] = 400;  
   order.ItemCount = 4;  
  
   OrdEntry_TVP(order);  
  
   printOrder(order.OrdNo);  
}  
  
int _tmain() {  
   connect();  
   setup_ODBC_basics();  
   setup_TVP_demo();  
  
   testSimpleOrderEntry();  
   testPAOrderEntry ();  
   testTVPOrderEntry();  
   demo_metadata_from_catalog_APIs((SQLTCHAR *)_T("TVPOrderEntry"));  
}  
```  
  
  
