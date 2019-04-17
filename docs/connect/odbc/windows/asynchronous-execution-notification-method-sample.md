---
title: "Asynchronous Execution (Notification Method) Sample | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 55c51fff-119d-445f-8732-c1569966e559
author: MightyPen
ms.author: genemi
manager: craigg
---
# Asynchronous Execution (Notification Method) Sample
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

  The code samples in this topic demonstrate how to use the [Asynchronous Execution (Notification Method)](https://msdn.microsoft.com/library/hh405038(VS.85).aspx).  
  
 This function uses asynchronous notification to open five connections, and executes one query on a statement of each connection.  
  
```  
  
#define NUMBER_OPERATIONS 5  
int AsyncNotificationSample(void)  
{  
    RETCODE     rc;  
  
    SQLHENV     hEnv              = NULL;  
    SQLHDBC     arhDbc[NUMBER_OPERATIONS]         = {NULL};  
    SQLHSTMT    arhStmt[NUMBER_OPERATIONS]        = {NULL};  
  
    HANDLE      arhDBCEvent[NUMBER_OPERATIONS]    = {NULL};  
    RETCODE     arrcDBC[NUMBER_OPERATIONS]        = {0};  
    HANDLE      arhSTMTEvent[NUMBER_OPERATIONS]   = {NULL};  
    RETCODE     arrcSTMT[NUMBER_OPERATIONS]       = {0};  
  
    rc = SQLAllocHandle(SQL_HANDLE_ENV, NULL, &hEnv);  
    if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
  
    rc = SQLSetEnvAttr(hEnv,  
        SQL_ATTR_ODBC_VERSION,  
        (SQLPOINTER) SQL_OV_ODBC3_80,  
        SQL_IS_INTEGER);  
    if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
  
    // Connection operations begin here  
  
    // Alloc NUMBER_OPERATIONS connection handles  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        rc = SQLAllocHandle(SQL_HANDLE_DBC, hEnv, &arhDbc[i]);  
        if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
    }  
  
    // Enable DBC Async on all connection handles  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        rc= SQLSetConnectAttr(arhDbc[i], SQL_ATTR_ASYNC_DBC_FUNCTIONS_ENABLE, (SQLPOINTER)SQL_ASYNC_DBC_ENABLE_ON, SQL_IS_INTEGER);  
        if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
    }  
  
    // Application must create event objects  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        arhDBCEvent[i] = CreateEvent(NULL, FALSE, FALSE, NULL); // Auto-reset, initial state is not-signaled  
        if (!arhDBCEvent[i]) goto Cleanup;  
    }  
  
    // Enable notification on all connection handles  
    // Event  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        rc= SQLSetConnectAttr(arhDbc[i], SQL_ATTR_ASYNC_DBC_EVENT, arhDBCEvent[i], SQL_IS_POINTER);  
        if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
    }  
  
    // Initiate connect establishing  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        SQLDriverConnect(arhDbc[i], NULL, (SQLTCHAR*)TEXT("Driver={ODBC Driver 11 for SQL Server};SERVER=your_server;DATABASE=your_database;UID=sa;PWD=your_password;"), SQL_NTS, NULL, 0, NULL, SQL_DRIVER_NOPROMPT);  
    }  
  
    // Can do some other staff before calling WaitForMultipleObjects  
    WaitForMultipleObjects(NUMBER_OPERATIONS, arhDBCEvent, TRUE, INFINITE); // Wait All  
  
    // Complete connect API calls  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        SQLCompleteAsync(SQL_HANDLE_DBC, arhDbc[i], & arrcDBC[i]);  
    }  
  
    BOOL fFail = FALSE; // Whether some connection opening fails.  
  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        if ( !SQL_SUCCEEDED(arrcDBC[i]) )   
            fFail = TRUE;  
    }  
  
    // If some SQLDriverConnect() fail, clean up.  
    if (fFail)  
    {  
        for (int i=0; i<NUMBER_OPERATIONS; i++)  
        {  
            if (SQL_SUCCEEDED(arrcDBC[i]) )   
            {  
                SQLDisconnect(arhDbc[i]); // This is also async  
            }  
            else  
            {  
                SetEvent(arhDBCEvent[i]); // Previous SQLDriverConnect() failed. No need to call SQLDisconnect().  
            }  
        }  
        WaitForMultipleObjects(NUMBER_OPERATIONS, arhDBCEvent, TRUE, INFINITE);   
        for (int i=0; i<NUMBER_OPERATIONS; i++)  
        {  
            if (SQL_SUCCEEDED(arrcDBC[i]) )   
            {     
                SQLCompleteAsync(SQL_HANDLE_DBC, arhDbc[i], &arrcDBC[i]);; // To Complete  
            }  
        }  
  
        goto Cleanup;  
    }  
  
    // Statement Operations begin here  
  
    // Alloc statement handle  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        rc = SQLAllocHandle(SQL_HANDLE_STMT, arhDbc[i], &arhStmt[i]);  
        if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
    }  
  
    // Enable STMT Async on all statement handles  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        rc = SQLSetStmtAttr(arhStmt[i], SQL_ATTR_ASYNC_ENABLE, (SQLPOINTER)SQL_ASYNC_ENABLE_ON, SQL_IS_INTEGER);  
        if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
    }  
  
    // Create event objects  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        arhSTMTEvent[i] = CreateEvent(NULL, FALSE, FALSE, NULL); // Auto-reset, initial state is not-signaled  
        if (!arhSTMTEvent[i]) goto Cleanup;  
    }  
  
    // Enable notification on all statement handles  
    // Event  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        rc= SQLSetStmtAttr(arhStmt[i], SQL_ATTR_ASYNC_STMT_EVENT, arhSTMTEvent[i], SQL_IS_POINTER);  
        if ( !SQL_SUCCEEDED(rc) ) goto Cleanup;  
    }  
  
    // Initiate SQLExecDirect() calls  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        SQLExecDirect(arhStmt[i], (SQLTCHAR*)TEXT("select au_lname, au_fname from authors"), SQL_NTS);  
    }  
  
    // Can do some other staff before calling WaitForMultipleObjects  
    WaitForMultipleObjects(NUMBER_OPERATIONS, arhSTMTEvent, TRUE, INFINITE); // Wait All  
  
    // Now, call SQLCompleteAsync to complete the operation and get return code  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        SQLCompleteAsync(SQL_HANDLE_STMT, arhStmt[i], &arrcSTMT[i]);  
    }  
  
    // Check return values  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        if ( !SQL_SUCCEEDED(arrcSTMT[i]) ) goto Cleanup;  
    }  
  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        //Do some binding jobs here, set SQL_ATTR_ROW_ARRAY_SIZE   
    }  
  
    // Now, initiate fetching  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        SQLFetch(arhStmt[i]);  
    }  
  
    // Can do some other staff before calling WaitForMultipleObjects  
    WaitForMultipleObjects(NUMBER_OPERATIONS, arhSTMTEvent, TRUE, INFINITE);   
  
    // Now, to complete the operations and get return code  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        SQLCompleteAsync(SQL_HANDLE_STMT, arhStmt[i], &arrcSTMT[i]);  
    }  
  
    // Check return code  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        if ( !SQL_SUCCEEDED(arrcSTMT[i]) ) goto Cleanup;  
    }  
  
    // USE fetched data here!!  
  
Cleanup:  
  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        if (arhStmt[NUMBER_OPERATIONS])  
        {  
            SQLFreeHandle(SQL_HANDLE_STMT, arhStmt[i]);  
            arhStmt[i] = NULL;  
        }  
    }  
  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        if (arhSTMTEvent[i])  
        {  
            CloseHandle(arhSTMTEvent[i]);  
            arhSTMTEvent[i] = NULL;  
        }  
    }  
  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        if (arhDbc[i])  
        {  
            SQLFreeHandle(SQL_HANDLE_DBC, arhDbc[i]);  
            arhDbc[i] = NULL;  
        }  
    }  
  
    for (int i=0; i<NUMBER_OPERATIONS; i++)  
    {  
        if (arhDBCEvent[i])  
        {  
            CloseHandle(arhDBCEvent[i]);  
            arhDBCEvent[i] = NULL;  
        }  
    }  
  
    if (hEnv)  
    {  
        SQLFreeHandle(SQL_HANDLE_ENV, hEnv);  
        hEnv = NULL;  
    }  
  
    return 0;  
}  
```  
  
 This function shows a pattern to start multiple operations simultaneously and wait for them when asynchronous notification is used:  
  
```  
#define ODBCVER 0x0380  
#define _SQLNCLI_ODBC  
  
// Global variables  
  
const int g_nConnection = 700;  
SQLHENV g_hEnv = NULL;  
SQLHDBC g_hDbcs[g_nConnection];  
HANDLE g_hevents[g_nConnection];  
  
LONG volatile g_JobDoneNumber;  
  
struct  
{  
    char szOutConnectionString[500];  
    SQLSMALLINT iLen;  
} g_connOut[g_nConnection];  
  
void CALLBACK WaitCallBack(PTP_CALLBACK_INSTANCE Inst, PVOID Context, PTP_WAIT Wait, TP_WAIT_RESULT WaitResult)  
{  
    UINT_PTR i = reinterpret_cast<UINT_PTR>(Context);  
    SQLRETURN rc ;  
    SQLCompleteAsync(SQL_HANDLE_DBC, g_hDbcs[(int)i], &rc);  
    printf("Connection %d done: RC: %d, threadid:%u \n", (int)i, rc, GetCurrentThreadId());  
    InterlockedIncrement(&g_JobDoneNumber);  
}  
  
int _tmain(int argc, _TCHAR* argv[])
{
    for(int i = 0; i< g_nConnection; i++)
        g_hevents[i] = CreateEvent(NULL, FALSE, FALSE, NULL);
  
    PTP_WAIT waits[g_nConnection];
    for(int i = 0; i < g_nConnection; i++)
    {  
        waits[i] = CreateThreadpoolWait(&WaitCallBack, reinterpret_cast<PVOID>((UINT_PTR)i), NULL);
        SetThreadpoolWait(waits[i], g_hevents[i], NULL);
    }

    SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE,&g_hEnv);
    SQLSetEnvAttr(g_hEnv,SQL_ATTR_ODBC_VERSION, (SQLPOINTER)SQL_OV_ODBC3_80, SQL_IS_UINTEGER);
    for(int i = 0; i < g_nConnection; i++)
    {  
        SQLAllocHandle( SQL_HANDLE_DBC, g_hEnv , &g_hDbcs[i]);
        SQLSetConnectAttr(
            g_hDbcs[i],  
            SQL_ATTR_ASYNC_DBC_FUNCTIONS_ENABLE,  
            (SQLPOINTER)SQL_ASYNC_ENABLE_ON,  
            SQL_IS_INTEGER);  
        SQLSetConnectAttr(g_hDbcs[i], SQL_ATTR_ASYNC_DBC_EVENT, g_hevents[i], SQL_IS_POINTER);
    }
  
    // make connections  
    g_JobDoneNumber = 0;  
    for(int i = 0; i < g_nConnection; i++)  
    {  
        SQLDriverConnect(g_hDbcs[i],NULL,
            (SQLCHAR*)"DRIVER={ODBC Driver 13 for SQL Server};Server=your_server;database=your_database;uid=usr;pwd=your_password",
            SQL_NTS, (SQLCHAR*)g_connOut[i].szOutConnectionString, 500, &g_connOut[i].iLen, SQL_DRIVER_NOPROMPT);  
    }

    printf("connect wait..\n");
    while(g_JobDoneNumber < g_nConnection)
        SleepEx(50, false);

    // disconnect  
    for(int i = 0; i < g_nConnection; i++)  
      SetThreadpoolWait(waits[i], g_hevents[i], NULL);

    printf("disconnect wait..\n");
    g_JobDoneNumber = 0;
    for(int i = 0; i < g_nConnection; i++)
        SQLDisconnect(g_hDbcs[i]);

    while(g_JobDoneNumber < g_nConnection)  
    SleepEx(50, false);

    for(int i = 0; i < g_nConnection; i++)  
        CloseThreadpoolWait(waits[i]);  

    for(int i = 0; i < g_nConnection; i++)
    {  
        CloseHandle(g_hevents[i]);
        SQLFreeHandle(SQL_HANDLE_DBC, g_hDbcs[i]);
    }
    SQLFreeHandle(SQL_HANDLE_ENV, g_hEnv);
    return 0;
}
```  
  
## See Also  
 [Microsoft ODBC Driver for SQL Server on Windows](../../../connect/odbc/windows/microsoft-odbc-driver-for-sql-server-on-windows.md)  
  
  
