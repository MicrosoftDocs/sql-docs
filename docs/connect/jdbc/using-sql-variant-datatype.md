---
title: "Using Sql_variant Data Type | Microsoft Docs"
ms.custom: ""
ms.date: "01/28/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Sql_variant Data Type

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

As of version 6.3.0, the JDBC driver supports the sql_variant datatype. Sql_variant is also supported when using features such as Table-Valued Parameters and BulkCopy with some limitations mentioned later on this page. Not all data types can be stored in the sql_variant data type. For a list of supported data types with sql_variant, check the SQL Server [Docs](https://docs.microsoft.com/sql/t-sql/data-types/sql-variant-transact-sql).

##  Populating and retrieving a table:
Assuming one has a table with a sql_variant column as:

```sql
CREATE TABLE sampleTable (col1 sql_variant)  
```

A sample script to insert values using statement:

```java
try (Statement stmt = connection.createStatement()){
    stmt.execute("insert into sampleTable values (1)");
}
```

Inserting value using prepared statement:

```java
try (PreparedStatement preparedStatement = con.prepareStatement("insert into sampleTable values (?)")) {
    preparedStatement.setObject(1, 1);  
    preparedStatement.execute();
}
```      

If the underlying type of the data being passed is known, the respective setter can be used. For instance, `preparedStatement.setInt()` can be used when inserting an integer value.

```java
try (PreparedStatement preparedStatement = con.prepareStatement("insert into table values (?)")) {
    preparedStatement.setInt (1, 1);
    preparedStatement.execute();
}
```

For reading values from the table, the respective getters can be used. For example, `getInt()` or `getString()` methods can be used if the values coming from the server are known:    

```java
try (SQLServerResultSet resultSet = (SQLServerResultSet) stmt.executeQuery("select * from sampleTable ")) {
    resultSet.next();          
    resultSet.getInt(1); //or rs.getString(1); or rs.getObject(1);
}
```

## Using stored procedures with sql_variant:   
Having a stored procedure such as:     

```java
String sql = "CREATE PROCEDURE " + inputProc + " @p0 sql_variant OUTPUT AS SELECT TOP 1 @p0=col1 FROM sampleTable ";
``` 
    
Output parameters must be registered:

```java
try (CallableStatement callableStatement = con.prepareCall(" {call " + inputProc + " (?) }")) {
    callableStatement.registerOutParameter(1, microsoft.sql.Types.SQL_VARIANT);      
    callableStatement.execute();
}
```

## Limitations of sql_variant:
- When using TVP to populate a table with a `datetime`/`smalldatetime`/`date` value stored in a sql_variant, calling `getDateTime()`/`getSmallDateTime()`/`getDate()` on a ResultSet does not work and throws the following exception:
    
    `Java.lang.String cannot be cast to java.sql.Timestamp`
   
    Workaround: use `getString()` or `getObject()` instead. 
    
- Using TVP to populate a table and sending a null value in a sql_variant is not supported and throws an exception:
    
    `Inserting null value with column type sql_variant in TVP is not supported.`

## See Also

[Understanding the JDBC Driver Data Types](../../connect/jdbc/understanding-the-jdbc-driver-data-types.md)  
