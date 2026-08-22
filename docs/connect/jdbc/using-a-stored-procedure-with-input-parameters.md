---
title: "Using a stored procedure with input parameters"
description: "Learn how to use input parameters with a stored procedure with the JDBC Driver for SQL Server."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 12/18/2025
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
---

# Using a stored procedure with input parameters

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure that you can call is one that contains one or more IN parameters, which are parameters that can be used to pass data into the stored procedure. The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides the [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) class, which you can use to call this kind of stored procedure and to process the data that it returns.

When you use the JDBC driver to call a stored procedure with IN parameters, you must use the `call` SQL escape sequence together with the [prepareCall](../../connect/jdbc/reference/preparecall-method-sqlserverconnection.md) method of the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class. The syntax for the `call` escape sequence with IN parameters is as follows:

`{call procedure-name[([parameter][,[parameter]]...)]}`

> [!NOTE]  
> For more information about the SQL escape sequences, see [Using SQL escape sequences](../../connect/jdbc/using-sql-escape-sequences.md).

When you construct the `call` escape sequence, specify the IN parameters by using the `?` (question mark) character. This character acts as a placeholder for the parameter values passed into the stored procedure. To specify a value for a parameter, you can use one of the setter methods of the SQLServerPreparedStatement class. The data type of the IN parameter determines the setter method that you can use.

When you pass a value to the setter method, you must specify not only the actual value that is used in the parameter, but also the ordinal placement of the parameter in the stored procedure. For example, if your stored procedure contains a single IN parameter, its ordinal value is 1. If the stored procedure contains two parameters, the first ordinal value is 1, and the second ordinal value is 2.

As an example of how to call a stored procedure that contains an IN parameter, use the uspGetEmployeeManagers stored procedure in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database. This stored procedure accepts a single input parameter named EmployeeID, which is an integer value, and it returns a recursive list of employees and their managers based on the specified EmployeeID. The Java code for calling this stored procedure is as follows:

```java
public static void executeSprocInParams(Connection con) throws SQLException {  
    try(PreparedStatement pstmt = con.prepareStatement("{call dbo.uspGetEmployeeManagers(?)}"); ) {  

        pstmt.setInt(1, 50);  
        ResultSet rs = pstmt.executeQuery();  

        while (rs.next()) {  
            System.out.println("EMPLOYEE:");  
            System.out.println(rs.getString("LastName") + ", " + rs.getString("FirstName"));  
            System.out.println("MANAGER:");  
            System.out.println(rs.getString("ManagerLastName") + ", " + rs.getString("ManagerFirstName"));  
            System.out.println();  
        }  
    }
}
```

## Related content

- [Using statements with stored procedures](using-statements-with-stored-procedures.md)
- [Prepared statement parameter performance for the JDBC driver](prepared-statement-parameter-performance.md)
