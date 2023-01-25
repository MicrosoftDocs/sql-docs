---
title: "Using holdability"
description: "Using holdability"
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Using holdability

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

By default, a result set created within a transaction is kept open after the transaction is committed to the database, or when it is rolled back. However, it is sometimes useful for the result set to be closed, after the transaction has been committed. To do this, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] supports the use of result set holdability.

Result set holdability can be set by using the [setHoldability](../../connect/jdbc/reference/setholdability-method-sqlserverconnection.md) method of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class. When setting the holdability by using the setHoldability method, the result set holdability constants of `ResultSet.HOLD_CURSORS_OVER_COMMIT` or `ResultSet.CLOSE_CURSORS_AT_COMMIT` can be used.

The JDBC driver also supports setting holdability when creating one of the Statement objects. When creating the Statement objects that have overloads with result set holdability parameters, the holdability of statement object must match the connection's holdability. When they don't match, an exception is thrown. It's because SQL Server supports the holdability only at the connection level.

The holdability of a result set is the holdability of the SQLServerConnection object that is associated with the result set at the time when the result set is created for server-side cursors only. It does not apply to client-side cursors. All result sets with client-side cursors will always have the holdability value of `ResultSet.HOLD_CURSORS_OVER_COMMIT`.

For server cursors, when connected to SQL Server 2005 or later, setting the holdability affects only the holdability of new result sets that are yet to be created on that connection. It means that setting holdability has no impact on the holdability of any result sets that were previously created and are already open on that connection.

In the following example, the result set holdability is set while performing a local transaction consisting of two separate statements in the `try` block. The statements are run against the Production.ScrapReason table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database. First, the example switches to manual transaction mode by setting the auto-commit to `false`. Once auto-commit mode is disabled, no SQL Statements will be committed until the application calls the [commit](../../connect/jdbc/reference/commit-method-sqlserverconnection.md) method explicitly. The code in the catch block rolls back the transaction if an exception is thrown.

[!code[JDBC#UsingHoldability1](../../connect/jdbc/codesnippet/Java/using-holdability_1.java)]

## See also

[Performing transactions with the JDBC driver](../../connect/jdbc/performing-transactions-with-the-jdbc-driver.md)
