---
title: "Using savepoints"
description: "Learn how to use savepoints as part of transaction management in your application when using the Microsoft JDBC Driver for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Using savepoints

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Savepoints offer a mechanism to roll back portions of transactions. Within [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can create a savepoint by using the SAVE TRANSACTION savepoint_name statement. Later, you run a ROLLBACK TRANSACTION savepoint_name statement to roll back to the savepoint instead of rolling back to the start of the transaction.

Savepoints are useful in situations where errors are unlikely to occur. The use of a savepoint to roll back part of a transaction in the case of an infrequent error can be more efficient than having each transaction test to see if an update is valid before making the update. Updates and rollbacks are expensive operations, so savepoints are effective only if the probability of encountering the error is low and the cost of checking the validity of an update beforehand is relatively high.

The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] supports the use of savepoints through the [setSavepoint](../../connect/jdbc/reference/setsavepoint-method-sqlserverconnection.md) method of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class. By using the setSavepoint method, you can create a named or unnamed savepoint within the current transaction, and the method will return a [SQLServerSavepoint](../../connect/jdbc/reference/sqlserversavepoint-class.md) object. Multiple savepoints can be created within a transaction. To roll back a transaction to a given savepoint, you can pass the SQLServerSavepoint object to the [rollback (java.sql.Savepoint)](../../connect/jdbc/reference/rollback-method-java-sql-savepoint.md) method.

In the following example, a savepoint is used while performing a local transaction consisting of two separate statements in the `try` block. The statements are run against the Production.ScrapReason table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database, and a savepoint is used to roll back the second statement. This results in only the first statement being committed to the database.

[!code[JDBC#UsingSavepoints1](../../connect/jdbc/codesnippet/Java/using-savepoints_1.java)]

## See also

[Performing transactions with the JDBC driver](../../connect/jdbc/performing-transactions-with-the-jdbc-driver.md)
