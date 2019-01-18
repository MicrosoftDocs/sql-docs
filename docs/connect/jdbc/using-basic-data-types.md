---
title: "Using Basic Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "07/19/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: d7044936-5b8c-4def-858c-28a11ef70a97
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Basic Data Types

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] uses the JDBC basic data types to convert the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types to a format that can be understood by the Java programming language, and vice versa. The JDBC driver provides support for the JDBC 4.0 API, which includes the **SQLXML** data type, and National (Unicode) data types, such as **NCHAR**, **NVARCHAR**, **LONGNVARCHAR**, and **NCLOB**.  
  
## Data Type Mappings

The following table lists the default mappings between the basic [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], JDBC, and Java programming language data types:  
  
| SQL Server Types   | JDBC Types (java.sql.Types)                        | Java Language Types          |
| ------------------ | -------------------------------------------------- | ---------------------------- |
| bigint             | BIGINT                                             | long                         |
| binary             | BINARY                                             | byte[]                       |
| bit                | BIT                                                | boolean                      |
| char               | CHAR                                               | String                       |
| date               | DATE                                               | java.sql.Date                |
| datetime           | TIMESTAMP                                          | java.sql.Timestamp           |
| datetime2          | TIMESTAMP                                          | java.sql.Timestamp           |
| datetimeoffset (2) | microsoft.sql.Types.DATETIMEOFFSET                 | microsoft.sql.DateTimeOffset |
| decimal            | DECIMAL                                            | java.math.BigDecimal         |
| float              | DOUBLE                                             | double                       |
| image              | LONGVARBINARY                                      | byte[]                       |
| int                | INTEGER                                            | int                          |
| money              | DECIMAL                                            | java.math.BigDecimal         |
| nchar              | CHAR<br /><br /> NCHAR (Java SE 6.0)               | String                       |
| ntext              | LONGVARCHAR<br /><br /> LONGNVARCHAR (Java SE 6.0) | String                       |
| numeric            | NUMERIC                                            | java.math.BigDecimal         |
| nvarchar           | VARCHAR<br /><br /> NVARCHAR (Java SE 6.0)         | String                       |
| nvarchar(max)      | VARCHAR<br /><br /> NVARCHAR (Java SE 6.0)         | String                       |
| real               | REAL                                               | float                        |
| smalldatetime      | TIMESTAMP                                          | java.sql.Timestamp           |
| smallint           | SMALLINT                                           | short                        |
| smallmoney         | DECIMAL                                            | java.math.BigDecimal         |
| text               | LONGVARCHAR                                        | String                       |
| time               | TIME (1)                                           | java.sql.Time (1)            |
| timestamp          | BINARY                                             | byte[]                       |
| tinyint            | TINYINT                                            | short                        |
| udt                | VARBINARY                                          | byte[]                       |
| uniqueidentifier   | CHAR                                               | String                       |
| varbinary          | VARBINARY                                          | byte[]                       |
| varbinary(max)     | VARBINARY                                          | byte[]                       |
| varchar            | VARCHAR                                            | String                       |
| varchar(max)       | VARCHAR                                            | String                       |
| xml                | LONGVARCHAR<br /><br /> LONGNVARCHAR (Java SE 6.0) | String<br /><br /> SQLXML    |
| sqlvariant         | SQLVARIANT                                         | Object                       |
| geometry           | VARBINARY                                          | byte[]                       |
| geography          | VARBINARY                                          | byte[]                       |
  
(1) To use java.sql.Time with the time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type, you must set the **sendTimeAsDatetime** connection property to false.  
  
(2) You can programmatically access values of **datetimeoffset** with [DateTimeOffset Class](../../connect/jdbc/reference/datetimeoffset-class.md).  
  
The following sections provide examples of how you can use the JDBC Driver and the basic data types. For a more detailed example of how to use the basic data types in a Java application, see [Basic Data Types Sample](../../connect/jdbc/basic-data-types-sample.md).  
  
## Retrieving Data as a String

If you have to retrieve data from a data source that maps to any of the JDBC basic data types for viewing as a string, or if strongly typed data is not required, you can use the [getString](../../connect/jdbc/reference/getstring-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) class, as in the following:  
  
[!code[JDBC#UsingBasicDataTypes1](../../connect/jdbc/codesnippet/Java/using-basic-data-types_1.java)]  
  
## Retrieving Data by Data Type

If you have to retrieve data from a data source, and you know the type of data that is being retrieved, use one of the get\<Type> methods of the SQLServerResultSet class, also known as the *getter methods*. You can use either a column name or a column index with the get\<Type> methods, as in the following:  
  
[!code[JDBC#UsingBasicDataTypes2](../../connect/jdbc/codesnippet/Java/using-basic-data-types_2.java)]  
  
> [!NOTE]  
> The getUnicodeStream and getBigDecimal with scale methods are deprecated and are not supported by the JDBC driver.

## Updating Data by Data Type

If you have to update the value of a field in a data source, use one of the update\<Type> methods of the SQLServerResultSet class. In the following example, the [updateInt](../../connect/jdbc/reference/updateint-method-sqlserverresultset.md) method is used in conjunction with the [updateRow](../../connect/jdbc/reference/updaterow-method-sqlserverresultset.md) method to update the data in the data source:  
  
[!code[JDBC#UsingBasicDataTypes3](../../connect/jdbc/codesnippet/Java/using-basic-data-types_3.java)]  
  
> [!NOTE]  
> The JDBC driver cannot update a SQL Server column with a column name that is more than 127 characters long. If an update to a column whose name is more than 127 characters is attempted, an exception is thrown.  
  
## Updating Data by Parameterized Query

If you have to update data in a data source by using a parameterized query, you can set the data type of the parameters by using one of the set\<Type> methods of the [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) class, also known as the *setter methods*. In the following example, the [prepareStatement](../../connect/jdbc/reference/preparestatement-method-sqlserverconnection.md) method is used to pre-compile the parameterized query, and then the [setString](../../connect/jdbc/reference/setstring-method-sqlserverpreparedstatement.md) method is used to set the string value of the parameter before the [executeUpdate](../../connect/jdbc/reference/executeupdate-method.md) method is called.  
  
[!code[JDBC#UsingBasicDataTypes4](../../connect/jdbc/codesnippet/Java/using-basic-data-types_4.java)]  
  
For more information about parameterized queries, see [Using an SQL Statement with Parameters](../../connect/jdbc/using-an-sql-statement-with-parameters.md).  

## Passing Parameters to a Stored Procedure

If you have to pass typed parameters into a stored procedure, you can set the parameters by index or name by using one of the set\<Type> methods of the [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md) class. In the following example, the [prepareCall](../../connect/jdbc/reference/preparecall-method-sqlserverconnection.md) method is used to set up the call to the stored procedure, and then the [setString](../../connect/jdbc/reference/setstring-method-sqlservercallablestatement.md) method is used to set the parameter for the call before the [executeQuery](../../connect/jdbc/reference/executequery-method-sqlserverstatement.md) method is called.  
  
[!code[JDBC#UsingBasicDataTypes5](../../connect/jdbc/codesnippet/Java/using-basic-data-types_5.java)]  
  
> [!NOTE]  
> In this example, a result set is returned with the results of running the stored procedure.

For more information about using the JDBC driver with stored procedures and input parameters, see [Using a Stored Procedure with Input Parameters](../../connect/jdbc/using-a-stored-procedure-with-input-parameters.md).  

## Retrieving Parameters from a Stored Procedure

If you have to retrieve parameters back from a stored procedure, you must first register an out parameter by name or index by using the [registerOutParameter](../../connect/jdbc/reference/registeroutparameter-method-sqlservercallablestatement.md) method of the SQLServerCallableStatement class, and then assign the returned out parameter to an appropriate variable after you run the call to the stored procedure. In the following example, the prepareCall method is used to set up the call to the stored procedure, the registerOutParameter method is used to set up the out parameter, and then the [setString](../../connect/jdbc/reference/setstring-method-sqlservercallablestatement.md) method is used to set the parameter for the call before executeQuery method is called. The value that is returned by the out parameter of the stored procedure is retrieved by using the [getShort](../../connect/jdbc/reference/getshort-method-sqlservercallablestatement.md) method.  
  
[!code[JDBC#UsingBasicDataTypes6](../../connect/jdbc/codesnippet/Java/using-basic-data-types_6.java)]  
  
> [!NOTE]  
> In addition to the returned out parameter, a result set might also be returned with the results of running the stored procedure.  
  
For more information about how to use the JDBC driver with stored procedures and output parameters, see [Using a Stored Procedure with Output Parameters](../../connect/jdbc/using-a-stored-procedure-with-output-parameters.md).  

## See Also

[Understanding the JDBC Driver Data Types](../../connect/jdbc/understanding-the-jdbc-driver-data-types.md)  
