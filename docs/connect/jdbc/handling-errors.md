---
title: Handling errors
description: Learn about error handling and what information the SQLServerException class provides in the Microsoft JDBC Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Handling errors

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

When using the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], all database error conditions are returned to your Java application as exceptions using the [SQLServerException](reference/sqlserverexception-class.md) class. The following methods of the SQLServerException class are inherited from java.sql.SQLException and java.lang.Throwable; and they can be used to return specific information about the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error that has occurred:

- `getSQLState()` returns the standard X/Open or SQL99 state code of the exception.

- `getErrorCode()` returns the specific database error number.

- `getMessage()` returns the full text of the exception. The error message text describes the problem, and frequently includes placeholders for information, such as object names, that are inserted in the error message when it's displayed.

- `getNextException()` returns the next `SQLServerException` object or null if there are no more exception objects to return.

- `getSQLServerError()` returns the `SQLServerError` object containing detailed info about the exception as received from SQL Server. This method returns null if no server error has occurred.

The following methods of the `SQLServerError` class can be used to obtain more details about the error generated from the server.

- `SQLServerError.getErrorMessage()` returns the error message as received from the server.

- `SQLServerError.getErrorNumber()` returns a number that identifies the type of the error.

- `SQLServerError.getErrorState()` returns a numeric error code from SQL Server that represents an error, warning or "no data found" message.

- `SQLServerError.getErrorSeverity()` returns the severity level of the error received.

- `SQLServerError.getServerName()` returns the name of the computer that is running an instance of SQL Server that generated the error.

- `SQLServerError.getProcedureName()` returns the name of the stored procedure or remote procedure call (RPC) that generated the error.

- `SQLServerError.getLineNumber()` returns the line number within the Transact-SQL command batch or stored procedure that generated the error.

In the next example, an open connection to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database is passed in to the function and a malformed SQL statement is constructed that doesn't have a FROM clause. Then, the statement is run and an SQL exception is processed.

[!code[JDBC#HandlingErrors1](codesnippet/Java/handling-errors_1.java)]

## See also

[Diagnosing problems with the JDBC driver](diagnosing-problems-with-the-jdbc-driver.md)
