---
title: Microsoft Extensibility SDK for C#
description: Learn how you can implement a C# program for SQL Server using the Microsoft Extensibility SDK for C#.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/29/2024
ms.service: sql
ms.subservice: language-extensions
ms.topic: conceptual
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15"
---

# Microsoft Extensibility SDK for C# for SQL Server

[!INCLUDE [sqlserver2019-and-later](../../includes/applies-to-version/sqlserver2019-and-later.md)]

Learn how you can implement a C# program for SQL Server using the Microsoft Extensibility SDK for .NET. The SDK is an interface for the .NET language extension that is used to exchange data with SQL Server and to execute C# code from SQL Server.

The code is open source and can be found on the [SQL Server Language Extensions GitHub repository](https://github.com/microsoft/sql-server-language-extensions).

## Implementation requirements

The SDK interface defines a set of requirements that need to be fulfilled for SQL Server to communicate with the .NET runtime. To use the SDK, you need to follow some implementation rules in your main class. SQL Server can then execute a specific method in C# and exchange data using the .NET language extension.

For an example of how you can use the SDK, see [Tutorial: Search for a string using regular expressions (regex) in C#](../tutorials/search-for-string-using-regular-expressions-in-c-sharp.md).

## SDK classes

The SDK consists of several classes.

- The abstract class `AbstractSqlServerExtensionExecutor` defines the interface the .NET extension uses to exchange data with SQL Server.

- Several helper classes implement the `CSharpDataSet` data set object.

In the following section, you find descriptions of each class in the SDK. The source code of the SDK classes is available in the [SQL Server Language Extensions GitHub repository](https://github.com/microsoft/sql-server-language-extensions/tree/main/language-extensions/dotnet-core-CSharp).

### Class: `AbstractSqlServerExtensionExecutor`

The abstract class `AbstractSqlServerExtensionExecutor` contains the interface used to execute C# code by the .NET language extension for SQL Server.

Your main C# class needs to inherit from this class. Inheriting from this class means that there are certain methods in the class you need to implement in your own class.

To inherit from this abstract class, you extend with the abstract class name in the class declaration, where `MyClass` is the name of your class:

```csharp
public class MyClass : AbstractSqlServerExtensionExecutor {}
```

At a minimum, your main class needs to implement the `Execute(...)` method.

#### The `Execute` method

The `Execute` method is the method that is called from SQL Server via the .NET language extension, to invoke C# code from SQL Server. It's a key method where you include the main operations you wish to execute from SQL Server.

To pass method arguments to C# from SQL Server, use the `@param` parameter in `sp_execute_external_script`. The `Execute` method takes its arguments that way.

```csharp
public abstract DataFrame Execute(DataFrame input, Dictionary<string, dynamic> sqlParams);
```

#### The `Init` method

The `Init` method is executed after the constructor, and before the `Execute` method. Any operations that need to be performed before `Execute` can be done in this method.

```csharp
public virtual void Init(string sessionId, int taskId, int numTasks);
```

### Class: `CSharpExtension`

The class `CSharpExtension` implements all language extensions APIs and returns results to native host.

### Class: `CSharpDataSet`

The abstract class `CSharpDataSet` contains the interface for handling input and output data used by the .NET extension.

## Related content

- [Tutorial: Search for a string using regular expressions (regex) in C#](../tutorials/search-for-string-using-regular-expressions-in-c-sharp.md)
- [How to call the .NET runtime in SQL Server Language Extensions](call-c-sharp-from-sql.md)
