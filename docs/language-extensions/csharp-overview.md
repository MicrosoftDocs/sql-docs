---
title: What is C# Language Extension?
titleSuffix: SQL Server Language Extensions
description: C# Language Extension is a feature of SQL Server used for executing external C# code. Relational data can be used in the external C# code using the extensibility framework.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 09/28/2021
ms.topic: overview
ms.service: sql
ms.subservice: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
ms.custom:
  - intro-overview
---

# What is C# Language Extension?
[!INCLUDE [SQL Server 2019 and later](../includes/applies-to-version/sqlserver2019.md)]

The open-source [.NET C# language extension](https://github.com/microsoft/sql-server-language-extensions/tree/main/language-extensions/dotnet-core-CSharp) is a feature of [SQL Server Language Extensions](language-extensions-overview.md) that can be used for executing C# code within SQL Server. You can pass an existing SQL Server table to a C# application as a DataFrame, perform operations in C# using rich libraries, and obtain back a result set. This C# language extension allows you to reuse existing C# code, calculations, logic, or extensive libraries that provide functionality which you cannot get in T-SQL.

The external C# language is defined with [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md). The system stored procedure [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) is used as the interface for executing the C# code.

> [!NOTE]
> The C# language extension is compatible with SQL Server 2019 CU3 or later. Currently, it integrates .NET core on SQL Server Windows only.

## What you can do

The C# language extension uses the extensibility framework for executing external C# code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution. You can execute C# code at the data's source, eliminating the need to pull data across the network.

You can do data cleaning, fast data querying, or any other processing in C# that can occur through a DataFrame. By embedding C# code in stored procedures, you can push business logic down into the database for better performance. This helps avoid unnecessary data movement and latency because data does not need to be retrieved from SQL Server and moved into the app tier to do the business logic processing.

## Get started

The C# language extension is not installed with SQL Server. To install and configure the C# extension, see [.NET Core CSharp Language Extension](https://github.com/microsoft/sql-server-language-extensions/tree/main/language-extensions/dotnet-core-CSharp). The [Regex Sample](https://github.com/microsoft/sql-server-language-extensions/blob/main/language-extensions/dotnet-core-CSharp/sample/regex/README.md) tutorial shows you how to create a C# program that uses a regular expression to check text in a SQL table.

## Next steps

+ Install the [SQL Server Language Extensions on Windows](install/windows-java.md)
+ Install the [.NET Core CSharp Language Extension](https://github.com/microsoft/sql-server-language-extensions/tree/main/language-extensions/dotnet-core-CSharp)
