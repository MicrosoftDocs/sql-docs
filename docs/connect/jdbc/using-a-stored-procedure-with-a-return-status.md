---
title: "Using a stored procedure with a return status"
description: "Learn how to call a stored procedure that returns a result and read the result using the Microsoft JDBC Driver for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Using a stored procedure with a return status

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure that you can call is one that returns a status or a result parameter. This status is typically used to indicate the success or failure of the stored procedure. The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides the [SQLServerCallableStatement](reference/sqlservercallablestatement-class.md) class, which you can use to call this kind of stored procedure and to process the data that it returns.

When you call this kind of stored procedure by using the JDBC driver, you have to use the `call` SQL escape sequence in conjunction with the [prepareCall](reference/preparecall-method-sqlserverconnection.md) method of the [SQLServerConnection](reference/sqlserverconnection-class.md) class. The syntax for the `call` escape sequence with a return status parameter is the following:

`{[?=]call procedure-name[([parameter][,[parameter]]...)]}`

> [!NOTE]  
> For more information about the SQL escape sequences, see [Using SQL escape sequences](using-sql-escape-sequences.md).

When you construct the `call` escape sequence, specify the return status parameter by using the `?` (question mark) character. This character acts as a placeholder for the parameter value that will be returned from the stored procedure. To specify a value for a return status parameter, you must specify the data type of the parameter by using the [registerOutParameter](reference/registeroutparameter-method-sqlservercallablestatement.md) method of the SQLServerCallableStatement class, before executing the stored procedure.

> [!NOTE]  
> When using the JDBC driver with a SQL Server database, the value that you specify for the return status parameter in the registerOutParameter method will always be an integer, which you can specify by using the java.sql.Types.INTEGER data type.

In addition, when you pass a value to the registerOutParameter method for a return status parameter, you must specify not only the data type to be used for the parameter, but also the parameter's ordinal placement in the stored procedure call. In the case of the return status parameter, its ordinal position will always be 1 because it is always the first parameter in the call to the stored procedure. Although the SQLServerCallableStatement class provides support for using the parameter's name to indicate the specific parameter, you can use only a parameter's ordinal position number for return status parameters.

As an example, create the following stored procedure in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database:

```sql
CREATE PROCEDURE CheckContactCity  
   (@cityName CHAR(50))  
AS  
BEGIN  
   IF ((SELECT COUNT(*)  
   FROM Person.Address  
   WHERE City = @cityName) > 1)  
   RETURN 1  
ELSE  
   RETURN 0  
END  
```

This stored procedure returns a status value of 1 or 0, depending on whether the city that is specified in the cityName parameter is found in the Person.Address table.

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database is passed in to the function, and the [execute](reference/execute-method-sqlserverstatement.md) method is used to call the CheckContactCity stored procedure:

[!code[JDBC#UsingSprocWithReturnStatus1](codesnippet/Java/using-a-stored-procedure_1_1.java)]

## See also

[Using statements with stored procedures](using-statements-with-stored-procedures.md)
