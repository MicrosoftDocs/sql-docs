---
title: "Using a Stored Procedure with a Return Status | Microsoft Docs"
ms.custom: ""
ms.date: "07/11/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 4b126e95-8458-41d6-af37-fc6662859f19
author: MightyPen
ms.author: genemi
manager: craigg
---

# Using a Stored Procedure with a Return Status

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure that you can call is one that returns a status or a result parameter. This is typically used to indicate the success or failure of the stored procedure. The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) class, which you can use to call this kind of stored procedure and to process the data that it returns.

When you call this kind of stored procedure by using the JDBC driver, you have to use the `call` SQL escape sequence in conjunction with the [prepareCall](../../connect/jdbc/reference/preparecall-method-sqlserverconnection.md) method of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class. The syntax for the `call` escape sequence with a return status parameter is the following:

`{[?=]call procedure-name[([parameter][,[parameter]]...)]}`

> [!NOTE]  
> For more information about the SQL escape sequences, see [Using SQL Escape Sequences](../../connect/jdbc/using-sql-escape-sequences.md).

When you construct the `call` escape sequence, specify the return status parameter by using the ? (question mark) character. This character acts as a placeholder for the parameter value that will be returned from the stored procedure. To specify a value for a return status parameter, you must specify the data type of the parameter by using the [registerOutParameter](../../connect/jdbc/reference/registeroutparameter-method-sqlservercallablestatement.md) method of the SQLServerCallableStatement class, before executing the stored procedure.

> [!NOTE]  
> When using the JDBC driver with a SQL Server database, the value that you specify for the return status parameter in the registerOutParameter method will always be an integer, which you can specify by using the java.sql.Types.INTEGER data type.

In addition, when you pass a value to the registerOutParameter method for a return status parameter, you must specify not only the data type to be used for the parameter, but also the parameter's ordinal placement in the stored procedure call. In the case of the return status parameter, its ordinal position will always be 1 because it is always the first parameter in the call to the stored procedure. Although the SQLServerCallableStatement class provides support for using the parameter's name to indicate the specific parameter, you can use only a parameter's ordinal position number for return status parameters.

As an example, create the following stored procedure in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database:

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

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database is passed in to the function, and the [execute](../../connect/jdbc/reference/execute-method-sqlserverstatement.md) method is used to call the CheckContactCity stored procedure:

[!code[JDBC#UsingSprocWithReturnStatus1](../../connect/jdbc/codesnippet/Java/using-a-stored-procedure_1_1.java)]

## See Also

[Using Statements with Stored Procedures](../../connect/jdbc/using-statements-with-stored-procedures.md)
