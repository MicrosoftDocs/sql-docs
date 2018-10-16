---
title: "Using a Stored Procedure with Output Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "07/11/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 1c006f27-7e99-43d5-974c-7b782659290c
author: MightyPen
ms.author: genemi
manager: craigg
---

# Using a Stored Procedure with Output Parameters

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure that you can call is one that returns one or more OUT parameters, which are parameters that the stored procedure uses to return data back to the calling application. The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) class, which you can use to call this kind of stored procedure and process the data that it returns.

When you call this kind of stored procedure by using the JDBC driver, you must use the `call` SQL escape sequence together with the [prepareCall](../../connect/jdbc/reference/preparecall-method-sqlserverconnection.md) method of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class. The syntax for the `call` escape sequence with OUT parameters is the following:

`{call procedure-name[([parameter][,[parameter]]...)]}`

> [!NOTE]  
> For more information about the SQL escape sequences, see [Using SQL Escape Sequences](../../connect/jdbc/using-sql-escape-sequences.md).

When you construct the `call` escape sequence, specify the OUT parameters by using the ? (question mark) character. This character acts as a placeholder for the parameter values that will be returned from the stored procedure. To specify a value for an OUT parameter, you must specify the data type of each parameter by using the [registerOutParameter](../../connect/jdbc/reference/registeroutparameter-method-sqlservercallablestatement.md) method of the SQLServerCallableStatement class before you run the stored procedure.

The value that you specify for the OUT parameter in the registerOutParameter method must be one of the JDBC data types contained in java.sql.Types, which in turn maps to one of the native [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types. For more information about the JDBC and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types, see [Understanding the JDBC Driver Data Types](../../connect/jdbc/understanding-the-jdbc-driver-data-types.md).

When you pass a value to the registerOutParameter method for an OUT parameter, you must specify not only the data type to be used for the parameter, but also the parameter's ordinal placement or the parameter's name in the stored procedure. For example, if your stored procedure contains a single OUT parameter, its ordinal value will be 1; if the stored procedure contains two parameters, the first ordinal value will be 1, and the second ordinal value will be 2.

> [!NOTE]  
> The JDBC driver does not support the use of CURSOR, SQLVARIANT, TABLE, and TIMESTAMP [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types as OUT parameters.

As an example, create the following stored procedure in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database:

```sql
CREATE PROCEDURE GetImmediateManager  
   @employeeID INT,  
   @managerID INT OUTPUT  
AS  
BEGIN  
   SELECT @managerID = ManagerID
   FROM HumanResources.Employee
   WHERE EmployeeID = @employeeID  
END
```

This stored procedure returns a single OUT parameter (managerID), which is an integer, based on the specified IN parameter (employeeID), which is also an integer. The value that is returned in the OUT parameter is the ManagerID based on the EmployeeID that is contained in the HumanResources.Employee table.

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database is passed in to the function, and the [execute](../../connect/jdbc/reference/execute-method-sqlserverstatement.md) method is used to call the GetImmediateManager stored procedure:

```java
public static void executeStoredProcedure(Connection con) throws SQLException {  
    try(CallableStatement cstmt = con.prepareCall("{call dbo.GetImmediateManager(?, ?)}");) {  
        cstmt.setInt(1, 5);  
        cstmt.registerOutParameter(2, java.sql.Types.INTEGER);  
        cstmt.execute();  
        System.out.println("MANAGER ID: " + cstmt.getInt(2));  
    }  
}
```

This example uses the ordinal positions to identify the parameters. Alternatively, you can identify a parameter by using its name instead of its ordinal position. The following code example modifies the previous example to demonstrate how to use named parameters in a Java application. Note that parameter names correspond to the parameter names in the stored procedure's definition:

```java
public static void executeStoredProcedure(Connection con) throws SQLException {  
    try(CallableStatement cstmt = con.prepareCall("{call dbo.GetImmediateManager(?, ?)}"); ) {  
        cstmt.setInt("employeeID", 5);  
        cstmt.registerOutParameter("managerID", java.sql.Types.INTEGER);  
        cstmt.execute();  
        System.out.println("MANAGER ID: " + cstmt.getInt("managerID"));  
    }  
}
```

> [!NOTE]  
> These examples use the execute method of the SQLServerCallableStatement class to run the stored procedure. This is used because the stored procedure did not also return a result set. If it did, the [executeQuery](../../connect/jdbc/reference/executequery-method-sqlserverstatement.md) method would be used.

Stored procedures can return update counts and multiple result sets. The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] follows the JDBC 3.0 specification, which states that multiple result sets and update counts should be retrieved before the OUT parameters are retrieved. That is, the application should retrieve all of the ResultSet objects and update counts before retrieving the OUT parameters by using the CallableStatement.getter methods. Otherwise, the ResultSet objects and update counts that haven't already been retrieved will be lost when the OUT parameters are retrieved. For more information about update counts and multiple result sets, see [Using a Stored Procedure with an Update Count](../../connect/jdbc/using-a-stored-procedure-with-an-update-count.md) and [Using Multiple Result Sets](../../connect/jdbc/using-multiple-result-sets.md).

## See Also

[Using Statements with Stored Procedures](../../connect/jdbc/using-statements-with-stored-procedures.md)
