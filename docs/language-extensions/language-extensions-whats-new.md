---
title: What's new in SQL Server Language Extensions?
titleSuffix: 
description: Learn about what's new in SQL Server Language Extensions that expands, extends, and deepens the integration between external languages and the data platform.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 09/28/2021
ms.topic: conceptual
ms.service: sql
ms.subservice: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
ms.custom:
  - intro-whats-new
---

# What's new in SQL Server Language Extensions?
[!INCLUDE [SQL Server 2019 and later](../includes/applies-to-version/sqlserver2019.md)]

[Language Extension](language-extensions-overview.md) capabilities are added to SQL Server in each release as we continue to expand, extend, and deepen the integration between external languages and the data platform.

## SQL Server 2019

The new capabilities for [Language Extension](language-extensions-overview.md) in SQL Server 2019 can be found below. For more information about all of the features in this release, see [What's New in SQL Server 2019](../sql-server/what-s-new-in-sql-server-2019.md) and [Release Notes for SQL Server 2019](../sql-server/sql-server-2019-release-notes.md).

### New Python and R language extensions

- A [Python custom runtime](../machine-learning/install/custom-runtime-python.md) is available with Language Extensions. For more information, see how to [Install a Python custom runtime on Windows](../machine-learning/install/custom-runtime-python.md?view=sql-server-ver15&preserve-view=true) or [Install a Python custom runtime on Linux](../machine-learning/install/custom-runtime-python.md?view=sql-server-linux-ver15&preserve-view=true).

- An [R custom runtime](../machine-learning/install/custom-runtime-r.md) is available with Language Extensions. For more information, see how to [Install a R custom runtime on Windows](../machine-learning/install/custom-runtime-r.md?view=sql-server-ver15&preserve-view=true) or [Install a R custom runtime on Linux](../machine-learning/install/custom-runtime-r.md?view=sql-server-linux-ver15&preserve-view=true).

### New Java language extension

- The default Java Runtime on Windows and Linux is Open Zulu JRE and is included with the [SQL Server Language Extensions installation on Windows](install/windows-java.md) and [SQL Server Language Extensions installation on Linux](../linux/sql-server-linux-setup-language-extensions-java.md).
- Supported [Java data types](how-to/java-to-sql-data-types.md).
- [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md) for registering external language (for example, Java) in SQL Server.
- [Microsoft Extensibility SDK for Java](how-to/extensibility-sdk-java-sql-server.md).
- On Windows and Linux, Java code can be accessed in an external library using the [CREATE EXTERNAL LIBRARY (Transact-SQL)](../t-sql/statements/create-external-library-transact-sql.md) statement. Learn more: [How to call Java from SQL Server](how-to/call-java-from-sql.md).
- [Java language extension](language-extensions-overview.md) on  Windows and Linux. You can make compiled Java code available to SQL Server by assigning permissions and setting the path. Client apps with access SQL Server can use data and run your code by calling [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), the same procedure used for R and Python integration on SQL Server Machine Learning Services.

### New C# language extension

- A [C# language extension](csharp-overview.md) is available, supported by the [SQL Server Language Extensions](language-extensions-overview.md) extensibility framework.
- For details on how to install, configure, and use the extension, see [What is C# Language Extension?](csharp-overview.md)

## Next steps

+ Install [SQL Server Language Extensions on Windows](install/windows-java.md) or [on Linux](../linux/sql-server-linux-setup-language-extensions-java.md).