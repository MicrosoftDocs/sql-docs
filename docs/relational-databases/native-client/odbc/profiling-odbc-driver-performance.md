---
title: "Profiling ODBC Driver Performance | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "profiling ODBC driver performance data [SQL Server Native Client]"
  - "performance [ODBC]"
  - "application statistics [ODBC]"
  - "time statistics [ODBC]"
  - "ODBC, performance data"
  - "SQL Server Native Client ODBC driver, profiling performance data"
  - "SQLPERF data structure"
  - "statistical information [ODBC]"
ms.assetid: 8f44e194-d556-4119-a759-4c9dec7ecead
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Profiling ODBC Driver Performance
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

  The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver can profile two types of performance data:  
  
-   Long-running queries.  
  
     The driver can write to a log file any query that does not get a response from the server within a specified amount of time. Application programmers or database administrators can then research each logged SQL statement to determine how they can improve its performance.  
  
-   Driver-performance data.  
  
     The driver can record performance statistics and either write them to a file or make them available to an application through a driver-specific data structure named SQLPERF. The file containing the performance statistics is a tab-delimited file that can be easily analyzed with any spreadsheet that supports tab-delimited files, such as Microsoft Excel.  
  
 Either type of profiling can be turned on by:  
  
-   Connecting to a data source that specifies logging.  
  
-   Calling [SQLSetConnectAttr](../../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md) to set driver-specific attributes that control profiling.  
  
 Each application process gets its own copy of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver, and profiling is global to the combination of a driver copy and an application process. When anything in the application turns on profiling, profiling records information for all connections active in the driver from that application. Even connections that did not specifically call for profiling are included.  
  
 After the driver has opened a profiling log (either the performance data or long-running query log), it does not close the log until the driver is unloaded by the ODBC Driver Manager, when an application frees all the environment handles it opened in the driver. If the application opens a new environment handle, a new copy of the driver is loaded. If the application then either connects to a data source that specifies the same log file or sets the driver-specific attributes to log to the same file, the driver overwrites the old log.  
  
 If an application starts profiling to a log file and a second application attempts to start profiling to the same log file, the second application is not able to log any profiling data. If the second application starts profiling after the first application has unloaded its driver, the second application overwrites the log file from the first application.  
  
 If an application connects to a data source that has profiling enabled, the driver returns SQL_ERROR if the application calls **SQLSetConnectOption** to start logging. A call to **SQLGetDiagRec** then returns the following:  
  
```  
SQLState: 01000, pfNative = 0  
ErrorMsg: [Microsoft][SQL Server Native Client]  
   An error has occurred during the attempt to access  
   the log file, logging disabled.  
```  
  
 The driver stops gathering performance data when an environment handle is closed. If an [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client application has multiple connections, each with its own environment handle, then the driver will stop gathering performance data when any of the associated environment handles are closed.  
  
 The driver's performance data can either be stored in the SQLPERF data structure or logged in a tab-delimited file. The data includes the following categories of statistics:  
  
-   Application profile  
  
-   Connection  
  
-   Network  
  
-   Time  
  
 In the following table, the descriptions of the fields in the SQLPERF data structure also apply to the statistics recorded in the performance log file.  
  
### Application Profile Statistics  
  
|SQLPERF field|Description|  
|-------------------|-----------------|  
|TimerResolution|Minimum resolution of the server's clock time in milliseconds. This is usually reported as 0 (zero) and should only be considered if the number reported is large. If the minimum resolution of the server clock is larger than the likely interval for some of the timer-based statistics, those statistics could be inflated.|  
|SQLidu|Number of INSERT, DELETE, or UPDATE statements after SQL_PERF_START.|  
|SQLiduRows|Number of INSERT, DELETE, or UPDATE statements after SQL_PERF_START.|  
|SQLSelects|Number of SELECT statements processed after SQL_PERF_START.|  
|SQLSelectRows|Number of rows selected after SQL_PERF_START.|  
|Transactions|Number of user transactions after SQL_PERF_START, including rollbacks. When an ODBC application is running with SQL_AUTOCOMMIT_ON, each command is considered a transaction.|  
|SQLPrepares|Number of [SQLPrepare Function](https://go.microsoft.com/fwlink/?LinkId=59360) calls after SQL_PERF_START.|  
|ExecDirects|Number of **SQLExecDirect** calls after SQL_PERF_START.|  
|SQLExecutes|Number of **SQLExecute** calls after SQL_PERF_START.|  
|CursorOpens|Number of times the driver has opened a server cursor after SQL_PERF_START.|  
|CursorSize|Number of rows in the result sets opened by cursors after SQL_PERF_START.|  
|CursorUsed|Number of rows actually retrieved through the driver from cursors after SQL_PERF_START.|  
|PercentCursorUsed|Equals CursorUsed/CursorSize. For example, if an application causes the driver to open a server cursor to do "SELECT COUNT(*) FROM Authors," 23 rows will be in the result set for the SELECT statement. If the application then fetches only three of these rows, CursorUsed/CursorSize is 3/23, so PercentCursorUsed is 13.043478.|  
|AvgFetchTime|Equals SQLFetchTime/SQLFetchCount.|  
|AvgCursorSize|Equals CursorSize/CursorOpens.|  
|AvgCursorUsed|Equals CursorUsed/CursorOpens.|  
|SQLFetchTime|Cumulative amount of time it took fetches against server cursors to complete.|  
|SQLFetchCount|Number of fetches done against server cursors after SQL_PERF_START.|  
|CurrentStmtCount|Number of statement handles currently open on all connections open in the driver.|  
|MaxOpenStmt|Maximum number of concurrently opened statement handles after SQL_PERF_START.|  
|SumOpenStmt|Number of statement handles that have been opened after SQL_PERF_START.|  
|**Connection Statistics:**||  
|CurrentConnectionCount|Current number of active connection handles the application has open to the server.|  
|MaxConnectionsOpened|Maximum number of concurrent connection handles opened after SQL_PERF_START.|  
|SumConnectionsOpened|Sum of the number of connection handles that have been opened after SQL_PERF_START.|  
|SumConnectionTime|Sum of the amount of time that all of the connections have been opened after SQL_PERF_START. For example, if an application opened 10 connections and maintained each connection for 5 seconds, then SumConnectionTime would be 50 seconds.|  
|AvgTimeOpened|Equals SumConnectionsOpened/ SumConnectionTime.|  
|**Network Statistics:**||  
|ServerRndTrips|The number of times the driver sent commands to the server and got a reply back.|  
|BuffersSent|Number of Tabular Data Stream (TDS) packets sent to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] by the driver after SQL_PERF_START. Large commands can take multiple buffers, so if a large command is sent to the server and it fills six packets, ServerRndTrips is incremented by one and BuffersSent is incremented by six.|  
|BuffersRec|Number of TDS packets received by the driver from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] after the application started using the driver.|  
|BytesSent|Number of bytes of data sent to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in TDS packets after the application started using the driver.|  
|BytesRec|Number of bytes of data in TDS packets received by the driver from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] after the application started using the driver.|  
  
### Time Statistics  
  
|SQLPERF Field|Description|  
|-------------------|-----------------|  
|msExecutionTime|Cumulative amount of time the driver spent processing after SQL_PERF_START, including the time spent waiting for replies from the server.|  
|msNetworkServerTime|Cumulative amount of time the driver spent waiting for replies from the server.|  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](../../../relational-databases/native-client/odbc/sql-server-native-client-odbc.md)   
 [Profiling ODBC Driver Performance How-to Topics &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-how-to/profiling-odbc-driver-performance-odbc.md)  
  
  
