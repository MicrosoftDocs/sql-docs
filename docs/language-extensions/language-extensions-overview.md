---
title: What is SQL Server Language Extensions?
titleSuffix: 
description: Language Extensions is a feature of SQL Server used for executing external code. In SQL Server, Java, C#, Python, and R are supported. Relational data can be used in the external code using the extensibility framework.
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
# What is SQL Server Language Extensions?
[!INCLUDE [SQL Server 2019 and later](../includes/applies-to-version/sqlserver2019.md)]

Language Extensions is a feature of SQL Server used for executing external code. The relational data can be used in the external code using the [extensibility framework](concepts/extensibility-framework.md). In SQL Server 2019, Java, C#, Python, and R runtimes are supported.

> [!NOTE]
> For executing Python or R in SQL Server, see the [Machine Learning Services](../machine-learning/sql-server-machine-learning-services.md) documentation. With SQL Server 2019 and later, you can use a custom Python and R runtime with Language Extensions. For more information, see how to install the [Python custom runtime](../machine-learning/install/custom-runtime-python.md) and the [R custom runtime](../machine-learning/install/custom-runtime-r.md).

## What you can do with Language Extensions

Language Extensions uses the [extensibility framework](concepts/extensibility-framework.md) for executing external code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution. You can execute code at the data's source, eliminating the need to pull data across the network.

External languages are defined with [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md). The system stored procedure [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) is used as the interface for executing the code.

Language Extensions provides multiple advantages:

+ Data security. Bringing external language execution closer to the source of data avoids insecure data movement.
+ Speed. Databases are optimized for set-based operations. 
+ Ease of deployment and integration. [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] is the central point of operations for many other data management tasks and applications. By using data in the database, you ensure that the data used by the language extension is consistent and up-to-date.

The native [SQL common language runtime (CLR)](../relational-databases/clr-integration/common-language-runtime-integration-overview.md) allows you to implement some of the functionalities of SQL Server with .NET languages. For a discussion of the differences between SQL CLR and SQL language extensions, see [Compare SQL Server Language Extensions to SQL CLR](concepts/compare-extensibility-to-clr.md).

## Next steps

+ Install the [Java language extension on Windows](install/windows-java.md) or [on Linux](../linux/sql-server-linux-setup-language-extensions-java.md)
+ Install the [C# language extension on Windows](csharp-overview.md)
+ Install your [Python custom runtime for SQL Server](../machine-learning/install/custom-runtime-python.md)
+ Install your [R custom runtime for SQL Server](../machine-learning/install/custom-runtime-r.md)
+ Install the [Microsoft Extensibility SDK for Java](how-to/extensibility-sdk-java-sql-server.md)
