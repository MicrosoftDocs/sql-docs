---
title: "Using a stored procedure with an update count"
description: "Learn how to use a stored procedure and return a count of the number of rows affected (also called the update count) using the JDBC Driver for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Using a stored procedure with an update count

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

To modify data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database by using a stored procedure, the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) class. By using the SQLServerCallableStatement class, you can call stored procedures that modify data that is in the database and return a count of the number of rows affected, also referred to as the update count.

After you have set up the call to the stored procedure by using the SQLServerCallableStatement class, you can then call the stored procedure by using either the [execute](../../connect/jdbc/reference/execute-method-sqlserverstatement.md) or the [executeUpdate](../../connect/jdbc/reference/executeupdate-method-sqlserverstatement.md) method. The executeUpdate method will return an **int** value that contains the number of rows affected by the stored procedure, but the execute method doesn't. If you use the execute method and want to get the count of the number of rows affected, you can call the [getUpdateCount](../../connect/jdbc/reference/getupdatecount-method-sqlserverstatement.md) method after you run the stored procedure.

> [!NOTE]  
> If you want the JDBC driver to return all update counts, including update counts returned by any triggers that may have fired, set the lastUpdateCount connection string property to "false". For more information about the lastUpdateCount property, see [Setting the connection properties](../../connect/jdbc/setting-the-connection-properties.md).

As an example, create the following table and stored procedure, and also insert sample data in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database:

```sql
CREATE TABLE TestTable
   (Col1 int IDENTITY,
    Col2 varchar(50),
    Col3 int);  

CREATE PROCEDURE UpdateTestTable  
   @Col2 varchar(50),  
   @Col3 int  
AS  
BEGIN  
   UPDATE TestTable  
   SET Col2 = @Col2, Col3 = @Col3  
END;  
INSERT INTO dbo.TestTable (Col2, Col3) VALUES ('b', 10);  
```

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database is passed in to the function, the execute method is used to call the UpdateTestTable stored procedure, and then the getUpdateCount method is used to return a count of the rows that are affected by the stored procedure.

[!code[JDBC#UsingSprocWithUpdateCount1](../../connect/jdbc/codesnippet/Java/using-a-stored-procedure_0_1.java)]

## See also

[Using statements with stored procedures](../../connect/jdbc/using-statements-with-stored-procedures.md)
