---
title: "Using an SQL Statement with Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 3202b88f-ce13-44dd-982c-c6a3b0260378
author: MightyPen
ms.author: genemi
manager: craigg
---

# Using an SQL Statement with Parameters

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

To work with data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using an SQL statement that contains IN parameters, you can use the [executeQuery](../../connect/jdbc/reference/executequery-method-sqlserverpreparedstatement.md) method of the [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) class to return a [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) that will contain the requested data. To do this, you must first create a SQLServerPreparedStatement object by using the [prepareStatement](../../connect/jdbc/reference/preparestatement-method-sqlserverconnection.md) method of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class.

When you construct your SQL statement, the IN parameters are specified by using the ? (question mark) character, which acts as a placeholder for the parameter values that will later be passed into the SQL statement. To specify a value for a parameter, you can use one of the setter methods of the SQLServerPreparedStatement class. The setter method that you use is determined by the data type of the value that you want to pass into the SQL statement.

When you pass a value to the setter method, you must specify not only the actual value to be used in the SQL statement, but also the parameter's ordinal placement in the SQL statement. For example, if your SQL statement contains a single parameter, its ordinal value will be 1. If the statement contains two parameters, the first ordinal value will be 1, while the second ordinal value will be 2.

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database is passed in to the function, an SQL prepared statement is constructed and run with a single String parameter value, and then the results are read from the result set.

[!code[JDBC#UsingSQLWithParams1](../../connect/jdbc/codesnippet/Java/using-an-sql-statement-w_1_1.java)]

## See Also

[Using Statements with SQL](../../connect/jdbc/using-statements-with-sql.md)
