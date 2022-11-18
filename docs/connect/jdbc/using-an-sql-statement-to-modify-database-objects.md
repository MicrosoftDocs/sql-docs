---
title: "Using an SQL statement to modify database objects"
description: "Using an SQL statement to modify database objects"
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Using an SQL statement to modify database objects

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

To modify [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database objects by using an SQL statement, you can use the [executeUpdate](../../connect/jdbc/reference/executeupdate-method-sqlserverstatement.md) method of the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) class. The executeUpdate method will pass the SQL statement to the database for processing, and then return a value of 0 because no rows were affected.

To do this, you must first create a SQLServerStatement object by using the [createStatement](../../connect/jdbc/reference/createstatement-method-sqlserverconnection.md) method of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class.

> [!NOTE]  
> SQL statements that modify objects within a database are called Data Definition Language (DDL) statements. These include statements such as `CREATE TABLE`, `DROP TABLE`, `CREATE INDEX`, and `DROP INDEX`. For more information about the types of DDL statements that are supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database is passed in to the function, an SQL statement is constructed that will create the simple TestTable in the database, and then the statement is run and the return value is displayed.

[!code[JDBC#UsingSQLToModifyDBObjects1](../../connect/jdbc/codesnippet/Java/using-an-sql-statement-t_0_1.java)]

## See also

[Using statements with SQL](../../connect/jdbc/using-statements-with-sql.md)
