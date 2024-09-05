---
title: Call .NET runtime
titleSuffix: SQL Server Language Extensions
description: Learn how to call C# code from a SQL Server stored procedures using SQL Server Language Extensions.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/29/2024
ms.service: sql
ms.subservice: language-extensions
ms.topic: how-to
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15"
---
# How to call the .NET runtime in SQL Server Language Extensions

[!INCLUDE [sqlserver2019-and-later](../../includes/applies-to-version/sqlserver2019-and-later.md)]

The [SQL Server Language Extensions](../language-extensions-overview.md) feature uses the [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) system stored procedure as the interface to call the .NET runtime.

This how-to article explains implementation details for C# code that executes on SQL Server.

## Where to place C# classes

You call C# code in SQL Server by uploading compiled .NET libraries (DLLs) and other dependencies into the database using the [external library](#external-library) DDL. For more information, see [Create a .NET DLL from a C# project](create-a-c-sharp-library.md).

### Basic principles

The following are some basic principles when executing C# on SQL Server.

- Compiled custom .NET classes must exist in DLL files.

- The C# method you're calling must be provided in the `script` parameter on the stored procedure.

- If the class belongs to a package, the `packageName` must be provided.

- `params` is used to pass parameters to a C# class. Calling a method that requires arguments isn't supported. Therefore, parameters are the only way to pass argument values to your method.

> [!NOTE]  
> This note restates supported and unsupported operations specific to C# in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions. On the stored procedure, input parameters are supported, while output parameters aren't supported.

### Call C# code

The [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) system stored procedure is the interface used to call the .NET runtime. The following example shows an `sp_execute_external_script` using the .NET extension, and parameters for specifying path, script, and your custom code.

> [!NOTE]  
> You don't need to define which method to call. By default, a method called `execute` is called. This means that you need to follow the [Microsoft Extensibility SDK for C# for SQL Server](extensibility-sdk-c-sharp-sql-server.md) and implement an execute method in your C# class.

```sql
DECLARE @param1 INT;

SET @param1 = 3;

EXEC sp_execute_external_script @language = N'dotnet',
    @script = N'<packageName>.<ClassName>',
    @input_data_1 = N'<Input Query>',
    @param1 = @param1;
```

## <a id="external-library"></a> Use external library

In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, you can use external libraries for the C# language on Windows. You can compile your classes into a DLL file and upload the DLL and other dependencies into the database using the [CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md) DDL.

Example of how to upload a DLL file with external library:

```sql
CREATE EXTERNAL LIBRARY [dotnetlibrary]
FROM (CONTENT = '<local path to .dll file>')
WITH (LANGUAGE = 'dotnet');
GO
```

When it creates an external library, SQL Server automatically has access to the C# classes, and you don't need to set any special permissions to the path.

The following code is an example of calling a method in a class from a package, uploaded as an external library:

```sql
EXEC sp_execute_external_script
    @language = N'dotnet',
    @script = N'MyPackage.MyCLass',
    @input_data_1 = N'SELECT * FROM MYTABLE'
WITH RESULT SETS((column1 INT));
```

For more information, see [CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md).

## Related content

- [Tutorial: Search for a string using regular expressions (regex) in C#](../tutorials/search-for-string-using-regular-expressions-in-c-sharp.md)
