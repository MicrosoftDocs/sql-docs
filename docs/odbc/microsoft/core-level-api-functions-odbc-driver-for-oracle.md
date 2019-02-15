---
title: "Core Level API Functions (ODBC Driver for Oracle) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "functions [ODBC], ODBC driver for Oracle"
  - "ODBC driver for Oracle [ODBC], functions"
  - "core level API functions [ODBC]"
  - "ODBC core level API functions [ODBC]"
ms.assetid: 8596eed7-bda6-4cac-ae1f-efde1aab785f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Core Level API Functions (ODBC Driver for Oracle)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 Functions at this level comprise the minimum level of interface conformance for ODBC drivers.  
  
|API function|Notes|  
|------------------|-----------|  
|**SQLAllocConnect**|Allocates memory for a connection handle, *hdbc*, within the environment identified by *henv*. The Driver Manager processes this call and calls the driver's **SQLAllocConnect** function whenever **SQLConnect**, **SQLBrowseConnect**, or **SQLDriverConnect** is called.|  
|**SQLAllocEnv**|Displays a dialog box specifying the requirement for Oracle Client software and then returns SQL_NULL_HANDLE. If the Oracle Client software is not installed, this function allocates memory for an environment handle, *henv*,and initializes the ODBC call-level interface for use by an application.|  
|**SQLAllocStmt**|Allocates memory for a statement handle and associates the statement handle with the connection specified by hdbc. The Driver Manager passes this call to the driver, which allocates the memory for the hstmt structure.|  
|**SQLBindCol**|Assigns storage space for a result column and specifies the type of the result.|  
|**SQLCancel**|Cancels the processing on a statement handle, hstmt. In some cases, Oracle does not allow cancellation of a running statement. This means that a running statement will continue until Oracle completes the process, at which time the results from the statements are canceled by the ODBC Driver for Oracle.|  
|**SQLColAttributes**|Returns descriptor information for a column in a result set. Descriptor information is returned as a character string, a 32-bit descriptor-dependent value, or an integer value.|  
|**SQLConnect**|Connects to a data source. To use Oracle Operating System Authentication, specify "/" as the *szUID* parameter and "" as the *szAuthStr* parameter.|  
|**SQLDescribeCol**|Returns the name, type, precision, scale, and nullability of the given result column. **Note:  SQLDescribeCol** reports calculated columns as SQL_VARCHAR.|  
|**SQLDisconnect**|Closes a connection. If connection pooling is enabled for a shared environment and an application calls **SQLDisconnect** on a connection in that environment, the connection is returned to the connection pool and is still available to other components using the same shared environment.|  
|**SQLError**|Returns error or status information about the last error. The driver maintains a stack or list of errors that can be returned for the *hstmt*, *hdbc*, and *henv* arguments, depending on how the call to **SQLError** is made. The error queue is flushed after each statement. Usually retrieves an Oracle error message and is otherwise empty.|  
|**SQLExecDirect**|Executes a new, unprepared SQL statement. The driver uses the current values of the parameter marker variables if any parameters exist in the statement. If your table, view, or field names contain spaces, enclose the names in back quote marks. For example, if your database contains a table named *My Table* and the field *My Field*, enclose each element of the identifier like so:<br /><br /> SELECT \`My Table\`. \`My Field1\`,; \`My Table\`.\`My Field2\` FROM \`My Table`|  
|**SQLExecute**|Executes a prepared SQL statement (a statement already prepared by **SQLPrepare**). The driver uses the current values of the parameter marker variables if any parameters exist in the statement.|  
|**SQLFetch**|Retrieves one row from a result set into the locations specified by the previous calls to **SQLBindCol**. Prepares the driver for a call to **SQLGetData** for the unbound columns.|  
|**SQLFreeConnect**|Releases a connection handle and frees all memory allocated for the handle.|  
|**SQLFreeEnv**|Closes the ODBC Driver for Oracle and releases all memory associated with the driver.|  
|**SQLFreeStmt**|Stops processing associated with a specific hstmt, closes any open cursors associated with the hstmt, discards pending results, and optionally frees all resources associated with the statement handle.|  
|**SQLGetCursorName**|Returns the name of the cursor associated with the given hstmt.|  
|**SQLNumResultCols**|Returns the number of columns in a result set cursor.|  
|**SQLPrepare**|Prepares an SQL statement by planning how to optimize and execute the statement. The SQL statement is compiled for execution by **SQLExecDirect**.<br /><br /> If your table, view, or field names contain spaces, enclose the names in back quote marks. For example, if your database contains a table named *My Table* and the field *My Field*, enclose each element of the identifier as follows:<br /><br /> SELECT \`My Table\`.\`My Field\` FROM \`My Table`<br /><br /> For information about using result sets containing arrays as formal parameters, see [Returning Array Parameters from Stored Procedures](../../odbc/microsoft/returning-array-parameters-from-stored-procedures.md).|  
|**SQLRowCount**|Oracle does not provide a way to determine the number of rows in a result set until after you fetch the last row, so it returns -1.|  
|**SQLSetCursorName**|Associates a cursor name with an active statement handle, *hstmt*.|  
|**SQLSetParam**|Replaced by SQLBindParameter in ODBC 2.*x*.|  
|**SQLTransact**|Requests a commit or rollback operation for all active operations on all statement handles (hstmts) associated with a connection, or for all connections associated with the environment handle, *henv*. If a commit fails when in manual mode, the transaction remains active; you can choose to roll back the transaction or retry the commit operation. If a commit operation fails when in automatic transaction mode, the transaction is rolled back automatically; the transaction cannot be inactive.|
