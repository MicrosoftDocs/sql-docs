---
title: What's new in SQL Server Language Extensions?
description: Learn about what's new in SQL Server Language Extensions that expands, extends, and deepens the integration between external languages and the data platform.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/29/2024
ms.service: sql
ms.subservice: language-extensions
ms.topic: conceptual
ms.custom:
  - intro-whats-new
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15"
---

# What's new in SQL Server Language Extensions?

[!INCLUDE [sqlserver2019-and-later](../includes/applies-to-version/sqlserver2019-and-later.md)]

[Language Extension](language-extensions-overview.md) capabilities are added to SQL Server in each release as we continue to expand, extend, and deepen the integration between external languages and the data platform.

## SQL Server 2019 and later versions

The new capabilities for [SQL Server Language Extensions](language-extensions-overview.md) in [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] and later versions are described in this article. For more information, review the following articles.

- [What's new in [!INCLUDE [sql-server-2019](../includes/sssql19-md.md)]](../sql-server/what-s-new-in-sql-server-2019.md)
- [What's new in [!INCLUDE [sql-server-2022](../includes/sssql22-md.md)]](../sql-server/what-s-new-in-sql-server-2022.md)
- [[!INCLUDE [SQL Server 2019](../includes/sssql19-md.md)] release notes](../sql-server/sql-server-2019-release-notes.md)
- [SQL Server 2022 release notes](../sql-server/sql-server-2022-release-notes.md)

### Language extensions available

### [Python and R language extensions](#tab/mlext)

- A [Python custom runtime](../machine-learning/install/custom-runtime-python.md) is available with Language Extensions. For more information, see how to [Install a Python custom runtime on Windows](../machine-learning/install/custom-runtime-python.md?view=sql-server-ver15&preserve-view=true) or [Install a Python custom runtime on Linux](../machine-learning/install/custom-runtime-python.md?view=sql-server-linux-ver15&preserve-view=true).

- An [R custom runtime](../machine-learning/install/custom-runtime-r.md) is available with Language Extensions. For more information, see how to [Install a R custom runtime on Windows](../machine-learning/install/custom-runtime-r.md?view=sql-server-ver15&preserve-view=true) or [Install a R custom runtime on Linux](../machine-learning/install/custom-runtime-r.md?view=sql-server-linux-ver15&preserve-view=true).

### [Java language extension](#tab/javaext)

- A [Java language extension](java-overview.md) is available, supported by the [SQL Server Language Extensions](language-extensions-overview.md) extensibility framework.

- The default Java Runtime on Windows and Linux is Open Zulu JRE and is included with the [SQL Server Language Extensions installation on Windows](install/windows-java.md) and [SQL Server Language Extensions installation on Linux](../linux/sql-server-linux-setup-language-extensions-java.md).

- [Java and SQL Server supported data types](how-to/java-to-sql-data-types.md).

- [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md) for registering external language (for example, Java) in SQL Server.

- [Microsoft Extensibility SDK for Java for SQL Server](how-to/extensibility-sdk-java-sql-server.md).

- On Windows and Linux, Java code can be accessed in an external library using the [CREATE EXTERNAL LIBRARY (Transact-SQL)](../t-sql/statements/create-external-library-transact-sql.md) statement. For more information, see [How to call the Java runtime in SQL Server Language Extensions](how-to/call-java-from-sql.md).

- [SQL Server Language Extensions](language-extensions-overview.md) on Windows and Linux. You can make compiled Java code available to SQL Server by assigning permissions and setting the path. Client apps with access SQL Server can use data and run your code by calling [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), the same procedure used for R and Python integration on SQL Server Machine Learning Services.

### [C# language extension](#tab/csharpext)

- A [C# language extension](csharp-overview.md) is available, supported by the [SQL Server Language Extensions](language-extensions-overview.md) extensibility framework.

- [Install SQL Server .NET Language Extension on Windows](install/windows-c-sharp.md).

- [C# and SQL Server supported data types](how-to/c-sharp-to-sql-data-types.md).

- [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md) for registering external language (for example, C#) in SQL Server.

- [Microsoft Extensibility SDK for C# for SQL Server](how-to/extensibility-sdk-c-sharp-sql-server.md).

- On Windows, C# code can be accessed in an external library using the [CREATE EXTERNAL LIBRARY (Transact-SQL)](../t-sql/statements/create-external-library-transact-sql.md) statement. For more information, see [How to call the .NET runtime in SQL Server Language Extensions](how-to/call-c-sharp-from-sql.md).

- [SQL Server Language Extensions](language-extensions-overview.md) on Windows. Client apps with access SQL Server can use data and run your code by calling [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), the same procedure used for Java, as well as R and Python integration on SQL Server Machine Learning Services.

---

## Related content

- [Install SQL Server Java Language Extension on Windows](install/windows-java.md)
- [Install SQL Server Java Language Extension on Linux](../linux/sql-server-linux-setup-language-extensions-java.md)
