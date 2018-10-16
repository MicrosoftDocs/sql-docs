---
title: "Using an SQL Statement with No Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 4b0728bd-059b-4b71-895c-999335bc7427
author: MightyPen
ms.author: genemi
manager: craigg
---

# Using an SQL Statement with No Parameters

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

To work with data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using an SQL statement that contains no parameters, you can use the [executeQuery](../../connect/jdbc/reference/executequery-method-sqlserverstatement.md) method of the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) class to return a [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) that will contain the requested data. To do this, you must first create a SQLServerStatement object by using the [createStatement](../../connect/jdbc/reference/createstatement-method-sqlserverconnection.md) method of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class.

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database is passed in to the function, an SQL statement is constructed and run, and then the results are read from the result set.

[!code[JDBC#UsingSQLWithNoParams1](../../connect/jdbc/codesnippet/Java/using-an-sql-statement-w_0_1.java)]

For more information about using result sets, see [Managing Result Sets with the JDBC Driver](../../connect/jdbc/managing-result-sets-with-the-jdbc-driver.md).

## See Also

[Using Statements with SQL](../../connect/jdbc/using-statements-with-sql.md)
