---
title: "Using statements with stored procedures"
description: "Learn how to execute stored procedures using the Microsoft JDBC Driver for SQL Server and how to use input and output parameters to pass data to and from them."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Using statements with stored procedures

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

A stored procedure is a database procedure, similar to a procedure in other programming languages, which is contained within the database itself. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], stored procedures can be created by using [!INCLUDE[tsql](../../includes/tsql-md.md)], or by using the common language runtime (CLR) and one of the Visual Studio programming languages such as Visual Basic or C#. Generally, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedures can do the following:  
  
- Accept input parameters and return multiple values in the form of output parameters to the calling procedure or batch.  
  
- Contain programming statements that perform operations in the database, including calling other procedures.  
  
- Return a status value to a calling procedure or batch to indicate success or failure (and the reason for failure).  
  
> [!NOTE]  
> For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedures, see "Understanding Stored Procedures" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
To work with data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using a stored procedure, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md), [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md), and [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) classes. Which class you use depends on whether IN (input) or OUT (output) parameters are required by the stored procedure. If the stored procedure requires no IN or OUT parameters, you can use the SQLServerStatement class; if the stored procedure will be called multiple times, or requires only IN parameters, you can use the SQLServerPreparedStatement class. If the stored procedure requires both IN and OUT parameters, you should use the SQLServerCallableStatement class. It is only when the stored procedure requires OUT parameters that you will need the overhead of using the SQLServerCallableStatement class.  
  
> [!NOTE]  
> Stored procedures can also return update counts and multiple result sets. For more information, see [Using a stored procedure with an update count](../../connect/jdbc/using-a-stored-procedure-with-an-update-count.md) and [Using multiple result sets](../../connect/jdbc/using-multiple-result-sets.md).  
  
When you use the JDBC driver to call a stored procedure with parameters, you must use the `call` SQL escape sequence together with the [prepareCall](../../connect/jdbc/reference/preparecall-method-sqlserverconnection.md) method of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class. The complete syntax for the `call` escape sequence is as follows:  
  
 `{[?=]call procedure-name[([parameter][,[parameter]]...)]}`  
  
> [!NOTE]  
> For more information about the `call` and other SQL escape sequences, see [Using SQL escape sequences](../../connect/jdbc/using-sql-escape-sequences.md).  
  
The topics in this section describe the ways that you can call [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedures by using the JDBC driver and the `call` SQL escape sequence.  
  
## In this section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Using a stored procedure with no parameters](../../connect/jdbc/using-a-stored-procedure-with-no-parameters.md)|Describes how to use the JDBC driver to run stored procedures that contain no input or output parameters.|  
|[Using a stored procedure with input parameters](../../connect/jdbc/using-a-stored-procedure-with-input-parameters.md)|Describes how to use the JDBC driver to run stored procedures that contain input parameters.|  
|[Using a stored procedure with output parameters](../../connect/jdbc/using-a-stored-procedure-with-output-parameters.md)|Describes how to use the JDBC driver to run stored procedures that contain output parameters.|  
|[Using a stored procedure with a return status](../../connect/jdbc/using-a-stored-procedure-with-a-return-status.md)|Describes how to use the JDBC driver to run stored procedures that contain return status values.|  
|[Using a stored procedure with an update count](../../connect/jdbc/using-a-stored-procedure-with-an-update-count.md)|Describes how to use the JDBC driver to run stored procedures that return update counts.|  
  
## See also

[Using statements with the JDBC driver](../../connect/jdbc/using-statements-with-the-jdbc-driver.md)  
