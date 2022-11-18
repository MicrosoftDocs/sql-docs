---
description: "Executing a Statement"
title: "Executing a Statement | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL statements [ODBC], executing"
ms.assetid: e5f0d2ee-0453-4faf-b007-12978dd300a1
author: David-Engel
ms.author: v-davidengel
---
# Executing a Statement
There are four ways to execute a statement, depending on when they are compiled (prepared) by the database engine and who defines them:  
  
-   **Direct Execution** The application defines the SQL statement. It is prepared and executed at run time in a single step.  
  
-   **Prepared Execution** The application defines the SQL statement. It is prepared and executed at run time in separate steps. The statement can be prepared once and executed multiple times.  
  
-   **Procedures** The application can define and compile one or more SQL statements at development time and store these statements on the data source as a procedure. The procedure is executed one or more times at run time. The application can enumerate available stored procedures using catalog functions.  
  
-   **Catalog Functions** The driver writer creates a function that returns a predefined result set. Usually, this function submits a predefined SQL statement or calls a procedure created for this purpose. The function is executed one or more times at run time.  
  
 A particular statement (as identified by its statement handle) can be executed any number of times. The statement can be executed with a variety of different SQL statements, or it can be executed repeatedly with the same SQL statement. For example, the following code uses the same statement handle (*hstmt1*) to retrieve and display the tables in the Sales database. It then reuses this handle to retrieve the columns in a table selected by the user.  
  
```  
SQLHSTMT    hstmt1;  
SQLCHAR *   Table;  
  
// Create a result set of all tables in the Sales database.  
SQLTables(hstmt1, "Sales", SQL_NTS, "sysadmin", SQL_NTS, NULL, 0, NULL, 0);  
  
// Fetch and display the table names; then close the cursor.  
// Code not shown.  
  
// Have the user select a particular table.  
SelectTable(Table);  
  
// Reuse hstmt1 to create a result set of all columns in Table.  
SQLColumns(hstmt1, "Sales", SQL_NTS, "sysadmin", SQL_NTS, Table, SQL_NTS, NULL, 0);  
  
// Fetch and display the column names in Table; then close the cursor.  
// Code not shown.  
```  
  
 And the following code shows how a single handle is used to repeatedly execute the same statement to delete rows from a table.  
  
```  
SQLHSTMT      hstmt1;  
SQLUINTEGER   OrderID;  
SQLINTEGER    OrderIDInd = 0;  
  
// Prepare a statement to delete orders from the Orders table.  
SQLPrepare(hstmt1, "DELETE FROM Orders WHERE OrderID = ?", SQL_NTS);  
  
// Bind OrderID to the parameter for the OrderID column.  
SQLBindParameter(hstmt1, 1, SQL_PARAM_INPUT, SQL_C_ULONG, SQL_INTEGER, 5, 0,  
                  &OrderID, 0, &OrderIDInd);  
  
// Repeatedly execute hstmt1 with different values of OrderID.  
while ((OrderID = GetOrderID()) != 0) {  
   SQLExecute(hstmt1);  
}  
```  
  
 For many drivers, allocating statements is an expensive task, so reusing the same statement in this manner is usually more efficient than freeing existing statements and allocating new ones. Applications that create result sets on a statement must be careful to close the cursor over the result set before reexecuting the statement; for more information, see [Closing the Cursor](../../../odbc/reference/develop-app/closing-the-cursor.md).  
  
 Reusing statements also forces the application to avoid a limitation in some drivers of the number of statements that can be active at one time. The exact definition of "active" is driver-specific, but it often refers to any statement that has been prepared or executed and still has results available. For example, after an **INSERT** statement has been prepared, it is generally considered to be active; after a **SELECT** statement has been executed and the cursor is still open, it is generally considered to be active; after a **CREATE TABLE** statement has been executed, it is not generally considered to be active.  
  
 An application determines how many statements can be active on a single connection at one time by calling **SQLGetInfo** with the SQL_MAX_CONCURRENT_ACTIVITIES option. An application can use more active statements than this limit by opening multiple connections to the data source; because connections can be expensive, however, the effect on performance should be considered.  
  
 Applications can limit the amount of time allotted for a statement to execute with the SQL_ATTR_QUERY_TIMEOUT statement attribute. If the timeout period expires before the data source returns the result set, the function executing the SQL statement returns SQLSTATE HYT00 (Timeout expired). By default, there is no timeout.  
  
 This section contains the following topics.  
  
-   [Direct Execution](../../../odbc/reference/develop-app/direct-execution-odbc.md)  
  
-   [Prepared Execution](../../../odbc/reference/develop-app/prepared-execution-odbc.md)  
  
-   [Procedures](../../../odbc/reference/develop-app/procedures-odbc.md)  
  
-   [Batches of SQL Statements](../../../odbc/reference/develop-app/batches-of-sql-statements.md)  
  
-   [Executing Catalog Functions](../../../odbc/reference/develop-app/executing-catalog-functions.md)
