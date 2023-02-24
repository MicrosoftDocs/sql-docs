---
title: "Understanding the JDBC driver data types"
description: "Learn about JDBC data types and how the Microsoft JDBC Driver for SQL Server converts those types to database types."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Understanding the JDBC driver data types

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

[!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] supports the use of JDBC basic and advanced data types within a Java application that uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as its database.  
  
The JDBC type system mediates the conversion between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types and Java language types and objects. The JDBC types are modeled on the SQL-92 and SQL-99 types. The JDBC driver adheres to the JDBC specification and is designed to provide the right balance between predictability and flexibility.  
  
The topics in this section describe how to use the basic and advanced data types, and how data types can be converted into other data types.  
  
## In this section  
  
| Topic                                                                                                                                            | Description                                                                                                                                                                                                                                                          |
| ------------------------------------------------------------------------------------------------------------------------------------------------ | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Using basic data types](../../connect/jdbc/using-basic-data-types.md)                                                                           | Describes the JDBC basic data types. Includes examples of how to work with the data types by using result sets, parameterized queries, and stored procedures.                                                                                                        |
| [Configuring how java.sql.Time values are sent to the server](../../connect/jdbc/configuring-how-java-sql-time-values-are-sent-to-the-server.md) | Describes how the JDBC Driver generates dates.                                                                                                                                                                                                                       |
| [Using advanced data types](../../connect/jdbc/using-advanced-data-types.md)                                                                     | Describes the JDBC advanced data types.                                                                                                                                                                                                                              |
| [Understanding data type differences](../../connect/jdbc/understanding-data-type-differences.md)                                                 | Describes differences between the various JDBC driver data types.                                                                                                                                                                                                    |
| [Understanding data type conversions](../../connect/jdbc/understanding-data-type-conversions.md)                                                 | Describes how data type conversion is handled when using getter and setter methods.                                                                                                                                                                                  |
| [National character set support](../../connect/jdbc/national-character-set-support.md)                                                           | Describes the support for the national character set types.                                                                                                                                                                                                          |
| [Supporting XML data](../../connect/jdbc/supporting-xml-data.md)                                                                                 | Describes the SQLXML interface. Also describes how to read and write an XML data from and to the relational database with the **SQLXML** Java data type.                                                                                                             |
| [Wrappers and interfaces](../../connect/jdbc/wrappers-and-interfaces.md)                                                                         | Discusses the interfaces that have the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] specific methods and constants that allow an application server to create a proxy of the class, Also discusses supports for the `java.sql.Wrapper` interface. |
  
## See also

[Overview of the JDBC driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)  
