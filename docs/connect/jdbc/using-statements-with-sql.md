---
title: "Using statements with SQL"
description: "Learn an overview of using different types of SQL statements with the Microsoft JDBC Driver for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Using statements with SQL

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

When you work with data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] and inline SQL statements, there are different classes that you can use. Which class you use depends on the type of SQL statement that you want to run.  
  
If your SQL statement contains no IN parameters, use the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) class, but if it does contain IN parameters, use the [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) class.  
  
> [!NOTE]  
> If you need to use SQL statements that contain both IN and OUT parameters, you must implement them as stored procedures and call them by using the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) class. For more information about using stored procedures, see [Using statements with stored procedures](../../connect/jdbc/using-statements-with-stored-procedures.md).  
  
The following sections describe the different scenarios for working with data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using SQL statements.  

## In This Section  

| Topic                                                                                                                        | Description                                                       |
| ---------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------- |
| [Using an SQL statement with no parameters](../../connect/jdbc/using-an-sql-statement-with-no-parameters.md)                 | Describes how to use SQL statements that contain no parameters.   |
| [Using an SQL statement with parameters](../../connect/jdbc/using-an-sql-statement-with-parameters.md)                       | Describes how to use SQL statements that contain parameters.      |
| [Using an SQL statement to modify database objects](../../connect/jdbc/using-an-sql-statement-to-modify-database-objects.md) | Describes how to use SQL statements to modify database objects.   |
| [Using an SQL statement to modify data](../../connect/jdbc/using-an-sql-statement-to-modify-data.md)                         | Describes how to use SQL statements to modify data in a database. |
  
## See also

[Using Statements with the JDBC driver](../../connect/jdbc/using-statements-with-the-jdbc-driver.md)  
