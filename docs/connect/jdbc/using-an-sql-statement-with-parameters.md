---
title: "Using a SQL statement with parameters"
description: "To work with a SQL statement that contains IN parameters, use the executeQuery method of the SQLServerPreparedStatement class to return a SQLServerResultSet."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 12/18/2025
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
---

# Using a SQL statement with parameters

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

To work with data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using a SQL statement that contains IN parameters, you can use the [executeQuery](../../connect/jdbc/reference/executequery-method-sqlserverpreparedstatement.md) method of the [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) class. This class returns a [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) that contains the requested data. First create a SQLServerPreparedStatement object by using the [prepareStatement](../../connect/jdbc/reference/preparestatement-method-sqlserverconnection.md) method of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class.

When you construct your SQL statement, the IN parameters are specified by using the `?` (question mark) character, which acts as a placeholder for the parameter values that are passed into the SQL statement. To specify a value for a parameter, use one of the setter methods of the SQLServerPreparedStatement class. The data type of the value that you pass into the SQL statement determines the setter method that you use.

When you pass a value to the setter method, you must specify not only the actual value to be used in the SQL statement, but also the parameter's ordinal placement in the SQL statement. For example, if your SQL statement contains a single parameter, its ordinal value is 1. If the statement contains two parameters, the first ordinal value is 1, while the second ordinal value is 2.

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database is passed in to the function. Then a SQL prepared statement is constructed and run with a single String parameter value. Then the results are read from the result set.

[!code[JDBC#UsingSQLWithParams1](../../connect/jdbc/codesnippet/Java/using-an-sql-statement-w_1_1.java)]

## Related content

- [Using statements with SQL](using-statements-with-sql.md)
- [Prepared statement parameter performance for the JDBC driver](prepared-statement-parameter-performance.md)
