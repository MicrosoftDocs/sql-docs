---
title: "Handling Errors | Microsoft Docs"
ms.custom: ""
ms.date: "01/21/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 8fd5b5ef-d939-4b78-b900-5b7b6ddb3eb9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Handling Errors
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  When using the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], all database error conditions are returned to your Java application as exceptions using the [SQLServerException](../../connect/jdbc/reference/sqlserverexception-class.md) class. The following methods of the SQLServerException class are inherited from java.sql.SQLException and java.lang.Throwable; and they can be used to return specific information about the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error that has occurred:  
  
-   getSQLState() returns the standard X/Open or SQL99 state code of the exception.
  
-   getErrorCode() returns the specific database error number.
  
-   getMessage() returns the full text of the exception. The error message text describes the problem, and frequently includes placeholders for information, such as object names, that are inserted in the error message when it is displayed.
  
-   getNextException() returns the next SQLServerException object or null if there are no more exception objects to return.

-   getSQLServerError() returns SQLServerError object containing detailed info about exception as received from SQL Server. This method returns null if no server error has occurred.

The following methods of SQLServerError class can be used to obtain additional details about the error generated from the server.

-   SQLServerError.getErrorMessage() returns the error message as received from the server.

-   SQLServerError.getErrorNumber() returns a number that identifies the type of the error.

-   SQLServerError.getErrorState() returns a numeric error code from SQL Server that represents an error, warning or "no data found" message.

-   SQLServerError.getErrorSeverity() returns the severity level of the error received.

-   SQLServerError.getServerName() returns the name of the computer that is running an instance of SQL Server that generated the error.

-   SQLServerError.getProcedureName() returns the name of the stored procedure or remote procedure call (RPC) that generated the error.

-   SQLServerError.getLineNumber() returns the line number within the Transact-SQL command batch or stored procedure that generated the error.
  
 In the following example, an open connection to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database is passed in to the function and a malformed SQL statement is constructed that does not have a FROM clause. Then, the statement is run and an SQL exception is processed.  
  
 [!code[JDBC#HandlingErrors1](../../connect/jdbc/codesnippet/Java/handling-errors_1.java)]  
  
## See Also  
 [Diagnosing Problems with the JDBC Driver](../../connect/jdbc/diagnosing-problems-with-the-jdbc-driver.md)  
  
  
