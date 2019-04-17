---
title: "Connection Resiliency in the Windows ODBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 614fa0b4-e9fd-4c68-aab3-183f9b9df143
author: MightyPen
ms.author: genemi
manager: craigg
---
# Connection Resiliency in the Windows ODBC Driver
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

  To ensure that applications remain connected to an [!INCLUDE[ssAzure](../../../includes/ssazure_md.md)], the ODBC driver on Windows can restore idle connections.  
  
> [!IMPORTANT]  
>  The connection resiliency feature is supported on Microsoft Azure SQL Databases and SQL Server 2014 (and later) server versions.  
  
 For additional information about idle connection resiliency, see [Technical Article - Idle Connection Resiliency](https://download.microsoft.com/download/D/2/0/D20E1C5F-72EA-4505-9F26-FEF9550EFD44/Idle%20Connection%20Resiliency.docx).
  
 To control reconnect behavior, the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Windows has two options:  
  
-   Connection retry count.  
  
     Connect retry count controls the number of reconnection attempts if there is a connection failure. Valid values range from 0 to 255. Zero (0) means do not attempt to reconnect. The default value is one reconnection attempt.  
  
     You can modify the number of connection retries when you:  
  
    -   Define or modify a data source that uses the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] with the **Connection Retry Count** control.  
  
    -   Use the **ConnectRetryCount** connection string keyword.  
  
     To retrieve the number of connection retry attempts, use the **SQL_COPT_SS_CONNECT_RETRY_COUNT** (read only) connection attribute. If an application connects to a server that does not support connection resiliency, **SQL_COPT_SS_CONNECT_RETRY_COUNT** returns 0.  
  
-   Connect retry interval.  
  
     The connect retry interval specifies the number of seconds between each connection retry attempt. Valid values are 1-60. The total time to reconnect cannot exceed the connection timeout (SQL_ATTR_QUERY_TIMEOUT in SQLSetStmtAttr). The default value is 10 seconds.  
  
     You can modify the connection retry interval when you:  
  
    -   Define or modify a data source that uses the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] with the **Connect Retry Interval** control.  
  
    -   Use the **ConnectRetryInterval** connection string keyword.  
  
     To retrieve the length of the connection retry interval, use the **SQL_COPT_SS_CONNECT_RETRY_INTERVAL** (read only) connection attribute.  
  
 If an application establishes a connection with SQL_DRIVER_COMPLETE_REQUIRED and later tries to execute a statement over a broken connection, the ODBC driver will not display the dialog box again. Also, during recovery in progress,  
  
-   During recovery, any call to **SQLGetConnectAttr(SQL_COPT_SS_CONNECTION_DEAD)**, must return **SQL_CD_FALSE**.  
  
-   If recovery fails, any call to **SQLGetConnectAttr(SQL_COPT_SS_CONNECTION_DEAD)**, must return **SQL_CD_TRUE**.  
  
 The following state codes are returned by any function that executes a command on the server:  
  
|State|Message|  
|-----------|-------------|  
|IMC01|The connection is broken and recovery is not possible. The client driver attempted to recover the connection one or more times and all attempts failed. Increase the value of ConnectRetryCount to increase the number of recovery attempts.|  
|IMC02|The server did not acknowledge a recovery attempt, connection recovery is not possible.|  
|IMC03|The server did not preserve the exact client TDS version requested during a recovery attempt, connection recovery is not possible.|  
|IMC04|The server did not preserve the exact server major version requested during a recovery attempt, connection recovery is not possible.|  
|IMC05|The connection is broken and recovery is not possible. The connection is marked by the server as unrecoverable. No attempt was made to restore the connection.|  
|IMC06|The connection is broken and recovery is not possible. The connection is marked by the client driver as unrecoverable. No attempt was made to restore the connection.|  
  
## Example  
 The following sample contains two functions. **func1** shows how you can connect with a data source name (DSN) that uses the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Windows. The DSN uses [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication, and it specifies the user ID. **func1** then retrieves the number of connection retries with **SQL_COPT_SS_CONNECT_RETRY_COUNT**.  
  
 **func2** uses **SQLDriverConnect**, **ConnectRetryCount** connection string keyword, and connection attributes to retrieve the setting for connection retries and retry interval.  
  
```  
// Connection_resiliency.cpp  
// compile with: odbc32.lib  
#include <windows.h>  
#include <stdio.h>  
#include <sqlext.h>  
#include <msodbcsql.h>  
  
void func1() {  
   SQLHENV henv;  
   SQLHDBC hdbc;  
   SQLHSTMT hstmt;  
   SQLRETURN retcode;  
   SQLSMALLINT i = 21;  

   // Allocate environment handle  
   retcode = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &henv);  
  
   // Set the ODBC version environment attribute  
   if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
      retcode = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (void*)SQL_OV_ODBC3, 0);   
  
      // Allocate connection handle  
      if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
         retcode = SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc);   
  
         // Set login timeout to 5 seconds  
         if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
            SQLSetConnectAttr(hdbc, SQL_LOGIN_TIMEOUT, (SQLPOINTER)5, 0);  
  
            // Connect to data source  
            retcode = SQLConnect(hdbc, (SQLCHAR*) "MyDSN", SQL_NTS, (SQLCHAR*) "userID", SQL_NTS, (SQLCHAR*) "password_for_userID", SQL_NTS);  
            retcode = SQLGetConnectAttr(hdbc, SQL_COPT_SS_CONNECT_RETRY_COUNT, &i, SQL_IS_INTEGER, NULL);  
  
            // Allocate statement handle  
            if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
               retcode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt);   
  
               // Process data  
               if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
                  SQLFreeHandle(SQL_HANDLE_STMT, hstmt);  
               }  
  
               SQLDisconnect(hdbc);  
            }  
  
            SQLFreeHandle(SQL_HANDLE_DBC, hdbc);  
         }  
      }  
      SQLFreeHandle(SQL_HANDLE_ENV, henv);  
   }  
}
  
void func2() {  
   SQLHENV henv;  
   SQLHDBC hdbc1;  
   SQLHSTMT hstmt;  
   SQLRETURN retcode;  
   SQLSMALLINT i = 21;  
  
#define MAXBUFLEN 255  
  
   SQLCHAR ConnStrIn[MAXBUFLEN] = "DRIVER={ODBC Driver 13 for SQL Server};SERVER=server_that_supports_connection_resiliency;UID=userID;PWD= password_for_userID;ConnectRetryCount=2";
   SQLCHAR ConnStrOut[MAXBUFLEN];

   SQLSMALLINT cbConnStrOut = 0;  
 
   // Allocate environment handle  
   retcode = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &henv);  
  
   // Set the ODBC version environment attribute  
   if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
  
      retcode = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER*)SQL_OV_ODBC3_80, SQL_IS_INTEGER);   
  
      // Allocate connection handle  
      if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
         retcode = SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc1);   
  
         // Set login timeout to 5 seconds  
         if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
            // SQLSetConnectAttr(hdbc1, SQL_LOGIN_TIMEOUT, (SQLPOINTER)5, 0);  
  
            retcode = SQLDriverConnect(hdbc1, NULL, ConnStrIn, SQL_NTS, NULL, 0, NULL, SQL_DRIVER_NOPROMPT);  
         }  
         retcode = SQLGetConnectAttr(hdbc1, SQL_COPT_SS_CONNECT_RETRY_COUNT, &i, SQL_IS_INTEGER, NULL);  
         retcode = SQLGetConnectAttr(hdbc1, SQL_COPT_SS_CONNECT_RETRY_INTERVAL, &i, SQL_IS_INTEGER, NULL);  
      }  
   }  
}  
  
int main() {  
   func1();  
   func2();  
}  
```  
  
## See Also  
 [Microsoft ODBC Driver for SQL Server on Windows](../../../connect/odbc/windows/microsoft-odbc-driver-for-sql-server-on-windows.md)  
  
  
