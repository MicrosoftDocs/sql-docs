---
title: Microsoft Extensibility SDK for Java
description: Learn how you can implement a Java program for SQL Server using the Microsoft Extensibility SDK for Java.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/29/2024
ms.service: sql
ms.subservice: language-extensions
ms.topic: conceptual
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15"
---

# Microsoft Extensibility SDK for Java for SQL Server

[!INCLUDE [sqlserver2019-and-later](../../includes/applies-to-version/sqlserver2019-and-later.md)]

Learn how you can implement a Java program for SQL Server using the Microsoft Extensibility SDK for Java. The SDK is an interface for the Java language extension that is used to exchange data with SQL Server and to execute Java code from SQL Server.

The SDK is installed as part of [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, on both Windows and Linux:

- Default installation path on Windows: `<instance installation home directory>\MSSQL\Binn\mssql-java-lang-extension.jar`
- Default installation path on Linux: `/opt/mssql/lib/mssql-java-lang-extension.jar`

The code is open source and can be found on the [SQL Server Language Extensions GitHub repository](https://github.com/microsoft/sql-server-language-extensions).

## Implementation requirements

The SDK interface defines a set of requirements that need to be fulfilled for SQL Server to communicate with the Java runtime. To use the SDK, you need to follow some implementation rules in your main class. SQL Server can then execute a specific method in the Java class and exchange data using the Java language extension.

For an example of how you can use the SDK, see [Tutorial: Search for a string using regular expressions (regex) in Java](../tutorials/search-for-string-using-regular-expressions-in-java.md).

## SDK Classes

The SDK consists of three classes.

Two abstract classes that define the interface the Java extension uses to exchange data with SQL Server:

- `AbstractSqlServerExtensionExecutor`
- `AbstractSqlServerExtensionDataset`

The third class is a helper class, which contains an implementation of a data set object. It's an optional class you can use, which makes it easier to get started. You can also use your own implementation of such a class instead.

- `PrimitiveDataset`

In the following section, you find descriptions of each class in the SDK. The source code of the SDK classes is available in the [SQL Server Language Extensions GitHub repository](https://github.com/microsoft/sql-server-language-extensions/tree/master/language-extensions/java/sdk).

### Class: AbstractSqlServerExtensionExecutor

The abstract class `AbstractSqlServerExtensionExecutor` contains the interface used to execute Java code by the Java language extension for SQL Server.

Your main Java class needs to inherit from this class. Inheriting from this class means that there are certain methods in the class you need to implement in your own class.

To inherit from this abstract class, you extend with the abstract class name in the class declaration:

```java
public class <MyClass> extends AbstractSqlServerExtensionExecutor {}
```

At a minimum, your main class needs to implement the execute(...) method.

#### Method execute

The execute method is the method that is called from SQL Server via the Java language extension, to invoke Java code from SQL Server. It's a key method where you include the main operations you wish to execute from SQL Server.

To pass method arguments to Java from SQL Server, use the `@param` parameter in `sp_execute_external_script`. The method `execute` takes its arguments that way.

```java
public AbstractSqlServerExtensionDataset execute(AbstractSqlServerExtensionDataset input, LinkedHashMap<String, Object> params)  {}
```

#### Method init

The *init* method is executed after the constructor, and before the *execute* method. Any operations that need to be performed before `execute(...)` can be done in this method.

```java
public void init(String sessionId, int taskId, int numtask) {}
```

### Class: AbstractSqlServerExtensionDataset

The abstract class `AbstractSqlServerExtensionDataset` contains the interface for handling input and output data used by the Java extension.

### Class: PrimitiveDataset

The class `PrimitiveDataset` is an implementation of `AbstractSqlServerExtensionDataset` that stores simple types as primitives arrays.

`PrimitiveDataset` is provided in the SDK as an optional helper class. If you don't use this class, you need to implement your own class that inherits from `AbstractSqlServerExtensionDataset`.

## Related content

- [Tutorial: Search for a string using regular expressions (regex) in Java](../tutorials/search-for-string-using-regular-expressions-in-java.md)
- [How to call the Java runtime in SQL Server Language Extensions](call-java-from-sql.md)
