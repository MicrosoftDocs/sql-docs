---
title: Using an SQL statement with no parameters
description: Learn how to execute SQL statement with no parameters using the Microsoft JDBC Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 04/20/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Using an SQL statement with no parameters

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

To work with data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using an SQL statement that contains no parameters, you can use the [executeQuery](reference/executequery-method-sqlserverstatement.md) method of the [SQLServerStatement](reference/sqlserverstatement-class.md) class to return a [SQLServerResultSet](reference/sqlserverresultset-class.md) that will contain the requested data. First create a SQLServerStatement object by using the [createStatement](reference/createstatement-method-sqlserverconnection.md) method of the [SQLServerConnection](reference/sqlserverconnection-class.md) class.

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database is passed in to the `executeStatement` function. From there, a SQL statement is constructed and run. Finally, the results are read from the result set.

:::code language="java" source="codesnippet/Java/using-an-sql-statement-w_0_1.java":::

For more information about using result sets, see [Managing result sets with the JDBC driver](managing-result-sets-with-the-jdbc-driver.md).

## See also

[Using statements with SQL](using-statements-with-sql.md)
