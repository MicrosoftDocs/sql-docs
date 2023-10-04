---
title: "Using a stored procedure with no parameters"
description: "The JDBC driver provides the SQLServerStatement class, which can be used to call stored procedures that take no parameters and process the data that it returns."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Using a stored procedure with no parameters

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The simplest kind of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedure that you can call is one that contains no parameters and returns a single result set. The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides the [SQLServerStatement](reference/sqlserverstatement-class.md) class, which you can use to call this kind of stored procedure and process the data that it returns.

When you use the JDBC driver to call a stored procedure without parameters, you must use the `call` SQL escape sequence. The syntax for the `call` escape sequence with no parameters is as follows:

`{call procedure-name}`

> [!NOTE]  
> For more information about the SQL escape sequences, see [Using SQL escape sequences](using-sql-escape-sequences.md).

As an example, create the following stored procedure in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database:

```sql
CREATE PROCEDURE GetContactFormalNames
AS  
BEGIN  
   SELECT TOP 10 Title + ' ' + FirstName + ' ' + LastName AS FormalName
   FROM Person.Contact  
END  
```

This stored procedure returns a single result set that contains one column of data, which is a combination of the title, first name, and last name of the top 10 contacts that are in the Person.Contact table.

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database is passed in to the function, and the [executeQuery](reference/executequery-method-sqlserverstatement.md) method is used to call the GetContactFormalNames stored procedure.

```java
public static void executeSprocNoParams(Connection con) throws SQLException {  
    try(Statement stmt = con.createStatement();) {  

        ResultSet rs = stmt.executeQuery("{call dbo.GetContactFormalNames}");  
        while (rs.next()) {  
            System.out.println(rs.getString("FormalName"));  
        }  
    }  
}
```

## See also

[Using statements with stored procedures](using-statements-with-stored-procedures.md)
