---
title: Using Sql_variant data type
description: Learn about the sql_variant data type in the JDBC driver and how it can be used to support table-valued parameters (TVP) and bulk copy.
author: David-Engel
ms.author: v-davidengel
ms.date: 03/26/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Using Sql_variant data type

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Starting with version 6.3.0, the JDBC driver supports the sql_variant datatype. Sql_variant is also supported when using features such as Table-Valued Parameters and BulkCopy, with some limitations. Not all data types can be stored in the sql_variant data type. For a list of supported data types with sql_variant, see [sql_variant (Transact-SQL)](../../t-sql/data-types/sql-variant-transact-sql.md).

## Populating and retrieving a table

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

## Using stored procedures with sql_variant

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

## Limitations of sql_variant

- When using TVP to populate a table with a `datetime`/`smalldatetime`/`date` value stored in a sql_variant, calling `getDateTime()`/`getSmallDateTime()`/`getDate()` on a ResultSet doesn't work and throws the following exception:

    `Java.lang.String cannot be cast to java.sql.Timestamp`

    Workaround: use `getString()` or `getObject()` instead.

- Using TVP to populate a table and send a null value in a sql_variant isn't supported. Trying to do that results in an exception:

    `Inserting null value with column type sql_variant in TVP is not supported.`

## See also

[Understanding the JDBC driver data types](understanding-the-jdbc-driver-data-types.md)
