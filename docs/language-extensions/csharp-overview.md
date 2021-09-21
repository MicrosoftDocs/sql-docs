---
title: What is C# Language Extension?
titleSuffix: SQL Server Language Extensions
description: C# Language Extension is a feature of SQL Server used for executing external C# code. Relational data can be used in the external C# code using the extensibility framework.
author: garyericson
ms.author: garye
ms.date: 09/21/2021
ms.topic: overview
ms.prod: sql
ms.technology: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
ms.custom:
  - intro-overview
---
# What is C# Language Extension?
[!INCLUDE [SQL Server 2019 and later](../includes/applies-to-version/sqlserver2019.md)]

C# Language Extension is a feature of SQL Server used for executing external C# code. The relational data can be used in the external C# code using the [extensibility framework](concepts/extensibility-framework.md). The C# Language Extension is supported by [SQL Server Language Extensions](language-extensions-overview.md).

***&#709;&#709;&#709; UPDATE HERE &#709;&#709;&#709;***

The default Java runtime is Zulu Open JRE. You can also use another Java JRE or SDK.

***&#708;&#708;&#708; UPDATE HERE &#708;&#708;&#708;***

## What you can do with the C# Language Extension

The C# Language Extension uses the extensibility framework for executing external C# code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution. You can execute C# code at the data's source, eliminating the need to pull data across the network.

The external C# language is defined with [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md). The system stored procedure [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) is used as the interface for executing the C# code.

## Get started with C# Language Extension

***&#709;&#709;&#709; UPDATE HERE &#709;&#709;&#709;***

1. [Install SQL Server Java Language Extension on Windows](install/windows-java.md) or [on Linux](../linux/sql-server-linux-setup-language-extensions-java.md).

1. Configure a development tools.

    + Use the IDE you prefer for developing Java code.
    + Install the [Microsoft Extensibility SDK for Java](how-to/extensibility-sdk-java-sql-server.md) to execute Java code on SQL Server.
    + Use [Azure Data Studio](../azure-data-studio/what-is-azure-data-studio.md) for executing external code on SQL Server.
    + Use the system stored procedure [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) to execute your Java code on SQL Server.

1. Write your first Java code.

    + [Tutorial: Regular expressions with Java](tutorials/search-for-string-using-regular-expressions-in-java.md)

***&#708;&#708;&#708; UPDATE HERE &#708;&#708;&#708;***

***&#709;&#709;&#709; UPDATE HERE &#709;&#709;&#709;***

## Limitations

The number of values in input and output buffers can't exceed `MAX_INT (2^31-1)` since that is the maximum number of elements that can be allocated in an array in Java.

***&#708;&#708;&#708; UPDATE HERE &#708;&#708;&#708;***

## Next steps

***&#709;&#709;&#709; UPDATE HERE &#709;&#709;&#709;***

+ Install the [SQL Server Java Language Extension on Windows](install/windows-java.md) or [on Linux](../linux/sql-server-linux-setup-language-extensions-java.md)
+ Install the [Microsoft Extensibility SDK for Java](how-to/extensibility-sdk-java-sql-server.md)
