---
title: "What is the C# Language Extension?"
titleSuffix: SQL Server Language Extensions
description: "The C# Language Extension is a feature of SQL Server used for executing external C# code. Relational data can be used in the external C# code using the extensibility framework."
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/29/2024
ms.service: sql
ms.subservice: language-extensions
ms.topic: overview
ms.custom:
  - intro-overview
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15"
---

# What is the C# Language Extension?

[!INCLUDE [sqlserver2019-and-later](../includes/applies-to-version/sqlserver2019-and-later.md)]

The C# Language Extension is a feature of [SQL Server Language Extensions](language-extensions-overview.md) that can be used for executing C# code within SQL Server. You can pass an existing SQL Server table to a C# application as a DataFrame, perform operations in C# using rich libraries, and obtain back a result set. This C# language extension allows you to reuse existing C# code, calculations, logic, or extensive libraries that provide functionality you can't get in Transact-SQL (T-SQL).

The external C# language is defined with [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md). The system stored procedure [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) is used as the interface for executing the C# code.

> [!NOTE]  
> The C# language extension is compatible with [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] CU 3 and later versions. Currently, it integrates .NET Core on SQL Server for Windows only. Linux isn't supported.

## What you can do

The C# language extension uses the extensibility framework for executing external C# code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution. You can execute C# code at the data's source, eliminating the need to pull data across the network.

You can do data cleaning, fast data querying, or any other processing in C# that can occur through a DataFrame. By embedding C# code in stored procedures, you can push business logic down into the database for better performance. This helps avoid unnecessary data movement and latency, because data doesn't need to be retrieved from SQL Server and moved into the app tier to do the business logic processing.

## Get started

1. [Install SQL Server .NET Language Extension on Windows](install/windows-c-sharp.md).

1. Configure development tools.

   - Use the IDE you prefer for developing C# code.
   - Install the [Microsoft Extensibility SDK for C# for SQL Server](how-to/extensibility-sdk-c-sharp-sql-server.md) to execute C# code on SQL Server.
   - Use [SQL Server Management Studio (SSMS)](../ssms/sql-server-management-studio-ssms.md) or [Azure Data Studio](/azure-data-studio/what-is-azure-data-studio) for executing external code on SQL Server.
   - Use the system stored procedure [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) to execute your C# code on SQL Server.

1. Write your first C# code. Use the following tutorial as a starting point. [Tutorial: Search for a string using regular expressions (regex) in C#](tutorials/search-for-string-using-regular-expressions-in-c-sharp.md).

## Related content

- [Install SQL Server .NET Language Extension on Windows](install/windows-c-sharp.md)
- [Microsoft Extensibility SDK for C# for SQL Server](how-to/extensibility-sdk-c-sharp-sql-server.md)
- [Install SQL Server Java Language Extension on Windows](install/windows-java.md)
- [Security architecture for the extensibility framework in SQL Server Machine Learning Services](../machine-learning/concepts/security.md)
