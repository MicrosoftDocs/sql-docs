---
title: Call Java runtime
titleSuffix: SQL Server Language Extensions
description: Learn how to call Java classes from a SQL Server stored procedures using SQL Server Language Extensions.
author: rothja
ms.author: jroth 
ms.date: 06/25/2020
ms.topic: how-to
ms.service: sql
ms.subservice: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
---
# How to call the Java runtime in SQL Server Language Extensions
[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019.md)]

[SQL Server Language Extensions](../language-extensions-overview.md) uses the [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) system stored procedure as the interface to call the Java runtime. 

This how-to article explains implementation details for Java classes and methods that execute on SQL Server.

## Where to place Java classes

There are two methods for calling Java classes in SQL Server:

1. Place **.class** or **.jar** files in your [Java classpath](#classpath). 

2. Upload compiled classes in a **.jar** file and other dependencies into the database using the [external library](#external-library) DDL. 

> [!NOTE]
> As a general recommendation, use **.jar** files and not individual **.class** files. This is common practice in Java and will make the overall experience easier. See also, [How to create a jar file from class files](create-a-java-jar-file-from-class-files.md).

<a name="classpath"></a>

## Use Classpath

### Basic principles

The following are some basic principles when executing Java on SQL Server.

* Compiled custom Java classes must exist in **.class** files or **.jar** files in your Java classpath. The [CLASSPATH parameter](#set-classpath) provides the path to the compiled Java files. 

* The Java method you are calling must be provided in the **script** parameter on the stored procedure.

* If the class belongs to a package, the **packageName** must be provided.

* **params** is used to pass parameters to a Java class. Calling a method that requires arguments is not supported. Therefore, parameters the only way to pass argument values to your method. 

> [!NOTE]
> This note restates supported and unsupported operations specific to Java in SQL Server 2019 Release Candidate 1.
> * On the stored procedure, input parameters are supported. Output parameters are not.

### Call Java class

The [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) system stored procedure is the interface used to call the Java runtime. The following example shows an `sp_execute_external_script` using the Java extension, and parameters for specifying path, script, and your custom code.

> [!NOTE]
> Note that you don't need to define which method to call. By default, a method called **execute** is called. This means that you need to follow the [Extensibility SDK for Java in SQL Server](extensibility-sdk-java-sql-server.md) and implement an execute method in your Java class.

```sql
DECLARE @param1 int
SET @param1 = 3

EXEC sp_execute_external_script
  @language = N'Java'
, @script = N'<packageName>.<ClassName>'
, @input_data_1 = N'<Input Query>'
, @param1 = @param1
```

<a name="set-classpath"></a>

### Set CLASSPATH

Once you have compiled your Java class or classes and created a jar file in your Java classpath, you have two options for providing the classpath to the SQL Server Java extension:

1. Use external libraries

    The easiest option is to make SQL Server automatically find your classes by creating external libraries and pointing the library to a jar. [Use external libraries for Java](#external-library)

2. Register a system environment variable

    You can create a system environment variable and provide the paths to your jar file that contains the classes. Create a system environment variable called **CLASSPATH**.

<a name="external-library"></a>

## Use external library

In SQL Server 2019 Release Candidate 1, you can use external libraries for the Java language on Windows and Linux. You can compile your classes into a .jar file and upload the .jar file and other dependencies into the database using the [CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md) DDL.

Example of how to upload a .jar file with external library:

```sql 
CREATE EXTERNAL LIBRARY myJar
FROM (CONTENT = '<local path to .jar file>') 
WITH (LANGUAGE = 'Java'); 
GO
```

By creating an external library, SQL Server will automatically have access to the Java classes and you do not need to set any special permissions to the classpath.

Example of calling a method in a class from a package uploaded as an external library:

```sql
EXEC sp_execute_external_script
  @language = N'Java'
, @script = N'MyPackage.MyCLass'
, @input_data_1 = N'SELECT * FROM MYTABLE'
with result sets ((column1 int))
```

For more information, see [CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md).

## Loopback connection to SQL Server

Use a loopback connection to connect back to SQL Server over JDBC to read or write data from Java executed from `sp_execute_external_script`. You can use this when using the **InputDataSet** and **OutputDataSet** arguments of `sp_execute_external_script` are not possible.
To make a loopback connection in Windows use the following example:

```
jdbc:sqlserver://localhost:1433;databaseName=Adventureworks;integratedSecurity=true;
``` 

To make a loopback connection in Linux the JDBC driver requires three connection properties defined in the following Certificate:

[Client-Certificate-Authenication](https://github.com/microsoft/mssql-jdbc/wiki/Client-Certificate-Authentication-for-Loopback-Scenarios)


## Next steps

+ [Tutorial: Search for a string using regular expressions in Java](../tutorials/search-for-string-using-regular-expressions-in-java.md)